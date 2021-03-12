using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Connections.Eas.Model.Common.AirSyncBase
{
	// Token: 0x02000087 RID: 135
	public enum MimeTruncationType
	{
		// Token: 0x0400042D RID: 1069
		[XmlEnum("0")]
		TruncateAll,
		// Token: 0x0400042E RID: 1070
		[XmlEnum("1")]
		Truncate4K,
		// Token: 0x0400042F RID: 1071
		[XmlEnum("2")]
		Truncate5K,
		// Token: 0x04000430 RID: 1072
		[XmlEnum("3")]
		Truncate7K,
		// Token: 0x04000431 RID: 1073
		[XmlEnum("4")]
		Truncate10K,
		// Token: 0x04000432 RID: 1074
		[XmlEnum("5")]
		Truncate20K,
		// Token: 0x04000433 RID: 1075
		[XmlEnum("6")]
		Truncate50K,
		// Token: 0x04000434 RID: 1076
		[XmlEnum("7")]
		Truncate100K,
		// Token: 0x04000435 RID: 1077
		[XmlEnum("8")]
		NoTruncate
	}
}
