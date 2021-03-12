using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMFolder
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	public enum bitType
	{
		// Token: 0x0400035E RID: 862
		[XmlEnum(Name = "0")]
		zero,
		// Token: 0x0400035F RID: 863
		[XmlEnum(Name = "1")]
		one
	}
}
