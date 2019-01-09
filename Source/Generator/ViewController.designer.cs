// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Generator
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSComboBox cbbDBType { get; set; }

		[Outlet]
		AppKit.NSTextField txtDatabase { get; set; }

		[Outlet]
		AppKit.NSTextField txtFolderPath { get; set; }

		[Outlet]
		AppKit.NSTextField txtPassword { get; set; }

		[Outlet]
		AppKit.NSTextField txtPort { get; set; }

		[Outlet]
		AppKit.NSTextField txtServer { get; set; }

		[Outlet]
		AppKit.NSTextField txtUser { get; set; }

		[Action ("btnGenerate:")]
		partial void btnGenerate (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (cbbDBType != null) {
				cbbDBType.Dispose ();
				cbbDBType = null;
			}

			if (txtDatabase != null) {
				txtDatabase.Dispose ();
				txtDatabase = null;
			}

			if (txtPassword != null) {
				txtPassword.Dispose ();
				txtPassword = null;
			}

			if (txtPort != null) {
				txtPort.Dispose ();
				txtPort = null;
			}

			if (txtServer != null) {
				txtServer.Dispose ();
				txtServer = null;
			}

			if (txtUser != null) {
				txtUser.Dispose ();
				txtUser = null;
			}

			if (txtFolderPath != null) {
				txtFolderPath.Dispose ();
				txtFolderPath = null;
			}
		}
	}
}
