using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200030E RID: 782
	internal class GetBposNavBarData : ServiceCommand<NavBarData>
	{
		// Token: 0x060019F8 RID: 6648 RVA: 0x0005DBCE File Offset: 0x0005BDCE
		public GetBposNavBarData(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0005DBD8 File Offset: 0x0005BDD8
		protected override NavBarData InternalExecute()
		{
			AuthZClientInfo effectiveCaller = CallContext.Current.EffectiveCaller;
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, effectiveCaller, true);
			if (!userContext.IsBposUser)
			{
				return null;
			}
			BposNavBarInfoAssetReader bposNavBarInfoAssetReader = userContext.BposNavBarInfoAssetReader;
			return bposNavBarInfoAssetReader.GetData(effectiveCaller).NavBarData;
		}
	}
}
