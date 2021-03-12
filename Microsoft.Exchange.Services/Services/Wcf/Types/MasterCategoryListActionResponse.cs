using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A63 RID: 2659
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MasterCategoryListActionResponse
	{
		// Token: 0x06004B77 RID: 19319 RVA: 0x0010584F File Offset: 0x00103A4F
		public MasterCategoryListActionResponse(CategoryType[] masterCategoryList)
		{
			this.WasSuccessful = true;
			this.MasterList = masterCategoryList;
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x00105865 File Offset: 0x00103A65
		public MasterCategoryListActionResponse(MasterCategoryListActionError errorCode)
		{
			this.WasSuccessful = false;
			this.ErrorCode = errorCode;
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06004B79 RID: 19321 RVA: 0x0010587B File Offset: 0x00103A7B
		// (set) Token: 0x06004B7A RID: 19322 RVA: 0x00105883 File Offset: 0x00103A83
		[DataMember]
		public CategoryType[] MasterList { get; set; }

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06004B7B RID: 19323 RVA: 0x0010588C File Offset: 0x00103A8C
		// (set) Token: 0x06004B7C RID: 19324 RVA: 0x00105894 File Offset: 0x00103A94
		[DataMember]
		public bool WasSuccessful { get; set; }

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06004B7D RID: 19325 RVA: 0x0010589D File Offset: 0x00103A9D
		// (set) Token: 0x06004B7E RID: 19326 RVA: 0x001058A5 File Offset: 0x00103AA5
		[DataMember]
		public MasterCategoryListActionError ErrorCode { get; set; }
	}
}
