using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OutlookService.Service;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000933 RID: 2355
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OutlookServiceSubscriptionItem : Item, IOutlookServiceSubscriptionItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x060057C5 RID: 22469 RVA: 0x0016909A File Offset: 0x0016729A
		internal OutlookServiceSubscriptionItem(ICoreItem coreItem) : base(coreItem, false)
		{
			if (base.IsNew)
			{
				this.Initialize();
			}
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x060057C6 RID: 22470 RVA: 0x001690B2 File Offset: 0x001672B2
		// (set) Token: 0x060057C7 RID: 22471 RVA: 0x001690CB File Offset: 0x001672CB
		public string SubscriptionId
		{
			get
			{
				this.CheckDisposed("SubscriptionId::get");
				return base.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.SubscriptionId, null);
			}
			set
			{
				this.CheckDisposed("SubscriptionId::set");
				this[OutlookServiceSubscriptionItemSchema.SubscriptionId] = value;
			}
		}

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x060057C8 RID: 22472 RVA: 0x001690E4 File Offset: 0x001672E4
		// (set) Token: 0x060057C9 RID: 22473 RVA: 0x00169101 File Offset: 0x00167301
		public ExDateTime LastUpdateTimeUTC
		{
			get
			{
				this.CheckDisposed("LastUpdateTimeUTC::get");
				return base.GetValueOrDefault<ExDateTime>(OutlookServiceSubscriptionItemSchema.LastUpdateTimeUTC, ExDateTime.UtcNow);
			}
			set
			{
				this.CheckDisposed("LastUpdateTimeUTC::set");
				this[OutlookServiceSubscriptionItemSchema.LastUpdateTimeUTC] = value;
			}
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x060057CA RID: 22474 RVA: 0x0016911F File Offset: 0x0016731F
		// (set) Token: 0x060057CB RID: 22475 RVA: 0x0016913D File Offset: 0x0016733D
		public string PackageId
		{
			get
			{
				this.CheckDisposed("PackageId::get");
				return base.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.PackageId, this.AppId);
			}
			set
			{
				this.CheckDisposed("PackageId::set");
				this[OutlookServiceSubscriptionItemSchema.PackageId] = value;
			}
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x060057CC RID: 22476 RVA: 0x00169156 File Offset: 0x00167356
		// (set) Token: 0x060057CD RID: 22477 RVA: 0x0016916F File Offset: 0x0016736F
		public string AppId
		{
			get
			{
				this.CheckDisposed("AppId::get");
				return base.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.AppId, null);
			}
			set
			{
				this.CheckDisposed("AppId::set");
				this[OutlookServiceSubscriptionItemSchema.AppId] = value;
			}
		}

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x060057CE RID: 22478 RVA: 0x00169188 File Offset: 0x00167388
		// (set) Token: 0x060057CF RID: 22479 RVA: 0x001691A1 File Offset: 0x001673A1
		public string DeviceNotificationId
		{
			get
			{
				this.CheckDisposed("DeviceNotificationId::get");
				return base.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.DeviceNotificationId, null);
			}
			set
			{
				this.CheckDisposed("DeviceNotificationId::set");
				this[OutlookServiceSubscriptionItemSchema.DeviceNotificationId] = value;
			}
		}

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x060057D0 RID: 22480 RVA: 0x001691BA File Offset: 0x001673BA
		// (set) Token: 0x060057D1 RID: 22481 RVA: 0x001691D7 File Offset: 0x001673D7
		public ExDateTime ExpirationTime
		{
			get
			{
				this.CheckDisposed("ExpirationTime::get");
				return base.GetValueOrDefault<ExDateTime>(OutlookServiceSubscriptionItemSchema.ExpirationTime, ExDateTime.UtcNow);
			}
			set
			{
				this.CheckDisposed("ExpirationTime::set");
				this[OutlookServiceSubscriptionItemSchema.ExpirationTime] = value;
			}
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x060057D2 RID: 22482 RVA: 0x001691F5 File Offset: 0x001673F5
		// (set) Token: 0x060057D3 RID: 22483 RVA: 0x0016920E File Offset: 0x0016740E
		public bool LockScreen
		{
			get
			{
				this.CheckDisposed("LockScreen::get");
				return base.GetValueOrDefault<bool>(OutlookServiceSubscriptionItemSchema.LockScreen, true);
			}
			set
			{
				this.CheckDisposed("LockScreen::set");
				this[OutlookServiceSubscriptionItemSchema.LockScreen] = value;
			}
		}

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x060057D4 RID: 22484 RVA: 0x0016922C File Offset: 0x0016742C
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return OutlookServiceSubscriptionItemSchema.Instance;
			}
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x00169240 File Offset: 0x00167440
		private void Initialize()
		{
			this[InternalSchema.ItemClass] = "OutlookService.Notification.Subscription";
			this[OutlookServiceSubscriptionItemSchema.LastUpdateTimeUTC] = ExDateTime.UtcNow;
			if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<string>((long)this.GetHashCode(), "OutlookServiceSubscriptionItem.Initialize: Initialized new SubscriptionItem, RefTm:{1}", this[OutlookServiceSubscriptionItemSchema.LastUpdateTimeUTC].ToString());
			}
		}
	}
}
