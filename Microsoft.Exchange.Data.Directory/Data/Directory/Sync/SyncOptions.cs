using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008A3 RID: 2211
	[Flags]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[Serializable]
	public enum SyncOptions
	{
		// Token: 0x04004793 RID: 18323
		None = 1,
		// Token: 0x04004794 RID: 18324
		DelayUntilContextIsProvisioned = 2,
		// Token: 0x04004795 RID: 18325
		SkipExchangeSpecificFiltering = 4,
		// Token: 0x04004796 RID: 18326
		SkipBackfillOnRevisionUpdate = 8,
		// Token: 0x04004797 RID: 18327
		SkipBackfill = 16
	}
}
