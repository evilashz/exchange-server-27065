using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports
{
	// Token: 0x0200019B RID: 411
	internal class TrafficReport : Schema
	{
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0003541C File Offset: 0x0003361C
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x0003542E File Offset: 0x0003362E
		public string Organization
		{
			get
			{
				return (string)this[TrafficReport.OrganizationDefinition];
			}
			set
			{
				this[TrafficReport.OrganizationDefinition] = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0003543C File Offset: 0x0003363C
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x0003544E File Offset: 0x0003364E
		public string Domain
		{
			get
			{
				return (string)this[TrafficReport.DomainDefinition];
			}
			set
			{
				this[TrafficReport.DomainDefinition] = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0003545C File Offset: 0x0003365C
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x0003546E File Offset: 0x0003366E
		public int DateKey
		{
			get
			{
				return (int)this[TrafficReport.DateKeyDefinition];
			}
			set
			{
				this[TrafficReport.DateKeyDefinition] = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x00035481 File Offset: 0x00033681
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x00035493 File Offset: 0x00033693
		public int HourKey
		{
			get
			{
				return (int)this[TrafficReport.HourKeyDefinition];
			}
			set
			{
				this[TrafficReport.HourKeyDefinition] = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x000354A6 File Offset: 0x000336A6
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x000354B8 File Offset: 0x000336B8
		public string Direction
		{
			get
			{
				return (string)this[TrafficReport.DirectionDefinition];
			}
			set
			{
				this[TrafficReport.DirectionDefinition] = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x000354C6 File Offset: 0x000336C6
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x000354D8 File Offset: 0x000336D8
		public string EventType
		{
			get
			{
				return (string)this[TrafficReport.EventTypeDefinition];
			}
			set
			{
				this[TrafficReport.EventTypeDefinition] = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x000354E6 File Offset: 0x000336E6
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x000354F8 File Offset: 0x000336F8
		public string Action
		{
			get
			{
				return (string)this[TrafficReport.ActionDefinition];
			}
			set
			{
				this[TrafficReport.ActionDefinition] = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00035506 File Offset: 0x00033706
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x00035518 File Offset: 0x00033718
		public int MessageCount
		{
			get
			{
				return (int)this[TrafficReport.MessageCountDefinition];
			}
			set
			{
				this[TrafficReport.MessageCountDefinition] = value;
			}
		}

		// Token: 0x04000841 RID: 2113
		internal static readonly HygienePropertyDefinition OrganizationDefinition = new HygienePropertyDefinition("OrganizationalUnitRootId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000842 RID: 2114
		internal static readonly HygienePropertyDefinition DomainDefinition = new HygienePropertyDefinition("DomainName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000843 RID: 2115
		internal static readonly HygienePropertyDefinition DateKeyDefinition = new HygienePropertyDefinition("DateKey", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000844 RID: 2116
		internal static readonly HygienePropertyDefinition HourKeyDefinition = new HygienePropertyDefinition("HourKey", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000845 RID: 2117
		internal static readonly HygienePropertyDefinition ActionDefinition = new HygienePropertyDefinition("Action", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000846 RID: 2118
		internal static readonly HygienePropertyDefinition EventTypeDefinition = new HygienePropertyDefinition("EventType", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000847 RID: 2119
		internal static readonly HygienePropertyDefinition DirectionDefinition = new HygienePropertyDefinition("Direction", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000848 RID: 2120
		internal static readonly HygienePropertyDefinition MessageCountDefinition = new HygienePropertyDefinition("MessageCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
