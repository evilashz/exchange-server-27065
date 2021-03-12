using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A64 RID: 2660
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateMasterCategoryListResponse : MasterCategoryListActionResponse
	{
		// Token: 0x06004B7F RID: 19327 RVA: 0x001058AE File Offset: 0x00103AAE
		public UpdateMasterCategoryListResponse(CategoryType[] masterCategoryList) : base(masterCategoryList)
		{
		}
	}
}
