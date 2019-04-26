# -*- coding: utf-8 -*-

import tensorflow as tf
import numpy as np


class IL:

    def __init__(self, BATCH_SIZE, para):
        self.sess = tf.Session()
        # Create Placeholders of shape (n_x, n_y)
        self.X = tf.placeholder(tf.float32, [6, None])
        self.Y = tf.placeholder(tf.float32, [6, None])
        self.BATCH_SIZE = BATCH_SIZE
        self.parameters

    def cost(self, logits, labels):
        """
        Computes the cost using the sigmoid cross entropy
        
        Arguments:
        logits -- vector containing z, output of the last linear unit (before the final sigmoid activation)
        labels -- vector of labels y (1 or 0)

        Note: What we've been calling "z" and "y" in this class are respectively called "logits" and "labels"
        in the TensorFlow documentation. So logits will feed into z, and labels into y.
        
        Returns:
        cost -- runs the session of the cost (formula (2))
        """

        # Create the placeholders for "logits" (z) and "labels" (y) (approx. 2 lines)
        z = tf.placeholder(tf.float32, name="z")
        y = tf.placeholder(tf.float32, name="y")

        # Use the loss function (approx. 1 line)
        cost = tf.nn.sigmoid_cross_entropy_with_logits(logits=z, labels=y)

        # Create a session (approx. 1 line). See method 1 above.
        sess = tf.Session()

        # Run the session (approx. 1 line).
        cost = sess.run(cost, feed_dict={z: logits, y: labels})

        # Close the session (approx. 1 line). See method 1 above.
        sess.close()

        return cost

    def initialize_parameters(self):
        """
        Initializes parameters to build a neural network with tensorflow. The shapes are:
                            W1 : [25, 6]
                            b1 : [25, 1]
                            W2 : [12, 25]
                            b2 : [12, 1]
                            W3 : [6, 12]
                            b3 : [6, 1]

        Returns:
        parameters -- a dictionary of tensors containing W1, b1, W2, b2, W3, b3
        """

        tf.set_random_seed(1)  # so that your "random" numbers match ours
        # W1 = tf.Variable(tf.random_normal([25, 6], stddev=1), name="W1")
        # b1 = tf.Variable(tf.zeros([25]), name="b1")
        # W2 = tf.Variable(tf.random_normal([12, 25], stddev=1), name="W2")
        # b2 = tf.Variable(tf.zeros(12), name="b2")
        # W3 = tf.Variable(tf.random_normal([6, 12], stddev=1), name="W3")
        # b3 = tf.Variable(tf.zeros(6), name="b3")

        ### START CODE HERE ### (approx. 6 lines of code)
        W1 = tf.get_variable("W1", [25, 6], initializer=tf.contrib.layers.xavier_initializer(seed=1))
        b1 = tf.get_variable("b1", [25, 1], initializer=tf.zeros_initializer())
        W2 = tf.get_variable("W2", [12, 25], initializer=tf.contrib.layers.xavier_initializer(seed=1))
        b2 = tf.get_variable("b2", [12, 1], initializer=tf.zeros_initializer())
        W3 = tf.get_variable("W3", [6, 12], initializer=tf.contrib.layers.xavier_initializer(seed=1))
        b3 = tf.get_variable("b3", [6, 1], initializer=tf.zeros_initializer())
        ### END CODE HERE ###

        parameters = {"W1": W1,
                      "b1": b1,
                      "W2": W2,
                      "b2": b2,
                      "W3": W3,
                      "b3": b3}

        return parameters

    def run_global_variables_initializer(self):
        self.sess.run(tf.global_variables_initializer())

    def forward_propagation(self, X, parameters):
        """
        Implements the forward propagation for the model: LINEAR -> RELU -> LINEAR -> RELU -> LINEAR -> SOFTMAX

        Arguments:
        X -- input dataset placeholder, of shape (input size, number of examples)
        parameters -- python dictionary containing your parameters "W1", "b1", "W2", "b2", "W3", "b3"
                      the shapes are given in initialize_parameters

        Returns:
        Z3 -- the output of the last LINEAR unit
        """
        # Retrieve the parameters from the dictionary "parameters"
        W1 = parameters['W1']
        b1 = parameters['b1']
        W2 = parameters['W2']
        b2 = parameters['b2']
        W3 = parameters['W3']
        b3 = parameters['b3']
        # Numpy Equivalents:
        Z1 = tf.add(tf.matmul(W1, X), b1)  # Z1 = np.dot(W1, X) + b1
        A1 = tf.nn.relu(Z1)  # A1 = relu(Z1)
        Z2 = tf.add(tf.matmul(W2, A1), b2)  # Z2 = np.dot(W2, a1) + b2
        A2 = tf.nn.relu(Z2)  # A2 = relu(Z2)
        Z3 = tf.add(tf.matmul(W3, A2), b3)  # Z3 = np.dot(W3,Z2) + b3

        return Z3

    def compute_cost(self, Z3, Y):
        """
        Computes the cost

        Arguments:
        Z3 -- output of forward propagation (output of the last LINEAR unit), of shape (6, number of examples)
        Y -- "true" labels vector placeholder, same shape as Z3

        Returns:
        cost - Tensor of the cost function
        """

        # to fit the tensorflow requirement for tf.nn.softmax_cross_entropy_with_logits(...,...)
        logits = tf.transpose(Z3)
        labels = tf.transpose(Y)

        ### START CODE HERE ### (1 line of code)
        cost = tf.reduce_mean(tf.nn.softmax_cross_entropy_with_logits(logits=logits, labels=labels))
        ### END CODE HERE ###

        return cost

    def model(self, Pyqt5_Serial, X_train, Y_train, X_test=None, Y_test=None, learning_rate=0.0001,
              num_epochs=1, minibatch_size=1, print_cost=True):
        """
        Implements a three-layer tensorflow neural network: LINEAR->RELU->LINEAR->RELU->LINEAR->SOFTMAX.

        Arguments:
        X_train -- training set, of shape (input size = 12288, number of training examples = 1080)
        Y_train -- test set, of shape (output size = 6, number of training examples = 1080)
        X_test -- training set, of shape (input size = 12288, number of training examples = 120)
        Y_test -- test set, of shape (output size = 6, number of test examples = 120)
        learning_rate -- learning rate of the optimization
        num_epochs -- number of epochs of the optimization loop
        minibatch_size -- size of a minibatch
        print_cost -- True to print the cost every 100 epochs

        Returns:
        parameters -- parameters learnt by the model. They can then be used to predict.
        """
        tf.reset_default_graph()
        tf.set_random_seed(1)  # to keep consistent results
        seed = 3  # to keep consistent results
        (n_x, m) = X_train.shape  # n_x: input size, m : number of examples in the train set
        n_y = Y_train.shape[0]  # n_y : output size
        costs = []  # To keep track of the cost

        # Initialize parameters
        if Pyqt5_Serial.para_flag == 0:
            parameters = self.initialize_parameters()
        # else:
        #     parameters = get_parameters(Pyqt5_Serial)

        # Forward propagation: Build the forward propagation in the tensorflow graph
        z3 = self.forward_propagation(self.X, parameters)

        # Cost function: Add cost function to tensorflow graph
        cost = self.compute_cost(z3, self.Y)

        # Backpropagation: Define the tensorflow optimizer. Use an AdamOptimizer.
        optimizer = tf.train.AdamOptimizer(learning_rate=learning_rate).minimize(cost)
        # Initialize all the variables
        init = tf.global_variables_initializer()

        # Start the session to compute the tensorflow graph
        with tf.Session() as sess:
            # Run the initializatio    Pyqt5_Serial.decouple_show(z3)n
            # Pyqt5_Serial.decouple_show(sess.run(z3))
            # saver = tf.train.Saver()
            # if Pyqt5_Serial.para_flag == 0:
            sess.run(init)

            # else:
            #     check_point_path = 'saved_model/'  # 保存好模型的文件路径
            #     ckpt = tf.train.get_checkpoint_state(checkpoint_dir=check_point_path)
            #     saver.restore(sess, ckpt.model_checkpoint_path)
            #     print(Pyqt5_Serial.para_flag)
            #     Pyqt5_Serial.para_flag += 1

            # Do the training loop
            for epoch in range(num_epochs):
                minibatch_cost = 0.
                num_minibatches = int(
                    m / minibatch_size)  # number of minibatches of size minibatch_size in the train set
                seed = seed + 1
                minibatch = (X_train, Y_train)

                # IMPORTANT: The line that runs the graph on a minibatch.
                # Run the session to execute the optimizer and the cost, the feedict should contain a minibatch for (X,Y).
                _, temp_cost = sess.run([optimizer, cost], feed_dict={self.X: X_train, self.Y: Y_train})

                # Print the cost every epoch
                if print_cost == True and epoch == 10:
                    print("Cost after epoch %i: %f" % (epoch, temp_cost))
                if print_cost == True and epoch % 5 == 0:
                    costs.append(temp_cost)
            # # plot the cost
            # plt.plot(np.squeeze(costs))
            # plt.ylabel('cost')
            # plt.xlabel('iterations (per tens)')
            # plt.title("Learning rate =" + str(learning_rate))
            # plt.show()

            output = sess.run(z3, feed_dict={self.X: X_train})
            Pyqt5_Serial.decouple_show(output)
            parameters = sess.run(parameters)  # lets save the parameters in a variable
            print("Parameters have been trained!")

            # saver.save(sess, model_path+'/model.ckpt')
            # # Calculate the correct predictions
            # correct_prediction = tf.equal(tf.argmax(z3), tf.argmax(Y))
            #
            # # Calculate accuracy on the test set
            # accuracy = tf.reduce_mean(tf.cast(correct_prediction, "float"))
            #
            # print("Train Accuracy:", accuracy.eval({X: X_train, Y: Y_train}))
            # # print("Test Accuracy:", accuracy.eval({X: X_test, Y: Y_test}))
            return parameters
