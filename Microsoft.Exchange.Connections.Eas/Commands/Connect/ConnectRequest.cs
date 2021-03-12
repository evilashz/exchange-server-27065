using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Commands.Autodiscover;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Connect
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class ConnectRequest
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x0000445E File Offset: 0x0000265E
		internal ConnectRequest()
		{
			this.AutodiscoverOption = AutodiscoverOption.ExistingEndpointAndProbes;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000446D File Offset: 0x0000266D
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00004474 File Offset: 0x00002674
		[XmlIgnore]
		internal static ConnectRequest Default { get; set; } = new ConnectRequest();

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000447C File Offset: 0x0000267C
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00004484 File Offset: 0x00002684
		[XmlIgnore]
		internal AutodiscoverOption AutodiscoverOption { get; set; }
	}
}
