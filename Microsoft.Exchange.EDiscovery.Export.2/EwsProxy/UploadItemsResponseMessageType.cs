using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200026B RID: 619
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UploadItemsResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x000273CB File Offset: 0x000255CB
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x000273D3 File Offset: 0x000255D3
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

		// Token: 0x04000FB3 RID: 4019
		private ItemIdType itemIdField;
	}
}
