using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000062 RID: 98
	internal abstract class MigrationObjectLogConfiguration : ObjectLogConfiguration
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001CBDD File Offset: 0x0001ADDD
		public override bool IsEnabled
		{
			get
			{
				return ConfigBase<MigrationServiceConfigSchema>.GetConfig<bool>("MigrationReportingLoggingEnabled");
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001CBE9 File Offset: 0x0001ADE9
		public override TimeSpan MaxLogAge
		{
			get
			{
				return ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("MigrationReportingMaxLogAge");
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001CBF5 File Offset: 0x0001ADF5
		public override string LoggingFolder
		{
			get
			{
				return ConfigBase<MigrationServiceConfigSchema>.GetConfig<string>("MigrationReportingLoggingFolder");
			}
		}
	}
}
