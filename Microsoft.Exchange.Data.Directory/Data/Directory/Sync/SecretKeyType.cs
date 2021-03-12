using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000921 RID: 2337
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public enum SecretKeyType
	{
		// Token: 0x0400484E RID: 18510
		Password,
		// Token: 0x0400484F RID: 18511
		Symmetric,
		// Token: 0x04004850 RID: 18512
		Salt
	}
}
