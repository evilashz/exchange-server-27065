using System;
using Microsoft.Exchange.MailboxTransport.StoreDriverDelivery;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxTransport.Delivery
{
	// Token: 0x02000006 RID: 6
	internal class BackgroundThreadDelivery : BackgroundProcessingThreadBase
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000028DA File Offset: 0x00000ADA
		public BackgroundThreadDelivery() : base(TimeSpan.FromSeconds(1.0))
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000028F0 File Offset: 0x00000AF0
		protected override void Run()
		{
			DateTime utcNow = DateTime.UtcNow;
			this.lastTimeThrottlingManagerSwept = utcNow;
			this.lastDeliveryScan = utcNow;
			base.Run();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002918 File Offset: 0x00000B18
		protected override void RunMain(DateTime now)
		{
			Components.SmtpInComponent.UpdateTime(now);
			IStoreDriverDelivery storeDriverDelivery;
			bool flag = Components.TryGetStoreDriverDelivery(out storeDriverDelivery);
			if (now - this.lastTimeThrottlingManagerSwept > BackgroundThreadDelivery.ScanInterval)
			{
				Components.MessageThrottlingComponent.MessageThrottlingManager.CleanupIdleEntries();
				Components.UnhealthyTargetFilterComponent.CleanupExpiredEntries();
				this.lastTimeThrottlingManagerSwept = now;
			}
			if (Components.ResourceManager.IsMonitoringEnabled && now - Components.ResourceManager.LastTimeResourceMonitored > Components.ResourceManager.MonitorInterval)
			{
				Components.ResourceManager.OnMonitorResource(null);
			}
			if (flag)
			{
				this.DetectAndHandleDeliveryHang(now);
				this.UpdateDeliveryThreadCounters(now);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000029BC File Offset: 0x00000BBC
		private void DetectAndHandleDeliveryHang(DateTime now)
		{
			if (now - this.lastDeliveryScan > Components.Configuration.AppConfig.RemoteDelivery.StoreDriverDeliveryHangDetectionInterval)
			{
				string message;
				if (StoreDriverDeliveryDiagnostics.DetectDeliveryHang(out message) && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).MailboxTransport.DeliveryHangRecovery.Enabled)
				{
					throw new BackgroundThreadDelivery.DeliveryHangDetectedException(message);
				}
				this.lastDeliveryScan = now;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002A27 File Offset: 0x00000C27
		private void UpdateDeliveryThreadCounters(DateTime now)
		{
			if (now - this.lastDeliveryScan > TimeSpan.FromSeconds(5.0))
			{
				StoreDriverDeliveryDiagnostics.UpdateDeliveryThreadCounters();
			}
		}

		// Token: 0x0400002A RID: 42
		private static readonly TimeSpan ScanInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400002B RID: 43
		private DateTime lastTimeThrottlingManagerSwept;

		// Token: 0x0400002C RID: 44
		private DateTime lastDeliveryScan;

		// Token: 0x02000007 RID: 7
		private class DeliveryHangDetectedException : Exception
		{
			// Token: 0x0600001A RID: 26 RVA: 0x00002A64 File Offset: 0x00000C64
			public DeliveryHangDetectedException(string message) : base(message)
			{
			}
		}
	}
}
