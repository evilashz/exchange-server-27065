using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000020 RID: 32
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum OofState
	{
		// Token: 0x04000046 RID: 70
		Disabled,
		// Token: 0x04000047 RID: 71
		Enabled,
		// Token: 0x04000048 RID: 72
		Scheduled
	}
}
