using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000967 RID: 2407
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[Serializable]
	public enum DirectoryObjectClassCapabilityTarget
	{
		// Token: 0x04004937 RID: 18743
		Company,
		// Token: 0x04004938 RID: 18744
		User,
		// Token: 0x04004939 RID: 18745
		Contact,
		// Token: 0x0400493A RID: 18746
		Group
	}
}
