# -*-coding:utf-8-*-
import sys
import serial
import serial.tools.list_ports
from PyQt5 import QtWidgets
from PyQt5.QtWidgets import QMessageBox, QStyleFactory
from PyQt5.QtCore import QTimer
from ForceSensor import Ui_ForceSensor
import tensorflow as tf
import Online_TF
import numpy as np



class Pyqt5_Serial(QtWidgets.QMainWindow, Ui_ForceSensor):
    def __init__(self):
        super(Pyqt5_Serial, self).__init__()
        self.open_flag = 0
        self.enter_flag = 0
        self.para_flag = 0
        self.setupUi(self)
        self.init()
        self.ser1 = serial.Serial()
        self.ser2 = serial.Serial()
        self.port_check()
        self.rcv_bytes1 = ''
        self.rcv_bytes2 = ''
        self.X_train = np.zeros((6, 1))
        self.Y_train = np.zeros((6, 1))
        self.zero = np.zeros((6, 1))
        self.parameters = None
        # Initialize parameters
        # self.parameters = Online_TF.initialize_parameters()

    def init(self):
        if self.open_flag == 0:
            #
            self.open_button.clicked.connect(self.port_open)
        else:
            # 关闭串口按钮
            self.open_button.clicked.connect(self.port_close)
        self.decp_button.clicked.connect(self.decouple_start)
        self.zero_button.clicked.connect(self.set_zero)
        # 定时器接收数据
        self.timer = QTimer(self)
        self.timer.timeout.connect(self.data_receive)

        self.timer1 = QTimer(self)
        self.timer1.timeout.connect(self.online_decouple)


        # 发送数据按钮
        # self.s3__send_button.clicked.connect(self.data_send)

        # # 定时发送数据
        # self.timer_send = QTimer()
        # self.timer_send.timeout.connect(self.data_send)
        # self.timer_send_cb.stateChanged.connect(self.data_send_timer)

        # # 清除发送窗口
        # self.s3__clear_button.clicked.connect(self.send_data_clear)
        #
        # # 清除接收窗口
        # self.s2__clear_button.clicked.connect(self.receive_data_clear)

    # 串口检测
    def port_check(self):
        # 检测所有存在的串口，将信息存储在字典中
        self.Com_Dict = {}
        port_list = list(serial.tools.list_ports.comports())
        self.comboBox.clear()
        self.comboBox_2.clear()
        for port in port_list:
            self.Com_Dict["%s" % port[0]] = "%s" % port[1]
            self.comboBox.addItem(port[0])
            self.comboBox_2.addItem(port[0])

    # 打开串口
    def port_open(self):
        # self.ser1.port = self.comboBox.currentText()
        self.ser1.port = '/dev/ttyUSB2'
        self.ser1.baudrate = 9600
        self.ser1.bytesize = 8
        self.ser1.stopbits = 1

        # self.ser2.port = self.comboBox_2.currentText()
        self.ser2.port = '/dev/ttyUSB5'
        self.ser2.baudrate = 9600
        self.ser2.bytesize = 8
        self.ser2.stopbits = 1

        self.open_flag = 1

        try:
            self.ser1.open()
            self.ser2.open()
        except:
            QMessageBox.critical(self, "Port Error", "此串口不能被打开！")
            return None

        # 打开串口接收定时器，周期为50ms
        self.timer.start(50)

        if self.ser1.isOpen() & self.ser2.isOpen():
            self.open_button.setText("关闭串口")
            # self.formGroupBox1.setTitle("串口状态（已开启）")

    # 关闭串口
    def port_close(self):
        self.timer.stop()
        self.timer_send.stop()
        try:
            self.ser1.close()
            self.ser2.close()
        except:
            pass
        self.open_button.setEnabled(True)
        # 接收数据和发送数据数目置零
        self.data_num_received = 0
        self.open_flag = 0
        # self.formGroupBox1.setTitle("串口状态（已关闭）")

    def data_diff(self):
        try:
            self.R1.setText(str(float(self.U2.text()) - float(self.U3.text())))
            self.R2.setText(str(float(self.U4.text()) - float(self.U5.text())))
            self.R3.setText(str(float(self.U6.text()) - float(self.U7.text())))
            self.R4.setText(str(float(self.D2.text()) - float(self.D3.text())))
            self.R5.setText(str(float(self.D4.text()) - float(self.D5.text())))
            self.R6.setText(str(float(self.D6.text()) - float(self.D7.text())))
        except:
            print('Wrong string')
            None

    def set_zero(self):
        self.zero[0] = float(self.R1.text())
        self.zero[1] = float(self.R2.text())
        self.zero[2] = float(self.R3.text())
        self.zero[3] = float(self.R4.text())
        self.zero[4] = float(self.R5.text())
        self.zero[5] = float(self.R6.text())

    # # 发送数据
    # def data_send(self):
    #     if self.ser.isOpen():
    #         input_s = self.s3__send_text.toPlainText()
    #         if input_s != "":
    #             # 非空字符串
    #             if self.hex_send.isChecked():
    #                 # hex发送
    #                 input_s = input_s.strip()
    #                 send_list = []
    #                 while input_s != '':
    #                     try:
    #                         num = int(input_s[0:2], 16)
    #                     except ValueError:
    #                         QMessageBox.critical(self, 'wrong data', '请输入十六进制数据，以空格分开!')
    #                         return None
    #                     input_s = input_s[2:].strip()
    #                     send_list.append(num)
    #                 input_s = bytes(send_list)
    #             else:
    #                 # ascii发送
    #                 input_s = (input_s + '\r\n').encode('utf-8')
    #
    #             num = self.ser.write(input_s)
    #             self.data_num_sended += num
    #             self.lineEdit_2.setText(str(self.data_num_sended))
    #     else:
    #         pass

    # 接收数据
    def data_receive(self):
        self.rcv_bytes1 = self.ser1.readline()
        self.rcv_bytes2 = self.ser2.readline()

        data1 = str(self.rcv_bytes1).lstrip("b'")
        data1 = data1.rstrip(", \\n'")
        self.datalist1 = data1.split(', ')

        data2 = str(self.rcv_bytes2).lstrip("b'")
        data2 = data2.rstrip(", \\n'")
        self.datalist2 = data2.split(', ')

        # if self.datalist1[0] != "" \
        #         and self.datalist1[0] != "\n" and self.datalist1[0] != "\r\n" \
        #         and len(self.datalist1) == 8:
        if self.enter_flag == 1:
            first1 = float(self.datalist1[0])
            if first1 > 2400000 or first1 < 10000:
                self.U1.setText(self.datalist1[0])
                self.U2.setText(self.datalist1[1])
                self.U3.setText(self.datalist1[2])
                self.U4.setText(self.datalist1[3])
                self.U5.setText(self.datalist1[4])
                self.U6.setText(self.datalist1[5])
                self.U7.setText(self.datalist1[6])
                self.U8.setText(self.datalist1[7])

        # if self.datalist2[0] != "" \
        #         and self.datalist2[0] != "\n" and self.datalist2[0] != "\r\n" \
        #         and len(self.datalist2) == 8:
        if self.enter_flag == 1:
            try:
                first2 = float(self.datalist2[0])
                if first2 < 1000:
                    self.D1.setText(self.datalist2[0])
                    self.D2.setText(self.datalist2[1])
                    self.D3.setText(self.datalist2[2])
                    self.D4.setText(self.datalist2[3])
                    self.D5.setText(self.datalist2[4])
                    self.D6.setText(self.datalist2[5])
                    self.D7.setText(self.datalist2[6])
                    self.D8.setText(self.datalist2[7])
            except:
                print('Wrong string')
                None
        self.enter_flag = 1
        self.data_diff()

    def decouple_start(self):
        self.timer1.start(50)

    def decouple_show(self, z: object) -> object:
        self.DE1.setText(str(z[0][0]))
        self.DE2.setText(str(z[1][0]))
        self.DE3.setText(str(z[2][0]))
        self.DE4.setText(str(z[3][0]))
        self.DE5.setText(str(z[4][0]))
        self.DE6.setText(str(z[5][0]))

    def online_decouple(self):
        self.Y_train[0][0] = float(self.label1.toPlainText())
        self.Y_train[1][0] = float(self.label2.toPlainText())
        self.Y_train[2][0] = float(self.label3.toPlainText())
        self.Y_train[3][0] = float(self.label4.toPlainText())
        self.Y_train[4][0] = float(self.label5.toPlainText())
        self.Y_train[5][0] = float(self.label6.toPlainText())

        self.X_train[0][0] = float(self.R1.text())
        self.X_train[1][0] = float(self.R2.text())
        self.X_train[2][0] = float(self.R3.text())
        self.X_train[3][0] = float(self.R4.text())
        self.X_train[4][0] = float(self.R5.text())
        self.X_train[5][0] = float(self.R6.text())

        if self.para_flag == 0:
            self.parameters = Online_TF.model(self, X_train=self.X_train, Y_train=self.Y_train)
            self.para_flag = 1
            print("You are already successed at the first time!")
        else:
            self.parameters = Online_TF.model(self, X_train=self.X_train, Y_train=self.Y_train,
                                              parameters=self.parameters)



    # # 定时发送数据
    # def data_send_timer(self):
    #     if self.timer_send_cb.isChecked():
    #         self.timer_send.start(int(self.lineEdit_3.text()))
    #         self.lineEdit_3.setEnabled(False)
    #     else:
    #         self.timer_send.stop()
    #         self.lineEdit_3.setEnabled(True)
    #
    # # 清除显示
    # def send_data_clear(self):
    #     self.s3__send_text.setText("")
    #
    # def receive_data_clear(self):
    #     self.s2__receive_text.setText("")


if __name__ == '__main__':
    app = QtWidgets.QApplication(sys.argv)
    app.setStyle(QStyleFactory.create('GTK+'))
    font = app.font()
    font.setPointSize(14)
    app.setFont(font)
    myshow = Pyqt5_Serial()
    myshow.show()
    sys.exit(app.exec_())
