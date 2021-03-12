using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.Autodiscover
{
	// Token: 0x0200009C RID: 156
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(TypeName = "Request")]
	public class Request
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0000A09F File Offset: 0x0000829F
		public Request()
		{
			this.AcceptableResponseSchema = "http://schemas.microsoft.com/exchange/autodiscover/mobilesync/responseschema/2006";
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000A0B2 File Offset: 0x000082B2
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000A0BA File Offset: 0x000082BA
		[XmlElement(ElementName = "EMailAddress")]
		public string EMailAddress { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000A0C3 File Offset: 0x000082C3
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000A0CB File Offset: 0x000082CB
		[XmlElement(ElementName = "AcceptableResponseSchema")]
		public string AcceptableResponseSchema { get; set; }

		// Token: 0x04000493 RID: 1171
		private const string Schema = "http://schemas.microsoft.com/exchange/autodiscover/mobilesync/responseschema/2006";
	}
}
