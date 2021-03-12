using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports
{
	// Token: 0x0200019A RID: 410
	internal class TopTrafficReport : Schema
	{
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x000351EC File Offset: 0x000333EC
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x000351FE File Offset: 0x000333FE
		public string Organization
		{
			get
			{
				return (string)this[TopTrafficReport.OrganizationDefinition];
			}
			set
			{
				this[TopTrafficReport.OrganizationDefinition] = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0003520C File Offset: 0x0003340C
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x0003521E File Offset: 0x0003341E
		public string Domain
		{
			get
			{
				return (string)this[TopTrafficReport.DomainDefinition];
			}
			set
			{
				this[TopTrafficReport.DomainDefinition] = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0003522C File Offset: 0x0003342C
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x0003523E File Offset: 0x0003343E
		public int DateKey
		{
			get
			{
				return (int)this[TopTrafficReport.DateKeyDefinition];
			}
			set
			{
				this[TopTrafficReport.DateKeyDefinition] = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00035251 File Offset: 0x00033451
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x00035263 File Offset: 0x00033463
		public int HourKey
		{
			get
			{
				return (int)this[TopTrafficReport.HourKeyDefinition];
			}
			set
			{
				this[TopTrafficReport.HourKeyDefinition] = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00035276 File Offset: 0x00033476
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x00035288 File Offset: 0x00033488
		public string Direction
		{
			get
			{
				return (string)this[TopTrafficReport.DirectionDefinition];
			}
			set
			{
				this[TopTrafficReport.DirectionDefinition] = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00035296 File Offset: 0x00033496
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x000352A8 File Offset: 0x000334A8
		public string EventType
		{
			get
			{
				return (string)this[TopTrafficReport.EventTypeDefinition];
			}
			set
			{
				this[TopTrafficReport.EventTypeDefinition] = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x000352B6 File Offset: 0x000334B6
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x000352C8 File Offset: 0x000334C8
		public int MessageCount
		{
			get
			{
				return (int)this[TopTrafficReport.MessageCountDefinition];
			}
			set
			{
				this[TopTrafficReport.MessageCountDefinition] = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x000352DB File Offset: 0x000334DB
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x000352ED File Offset: 0x000334ED
		public string Name
		{
			get
			{
				return (string)this[TopTrafficReport.NameDefinition];
			}
			set
			{
				this[TopTrafficReport.NameDefinition] = value;
			}
		}

		// Token: 0x04000839 RID: 2105
		internal static readonly HygienePropertyDefinition OrganizationDefinition = new HygienePropertyDefinition("OrganizationalUnitRootId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400083A RID: 2106
		internal static readonly HygienePropertyDefinition DomainDefinition = new HygienePropertyDefinition("DomainName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400083B RID: 2107
		internal static readonly HygienePropertyDefinition DateKeyDefinition = new HygienePropertyDefinition("DateKey", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400083C RID: 2108
		internal static readonly HygienePropertyDefinition HourKeyDefinition = new HygienePropertyDefinition("HourKey", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400083D RID: 2109
		internal static readonly HygienePropertyDefinition NameDefinition = new HygienePropertyDefinition("Name", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400083E RID: 2110
		internal static readonly HygienePropertyDefinition EventTypeDefinition = new HygienePropertyDefinition("EventType", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400083F RID: 2111
		internal static readonly HygienePropertyDefinition DirectionDefinition = new HygienePropertyDefinition("Direction", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000840 RID: 2112
		internal static readonly HygienePropertyDefinition MessageCountDefinition = new HygienePropertyDefinition("MessageCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
