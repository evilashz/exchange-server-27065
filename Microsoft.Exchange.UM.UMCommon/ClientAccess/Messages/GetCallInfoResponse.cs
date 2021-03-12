using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000080 RID: 128
	[Serializable]
	public class GetCallInfoResponse : ResponseBase
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000F2F4 File Offset: 0x0000D4F4
		public UMCallInfoEx CallInfo
		{
			get
			{
				return this.callInfo;
			}
			set
			{
				this.callInfo = value;
			}
		}

		// Token: 0x040002F2 RID: 754
		private UMCallInfoEx callInfo;
	}
}
