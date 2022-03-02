namespace XYTest
{
    partial class XYTestForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.btn_Circle = new System.Windows.Forms.Button();
            this.btn_xy = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(22, 12);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(500, 500);
            this.formsPlot1.TabIndex = 0;
            // 
            // btn_Circle
            // 
            this.btn_Circle.Location = new System.Drawing.Point(599, 42);
            this.btn_Circle.Name = "btn_Circle";
            this.btn_Circle.Size = new System.Drawing.Size(75, 23);
            this.btn_Circle.TabIndex = 1;
            this.btn_Circle.Text = "Circle";
            this.btn_Circle.UseVisualStyleBackColor = true;
            this.btn_Circle.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_xy
            // 
            this.btn_xy.Location = new System.Drawing.Point(599, 78);
            this.btn_xy.Name = "btn_xy";
            this.btn_xy.Size = new System.Drawing.Size(75, 23);
            this.btn_xy.TabIndex = 2;
            this.btn_xy.Text = "y=x";
            this.btn_xy.UseVisualStyleBackColor = true;
            this.btn_xy.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(599, 107);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.btn_Clear.TabIndex = 3;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Click);
            // 
            // XYTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.btn_xy);
            this.Controls.Add(this.btn_Circle);
            this.Controls.Add(this.formsPlot1);
            this.Name = "XYTestForm";
            this.Text = "Form1";
            this.Click += new System.EventHandler(this.btn_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.Button btn_Circle;
        private System.Windows.Forms.Button btn_xy;
        private System.Windows.Forms.Button btn_Clear;
    }
}

