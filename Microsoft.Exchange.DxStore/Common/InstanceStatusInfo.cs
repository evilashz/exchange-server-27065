using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200003E RID: 62
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class InstanceStatusInfo
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00003ADF File Offset: 0x00001CDF
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00003AE7 File Offset: 0x00001CE7
		[DataMember]
		public string Self { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00003AF0 File Offset: 0x00001CF0
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00003AF8 File Offset: 0x00001CF8
		[DataMember]
		public int StateRaw { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00003B01 File Offset: 0x00001D01
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00003B09 File Offset: 0x00001D09
		[DataMember]
		public int LastInstanceExecuted { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00003B12 File Offset: 0x00001D12
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00003B1A File Offset: 0x00001D1A
		[DataMember]
		public InstanceGroupMemberConfig[] MemberConfigs { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00003B23 File Offset: 0x00001D23
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00003B2B File Offset: 0x00001D2B
		[DataMember]
		public PaxosBasicInfo PaxosInfo { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00003B34 File Offset: 0x00001D34
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00003B3C File Offset: 0x00001D3C
		[DataMember]
		public ProcessBasicInfo HostProcessInfo { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00003B45 File Offset: 0x00001D45
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00003B4D File Offset: 0x00001D4D
		[DataMember]
		public DateTimeOffset CommitAckOldestItemTime { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00003B56 File Offset: 0x00001D56
		public bool IsLeader
		{
			get
			{
				return this.PaxosInfo != null && this.PaxosInfo.IsLeader;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00003B70 File Offset: 0x00001D70
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00003B9C File Offset: 0x00001D9C
		public InstanceState State
		{
			get
			{
				InstanceState result;
				try
				{
					result = (InstanceState)this.StateRaw;
				}
				catch
				{
					result = InstanceState.Unknown;
				}
				return result;
			}
			set
			{
				this.StateRaw = (int)value;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00003BA5 File Offset: 0x00001DA5
		public bool IsValidPaxosMembersExist()
		{
			return this.PaxosInfo != null && this.PaxosInfo.Members != null && this.PaxosInfo.Members.Length > 0;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00003BCE File Offset: 0x00001DCE
		public bool IsValidLeaderExist()
		{
			return this.IsValidPaxosMembersExist() && !string.IsNullOrWhiteSpace(this.PaxosInfo.LeaderHint.replica);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00003BFC File Offset: 0x00001DFC
		public bool AreMembersSame(InstanceGroupMemberConfig[] members)
		{
			Tuple<string[], string[], string[]> tuple = Utils.DiffArrays<string>(this.PaxosInfo.Members, (from m in members
			select m.Name).ToArray<string>());
			return tuple.Item2.Length == 0 && tuple.Item3.Length == 0;
		}
	}
}
