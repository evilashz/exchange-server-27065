using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000901 RID: 2305
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum TakeoverActionType
	{
		// Token: 0x0400480B RID: 18443
		Add,
		// Token: 0x0400480C RID: 18444
		Remove
	}
}
