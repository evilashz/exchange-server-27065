using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PushNotifications.Publishers;
using Microsoft.Exchange.PushNotifications.Server.Commands;
using Microsoft.Exchange.PushNotifications.Server.Core;
using Microsoft.Exchange.PushNotifications.Server.Wcf;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x0200001E RID: 30
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	[PushNotificationServiceBehavior]
	internal class PushNotificationOnPremService : ServiceBase, IAzureChallengeRequestServiceContract, IAzureDeviceRegistrationServiceContract, IPublisherServiceContract
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x000037B8 File Offset: 0x000019B8
		static PushNotificationOnPremService()
		{
			int maxThreadCount = 10 * Environment.ProcessorCount;
			Microsoft.Exchange.WorkloadManagement.UserWorkloadManager.Initialize(maxThreadCount, 500, 10, TimeSpan.FromMinutes(5.0), null);
			if (ActivityContextLogConfig.IsActivityContextLogEnabled())
			{
				ActivityContextLogger.Initialize();
			}
			PushNotificationOnPremService.ConfigWatcher = new PublisherConfigurationWatcher("MSExchangePushNotificationsAppPool", ServiceConfig.ConfigurationRefreshRateInMinutes);
			PushNotificationOnPremService.ConfigWatcher.OnChangeEvent += PushNotificationOnPremService.RestartPushNotificationAppPool;
			PushNotificationPublisherConfiguration pushNotificationPublisherConfiguration = PushNotificationOnPremService.ConfigWatcher.Start();
			PushNotificationOnPremService.IsRunningLegacyMode = (pushNotificationPublisherConfiguration.AzurePublisherSettings.Count<AzurePublisherSettings>() == 0);
			if (!PushNotificationOnPremService.IsRunningLegacyMode)
			{
				PushNotificationOnPremService.ConfigWatcher.OnReadEvent += PushNotificationOnPremService.ConfigurationRead;
				if (pushNotificationPublisherConfiguration.ProxyPublisherSettings != null && pushNotificationPublisherConfiguration.ProxyPublisherSettings.IsSuitable)
				{
					PushNotificationOnPremService.HubName = pushNotificationPublisherConfiguration.ProxyPublisherSettings.HubName;
				}
			}
			PushNotificationPublisherManagerBuilder pushNotificationPublisherManagerBuilder = new PushNotificationPublisherManagerBuilder(new List<PushNotificationPlatform>
			{
				PushNotificationPlatform.Azure,
				PushNotificationPlatform.AzureChallengeRequest,
				PushNotificationPlatform.AzureDeviceRegistration,
				PushNotificationPlatform.Proxy
			});
			PushNotificationOnPremService.PublisherManager = pushNotificationPublisherManagerBuilder.Build(pushNotificationPublisherConfiguration, null, null);
			if (ServiceConfig.IgnoreCertificateErrors)
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true));
			}
			AppDomain.CurrentDomain.DomainUnload += delegate(object sender, EventArgs e)
			{
				PushNotificationOnPremService.DisposeStaticResources();
			};
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003921 File Offset: 0x00001B21
		public PushNotificationOnPremService() : base(Microsoft.Exchange.WorkloadManagement.UserWorkloadManager.Singleton)
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000392E File Offset: 0x00001B2E
		[PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "LocalAdministrators")]
		[PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "UserService")]
		public IAsyncResult BeginPublishNotifications(MailboxNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState)
		{
			if (PushNotificationOnPremService.IsRunningLegacyMode)
			{
				return base.BeginServiceCommand(new PublishOnPremNotifications(notifications, PushNotificationOnPremService.PublisherManager, asyncCallback, asyncState));
			}
			return base.BeginServiceCommand(new PublishNotifications(notifications, PushNotificationOnPremService.PublisherManager, PushNotificationOnPremService.HubName, asyncCallback, asyncState));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003963 File Offset: 0x00001B63
		public void EndPublishNotifications(IAsyncResult result)
		{
			base.EndServiceCommand<ServiceCommandResultNone>(result);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000396D File Offset: 0x00001B6D
		public IAsyncResult BeginChallengeRequest(AzureChallengeRequestInfo issueSecret, AsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginServiceCommand(new IssueRegistrationChallenge(issueSecret, PushNotificationOnPremService.PublisherManager, PushNotificationOnPremService.HubName, asyncCallback, asyncState));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003987 File Offset: 0x00001B87
		public void EndChallengeRequest(IAsyncResult result)
		{
			base.EndServiceCommand<ServiceCommandResultNone>(result);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003991 File Offset: 0x00001B91
		public IAsyncResult BeginDeviceRegistration(AzureDeviceRegistrationInfo deviceRegistrationInfo, AsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginServiceCommand(new CreateDeviceRegistration(deviceRegistrationInfo, PushNotificationOnPremService.PublisherManager, PushNotificationOnPremService.HubName, asyncCallback, asyncState));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000039AB File Offset: 0x00001BAB
		public void EndDeviceRegistration(IAsyncResult result)
		{
			base.EndServiceCommand<ServiceCommandResultNone>(result);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000039B5 File Offset: 0x00001BB5
		internal override IStandardBudget AcquireBudget(IServiceCommand serviceCommand)
		{
			SecurityIdentifier user = OperationContext.Current.ServiceSecurityContext.WindowsIdentity.User;
			return StandardBudget.Acquire(ServiceBase.LocalSystemBudgetKey);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000039D8 File Offset: 0x00001BD8
		private static void RestartPushNotificationAppPool(object sender, ConfigurationChangedEventArgs config)
		{
			using (ServerManager serverManager = new ServerManager())
			{
				ApplicationPool applicationPool = serverManager.ApplicationPools["MSExchangePushNotificationsAppPool"];
				applicationPool.Recycle();
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003A20 File Offset: 0x00001C20
		private static void ConfigurationRead(object sender, ConfigurationReadEventArgs config)
		{
			ProxyNotification notification = new ProxyNotification(PushNotificationCannedApp.OnPremProxy.Name, config.Configuration.AzurePublisherSettings);
			PushNotificationOnPremService.PublisherManager.Publish(notification);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003A53 File Offset: 0x00001C53
		private static void DisposeStaticResources()
		{
			if (PushNotificationOnPremService.ConfigWatcher != null)
			{
				PushNotificationOnPremService.ConfigWatcher.OnChangeEvent -= PushNotificationOnPremService.RestartPushNotificationAppPool;
				PushNotificationOnPremService.ConfigWatcher.Dispose();
			}
			if (PushNotificationOnPremService.PublisherManager != null)
			{
				PushNotificationOnPremService.PublisherManager.Dispose();
			}
		}

		// Token: 0x04000047 RID: 71
		private static readonly PushNotificationPublisherManager PublisherManager;

		// Token: 0x04000048 RID: 72
		private static readonly PublisherConfigurationWatcher ConfigWatcher;

		// Token: 0x04000049 RID: 73
		private static readonly bool IsRunningLegacyMode;

		// Token: 0x0400004A RID: 74
		private static readonly string HubName;
	}
}
