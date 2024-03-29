// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if WINDOWS_UWP
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
#endif

namespace CommunityToolkit.WinUI.Notifications
{
    /// <summary>
    /// Base Tile element, which contains a single visual element.
    /// </summary>
    public sealed class TileContent
    {
        /// <summary>
        /// Gets or sets the visual element. Required.
        /// </summary>
        public TileVisual Visual { get; set; }

        /// <summary>
        /// Retrieves the notification XML content as a string, so that it can be sent with a HTTP POST in a push notification.
        /// </summary>
        /// <returns>The notification XML content as a string.</returns>
        public string GetContent()
        {
            return ConvertToElement().GetContent();
        }

#if WINDOWS_UWP
        /// <summary>
        /// Retrieves the notification XML content as a WinRT XmlDocument, so that it can be used with a local Tile notification's constructor on either <see cref="TileNotification"/> or <see cref="ScheduledTileNotification"/>.
        /// </summary>
        /// <returns>The notification XML content as a WinRT XmlDocument.</returns>
        public XmlDocument GetXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(GetContent());

            return doc;
        }
#endif

        internal Element_Tile ConvertToElement()
        {
            var tile = new Element_Tile();

            if (Visual != null)
            {
                tile.Visual = Visual.ConvertToElement();
            }

            return tile;
        }
    }
}