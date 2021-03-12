using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000044 RID: 68
	internal class StoreDriverDeliveryEventArgsImpl : StoreDriverDeliveryEventArgs
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000D2DE File Offset: 0x0000B4DE
		internal StoreDriverDeliveryEventArgsImpl(MailItemDeliver mailItemDeliver)
		{
			this.mailItemDeliver = mailItemDeliver;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000D2ED File Offset: 0x0000B4ED
		public override DeliverableMailItem MailItem
		{
			get
			{
				return this.mailItemDeliver.MailItemWrapper;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000D2FA File Offset: 0x0000B4FA
		public override RoutingAddress RecipientAddress
		{
			get
			{
				if (this.MailRecipient == null)
				{
					return RoutingAddress.Empty;
				}
				return this.MailRecipient.Email;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000D315 File Offset: 0x0000B515
		public override string MessageClass
		{
			get
			{
				return this.mailItemDeliver.MessageClass;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000D322 File Offset: 0x0000B522
		internal MessageItem ReplayItem
		{
			get
			{
				return this.mailItemDeliver.ReplayItem;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000D32F File Offset: 0x0000B52F
		internal MessageItem MessageItem
		{
			get
			{
				return this.mailItemDeliver.DeliveryItem.Message;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000D341 File Offset: 0x0000B541
		internal MailRecipient MailRecipient
		{
			get
			{
				return this.mailItemDeliver.Recipient;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000D34E File Offset: 0x0000B54E
		internal bool IsPublicFolderRecipient
		{
			get
			{
				return this.mailItemDeliver.IsPublicFolderRecipient;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000D35B File Offset: 0x0000B55B
		internal bool IsJournalReport
		{
			get
			{
				return this.mailItemDeliver.IsJournalReport;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000D368 File Offset: 0x0000B568
		internal StoreSession StoreSession
		{
			get
			{
				return this.mailItemDeliver.DeliveryItem.Session;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000D37A File Offset: 0x0000B57A
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailItemDeliver.DeliveryItem.MailboxSession;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000D38C File Offset: 0x0000B58C
		internal PublicFolderSession PublicFolderSession
		{
			get
			{
				return this.mailItemDeliver.DeliveryItem.PublicFolderSession;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000D39E File Offset: 0x0000B59E
		internal ADRecipientCache<TransportMiniRecipient> ADRecipientCache
		{
			get
			{
				return (ADRecipientCache<TransportMiniRecipient>)this.MailItem.RecipientCache;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		internal MailItemDeliver MailItemDeliver
		{
			get
			{
				return this.mailItemDeliver;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000D3CA File Offset: 0x0000B5CA
		internal StoreId DeliverToFolder
		{
			get
			{
				return this.mailItemDeliver.DeliveryItem.DeliverToFolder;
			}
			set
			{
				this.mailItemDeliver.DeliveryItem.DeliverToFolder = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000D3DD File Offset: 0x0000B5DD
		internal string DeliverToFolderName
		{
			set
			{
				this.mailItemDeliver.DeliverToFolderName = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000D3EB File Offset: 0x0000B5EB
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000D3F3 File Offset: 0x0000B5F3
		internal object RetentionPolicyTag
		{
			get
			{
				return this.retentionPolicyTag;
			}
			set
			{
				this.retentionPolicyTag = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000D3FC File Offset: 0x0000B5FC
		internal MiniRecipient MailboxOwner
		{
			get
			{
				ProxyAddress proxyAddress = new SmtpProxyAddress((string)this.MailRecipient.Email, true);
				return this.ADRecipientCache.FindAndCacheRecipient(proxyAddress).Data;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000D434 File Offset: 0x0000B634
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000D43C File Offset: 0x0000B63C
		internal object RetentionPeriod
		{
			get
			{
				return this.retentionPeriod;
			}
			set
			{
				this.retentionPeriod = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000D445 File Offset: 0x0000B645
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000D44D File Offset: 0x0000B64D
		internal object RetentionFlags
		{
			get
			{
				return this.retentionFlags;
			}
			set
			{
				this.retentionFlags = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000D456 File Offset: 0x0000B656
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000D45E File Offset: 0x0000B65E
		internal object ArchiveTag
		{
			get
			{
				return this.archiveTag;
			}
			set
			{
				this.archiveTag = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000D467 File Offset: 0x0000B667
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000D46F File Offset: 0x0000B66F
		internal object ArchivePeriod
		{
			get
			{
				return this.archivePeriod;
			}
			set
			{
				this.archivePeriod = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000D478 File Offset: 0x0000B678
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000D480 File Offset: 0x0000B680
		internal object CompactDefaultRetentionPolicy
		{
			get
			{
				return this.compactDefaultRetentionPolicy;
			}
			set
			{
				this.compactDefaultRetentionPolicy = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000D489 File Offset: 0x0000B689
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000D491 File Offset: 0x0000B691
		internal bool ShouldSkipMoveRule
		{
			get
			{
				return this.shouldSkipMoveRule;
			}
			set
			{
				this.shouldSkipMoveRule = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000D49A File Offset: 0x0000B69A
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000D4A2 File Offset: 0x0000B6A2
		internal bool ShouldRunMailboxRulesBasedOnDeliveryFolder
		{
			get
			{
				return this.shouldRunMailboxRulesBasedOnDeliveryFolder;
			}
			set
			{
				this.shouldRunMailboxRulesBasedOnDeliveryFolder = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000D4AB File Offset: 0x0000B6AB
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000D4B3 File Offset: 0x0000B6B3
		internal Dictionary<PropertyDefinition, object> PropertiesForAllMessageCopies
		{
			get
			{
				return this.propertiesForAllMessageCopies;
			}
			set
			{
				this.propertiesForAllMessageCopies = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		internal Dictionary<PropertyDefinition, object> PropertiesForDelegateForward
		{
			get
			{
				return this.propertiesForDelegateForward;
			}
			set
			{
				this.propertiesForDelegateForward = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000D4CD File Offset: 0x0000B6CD
		// (set) Token: 0x0600030B RID: 779 RVA: 0x0000D4D5 File Offset: 0x0000B6D5
		internal bool ShouldCreateItemForDelete
		{
			get
			{
				return this.shouldCreateItemForDelete;
			}
			set
			{
				this.shouldCreateItemForDelete = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000D4DE File Offset: 0x0000B6DE
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000D4E6 File Offset: 0x0000B6E6
		internal SmtpResponse BounceSmtpResponse
		{
			get
			{
				return this.bounceSmtpResponse;
			}
			set
			{
				this.bounceSmtpResponse = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000D4EF File Offset: 0x0000B6EF
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000D4F7 File Offset: 0x0000B6F7
		internal string BounceSource
		{
			get
			{
				return this.bounceSource;
			}
			set
			{
				this.bounceSource = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000D500 File Offset: 0x0000B700
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000D508 File Offset: 0x0000B708
		internal Dictionary<PropertyDefinition, object> SharedPropertiesBetweenAgents
		{
			get
			{
				return this.sharedPropertiesBetweenAgents;
			}
			set
			{
				this.sharedPropertiesBetweenAgents = value;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000D511 File Offset: 0x0000B711
		public void AddDeliveryErrors(List<string> deliveryErrors)
		{
			this.mailItemDeliver.AddDeliveryErrors(deliveryErrors);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000D51F File Offset: 0x0000B71F
		public void SetDeliveryFolder(Folder folder)
		{
			this.mailItemDeliver.SetDeliveryFolder(folder);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D52D File Offset: 0x0000B72D
		public override void AddAgentInfo(string agentName, string eventName, List<KeyValuePair<string, string>> data)
		{
			this.MailItemDeliver.MbxTransportMailItem.AddAgentInfo(agentName, eventName, data);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000D542 File Offset: 0x0000B742
		internal void ResetPerRecipientState()
		{
			this.PropertiesForAllMessageCopies = null;
			this.PropertiesForDelegateForward = null;
			this.SharedPropertiesBetweenAgents = null;
			this.ShouldSkipMoveRule = false;
		}

		// Token: 0x04000139 RID: 313
		private MailItemDeliver mailItemDeliver;

		// Token: 0x0400013A RID: 314
		private Dictionary<PropertyDefinition, object> propertiesForAllMessageCopies;

		// Token: 0x0400013B RID: 315
		private Dictionary<PropertyDefinition, object> propertiesForDelegateForward;

		// Token: 0x0400013C RID: 316
		private bool shouldSkipMoveRule;

		// Token: 0x0400013D RID: 317
		private bool shouldRunMailboxRulesBasedOnDeliveryFolder;

		// Token: 0x0400013E RID: 318
		private object retentionPolicyTag;

		// Token: 0x0400013F RID: 319
		private object retentionPeriod;

		// Token: 0x04000140 RID: 320
		private object retentionFlags;

		// Token: 0x04000141 RID: 321
		private object archiveTag;

		// Token: 0x04000142 RID: 322
		private object archivePeriod;

		// Token: 0x04000143 RID: 323
		private object compactDefaultRetentionPolicy;

		// Token: 0x04000144 RID: 324
		private bool shouldCreateItemForDelete;

		// Token: 0x04000145 RID: 325
		private SmtpResponse bounceSmtpResponse;

		// Token: 0x04000146 RID: 326
		private string bounceSource;

		// Token: 0x04000147 RID: 327
		private Dictionary<PropertyDefinition, object> sharedPropertiesBetweenAgents;
	}
}
