using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200032B RID: 811
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class AddNewImContactToGroupType : BaseRequestType
	{
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00028D99 File Offset: 0x00026F99
		// (set) Token: 0x06001A4D RID: 6733 RVA: 0x00028DA1 File Offset: 0x00026FA1
		public string ImAddress
		{
			get
			{
				return this.imAddressField;
			}
			set
			{
				this.imAddressField = value;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00028DAA File Offset: 0x00026FAA
		// (set) Token: 0x06001A4F RID: 6735 RVA: 0x00028DB2 File Offset: 0x00026FB2
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00028DBB File Offset: 0x00026FBB
		// (set) Token: 0x06001A51 RID: 6737 RVA: 0x00028DC3 File Offset: 0x00026FC3
		public ItemIdType GroupId
		{
			get
			{
				return this.groupIdField;
			}
			set
			{
				this.groupIdField = value;
			}
		}

		// Token: 0x040011A0 RID: 4512
		private string imAddressField;

		// Token: 0x040011A1 RID: 4513
		private string displayNameField;

		// Token: 0x040011A2 RID: 4514
		private ItemIdType groupIdField;
	}
}
