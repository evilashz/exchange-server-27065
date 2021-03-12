using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x02000135 RID: 309
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ArrayOfResponseMessagesType
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x000250F4 File Offset: 0x000232F4
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x000250FC File Offset: 0x000232FC
		[XmlChoiceIdentifier("ItemsElementName")]
		public ResponseMessageType[] Items
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

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00025105 File Offset: 0x00023305
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x0002510D File Offset: 0x0002330D
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

		// Token: 0x040006A4 RID: 1700
		private ResponseMessageType[] itemsField;

		// Token: 0x040006A5 RID: 1701
		private ItemsChoiceType3[] itemsElementNameField;
	}
}
