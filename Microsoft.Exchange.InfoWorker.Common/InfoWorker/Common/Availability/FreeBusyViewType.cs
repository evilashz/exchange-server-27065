using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000FE RID: 254
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Flags]
	public enum FreeBusyViewType
	{
		// Token: 0x0400040B RID: 1035
		None = 0,
		// Token: 0x0400040C RID: 1036
		MergedOnly = 1,
		// Token: 0x0400040D RID: 1037
		FreeBusy = 32,
		// Token: 0x0400040E RID: 1038
		FreeBusyMerged = 33,
		// Token: 0x0400040F RID: 1039
		Detailed = 96,
		// Token: 0x04000410 RID: 1040
		DetailedMerged = 97
	}
}
