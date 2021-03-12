using System;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000800 RID: 2048
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupMailboxLocator : MailboxLocator
	{
		// Token: 0x06004C6A RID: 19562 RVA: 0x0013C999 File Offset: 0x0013AB99
		public GroupMailboxLocator(IRecipientSession adSession, string externalDirectoryObjectId, string legacyDn) : base(adSession, externalDirectoryObjectId, legacyDn)
		{
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x0013C9A4 File Offset: 0x0013ABA4
		private GroupMailboxLocator(IRecipientSession adSession) : base(adSession)
		{
		}

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06004C6C RID: 19564 RVA: 0x0013C9AD File Offset: 0x0013ABAD
		public override string LocatorType
		{
			get
			{
				return GroupMailboxLocator.MailboxLocatorType;
			}
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x0013C9B4 File Offset: 0x0013ABB4
		public static GroupMailboxLocator Instantiate(IRecipientSession adSession, ProxyAddress proxyAddress)
		{
			GroupMailboxLocator groupMailboxLocator = new GroupMailboxLocator(adSession);
			groupMailboxLocator.InitializeFromAd(proxyAddress);
			return groupMailboxLocator;
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x0013C9D0 File Offset: 0x0013ABD0
		public static GroupMailboxLocator Instantiate(IRecipientSession adSession, ADUser adUser)
		{
			GroupMailboxLocator groupMailboxLocator = new GroupMailboxLocator(adSession);
			groupMailboxLocator.InitializeFromAd(adUser);
			return groupMailboxLocator;
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x0013C9EC File Offset: 0x0013ABEC
		public override bool IsValidReplicationTarget()
		{
			ADUser aduser = base.FindAdUser();
			return aduser.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox;
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x0013CA11 File Offset: 0x0013AC11
		public ModernGroupObjectType GetGroupType()
		{
			return base.FindAdUser().ModernGroupType;
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0013CA20 File Offset: 0x0013AC20
		public string GetThumbnailPhoto()
		{
			ADUser aduser = base.FindAdUser();
			if (aduser.ThumbnailPhoto == null)
			{
				return string.Empty;
			}
			return Convert.ToBase64String(aduser.ThumbnailPhoto);
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x0013CA4D File Offset: 0x0013AC4D
		public string GetYammerGroupAddress()
		{
			return base.FindAdUser().YammerGroupAddress;
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x0013CA5A File Offset: 0x0013AC5A
		protected override bool IsValidAdUser(ADUser adUser)
		{
			return MailboxLocatorValidator.IsValidGroupLocator(adUser);
		}

		// Token: 0x040029B8 RID: 10680
		public static readonly string MailboxLocatorType = "Group Mailbox";
	}
}
