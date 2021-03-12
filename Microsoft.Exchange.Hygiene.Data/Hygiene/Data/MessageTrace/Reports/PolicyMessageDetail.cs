using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports
{
	// Token: 0x02000197 RID: 407
	internal class PolicyMessageDetail : Schema
	{
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0003485B File Offset: 0x00032A5B
		// (set) Token: 0x060010EF RID: 4335 RVA: 0x0003486D File Offset: 0x00032A6D
		public string Organization
		{
			get
			{
				return (string)this[PolicyMessageDetail.OrganizationDefinition];
			}
			set
			{
				this[PolicyMessageDetail.OrganizationDefinition] = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0003487B File Offset: 0x00032A7B
		// (set) Token: 0x060010F1 RID: 4337 RVA: 0x0003488D File Offset: 0x00032A8D
		public string Domain
		{
			get
			{
				return (string)this[PolicyMessageDetail.DomainDefinition];
			}
			set
			{
				this[PolicyMessageDetail.DomainDefinition] = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0003489B File Offset: 0x00032A9B
		// (set) Token: 0x060010F3 RID: 4339 RVA: 0x000348AD File Offset: 0x00032AAD
		public DateTime Received
		{
			get
			{
				return (DateTime)this[PolicyMessageDetail.ReceivedDefinition];
			}
			set
			{
				this[PolicyMessageDetail.ReceivedDefinition] = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x000348C0 File Offset: 0x00032AC0
		// (set) Token: 0x060010F5 RID: 4341 RVA: 0x000348D2 File Offset: 0x00032AD2
		public string ClientMessageId
		{
			get
			{
				return (string)this[PolicyMessageDetail.ClientMessageIdDefinition];
			}
			set
			{
				this[PolicyMessageDetail.ClientMessageIdDefinition] = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x000348E0 File Offset: 0x00032AE0
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x000348F2 File Offset: 0x00032AF2
		public string Direction
		{
			get
			{
				return (string)this[PolicyMessageDetail.DirectionDefinition];
			}
			set
			{
				this[PolicyMessageDetail.DirectionDefinition] = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00034900 File Offset: 0x00032B00
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x00034912 File Offset: 0x00032B12
		public string RecipientAddress
		{
			get
			{
				return (string)this[PolicyMessageDetail.RecipientAddressDefinition];
			}
			set
			{
				this[PolicyMessageDetail.RecipientAddressDefinition] = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00034920 File Offset: 0x00032B20
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x00034932 File Offset: 0x00032B32
		public string SenderAddress
		{
			get
			{
				return (string)this[PolicyMessageDetail.SenderAddressDefinition];
			}
			set
			{
				this[PolicyMessageDetail.SenderAddressDefinition] = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00034940 File Offset: 0x00032B40
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x00034952 File Offset: 0x00032B52
		public string MessageSubject
		{
			get
			{
				return (string)this[PolicyMessageDetail.MessageSubjectDefinition];
			}
			set
			{
				this[PolicyMessageDetail.MessageSubjectDefinition] = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00034960 File Offset: 0x00032B60
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x00034972 File Offset: 0x00032B72
		public int MessageSize
		{
			get
			{
				return (int)this[PolicyMessageDetail.MessageSizeDefinition];
			}
			set
			{
				this[PolicyMessageDetail.MessageSizeDefinition] = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00034985 File Offset: 0x00032B85
		// (set) Token: 0x06001101 RID: 4353 RVA: 0x00034997 File Offset: 0x00032B97
		public string EventType
		{
			get
			{
				return (string)this[PolicyMessageDetail.EventTypeDefinition];
			}
			set
			{
				this[PolicyMessageDetail.EventTypeDefinition] = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x000349A5 File Offset: 0x00032BA5
		// (set) Token: 0x06001103 RID: 4355 RVA: 0x000349B7 File Offset: 0x00032BB7
		public string Action
		{
			get
			{
				return (string)this[PolicyMessageDetail.ActionDefinition];
			}
			set
			{
				this[PolicyMessageDetail.ActionDefinition] = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x000349C5 File Offset: 0x00032BC5
		// (set) Token: 0x06001105 RID: 4357 RVA: 0x000349D7 File Offset: 0x00032BD7
		public Guid InternalMessageId
		{
			get
			{
				return (Guid)this[PolicyMessageDetail.InternalMessageIdDefinition];
			}
			set
			{
				this[PolicyMessageDetail.InternalMessageIdDefinition] = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x000349EA File Offset: 0x00032BEA
		// (set) Token: 0x06001107 RID: 4359 RVA: 0x000349FC File Offset: 0x00032BFC
		public string TransportRuleName
		{
			get
			{
				return (string)this[PolicyMessageDetail.TransportRuleNameDefinition];
			}
			set
			{
				this[PolicyMessageDetail.TransportRuleNameDefinition] = value;
			}
		}

		// Token: 0x04000815 RID: 2069
		internal static readonly HygienePropertyDefinition OrganizationDefinition = new HygienePropertyDefinition("OrganizationalUnitRootId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000816 RID: 2070
		internal static readonly HygienePropertyDefinition DomainDefinition = new HygienePropertyDefinition("DomainName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000817 RID: 2071
		internal static readonly HygienePropertyDefinition ReceivedDefinition = new HygienePropertyDefinition("Received", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000818 RID: 2072
		internal static readonly HygienePropertyDefinition ClientMessageIdDefinition = new HygienePropertyDefinition("ClientMessageId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000819 RID: 2073
		internal static readonly HygienePropertyDefinition DirectionDefinition = new HygienePropertyDefinition("Direction", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400081A RID: 2074
		internal static readonly HygienePropertyDefinition RecipientAddressDefinition = new HygienePropertyDefinition("RecipientAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400081B RID: 2075
		internal static readonly HygienePropertyDefinition SenderAddressDefinition = new HygienePropertyDefinition("SenderAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400081C RID: 2076
		internal static readonly HygienePropertyDefinition MessageSubjectDefinition = new HygienePropertyDefinition("MessageSubject", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400081D RID: 2077
		internal static readonly HygienePropertyDefinition MessageSizeDefinition = new HygienePropertyDefinition("MessageSize", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400081E RID: 2078
		internal static readonly HygienePropertyDefinition TransportRuleNameDefinition = new HygienePropertyDefinition("TransportRuleName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400081F RID: 2079
		internal static readonly HygienePropertyDefinition EventTypeDefinition = new HygienePropertyDefinition("EventType", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000820 RID: 2080
		internal static readonly HygienePropertyDefinition ActionDefinition = new HygienePropertyDefinition("Action", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000821 RID: 2081
		internal static readonly HygienePropertyDefinition InternalMessageIdDefinition = new HygienePropertyDefinition("InternalMessageId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
