using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x020001A0 RID: 416
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlType(AnonymousType = true, Namespace = "HMMAIL:")]
	[XmlRoot(Namespace = "HMMAIL:", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class Flag
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0001DFE1 File Offset: 0x0001C1E1
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x0001DFE9 File Offset: 0x0001C1E9
		[XmlElement("Completed", typeof(byte))]
		[XmlElement("ReminderDate", typeof(string))]
		[XmlElement("State", typeof(byte))]
		[XmlElement("Title", typeof(stringWithCharSetType))]
		[XmlChoiceIdentifier("ItemsElementName")]
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

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0001DFF2 File Offset: 0x0001C1F2
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x0001DFFA File Offset: 0x0001C1FA
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

		// Token: 0x040006A2 RID: 1698
		private object[] itemsField;

		// Token: 0x040006A3 RID: 1699
		private ItemsChoiceType2[] itemsElementNameField;
	}
}
