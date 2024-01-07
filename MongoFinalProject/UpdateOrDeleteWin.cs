using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class UpdateOrDeleteWin : Form
    {
        // Product collection
        IMongoCollection<Models.Product> products { get; set; }

/*        public UpdateOrDeleteWin()
        {
            InitializeComponent();
        }*/

        public UpdateOrDeleteWin(IMongoCollection<Models.Product> products)
        {
            InitializeComponent();
            this.products = products;
        }

        private void UpdateOrDeleteWin_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_UpdateProduct_Click(object sender, EventArgs e)
        {

            // ToDo - use the mongo id and the updateOne
            string id = textBox_ProductID.Text;

            // Define the filter (Where criteria of the update statment)
            var filter = Builders<Models.Product>.Filter.Eq(prod => prod.ProductId, id);
            // => כך ש

            // Define the set for the update statement
            UpdateDefinition<Models.Product> updateDefinition =
                                     Builders<Models.Product>.Update
                                    .Set(product => product.ProductCode, textBox_ProductCode.Text)
                                    .Set(product => product.ProductName, textBox_ProductName.Text)
                                    .Set(product => product.Price, Convert.ToDouble(textBox_ProductPrice.Text));

            try
            {
                MongoDB.Driver.UpdateResult result = products.UpdateOne(filter, updateDefinition);

                if(result.ModifiedCount == 1)
                {
                    // Executed successfully
                    MessageBox.Show("Update of item # " + id + " succeeded\n\n The window " +
                                    " will be closed now",
                                    "Update Succeded",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    // Failed to execute
                }
            }
            catch(Exception ex)
            {

            }
            this.Close();
        }

        private void btn_DeleteProduct_Click(object sender, EventArgs e)
        {
            // ToDo - use the mongo id and the deleteOne

            // Take the data upon the screen
            string id = textBox_ProductID.Text;

            // Define a filter for seraching all the products that have the same product ID
            var filter = Builders<Models.Product>.Filter.Eq(p => p.ProductId, id);
            try
            {
                MongoDB.Driver.DeleteResult result = products.DeleteOne(filter);

                // Does the delete succeeded
                if(result.DeletedCount == 1) // After all we expect to get one product deleted
                {
                    MessageBox.Show("Delete of item # " + id + " succeeded\n\n The window " +
                                    " will be closed now",
                                    "Delete Succeded",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Delete of item # " + id + " failed\n\n The window " +
                                    " will be closed now",
                                    "Delete Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("The following error occured:\n\n" + ex.Message + "\n\n The window " +
                                " will be closed now",
                                "Delete Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            this.Close();
        }
    }
}
