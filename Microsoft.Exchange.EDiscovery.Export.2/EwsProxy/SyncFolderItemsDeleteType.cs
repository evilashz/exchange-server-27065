using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001FD RID: 509
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class SyncFolderItemsDeleteType
	{
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x00025BF0 File Offset: 0x00023DF0
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x00025BF8 File Offset: 0x00023DF8
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

		// Token: 0x04000E0E RID: 3598
		private ItemIdType itemIdField;
	}
}
