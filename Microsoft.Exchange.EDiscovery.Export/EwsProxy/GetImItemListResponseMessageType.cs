using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000FE RID: 254
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetImItemListResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0002122B File Offset: 0x0001F42B
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x00021233 File Offset: 0x0001F433
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

		// Token: 0x04000862 RID: 2146
		private ImItemListType imItemListField;
	}
}
