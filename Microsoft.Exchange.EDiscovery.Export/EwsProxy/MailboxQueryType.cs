using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200019E RID: 414
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class MailboxQueryType
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00024496 File Offset: 0x00022696
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x0002449E File Offset: 0x0002269E
		public string Query
		{
			get
			{
				return this.queryField;
			}
			set
			{
				this.queryField = value;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x000244A7 File Offset: 0x000226A7
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x000244AF File Offset: 0x000226AF
		[XmlArrayItem("MailboxSearchScope", IsNullable = false)]
		public MailboxSearchScopeType[] MailboxSearchScopes
		{
			get
			{
				return this.mailboxSearchScopesField;
			}
			set
			{
				this.mailboxSearchScopesField = value;
			}
		}

		// Token: 0x04000C16 RID: 3094
		private string queryField;

		// Token: 0x04000C17 RID: 3095
		private MailboxSearchScopeType[] mailboxSearchScopesField;
	}
}
