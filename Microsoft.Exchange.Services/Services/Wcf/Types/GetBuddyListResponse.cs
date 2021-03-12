using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A06 RID: 2566
	[DataContract]
	public class GetBuddyListResponse
	{
		// Token: 0x06004878 RID: 18552 RVA: 0x00101876 File Offset: 0x000FFA76
		internal GetBuddyListResponse()
		{
			this.Buddies = new Persona[0];
			this.Groups = new BuddyGroup[0];
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06004879 RID: 18553 RVA: 0x00101896 File Offset: 0x000FFA96
		// (set) Token: 0x0600487A RID: 18554 RVA: 0x0010189E File Offset: 0x000FFA9E
		[DataMember]
		public Persona[] Buddies { get; internal set; }

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x0600487B RID: 18555 RVA: 0x001018A7 File Offset: 0x000FFAA7
		// (set) Token: 0x0600487C RID: 18556 RVA: 0x001018AF File Offset: 0x000FFAAF
		[DataMember]
		public BuddyGroup[] Groups { get; internal set; }
	}
}
