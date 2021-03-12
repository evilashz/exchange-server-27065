using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A05 RID: 2565
	[DataContract]
	public class BuddyGroup
	{
		// Token: 0x06004871 RID: 18545 RVA: 0x0010181C File Offset: 0x000FFA1C
		internal BuddyGroup(StoreId id, string displayName)
		{
			this.Id = id.ToString();
			this.DisplayName = displayName;
			this.PersonaIds = new ItemId[0];
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06004872 RID: 18546 RVA: 0x00101843 File Offset: 0x000FFA43
		// (set) Token: 0x06004873 RID: 18547 RVA: 0x0010184B File Offset: 0x000FFA4B
		[DataMember]
		public string Id { get; set; }

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06004874 RID: 18548 RVA: 0x00101854 File Offset: 0x000FFA54
		// (set) Token: 0x06004875 RID: 18549 RVA: 0x0010185C File Offset: 0x000FFA5C
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06004876 RID: 18550 RVA: 0x00101865 File Offset: 0x000FFA65
		// (set) Token: 0x06004877 RID: 18551 RVA: 0x0010186D File Offset: 0x000FFA6D
		[DataMember]
		public ItemId[] PersonaIds { get; internal set; }
	}
}
