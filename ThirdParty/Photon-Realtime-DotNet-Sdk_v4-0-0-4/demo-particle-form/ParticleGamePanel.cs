// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Exit Games GmbH">
//   Exit Games GmbH, 2012
// </copyright>
// <summary>
//   Windows Forms GUI for the "Particle" Photon Demo.
//   The base logic is referenced from another project.
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

namespace ExitGames.Client.DemoParticle
{
    using System.Windows.Forms;

    /// <summary>
    /// Simple double buffered Panel (improves performance and removed flickering).
    /// </summary>
    class ParticleGamePanel : Panel
    {
        public ParticleGamePanel() : base()
        {
            this.DoubleBuffered = true;
        }
    }
}
