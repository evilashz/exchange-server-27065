using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000C5 RID: 197
	internal class ProxyNotification : PushNotification
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x00015238 File Offset: 0x00013438
		public ProxyNotification(string appId, string tenantId, MailboxNotificationBatch batch) : base(appId, OrganizationId.ForestWideOrgId)
		{
			this.NotificationBatch = batch;
			this.recipientId = tenantId;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00015254 File Offset: 0x00013454
		public ProxyNotification(string appId, IEnumerable<AzurePublisherSettings> azureSettings) : base(appId, OrganizationId.ForestWideOrgId)
		{
			this.AzureSettings = azureSettings;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00015269 File Offset: 0x00013469
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00015271 File Offset: 0x00013471
		public MailboxNotificationBatch NotificationBatch { get; private set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001527A File Offset: 0x0001347A
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00015282 File Offset: 0x00013482
		public IEnumerable<AzurePublisherSettings> AzureSettings { get; private set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001528B File Offset: 0x0001348B
		public override string RecipientId
		{
			get
			{
				return this.recipientId;
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00015293 File Offset: 0x00013493
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (this.NotificationBatch == null && this.AzureSettings == null)
			{
				errors.Add(Strings.InvalidProxyNotificationBatch);
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000152C8 File Offset: 0x000134C8
		protected override string InternalToFullString()
		{
			string format = "{0}; recipientId:{1}; batch:{2}; configuration:{3}";
			object[] array = new object[4];
			array[0] = base.InternalToFullString();
			array[1] = this.RecipientId;
			array[2] = this.NotificationBatch.ToNullableString((MailboxNotificationBatch x) => x.ToFullString());
			array[3] = this.AzureSettings.ToNullableString((AzurePublisherSettings x) => x.ToNullableString(null));
			return string.Format(format, array);
		}

		// Token: 0x04000351 RID: 849
		private readonly string recipientId;
	}
}
