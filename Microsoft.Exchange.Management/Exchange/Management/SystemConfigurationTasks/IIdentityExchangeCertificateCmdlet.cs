using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AB7 RID: 2743
	internal interface IIdentityExchangeCertificateCmdlet
	{
		// Token: 0x17001D5D RID: 7517
		// (get) Token: 0x060060FE RID: 24830
		// (set) Token: 0x060060FF RID: 24831
		ExchangeCertificateIdParameter Identity { get; set; }

		// Token: 0x17001D5E RID: 7518
		// (get) Token: 0x06006100 RID: 24832
		// (set) Token: 0x06006101 RID: 24833
		ServerIdParameter Server { get; set; }

		// Token: 0x17001D5F RID: 7519
		// (get) Token: 0x06006102 RID: 24834
		// (set) Token: 0x06006103 RID: 24835
		string Thumbprint { get; set; }
	}
}
