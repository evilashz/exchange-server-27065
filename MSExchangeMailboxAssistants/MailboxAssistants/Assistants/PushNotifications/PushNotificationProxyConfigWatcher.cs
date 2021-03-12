using System;
using System.Timers;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000212 RID: 530
	internal class PushNotificationProxyConfigWatcher : DisposableObject
	{
		// Token: 0x06001443 RID: 5187 RVA: 0x00074EAD File Offset: 0x000730AD
		public PushNotificationProxyConfigWatcher(PushNotificationAssistantConfig assistantConfig)
		{
			ArgumentValidator.ThrowIfNull("assistantConfig", assistantConfig);
			this.AssistantConfig = assistantConfig;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00074EC7 File Offset: 0x000730C7
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x00074ECF File Offset: 0x000730CF
		private PushNotificationAssistantConfig AssistantConfig { get; set; }

		// Token: 0x06001446 RID: 5190 RVA: 0x00074ED8 File Offset: 0x000730D8
		public void Start()
		{
			this.AssistantConfig.IsProxyPublishingEnabled = this.IsProxyEnabled();
			this.timer = new Timer(this.AssistantConfig.ProxyWatcherIntervalTimeInMinutes * 60U * 1000U);
			this.timer.Elapsed += this.OnTimedEvent;
			this.timer.Start();
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00074F39 File Offset: 0x00073139
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PushNotificationProxyConfigWatcher>(this);
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00074F41 File Offset: 0x00073141
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.timer != null)
			{
				this.timer.Stop();
				this.timer.Dispose();
				this.timer = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00074F72 File Offset: 0x00073172
		private void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			this.AssistantConfig.IsProxyPublishingEnabled = this.IsProxyEnabled();
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00074F88 File Offset: 0x00073188
		private bool IsProxyEnabled()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 107, "IsProxyEnabled", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\PushNotifications\\PushNotificationProxyConfigWatcher.cs");
			string text = null;
			try
			{
				foreach (PushNotificationApp pushNotificationApp in topologyConfigurationSession.FindAllPaged<PushNotificationApp>())
				{
					if (pushNotificationApp.Platform == PushNotificationPlatform.Proxy && pushNotificationApp.Enabled.GetValueOrDefault(false))
					{
						return true;
					}
				}
			}
			catch (ADTransientException exception)
			{
				text = exception.ToTraceString();
			}
			catch (ADOperationException exception2)
			{
				text = exception2.ToTraceString();
			}
			if (text != null)
			{
				PushNotificationsCrimsonEvents.ErrorReadingProxyConfiguration.Log<string>(text);
				ExTraceGlobals.PushNotificationServiceTracer.TraceWarning<string>((long)this.GetHashCode(), "An error was generated when attempting to read the server configuration '{0}'.", text);
				return this.AssistantConfig.IsPublishingEnabled;
			}
			return false;
		}

		// Token: 0x04000C30 RID: 3120
		private Timer timer;
	}
}
