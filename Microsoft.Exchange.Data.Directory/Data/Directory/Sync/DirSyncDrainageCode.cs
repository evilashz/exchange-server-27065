using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000975 RID: 2421
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum DirSyncDrainageCode
	{
		// Token: 0x04004964 RID: 18788
		Completed,
		// Token: 0x04004965 RID: 18789
		ContextNotFound,
		// Token: 0x04004966 RID: 18790
		ContextOutOfScope,
		// Token: 0x04004967 RID: 18791
		PartitionUnavailable,
		// Token: 0x04004968 RID: 18792
		ContextDeleted,
		// Token: 0x04004969 RID: 18793
		DirSyncStatusMismatch,
		// Token: 0x0400496A RID: 18794
		InProgress,
		// Token: 0x0400496B RID: 18795
		UnspecifiedError
	}
}
