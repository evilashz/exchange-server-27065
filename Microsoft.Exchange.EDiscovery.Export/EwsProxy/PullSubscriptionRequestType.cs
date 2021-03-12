using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002D4 RID: 724
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PullSubscriptionRequestType : BaseSubscriptionRequestType
	{
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00027ECC File Offset: 0x000260CC
		// (set) Token: 0x0600188B RID: 6283 RVA: 0x00027ED4 File Offset: 0x000260D4
		public int Timeout
		{
			get
			{
				return this.timeoutField;
			}
			set
			{
				this.timeoutField = value;
			}
		}

		// Token: 0x040010A2 RID: 4258
		private int timeoutField;
	}
}
