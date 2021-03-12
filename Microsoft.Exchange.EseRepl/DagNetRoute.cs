using System;
using System.Net;
using Microsoft.Exchange.Cluster.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000008 RID: 8
	public class DagNetRoute : IEquatable<DagNetRoute>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002B93 File Offset: 0x00000D93
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002B9B File Offset: 0x00000D9B
		public IPAddress SourceIPAddr { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002BA4 File Offset: 0x00000DA4
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002BAC File Offset: 0x00000DAC
		public IPAddress TargetIPAddr { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002BB5 File Offset: 0x00000DB5
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002BBD File Offset: 0x00000DBD
		public int TargetPort { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002BC6 File Offset: 0x00000DC6
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002BCE File Offset: 0x00000DCE
		public string NetworkName { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002BD7 File Offset: 0x00000DD7
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002BDF File Offset: 0x00000DDF
		public bool IsCrossSubnet { get; set; }

		// Token: 0x0600003D RID: 61 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public bool Equals(DagNetRoute other)
		{
			return this.SourceIPAddr.Equals(other.SourceIPAddr) && this.TargetIPAddr.Equals(other.TargetIPAddr) && this.TargetPort == other.TargetPort && this.IsCrossSubnet == other.IsCrossSubnet && StringUtil.IsEqualIgnoreCase(this.NetworkName, other.NetworkName);
		}
	}
}
