using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000257 RID: 599
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ArrayOfGroupedItemsType
	{
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x00026C98 File Offset: 0x00024E98
		// (set) Token: 0x06001660 RID: 5728 RVA: 0x00026CA0 File Offset: 0x00024EA0
		[XmlElement("GroupedItems")]
		public GroupedItemsType[] Items
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

		// Token: 0x04000F50 RID: 3920
		private GroupedItemsType[] itemsField;
	}
}
