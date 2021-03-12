using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000CF RID: 207
	internal class WebAppNotification : PushNotification
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x00015A79 File Offset: 0x00013C79
		public WebAppNotification(string appId, OrganizationId tenantId, string action, string payload) : base(appId, tenantId)
		{
			this.Action = action;
			this.Payload = payload;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00015A92 File Offset: 0x00013C92
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x00015A9A File Offset: 0x00013C9A
		public string Action { get; private set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00015AA3 File Offset: 0x00013CA3
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x00015AAB File Offset: 0x00013CAB
		public string Payload { get; private set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00015AB4 File Offset: 0x00013CB4
		public override string RecipientId
		{
			get
			{
				return this.Action;
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00015ABC File Offset: 0x00013CBC
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (this.Payload == null)
			{
				errors.Add(Strings.InvalidPayload);
				return;
			}
			if (string.IsNullOrWhiteSpace(this.Action))
			{
				errors.Add(Strings.WebAppInvalidAction);
			}
			if (string.IsNullOrWhiteSpace(this.Payload))
			{
				errors.Add(Strings.WebAppInvalidPayload);
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00015B14 File Offset: 0x00013D14
		protected override string InternalToFullString()
		{
			return string.Format("{0}; payload:{1}", base.InternalToFullString(), this.Payload.ToNullableString());
		}
	}
}
