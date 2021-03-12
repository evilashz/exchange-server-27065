using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000970 RID: 2416
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[Flags]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum GetDirectoryObjectsOptions
	{
		// Token: 0x04004957 RID: 18775
		None = 1,
		// Token: 0x04004958 RID: 18776
		IncludeForwardLinks = 2,
		// Token: 0x04004959 RID: 18777
		IncludeBackLinks = 4
	}
}
