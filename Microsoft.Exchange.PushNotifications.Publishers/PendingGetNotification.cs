using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000B3 RID: 179
	internal class PendingGetNotification : PushNotification
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x00013A13 File Offset: 0x00011C13
		public PendingGetNotification(string appId, OrganizationId tenantId, string subscriptionId, PendingGetPayload payload) : base(appId, tenantId)
		{
			this.SubscriptionId = subscriptionId;
			this.Payload = payload;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00013A2C File Offset: 0x00011C2C
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x00013A34 File Offset: 0x00011C34
		public string SubscriptionId { get; private set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00013A3D File Offset: 0x00011C3D
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x00013A45 File Offset: 0x00011C45
		public PendingGetPayload Payload { get; private set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00013A4E File Offset: 0x00011C4E
		public override string RecipientId
		{
			get
			{
				return this.SubscriptionId;
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00013A56 File Offset: 0x00011C56
		protected override string InternalToFullString()
		{
			return string.Format("{0}; subscriptionId:{1}; payload:{2}", base.InternalToFullString(), this.SubscriptionId.ToNullableString(), this.Payload.ToNullableString(null));
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00013A80 File Offset: 0x00011C80
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (string.IsNullOrEmpty(this.SubscriptionId))
			{
				errors.Add(Strings.InvalidSubscriptionId);
			}
			if (this.Payload == null)
			{
				errors.Add(Strings.InvalidPayload);
			}
			if (this.Payload.EmailCount == null)
			{
				errors.Add(Strings.InvalidEmailCount);
			}
		}
	}
}
