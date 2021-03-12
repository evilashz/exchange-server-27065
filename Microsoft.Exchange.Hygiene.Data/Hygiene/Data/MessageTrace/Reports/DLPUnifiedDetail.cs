using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports
{
	// Token: 0x02000192 RID: 402
	internal class DLPUnifiedDetail : Schema
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x000336EE File Offset: 0x000318EE
		// (set) Token: 0x06001061 RID: 4193 RVA: 0x00033700 File Offset: 0x00031900
		public string OrganizationalUnitRootId
		{
			get
			{
				return this[DLPUnifiedDetail.OrganizationalUnitRootIdDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.OrganizationalUnitRootIdDefinition] = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0003370E File Offset: 0x0003190E
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x00033720 File Offset: 0x00031920
		public string DataSource
		{
			get
			{
				return this[DLPUnifiedDetail.DataSourceDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.DataSourceDefinition] = value;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0003372E File Offset: 0x0003192E
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x00033740 File Offset: 0x00031940
		public DateTime PolicyMatchTime
		{
			get
			{
				return (DateTime)this[DLPUnifiedDetail.PolicyMatchTimeDefinition];
			}
			set
			{
				this[DLPUnifiedDetail.PolicyMatchTimeDefinition] = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00033753 File Offset: 0x00031953
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x00033765 File Offset: 0x00031965
		public string Title
		{
			get
			{
				return this[DLPUnifiedDetail.TitleDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.TitleDefinition] = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x00033773 File Offset: 0x00031973
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x00033785 File Offset: 0x00031985
		public long Size
		{
			get
			{
				return (long)this[DLPUnifiedDetail.SizeDefinition];
			}
			set
			{
				this[DLPUnifiedDetail.SizeDefinition] = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x00033798 File Offset: 0x00031998
		// (set) Token: 0x0600106B RID: 4203 RVA: 0x000337AA File Offset: 0x000319AA
		public string Location
		{
			get
			{
				return this[DLPUnifiedDetail.LocationDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.LocationDefinition] = value;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x000337B8 File Offset: 0x000319B8
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x000337CA File Offset: 0x000319CA
		public string Actor
		{
			get
			{
				return this[DLPUnifiedDetail.ActorDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.ActorDefinition] = value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x000337D8 File Offset: 0x000319D8
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x000337EA File Offset: 0x000319EA
		public string PolicyName
		{
			get
			{
				return this[DLPUnifiedDetail.PolicyNameDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.PolicyNameDefinition] = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x000337F8 File Offset: 0x000319F8
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0003380A File Offset: 0x00031A0A
		public string TransportRuleName
		{
			get
			{
				return this[DLPUnifiedDetail.TransportRuleNameDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.TransportRuleNameDefinition] = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00033818 File Offset: 0x00031A18
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x0003382A File Offset: 0x00031A2A
		public string Severity
		{
			get
			{
				return this[DLPUnifiedDetail.SeverityDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.SeverityDefinition] = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00033838 File Offset: 0x00031A38
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x0003384A File Offset: 0x00031A4A
		public string OverrideType
		{
			get
			{
				return this[DLPUnifiedDetail.OverrideTypeDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.OverrideTypeDefinition] = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00033858 File Offset: 0x00031A58
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x0003386A File Offset: 0x00031A6A
		public string OverrideJustification
		{
			get
			{
				return this[DLPUnifiedDetail.OverrideJustificationDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.OverrideJustificationDefinition] = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00033878 File Offset: 0x00031A78
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x0003388A File Offset: 0x00031A8A
		public string DataClassification
		{
			get
			{
				return this[DLPUnifiedDetail.DataClassificationDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.DataClassificationDefinition] = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00033898 File Offset: 0x00031A98
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x000338AA File Offset: 0x00031AAA
		public int ClassificationConfidence
		{
			get
			{
				return (int)this[DLPUnifiedDetail.ClassificationConfidenceDefinition];
			}
			set
			{
				this[DLPUnifiedDetail.ClassificationConfidenceDefinition] = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x000338BD File Offset: 0x00031ABD
		// (set) Token: 0x0600107D RID: 4221 RVA: 0x000338CF File Offset: 0x00031ACF
		public int ClassificationCount
		{
			get
			{
				return (int)this[DLPUnifiedDetail.ClassificationCountDefinition];
			}
			set
			{
				this[DLPUnifiedDetail.ClassificationCountDefinition] = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x000338E2 File Offset: 0x00031AE2
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x000338F4 File Offset: 0x00031AF4
		public string EventType
		{
			get
			{
				return this[DLPUnifiedDetail.EventTypeDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.EventTypeDefinition] = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00033902 File Offset: 0x00031B02
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x00033914 File Offset: 0x00031B14
		public string Action
		{
			get
			{
				return this[DLPUnifiedDetail.ActionDefinition] as string;
			}
			set
			{
				this[DLPUnifiedDetail.ActionDefinition] = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x00033922 File Offset: 0x00031B22
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x00033934 File Offset: 0x00031B34
		public Guid ObjectId
		{
			get
			{
				return (Guid)this[DLPUnifiedDetail.ObjectIdDefinition];
			}
			set
			{
				this[DLPUnifiedDetail.ObjectIdDefinition] = value;
			}
		}

		// Token: 0x040007D3 RID: 2003
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootIdDefinition = new HygienePropertyDefinition("OrganizationalUnitRootId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007D4 RID: 2004
		internal static readonly HygienePropertyDefinition DataSourceDefinition = new HygienePropertyDefinition("DataSource", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007D5 RID: 2005
		internal static readonly HygienePropertyDefinition PolicyMatchTimeDefinition = new HygienePropertyDefinition("PolicyMatchTime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007D6 RID: 2006
		internal static readonly HygienePropertyDefinition TitleDefinition = new HygienePropertyDefinition("Title", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007D7 RID: 2007
		internal static readonly HygienePropertyDefinition SizeDefinition = new HygienePropertyDefinition("Size", typeof(long), 0L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007D8 RID: 2008
		internal static readonly HygienePropertyDefinition LocationDefinition = new HygienePropertyDefinition("Location", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007D9 RID: 2009
		internal static readonly HygienePropertyDefinition SeverityDefinition = new HygienePropertyDefinition("Severity", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007DA RID: 2010
		internal static readonly HygienePropertyDefinition ActorDefinition = new HygienePropertyDefinition("Actor", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007DB RID: 2011
		internal static readonly HygienePropertyDefinition PolicyNameDefinition = new HygienePropertyDefinition("PolicyName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007DC RID: 2012
		internal static readonly HygienePropertyDefinition TransportRuleNameDefinition = new HygienePropertyDefinition("TransportRuleName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007DD RID: 2013
		internal static readonly HygienePropertyDefinition DataClassificationDefinition = new HygienePropertyDefinition("DataClassification", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007DE RID: 2014
		internal static readonly HygienePropertyDefinition ClassificationConfidenceDefinition = new HygienePropertyDefinition("ClassificationConfidence", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007DF RID: 2015
		internal static readonly HygienePropertyDefinition ClassificationCountDefinition = new HygienePropertyDefinition("ClassificationCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007E0 RID: 2016
		internal static readonly HygienePropertyDefinition OverrideJustificationDefinition = new HygienePropertyDefinition("OverrideJustification", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007E1 RID: 2017
		internal static readonly HygienePropertyDefinition OverrideTypeDefinition = new HygienePropertyDefinition("OverrideType", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007E2 RID: 2018
		internal static readonly HygienePropertyDefinition EventTypeDefinition = new HygienePropertyDefinition("EventType", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007E3 RID: 2019
		internal static readonly HygienePropertyDefinition ActionDefinition = new HygienePropertyDefinition("Action", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007E4 RID: 2020
		internal static readonly HygienePropertyDefinition ObjectIdDefinition = new HygienePropertyDefinition("ObjectId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
