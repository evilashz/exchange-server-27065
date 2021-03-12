using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000950 RID: 2384
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum DirectoryObjectErrorCode
	{
		// Token: 0x040048D1 RID: 18641
		Busy,
		// Token: 0x040048D2 RID: 18642
		ContextOutOfScope,
		// Token: 0x040048D3 RID: 18643
		ObjectNotFound,
		// Token: 0x040048D4 RID: 18644
		ObjectOutOfScope,
		// Token: 0x040048D5 RID: 18645
		PartitionUnavailable,
		// Token: 0x040048D6 RID: 18646
		UnspecifiedError
	}
}
