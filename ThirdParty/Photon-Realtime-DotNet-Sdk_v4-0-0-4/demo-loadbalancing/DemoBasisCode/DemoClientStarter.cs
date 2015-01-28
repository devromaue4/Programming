// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DemoClientStarter.cs" company="Exit Games GmbH">
//   Copyright (c) Exit Games GmbH.  All rights reserved.
// </copyright>
// <summary>
//   The DemoClientStarter class mainly wraps a singleton pattern around the used client which connects to the cloud. 
//   Periodically the clients service-method is called in a custom gameloop where demo-specific tasks are done.
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------
namespace ExitGames.Client.DemoBasisCode
{
    public class DemoClientStarter
    {
        #region Fields
        public DemoClient Client { get; set; }

        private static DemoClientStarter instance; 
        #endregion

        public DemoClientStarter()
        {
            this.Client = new DemoClient(true);
        }

        /// <summary>
        /// Singleton pattern. 
        /// </summary>
        public static DemoClientStarter Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new DemoClientStarter();
                }
                return instance;
            }
        }
    }
}
