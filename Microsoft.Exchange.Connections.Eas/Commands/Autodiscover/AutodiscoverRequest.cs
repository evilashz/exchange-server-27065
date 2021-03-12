using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Request.Autodiscover;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlRoot(ElementName = "Autodiscover", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/mobilesync/requestschema/2006", IsNullable = false)]
	public class AutodiscoverRequest
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00003853 File Offset: 0x00001A53
		public AutodiscoverRequest()
		{
			this.Request = new Request();
			this.AutodiscoverOption = AutodiscoverOption.Probes;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000386D File Offset: 0x00001A6D
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003875 File Offset: 0x00001A75
		[XmlElement(ElementName = "Request")]
		public Request Request { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000387E File Offset: 0x00001A7E
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003886 File Offset: 0x00001A86
		[XmlIgnore]
		internal AutodiscoverOption AutodiscoverOption { get; set; }
	}
}
