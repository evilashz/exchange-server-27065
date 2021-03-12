using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000369 RID: 873
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetStreamingEventsType : BaseRequestType
	{
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x00029B0C File Offset: 0x00027D0C
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x00029B14 File Offset: 0x00027D14
		[XmlArrayItem("SubscriptionId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] SubscriptionIds
		{
			get
			{
				return this.subscriptionIdsField;
			}
			set
			{
				this.subscriptionIdsField = value;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x00029B1D File Offset: 0x00027D1D
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x00029B25 File Offset: 0x00027D25
		public int ConnectionTimeout
		{
			get
			{
				return this.connectionTimeoutField;
			}
			set
			{
				this.connectionTimeoutField = value;
			}
		}

		// Token: 0x0400128E RID: 4750
		private string[] subscriptionIdsField;

		// Token: 0x0400128F RID: 4751
		private int connectionTimeoutField;
	}
}
