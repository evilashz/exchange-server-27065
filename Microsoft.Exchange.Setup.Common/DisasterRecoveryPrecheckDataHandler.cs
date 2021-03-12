using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DisasterRecoveryPrecheckDataHandler : PreCheckDataHandler
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00006E8D File Offset: 0x0000508D
		public DisasterRecoveryPrecheckDataHandler(ISetupContext setupContext, DataHandler topLevelHandler, MonadConnection connection) : base(setupContext, topLevelHandler, connection)
		{
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00006E98 File Offset: 0x00005098
		public override string ShortDescription
		{
			get
			{
				return Strings.DRPreCheckText;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006EA4 File Offset: 0x000050A4
		public override string Title
		{
			get
			{
				return Strings.DRPrereq;
			}
		}
	}
}
