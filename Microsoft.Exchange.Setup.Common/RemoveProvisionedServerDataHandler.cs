using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RemoveProvisionedServerDataHandler : ConfigurationDataHandler
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0000C74C File Offset: 0x0000A94C
		public RemoveProvisionedServerDataHandler(ISetupContext context, MonadConnection connection) : base(context, "", "Remove-ProvisionedServer", connection)
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000C760 File Offset: 0x0000A960
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			string removeProvisionedServerName = base.SetupContext.RemoveProvisionedServerName;
			if (!string.IsNullOrEmpty(removeProvisionedServerName))
			{
				base.Parameters.AddWithValue("ServerName", removeProvisionedServerName);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		public override void UpdatePreCheckTaskDataHandler()
		{
			SetupLogger.TraceEnter(new object[0]);
			PrerequisiteAnalysisTaskDataHandler instance = PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
			instance.AddRole("Global");
			instance.RemoveProvisionedServerName = base.SetupContext.RemoveProvisionedServerName;
			SetupLogger.TraceExit();
		}
	}
}
