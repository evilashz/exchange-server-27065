using System;
using System.Web;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200001F RID: 31
	internal interface IAuthBehavior
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CF RID: 207
		AuthState AuthState { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D0 RID: 208
		bool ShouldDoFullAuthOnUnresolvedAnchorMailbox { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D1 RID: 209
		bool ShouldCopyAuthenticationHeaderToClientResponse { get; }

		// Token: 0x060000D2 RID: 210
		void SetState(int serverVersion);

		// Token: 0x060000D3 RID: 211
		void ResetState();

		// Token: 0x060000D4 RID: 212
		string GetExecutingUserOrganization();

		// Token: 0x060000D5 RID: 213
		bool IsFullyAuthenticated();

		// Token: 0x060000D6 RID: 214
		AnchorMailbox CreateAuthModuleSpecificAnchorMailbox(IRequestContext requestContext);

		// Token: 0x060000D7 RID: 215
		void ContinueOnAuthenticate(HttpApplication app, AsyncCallback callback);

		// Token: 0x060000D8 RID: 216
		void SetFailureStatus();
	}
}
