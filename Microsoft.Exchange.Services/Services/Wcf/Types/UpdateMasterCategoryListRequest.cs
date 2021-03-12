using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A62 RID: 2658
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateMasterCategoryListRequest
	{
		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06004B70 RID: 19312 RVA: 0x00105814 File Offset: 0x00103A14
		// (set) Token: 0x06004B71 RID: 19313 RVA: 0x0010581C File Offset: 0x00103A1C
		[DataMember(Name = "AddCategoryList", IsRequired = false)]
		public CategoryType[] AddCategoryList { get; set; }

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06004B72 RID: 19314 RVA: 0x00105825 File Offset: 0x00103A25
		// (set) Token: 0x06004B73 RID: 19315 RVA: 0x0010582D File Offset: 0x00103A2D
		[DataMember(Name = "RemoveCategoryList", IsRequired = false)]
		public string[] RemoveCategoryList { get; set; }

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06004B74 RID: 19316 RVA: 0x00105836 File Offset: 0x00103A36
		// (set) Token: 0x06004B75 RID: 19317 RVA: 0x0010583E File Offset: 0x00103A3E
		[DataMember(Name = "ChangeCategoryColorList", IsRequired = false)]
		public CategoryType[] ChangeCategoryColorList { get; set; }
	}
}
