using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Description;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200002F RID: 47
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class InstanceGroupConfig
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00003339 File Offset: 0x00001539
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00003341 File Offset: 0x00001541
		public IServerNameResolver NameResolver { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000334A File Offset: 0x0000154A
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00003352 File Offset: 0x00001552
		[DataMember]
		public bool IsZeroboxMode { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000335B File Offset: 0x0000155B
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00003363 File Offset: 0x00001563
		[DataMember]
		public string Self { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000336C File Offset: 0x0000156C
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00003374 File Offset: 0x00001574
		[DataMember]
		public string ComponentName { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000337D File Offset: 0x0000157D
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00003385 File Offset: 0x00001585
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000338E File Offset: 0x0000158E
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00003396 File Offset: 0x00001596
		[DataMember]
		public bool IsExistInConfigProvider { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000339F File Offset: 0x0000159F
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000033A7 File Offset: 0x000015A7
		[DataMember]
		public bool IsAutomaticActionsAllowed { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000033B0 File Offset: 0x000015B0
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000033B8 File Offset: 0x000015B8
		[DataMember]
		public bool IsRestartRequested { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000033C1 File Offset: 0x000015C1
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000033C9 File Offset: 0x000015C9
		[DataMember]
		public bool IsConfigurationReady { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000033D2 File Offset: 0x000015D2
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000033DA File Offset: 0x000015DA
		[DataMember]
		public bool IsConfigurationManagedExternally { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000033E3 File Offset: 0x000015E3
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000033EB File Offset: 0x000015EB
		public DateTimeOffset ConfigInProgressExpiryTime { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000033F4 File Offset: 0x000015F4
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000033FC File Offset: 0x000015FC
		[DataMember]
		public InstanceGroupMemberConfig[] Members { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00003405 File Offset: 0x00001605
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000340D File Offset: 0x0000160D
		[DataMember]
		public bool IsDefaultGroup { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00003416 File Offset: 0x00001616
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000341E File Offset: 0x0000161E
		[DataMember]
		public InstanceGroupSettings Settings { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00003427 File Offset: 0x00001627
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000342F File Offset: 0x0000162F
		[DataMember]
		public string Identity { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00003438 File Offset: 0x00001638
		public bool IsMembersContainSelf
		{
			get
			{
				return this.IsMember(this.Self, false);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00003447 File Offset: 0x00001647
		public bool IsOneNodeGroup
		{
			get
			{
				return this.Members.Length == 1;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00003494 File Offset: 0x00001694
		public bool IsMember(string memberName, bool checkExternallyManaged = false)
		{
			if (!checkExternallyManaged)
			{
				return this.Members.Any((InstanceGroupMemberConfig m) => Utils.IsEqual(m.Name, memberName, StringComparison.OrdinalIgnoreCase));
			}
			return this.Members.Any((InstanceGroupMemberConfig m) => Utils.IsEqual(m.Name, memberName, StringComparison.OrdinalIgnoreCase) && !m.IsManagedExternally);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000034EE File Offset: 0x000016EE
		public virtual ServiceEndpoint GetStoreAccessEndpoint(string target, bool isUseDefaultGroup, bool isServerBinding, WcfTimeout timeout = null)
		{
			return EndpointBuilder.GetStoreAccessEndpoint(this, target, isUseDefaultGroup, isServerBinding, timeout);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000034FB File Offset: 0x000016FB
		public virtual ServiceEndpoint GetStoreInstanceEndpoint(string target, bool isUseDefaultGroup, bool isServerBinding, WcfTimeout timeout = null)
		{
			return EndpointBuilder.GetStoreInstanceEndpoint(this, target, isUseDefaultGroup, isServerBinding, timeout);
		}

		// Token: 0x02000030 RID: 48
		public static class PropertyNames
		{
			// Token: 0x040000B0 RID: 176
			public const string IsAutomaticActionsAllowed = "IsAutomaticActionsAllowed";

			// Token: 0x040000B1 RID: 177
			public const string IsRestartRequested = "IsRestartRequested";

			// Token: 0x040000B2 RID: 178
			public const string IsConfigurationReady = "IsConfigurationReady";

			// Token: 0x040000B3 RID: 179
			public const string IsConfigurationManagedExternally = "IsConfigurationManagedExternally";

			// Token: 0x040000B4 RID: 180
			public const string ConfigInProgressExpiryTime = "ConfigInProgressExpiryTime";
		}

		// Token: 0x02000031 RID: 49
		public static class ContainerNames
		{
			// Token: 0x040000B5 RID: 181
			public const string Members = "Members";

			// Token: 0x040000B6 RID: 182
			public const string Settings = "Settings";
		}
	}
}
