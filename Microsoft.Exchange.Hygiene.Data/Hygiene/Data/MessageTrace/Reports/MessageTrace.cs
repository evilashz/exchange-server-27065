using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports
{
	// Token: 0x02000195 RID: 405
	internal class MessageTrace : Schema
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x00034240 File Offset: 0x00032440
		// (set) Token: 0x060010BD RID: 4285 RVA: 0x00034252 File Offset: 0x00032452
		public string Organization
		{
			get
			{
				return (string)this[MessageTrace.OrganizationDefinition];
			}
			set
			{
				this[MessageTrace.OrganizationDefinition] = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x00034260 File Offset: 0x00032460
		// (set) Token: 0x060010BF RID: 4287 RVA: 0x00034272 File Offset: 0x00032472
		public Guid InternalMessageId
		{
			get
			{
				return (Guid)this[MessageTrace.InternalMessageIdDefinition];
			}
			set
			{
				this[MessageTrace.InternalMessageIdDefinition] = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00034285 File Offset: 0x00032485
		// (set) Token: 0x060010C1 RID: 4289 RVA: 0x00034297 File Offset: 0x00032497
		public string ClientMessageId
		{
			get
			{
				return (string)this[MessageTrace.ClientMessageIdDefinition];
			}
			set
			{
				this[MessageTrace.ClientMessageIdDefinition] = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x000342A5 File Offset: 0x000324A5
		// (set) Token: 0x060010C3 RID: 4291 RVA: 0x000342B7 File Offset: 0x000324B7
		public DateTime Received
		{
			get
			{
				return (DateTime)this[MessageTrace.ReceivedDefinition];
			}
			set
			{
				this[MessageTrace.ReceivedDefinition] = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x000342CA File Offset: 0x000324CA
		// (set) Token: 0x060010C5 RID: 4293 RVA: 0x000342DC File Offset: 0x000324DC
		public string SenderAddress
		{
			get
			{
				return (string)this[MessageTrace.SenderAddressDefinition];
			}
			set
			{
				this[MessageTrace.SenderAddressDefinition] = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x000342EA File Offset: 0x000324EA
		// (set) Token: 0x060010C7 RID: 4295 RVA: 0x000342FC File Offset: 0x000324FC
		public string RecipientAddress
		{
			get
			{
				return (string)this[MessageTrace.RecipientAddressDefinition];
			}
			set
			{
				this[MessageTrace.RecipientAddressDefinition] = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x0003430A File Offset: 0x0003250A
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x0003431C File Offset: 0x0003251C
		public string MailDeliveryStatus
		{
			get
			{
				return (string)this[MessageTrace.MailDeliveryStatusDefinition];
			}
			set
			{
				this[MessageTrace.MailDeliveryStatusDefinition] = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x0003432A File Offset: 0x0003252A
		// (set) Token: 0x060010CB RID: 4299 RVA: 0x0003433C File Offset: 0x0003253C
		public string MessageSubject
		{
			get
			{
				return (string)this[MessageTrace.MessageSubjectDefinition];
			}
			set
			{
				this[MessageTrace.MessageSubjectDefinition] = value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x0003434A File Offset: 0x0003254A
		// (set) Token: 0x060010CD RID: 4301 RVA: 0x0003435C File Offset: 0x0003255C
		public int MessageSize
		{
			get
			{
				return (int)this[MessageTrace.MessageSizeDefinition];
			}
			set
			{
				this[MessageTrace.MessageSizeDefinition] = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0003436F File Offset: 0x0003256F
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x00034381 File Offset: 0x00032581
		public string FromIP
		{
			get
			{
				return (string)this[MessageTrace.FromIPAddressDefinition];
			}
			set
			{
				this[MessageTrace.FromIPAddressDefinition] = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0003438F File Offset: 0x0003258F
		// (set) Token: 0x060010D1 RID: 4305 RVA: 0x000343A1 File Offset: 0x000325A1
		public string ToIP
		{
			get
			{
				return (string)this[MessageTrace.ToIPAddressDefinition];
			}
			set
			{
				this[MessageTrace.ToIPAddressDefinition] = value;
			}
		}

		// Token: 0x040007FE RID: 2046
		internal static readonly HygienePropertyDefinition OrganizationDefinition = new HygienePropertyDefinition("OrganizationalUnitRootId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040007FF RID: 2047
		internal static readonly HygienePropertyDefinition InternalMessageIdDefinition = new HygienePropertyDefinition("InternalMessageId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000800 RID: 2048
		internal static readonly HygienePropertyDefinition ClientMessageIdDefinition = new HygienePropertyDefinition("ClientMessageId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000801 RID: 2049
		internal static readonly HygienePropertyDefinition ReceivedDefinition = new HygienePropertyDefinition("Received", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000802 RID: 2050
		internal static readonly HygienePropertyDefinition SenderAddressDefinition = new HygienePropertyDefinition("SenderAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000803 RID: 2051
		internal static readonly HygienePropertyDefinition RecipientAddressDefinition = new HygienePropertyDefinition("RecipientAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000804 RID: 2052
		internal static readonly HygienePropertyDefinition MailDeliveryStatusDefinition = new HygienePropertyDefinition("MailDeliveryStatus", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000805 RID: 2053
		internal static readonly HygienePropertyDefinition MessageSubjectDefinition = new HygienePropertyDefinition("MessageSubject", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000806 RID: 2054
		internal static readonly HygienePropertyDefinition MessageSizeDefinition = new HygienePropertyDefinition("MessageSize", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000807 RID: 2055
		internal static readonly HygienePropertyDefinition FromIPAddressDefinition = new HygienePropertyDefinition("FromIPAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000808 RID: 2056
		internal static readonly HygienePropertyDefinition ToIPAddressDefinition = new HygienePropertyDefinition("ToIPAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
