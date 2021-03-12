using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000278 RID: 632
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlRoot("SharingSecurity", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[Serializable]
	public sealed class SharingSecurityHeader : SoapHeader
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x00054746 File Offset: 0x00052946
		public SharingSecurityHeader()
		{
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0005474E File Offset: 0x0005294E
		internal SharingSecurityHeader(XmlElement any)
		{
			this.Any = any;
		}

		// Token: 0x04000BE1 RID: 3041
		[XmlAnyElement]
		public XmlElement Any;
	}
}
