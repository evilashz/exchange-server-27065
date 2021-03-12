using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000ED RID: 237
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EmailAddressType : BaseEmailAddressType
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00020DE1 File Offset: 0x0001EFE1
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x00020DE9 File Offset: 0x0001EFE9
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00020DF2 File Offset: 0x0001EFF2
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x00020DFA File Offset: 0x0001EFFA
		public string EmailAddress
		{
			get
			{
				return this.emailAddressField;
			}
			set
			{
				this.emailAddressField = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00020E03 File Offset: 0x0001F003
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x00020E0B File Offset: 0x0001F00B
		public string RoutingType
		{
			get
			{
				return this.routingTypeField;
			}
			set
			{
				this.routingTypeField = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00020E14 File Offset: 0x0001F014
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x00020E1C File Offset: 0x0001F01C
		public MailboxTypeType MailboxType
		{
			get
			{
				return this.mailboxTypeField;
			}
			set
			{
				this.mailboxTypeField = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00020E25 File Offset: 0x0001F025
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x00020E2D File Offset: 0x0001F02D
		[XmlIgnore]
		public bool MailboxTypeSpecified
		{
			get
			{
				return this.mailboxTypeFieldSpecified;
			}
			set
			{
				this.mailboxTypeFieldSpecified = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00020E36 File Offset: 0x0001F036
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x00020E3E File Offset: 0x0001F03E
		public ItemIdType ItemId
		{
			get
			{
				return this.itemIdField;
			}
			set
			{
				this.itemIdField = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00020E47 File Offset: 0x0001F047
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x00020E4F File Offset: 0x0001F04F
		public string OriginalDisplayName
		{
			get
			{
				return this.originalDisplayNameField;
			}
			set
			{
				this.originalDisplayNameField = value;
			}
		}

		// Token: 0x040007ED RID: 2029
		private string nameField;

		// Token: 0x040007EE RID: 2030
		private string emailAddressField;

		// Token: 0x040007EF RID: 2031
		private string routingTypeField;

		// Token: 0x040007F0 RID: 2032
		private MailboxTypeType mailboxTypeField;

		// Token: 0x040007F1 RID: 2033
		private bool mailboxTypeFieldSpecified;

		// Token: 0x040007F2 RID: 2034
		private ItemIdType itemIdField;

		// Token: 0x040007F3 RID: 2035
		private string originalDisplayNameField;
	}
}
