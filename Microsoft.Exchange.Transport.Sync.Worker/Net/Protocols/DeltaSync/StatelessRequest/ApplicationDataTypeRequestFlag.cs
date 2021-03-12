using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x0200016D RID: 365
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public class ApplicationDataTypeRequestFlag
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0001D7A3 File Offset: 0x0001B9A3
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x0001D7AB File Offset: 0x0001B9AB
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("ReminderDate", typeof(string))]
		[XmlElement("Completed", typeof(byte))]
		[XmlElement("State", typeof(byte))]
		[XmlElement("Title", typeof(stringWithCharSetType))]
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

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0001D7B4 File Offset: 0x0001B9B4
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x0001D7BC File Offset: 0x0001B9BC
		[XmlIgnore]
		[XmlElement("ItemsElementName")]
		public ItemsChoiceType[] ItemsElementName
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

		// Token: 0x040005E6 RID: 1510
		private object[] itemsField;

		// Token: 0x040005E7 RID: 1511
		private ItemsChoiceType[] itemsElementNameField;
	}
}
