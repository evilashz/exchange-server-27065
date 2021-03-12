using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009B0 RID: 2480
	[Cmdlet("Get", "ExchangeServerAccessLicense")]
	public sealed class GetExchangeServerAccessLicense : Task
	{
		// Token: 0x06005889 RID: 22665 RVA: 0x001718B0 File Offset: 0x0016FAB0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			foreach (ExchangeServerAccessLicense sendToPipeline in GetExchangeServerAccessLicense.SupportedLicenses)
			{
				base.WriteObject(sendToPipeline);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x040032D4 RID: 13012
		private static readonly ExchangeServerAccessLicense[] SupportedLicenses = new ExchangeServerAccessLicense[]
		{
			new ExchangeServerAccessLicense(ExchangeServerAccessLicense.ServerVersionMajor.E15, ExchangeServerAccessLicense.AccessLicenseType.Standard, ExchangeServerAccessLicense.UnitLabelType.Server),
			new ExchangeServerAccessLicense(ExchangeServerAccessLicense.ServerVersionMajor.E15, ExchangeServerAccessLicense.AccessLicenseType.Enterprise, ExchangeServerAccessLicense.UnitLabelType.Server),
			new ExchangeServerAccessLicense(ExchangeServerAccessLicense.ServerVersionMajor.E15, ExchangeServerAccessLicense.AccessLicenseType.Standard, ExchangeServerAccessLicense.UnitLabelType.CAL),
			new ExchangeServerAccessLicense(ExchangeServerAccessLicense.ServerVersionMajor.E15, ExchangeServerAccessLicense.AccessLicenseType.Enterprise, ExchangeServerAccessLicense.UnitLabelType.CAL)
		};
	}
}
