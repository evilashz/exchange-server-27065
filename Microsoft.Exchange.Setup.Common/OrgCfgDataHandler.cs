using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class OrgCfgDataHandler : ConfigurationDataHandler
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0000987F File Offset: 0x00007A7F
		public OrgCfgDataHandler(ISetupContext context, string commandText, MonadConnection connection) : base(context, "", commandText, connection)
		{
		}

		// Token: 0x06000245 RID: 581
		public abstract bool WillDataHandlerDoAnyWork();
	}
}
