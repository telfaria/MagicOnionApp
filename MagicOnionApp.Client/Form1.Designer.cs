namespace MagicOnionApp.Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnQuery = new Button();
            txtResult = new TextBox();
            btnConnect = new Button();
            btnHubConnect = new Button();
            txtMessage = new TextBox();
            label1 = new Label();
            btnSendMessage = new Button();
            btnHubDisConnect = new Button();
            btnClearLog = new Button();
            lblUserName = new Label();
            SuspendLayout();
            // 
            // btnQuery
            // 
            btnQuery.Location = new Point(12, 41);
            btnQuery.Name = "btnQuery";
            btnQuery.Size = new Size(75, 23);
            btnQuery.TabIndex = 0;
            btnQuery.Text = "Query";
            btnQuery.UseVisualStyleBackColor = true;
            btnQuery.Click += btnQuery_Click;
            // 
            // txtResult
            // 
            txtResult.Location = new Point(174, 13);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ScrollBars = ScrollBars.Both;
            txtResult.Size = new Size(215, 174);
            txtResult.TabIndex = 1;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(12, 12);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnHubConnect
            // 
            btnHubConnect.Location = new Point(5, 138);
            btnHubConnect.Name = "btnHubConnect";
            btnHubConnect.Size = new Size(163, 23);
            btnHubConnect.TabIndex = 2;
            btnHubConnect.Text = "ConnectStreamingHub";
            btnHubConnect.UseVisualStyleBackColor = true;
            btnHubConnect.Click += btnhubConnect_Click;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(81, 196);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(389, 23);
            txtMessage.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 199);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 4;
            label1.Text = "Messages";
            // 
            // btnSendMessage
            // 
            btnSendMessage.Location = new Point(346, 225);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(124, 23);
            btnSendMessage.TabIndex = 5;
            btnSendMessage.Text = "SendMessage";
            btnSendMessage.UseVisualStyleBackColor = true;
            btnSendMessage.Click += btnSendMessage_Click;
            // 
            // btnHubDisConnect
            // 
            btnHubDisConnect.Location = new Point(5, 167);
            btnHubDisConnect.Name = "btnHubDisConnect";
            btnHubDisConnect.Size = new Size(163, 23);
            btnHubDisConnect.TabIndex = 6;
            btnHubDisConnect.Text = "Disconnect StreamingHub";
            btnHubDisConnect.UseVisualStyleBackColor = true;
            btnHubDisConnect.Click += btnHubDisConnect_Click;
            // 
            // btnClearLog
            // 
            btnClearLog.Location = new Point(395, 164);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.Size = new Size(75, 23);
            btnClearLog.TabIndex = 7;
            btnClearLog.Text = "Clear Log";
            btnClearLog.UseVisualStyleBackColor = true;
            btnClearLog.Click += btnClearLog_Click;
            // 
            // lblUserName
            // 
            lblUserName.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblUserName.Location = new Point(409, 20);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(169, 55);
            lblUserName.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(604, 316);
            Controls.Add(lblUserName);
            Controls.Add(btnClearLog);
            Controls.Add(btnHubDisConnect);
            Controls.Add(btnSendMessage);
            Controls.Add(label1);
            Controls.Add(txtMessage);
            Controls.Add(btnHubConnect);
            Controls.Add(btnConnect);
            Controls.Add(txtResult);
            Controls.Add(btnQuery);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnQuery;
        private TextBox txtResult;
        private Button btnConnect;
        private Button btnHubConnect;
        private TextBox txtMessage;
        private Label label1;
        private Button btnSendMessage;
        private Button btnHubDisConnect;
        private Button btnClearLog;
        private Label lblUserName;
    }
}