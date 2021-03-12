using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002A2 RID: 674
	public class CalendarVDirSettingsLoader : OwaSettingsLoaderBase
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x00096691 File Offset: 0x00094891
		public override bool IsPushNotificationsEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x00096694 File Offset: 0x00094894
		public override bool IsPullNotificationsEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00096697 File Offset: 0x00094897
		public override bool IsFolderContentNotificationsEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x0009669A File Offset: 0x0009489A
		public override bool IsPreCheckinApp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x0009669D File Offset: 0x0009489D
		public override int ConnectionCacheSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x000966A0 File Offset: 0x000948A0
		public override bool ListenAdNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x000966A3 File Offset: 0x000948A3
		public override bool RenderBreadcrumbsInAboutPage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x000966A6 File Offset: 0x000948A6
		public override int MaximumTemporaryFilteredViewPerUser
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x000966A9 File Offset: 0x000948A9
		public override int MaximumFilteredViewInFavoritesPerUser
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x000966AC File Offset: 0x000948AC
		public override bool DisableBreadcrumbs
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x000966AF File Offset: 0x000948AF
		public override int MaxBreadcrumbs
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x000966B2 File Offset: 0x000948B2
		public override bool StoreTransientExceptionEventLogEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x000966B5 File Offset: 0x000948B5
		public override int StoreTransientExceptionEventLogThreshold
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x000966B8 File Offset: 0x000948B8
		public override int StoreTransientExceptionEventLogFrequencyInSeconds
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x000966BB File Offset: 0x000948BB
		public override int MaxPendingRequestLifeInSeconds
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x000966BE File Offset: 0x000948BE
		public override int MaxItemsInConversationExpansion
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x000966C1 File Offset: 0x000948C1
		public override int MaxItemsInConversationReadingPane
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x000966C4 File Offset: 0x000948C4
		public override long MaxBytesInConversationReadingPane
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x000966C8 File Offset: 0x000948C8
		public override bool HideDeletedItems
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x000966CB File Offset: 0x000948CB
		public override string OCSServerName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x000966CE File Offset: 0x000948CE
		public override int ActivityBasedPresenceDuration
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x000966D1 File Offset: 0x000948D1
		public override int MailTipsMaxClientCacheSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x000966D4 File Offset: 0x000948D4
		public override int MailTipsMaxMailboxSourcedRecipientSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x000966D7 File Offset: 0x000948D7
		public override int MailTipsClientCacheEntryExpiryInHours
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x000966DA File Offset: 0x000948DA
		internal override PhishingLevel MinimumSuspiciousPhishingLevel
		{
			get
			{
				return PhishingLevel.Suspicious1;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000966DD File Offset: 0x000948DD
		internal override int UserContextLockTimeout
		{
			get
			{
				return 3000;
			}
		}
	}
}
