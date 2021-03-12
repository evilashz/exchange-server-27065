using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000251 RID: 593
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ResolutionType
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x00026B1C File Offset: 0x00024D1C
		// (set) Token: 0x06001633 RID: 5683 RVA: 0x00026B24 File Offset: 0x00024D24
		public EmailAddressType Mailbox
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

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00026B2D File Offset: 0x00024D2D
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x00026B35 File Offset: 0x00024D35
		public ContactItemType Contact
		{
			get
			{
				return this.contactField;
			}
			set
			{
				this.contactField = value;
			}
		}

		// Token: 0x04000F38 RID: 3896
		private EmailAddressType mailboxField;

		// Token: 0x04000F39 RID: 3897
		private ContactItemType contactField;
	}
}
