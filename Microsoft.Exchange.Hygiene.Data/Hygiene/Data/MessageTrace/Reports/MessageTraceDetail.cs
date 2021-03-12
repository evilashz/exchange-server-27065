using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports
{
	// Token: 0x02000196 RID: 406
	internal class MessageTraceDetail : Schema
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00034530 File Offset: 0x00032730
		// (set) Token: 0x060010D5 RID: 4309 RVA: 0x00034542 File Offset: 0x00032742
		public string Organization
		{
			get
			{
				return (string)this[MessageTraceDetail.OrganizationDefinition];
			}
			set
			{
				this[MessageTraceDetail.OrganizationDefinition] = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x00034550 File Offset: 0x00032750
		// (set) Token: 0x060010D7 RID: 4311 RVA: 0x00034562 File Offset: 0x00032762
		public Guid InternalMessageId
		{
			get
			{
				return (Guid)this[MessageTraceDetail.InternalMessageIdDefinition];
			}
			set
			{
				this[MessageTraceDetail.InternalMessageIdDefinition] = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x00034575 File Offset: 0x00032775
		// (set) Token: 0x060010D9 RID: 4313 RVA: 0x00034587 File Offset: 0x00032787
		public string ClientMessageId
		{
			get
			{
				return (string)this[MessageTraceDetail.ClientMessageIdDefinition];
			}
			set
			{
				this[MessageTraceDetail.ClientMessageIdDefinition] = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x00034595 File Offset: 0x00032795
		// (set) Token: 0x060010DB RID: 4315 RVA: 0x000345A7 File Offset: 0x000327A7
		public DateTime EventDate
		{
			get
			{
				return (DateTime)this[MessageTraceDetail.EventDateDefinition];
			}
			set
			{
				this[MessageTraceDetail.EventDateDefinition] = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x000345BA File Offset: 0x000327BA
		// (set) Token: 0x060010DD RID: 4317 RVA: 0x000345CC File Offset: 0x000327CC
		public string EventDescription
		{
			get
			{
				return (string)this[MessageTraceDetail.EventDescriptionDefinition];
			}
			set
			{
				this[MessageTraceDetail.EventDescriptionDefinition] = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x000345DA File Offset: 0x000327DA
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x000345EC File Offset: 0x000327EC
		public string AgentName
		{
			get
			{
				return (string)this[MessageTraceDetail.AgentNameDefinition];
			}
			set
			{
				this[MessageTraceDetail.AgentNameDefinition] = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x000345FA File Offset: 0x000327FA
		// (set) Token: 0x060010E1 RID: 4321 RVA: 0x0003460C File Offset: 0x0003280C
		public string Action
		{
			get
			{
				return (string)this[MessageTraceDetail.ActionDefinition];
			}
			set
			{
				this[MessageTraceDetail.ActionDefinition] = value;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0003461A File Offset: 0x0003281A
		// (set) Token: 0x060010E3 RID: 4323 RVA: 0x0003462C File Offset: 0x0003282C
		public string RuleId
		{
			get
			{
				return (string)this[MessageTraceDetail.RuleIdDefinition];
			}
			set
			{
				this[MessageTraceDetail.RuleIdDefinition] = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x0003463A File Offset: 0x0003283A
		// (set) Token: 0x060010E5 RID: 4325 RVA: 0x0003464C File Offset: 0x0003284C
		public string RuleName
		{
			get
			{
				return (string)this[MessageTraceDetail.TransportRuleNameDefinition];
			}
			set
			{
				this[MessageTraceDetail.TransportRuleNameDefinition] = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0003465A File Offset: 0x0003285A
		// (set) Token: 0x060010E7 RID: 4327 RVA: 0x0003466C File Offset: 0x0003286C
		public string PolicyId
		{
			get
			{
				return (string)this[MessageTraceDetail.PolicyIdDefinition];
			}
			set
			{
				this[MessageTraceDetail.PolicyIdDefinition] = value;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0003467A File Offset: 0x0003287A
		// (set) Token: 0x060010E9 RID: 4329 RVA: 0x0003468C File Offset: 0x0003288C
		public string PolicyName
		{
			get
			{
				return (string)this[MessageTraceDetail.PolicyNameDefinition];
			}
			set
			{
				this[MessageTraceDetail.PolicyNameDefinition] = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x0003469A File Offset: 0x0003289A
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x000346AC File Offset: 0x000328AC
		public string Data
		{
			get
			{
				return (string)this[MessageTraceDetail.DataDefinition];
			}
			set
			{
				this[MessageTraceDetail.DataDefinition] = value;
			}
		}

		// Token: 0x04000809 RID: 2057
		internal static readonly HygienePropertyDefinition OrganizationDefinition = new HygienePropertyDefinition("OrganizationalUnitRootId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400080A RID: 2058
		internal static readonly HygienePropertyDefinition InternalMessageIdDefinition = new HygienePropertyDefinition("InternalMessageId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400080B RID: 2059
		internal static readonly HygienePropertyDefinition ClientMessageIdDefinition = new HygienePropertyDefinition("ClientMessageId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400080C RID: 2060
		internal static readonly HygienePropertyDefinition EventDateDefinition = new HygienePropertyDefinition("EventDate", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400080D RID: 2061
		internal static readonly HygienePropertyDefinition EventDescriptionDefinition = new HygienePropertyDefinition("EventDescription", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400080E RID: 2062
		internal static readonly HygienePropertyDefinition AgentNameDefinition = new HygienePropertyDefinition("AgentName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400080F RID: 2063
		internal static readonly HygienePropertyDefinition ActionDefinition = new HygienePropertyDefinition("Action", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000810 RID: 2064
		internal static readonly HygienePropertyDefinition RuleIdDefinition = new HygienePropertyDefinition("RuleId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000811 RID: 2065
		internal static readonly HygienePropertyDefinition TransportRuleNameDefinition = new HygienePropertyDefinition("TransportRuleName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000812 RID: 2066
		internal static readonly HygienePropertyDefinition PolicyIdDefinition = new HygienePropertyDefinition("PolicyId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000813 RID: 2067
		internal static readonly HygienePropertyDefinition PolicyNameDefinition = new HygienePropertyDefinition("PolicyName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000814 RID: 2068
		internal static readonly HygienePropertyDefinition DataDefinition = new HygienePropertyDefinition("PropertyBag", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
