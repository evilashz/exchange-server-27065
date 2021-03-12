using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000039 RID: 57
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class InstallPreCheckDataHandler : PreCheckDataHandler
	{
		// Token: 0x06000251 RID: 593 RVA: 0x0000A030 File Offset: 0x00008230
		public InstallPreCheckDataHandler(ISetupContext setupContext, DataHandler topLevelHandler, MonadConnection connection) : base(setupContext, topLevelHandler, connection)
		{
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A03B File Offset: 0x0000823B
		public override string ShortDescription
		{
			get
			{
				return Strings.AddPreCheckText;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A047 File Offset: 0x00008247
		public override string Title
		{
			get
			{
				return Strings.AddPrereq;
			}
		}
	}
}
