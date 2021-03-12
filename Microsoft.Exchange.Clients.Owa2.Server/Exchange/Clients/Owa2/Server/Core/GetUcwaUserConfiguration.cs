using System;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200032D RID: 813
	internal class GetUcwaUserConfiguration : ServiceCommand<UcwaUserConfiguration>
	{
		// Token: 0x06001B0D RID: 6925 RVA: 0x00066AF0 File Offset: 0x00064CF0
		public GetUcwaUserConfiguration(CallContext callContext, string sipUri) : base(callContext)
		{
			this.sipUri = sipUri;
			OwsLogRegistry.Register(GetUcwaUserConfiguration.GetUcwaUserConfigurationActionName, typeof(GetUcwaUserConfigurationMetaData), new Type[0]);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x00066B1A File Offset: 0x00064D1A
		protected override UcwaUserConfiguration InternalExecute()
		{
			return UcwaConfigurationUtilities.GetUcwaUserConfiguration(this.sipUri, base.CallContext);
		}

		// Token: 0x04000F02 RID: 3842
		private static readonly string GetUcwaUserConfigurationActionName = typeof(GetUcwaUserConfiguration).Name;

		// Token: 0x04000F03 RID: 3843
		private readonly string sipUri;
	}
}
