using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services
{
	// Token: 0x0200001A RID: 26
	internal class ModernGroupNotificationLocator
	{
		// Token: 0x06000189 RID: 393 RVA: 0x000075CE File Offset: 0x000057CE
		internal ModernGroupNotificationLocator(IRecipientSession adSession)
		{
			this.adSession = adSession;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000075E0 File Offset: 0x000057E0
		internal IMemberSubscriptionItem GetMemberSubscription(MailboxSession mailboxSession, UserMailboxLocator userMailboxLocator)
		{
			return this.GetMemberSubscriptions(mailboxSession, new UserMailboxLocator[]
			{
				userMailboxLocator
			})[0];
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007604 File Offset: 0x00005804
		internal IMemberSubscriptionItem[] GetMemberSubscriptions(MailboxSession mailboxSession, IEnumerable<UserMailboxLocator> userMailboxLocators)
		{
			IEnumerable<UserMailbox> userGroupRelationship = this.GetUserGroupRelationship(mailboxSession, userMailboxLocators);
			MemberSubscriptionItem[] array = new MemberSubscriptionItem[userGroupRelationship.Count<UserMailbox>()];
			int num = 0;
			foreach (UserMailbox userMailbox in userGroupRelationship)
			{
				string subscriptionId = ModernGroupNotificationLocator.GetSubscriptionId(userMailbox.Locator);
				ExDateTime lastVisitedDate = ModernGroupNotificationLocator.GetLastVisitedDate(userMailbox);
				array[num++] = new MemberSubscriptionItem(subscriptionId, lastVisitedDate);
			}
			return array;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000076AC File Offset: 0x000058AC
		internal void UpdateMemberSubscription(MailboxSession mailboxSession, UserMailboxLocator userMailboxLocator)
		{
			ProxyAddress proxyAddress = new SmtpProxyAddress(mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), true);
			GroupMailboxLocator groupMailboxLocator = GroupMailboxLocator.Instantiate(this.adSession, proxyAddress);
			GroupMailboxAccessLayer.Execute("ModernGroupNotificationLocator.UpdateMemberSubscription", this.adSession, mailboxSession, delegate(GroupMailboxAccessLayer accessLayer)
			{
				accessLayer.SetLastVisitedDate(userMailboxLocator, groupMailboxLocator, ExDateTime.UtcNow);
			});
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000771A File Offset: 0x0000591A
		public static string GetSubscriptionId(IMailboxLocator mailboxLocator)
		{
			if (string.IsNullOrEmpty(mailboxLocator.ExternalId))
			{
				return mailboxLocator.LegacyDn;
			}
			return mailboxLocator.ExternalId;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000077B8 File Offset: 0x000059B8
		private IEnumerable<UserMailbox> GetUserGroupRelationship(MailboxSession mailboxSession, IEnumerable<UserMailboxLocator> userMailboxLocators)
		{
			IEnumerable<UserMailbox> members = null;
			ProxyAddress proxyAddress = new SmtpProxyAddress(mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), true);
			GroupMailboxLocator groupMailboxLocator = GroupMailboxLocator.Instantiate(this.adSession, proxyAddress);
			GroupMailboxAccessLayer.Execute("ModernGroupNotificationLocator.GetMailboxAssociation", this.adSession, mailboxSession, delegate(GroupMailboxAccessLayer accessLayer)
			{
				if (userMailboxLocators.Count<UserMailboxLocator>() > 0 && !string.IsNullOrEmpty(userMailboxLocators.First<UserMailboxLocator>().ExternalId))
				{
					members = accessLayer.GetUnseenMembers(groupMailboxLocator, userMailboxLocators);
					return;
				}
				ExWatson.SendReport(new InvalidOperationException("ModernGroupNotificationLocator - Getting unseen notification members without external id."), ReportOptions.None, null);
				members = accessLayer.GetMembers(groupMailboxLocator, userMailboxLocators, false);
			});
			return members;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007834 File Offset: 0x00005A34
		private static ExDateTime GetLastVisitedDate(UserMailbox member)
		{
			ExDateTime d = default(ExDateTime);
			if (member != null)
			{
				if (member.LastVisitedDate > member.JoinDate)
				{
					d = member.LastVisitedDate;
				}
				else
				{
					d = member.JoinDate;
				}
			}
			if (d == default(ExDateTime))
			{
				d = ExDateTime.UtcNow;
			}
			return d.ToUtc();
		}

		// Token: 0x04000141 RID: 321
		private IRecipientSession adSession;
	}
}
