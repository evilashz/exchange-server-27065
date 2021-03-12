using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DisasterRecoveryCfgDataHandler : ConfigurationDataHandler
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00006097 File Offset: 0x00004297
		public DisasterRecoveryCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo.RoleName, "DisasterRecovery-" + roleInfo.RoleName, connection)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000060B8 File Offset: 0x000042B8
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			if (base.SetupContext.IsDatacenter && base.SetupContext.IsFfo)
			{
				base.Parameters.AddWithValue("IsFfo", true);
			}
			base.Parameters.AddWithValue("LanguagePacksPath", base.GetMsiSourcePath());
			base.Parameters.AddWithValue("updatesdir", this.UpdatesDir);
			SetupLogger.TraceExit();
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000613A File Offset: 0x0000433A
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00006147 File Offset: 0x00004347
		private LongPath UpdatesDir
		{
			get
			{
				return base.SetupContext.UpdatesDir;
			}
			set
			{
				base.SetupContext.UpdatesDir = value;
			}
		}
	}
}
