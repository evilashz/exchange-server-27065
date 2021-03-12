using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000902 RID: 2306
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum TakeoverStatus
	{
		// Token: 0x0400480E RID: 18446
		None,
		// Token: 0x0400480F RID: 18447
		Scheduled,
		// Token: 0x04004810 RID: 18448
		InProgress,
		// Token: 0x04004811 RID: 18449
		Complete,
		// Token: 0x04004812 RID: 18450
		Failed
	}
}
