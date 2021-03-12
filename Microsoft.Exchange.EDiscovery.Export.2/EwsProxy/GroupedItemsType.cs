using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000258 RID: 600
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GroupedItemsType
	{
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x00026CB1 File Offset: 0x00024EB1
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x00026CB9 File Offset: 0x00024EB9
		public string GroupIndex
		{
			get
			{
				return this.groupIndexField;
			}
			set
			{
				this.groupIndexField = value;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x00026CC2 File Offset: 0x00024EC2
		// (set) Token: 0x06001665 RID: 5733 RVA: 0x00026CCA File Offset: 0x00024ECA
		public ArrayOfRealItemsType Items
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

		// Token: 0x04000F51 RID: 3921
		private string groupIndexField;

		// Token: 0x04000F52 RID: 3922
		private ArrayOfRealItemsType itemsField;
	}
}
