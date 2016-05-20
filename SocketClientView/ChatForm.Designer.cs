namespace SocketClientView
{
    partial class ChatForm
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
            this.chatText = new System.Windows.Forms.RichTextBox();
            this.messageText = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.logRequestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chatText
            // 
            this.chatText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatText.BackColor = System.Drawing.SystemColors.Window;
            this.chatText.Location = new System.Drawing.Point(12, 41);
            this.chatText.Multiline = true;
            this.chatText.Name = "chatText";
            this.chatText.ReadOnly = true;
            this.chatText.Size = new System.Drawing.Size(659, 202);
            this.chatText.TabIndex = 0;
            // 
            // messageText
            // 
            this.messageText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageText.Location = new System.Drawing.Point(12, 251);
            this.messageText.Name = "messageText";
            this.messageText.Size = new System.Drawing.Size(578, 20);
            this.messageText.TabIndex = 1;
            this.messageText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.messageText_KeyUp);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Location = new System.Drawing.Point(596, 249);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Отправить";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // logRequestButton
            // 
            this.logRequestButton.Location = new System.Drawing.Point(12, 12);
            this.logRequestButton.Name = "logRequestButton";
            this.logRequestButton.Size = new System.Drawing.Size(139, 23);
            this.logRequestButton.TabIndex = 4;
            this.logRequestButton.Text = "Лог сообщений";
            this.logRequestButton.UseVisualStyleBackColor = true;
            this.logRequestButton.Click += new System.EventHandler(this.logRequestButton_Click);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 284);
            this.Controls.Add(this.logRequestButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageText);
            this.Controls.Add(this.chatText);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "ChatForm";
            this.Text = "Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox chatText;
        private System.Windows.Forms.TextBox messageText;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button logRequestButton;
    }
}

