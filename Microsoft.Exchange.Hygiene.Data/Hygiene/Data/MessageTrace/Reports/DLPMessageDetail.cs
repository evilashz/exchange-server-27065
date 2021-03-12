using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports
{
	// Token: 0x02000191 RID: 401
	internal class DLPMessageDetail : Schema
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x000331F3 File Offset: 0x000313F3
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x00033205 File Offset: 0x00031405
		public string Organization
		{
			get
			{
				return (string)this[DLPMessageDetail.OrganizationDefinition];
			}
			set
			{
				this[DLPMessageDetail.OrganizationDefinition] = value;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x00033213 File Offset: 0x00031413
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x00033225 File Offset: 0x00031425
		public string Domain
		{
			get
			{
				return (string)this[DLPMessageDetail.DomainDefinition];
			}
			set
			{
				this[DLPMessageDetail.DomainDefinition] = value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x00033233 File Offset: 0x00031433
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x00033245 File Offset: 0x00031445
		public DateTime Received
		{
			get
			{
				return (DateTime)this[DLPMessageDetail.ReceivedDefinition];
			}
			set
			{
				this[DLPMessageDetail.ReceivedDefinition] = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x00033258 File Offset: 0x00031458
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x0003326A File Offset: 0x0003146A
		public string ClientMessageId
		{
			get
			{
				return (string)this[DLPMessageDetail.ClientMessageIdDefinition];
			}
			set
			{
				this[DLPMessageDetail.ClientMessageIdDefinition] = value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x00033278 File Offset: 0x00031478
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x0003328A File Offset: 0x0003148A
		public string Direction
		{
			get
			{
				return (string)this[DLPMessageDetail.DirectionDefinition];
			}
			set
			{
				this[DLPMessageDetail.DirectionDefinition] = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x00033298 File Offset: 0x00031498
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x000332AA File Offset: 0x000314AA
		public string RecipientAddress
		{
			get
			{
				return (string)this[DLPMessageDetail.RecipientAddressDefinition];
			}
			set
			{
				this[DLPMessageDetail.RecipientAddressDefinition] = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x000332B8 File Offset: 0x000314B8
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x000332CA File Offset: 0x000314CA
		public string SenderAddress
		{
			get
			{
				return (string)this[DLPMessageDetail.SenderAddressDefinition];
			}
			set
			{
				this[DLPMessageDetail.SenderAddressDefinition] = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x000332D8 File Offset: 0x000314D8
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x000332EA File Offset: 0x000314EA
		public string MessageSubject
		{
			get
			{
				return (string)this[DLPMessageDetail.MessageSubjectDefinition];
			}
			set
			{
				this[DLPMessageDetail.MessageSubjectDefinition] = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x000332F8 File Offset: 0x000314F8
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x0003330A File Offset: 0x0003150A
		public int MessageSize
		{
			get
			{
				return (int)this[DLPMessageDetail.MessageSizeDefinition];
			}
			set
			{
				this[DLPMessageDetail.MessageSizeDefinition] = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0003331D File Offset: 0x0003151D
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x0003332F File Offset: 0x0003152F
		public string PolicyName
		{
			get
			{
				return (string)this[DLPMessageDetail.PolicyNameDefinition];
			}
			set
			{
				this[DLPMessageDetail.PolicyNameDefinition] = value;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0003333D File Offset: 0x0003153D
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x0003334F File Offset: 0x0003154F
		public string TransportRuleName
		{
			get
			{
				return (string)this[DLPMessageDetail.TransportRuleNameDefinition];
			}
			set
			{
				this[DLPMessageDetail.TransportRuleNameDefinition] = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0003335D File Offset: 0x0003155D
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x0003336F File Offset: 0x0003156F
		public string DataClassification
		{
			get
			{
				return (string)this[DLPMessageDetail.DataClassificationDefinition];
			}
			set
			{
				this[DLPMessageDetail.DataClassificationDefinition] = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0003337D File Offset: 0x0003157D
		// (set) Token: 0x06001051 RID: 4177 RVA: 0x0003338F File Offset: 0x0003158F
		public int ClassificationConfidence
		{
			get
			{
				return (int)this[DLPMessageDetail.ClassificationConfidenceDefinition];
			}
			set
			{
				this[DLPMessageDetail.ClassificationConfidenceDefinition] = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x000333A2 File Offset: 0x000315A2
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x000333B4 File Offset: 0x000315B4
		public int ClassificationCount
		{
			get
			{
				return (int)this[DLPMessageDetail.ClassificationCountDefinition];
			}
			set
			{
				this[DLPMessageDetail.ClassificationCountDefinition] = value;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x000333C7 File Offset: 0x000315C7
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x000333D9 File Offset: 0x000315D9
		public string ClassificationJustification
		{
			get
			{
				return (string)this[DLPMessageDetail.ClassificationJustificationDefinition];
			}
			set
			{
				this[DLPMessageDetail.ClassificationJustificationDefinition] = value;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x000333E7 File Offset: 0x000315E7
		// (set) Token: 0x06001057 RID: 4183 RVA: 0x000333F9 File Offset: 0x000315F9
		public string ClassificationSndoverride
		{
			get
			{
				return (string)this[DLPMessageDetail.ClassificationSndoverrideDefinition];
			}
			set
			{
				this[DLPMessageDetail.ClassificationSndoverrideDefinition] = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x00033407 File Offset: 0x00031607
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x00033419 File Offset: 0x00031619
		public string EventType
		{
			get
			{
				return (string)this[DLPMessageDetail.EventTypeDefinition];
			}
			set
			{
				this[DLPMessageDetail.EventTypeDefinition] = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x00033427 File Offset: 0x00031627
		// (set) Token: 0x0600105B RID: 4187 RVA: 0x00033439 File Offset: 0x00031639
		public string Action
		{
			get
			{
				return (string)this[DLPMessageDetail.ActionDefinition];
			}
			set
			{
				this[DLPMessageDetail.ActionDefinition] = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00033447 File Offset: 0x00031647
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x00033459 File Offset: 0x00031659
		public Guid InternalMessageId
		{
			get
			{
				return (Guid)this[DLPMessageDetail.InternalMessageIdDefinition];
			}
			set
			{
				this[DLPMessageDetail.InternalMessageIdDefinition] = value;
			}
		}

		// Token: 0x040007C0 RID: 1984
		internal static readonly HygienePropertyDefinition OrganizationDefinition = new HygienePropertyDefinition("OrganizationalUnitRootId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007C1 RID: 1985
		internal static readonly HygienePropertyDefinition DomainDefinition = new HygienePropertyDefinition("DomainName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007C2 RID: 1986
		internal static readonly HygienePropertyDefinition ReceivedDefinition = new HygienePropertyDefinition("Received", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007C3 RID: 1987
		internal static readonly HygienePropertyDefinition ClientMessageIdDefinition = new HygienePropertyDefinition("ClientMessageId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007C4 RID: 1988
		internal static readonly HygienePropertyDefinition DirectionDefinition = new HygienePropertyDefinition("Direction", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007C5 RID: 1989
		internal static readonly HygienePropertyDefinition RecipientAddressDefinition = new HygienePropertyDefinition("RecipientAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007C6 RID: 1990
		internal static readonly HygienePropertyDefinition SenderAddressDefinition = new HygienePropertyDefinition("SenderAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007C7 RID: 1991
		internal static readonly HygienePropertyDefinition MessageSubjectDefinition = new HygienePropertyDefinition("MessageSubject", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007C8 RID: 1992
		internal static readonly HygienePropertyDefinition MessageSizeDefinition = new HygienePropertyDefinition("MessageSize", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007C9 RID: 1993
		internal static readonly HygienePropertyDefinition PolicyNameDefinition = new HygienePropertyDefinition("PolicyName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007CA RID: 1994
		internal static readonly HygienePropertyDefinition TransportRuleNameDefinition = new HygienePropertyDefinition("TransportRuleName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007CB RID: 1995
		internal static readonly HygienePropertyDefinition DataClassificationDefinition = new HygienePropertyDefinition("DataClassification", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007CC RID: 1996
		internal static readonly HygienePropertyDefinition ClassificationConfidenceDefinition = new HygienePropertyDefinition("ClassificationConfidence", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007CD RID: 1997
		internal static readonly HygienePropertyDefinition ClassificationCountDefinition = new HygienePropertyDefinition("ClassificationCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007CE RID: 1998
		internal static readonly HygienePropertyDefinition ClassificationJustificationDefinition = new HygienePropertyDefinition("ClassificationJustification", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007CF RID: 1999
		internal static readonly HygienePropertyDefinition ClassificationSndoverrideDefinition = new HygienePropertyDefinition("ClassificationSndoverride", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007D0 RID: 2000
		internal static readonly HygienePropertyDefinition EventTypeDefinition = new HygienePropertyDefinition("EventType", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007D1 RID: 2001
		internal static readonly HygienePropertyDefinition ActionDefinition = new HygienePropertyDefinition("Action", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007D2 RID: 2002
		internal static readonly HygienePropertyDefinition InternalMessageIdDefinition = new HygienePropertyDefinition("InternalMessageId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
