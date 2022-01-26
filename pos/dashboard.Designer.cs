namespace pos
{
    partial class dashboard
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.user_btn = new FontAwesome.Sharp.IconButton();
            this.inventory_btn = new FontAwesome.Sharp.IconButton();
            this.panelHolder = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panel1.Controls.Add(this.inventory_btn);
            this.panel1.Controls.Add(this.user_btn);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 800);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(210, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1070, 60);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(235)))));
            this.label2.Location = new System.Drawing.Point(1041, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "X";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(235)))));
            this.label1.Location = new System.Drawing.Point(26, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "SimplePOS";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(235)))));
            this.label3.Location = new System.Drawing.Point(445, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 30);
            this.label3.TabIndex = 5;
            this.label3.Text = "SimplePOS";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(210, 136);
            this.panel3.TabIndex = 5;
            // 
            // user_btn
            // 
            this.user_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.user_btn.FlatAppearance.BorderSize = 0;
            this.user_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.user_btn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(235)))));
            this.user_btn.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.user_btn.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(235)))));
            this.user_btn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.user_btn.IconSize = 32;
            this.user_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.user_btn.Location = new System.Drawing.Point(0, 136);
            this.user_btn.Name = "user_btn";
            this.user_btn.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.user_btn.Size = new System.Drawing.Size(210, 60);
            this.user_btn.TabIndex = 6;
            this.user_btn.Text = "Users";
            this.user_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.user_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.user_btn.UseVisualStyleBackColor = true;
            this.user_btn.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // inventory_btn
            // 
            this.inventory_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.inventory_btn.FlatAppearance.BorderSize = 0;
            this.inventory_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inventory_btn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inventory_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(235)))));
            this.inventory_btn.IconChar = FontAwesome.Sharp.IconChar.Cubes;
            this.inventory_btn.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(235)))));
            this.inventory_btn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.inventory_btn.IconSize = 32;
            this.inventory_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.inventory_btn.Location = new System.Drawing.Point(0, 196);
            this.inventory_btn.Name = "inventory_btn";
            this.inventory_btn.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.inventory_btn.Size = new System.Drawing.Size(210, 60);
            this.inventory_btn.TabIndex = 7;
            this.inventory_btn.Text = "Inventory";
            this.inventory_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.inventory_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.inventory_btn.UseVisualStyleBackColor = true;
            // 
            // panelHolder
            // 
            this.panelHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHolder.Location = new System.Drawing.Point(210, 60);
            this.panelHolder.Name = "panelHolder";
            this.panelHolder.Size = new System.Drawing.Size(1070, 740);
            this.panelHolder.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(235)))));
            this.label4.Location = new System.Drawing.Point(1009, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 27);
            this.label4.TabIndex = 6;
            this.label4.Text = "O";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(235)))));
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.panelHolder);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dashboard";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private FontAwesome.Sharp.IconButton user_btn;
        private FontAwesome.Sharp.IconButton inventory_btn;
        private System.Windows.Forms.Panel panelHolder;
        private System.Windows.Forms.Label label4;
    }
}