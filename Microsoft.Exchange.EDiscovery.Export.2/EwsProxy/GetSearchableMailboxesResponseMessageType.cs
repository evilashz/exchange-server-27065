using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001A8 RID: 424
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetSearchableMailboxesResponseMessageType : ResponseMessageType
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00024806 File Offset: 0x00022A06
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x0002480E File Offset: 0x00022A0E
		[XmlArrayItem("SearchableMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public SearchableMailboxType[] SearchableMailboxes
		{
			get
			{
				return this.searchableMailboxesField;
			}
			set
			{
				this.searchableMailboxesField = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00024817 File Offset: 0x00022A17
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x0002481F File Offset: 0x00022A1F
		[XmlArrayItem("FailedMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FailedSearchMailboxType[] FailedMailboxes
		{
			get
			{
				return this.failedMailboxesField;
			}
			set
			{
				this.failedMailboxesField = value;
			}
		}

		// Token: 0x04000C4D RID: 3149
		private SearchableMailboxType[] searchableMailboxesField;

		// Token: 0x04000C4E RID: 3150
		private FailedSearchMailboxType[] failedMailboxesField;
	}
}
