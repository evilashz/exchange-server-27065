using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000744 RID: 1860
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class LinkedInAppAuthorizationResponse
	{
		// Token: 0x06002421 RID: 9249 RVA: 0x0004B6B5 File Offset: 0x000498B5
		internal LinkedInAppAuthorizationResponse()
		{
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x0004B6BD File Offset: 0x000498BD
		// (set) Token: 0x06002423 RID: 9251 RVA: 0x0004B6C5 File Offset: 0x000498C5
		public string RequestToken { get; internal set; }

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x0004B6CE File Offset: 0x000498CE
		// (set) Token: 0x06002425 RID: 9253 RVA: 0x0004B6D6 File Offset: 0x000498D6
		public string RequestSecret { get; internal set; }

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x0004B6DF File Offset: 0x000498DF
		// (set) Token: 0x06002427 RID: 9255 RVA: 0x0004B6E7 File Offset: 0x000498E7
		public string OAuthVerifier { get; internal set; }

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x0004B6F0 File Offset: 0x000498F0
		// (set) Token: 0x06002429 RID: 9257 RVA: 0x0004B6F8 File Offset: 0x000498F8
		public string AppAuthorizationRedirectUri { get; internal set; }

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x0004B701 File Offset: 0x00049901
		// (set) Token: 0x0600242B RID: 9259 RVA: 0x0004B709 File Offset: 0x00049909
		public string OAuthProblem { get; internal set; }
	}
}
