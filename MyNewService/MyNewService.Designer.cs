using System.ComponentModel;
using System.Diagnostics;

namespace MyNewService
{
    partial class MyNewService
    {

        #region Properties

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// 
        /// </summary>
        private EventLog eventLog1;

        /// <summary>
        /// 
        /// </summary>
        private int eventId = 1;

        #endregion
        
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "MyNewService";
        }

        #endregion
    }
}
