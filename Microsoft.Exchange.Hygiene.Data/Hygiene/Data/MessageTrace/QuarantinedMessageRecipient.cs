using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200018B RID: 395
	[Serializable]
	internal class QuarantinedMessageRecipient : ConfigurablePropertyBag
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x000328D9 File Offset: 0x00030AD9
		// (set) Token: 0x0600100A RID: 4106 RVA: 0x000328EB File Offset: 0x00030AEB
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[QuarantinedMessageRecipientSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[QuarantinedMessageRecipientSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x000328FE File Offset: 0x00030AFE
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x00032910 File Offset: 0x00030B10
		public Guid ExMessageId
		{
			get
			{
				return (Guid)this[QuarantinedMessageRecipientSchema.ExMessageIdProperty];
			}
			set
			{
				this[QuarantinedMessageRecipientSchema.ExMessageIdProperty] = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00032923 File Offset: 0x00030B23
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x00032935 File Offset: 0x00030B35
		public string EmailPrefix
		{
			get
			{
				return (string)this[QuarantinedMessageRecipientSchema.EmailPrefixProperty];
			}
			set
			{
				this[QuarantinedMessageRecipientSchema.EmailPrefixProperty] = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00032943 File Offset: 0x00030B43
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x00032955 File Offset: 0x00030B55
		public string EmailDomain
		{
			get
			{
				return (string)this[QuarantinedMessageRecipientSchema.EmailDomainProperty];
			}
			set
			{
				this[QuarantinedMessageRecipientSchema.EmailDomainProperty] = value;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x00032963 File Offset: 0x00030B63
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x00032975 File Offset: 0x00030B75
		public bool Quarantined
		{
			get
			{
				return (bool)this[QuarantinedMessageDetailSchema.QuarantinedProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.QuarantinedProperty] = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00032988 File Offset: 0x00030B88
		// (set) Token: 0x06001014 RID: 4116 RVA: 0x0003299A File Offset: 0x00030B9A
		public bool Notified
		{
			get
			{
				return (bool)this[QuarantinedMessageDetailSchema.NotifiedProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.NotifiedProperty] = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x000329AD File Offset: 0x00030BAD
		// (set) Token: 0x06001016 RID: 4118 RVA: 0x000329BF File Offset: 0x00030BBF
		public bool Reported
		{
			get
			{
				return (bool)this[QuarantinedMessageDetailSchema.ReportedProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.ReportedProperty] = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x000329D2 File Offset: 0x00030BD2
		// (set) Token: 0x06001018 RID: 4120 RVA: 0x000329E4 File Offset: 0x00030BE4
		public bool Released
		{
			get
			{
				return (bool)this[QuarantinedMessageDetailSchema.ReleasedProperty];
			}
			set
			{
				this[QuarantinedMessageDetailSchema.ReleasedProperty] = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x000329F7 File Offset: 0x00030BF7
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x00032A09 File Offset: 0x00030C09
		public int FssCopyId
		{
			get
			{
				return (int)this[QuarantinedMessageRecipientSchema.FssCopyIdProp];
			}
			set
			{
				this[QuarantinedMessageRecipientSchema.FssCopyIdProp] = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00032A1C File Offset: 0x00030C1C
		public override ObjectId Identity
		{
			get
			{
				return new MessageTraceObjectId(this.OrganizationalUnitRoot, this.ExMessageId);
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00032A2F File Offset: 0x00030C2F
		public override Type GetSchemaType()
		{
			return typeof(QuarantinedMessageRecipientSchema);
		}
	}
}
