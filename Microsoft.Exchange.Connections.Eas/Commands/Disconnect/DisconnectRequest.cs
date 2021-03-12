using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Disconnect
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class DisconnectRequest
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004612 File Offset: 0x00002812
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00004619 File Offset: 0x00002819
		[XmlIgnore]
		internal static DisconnectRequest Default { get; set; } = new DisconnectRequest();
	}
}
