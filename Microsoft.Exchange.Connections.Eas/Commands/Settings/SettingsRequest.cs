using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Request.Settings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Settings
{
	// Token: 0x02000067 RID: 103
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlRoot(ElementName = "Settings", Namespace = "Settings", IsNullable = false)]
	public class SettingsRequest : Settings
	{
		// Token: 0x0400019E RID: 414
		[XmlIgnore]
		internal static readonly SettingsRequest Default = new SettingsRequest
		{
			UserInformation = new UserInformation
			{
				Get = true
			}
		};
	}
}
