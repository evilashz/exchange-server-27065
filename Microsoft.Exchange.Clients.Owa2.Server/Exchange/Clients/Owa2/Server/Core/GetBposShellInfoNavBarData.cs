using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200030F RID: 783
	internal class GetBposShellInfoNavBarData : ServiceCommand<NavBarData>
	{
		// Token: 0x060019FA RID: 6650 RVA: 0x0005DC1F File Offset: 0x0005BE1F
		public GetBposShellInfoNavBarData(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0005DC28 File Offset: 0x0005BE28
		protected override NavBarData InternalExecute()
		{
			AuthZClientInfo effectiveCaller = CallContext.Current.EffectiveCaller;
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, effectiveCaller, true);
			if (!userContext.IsBposUser)
			{
				return null;
			}
			BposShellInfoAssetReader bposShellInfoAssetReader = userContext.BposShellInfoAssetReader;
			BposAssetReader<BposShellInfo>.RegisterEvents(base.GetType().Name);
			return bposShellInfoAssetReader.GetData(effectiveCaller).NavBarData;
		}
	}
}
