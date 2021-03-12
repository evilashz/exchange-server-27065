using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200009E RID: 158
	internal class GcmNotification : PushNotification
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x00012830 File Offset: 0x00010A30
		public GcmNotification(string appId, OrganizationId tenantId, string registrationId, GcmPayload payload, string collapseKey = "c", bool? delayWhileIdle = null, int? timeToLive = null) : base(appId, tenantId)
		{
			this.RegistrationId = registrationId;
			this.Payload = payload;
			this.CollapseKey = collapseKey;
			this.DelayWhileIdle = delayWhileIdle;
			this.TimeToLive = timeToLive;
			if (payload != null)
			{
				payload.NotificationId = base.Identifier;
				base.IsBackgroundSyncAvailable = (payload.BackgroundSyncType != BackgroundSyncType.None);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00012890 File Offset: 0x00010A90
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x00012898 File Offset: 0x00010A98
		public string RegistrationId { get; private set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x000128A1 File Offset: 0x00010AA1
		public override string RecipientId
		{
			get
			{
				return this.RegistrationId;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x000128A9 File Offset: 0x00010AA9
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x000128B1 File Offset: 0x00010AB1
		public GcmPayload Payload { get; private set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x000128BA File Offset: 0x00010ABA
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x000128C2 File Offset: 0x00010AC2
		public string CollapseKey { get; private set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000128CB File Offset: 0x00010ACB
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x000128D3 File Offset: 0x00010AD3
		public bool? DelayWhileIdle { get; private set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x000128DC File Offset: 0x00010ADC
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x000128E4 File Offset: 0x00010AE4
		public int? TimeToLive { get; private set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x000128ED File Offset: 0x00010AED
		public virtual bool DryRun
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000128F0 File Offset: 0x00010AF0
		public string ToGcmFormat()
		{
			base.Validate();
			return this.serializedPayload;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00012900 File Offset: 0x00010B00
		protected override string InternalToFullString()
		{
			return string.Format("{0}; payload:{1}; collapseKey:{2}; delayWhileIdle:{3}; timeToLive:{4}", new object[]
			{
				base.InternalToFullString(),
				this.Payload.ToNullableString(null),
				this.CollapseKey.ToNullableString(),
				this.DelayWhileIdle.ToNullableString<bool>(),
				this.TimeToLive.ToNullableString<int>()
			});
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00012964 File Offset: 0x00010B64
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (string.IsNullOrWhiteSpace(this.RegistrationId))
			{
				errors.Add(Strings.GcmInvalidRegistrationId(this.RegistrationId.ToNullableString()));
			}
			if (this.TimeToLive != null && (this.TimeToLive.Value < 0 || this.TimeToLive.Value > 2419200))
			{
				errors.Add(Strings.GcmInvalidTimeToLive(this.TimeToLive.Value));
			}
			if (this.Payload == null || (this.Payload.UnseenEmailCount == null && string.IsNullOrWhiteSpace(this.Payload.Message) && string.IsNullOrWhiteSpace(this.Payload.ExtraData) && this.Payload.BackgroundSyncType == BackgroundSyncType.None))
			{
				errors.Add(Strings.GcmInvalidPayload);
			}
			this.serializedPayload = this.InternalToGcmFormat();
			if (this.serializedPayload.Length > 4096)
			{
				errors.Add(Strings.GcmInvalidPayloadLength(this.serializedPayload.Length, this.serializedPayload.Substring(0, Math.Min(this.serializedPayload.Length, 5120))));
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00012A9C File Offset: 0x00010C9C
		private string InternalToGcmFormat()
		{
			GcmPayloadWriter gcmPayloadWriter = new GcmPayloadWriter();
			gcmPayloadWriter.WriteProperty("registration_id", this.RegistrationId);
			gcmPayloadWriter.WriteProperty("collapse_key", this.CollapseKey);
			gcmPayloadWriter.WriteProperty<bool>("delay_while_idle", this.DelayWhileIdle);
			gcmPayloadWriter.WriteProperty<int>("time_to_live", this.TimeToLive);
			gcmPayloadWriter.WriteProperty("restricted_package_name", base.AppId);
			gcmPayloadWriter.WriteProperty<bool>("dry_run", this.IsMonitoring);
			if (this.Payload != null)
			{
				this.Payload.WriteGcmPayload(gcmPayloadWriter);
			}
			return gcmPayloadWriter.ToString();
		}

		// Token: 0x040002B3 RID: 691
		private const int MaxPayloadSize = 4096;

		// Token: 0x040002B4 RID: 692
		private const int MinTimeToLive = 0;

		// Token: 0x040002B5 RID: 693
		private const int MaxTimeToLive = 2419200;

		// Token: 0x040002B6 RID: 694
		private const string DefaultCollapseKey = "c";

		// Token: 0x040002B7 RID: 695
		private string serializedPayload;
	}
}
