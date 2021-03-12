using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000185 RID: 389
	[XmlType(AnonymousType = true, Namespace = "HMMAIL:")]
	[XmlRoot(Namespace = "HMMAIL:", IsNullable = false)]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class Flag
	{
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0001DB48 File Offset: 0x0001BD48
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x0001DB50 File Offset: 0x0001BD50
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("State", typeof(byte))]
		[XmlElement("Completed", typeof(byte))]
		[XmlElement("ReminderDate", typeof(string))]
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

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0001DB59 File Offset: 0x0001BD59
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x0001DB61 File Offset: 0x0001BD61
		[XmlIgnore]
		[XmlElement("ItemsElementName")]
		public ItemsChoiceType2[] ItemsElementName
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

		// Token: 0x0400063F RID: 1599
		private object[] itemsField;

		// Token: 0x04000640 RID: 1600
		private ItemsChoiceType2[] itemsElementNameField;
	}
}
