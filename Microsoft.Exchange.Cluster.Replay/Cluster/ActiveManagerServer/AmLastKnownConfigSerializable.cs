using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public class AmLastKnownConfigSerializable
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00011F3B File Offset: 0x0001013B
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00011F43 File Offset: 0x00010143
		public int Role { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00011F4C File Offset: 0x0001014C
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00011F54 File Offset: 0x00010154
		public string AuthoritativeServer { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00011F5D File Offset: 0x0001015D
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00011F65 File Offset: 0x00010165
		[XmlArray]
		public string[] Members { get; set; }
	}
}
