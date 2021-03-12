using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200001D RID: 29
	public static class IndexCapabilities
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000A450 File Offset: 0x00008650
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000A457 File Offset: 0x00008657
		public static bool SupportsCaseInsensitiveStringComparison
		{
			get
			{
				return IndexCapabilities.supportsCaseInsensitiveStringComparison;
			}
			set
			{
				IndexCapabilities.supportsCaseInsensitiveStringComparison = value;
			}
		}

		// Token: 0x040000F3 RID: 243
		private static bool supportsCaseInsensitiveStringComparison;
	}
}
