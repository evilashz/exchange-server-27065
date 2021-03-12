using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000069 RID: 105
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpgradeCfgDataHandler : ConfigurationDataHandler
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x000110EB File Offset: 0x0000F2EB
		public UpgradeCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo.RoleName, "Install-" + roleInfo.RoleName, connection)
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001110C File Offset: 0x0000F30C
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			if (base.SetupContext.IsDatacenter && base.SetupContext.IsFfo)
			{
				base.Parameters.AddWithValue("IsFfo", true);
			}
			base.Parameters.AddWithValue("LanguagePacksPath", base.GetMsiSourcePath());
			base.Parameters.AddWithValue("UpdatesDir", this.UpdatesDir);
			SetupLogger.TraceExit();
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001118E File Offset: 0x0000F38E
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0001119B File Offset: 0x0000F39B
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
