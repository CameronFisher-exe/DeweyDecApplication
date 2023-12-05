using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-------------------------------------------------------o0START0o------------------------------------------------//

namespace DeweyDecApplication
{
    #region ENUM COLOUR
    enum xColour
    {
        Red,
        Black
    }
    #endregion

    internal class MyRedBlackTree
    {

        public class Node
        {
            #region VARIABLES
            public xColour colour;
            public Node left;
            public Node right;
            public Node parent;
            public int data;
            public string desc;

            public Node(int data) { this.data = data; }
            public Node(xColour colour) { this.colour = colour; }
            public Node(int data, xColour colour) { this.data = data; this.colour = colour; }
            public Node(int data, string desc) { this.data = data; this.desc = desc; }
            #endregion
        }

        private Node root;

        public MyRedBlackTree()
        {

        }

        //----------------------------------------------------------------------------------------------------------------//

        #region LEFT ROTATE
        private void LeftRotate(Node X)
        {
            Node Y = X.right; // set Y
            X.right = Y.left; // turn Y's left subtree into X's right subtree
            if (Y.left != null)
            {
                Y.left.parent = X;
            }

            if (Y != null)
            {
                Y.parent = X.parent; // link X's parent to Y
            }

            if (X.parent == null)
            {
                root = Y;
            }
            else
            {
                if (X == X.parent.left)
                {
                    X.parent.left = Y;
                }
                else
                {
                    X.parent.right = Y;
                }
            }

            Y.left = X; // put X on Y's left

            if (X != null)
            {
                X.parent = Y;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region RIGHT ROTATE
        private void RightRotate(Node Y)
        {
            // right rotate is simply mirror code from left rotate
            Node X = Y.left;
            Y.left = X.right;
            if (X.right != null)
            {
                X.right.parent = Y;
            }
            if (X != null)
            {
                X.parent = Y.parent;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            if (Y == Y.parent.right)
            {
                Y.parent.right = X;
            }
            if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }

            X.right = Y;//put Y on X's right
            if (Y != null)
            {
                Y.parent = X;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region DISPLAY TREE
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Nothing in the tree!");
                return;
            }

            Console.WriteLine("Tree contents:");
            InOrderDisplay(root);
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region FIND
        public Node Find(int key)
        {
            bool isFound = false;
            Node temp = root;
            Node item = null;

            while (!isFound && temp != null)
            {
                if (key < temp.data)
                {
                    temp = temp.left;
                }
                else if (key > temp.data)
                {
                    temp = temp.right;
                }
                else
                {
                    isFound = true;
                    item = temp;
                }
            }

            if (isFound)
            {
                //Console.WriteLine("{0} was found", key);
                return item;
            }
            else
            {
                //Console.WriteLine("{0} not found", key);
                return null;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region INSERT
        public void Insert(int item, string desc)
        {
            Node newItem = new Node(item, desc);
            if (root == null)
            {
                root = newItem;
                root.colour = xColour.Black;
                return;
            }
            Node Y = null;
            Node X = root;
            while (X != null)
            {
                Y = X;
                if (newItem.data < X.data)
                {
                    X = X.left;
                }
                else
                {
                    X = X.right;
                }
            }
            newItem.parent = Y;
            if (Y == null)
            {
                root = newItem;
            }
            else if (newItem.data < Y.data)
            {
                Y.left = newItem;
            }
            else
            {
                Y.right = newItem;
            }
            newItem.left = null;
            newItem.right = null;
            newItem.colour = xColour.Red;//colour the new node red
            InsertFixUp(newItem);//call method to check for violations and fix
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region IN ORDER DISPLAY
        private void InOrderDisplay(Node current)
        {
            if (current != null)
            {
                InOrderDisplay(current.left);
                Console.Write("({0}) ", current.data);
                InOrderDisplay(current.right);
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region INSERT FIX UP
        private void InsertFixUp(Node item)
        {
            //Checks Red-Black Tree properties
            while (item != root && item.parent.colour == xColour.Red)
            {
                /*We have a violation*/
                if (item.parent == item.parent.parent.left)
                {
                    Node Y = item.parent.parent.right;
                    if (Y != null && Y.colour == xColour.Red)//Case 1: uncle is red
                    {
                        item.parent.colour = xColour.Black;
                        Y.colour = xColour.Black;
                        item.parent.parent.colour = xColour.Red;
                        item = item.parent.parent;
                    }
                    else //Case 2: uncle is black
                    {
                        if (item == item.parent.right)
                        {
                            item = item.parent;
                            LeftRotate(item);
                        }
                        //Case 3: recolour & rotate
                        item.parent.colour = xColour.Black;
                        item.parent.parent.colour = xColour.Red;
                        RightRotate(item.parent.parent);
                    }

                }
                else
                {
                    //mirror image of code above
                    Node X = null;

                    X = item.parent.parent.left;
                    if (X != null && X.colour == xColour.Black)//Case 1
                    {
                        item.parent.colour = xColour.Red;
                        X.colour = xColour.Red;
                        item.parent.parent.colour = xColour.Black;
                        item = item.parent.parent;
                    }
                    else //Case 2
                    {
                        if (item == item.parent.left)
                        {
                            item = item.parent;
                            RightRotate(item);
                        }
                        //Case 3: recolour & rotate
                        item.parent.colour = xColour.Black;
                        item.parent.parent.colour = xColour.Red;
                        LeftRotate(item.parent.parent);

                    }

                }
                root.colour = xColour.Black;//re-colour the root black as necessary
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region DELETE
        public void Delete(int key)
        {
            //first find the node in the tree to delete and assign to item pointer/reference
            Node item = Find(key);
            Node X = null;
            Node Y = null;

            if (item == null)
            {
                Console.WriteLine("Nothing to delete!");
                return;
            }
            if (item.left == null || item.right == null)
            {
                Y = item;
            }
            else
            {
                Y = TreeSuccessor(item);
            }
            if (Y.left != null)
            {
                X = Y.left;
            }
            else
            {
                X = Y.right;
            }
            if (X != null)
            {
                X.parent = Y;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            else if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }
            else
            {
                Y.parent.left = X;
            }
            if (Y != item)
            {
                item.data = Y.data;
            }
            if (Y.colour == xColour.Black)
            {
                DeleteFixUp(X);
            }

        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region DELETE FIX UP
        private void DeleteFixUp(Node X)
        {

            while (X != null && X != root && X.colour == xColour.Black)
            {
                if (X == X.parent.left)
                {
                    Node W = X.parent.right;
                    if (W.colour == xColour.Red)
                    {
                        W.colour = xColour.Black; //case 1
                        X.parent.colour = xColour.Red; //case 1
                        LeftRotate(X.parent); //case 1
                        W = X.parent.right; //case 1
                    }
                    if (W.left.colour == xColour.Black && W.right.colour == xColour.Black)
                    {
                        W.colour = xColour.Red; //case 2
                        X = X.parent; //case 2
                    }
                    else if (W.right.colour == xColour.Black)
                    {
                        W.left.colour = xColour.Black; //case 3
                        W.colour = xColour.Red; //case 3
                        RightRotate(W); //case 3
                        W = X.parent.right; //case 3
                    }
                    W.colour = X.parent.colour; //case 4
                    X.parent.colour = xColour.Black; //case 4
                    W.right.colour = xColour.Black; //case 4
                    LeftRotate(X.parent); //case 4
                    X = root; //case 4
                }
                else //mirror code from above with "right" & "left" exchanged
                {
                    Node W = X.parent.left;
                    if (W.colour == xColour.Red)
                    {
                        W.colour = xColour.Black;
                        X.parent.colour = xColour.Red;
                        RightRotate(X.parent);
                        W = X.parent.left;
                    }
                    if (W.right.colour == xColour.Black && W.left.colour == xColour.Black)
                    {
                        W.colour = xColour.Black;
                        X = X.parent;
                    }
                    else if (W.left.colour == xColour.Black)
                    {
                        W.right.colour = xColour.Black;
                        W.colour = xColour.Red;
                        LeftRotate(W);
                        W = X.parent.left;
                    }
                    W.colour = X.parent.colour;
                    X.parent.colour = xColour.Black;
                    W.left.colour = xColour.Black;
                    RightRotate(X.parent);
                    X = root;
                }
            }
            if (X != null)
                X.colour = xColour.Black;
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region MINIMUM
        private Node Minimum(Node X)
        {
            while (X.left.left != null)
            {
                X = X.left;
            }
            if (X.left.right != null)
            {
                X = X.left.right;
            }
            return X;
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region TREE SUCCESSOR
        private Node TreeSuccessor(Node X)
        {
            if (X.left != null)
            {
                return Minimum(X);
            }
            else
            {
                Node Y = X.parent;
                while (Y != null && X == Y.right)
                {
                    X = Y;
                    Y = Y.parent;
                }
                return Y;
            }
        }
        #endregion

    }
}
//------------------------------------------------------o0END0o-----------------------------------------------------//