using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000D8 RID: 216
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetImItemsResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x000204E9 File Offset: 0x0001E6E9
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x000204F1 File Offset: 0x0001E6F1
		public ImItemListType ImItemList
		{
			get
			{
				return this.imItemListField;
			}
			set
			{
				this.imItemListField = value;
			}
		}

		// Token: 0x040005DB RID: 1499
		private ImItemListType imItemListField;
	}
}
