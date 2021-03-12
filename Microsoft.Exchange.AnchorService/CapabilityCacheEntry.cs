using System;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CapabilityCacheEntry : CacheEntryBase
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00005FD7 File Offset: 0x000041D7
		public CapabilityCacheEntry(AnchorContext context, ADUser user) : base(context, user)
		{
			this.MailboxUser = user;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00005FE8 File Offset: 0x000041E8
		public OrganizationCapability AnchorCapability
		{
			get
			{
				return base.Context.AnchorCapability;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005FF5 File Offset: 0x000041F5
		public virtual OrganizationCapability ActiveCapability
		{
			get
			{
				return base.Context.AnchorCapability;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006002 File Offset: 0x00004202
		// (set) Token: 0x0600016B RID: 363 RVA: 0x0000600A File Offset: 0x0000420A
		public ADUser MailboxUser { get; protected set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006014 File Offset: 0x00004214
		public override bool IsLocal
		{
			get
			{
				AnchorUtil.AssertOrThrow(this.MailboxUser != null, "expect to have a valid user", new object[0]);
				AnchorUtil.AssertOrThrow(base.ADProvider != null, "expect to have a valid recipient session", new object[0]);
				try
				{
					base.ADProvider.EnsureLocalMailbox(this.MailboxUser, false);
				}
				catch (AnchorMailboxNotFoundOnServerException)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006088 File Offset: 0x00004288
		public override bool IsActive
		{
			get
			{
				AnchorUtil.AssertOrThrow(this.MailboxUser != null, "expect to have a valid user", new object[0]);
				return this.MailboxUser.PersistedCapabilities.Contains((Capability)this.AnchorCapability);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000060BC File Offset: 0x000042BC
		public override int UniqueEntryCount
		{
			get
			{
				return base.ADProvider.GetOrganizationMailboxesByCapability(this.AnchorCapability).Count<ADUser>();
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000060D4 File Offset: 0x000042D4
		public override bool Sync()
		{
			ADUser aduser = base.ADProvider.GetADRecipientByObjectId(base.ObjectId) as ADUser;
			if (aduser == null && this.MailboxUser != null)
			{
				base.ADProvider = new AnchorADProvider(base.Context, base.OrganizationId, this.MailboxUser.OriginatingServer);
				aduser = (base.ADProvider.GetADRecipientByObjectId(base.ObjectId) as ADUser);
			}
			if (aduser == null)
			{
				return false;
			}
			this.MailboxUser = aduser;
			return base.Sync();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000614E File Offset: 0x0000434E
		public override void Activate()
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006150 File Offset: 0x00004350
		public override void Deactivate()
		{
		}
	}
}
