using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020000DB RID: 219
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TimeZoneContext : SoapHeader
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000192A7 File Offset: 0x000174A7
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x000192AF File Offset: 0x000174AF
		[XmlAnyElement]
		public XmlElement TimeZoneDefinitionValue
		{
			get
			{
				return this.timeZoneDefinitionValue;
			}
			set
			{
				this.timeZoneDefinitionValue = value;
			}
		}

		// Token: 0x04000357 RID: 855
		private XmlElement timeZoneDefinitionValue;
	}
}
