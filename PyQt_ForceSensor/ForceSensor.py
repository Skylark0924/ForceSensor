# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'ForceSensor.ui'
#
# Created by: PyQt5 UI code generator 5.6
#
# WARNING! All changes made in this file will be lost!

from PyQt5 import QtCore, QtGui, QtWidgets

class Ui_ForceSensor(object):
    def setupUi(self, ForceSensor):
        ForceSensor.setObjectName("ForceSensor")
        ForceSensor.resize(1353, 638)
        self.centralwidget = QtWidgets.QWidget(ForceSensor)
        self.centralwidget.setObjectName("centralwidget")
        self.original_data = QtWidgets.QGroupBox(self.centralwidget)
        self.original_data.setGeometry(QtCore.QRect(20, 20, 651, 361))
        self.original_data.setObjectName("original_data")
        self.open_button = QtWidgets.QPushButton(self.original_data)
        self.open_button.setGeometry(QtCore.QRect(540, 320, 100, 28))
        self.open_button.setObjectName("open_button")
        self.comboBox_2 = QtWidgets.QComboBox(self.original_data)
        self.comboBox_2.setGeometry(QtCore.QRect(20, 190, 160, 28))
        self.comboBox_2.setObjectName("comboBox_2")
        self.comboBox = QtWidgets.QComboBox(self.original_data)
        self.comboBox.setGeometry(QtCore.QRect(20, 60, 160, 28))
        self.comboBox.setObjectName("comboBox")
        self.gridLayoutWidget_2 = QtWidgets.QWidget(self.original_data)
        self.gridLayoutWidget_2.setGeometry(QtCore.QRect(20, 230, 621, 71))
        self.gridLayoutWidget_2.setObjectName("gridLayoutWidget_2")
        self.gridLayout_3 = QtWidgets.QGridLayout(self.gridLayoutWidget_2)
        self.gridLayout_3.setContentsMargins(0, 0, 0, 0)
        self.gridLayout_3.setObjectName("gridLayout_3")
        self.D1 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        self.D1.setObjectName("D1")
        self.gridLayout_3.addWidget(self.D1, 0, 1, 1, 1)
        self.label_8 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_8.sizePolicy().hasHeightForWidth())
        self.label_8.setSizePolicy(sizePolicy)
        self.label_8.setObjectName("label_8")
        self.gridLayout_3.addWidget(self.label_8, 0, 4, 1, 1)
        self.label_4 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_4.sizePolicy().hasHeightForWidth())
        self.label_4.setSizePolicy(sizePolicy)
        self.label_4.setObjectName("label_4")
        self.gridLayout_3.addWidget(self.label_4, 0, 0, 1, 1)
        self.D3 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        self.D3.setObjectName("D3")
        self.gridLayout_3.addWidget(self.D3, 0, 5, 1, 1)
        self.D4 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        self.D4.setObjectName("D4")
        self.gridLayout_3.addWidget(self.D4, 0, 7, 1, 1)
        self.label_6 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_6.sizePolicy().hasHeightForWidth())
        self.label_6.setSizePolicy(sizePolicy)
        self.label_6.setObjectName("label_6")
        self.gridLayout_3.addWidget(self.label_6, 0, 2, 1, 1)
        self.label_16 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_16.sizePolicy().hasHeightForWidth())
        self.label_16.setSizePolicy(sizePolicy)
        self.label_16.setObjectName("label_16")
        self.gridLayout_3.addWidget(self.label_16, 0, 6, 1, 1)
        self.D2 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        self.D2.setObjectName("D2")
        self.gridLayout_3.addWidget(self.D2, 0, 3, 1, 1)
        self.label_10 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_10.sizePolicy().hasHeightForWidth())
        self.label_10.setSizePolicy(sizePolicy)
        self.label_10.setObjectName("label_10")
        self.gridLayout_3.addWidget(self.label_10, 1, 0, 1, 1)
        self.label_12 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_12.sizePolicy().hasHeightForWidth())
        self.label_12.setSizePolicy(sizePolicy)
        self.label_12.setObjectName("label_12")
        self.gridLayout_3.addWidget(self.label_12, 1, 2, 1, 1)
        self.D5 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        self.D5.setObjectName("D5")
        self.gridLayout_3.addWidget(self.D5, 1, 1, 1, 1)
        self.D6 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        self.D6.setObjectName("D6")
        self.gridLayout_3.addWidget(self.D6, 1, 3, 1, 1)
        self.label_13 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_13.sizePolicy().hasHeightForWidth())
        self.label_13.setSizePolicy(sizePolicy)
        self.label_13.setObjectName("label_13")
        self.gridLayout_3.addWidget(self.label_13, 1, 4, 1, 1)
        self.D7 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        self.D7.setObjectName("D7")
        self.gridLayout_3.addWidget(self.D7, 1, 5, 1, 1)
        self.label_17 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_17.sizePolicy().hasHeightForWidth())
        self.label_17.setSizePolicy(sizePolicy)
        self.label_17.setObjectName("label_17")
        self.gridLayout_3.addWidget(self.label_17, 1, 6, 1, 1)
        self.D8 = QtWidgets.QLabel(self.gridLayoutWidget_2)
        self.D8.setObjectName("D8")
        self.gridLayout_3.addWidget(self.D8, 1, 7, 1, 1)
        self.gridLayoutWidget = QtWidgets.QWidget(self.original_data)
        self.gridLayoutWidget.setGeometry(QtCore.QRect(20, 100, 621, 81))
        self.gridLayoutWidget.setObjectName("gridLayoutWidget")
        self.gridLayout = QtWidgets.QGridLayout(self.gridLayoutWidget)
        self.gridLayout.setSizeConstraint(QtWidgets.QLayout.SetDefaultConstraint)
        self.gridLayout.setContentsMargins(0, 0, 0, 0)
        self.gridLayout.setObjectName("gridLayout")
        self.U1 = QtWidgets.QLabel(self.gridLayoutWidget)
        self.U1.setObjectName("U1")
        self.gridLayout.addWidget(self.U1, 0, 1, 1, 1)
        self.label_7 = QtWidgets.QLabel(self.gridLayoutWidget)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_7.sizePolicy().hasHeightForWidth())
        self.label_7.setSizePolicy(sizePolicy)
        self.label_7.setObjectName("label_7")
        self.gridLayout.addWidget(self.label_7, 0, 4, 1, 1)
        self.label_2 = QtWidgets.QLabel(self.gridLayoutWidget)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Minimum)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_2.sizePolicy().hasHeightForWidth())
        self.label_2.setSizePolicy(sizePolicy)
        self.label_2.setObjectName("label_2")
        self.gridLayout.addWidget(self.label_2, 0, 0, 1, 1)
        self.U3 = QtWidgets.QLabel(self.gridLayoutWidget)
        self.U3.setObjectName("U3")
        self.gridLayout.addWidget(self.U3, 0, 5, 1, 1)
        self.U4 = QtWidgets.QLabel(self.gridLayoutWidget)
        self.U4.setObjectName("U4")
        self.gridLayout.addWidget(self.U4, 0, 7, 1, 1)
        self.label_5 = QtWidgets.QLabel(self.gridLayoutWidget)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_5.sizePolicy().hasHeightForWidth())
        self.label_5.setSizePolicy(sizePolicy)
        self.label_5.setObjectName("label_5")
        self.gridLayout.addWidget(self.label_5, 0, 2, 1, 1)
        self.label_14 = QtWidgets.QLabel(self.gridLayoutWidget)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_14.sizePolicy().hasHeightForWidth())
        self.label_14.setSizePolicy(sizePolicy)
        self.label_14.setObjectName("label_14")
        self.gridLayout.addWidget(self.label_14, 0, 6, 1, 1)
        self.U2 = QtWidgets.QLabel(self.gridLayoutWidget)
        self.U2.setObjectName("U2")
        self.gridLayout.addWidget(self.U2, 0, 3, 1, 1)
        self.label_3 = QtWidgets.QLabel(self.gridLayoutWidget)
        self.label_3.setObjectName("label_3")
        self.gridLayout.addWidget(self.label_3, 1, 0, 1, 1)
        self.label_11 = QtWidgets.QLabel(self.gridLayoutWidget)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_11.sizePolicy().hasHeightForWidth())
        self.label_11.setSizePolicy(sizePolicy)
        self.label_11.setObjectName("label_11")
        self.gridLayout.addWidget(self.label_11, 1, 2, 1, 1)
        self.U5 = QtWidgets.QLabel(self.gridLayoutWidget)
        self.U5.setObjectName("U5")
        self.gridLayout.addWidget(self.U5, 1, 1, 1, 1)
        self.U6 = QtWidgets.QLabel(self.gridLayoutWidget)
        self.U6.setObjectName("U6")
        self.gridLayout.addWidget(self.U6, 1, 3, 1, 1)
        self.label_9 = QtWidgets.QLabel(self.gridLayoutWidget)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_9.sizePolicy().hasHeightForWidth())
        self.label_9.setSizePolicy(sizePolicy)
        self.label_9.setObjectName("label_9")
        self.gridLayout.addWidget(self.label_9, 1, 4, 1, 1)
        self.U7 = QtWidgets.QLabel(self.gridLayoutWidget)
        self.U7.setObjectName("U7")
        self.gridLayout.addWidget(self.U7, 1, 5, 1, 1)
        self.label_15 = QtWidgets.QLabel(self.gridLayoutWidget)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_15.sizePolicy().hasHeightForWidth())
        self.label_15.setSizePolicy(sizePolicy)
        self.label_15.setObjectName("label_15")
        self.gridLayout.addWidget(self.label_15, 1, 6, 1, 1)
        self.U8 = QtWidgets.QLabel(self.gridLayoutWidget)
        self.U8.setObjectName("U8")
        self.gridLayout.addWidget(self.U8, 1, 7, 1, 1)
        self.diiff_data = QtWidgets.QGroupBox(self.centralwidget)
        self.diiff_data.setGeometry(QtCore.QRect(20, 400, 651, 181))
        self.diiff_data.setObjectName("diiff_data")
        self.zero_button = QtWidgets.QPushButton(self.diiff_data)
        self.zero_button.setGeometry(QtCore.QRect(540, 130, 100, 28))
        self.zero_button.setObjectName("zero_button")
        self.gridLayoutWidget_3 = QtWidgets.QWidget(self.diiff_data)
        self.gridLayoutWidget_3.setGeometry(QtCore.QRect(20, 40, 621, 71))
        self.gridLayoutWidget_3.setObjectName("gridLayoutWidget_3")
        self.gridLayout_4 = QtWidgets.QGridLayout(self.gridLayoutWidget_3)
        self.gridLayout_4.setContentsMargins(0, 0, 0, 0)
        self.gridLayout_4.setObjectName("gridLayout_4")
        self.label_18 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_18.sizePolicy().hasHeightForWidth())
        self.label_18.setSizePolicy(sizePolicy)
        self.label_18.setObjectName("label_18")
        self.gridLayout_4.addWidget(self.label_18, 0, 4, 1, 1)
        self.label_23 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_23.sizePolicy().hasHeightForWidth())
        self.label_23.setSizePolicy(sizePolicy)
        self.label_23.setObjectName("label_23")
        self.gridLayout_4.addWidget(self.label_23, 1, 2, 1, 1)
        self.label_19 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_19.sizePolicy().hasHeightForWidth())
        self.label_19.setSizePolicy(sizePolicy)
        self.label_19.setObjectName("label_19")
        self.gridLayout_4.addWidget(self.label_19, 0, 0, 1, 1)
        self.R1 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        self.R1.setObjectName("R1")
        self.gridLayout_4.addWidget(self.R1, 0, 1, 1, 1)
        self.label_20 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_20.sizePolicy().hasHeightForWidth())
        self.label_20.setSizePolicy(sizePolicy)
        self.label_20.setObjectName("label_20")
        self.gridLayout_4.addWidget(self.label_20, 0, 2, 1, 1)
        self.R2 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        self.R2.setObjectName("R2")
        self.gridLayout_4.addWidget(self.R2, 0, 3, 1, 1)
        self.label_22 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_22.sizePolicy().hasHeightForWidth())
        self.label_22.setSizePolicy(sizePolicy)
        self.label_22.setObjectName("label_22")
        self.gridLayout_4.addWidget(self.label_22, 1, 0, 1, 1)
        self.R3 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        self.R3.setObjectName("R3")
        self.gridLayout_4.addWidget(self.R3, 0, 5, 1, 1)
        self.R4 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        self.R4.setObjectName("R4")
        self.gridLayout_4.addWidget(self.R4, 1, 1, 1, 1)
        self.R5 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        self.R5.setObjectName("R5")
        self.gridLayout_4.addWidget(self.R5, 1, 3, 1, 1)
        self.R6 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        self.R6.setObjectName("R6")
        self.gridLayout_4.addWidget(self.R6, 1, 5, 1, 1)
        self.label_24 = QtWidgets.QLabel(self.gridLayoutWidget_3)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_24.sizePolicy().hasHeightForWidth())
        self.label_24.setSizePolicy(sizePolicy)
        self.label_24.setObjectName("label_24")
        self.gridLayout_4.addWidget(self.label_24, 1, 4, 1, 1)
        self.save_button = QtWidgets.QPushButton(self.diiff_data)
        self.save_button.setGeometry(QtCore.QRect(400, 130, 100, 28))
        self.save_button.setObjectName("save_button")
        self.decp_data = QtWidgets.QGroupBox(self.centralwidget)
        self.decp_data.setGeometry(QtCore.QRect(680, 20, 651, 141))
        self.decp_data.setObjectName("decp_data")
        self.gridLayoutWidget_6 = QtWidgets.QWidget(self.decp_data)
        self.gridLayoutWidget_6.setGeometry(QtCore.QRect(20, 40, 621, 71))
        self.gridLayoutWidget_6.setObjectName("gridLayoutWidget_6")
        self.gridLayout_decp = QtWidgets.QGridLayout(self.gridLayoutWidget_6)
        self.gridLayout_decp.setContentsMargins(0, 0, 0, 0)
        self.gridLayout_decp.setObjectName("gridLayout_decp")
        self.label_40 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_40.sizePolicy().hasHeightForWidth())
        self.label_40.setSizePolicy(sizePolicy)
        self.label_40.setObjectName("label_40")
        self.gridLayout_decp.addWidget(self.label_40, 0, 4, 1, 1)
        self.label_41 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_41.sizePolicy().hasHeightForWidth())
        self.label_41.setSizePolicy(sizePolicy)
        self.label_41.setObjectName("label_41")
        self.gridLayout_decp.addWidget(self.label_41, 1, 2, 1, 1)
        self.label_42 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_42.sizePolicy().hasHeightForWidth())
        self.label_42.setSizePolicy(sizePolicy)
        self.label_42.setObjectName("label_42")
        self.gridLayout_decp.addWidget(self.label_42, 0, 0, 1, 1)
        self.DE1 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        self.DE1.setObjectName("DE1")
        self.gridLayout_decp.addWidget(self.DE1, 0, 1, 1, 1)
        self.label_43 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_43.sizePolicy().hasHeightForWidth())
        self.label_43.setSizePolicy(sizePolicy)
        self.label_43.setObjectName("label_43")
        self.gridLayout_decp.addWidget(self.label_43, 0, 2, 1, 1)
        self.DE2 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        self.DE2.setObjectName("DE2")
        self.gridLayout_decp.addWidget(self.DE2, 0, 3, 1, 1)
        self.label_44 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_44.sizePolicy().hasHeightForWidth())
        self.label_44.setSizePolicy(sizePolicy)
        self.label_44.setObjectName("label_44")
        self.gridLayout_decp.addWidget(self.label_44, 1, 0, 1, 1)
        self.DE3 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        self.DE3.setObjectName("DE3")
        self.gridLayout_decp.addWidget(self.DE3, 0, 5, 1, 1)
        self.DE4 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        self.DE4.setObjectName("DE4")
        self.gridLayout_decp.addWidget(self.DE4, 1, 1, 1, 1)
        self.DE5 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        self.DE5.setObjectName("DE5")
        self.gridLayout_decp.addWidget(self.DE5, 1, 3, 1, 1)
        self.DE6 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        self.DE6.setObjectName("DE6")
        self.gridLayout_decp.addWidget(self.DE6, 1, 5, 1, 1)
        self.label_45 = QtWidgets.QLabel(self.gridLayoutWidget_6)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_45.sizePolicy().hasHeightForWidth())
        self.label_45.setSizePolicy(sizePolicy)
        self.label_45.setObjectName("label_45")
        self.gridLayout_decp.addWidget(self.label_45, 1, 4, 1, 1)
        self.label_data = QtWidgets.QGroupBox(self.centralwidget)
        self.label_data.setGeometry(QtCore.QRect(680, 170, 651, 281))
        self.label_data.setObjectName("label_data")
        self.gridLayoutWidget_7 = QtWidgets.QWidget(self.label_data)
        self.gridLayoutWidget_7.setGeometry(QtCore.QRect(20, 60, 621, 148))
        self.gridLayoutWidget_7.setObjectName("gridLayoutWidget_7")
        self.gridLayout_8 = QtWidgets.QGridLayout(self.gridLayoutWidget_7)
        self.gridLayout_8.setContentsMargins(0, 0, 0, 0)
        self.gridLayout_8.setObjectName("gridLayout_8")
        self.label_46 = QtWidgets.QLabel(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_46.sizePolicy().hasHeightForWidth())
        self.label_46.setSizePolicy(sizePolicy)
        self.label_46.setObjectName("label_46")
        self.gridLayout_8.addWidget(self.label_46, 0, 6, 1, 1)
        self.label5 = QtWidgets.QTextEdit(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Maximum)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label5.sizePolicy().hasHeightForWidth())
        self.label5.setSizePolicy(sizePolicy)
        self.label5.setObjectName("label5")
        self.label5.setText('0')
        self.gridLayout_8.addWidget(self.label5, 1, 5, 1, 1)
        self.label2 = QtWidgets.QTextEdit(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Maximum)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label2.sizePolicy().hasHeightForWidth())
        self.label2.setSizePolicy(sizePolicy)
        self.label2.setObjectName("label2")
        self.label2.setText('0')
        self.gridLayout_8.addWidget(self.label2, 0, 5, 1, 1)
        self.label1 = QtWidgets.QTextEdit(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Maximum)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label1.sizePolicy().hasHeightForWidth())
        self.label1.setSizePolicy(sizePolicy)
        self.label1.setObjectName("label1")
        self.label1.setText('0')
        self.gridLayout_8.addWidget(self.label1, 0, 1, 1, 1)
        self.label_49 = QtWidgets.QLabel(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_49.sizePolicy().hasHeightForWidth())
        self.label_49.setSizePolicy(sizePolicy)
        self.label_49.setObjectName("label_49")
        self.gridLayout_8.addWidget(self.label_49, 0, 4, 1, 1)
        self.label_48 = QtWidgets.QLabel(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_48.sizePolicy().hasHeightForWidth())
        self.label_48.setSizePolicy(sizePolicy)
        self.label_48.setObjectName("label_48")
        self.gridLayout_8.addWidget(self.label_48, 0, 0, 1, 1)
        self.label_47 = QtWidgets.QLabel(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_47.sizePolicy().hasHeightForWidth())
        self.label_47.setSizePolicy(sizePolicy)
        self.label_47.setObjectName("label_47")
        self.gridLayout_8.addWidget(self.label_47, 1, 4, 1, 1)
        self.label_50 = QtWidgets.QLabel(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_50.sizePolicy().hasHeightForWidth())
        self.label_50.setSizePolicy(sizePolicy)
        self.label_50.setObjectName("label_50")
        self.gridLayout_8.addWidget(self.label_50, 1, 0, 1, 1)
        self.label_51 = QtWidgets.QLabel(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label_51.sizePolicy().hasHeightForWidth())
        self.label_51.setSizePolicy(sizePolicy)
        self.label_51.setObjectName("label_51")
        self.gridLayout_8.addWidget(self.label_51, 1, 6, 1, 1)
        self.label4 = QtWidgets.QTextEdit(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Maximum)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label4.sizePolicy().hasHeightForWidth())
        self.label4.setSizePolicy(sizePolicy)
        self.label4.setObjectName("label4")
        self.label4.setText('0')
        self.gridLayout_8.addWidget(self.label4, 1, 1, 1, 1)
        self.label3 = QtWidgets.QTextEdit(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Maximum)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label3.sizePolicy().hasHeightForWidth())
        self.label3.setSizePolicy(sizePolicy)
        self.label3.setObjectName("label3")
        self.label3.setText('0')
        self.gridLayout_8.addWidget(self.label3, 0, 7, 1, 1)
        self.label6 = QtWidgets.QTextEdit(self.gridLayoutWidget_7)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Maximum, QtWidgets.QSizePolicy.Maximum)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.label6.sizePolicy().hasHeightForWidth())
        self.label6.setSizePolicy(sizePolicy)
        self.label6.setObjectName("label6")
        self.label6.setText('0')
        self.gridLayout_8.addWidget(self.label6, 1, 7, 1, 1)
        self.decp_button = QtWidgets.QPushButton(self.label_data)
        self.decp_button.setGeometry(QtCore.QRect(540, 230, 100, 28))
        self.decp_button.setObjectName("decp_button")
        ForceSensor.setCentralWidget(self.centralwidget)
        self.menubar = QtWidgets.QMenuBar(ForceSensor)
        self.menubar.setGeometry(QtCore.QRect(0, 0, 1353, 31))
        self.menubar.setObjectName("menubar")
        ForceSensor.setMenuBar(self.menubar)
        self.statusbar = QtWidgets.QStatusBar(ForceSensor)
        self.statusbar.setObjectName("statusbar")
        ForceSensor.setStatusBar(self.statusbar)

        self.retranslateUi(ForceSensor)
        QtCore.QMetaObject.connectSlotsByName(ForceSensor)

    def retranslateUi(self, ForceSensor):
        _translate = QtCore.QCoreApplication.translate
        ForceSensor.setWindowTitle(_translate("ForceSensor", "MainWindow"))
        self.original_data.setTitle(_translate("ForceSensor", "Original Data"))
        self.open_button.setText(_translate("ForceSensor", "打开串口"))
        self.D1.setText(_translate("ForceSensor", ""))
        self.label_8.setText(_translate("ForceSensor", "A-:"))
        self.label_4.setText(_translate("ForceSensor", "N1:"))
        self.D3.setText(_translate("ForceSensor", ""))
        self.D4.setText(_translate("ForceSensor", ""))
        self.label_6.setText(_translate("ForceSensor", "A+:"))
        self.label_16.setText(_translate("ForceSensor", "B+:"))
        self.D2.setText(_translate("ForceSensor", ""))
        self.label_10.setText(_translate("ForceSensor", "B-:"))
        self.label_12.setText(_translate("ForceSensor", "C+:"))
        self.D5.setText(_translate("ForceSensor", ""))
        self.D6.setText(_translate("ForceSensor", ""))
        self.label_13.setText(_translate("ForceSensor", "C-:"))
        self.D7.setText(_translate("ForceSensor", ""))
        self.label_17.setText(_translate("ForceSensor", "N2:"))
        self.D8.setText(_translate("ForceSensor", ""))
        self.U1.setText(_translate("ForceSensor", ""))
        self.label_7.setText(_translate("ForceSensor", "A-:"))
        self.label_2.setText(_translate("ForceSensor", "S1:"))
        self.U3.setText(_translate("ForceSensor", ""))
        self.U4.setText(_translate("ForceSensor", ""))
        self.label_5.setText(_translate("ForceSensor", "A+:"))
        self.label_14.setText(_translate("ForceSensor", "B+:"))
        self.U2.setText(_translate("ForceSensor", ""))
        self.label_3.setText(_translate("ForceSensor", "B-:"))
        self.label_11.setText(_translate("ForceSensor", "C+:"))
        self.U5.setText(_translate("ForceSensor", ""))
        self.U6.setText(_translate("ForceSensor", ""))
        self.label_9.setText(_translate("ForceSensor", "C-:"))
        self.U7.setText(_translate("ForceSensor", ""))
        self.label_15.setText(_translate("ForceSensor", "S2:"))
        self.U8.setText(_translate("ForceSensor", ""))
        self.diiff_data.setTitle(_translate("ForceSensor", "Difference Data"))
        self.zero_button.setText(_translate("ForceSensor", "设置零点"))
        self.label_18.setText(_translate("ForceSensor", "Fc:"))
        self.label_23.setText(_translate("ForceSensor", "Mb:"))
        self.label_19.setText(_translate("ForceSensor", "Fa:"))
        self.R1.setText(_translate("ForceSensor", ""))
        self.label_20.setText(_translate("ForceSensor", "Fb:"))
        self.R2.setText(_translate("ForceSensor", ""))
        self.label_22.setText(_translate("ForceSensor", "Ma:"))
        self.R3.setText(_translate("ForceSensor", ""))
        self.R4.setText(_translate("ForceSensor", ""))
        self.R5.setText(_translate("ForceSensor", ""))
        self.R6.setText(_translate("ForceSensor", ""))
        self.label_24.setText(_translate("ForceSensor", "Mc:"))
        self.save_button.setText(_translate("ForceSensor", "保存"))
        self.decp_data.setTitle(_translate("ForceSensor", "Decouple Data"))
        self.label_40.setText(_translate("ForceSensor", "Fz:"))
        self.label_41.setText(_translate("ForceSensor", "My:"))
        self.label_42.setText(_translate("ForceSensor", "Fx:"))
        self.DE1.setText(_translate("ForceSensor", ""))
        self.label_43.setText(_translate("ForceSensor", "Fy:"))
        self.DE2.setText(_translate("ForceSensor", ""))
        self.label_44.setText(_translate("ForceSensor", "Mx:"))
        self.DE3.setText(_translate("ForceSensor", ""))
        self.DE4.setText(_translate("ForceSensor", ""))
        self.DE5.setText(_translate("ForceSensor", ""))
        self.DE6.setText(_translate("ForceSensor", ""))
        self.label_45.setText(_translate("ForceSensor", "Mz:"))
        self.label_data.setTitle(_translate("ForceSensor", "Label"))
        self.label_46.setText(_translate("ForceSensor", "Fz_label:"))
        self.label_49.setText(_translate("ForceSensor", "Fy_label:"))
        self.label_48.setText(_translate("ForceSensor", "Fx_label:"))
        self.label_47.setText(_translate("ForceSensor", "My_label:"))
        self.label_50.setText(_translate("ForceSensor", "Mx_label:"))
        self.label_51.setText(_translate("ForceSensor", "Mz_label:"))
        self.decp_button.setText(_translate("ForceSensor", "解耦"))

