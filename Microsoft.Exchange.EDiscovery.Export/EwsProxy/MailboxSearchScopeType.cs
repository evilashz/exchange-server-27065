using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200019F RID: 415
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MailboxSearchScopeType
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x000244C0 File Offset: 0x000226C0
		// (set) Token: 0x060011A7 RID: 4519 RVA: 0x000244C8 File Offset: 0x000226C8
		public string Mailbox
		{
			get
			{
				return this.mailboxField;
			}
			set
			{
				this.mailboxField = value;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x000244D1 File Offset: 0x000226D1
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x000244D9 File Offset: 0x000226D9
		public MailboxSearchLocationType SearchScope
		{
			get
			{
				return this.searchScopeField;
			}
			set
			{
				this.searchScopeField = value;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x000244E2 File Offset: 0x000226E2
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x000244EA File Offset: 0x000226EA
		[XmlArrayItem("ExtendedAttribute", IsNullable = false)]
		public ExtendedAttributeType[] ExtendedAttributes
		{
			get
			{
				return this.extendedAttributesField;
			}
			set
			{
				this.extendedAttributesField = value;
			}
		}

		// Token: 0x04000C18 RID: 3096
		private string mailboxField;

		// Token: 0x04000C19 RID: 3097
		private MailboxSearchLocationType searchScopeField;

		// Token: 0x04000C1A RID: 3098
		private ExtendedAttributeType[] extendedAttributesField;
	}
}
