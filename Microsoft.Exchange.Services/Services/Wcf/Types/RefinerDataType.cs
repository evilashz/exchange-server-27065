using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B39 RID: 2873
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "RefinerDataType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RefinerDataType
	{
		// Token: 0x0600516D RID: 20845 RVA: 0x0010A7B5 File Offset: 0x001089B5
		public RefinerDataType()
		{
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0010A7BD File Offset: 0x001089BD
		public RefinerDataType(RefinerCategoryType refinerCategory, RefinerDataEntryType[] refinerDataEntryTypes)
		{
			this.RefinerCategory = refinerCategory;
			this.RefinerDataEntryTypes = refinerDataEntryTypes;
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x0600516F RID: 20847 RVA: 0x0010A7D3 File Offset: 0x001089D3
		// (set) Token: 0x06005170 RID: 20848 RVA: 0x0010A7DB File Offset: 0x001089DB
		[DataMember(IsRequired = true)]
		public RefinerCategoryType RefinerCategory { get; set; }

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06005171 RID: 20849 RVA: 0x0010A7E4 File Offset: 0x001089E4
		// (set) Token: 0x06005172 RID: 20850 RVA: 0x0010A7EC File Offset: 0x001089EC
		[DataMember(IsRequired = true)]
		public RefinerDataEntryType[] RefinerDataEntryTypes { get; set; }
	}
}
