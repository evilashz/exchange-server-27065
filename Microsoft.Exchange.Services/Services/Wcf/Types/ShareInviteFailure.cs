using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A15 RID: 2581
	[DataContract]
	public class ShareInviteFailure
	{
		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x060048D9 RID: 18649 RVA: 0x00101EAF File Offset: 0x001000AF
		// (set) Token: 0x060048DA RID: 18650 RVA: 0x00101EB7 File Offset: 0x001000B7
		[DataMember]
		public string Recipient { get; set; }

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x060048DB RID: 18651 RVA: 0x00101EC0 File Offset: 0x001000C0
		// (set) Token: 0x060048DC RID: 18652 RVA: 0x00101EC8 File Offset: 0x001000C8
		[DataMember]
		public string Error { get; set; }
	}
}
