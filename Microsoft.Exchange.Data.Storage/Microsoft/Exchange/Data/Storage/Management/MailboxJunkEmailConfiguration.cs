using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A06 RID: 2566
	[Serializable]
	public sealed class MailboxJunkEmailConfiguration : XsoMailboxConfigurationObject
	{
		// Token: 0x170019DD RID: 6621
		// (get) Token: 0x06005E25 RID: 24101 RVA: 0x0018DBD0 File Offset: 0x0018BDD0
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return MailboxJunkEmailConfiguration.schema;
			}
		}

		// Token: 0x170019DE RID: 6622
		// (get) Token: 0x06005E27 RID: 24103 RVA: 0x0018DBDF File Offset: 0x0018BDDF
		// (set) Token: 0x06005E28 RID: 24104 RVA: 0x0018DBF1 File Offset: 0x0018BDF1
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)this[MailboxJunkEmailConfigurationSchema.Enabled];
			}
			set
			{
				this[MailboxJunkEmailConfigurationSchema.Enabled] = value;
			}
		}

		// Token: 0x170019DF RID: 6623
		// (get) Token: 0x06005E29 RID: 24105 RVA: 0x0018DC04 File Offset: 0x0018BE04
		// (set) Token: 0x06005E2A RID: 24106 RVA: 0x0018DC16 File Offset: 0x0018BE16
		[Parameter(Mandatory = false)]
		public bool TrustedListsOnly
		{
			get
			{
				return (bool)this[MailboxJunkEmailConfigurationSchema.TrustedListsOnly];
			}
			set
			{
				this[MailboxJunkEmailConfigurationSchema.TrustedListsOnly] = value;
			}
		}

		// Token: 0x170019E0 RID: 6624
		// (get) Token: 0x06005E2B RID: 24107 RVA: 0x0018DC29 File Offset: 0x0018BE29
		// (set) Token: 0x06005E2C RID: 24108 RVA: 0x0018DC3B File Offset: 0x0018BE3B
		[Parameter(Mandatory = false)]
		public bool ContactsTrusted
		{
			get
			{
				return (bool)this[MailboxJunkEmailConfigurationSchema.ContactsTrusted];
			}
			set
			{
				this[MailboxJunkEmailConfigurationSchema.ContactsTrusted] = value;
			}
		}

		// Token: 0x170019E1 RID: 6625
		// (get) Token: 0x06005E2D RID: 24109 RVA: 0x0018DC4E File Offset: 0x0018BE4E
		// (set) Token: 0x06005E2E RID: 24110 RVA: 0x0018DC60 File Offset: 0x0018BE60
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> TrustedSendersAndDomains
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxJunkEmailConfigurationSchema.TrustedSendersAndDomains];
			}
			set
			{
				this[MailboxJunkEmailConfigurationSchema.TrustedSendersAndDomains] = value;
			}
		}

		// Token: 0x170019E2 RID: 6626
		// (get) Token: 0x06005E2F RID: 24111 RVA: 0x0018DC6E File Offset: 0x0018BE6E
		// (set) Token: 0x06005E30 RID: 24112 RVA: 0x0018DC80 File Offset: 0x0018BE80
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> BlockedSendersAndDomains
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxJunkEmailConfigurationSchema.BlockedSendersAndDomains];
			}
			set
			{
				this[MailboxJunkEmailConfigurationSchema.BlockedSendersAndDomains] = value;
			}
		}

		// Token: 0x0400348B RID: 13451
		private static MailboxJunkEmailConfigurationSchema schema = ObjectSchema.GetInstance<MailboxJunkEmailConfigurationSchema>();
	}
}
