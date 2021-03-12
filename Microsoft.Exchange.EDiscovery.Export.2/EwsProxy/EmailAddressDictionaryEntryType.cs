using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000150 RID: 336
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EmailAddressDictionaryEntryType
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00022CD5 File Offset: 0x00020ED5
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x00022CDD File Offset: 0x00020EDD
		[XmlAttribute]
		public EmailAddressKeyType Key
		{
			get
			{
				return this.keyField;
			}
			set
			{
				this.keyField = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00022CE6 File Offset: 0x00020EE6
		// (set) Token: 0x06000ED5 RID: 3797 RVA: 0x00022CEE File Offset: 0x00020EEE
		[XmlAttribute]
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

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00022CF7 File Offset: 0x00020EF7
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x00022CFF File Offset: 0x00020EFF
		[XmlAttribute]
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

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00022D08 File Offset: 0x00020F08
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x00022D10 File Offset: 0x00020F10
		[XmlAttribute]
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

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00022D19 File Offset: 0x00020F19
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x00022D21 File Offset: 0x00020F21
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

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00022D2A File Offset: 0x00020F2A
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x00022D32 File Offset: 0x00020F32
		[XmlText]
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04000A39 RID: 2617
		private EmailAddressKeyType keyField;

		// Token: 0x04000A3A RID: 2618
		private string nameField;

		// Token: 0x04000A3B RID: 2619
		private string routingTypeField;

		// Token: 0x04000A3C RID: 2620
		private MailboxTypeType mailboxTypeField;

		// Token: 0x04000A3D RID: 2621
		private bool mailboxTypeFieldSpecified;

		// Token: 0x04000A3E RID: 2622
		private string valueField;
	}
}
