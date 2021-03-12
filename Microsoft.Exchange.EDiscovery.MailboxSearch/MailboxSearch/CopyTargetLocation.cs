using System;
using Microsoft.Exchange.EDiscovery.Export;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000004 RID: 4
	internal class CopyTargetLocation : ITargetLocation
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000023C2 File Offset: 0x000005C2
		public CopyTargetLocation(string exportLocation, string workingLocation)
		{
			Util.ThrowIfNullOrEmpty(exportLocation, "exportLocation");
			Util.ThrowIfNullOrEmpty(workingLocation, "workingLocation");
			this.ExportLocation = exportLocation;
			this.WorkingLocation = workingLocation;
			this.UnsearchableExportLocation = exportLocation;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000023F5 File Offset: 0x000005F5
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000023FD File Offset: 0x000005FD
		public string ExportLocation { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002406 File Offset: 0x00000606
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000240E File Offset: 0x0000060E
		public string WorkingLocation { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002417 File Offset: 0x00000617
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000241F File Offset: 0x0000061F
		public string UnsearchableExportLocation { get; private set; }
	}
}
