using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A04 RID: 2564
	[DataContract]
	public class Buddy
	{
		// Token: 0x0600486A RID: 18538 RVA: 0x001017D3 File Offset: 0x000FF9D3
		internal Buddy(string displayName, string imAddress)
		{
			this.DisplayName = displayName;
			this.IMAddress = imAddress;
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x001017E9 File Offset: 0x000FF9E9
		// (set) Token: 0x0600486C RID: 18540 RVA: 0x001017F1 File Offset: 0x000FF9F1
		[DataMember]
		public string DisplayName { get; internal set; }

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x0600486D RID: 18541 RVA: 0x001017FA File Offset: 0x000FF9FA
		// (set) Token: 0x0600486E RID: 18542 RVA: 0x00101802 File Offset: 0x000FFA02
		[DataMember]
		public string IMAddress { get; internal set; }

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x0600486F RID: 18543 RVA: 0x0010180B File Offset: 0x000FFA0B
		// (set) Token: 0x06004870 RID: 18544 RVA: 0x00101813 File Offset: 0x000FFA13
		[DataMember]
		public string GroupId { get; internal set; }
	}
}
