using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003A5 RID: 933
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ExportItemsType : BaseRequestType
	{
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x0002A601 File Offset: 0x00028801
		// (set) Token: 0x06001D31 RID: 7473 RVA: 0x0002A609 File Offset: 0x00028809
		[XmlArrayItem("ItemId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ItemIdType[] ItemIds
		{
			get
			{
				return this.itemIdsField;
			}
			set
			{
				this.itemIdsField = value;
			}
		}

		// Token: 0x04001358 RID: 4952
		private ItemIdType[] itemIdsField;
	}
}
