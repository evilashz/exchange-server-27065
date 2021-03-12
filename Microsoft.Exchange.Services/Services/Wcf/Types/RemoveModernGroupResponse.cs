using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009FF RID: 2559
	[DataContract(Name = "RemoveModernGroupResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveModernGroupResponse : BaseJsonResponse
	{
		// Token: 0x0600484A RID: 18506 RVA: 0x00101623 File Offset: 0x000FF823
		internal RemoveModernGroupResponse()
		{
			this.Error = null;
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x00101632 File Offset: 0x000FF832
		internal RemoveModernGroupResponse(string error)
		{
			this.Error = error;
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x0600484C RID: 18508 RVA: 0x00101641 File Offset: 0x000FF841
		// (set) Token: 0x0600484D RID: 18509 RVA: 0x00101649 File Offset: 0x000FF849
		[DataMember(Name = "ErrorState", IsRequired = false)]
		public UnifiedGroupResponseErrorState ErrorState { get; set; }

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x0600484E RID: 18510 RVA: 0x00101652 File Offset: 0x000FF852
		// (set) Token: 0x0600484F RID: 18511 RVA: 0x0010165A File Offset: 0x000FF85A
		[DataMember(Name = "Error", IsRequired = false)]
		public string Error { get; set; }
	}
}
