using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000067 RID: 103
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UninstallPreCheckDataHandler : PreCheckDataHandler
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x000110B4 File Offset: 0x0000F2B4
		public UninstallPreCheckDataHandler(ISetupContext setupContext, DataHandler topLevelHandler, MonadConnection connection) : base(setupContext, topLevelHandler, connection)
		{
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x000110BF File Offset: 0x0000F2BF
		public override string ShortDescription
		{
			get
			{
				return Strings.RemovePreCheckText;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x000110CB File Offset: 0x0000F2CB
		public override string Title
		{
			get
			{
				return Strings.RemovePrereq;
			}
		}
	}
}
