using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200069B RID: 1691
	[DataContract]
	public class CollectionDelta
	{
		// Token: 0x170027C1 RID: 10177
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x000DDF5E File Offset: 0x000DC15E
		// (set) Token: 0x06004893 RID: 18579 RVA: 0x000DDF66 File Offset: 0x000DC166
		[DataMember]
		public Identity[] Added { get; set; }

		// Token: 0x170027C2 RID: 10178
		// (get) Token: 0x06004894 RID: 18580 RVA: 0x000DDF6F File Offset: 0x000DC16F
		// (set) Token: 0x06004895 RID: 18581 RVA: 0x000DDF77 File Offset: 0x000DC177
		[DataMember]
		public Identity[] Removed { get; set; }
	}
}
