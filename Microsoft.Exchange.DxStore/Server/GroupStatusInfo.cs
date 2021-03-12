using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using FUSE.Paxos;
using Microsoft.Exchange.DxStore.Common;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x0200002C RID: 44
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class GroupStatusInfo
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00002B9B File Offset: 0x00000D9B
		public GroupStatusInfo()
		{
			this.StatusMap = new Dictionary<string, InstanceStatusInfo>();
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002BAE File Offset: 0x00000DAE
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00002BB6 File Offset: 0x00000DB6
		[DataMember]
		public Dictionary<string, InstanceStatusInfo> StatusMap { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00002BBF File Offset: 0x00000DBF
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00002BC7 File Offset: 0x00000DC7
		[DataMember]
		public int TotalRequested { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002BD0 File Offset: 0x00000DD0
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00002BD8 File Offset: 0x00000DD8
		[DataMember]
		public int TotalSuccessful { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00002BE1 File Offset: 0x00000DE1
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00002BE9 File Offset: 0x00000DE9
		[DataMember]
		public int TotalFailed { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00002BF2 File Offset: 0x00000DF2
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00002BFA File Offset: 0x00000DFA
		[DataMember]
		public bool IsMajoritySuccessfulyReplied { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00002C03 File Offset: 0x00000E03
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00002C0B File Offset: 0x00000E0B
		[DataMember]
		public bool IsMajorityAgreeWithLeader { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00002C14 File Offset: 0x00000E14
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00002C1C File Offset: 0x00000E1C
		[DataMember]
		public bool IsMajorityHavePaxosInitialized { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002C25 File Offset: 0x00000E25
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00002C2D File Offset: 0x00000E2D
		[DataMember]
		public GroupStatusInfo.NodeInstancePair HighestInstance { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00002C36 File Offset: 0x00000E36
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00002C3E File Offset: 0x00000E3E
		[DataMember]
		public GroupStatusInfo.NodeInstancePair LocalInstance { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00002C47 File Offset: 0x00000E47
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00002C4F File Offset: 0x00000E4F
		[DataMember]
		public Round<string> LeaderHint { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00002C58 File Offset: 0x00000E58
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00002C60 File Offset: 0x00000E60
		[DataMember]
		public DateTimeOffset CollectionStartTime { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00002C69 File Offset: 0x00000E69
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00002C71 File Offset: 0x00000E71
		[DataMember]
		public TimeSpan CollectionDuration { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00002C7A File Offset: 0x00000E7A
		public bool IsLeaderExist
		{
			get
			{
				return !string.IsNullOrEmpty(this.LeaderHint.replica);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002C8F File Offset: 0x00000E8F
		public string LeaderName
		{
			get
			{
				if (!this.IsLeaderExist)
				{
					return string.Empty;
				}
				return this.LeaderHint.replica;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00002CAA File Offset: 0x00000EAA
		public bool IsAllRequestsSucceeded
		{
			get
			{
				return this.TotalRequested == this.TotalSuccessful;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00002CBA File Offset: 0x00000EBA
		public int TotalNoReplies
		{
			get
			{
				return this.TotalRequested - (this.TotalSuccessful + this.TotalFailed);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public bool IsSingleNodeGroup
		{
			get
			{
				return this.TotalRequested == 1;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00002CDC File Offset: 0x00000EDC
		public int Lag
		{
			get
			{
				int result = 0;
				if (this.HighestInstance != null)
				{
					if (this.LocalInstance != null)
					{
						result = this.HighestInstance.InstanceNumber - this.LocalInstance.InstanceNumber;
					}
					else
					{
						result = this.HighestInstance.InstanceNumber;
					}
				}
				return result;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00002D24 File Offset: 0x00000F24
		public InstanceStatusInfo GetMemberInfo(string memberName)
		{
			InstanceStatusInfo result = null;
			if (this.StatusMap != null)
			{
				this.StatusMap.TryGetValue(memberName, out result);
			}
			return result;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00002D4C File Offset: 0x00000F4C
		public string GetDebugString(string identity)
		{
			return string.Format("{0}: MajoritySuccess: {1}, MajorityAgreeWithLeader: {2}, MajorityHavePaxos: {3}, Leader: {4}, TotalRequested: {5}, TotalSuccess: {6}, TotalFailed = {7}, Lag: {8}, HighestNode = {9}, HighestInstance = {10}", new object[]
			{
				identity,
				this.IsMajoritySuccessfulyReplied,
				this.IsMajorityAgreeWithLeader,
				this.IsMajorityHavePaxosInitialized,
				this.LeaderName,
				this.TotalRequested,
				this.TotalSuccessful,
				this.TotalFailed,
				this.Lag,
				(this.HighestInstance != null) ? this.HighestInstance.NodeName : "<unknown>",
				(this.HighestInstance != null) ? this.HighestInstance.InstanceNumber : -1
			});
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002E9C File Offset: 0x0000109C
		public void Analyze(string self, InstanceGroupConfig groupConfig)
		{
			if (this.isAnalyzed)
			{
				return;
			}
			int num = this.TotalRequested / 2 + 1;
			this.IsMajoritySuccessfulyReplied = (this.TotalSuccessful >= num);
			IEnumerable<InstanceStatusInfo> source = from v in this.StatusMap.Values
			where v != null
			select v;
			IEnumerable<GroupStatusInfo.NodeInstancePair> enumerable = from s in source
			orderby s.LastInstanceExecuted descending
			select new GroupStatusInfo.NodeInstancePair
			{
				NodeName = s.Self,
				InstanceNumber = s.LastInstanceExecuted
			};
			foreach (GroupStatusInfo.NodeInstancePair nodeInstancePair in enumerable)
			{
				if (this.HighestInstance == null)
				{
					this.HighestInstance = nodeInstancePair;
				}
				if (Utils.IsEqual(nodeInstancePair.NodeName, self, StringComparison.OrdinalIgnoreCase))
				{
					this.LocalInstance = nodeInstancePair;
					break;
				}
			}
			PaxosBasicInfo[] array = (from s in source
			where s.PaxosInfo != null
			select s.PaxosInfo).ToArray<PaxosBasicInfo>();
			this.IsMajorityHavePaxosInitialized = (array.Length >= num);
			IEnumerable<Round<string>> source2 = from p in array
			where !string.IsNullOrEmpty(p.LeaderHint.replica)
			select p.LeaderHint;
			IOrderedEnumerable<IGrouping<string, Round<string>>> source3 = from s in source2
			group s by s.replica into g
			orderby g.Count<Round<string>>() descending
			select g;
			this.IsMajorityAgreeWithLeader = false;
			IGrouping<string, Round<string>> grouping = source3.FirstOrDefault<IGrouping<string, Round<string>>>();
			if (grouping != null)
			{
				this.LeaderHint = grouping.FirstOrDefault<Round<string>>();
				int num2 = grouping.Count<Round<string>>();
				if (num2 >= num)
				{
					this.IsMajorityAgreeWithLeader = true;
				}
			}
			this.isAnalyzed = true;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000030F0 File Offset: 0x000012F0
		public string[] GetPaxosMembers(string nodeName)
		{
			InstanceStatusInfo instanceStatusInfo = this.StatusMap.Values.FirstOrDefault((InstanceStatusInfo v) => v != null && Utils.IsEqual(v.Self, nodeName, StringComparison.OrdinalIgnoreCase));
			if (instanceStatusInfo != null && instanceStatusInfo.PaxosInfo != null)
			{
				return instanceStatusInfo.PaxosInfo.Members;
			}
			return null;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000313F File Offset: 0x0000133F
		public string[] GetLeaderPaxosMembers()
		{
			if (this.IsLeaderExist)
			{
				return this.GetPaxosMembers(this.LeaderName);
			}
			return null;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003180 File Offset: 0x00001380
		public PaxosBasicInfo GetBestPossiblePaxosConfig()
		{
			InstanceStatusInfo instanceStatusInfo = (from s in this.StatusMap.Values
			where s != null && s.PaxosInfo != null
			select s).OrderByDescending(delegate(InstanceStatusInfo s)
			{
				if (!s.IsLeader)
				{
					return 0;
				}
				return 1;
			}).ThenByDescending((InstanceStatusInfo s) => s.LastInstanceExecuted).FirstOrDefault<InstanceStatusInfo>();
			if (instanceStatusInfo != null)
			{
				return instanceStatusInfo.PaxosInfo;
			}
			return null;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000324C File Offset: 0x0000144C
		public bool IsAllNodesWithValidPaxosKnowAbout(string nodeName)
		{
			InstanceStatusInfo[] array = (from s in this.StatusMap.Values
			where s != null && s.PaxosInfo != null && s.PaxosInfo.Members.Length > 0
			select s).ToArray<InstanceStatusInfo>();
			int num = array.Count((InstanceStatusInfo s) => s.PaxosInfo.IsMember(nodeName));
			return array.Length == num;
		}

		// Token: 0x04000080 RID: 128
		private bool isAnalyzed;

		// Token: 0x0200002D RID: 45
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class NodeInstancePair
		{
			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000128 RID: 296 RVA: 0x000032B2 File Offset: 0x000014B2
			// (set) Token: 0x06000129 RID: 297 RVA: 0x000032BA File Offset: 0x000014BA
			[DataMember]
			public string NodeName { get; set; }

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x0600012A RID: 298 RVA: 0x000032C3 File Offset: 0x000014C3
			// (set) Token: 0x0600012B RID: 299 RVA: 0x000032CB File Offset: 0x000014CB
			[DataMember]
			public int InstanceNumber { get; set; }
		}
	}
}
