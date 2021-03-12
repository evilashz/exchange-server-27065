using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001C8 RID: 456
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ProtectionRuleAndType
	{
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x000254C0 File Offset: 0x000236C0
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x000254C8 File Offset: 0x000236C8
		[XmlElement("AllInternal", typeof(string))]
		[XmlElement("True", typeof(string))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("And", typeof(ProtectionRuleAndType))]
		[XmlElement("RecipientIs", typeof(ProtectionRuleRecipientIsType))]
		[XmlElement("SenderDepartments", typeof(ProtectionRuleSenderDepartmentsType))]
		public object[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x000254D1 File Offset: 0x000236D1
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x000254D9 File Offset: 0x000236D9
		[XmlElement("ItemsElementName")]
		[XmlIgnore]
		public ItemsChoiceType3[] ItemsElementName
		{
			get
			{
				return this.itemsElementNameField;
			}
			set
			{
				this.itemsElementNameField = value;
			}
		}

		// Token: 0x04000D7F RID: 3455
		private object[] itemsField;

		// Token: 0x04000D80 RID: 3456
		private ItemsChoiceType3[] itemsElementNameField;
	}
}
