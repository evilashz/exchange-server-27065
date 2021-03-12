using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008AA RID: 2218
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11")]
	[Flags]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum ServiceInstanceMoveTaskStatusCode
	{
		// Token: 0x040047AD RID: 18349
		Active = 1,
		// Token: 0x040047AE RID: 18350
		Failed = 2,
		// Token: 0x040047AF RID: 18351
		ReadyForFinalization = 4,
		// Token: 0x040047B0 RID: 18352
		Completed = 8
	}
}
