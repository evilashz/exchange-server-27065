using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000933 RID: 2355
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum AddressType
	{
		// Token: 0x04004884 RID: 18564
		Reply,
		// Token: 0x04004885 RID: 18565
		Realm,
		// Token: 0x04004886 RID: 18566
		Error,
		// Token: 0x04004887 RID: 18567
		SamlMetadata,
		// Token: 0x04004888 RID: 18568
		SamlLogout
	}
}
