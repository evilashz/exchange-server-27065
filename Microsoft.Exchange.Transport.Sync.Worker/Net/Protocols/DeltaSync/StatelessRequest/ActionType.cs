using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000168 RID: 360
	[XmlType(Namespace = "DeltaSyncV2:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public enum ActionType
	{
		// Token: 0x040005DA RID: 1498
		Equals,
		// Token: 0x040005DB RID: 1499
		GreaterThanOrEqualTo,
		// Token: 0x040005DC RID: 1500
		LessThanOrEqualTo
	}
}
