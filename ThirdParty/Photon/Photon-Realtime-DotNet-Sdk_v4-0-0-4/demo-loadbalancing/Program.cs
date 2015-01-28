// -----------------------------------------------------------------------
// <copyright file="DemoClient.cs" company="Exit Games GmbH">
//   Loadbalancing Demo for Photon - Copyright (C) 2012 Exit Games GmbH
// </copyright>
// <summary>
//   This project is a simple demo for the DotNet Loadbalancing API and 
//   Photon Cloud usage.
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace demo_loadbalancing
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
