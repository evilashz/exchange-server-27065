using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000838 RID: 2104
	public class OutlookServiceNotificationSubscription : ICloneable
	{
		// Token: 0x06003CA8 RID: 15528 RVA: 0x000D609E File Offset: 0x000D429E
		public OutlookServiceNotificationSubscription()
		{
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x000D60A8 File Offset: 0x000D42A8
		internal OutlookServiceNotificationSubscription(IOutlookServiceSubscriptionItem item)
		{
			this.SubscriptionId = item.SubscriptionId;
			this.LastUpdateTime = new ExDateTime?(item.LastUpdateTimeUTC);
			this.AppId = item.AppId;
			this.PackageId = item.PackageId;
			this.DeviceNotificationId = item.DeviceNotificationId;
			this.ExpirationTime = new ExDateTime?(item.ExpirationTime);
			this.LockScreen = item.LockScreen;
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x000D6119 File Offset: 0x000D4319
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003CAB RID: 15531 RVA: 0x000D6121 File Offset: 0x000D4321
		// (set) Token: 0x06003CAC RID: 15532 RVA: 0x000D6129 File Offset: 0x000D4329
		public string SubscriptionId { get; set; }

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x000D6132 File Offset: 0x000D4332
		// (set) Token: 0x06003CAE RID: 15534 RVA: 0x000D613A File Offset: 0x000D433A
		public ExDateTime? LastUpdateTime { get; set; }

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003CAF RID: 15535 RVA: 0x000D6143 File Offset: 0x000D4343
		// (set) Token: 0x06003CB0 RID: 15536 RVA: 0x000D614B File Offset: 0x000D434B
		public string AppId { get; set; }

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003CB1 RID: 15537 RVA: 0x000D6154 File Offset: 0x000D4354
		// (set) Token: 0x06003CB2 RID: 15538 RVA: 0x000D615C File Offset: 0x000D435C
		public string PackageId { get; set; }

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003CB3 RID: 15539 RVA: 0x000D6165 File Offset: 0x000D4365
		// (set) Token: 0x06003CB4 RID: 15540 RVA: 0x000D616D File Offset: 0x000D436D
		public string DeviceNotificationId { get; set; }

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x000D6176 File Offset: 0x000D4376
		// (set) Token: 0x06003CB6 RID: 15542 RVA: 0x000D617E File Offset: 0x000D437E
		public ExDateTime? ExpirationTime { get; set; }

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x000D6187 File Offset: 0x000D4387
		// (set) Token: 0x06003CB8 RID: 15544 RVA: 0x000D618F File Offset: 0x000D438F
		public bool LockScreen { get; set; }

		// Token: 0x06003CB9 RID: 15545 RVA: 0x000D6198 File Offset: 0x000D4398
		public virtual string ToFullString()
		{
			if (this.toFullStringCache == null)
			{
				this.toFullStringCache = string.Format("{{SubscriptionId:{0}; LastUpdateTime {1}; PackageId:{2}; AppId:{3}; DeviceNotificationId:{4}; ExpirationTime:{5}; LockScreen:{6}}}", new object[]
				{
					this.SubscriptionId ?? "null",
					(this.LastUpdateTime != null) ? this.LastUpdateTime.Value.ToString() : "null",
					this.PackageId ?? "null",
					this.AppId ?? "null",
					this.DeviceNotificationId ?? "null",
					(this.ExpirationTime != null) ? this.ExpirationTime.Value.ToString() : "null",
					this.LockScreen
				});
			}
			return this.toFullStringCache;
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x000D6294 File Offset: 0x000D4494
		public static string GenerateSubscriptionId(string appId, string deviceId)
		{
			if (string.IsNullOrWhiteSpace(appId))
			{
				throw new ArgumentException("Must have non-null and non-whitespace appid", "appId");
			}
			if (string.IsNullOrWhiteSpace(deviceId))
			{
				throw new ArgumentException("Must have non-null and non-whitespace deviceId", "deviceId");
			}
			string text = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", new object[]
			{
				appId,
				deviceId
			});
			text = Regex.Replace(text, "[^a-zA-Z0-9-_@#.:]+", string.Empty);
			if ((long)text.Length > 120L)
			{
				throw new InvalidOperationException("subscriptionId is too long : " + text);
			}
			return text;
		}

		// Token: 0x04002177 RID: 8567
		public const uint DefaultSubscriptionDeactivationInHours = 72U;

		// Token: 0x04002178 RID: 8568
		private const uint MaxSubscriptionIdLength = 120U;

		// Token: 0x04002179 RID: 8569
		public static readonly string AppId_HxMail = PushNotificationCannedApp.WnsOutlookMailOfficialWindowsImmersive.Name;

		// Token: 0x0400217A RID: 8570
		public static readonly string AppId_HxCalendar = PushNotificationCannedApp.WnsOutlookCalendarOfficialWindowsImmersive.Name;

		// Token: 0x0400217B RID: 8571
		private string toFullStringCache;
	}
}
