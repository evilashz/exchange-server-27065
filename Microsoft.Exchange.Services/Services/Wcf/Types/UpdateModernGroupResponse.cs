using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B49 RID: 2889
	[DataContract(Name = "UpdateModernGroupResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateModernGroupResponse : BaseJsonResponse
	{
		// Token: 0x060051E4 RID: 20964 RVA: 0x0010B075 File Offset: 0x00109275
		internal UpdateModernGroupResponse()
		{
			this.Error = null;
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x0010B084 File Offset: 0x00109284
		internal UpdateModernGroupResponse(string error)
		{
			this.Error = error;
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x060051E6 RID: 20966 RVA: 0x0010B093 File Offset: 0x00109293
		// (set) Token: 0x060051E7 RID: 20967 RVA: 0x0010B09B File Offset: 0x0010929B
		[DataMember(Name = "ErrorState", IsRequired = false)]
		public UnifiedGroupResponseErrorState ErrorState { get; set; }

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x060051E8 RID: 20968 RVA: 0x0010B0A4 File Offset: 0x001092A4
		// (set) Token: 0x060051E9 RID: 20969 RVA: 0x0010B0AC File Offset: 0x001092AC
		[DataMember(Name = "Error", IsRequired = false)]
		public string Error { get; set; }
	}
}
