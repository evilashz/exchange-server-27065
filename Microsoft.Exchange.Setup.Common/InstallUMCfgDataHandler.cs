using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200003A RID: 58
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallUMCfgDataHandler : InstallRoleBaseDataHandler
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000A053 File Offset: 0x00008253
		public InstallUMCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "UnifiedMessagingRole", "Install-UnifiedMessagingRole", connection)
		{
			this.unifiedMessagingRoleConfigurationInfo = (UnifiedMessagingRoleConfigurationInfo)base.InstallableUnitConfigurationInfo;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A078 File Offset: 0x00008278
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000A085 File Offset: 0x00008285
		public List<CultureInfo> SelectedCultures
		{
			get
			{
				return this.unifiedMessagingRoleConfigurationInfo.SelectedCultures;
			}
			set
			{
				this.unifiedMessagingRoleConfigurationInfo.SelectedCultures = value;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A094 File Offset: 0x00008294
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			if (base.SetupContext.SourceDir != null)
			{
				base.Parameters.AddWithValue("SourcePath", base.SetupContext.SourceDir.PathName + Path.DirectorySeparatorChar);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x040000AA RID: 170
		private UnifiedMessagingRoleConfigurationInfo unifiedMessagingRoleConfigurationInfo;
	}
}
