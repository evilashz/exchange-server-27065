using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x0200070F RID: 1807
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AppAuthorizationResponse
	{
		// Token: 0x06002213 RID: 8723 RVA: 0x00046240 File Offset: 0x00044440
		internal AppAuthorizationResponse()
		{
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x00046248 File Offset: 0x00044448
		// (set) Token: 0x06002215 RID: 8725 RVA: 0x00046250 File Offset: 0x00044450
		public string AppAuthorizationCode { get; internal set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x00046259 File Offset: 0x00044459
		// (set) Token: 0x06002217 RID: 8727 RVA: 0x00046261 File Offset: 0x00044461
		public string Error { get; internal set; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x0004626A File Offset: 0x0004446A
		// (set) Token: 0x06002219 RID: 8729 RVA: 0x00046272 File Offset: 0x00044472
		public string ErrorReason { get; internal set; }

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x0004627B File Offset: 0x0004447B
		// (set) Token: 0x0600221B RID: 8731 RVA: 0x00046283 File Offset: 0x00044483
		public string ErrorDescription { get; internal set; }
	}
}
