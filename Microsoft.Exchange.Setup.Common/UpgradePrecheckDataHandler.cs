using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000070 RID: 112
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UpgradePrecheckDataHandler : PreCheckDataHandler
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x00011D11 File Offset: 0x0000FF11
		public UpgradePrecheckDataHandler(ISetupContext setupContext, DataHandler topLevelHandler, MonadConnection connection) : base(setupContext, topLevelHandler, connection)
		{
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00011D1C File Offset: 0x0000FF1C
		public override string ShortDescription
		{
			get
			{
				return Strings.UpgradePreCheckText;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00011D28 File Offset: 0x0000FF28
		public override string Title
		{
			get
			{
				return Strings.UpgradePrereq;
			}
		}
	}
}
