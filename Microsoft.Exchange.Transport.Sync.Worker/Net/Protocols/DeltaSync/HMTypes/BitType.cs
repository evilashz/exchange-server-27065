using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public enum BitType
	{
		// Token: 0x04000391 RID: 913
		[XmlEnum(Name = "0")]
		zero,
		// Token: 0x04000392 RID: 914
		[XmlEnum(Name = "1")]
		one
	}
}
