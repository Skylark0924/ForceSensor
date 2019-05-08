# -*- coding: utf-8 -*-
import os
import numpy as np
import tensorflow as tf

os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'
xtest = np.array([[0.88253038],
                  [0.94960076],
                  [0.31629415],
                  [0.72655601],
                  [0.53271609],
                  [0.41769816]])


class IL:

    def __init__(self, restore_from=None):
        # Create Placeholders of shape (n_x, n_y)
        # 输入输出都只有1个神经元
        with tf.name_scope('inputs'):
            self.xs = tf.placeholder(tf.float32, [6, None])
            self.ys = tf.placeholder(tf.float32, [6, None])
        self.parameters = None
        self.layer1 = self.add_layer(self.xs, 6, 10, n_layer=1, activation_function=tf.nn.relu)
        self.prediction_layer = self.add_layer(self.layer1, 10, 6, n_layer=2, activation_function=None)

        config = tf.ConfigProto()
        config.gpu_options.allow_growth = True
        self.sess = tf.Session(config=config)

        # with tf.name_scope('loss'):
        self.loss = tf.losses.mean_squared_error(labels=self.ys, predictions=self.prediction_layer)
        # tf.summary.scalar('loss', self.loss)
        # with tf.name_scope('train'):
        self.train_op = tf.train.RMSPropOptimizer(0.001).minimize(self.loss)

        if restore_from:
            saver = tf.train.Saver()
            self.sess.run(tf.global_variables_initializer())
            saver.restore(self.sess, restore_from)

        else:
            self.sess.run(tf.global_variables_initializer())
        self.merged = tf.summary.merge_all()
        # 将图保存到文件中，才能在浏览器中查看
        self.writer = tf.summary.FileWriter("D:/Anaconda/pkgs/tensorboard-1.9.0-py36he025d50_0/Scripts/logs/IL/",
                                            self.sess.graph)
        # print(self.sess.run(self.layer1, feed_dict={self.xs: xtest}))

    @staticmethod
    def add_layer(inputs, inputs_size, outputs_size,
                  n_layer, activation_function=None):
        layer_name = 'layer%s' % n_layer  # define a new var
        with tf.name_scope(layer_name):
            with tf.name_scope('weights' + str(n_layer)):
                weights = tf.Variable(tf.random_normal([outputs_size, inputs_size]), name='W')
                tf.summary.histogram('/weights' + str(n_layer), weights)

            with tf.name_scope('biases' + str(n_layer)):
                biases = tf.Variable(tf.zeros([outputs_size, 1]) + 0.1, name='b')
                tf.summary.histogram('/biases' + str(n_layer), biases)

            with tf.name_scope('Wx_plus_b' + str(n_layer)):
                outputs_init = tf.add(tf.matmul(weights, inputs), biases)

            if activation_function is None:
                outputs = outputs_init
            else:
                outputs = activation_function(outputs_init)

            tf.summary.histogram('/outputs', outputs)

        return outputs

    def train(self, x, y):
        loss, _ = self.sess.run([self.loss, self.train_op],
                                feed_dict={self.xs: x, self.ys: y})
        return loss

    def save(self, path='./para_save_test'):
        saver = tf.train.Saver()
        saver.save(self.sess, path, write_meta_graph=False)


def model(WorkThread, X_train, Y_train):
    if WorkThread.para_flag is False:
        myIL = IL()
    else:
        tf.reset_default_graph()
        myIL = IL(restore_from='./para_save_test')
    for i in range(101):
        train_loss = myIL.train(X_train, Y_train)
        if i % 50 == 0:
            print(i, 'train loss: ', train_loss)
            rs = myIL.sess.run(myIL.merged,
                               feed_dict={myIL.xs: X_train, myIL.ys: Y_train})
            myIL.writer.add_summary(rs, i)
            output = myIL.sess.run(myIL.prediction_layer, feed_dict={myIL.xs: X_train})
            WorkThread.decp_show(output.tolist())
        # print(myIL.sess.run(myIL.layer1, feed_dict={myIL.xs: xtest}))

    myIL.save('./para_save_test')  # save learned fc layers


def test(WorkThread, X_test):
    myIL = IL(restore_from='./para_save_test')
    output = myIL.sess.run(myIL.prediction_layer, feed_dict={myIL.xs: X_test})
    WorkThread.decp_show(output.tolist())
