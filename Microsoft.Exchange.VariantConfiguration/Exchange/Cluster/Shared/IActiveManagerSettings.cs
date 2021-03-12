using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000015 RID: 21
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IActiveManagerSettings : ISettings
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005A RID: 90
		DxStoreMode DxStoreRunMode { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005B RID: 91
		bool DxStoreIsUseHttpForInstanceCommunication { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92
		bool DxStoreIsUseHttpForClientCommunication { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005D RID: 93
		bool DxStoreIsEncryptionEnabled { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005E RID: 94
		bool DxStoreIsPeriodicFixupEnabled { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005F RID: 95
		bool DxStoreIsUseBinarySerializerForClientCommunication { get; }
	}
}
