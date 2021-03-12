using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001C7 RID: 455
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ProtectionRuleConditionType
	{
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x00025496 File Offset: 0x00023696
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x0002549E File Offset: 0x0002369E
		[XmlElement("RecipientIs", typeof(ProtectionRuleRecipientIsType))]
		[XmlElement("SenderDepartments", typeof(ProtectionRuleSenderDepartmentsType))]
		[XmlChoiceIdentifier("ItemElementName")]
		[XmlElement("AllInternal", typeof(string))]
		[XmlElement("And", typeof(ProtectionRuleAndType))]
		[XmlElement("True", typeof(string))]
		public object Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x000254A7 File Offset: 0x000236A7
		// (set) Token: 0x06001388 RID: 5000 RVA: 0x000254AF File Offset: 0x000236AF
		[XmlIgnore]
		public ItemChoiceType ItemElementName
		{
			get
			{
				return this.itemElementNameField;
			}
			set
			{
				this.itemElementNameField = value;
			}
		}

		// Token: 0x04000D7D RID: 3453
		private object itemField;

		// Token: 0x04000D7E RID: 3454
		private ItemChoiceType itemElementNameField;
	}
}
