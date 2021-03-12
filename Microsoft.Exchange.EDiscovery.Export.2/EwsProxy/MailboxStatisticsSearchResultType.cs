using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001AA RID: 426
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class MailboxStatisticsSearchResultType
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00024849 File Offset: 0x00022A49
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x00024851 File Offset: 0x00022A51
		public UserMailboxType UserMailbox
		{
			get
			{
				return this.userMailboxField;
			}
			set
			{
				this.userMailboxField = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0002485A File Offset: 0x00022A5A
		// (set) Token: 0x06001214 RID: 4628 RVA: 0x00024862 File Offset: 0x00022A62
		public KeywordStatisticsSearchResultType KeywordStatisticsSearchResult
		{
			get
			{
				return this.keywordStatisticsSearchResultField;
			}
			set
			{
				this.keywordStatisticsSearchResultField = value;
			}
		}

		// Token: 0x04000C50 RID: 3152
		private UserMailboxType userMailboxField;

		// Token: 0x04000C51 RID: 3153
		private KeywordStatisticsSearchResultType keywordStatisticsSearchResultField;
	}
}
