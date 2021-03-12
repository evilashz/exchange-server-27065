using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000201 RID: 513
	internal sealed class PushNotificationAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x060013B0 RID: 5040 RVA: 0x0007314C File Offset: 0x0007134C
		static PushNotificationAssistant()
		{
			RegistrySession registrySession = new RegistrySession(true);
			PushNotificationBatchManagerConfig pushNotificationBatchManagerConfig = registrySession.Read<PushNotificationBatchManagerConfig>();
			PushNotificationHelper.CheckAndLogInvalidConfigurationSetting(pushNotificationBatchManagerConfig);
			PushNotificationAssistant.AssistantConfig = registrySession.Read<PushNotificationAssistantConfig>();
			PushNotificationHelper.CheckAndLogInvalidConfigurationSetting(PushNotificationAssistant.AssistantConfig);
			PushNotificationAssistant.SharedResources = new PushNotificationAssistant.SharedResourcesGuard(pushNotificationBatchManagerConfig, PushNotificationAssistant.AssistantConfig);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0007319C File Offset: 0x0007139C
		public PushNotificationAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.assistantAdapter = new PushNotificationAssistantAdapter(PushNotificationAssistant.AssistantConfig, databaseInfo, XSOFactory.Default, PushNotificationAssistant.TableEntry, PushNotificationAssistant.SharedResources.TakeReference());
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x000731CC File Offset: 0x000713CC
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			return this.assistantAdapter.IsEventInteresting(mapiEvent);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000731DC File Offset: 0x000713DC
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			try
			{
				this.assistantAdapter.HandleEvent(mapiEvent, itemStore, item);
			}
			catch (PushNotificationBaseException ex)
			{
				PushNotificationHelper.LogHandleEventError(ex, base.DatabaseInfo, mapiEvent);
				throw new SkipException(ex);
			}
			catch (Exception ex2)
			{
				PushNotificationHelper.LogHandleEventError(ex2, base.DatabaseInfo, mapiEvent);
				throw;
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0007323C File Offset: 0x0007143C
		protected override void OnStartInternal(EventBasedStartInfo startInfo)
		{
			base.OnStartInternal(startInfo);
			this.assistantAdapter.OnStart(startInfo);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00073251 File Offset: 0x00071451
		protected override void OnShutdownInternal()
		{
			base.OnShutdownInternal();
			this.assistantAdapter.OnShutdown();
			PushNotificationAssistant.SharedResources.ReturnReference();
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0007326E File Offset: 0x0007146E
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00073276 File Offset: 0x00071476
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0007327E File Offset: 0x0007147E
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000C05 RID: 3077
		private static readonly PushNotificationSubscriptionTableEntry TableEntry = new PushNotificationSubscriptionTableEntry();

		// Token: 0x04000C06 RID: 3078
		private static readonly PushNotificationAssistant.SharedResourcesGuard SharedResources;

		// Token: 0x04000C07 RID: 3079
		private static readonly PushNotificationAssistantConfig AssistantConfig;

		// Token: 0x04000C08 RID: 3080
		private readonly PushNotificationAssistantAdapter assistantAdapter;

		// Token: 0x02000202 RID: 514
		private class SharedResourcesGuard
		{
			// Token: 0x060013B9 RID: 5049 RVA: 0x00073286 File Offset: 0x00071486
			public SharedResourcesGuard(PushNotificationBatchManagerConfig config, PushNotificationAssistantConfig assistantConfig)
			{
				this.BatchConfig = config;
				this.AssistantConfig = assistantConfig;
				this.referenceCounter = 0;
				this.syncroot = new object();
			}

			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x060013BA RID: 5050 RVA: 0x000732AE File Offset: 0x000714AE
			// (set) Token: 0x060013BB RID: 5051 RVA: 0x000732B6 File Offset: 0x000714B6
			private PushNotificationAssistantConfig AssistantConfig { get; set; }

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x060013BC RID: 5052 RVA: 0x000732BF File Offset: 0x000714BF
			// (set) Token: 0x060013BD RID: 5053 RVA: 0x000732C7 File Offset: 0x000714C7
			private PushNotificationBatchManagerConfig BatchConfig { get; set; }

			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x060013BE RID: 5054 RVA: 0x000732D0 File Offset: 0x000714D0
			// (set) Token: 0x060013BF RID: 5055 RVA: 0x000732D8 File Offset: 0x000714D8
			private PushNotificationBatchManager BatchManager { get; set; }

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x060013C0 RID: 5056 RVA: 0x000732E1 File Offset: 0x000714E1
			// (set) Token: 0x060013C1 RID: 5057 RVA: 0x000732E9 File Offset: 0x000714E9
			private PublisherServiceProxy PublisherClient { get; set; }

			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x060013C2 RID: 5058 RVA: 0x000732F2 File Offset: 0x000714F2
			// (set) Token: 0x060013C3 RID: 5059 RVA: 0x000732FA File Offset: 0x000714FA
			private PushNotificationProxyConfigWatcher ProxyWatcher { get; set; }

			// Token: 0x060013C4 RID: 5060 RVA: 0x00073304 File Offset: 0x00071504
			public PushNotificationBatchManager TakeReference()
			{
				PushNotificationBatchManager batchManager;
				lock (this.syncroot)
				{
					this.referenceCounter++;
					if (this.BatchManager == null)
					{
						this.PublisherClient = new PublisherServiceProxy(null);
						this.BatchManager = new PushNotificationBatchManager(this.BatchConfig, this.PublisherClient);
					}
					if (this.ProxyWatcher == null && !this.AssistantConfig.IsAssistantPublishingEnabled)
					{
						this.ProxyWatcher = new PushNotificationProxyConfigWatcher(this.AssistantConfig);
						this.ProxyWatcher.Start();
					}
					if (this.referenceCounter == 1)
					{
						PushNotificationHelper.LogAssistantPublishingStatus(this.AssistantConfig.IsPublishingEnabled);
					}
					batchManager = this.BatchManager;
				}
				return batchManager;
			}

			// Token: 0x060013C5 RID: 5061 RVA: 0x000733CC File Offset: 0x000715CC
			public void ReturnReference()
			{
				lock (this.syncroot)
				{
					if (this.referenceCounter > 0)
					{
						this.referenceCounter--;
					}
					if (this.referenceCounter == 0)
					{
						if (this.BatchManager != null)
						{
							this.BatchManager.Dispose();
							this.BatchManager = null;
							this.PublisherClient.Dispose();
							this.PublisherClient = null;
						}
						if (this.ProxyWatcher != null)
						{
							this.ProxyWatcher.Dispose();
							this.ProxyWatcher = null;
						}
					}
				}
			}

			// Token: 0x04000C09 RID: 3081
			private int referenceCounter;

			// Token: 0x04000C0A RID: 3082
			private object syncroot;
		}
	}
}
