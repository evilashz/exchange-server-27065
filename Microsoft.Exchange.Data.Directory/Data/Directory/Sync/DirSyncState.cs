using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008BF RID: 2239
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum DirSyncState
	{
		// Token: 0x040047E6 RID: 18406
		Disabled,
		// Token: 0x040047E7 RID: 18407
		Enabled,
		// Token: 0x040047E8 RID: 18408
		PendingEnabled,
		// Token: 0x040047E9 RID: 18409
		PendingDisabledDraining,
		// Token: 0x040047EA RID: 18410
		PendingDisabledTransferring
	}
}
