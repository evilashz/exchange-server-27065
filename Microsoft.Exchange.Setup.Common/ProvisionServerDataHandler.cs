using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200004F RID: 79
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ProvisionServerDataHandler : ConfigurationDataHandler
	{
		// Token: 0x06000386 RID: 902 RVA: 0x0000C47B File Offset: 0x0000A67B
		public ProvisionServerDataHandler(ISetupContext context, MonadConnection connection) : base(context, "", "New-ProvisionedServer", connection)
		{
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000C490 File Offset: 0x0000A690
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			string newProvisionedServerName = base.SetupContext.NewProvisionedServerName;
			if (!string.IsNullOrEmpty(newProvisionedServerName))
			{
				base.Parameters.AddWithValue("ServerName", newProvisionedServerName);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		public override void UpdatePreCheckTaskDataHandler()
		{
			SetupLogger.TraceEnter(new object[0]);
			PrerequisiteAnalysisTaskDataHandler instance = PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
			instance.AddRole("Global");
			instance.NewProvisionedServerName = base.SetupContext.NewProvisionedServerName;
			SetupLogger.TraceExit();
		}
	}
}
