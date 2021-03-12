using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000993 RID: 2451
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11")]
	[Flags]
	[Serializable]
	public enum ServiceInstanceMoveOptions
	{
		// Token: 0x04004994 RID: 18836
		OutOfContextAndFaultInWithAutoComplete = 1,
		// Token: 0x04004995 RID: 18837
		OutOfContextWithPauseBeforeFaultin = 2,
		// Token: 0x04004996 RID: 18838
		FaultinWithPauseBeforeOutOfContext = 4
	}
}
