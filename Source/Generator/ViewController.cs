using System;

using AppKit;
using Foundation;

using Generator.DatabaseMapper;

namespace Generator
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            cbbDBType.RemoveAll();
            cbbDBType.Add(new NSString("PostgreSQL"));
            cbbDBType.Add(new NSString("MySQL"));

            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void btnGenerate(Foundation.NSObject sender)
        {
            Console.WriteLine(txtServer.StringValue);
            Console.WriteLine(txtPort.StringValue);
            Console.WriteLine(txtDatabase.StringValue);
            Console.WriteLine(txtUser.StringValue);
            Console.WriteLine(txtPassword.StringValue);
            Console.WriteLine(txtFolderPath.StringValue);

            string dbType = ((NSString)cbbDBType.SelectedValue).ToString();
            string connectionString = String.Format("Server = {0}; Port = {1}; Database = {2}; User Id = {3};Password = {4};", txtServer.StringValue, txtPort.StringValue, txtDatabase.StringValue, txtUser.StringValue, txtPassword.StringValue);
            string folderPath = txtFolderPath.StringValue;

            Console.WriteLine(connectionString);

            DatabaseGenerator.Generate(dbType, connectionString, folderPath);

            txtServer.StringValue = "";
            txtPort.StringValue = "";
            txtDatabase.StringValue = "";
            txtUser.StringValue = "";
            txtPassword.StringValue = "";
            txtFolderPath.StringValue = "";
        }
    }
}
