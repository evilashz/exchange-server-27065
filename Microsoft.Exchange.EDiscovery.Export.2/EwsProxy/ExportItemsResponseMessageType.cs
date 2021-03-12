using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200026A RID: 618
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ExportItemsResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x000273A1 File Offset: 0x000255A1
		// (set) Token: 0x06001735 RID: 5941 RVA: 0x000273A9 File Offset: 0x000255A9
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

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x000273B2 File Offset: 0x000255B2
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x000273BA File Offset: 0x000255BA
		[XmlElement(DataType = "base64Binary")]
		public byte[] Data
		{
			get
			{
				return this.dataField;
			}
			set
			{
				this.dataField = value;
			}
		}

		// Token: 0x04000FB1 RID: 4017
		private ItemIdType itemIdField;

		// Token: 0x04000FB2 RID: 4018
		private byte[] dataField;
	}
}
