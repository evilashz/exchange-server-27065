using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000173 RID: 371
	internal interface IInstallable
	{
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000DCD RID: 3533
		bool IsUnpacked { get; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000DCE RID: 3534
		Version UnpackedVersion { get; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000DCF RID: 3535
		bool IsDatacenterUnpacked { get; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000DD0 RID: 3536
		Version UnpackedDatacenterVersion { get; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000DD1 RID: 3537
		bool IsInstalled { get; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000DD2 RID: 3538
		Version InstalledVersion { get; }

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000DD3 RID: 3539
		string InstalledPath { get; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000DD4 RID: 3540
		Task InstallTask { get; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000DD5 RID: 3541
		Task DisasterRecoveryTask { get; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000DD6 RID: 3542
		Task UninstallTask { get; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000DD7 RID: 3543
		ValidatingTask ValidateTask { get; }
	}
}
