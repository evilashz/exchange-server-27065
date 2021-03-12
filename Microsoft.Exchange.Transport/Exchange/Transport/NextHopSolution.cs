using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200018D RID: 397
	internal class NextHopSolution : IReadOnlyMailRecipientCollection, IEnumerable<MailRecipient>, IEnumerable
	{
		// Token: 0x0600116B RID: 4459 RVA: 0x0004725C File Offset: 0x0004545C
		public NextHopSolution(NextHopSolutionKey key)
		{
			this.nextHopSolutionKey = key;
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x00047281 File Offset: 0x00045481
		public List<MailRecipient> Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x00047289 File Offset: 0x00045489
		public NextHopSolutionKey NextHopSolutionKey
		{
			get
			{
				return this.nextHopSolutionKey;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x00047291 File Offset: 0x00045491
		// (set) Token: 0x0600116F RID: 4463 RVA: 0x00047299 File Offset: 0x00045499
		public DeliveryStatus DeliveryStatus
		{
			get
			{
				return this.deliveryStatus;
			}
			set
			{
				this.deliveryStatus = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x000472A2 File Offset: 0x000454A2
		public bool IsInactive
		{
			get
			{
				return this.deliveryStatus == DeliveryStatus.Complete || this.deliveryStatus == DeliveryStatus.PendingResubmit;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x000472B8 File Offset: 0x000454B8
		public bool IsDeletedByAdmin
		{
			get
			{
				return this.AdminActionStatus == AdminActionStatus.PendingDeleteWithNDR || this.AdminActionStatus == AdminActionStatus.PendingDeleteWithOutNDR;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x000472CE File Offset: 0x000454CE
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x000472D6 File Offset: 0x000454D6
		public bool Deferred
		{
			get
			{
				return this.inDeferredQueue;
			}
			set
			{
				this.inDeferredQueue = value;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x000472DF File Offset: 0x000454DF
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x000472E7 File Offset: 0x000454E7
		public DateTime DeferUntil
		{
			get
			{
				return this.deferUntil;
			}
			set
			{
				this.deferUntil = value;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x000472F0 File Offset: 0x000454F0
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x00047308 File Offset: 0x00045508
		public AdminActionStatus AdminActionStatus
		{
			get
			{
				if (this.adminActionRecipient == null)
				{
					return AdminActionStatus.None;
				}
				return this.adminActionRecipient.AdminActionStatus;
			}
			set
			{
				if (this.adminActionRecipient != null)
				{
					this.adminActionRecipient.AdminActionStatus = value;
					if (value == AdminActionStatus.None)
					{
						this.adminActionRecipient = null;
						return;
					}
				}
				else if (value != AdminActionStatus.None)
				{
					MailRecipient mailRecipient = this.recipients.Find(new Predicate<MailRecipient>(NextHopSolution.IsNotProcessed));
					if (mailRecipient == null)
					{
						throw new InvalidOperationException("Attempting to set the admin status of a solution but all recipients have been processed");
					}
					this.adminActionRecipient = mailRecipient;
					this.adminActionRecipient.AdminActionStatus = value;
				}
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00047370 File Offset: 0x00045570
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x00047378 File Offset: 0x00045578
		public AccessToken AccessToken
		{
			get
			{
				return this.accessToken;
			}
			internal set
			{
				this.accessToken = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00047381 File Offset: 0x00045581
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x00047389 File Offset: 0x00045589
		public string LockReason
		{
			get
			{
				return this.lockReason;
			}
			internal set
			{
				this.lockReason = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x00047392 File Offset: 0x00045592
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x0004739A File Offset: 0x0004559A
		public DateTimeOffset LockExpirationTime
		{
			get
			{
				return this.lockExpirationTime;
			}
			internal set
			{
				this.lockExpirationTime = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x000473A3 File Offset: 0x000455A3
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x000473AB File Offset: 0x000455AB
		public WaitCondition CurrentCondition
		{
			get
			{
				return this.currentCondition;
			}
			internal set
			{
				this.currentCondition = value;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x000473B4 File Offset: 0x000455B4
		int IReadOnlyMailRecipientCollection.Count
		{
			get
			{
				return this.recipients.Count;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x000473C1 File Offset: 0x000455C1
		IEnumerable<MailRecipient> IReadOnlyMailRecipientCollection.All
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x000473DE File Offset: 0x000455DE
		IEnumerable<MailRecipient> IReadOnlyMailRecipientCollection.AllUnprocessed
		{
			get
			{
				return from recipient in this.recipients
				where recipient.IsActive && !recipient.IsProcessed
				select recipient;
			}
		}

		// Token: 0x170004A8 RID: 1192
		MailRecipient IReadOnlyMailRecipientCollection.this[int index]
		{
			get
			{
				return this.recipients[index];
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00047418 File Offset: 0x00045618
		public void AddRecipient(MailRecipient recipient)
		{
			this.recipients.Add(recipient);
			if (recipient.AdminActionStatus != AdminActionStatus.None)
			{
				if (this.adminActionRecipient != null)
				{
					if (this.adminActionRecipient.AdminActionStatus < recipient.AdminActionStatus)
					{
						this.adminActionRecipient.AdminActionStatus = AdminActionStatus.None;
						this.adminActionRecipient = recipient;
						return;
					}
					recipient.AdminActionStatus = AdminActionStatus.None;
					return;
				}
				else
				{
					this.adminActionRecipient = recipient;
				}
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00047478 File Offset: 0x00045678
		public void AddRecipients(IEnumerable<MailRecipient> recipients)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			foreach (MailRecipient recipient in recipients)
			{
				this.AddRecipient(recipient);
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000474D0 File Offset: 0x000456D0
		public void CheckAdminAction()
		{
			if (this.AdminActionStatus == AdminActionStatus.Suspended && this.deliveryStatus != DeliveryStatus.Complete && this.adminActionRecipient.IsProcessed)
			{
				MailRecipient mailRecipient = this.recipients.Find(new Predicate<MailRecipient>(NextHopSolution.IsNotProcessed));
				if (mailRecipient == null)
				{
					throw new InvalidOperationException("Attempting to switch the admin recipient of a solution but all recipients have been processed");
				}
				AdminActionStatus adminActionStatus = this.adminActionRecipient.AdminActionStatus;
				this.adminActionRecipient.AdminActionStatus = AdminActionStatus.None;
				this.adminActionRecipient = mailRecipient;
				this.adminActionRecipient.AdminActionStatus = adminActionStatus;
			}
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00047550 File Offset: 0x00045750
		public int GetOutboundIPPool()
		{
			if (this.recipients.Count == 0)
			{
				return 0;
			}
			int outboundIPPool = this.recipients[0].OutboundIPPool;
			foreach (MailRecipient mailRecipient in this.recipients)
			{
				if (mailRecipient.OutboundIPPool != outboundIPPool)
				{
					throw new InvalidOperationException("NextHopSolution has recipients with different Outbound IP Pools");
				}
			}
			return outboundIPPool;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000475D4 File Offset: 0x000457D4
		bool IReadOnlyMailRecipientCollection.Contains(MailRecipient item)
		{
			return this.recipients.Contains(item);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000475E2 File Offset: 0x000457E2
		public IEnumerator<MailRecipient> GetEnumerator()
		{
			return this.recipients.GetEnumerator();
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000475F4 File Offset: 0x000457F4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x000475FC File Offset: 0x000457FC
		private static bool IsNotProcessed(MailRecipient item)
		{
			return !item.IsProcessed || item.NextHop.NextHopType.DeliveryType == DeliveryType.ShadowRedundancy;
		}

		// Token: 0x04000944 RID: 2372
		private MailRecipient adminActionRecipient;

		// Token: 0x04000945 RID: 2373
		private List<MailRecipient> recipients = new List<MailRecipient>();

		// Token: 0x04000946 RID: 2374
		private NextHopSolutionKey nextHopSolutionKey;

		// Token: 0x04000947 RID: 2375
		private DeliveryStatus deliveryStatus;

		// Token: 0x04000948 RID: 2376
		private bool inDeferredQueue;

		// Token: 0x04000949 RID: 2377
		private AccessToken accessToken;

		// Token: 0x0400094A RID: 2378
		private DateTimeOffset lockExpirationTime;

		// Token: 0x0400094B RID: 2379
		private string lockReason;

		// Token: 0x0400094C RID: 2380
		private WaitCondition currentCondition;

		// Token: 0x0400094D RID: 2381
		private DateTime deferUntil = DateTime.MinValue;
	}
}
