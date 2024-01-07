using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class MainAppWin : Form
    {
        // אוסף של אובייקט מסוג מוצר
        IMongoCollection<Models.Product> productCollection;

        // הקרא את הנתיב של קובץ הנתונים
        private readonly string externalFile = ConfigurationManager.AppSettings["FileForBulkActivity"];



        public MainAppWin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

            MongoUrl mongoUrl = MongoUrl.Create(connectionString);
            string dbName = mongoUrl.DatabaseName;

            // Declare on a mongo client
            MongoClient mongoClient;
            try
            {
                mongoClient = new MongoClient(connectionString);

                // Get the db object itself
                IMongoDatabase db = mongoClient.GetDatabase(dbName);

                productCollection = db.GetCollection<Models.Product>("Products");

                // When the form is loaded- then we would like to load all the existing products upon screen.
                LoadProduct();

                // Show the path of the external file
                textBox_FullPathBulkActivities.Text = externalFile;
            }
            catch(Exception ex)
            {
                MessageBox.Show("We got the following error message:\n\n" + ex.Message, 
                                "Mongo Client was not created",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        public void LoadProduct()
        {

            // Option 1:
            //List<Models.Product> loadedProducts = productCollection.Aggregate().ToList();

            // Option 2:
            // Get strongy type component
            FilterDefinition<Models.Product> emptyFilter = Builders<Models.Product>.Filter.Empty;
            List<Models.Product> loadedProducts = productCollection.Find(emptyFilter).ToList();

            // Option3 - lamda experssion


            // Set the results
            dataGridView1.DataSource = loadedProducts;
        }

        public void btn_InsertProduct_Click(object sender, EventArgs e)
        {
            // Stage 1: Take the data out of the screen
            Models.Product productUponTheScreen = GetProductUponTheScreenAlone();

            // Stage 2: Insert a new product into the Mongo DB with the 'Products'
            if(productUponTheScreen != null)
            {
                try
                {
                    productCollection.InsertOne(productUponTheScreen);
                    MessageBox.Show("The following product was inserted:\n" + productUponTheScreen.ToString(),
                                    "Insert success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    // When the form is loaded- we would like to load all
                    // the existing products upon screen (including the new one).
                    LoadProduct();


                    textBox_ProductCode.Clear();
                    textBox_ProductName.Clear();
                    textBox_ProductPrice.Clear();
                    LoadProduct();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Did not insert the data ", ex.Message);
                }
            }
        }

        public Product GetProductUponTheScreenAlone()
        {
            // Stage 1: Take the data from the text box
            string productCode = textBox_ProductCode.Text;
            string productName = textBox_ProductName.Text;
            double productPrice = Convert.ToDouble(textBox_ProductPrice.Text);

            //int.Parse  throw exceptions
            //int.TryParse does not throw exceptions

            // Stage 2: Create a new Product object (Using the ctor)
            Product productFromScreen = new Product(productCode, productName, productPrice);

            // Stage 3: Return the newly created object
            return productFromScreen;
        }

        private void textBox_ProductCode_TextChanged(object sender, EventArgs e)
        {
            
           /* if(String.IsNullOrEmpty(textBox_ProductCode.Text) ||
                String.IsNullOrEmpty(textBox_ProductName.Text) ||
                String.IsNullOrEmpty(textBox_ProductPrice.Text))
                    btn_InsertProduct.Enabled = false;
            else
                btn_InsertProduct.Enabled = true;*/
        }

        /*private void btn_DeleteProduct_Click(object sender, EventArgs e)
        {
            string id = "_id";
            // Stage 1: Take the id
            //string _id = textBox__id.Text;

            // Stage 2: Delete the product with the given id from the database
            try
            {
                var filter = Builders<Product>.Filter.Eq(id, ObjectId.Parse(_id));
                var result = productCollection.DeleteOne(filter);

                if (result.DeletedCount > 0)
                {
                    MessageBox.Show("The product with _id " + _id + " was deleted successfully.",
                                    "Delete success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    // When the form is loaded- we would like to load all
                    // the existing products upon screen (excluding the deleted one).
                    LoadProduct();

                    //textBox__id.Clear();
                }
                else
                {
                    MessageBox.Show("No product with _id " + _id + " was found.",
                                    "Delete failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete the product with _id " + _id + ".\n\n" + ex.Message,
                                "Delete failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }*/

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Define a window object
            //UpdateOrDeleteWin detailsWin = new UpdateOrDeleteWin();
            //UpdateOrDeleteWin detailsWin1 = new UpdateOrDeleteWin(productCollection);

            // ToDo - Open the window with a custom constructor that gets a param of the container
            UpdateOrDeleteWin detailsWin = new UpdateOrDeleteWin(productCollection);
            //.....

            // Set the infromation on the dialog box
            detailsWin.textBox_ProductID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            detailsWin.textBox_ProductCode.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            detailsWin.textBox_ProductName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            detailsWin.textBox_ProductPrice.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();


            detailsWin.ShowDialog();


            // ToDo - After we are coming back update the data upon the screen (deleted should not appear)
            // updated rows should present the new data
            LoadProduct();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            LoadProduct();
            textBox_FilterByCode.Text = string.Empty;
            textBox_FilterByName.Text = string.Empty;
        }

        private void btn_SpicificFilterByCode_Click(object sender, EventArgs e)
        {
            string requestedProductCode = textBox_FilterByCode.Text;

            FilterDefinition<Models.Product> whereCriteriaByCode =
                Builders<Models.Product>.Filter.Eq(prod => prod.ProductCode, requestedProductCode);

            // 2 Ways fir retieving the data:
            // Option1 - based on Find
            //List<Models.Product> products = productCollection.Find(whereCriteriaByCode).ToList();

            // Option2 - based on aggregate
            List<Models.Product> products = productCollection.Aggregate().Match(whereCriteriaByCode).ToList();

            dataGridView1.DataSource = products; // Present the results
        }

        private void btn_FilterByName_Click(object sender, EventArgs e)
        {
            // Read the data from the screen
            string requestedProductName = textBox_FilterByName.Text;

            FilterDefinition<Models.Product> whereCriteriaByName =
                Builders<Models.Product>.Filter.Eq(prod => prod.ProductName, requestedProductName);

            List<Models.Product> products = productCollection.Aggregate().Match(whereCriteriaByName).ToList();

            dataGridView1.DataSource = products;
        }

        private void btn_FilterByPrice_EQ_Click(object sender, EventArgs e)
        {
            string requestedProductPrice = textBox_FilterByPrice.Text;
            FilterDefinition<Models.Product> whereCriteriaByPrice =
                Builders<Models.Product>.Filter.Eq(prod => prod.Price, Convert.ToDouble(requestedProductPrice));

            List<Models.Product> products = productCollection.Aggregate().Match(whereCriteriaByPrice).ToList();

            dataGridView1.DataSource = products;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox_FilterByCode_AND.Enabled = true;
                textBox_FilterByName_AND.Enabled = true;
                textBox_FilterByPrice_AND.Enabled = true;
            }
            else
            {
                textBox_FilterByCode_AND.Enabled = false;
                textBox_FilterByCode_AND.Text = string.Empty;
                textBox_FilterByName_AND.Enabled = false;
                textBox_FilterByName_AND.Text = string.Empty;
                textBox_FilterByPrice_AND.Enabled = false;
                textBox_FilterByPrice_AND.Text = string.Empty;
            }
        }

        private void btn_FilterByPrice_LTE_Click(object sender, EventArgs e)
        {
            string requestedProductPrice = textBox_FilterByPrice.Text;
            FilterDefinition<Models.Product> whereCriteriaByPrice =
                Builders<Models.Product>.Filter.Lte(prod => prod.Price, Convert.ToDouble(requestedProductPrice));

            List<Models.Product> products = productCollection.Aggregate().Match(whereCriteriaByPrice).ToList();

            dataGridView1.DataSource = products;
        }

        private void btn_FilterByPrice_In_Click(object sender, EventArgs e)
        {
            /*string requestedProductPrices = textBox_FilterByPrice.Text; // Get the entered product prices as a string
            string[] priceArray = requestedProductPrices.Split(','); // Split the string into an array of individual price strings

            List<double> prices = new List<double>(); // Create a list to store the parsed prices
            foreach (var price in priceArray) // Iterate over each price string in the array
            {
                if (double.TryParse(price.Trim(), out double parsedPrice)) // Trim and parse each price string to double
                {
                    prices.Add(parsedPrice); // Add the parsed price to the list of prices
                }
            }

            FilterDefinition<Models.Product> whereCriteriaByPrice =
                Builders<Models.Product>.Filter.In(prod => prod.Price, prices); // Create a filter to match products with prices in the list

            List<Models.Product> products = productCollection.Aggregate().Match(whereCriteriaByPrice).ToList(); // Retrieve the filtered products

            dataGridView1.DataSource = products; // Set the data source of the dataGridView1 to the filtered products*/


            // Stage1: Read the data from the screen
            string listPricesAsString = textBox_FilterByPrice.Text;

            // Stage2 : Take the numbers out the string (split)
            List<double> prices = new List<double>();
            string[] arrayOfStrPrices = listPricesAsString.Split(',');
            foreach (string priceStr in arrayOfStrPrices)
                prices.Add(Convert.ToDouble(priceStr));

            // Stage3: Build the filter
            FilterDefinition<Models.Product> whereCriteria =
                    Builders<Models.Product>.Filter.In(item => item.Price, prices);

            // Stage4: Trigger the filter
            List<Models.Product> products = productCollection.Find(whereCriteria).ToList();

            // Stage5: Present the results
            dataGridView1.DataSource = products;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string requestedProductCode = textBox_FilterByCode_AND.Text;
            string requestedProductName = textBox_FilterByName_AND.Text;
            string requestedProductPrice = textBox_FilterByPrice_AND.Text;

            FilterDefinition<Models.Product> whereCriteria = Builders<Models.Product>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(requestedProductCode))
            {
                FilterDefinition<Models.Product> codeFilter = Builders<Models.Product>.Filter.Eq(prod => prod.ProductCode, requestedProductCode);
                whereCriteria &= codeFilter;
            }

            if (!string.IsNullOrWhiteSpace(requestedProductName))
            {
                FilterDefinition<Models.Product> nameFilter = Builders<Models.Product>.Filter.Eq(prod => prod.ProductName, requestedProductName);
                whereCriteria &= nameFilter;
            }

            if (!string.IsNullOrWhiteSpace(requestedProductPrice))
            {
                double parsedPrice;
                if (double.TryParse(requestedProductPrice, out parsedPrice))
                {
                    FilterDefinition<Models.Product> priceFilter = Builders<Models.Product>.Filter.Lte(prod => prod.Price, parsedPrice);
                    whereCriteria &= priceFilter;
                }
            }

            List<Models.Product> products = productCollection.Aggregate().Match(whereCriteria).ToList();
            dataGridView1.DataSource = products;
        }

        private void btn_bulkInsert_Click(object sender, EventArgs e)
        {
            string[] productDetails;
            Models.Product product;
            List<Models.Product> products = new List<Product>();

            // Stage1 : using System.IO.Path - read all the lines which are inside the file in the path
            string fullPath = System.IO.Path.GetFullPath(externalFile);
            string[] csvLines = null;

            try
            {
                csvLines = System.IO.File.ReadAllLines(fullPath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            // Stage2 : Scan the results that were read above (foreach...)
            //          and store them inside a list
            foreach(string currentProduct in csvLines)
            {
                productDetails = currentProduct.Split(',');
                product = new Product(productDetails[0], productDetails[1], Convert.ToDouble(productDetails[2]));
                products.Add(product);
            }

            // When the code come here then all the items (products) are populated on the list

            // Stage3 : Perform insertMany into the collection
            productCollection.InsertMany(products);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*// Stage1 : Compose the where criteria
            FilterDefinition<Models.Product> whereCriteriaCode1 = null;

            string whereCriteriaCode = textBox_WhereCriteriaCode.Text;
            string whereCriteriaName = textBox_WhereCriteriaName.Text;
            string WhereCriteriaPrice = textBox_WhereCriteriaPrice.Text;

            string updatedCode = textBox_UpdatedCode.Text;
            string updatedName = textBox_UpdatedName.Text;
            string updatedPrice = textBox_UpdatedPrice.Text;

            whereCriteriaCode1 =
                    Builders<Models.Product>.Filter.Eq(code => code.ProductCode, whereCriteriaCode) &
                    Builders<Models.Product>.Filter.Eq(name => name.ProductName, whereCriteriaName) &
                    Builders<Models.Product>.Filter.Eq(price => price.Price, Convert.ToDouble(WhereCriteriaPrice));


            UpdateDefinition<Models.Product> updateDefinition =
             Builders<Models.Product>.Update
                                    .Set(product => product.ProductCode, updatedCode)
                                    .Set(product => product.ProductName, updatedName)
                                    .Set(product => product.Price, Convert.ToDouble(updatedPrice));


            try
            {
                MongoDB.Driver.UpdateResult result = productCollection.UpdateMany(whereCriteriaCode1, updateDefinition);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occured:\n\n" + ex.Message + "\n\n The window " +
                                " will be closed now",
                                "Delete Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);*/

            FilterDefinition<Models.Product> productCodeFilter = null;
            FilterDefinition<Models.Product> productNameFilter = null;
            FilterDefinition<Models.Product> productPriceFilter = null;

            FilterDefinition<Models.Product> combinedFilter = Builders<Models.Product>.Filter.Empty;

            if(!string.IsNullOrEmpty(textBox_WhereCriteriaCode.Text))
            {
                productCodeFilter = Builders<Models.Product>.Filter.Eq(p => p.ProductCode, textBox_WhereCriteriaCode.Text);
                combinedFilter &= productCodeFilter;
            }
            if (!string.IsNullOrEmpty(textBox_WhereCriteriaName.Text))
            {
                productNameFilter = Builders<Models.Product>.Filter.Eq(p => p.ProductName, textBox_WhereCriteriaName.Text);
                combinedFilter &= productNameFilter;
            }
            if (!string.IsNullOrEmpty(textBox_WhereCriteriaPrice.Text))
            {
                productPriceFilter = Builders<Models.Product>.Filter.Eq(p => p.Price, Convert.ToDouble(textBox_WhereCriteriaPrice.Text));
                combinedFilter &= productPriceFilter;
            }

            // Stage2: Now we will write the 'set' itself (what we should updated)


        }
    }
    }

