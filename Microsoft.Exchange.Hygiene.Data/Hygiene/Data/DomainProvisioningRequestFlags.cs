using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000CE RID: 206
	[Flags]
	public enum DomainProvisioningRequestFlags
	{
		// Token: 0x0400042B RID: 1067
		Default = 0,
		// Token: 0x0400042C RID: 1068
		Reporting = 1,
		// Token: 0x0400042D RID: 1069
		GlobalLocator = 2,
		// Token: 0x0400042E RID: 1070
		DNS = 4,
		// Token: 0x0400042F RID: 1071
		RelayDomainUpdate = 8
	}
}
