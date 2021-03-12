using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000243 RID: 579
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PhoneCallStateType
	{
		// Token: 0x04000EF3 RID: 3827
		Idle,
		// Token: 0x04000EF4 RID: 3828
		Connecting,
		// Token: 0x04000EF5 RID: 3829
		Alerted,
		// Token: 0x04000EF6 RID: 3830
		Connected,
		// Token: 0x04000EF7 RID: 3831
		Disconnected,
		// Token: 0x04000EF8 RID: 3832
		Incoming,
		// Token: 0x04000EF9 RID: 3833
		Transferring,
		// Token: 0x04000EFA RID: 3834
		Forwarding
	}
}
