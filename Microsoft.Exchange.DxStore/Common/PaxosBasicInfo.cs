using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using FUSE.Paxos;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000040 RID: 64
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class PaxosBasicInfo
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00003F24 File Offset: 0x00002124
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00003F2C File Offset: 0x0000212C
		[DataMember]
		public string Self { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00003F35 File Offset: 0x00002135
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00003F3D File Offset: 0x0000213D
		[DataMember]
		public string[] Members { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00003F46 File Offset: 0x00002146
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00003F4E File Offset: 0x0000214E
		[DataMember]
		public Round<string> LeaderHint { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00003F57 File Offset: 0x00002157
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00003F5F File Offset: 0x0000215F
		[DataMember]
		public int CountExecuted { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00003F68 File Offset: 0x00002168
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00003F70 File Offset: 0x00002170
		[DataMember]
		public int CountTruncated { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00003F79 File Offset: 0x00002179
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00003F81 File Offset: 0x00002181
		[DataMember]
		public PaxosBasicInfo.GossipDictionary Gossip { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00003F8A File Offset: 0x0000218A
		public bool IsLeader
		{
			get
			{
				return Utils.IsEqual(this.LeaderHint.replica, this.Self, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00003FA3 File Offset: 0x000021A3
		public PaxosBasicInfo Clone()
		{
			return (PaxosBasicInfo)base.MemberwiseClone();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00003FB0 File Offset: 0x000021B0
		public bool IsMember(string nodeName)
		{
			return this.Members != null && this.Members.Contains(nodeName, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x02000041 RID: 65
		[CollectionDataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class GossipDictionary : Dictionary<string, int>
		{
			// Token: 0x06000227 RID: 551 RVA: 0x00003FD5 File Offset: 0x000021D5
			public GossipDictionary()
			{
			}

			// Token: 0x06000228 RID: 552 RVA: 0x00003FDD File Offset: 0x000021DD
			protected GossipDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}
		}
	}
}
