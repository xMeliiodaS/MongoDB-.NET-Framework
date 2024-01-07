
namespace WindowsFormsApp1
{
    partial class UpdateOrDeleteWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_ProductID = new System.Windows.Forms.TextBox();
            this.textBox_ProductCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ProductName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_ProductPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_DeleteProduct = new System.Windows.Forms.Button();
            this.btn_UpdateProduct = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(55, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product ID:";
            // 
            // textBox_ProductID
            // 
            this.textBox_ProductID.Enabled = false;
            this.textBox_ProductID.Location = new System.Drawing.Point(185, 63);
            this.textBox_ProductID.Name = "textBox_ProductID";
            this.textBox_ProductID.Size = new System.Drawing.Size(230, 22);
            this.textBox_ProductID.TabIndex = 1;
            // 
            // textBox_ProductCode
            // 
            this.textBox_ProductCode.Location = new System.Drawing.Point(185, 152);
            this.textBox_ProductCode.Name = "textBox_ProductCode";
            this.textBox_ProductCode.Size = new System.Drawing.Size(230, 22);
            this.textBox_ProductCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(55, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Product Code:";
            // 
            // textBox_ProductName
            // 
            this.textBox_ProductName.Location = new System.Drawing.Point(185, 240);
            this.textBox_ProductName.Name = "textBox_ProductName";
            this.textBox_ProductName.Size = new System.Drawing.Size(230, 22);
            this.textBox_ProductName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label3.Location = new System.Drawing.Point(55, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Product Name:";
            // 
            // textBox_ProductPrice
            // 
            this.textBox_ProductPrice.Location = new System.Drawing.Point(185, 340);
            this.textBox_ProductPrice.Name = "textBox_ProductPrice";
            this.textBox_ProductPrice.Size = new System.Drawing.Size(230, 22);
            this.textBox_ProductPrice.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.Location = new System.Drawing.Point(55, 340);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Product Price:";
            // 
            // btn_DeleteProduct
            // 
            this.btn_DeleteProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_DeleteProduct.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btn_DeleteProduct.Location = new System.Drawing.Point(300, 390);
            this.btn_DeleteProduct.Name = "btn_DeleteProduct";
            this.btn_DeleteProduct.Size = new System.Drawing.Size(142, 48);
            this.btn_DeleteProduct.TabIndex = 9;
            this.btn_DeleteProduct.Text = "Delete Product";
            this.btn_DeleteProduct.UseVisualStyleBackColor = true;
            this.btn_DeleteProduct.Click += new System.EventHandler(this.btn_DeleteProduct_Click);
            // 
            // btn_UpdateProduct
            // 
            this.btn_UpdateProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_UpdateProduct.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btn_UpdateProduct.Location = new System.Drawing.Point(62, 390);
            this.btn_UpdateProduct.Name = "btn_UpdateProduct";
            this.btn_UpdateProduct.Size = new System.Drawing.Size(136, 48);
            this.btn_UpdateProduct.TabIndex = 10;
            this.btn_UpdateProduct.Text = "Update Product";
            this.btn_UpdateProduct.UseVisualStyleBackColor = true;
            this.btn_UpdateProduct.Click += new System.EventHandler(this.btn_UpdateProduct_Click);
            // 
            // UpdateOrDeleteWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 450);
            this.Controls.Add(this.btn_UpdateProduct);
            this.Controls.Add(this.btn_DeleteProduct);
            this.Controls.Add(this.textBox_ProductPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_ProductName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_ProductCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_ProductID);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "UpdateOrDeleteWin";
            this.Text = "UpdateOrDeleteWin";
            this.Load += new System.EventHandler(this.UpdateOrDeleteWin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_DeleteProduct;
        private System.Windows.Forms.Button btn_UpdateProduct;
        public System.Windows.Forms.TextBox textBox_ProductID;
        public System.Windows.Forms.TextBox textBox_ProductCode;
        public System.Windows.Forms.TextBox textBox_ProductName;
        public System.Windows.Forms.TextBox textBox_ProductPrice;
    }
}