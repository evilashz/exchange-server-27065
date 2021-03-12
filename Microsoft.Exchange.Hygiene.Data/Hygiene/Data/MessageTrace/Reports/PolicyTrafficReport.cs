using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports
{
	// Token: 0x02000198 RID: 408
	internal class PolicyTrafficReport : Schema
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x00034BCC File Offset: 0x00032DCC
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x00034BDE File Offset: 0x00032DDE
		public string Organization
		{
			get
			{
				return (string)this[PolicyTrafficReport.OrganizationDefinition];
			}
			set
			{
				this[PolicyTrafficReport.OrganizationDefinition] = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x00034BEC File Offset: 0x00032DEC
		// (set) Token: 0x0600110D RID: 4365 RVA: 0x00034BFE File Offset: 0x00032DFE
		public string Domain
		{
			get
			{
				return (string)this[PolicyTrafficReport.DomainDefinition];
			}
			set
			{
				this[PolicyTrafficReport.DomainDefinition] = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00034C0C File Offset: 0x00032E0C
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x00034C1E File Offset: 0x00032E1E
		public int DateKey
		{
			get
			{
				return (int)this[PolicyTrafficReport.DateKeyDefinition];
			}
			set
			{
				this[PolicyTrafficReport.DateKeyDefinition] = value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00034C31 File Offset: 0x00032E31
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x00034C43 File Offset: 0x00032E43
		public int HourKey
		{
			get
			{
				return (int)this[PolicyTrafficReport.HourKeyDefinition];
			}
			set
			{
				this[PolicyTrafficReport.HourKeyDefinition] = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00034C56 File Offset: 0x00032E56
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x00034C68 File Offset: 0x00032E68
		public string Direction
		{
			get
			{
				return (string)this[PolicyTrafficReport.DirectionDefinition];
			}
			set
			{
				this[PolicyTrafficReport.DirectionDefinition] = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00034C76 File Offset: 0x00032E76
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x00034C88 File Offset: 0x00032E88
		public string EventType
		{
			get
			{
				return (string)this[PolicyTrafficReport.EventTypeDefinition];
			}
			set
			{
				this[PolicyTrafficReport.EventTypeDefinition] = value;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00034C96 File Offset: 0x00032E96
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00034CA8 File Offset: 0x00032EA8
		public string Action
		{
			get
			{
				return (string)this[PolicyTrafficReport.ActionDefinition];
			}
			set
			{
				this[PolicyTrafficReport.ActionDefinition] = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00034CB6 File Offset: 0x00032EB6
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x00034CC8 File Offset: 0x00032EC8
		public int MessageCount
		{
			get
			{
				return (int)this[PolicyTrafficReport.MessageCountDefinition];
			}
			set
			{
				this[PolicyTrafficReport.MessageCountDefinition] = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00034CDB File Offset: 0x00032EDB
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x00034CED File Offset: 0x00032EED
		public string PolicyName
		{
			get
			{
				return (string)this[PolicyTrafficReport.PolicyNameDefinition];
			}
			set
			{
				this[PolicyTrafficReport.PolicyNameDefinition] = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00034CFB File Offset: 0x00032EFB
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x00034D0D File Offset: 0x00032F0D
		public string RuleName
		{
			get
			{
				return (string)this[PolicyTrafficReport.RuleNameDefinition];
			}
			set
			{
				this[PolicyTrafficReport.RuleNameDefinition] = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00034D1B File Offset: 0x00032F1B
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x00034D2D File Offset: 0x00032F2D
		public string DataSource
		{
			get
			{
				return this[PolicyTrafficReport.DataSourceDefinition] as string;
			}
			set
			{
				this[PolicyTrafficReport.DataSourceDefinition] = value;
			}
		}

		// Token: 0x04000822 RID: 2082
		internal static readonly HygienePropertyDefinition OrganizationDefinition = new HygienePropertyDefinition("OrganizationalUnitRootId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000823 RID: 2083
		internal static readonly HygienePropertyDefinition DomainDefinition = new HygienePropertyDefinition("DomainName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000824 RID: 2084
		internal static readonly HygienePropertyDefinition DateKeyDefinition = new HygienePropertyDefinition("DateKey", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000825 RID: 2085
		internal static readonly HygienePropertyDefinition HourKeyDefinition = new HygienePropertyDefinition("HourKey", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000826 RID: 2086
		internal static readonly HygienePropertyDefinition ActionDefinition = new HygienePropertyDefinition("Action", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000827 RID: 2087
		internal static readonly HygienePropertyDefinition EventTypeDefinition = new HygienePropertyDefinition("EventType", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000828 RID: 2088
		internal static readonly HygienePropertyDefinition DirectionDefinition = new HygienePropertyDefinition("Direction", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000829 RID: 2089
		internal static readonly HygienePropertyDefinition MessageCountDefinition = new HygienePropertyDefinition("MessageCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400082A RID: 2090
		internal static readonly HygienePropertyDefinition PolicyNameDefinition = new HygienePropertyDefinition("PolicyName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400082B RID: 2091
		internal static readonly HygienePropertyDefinition RuleNameDefinition = new HygienePropertyDefinition("RuleName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400082C RID: 2092
		internal static readonly HygienePropertyDefinition DataSourceDefinition = new HygienePropertyDefinition("DataSource", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
