using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncPoisonContext
	{
		// Token: 0x0600024F RID: 591 RVA: 0x0000AFD0 File Offset: 0x000091D0
		public SyncPoisonContext(Guid subscriptionId)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionId", subscriptionId);
			this.subscriptionId = subscriptionId;
			this.hasSubscriptionContextOnly = true;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000AFF1 File Offset: 0x000091F1
		public SyncPoisonContext(Guid subscriptionId, SyncPoisonItem item)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionId", subscriptionId);
			SyncUtilities.ThrowIfArgumentNull("item", item);
			this.subscriptionId = subscriptionId;
			this.item = item;
			this.hasSubscriptionContextOnly = false;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000B024 File Offset: 0x00009224
		public Guid SubscriptionId
		{
			get
			{
				return this.subscriptionId;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000B02C File Offset: 0x0000922C
		public SyncPoisonItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000B034 File Offset: 0x00009234
		public bool HasSubscriptionContextOnly
		{
			get
			{
				return this.hasSubscriptionContextOnly;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B03C File Offset: 0x0000923C
		public override string ToString()
		{
			if (this.cachedToString == null)
			{
				this.cachedToString = string.Format(CultureInfo.InvariantCulture, "SubscriptionId: {0}, Item: {1}", new object[]
				{
					this.subscriptionId,
					this.item
				});
			}
			return this.cachedToString;
		}

		// Token: 0x0400013A RID: 314
		private const string FormatString = "SubscriptionId: {0}, Item: {1}";

		// Token: 0x0400013B RID: 315
		private readonly Guid subscriptionId;

		// Token: 0x0400013C RID: 316
		private readonly SyncPoisonItem item;

		// Token: 0x0400013D RID: 317
		private bool hasSubscriptionContextOnly;

		// Token: 0x0400013E RID: 318
		private string cachedToString;
	}
}
