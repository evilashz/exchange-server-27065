using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x0200018E RID: 398
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public class ApplicationDataTypeResponseFlag
	{
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001DCE4 File Offset: 0x0001BEE4
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x0001DCEC File Offset: 0x0001BEEC
		[XmlElement("ReminderDate", typeof(string))]
		[XmlElement("Title", typeof(stringWithCharSetType))]
		[XmlElement("Completed", typeof(byte))]
		[XmlElement("State", typeof(byte))]
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

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0001DCF5 File Offset: 0x0001BEF5
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0001DCFD File Offset: 0x0001BEFD
		[XmlElement("ItemsElementName")]
		[XmlIgnore]
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

		// Token: 0x04000659 RID: 1625
		private object[] itemsField;

		// Token: 0x0400065A RID: 1626
		private ItemsChoiceType[] itemsElementNameField;
	}
}
