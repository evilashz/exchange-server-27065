using System;
using System.Timers;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000092 RID: 146
	internal class PublisherConfigurationWatcher : IDisposable
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0000FBEE File Offset: 0x0000DDEE
		public PublisherConfigurationWatcher(string service, int refreshRateInMinutes)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("service", service);
			ArgumentValidator.ThrowIfNegative("refreshRateInMinutes", refreshRateInMinutes);
			this.Service = service;
			this.refreshRateInMinutes = refreshRateInMinutes;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060004D3 RID: 1235 RVA: 0x0000FC1C File Offset: 0x0000DE1C
		// (remove) Token: 0x060004D4 RID: 1236 RVA: 0x0000FC54 File Offset: 0x0000DE54
		public event EventHandler<ConfigurationChangedEventArgs> OnChangeEvent;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060004D5 RID: 1237 RVA: 0x0000FC8C File Offset: 0x0000DE8C
		// (remove) Token: 0x060004D6 RID: 1238 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		public event EventHandler<ConfigurationReadEventArgs> OnReadEvent;

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000FCF9 File Offset: 0x0000DEF9
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x0000FD01 File Offset: 0x0000DF01
		public string Service { get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000FD0A File Offset: 0x0000DF0A
		public int ResfreshRateInMinutes
		{
			get
			{
				return this.refreshRateInMinutes;
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000FD14 File Offset: 0x0000DF14
		public PushNotificationPublisherConfiguration Start()
		{
			this.configuration = this.LoadConfiguration(true);
			if (this.ResfreshRateInMinutes > 0)
			{
				this.timer = new Timer((double)(this.refreshRateInMinutes * 60 * 1000));
				this.timer.Elapsed += this.OnTimedEvent;
				this.timer.Start();
			}
			return this.configuration;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000FD7A File Offset: 0x0000DF7A
		public void Dispose()
		{
			if (this.timer != null)
			{
				this.timer.Stop();
				this.timer.Dispose();
				this.timer = null;
			}
			if (this.OnChangeEvent != null)
			{
				this.OnChangeEvent = null;
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		internal void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			try
			{
				PushNotificationPublisherConfiguration pushNotificationPublisherConfiguration = this.LoadConfiguration(false);
				EventHandler<ConfigurationReadEventArgs> onReadEvent = this.OnReadEvent;
				if (onReadEvent != null)
				{
					onReadEvent(this, new ConfigurationReadEventArgs(pushNotificationPublisherConfiguration));
				}
				if (!this.configuration.Equals(pushNotificationPublisherConfiguration))
				{
					string arg = this.configuration.ToFullString();
					string arg2 = pushNotificationPublisherConfiguration.ToFullString();
					ExTraceGlobals.PushNotificationServiceTracer.TraceDebug<int, string, string>((long)this.GetHashCode(), "[PublisherConfigurationWatcher:OnTimedEvent] Worker Process to be recycled by the configuration watcher due to a change on the configuration. Interval={0} minutes | Current={1} | Updated={2}.", this.refreshRateInMinutes, arg, arg2);
					PushNotificationsCrimsonEvents.PublisherConfigurationChanged.Log<string, string>(string.Format("{{interval:{0}; current:{{{1}}}; updated:{{{2}}}}}", this.refreshRateInMinutes, arg, arg2), this.Service);
					this.configuration = pushNotificationPublisherConfiguration;
					EventHandler<ConfigurationChangedEventArgs> onChangeEvent = this.OnChangeEvent;
					if (onChangeEvent != null)
					{
						onChangeEvent(this, new ConfigurationChangedEventArgs(pushNotificationPublisherConfiguration));
					}
				}
			}
			catch (Exception ex)
			{
				this.ReportException(ex);
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000FE84 File Offset: 0x0000E084
		protected virtual PushNotificationPublisherConfiguration LoadConfiguration(bool ignoreErrors = false)
		{
			return new PushNotificationPublisherConfiguration(ignoreErrors, null);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000FE8D File Offset: 0x0000E08D
		protected virtual void ReportException(Exception ex)
		{
			PushNotificationsCrimsonEvents.ErrorReadingConfiguration.Log<Exception>(ex);
			if (ExTraceGlobals.PushNotificationServiceTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				ExTraceGlobals.PushNotificationServiceTracer.TraceError<string>((long)this.GetHashCode(), "[PublisherConfigurationWatcher:OnTimedEvent] An error occurred when attempting to read the server configuration {1}.", ex.ToTraceString());
			}
		}

		// Token: 0x0400025C RID: 604
		private readonly int refreshRateInMinutes;

		// Token: 0x0400025D RID: 605
		private Timer timer;

		// Token: 0x0400025E RID: 606
		private PushNotificationPublisherConfiguration configuration;
	}
}
