using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B27 RID: 2855
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetModernGroupMembershipResponse
	{
		// Token: 0x060050DC RID: 20700 RVA: 0x00109FE0 File Offset: 0x001081E0
		public SetModernGroupMembershipResponse()
		{
			this.ErrorCode = ModernGroupActionError.None;
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x00109FEF File Offset: 0x001081EF
		public SetModernGroupMembershipResponse(ModernGroupActionError errorCode)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x060050DE RID: 20702 RVA: 0x00109FFE File Offset: 0x001081FE
		// (set) Token: 0x060050DF RID: 20703 RVA: 0x0010A006 File Offset: 0x00108206
		[DataMember]
		public UnifiedGroupResponseErrorState ErrorState { get; set; }

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x060050E0 RID: 20704 RVA: 0x0010A00F File Offset: 0x0010820F
		// (set) Token: 0x060050E1 RID: 20705 RVA: 0x0010A017 File Offset: 0x00108217
		[DataMember]
		public string Error { get; set; }

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x060050E2 RID: 20706 RVA: 0x0010A020 File Offset: 0x00108220
		// (set) Token: 0x060050E3 RID: 20707 RVA: 0x0010A028 File Offset: 0x00108228
		[DataMember]
		public ModernGroupActionError ErrorCode { get; set; }

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x060050E4 RID: 20708 RVA: 0x0010A031 File Offset: 0x00108231
		// (set) Token: 0x060050E5 RID: 20709 RVA: 0x0010A039 File Offset: 0x00108239
		[DataMember]
		public JoinResponse JoinInfo { get; set; }
	}
}
