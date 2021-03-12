using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Extensions;
using Microsoft.Exchange.PushNotifications.Publishers;
using Microsoft.Exchange.PushNotifications.Server.Commands;
using Microsoft.Exchange.PushNotifications.Server.Core;
using Microsoft.Exchange.PushNotifications.Server.Wcf;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x0200001F RID: 31
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	[PushNotificationServiceBehavior]
	internal class PushNotificationService : ServiceBase, IAzureAppConfigDataServiceContract, IAzureHubCreationServiceContract, IAzureChallengeRequestServiceContract, IAzureDeviceRegistrationServiceContract, IPublisherServiceContract, IOnPremPublisherServiceContract, IOutlookPublisherServiceContract, ILocalUserNotificationPublisherServiceContract, IRemoteUserNotificationPublisherServiceContract
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00003A9C File Offset: 0x00001C9C
		static PushNotificationService()
		{
			int maxThreadCount = 10 * Environment.ProcessorCount;
			Microsoft.Exchange.WorkloadManagement.UserWorkloadManager.Initialize(maxThreadCount, 500, 10, TimeSpan.FromMinutes(5.0), null);
			if (ActivityContextLogConfig.IsActivityContextLogEnabled())
			{
				ActivityContextLogger.Initialize();
			}
			ADObjectId adobjectId = PushNotificationServiceBudgetKey.ResolveServiceThrottlingPolicyId();
			if (adobjectId != null)
			{
				PushNotificationService.ServiceBudgetKey = new PushNotificationServiceBudgetKey(adobjectId);
			}
			else
			{
				PushNotificationService.ServiceBudgetKey = ServiceBase.LocalSystemBudgetKey;
			}
			PushNotificationService.ConfigWatcher = new PublisherConfigurationWatcher("MSExchangePushNotificationsAppPool", ServiceConfig.ConfigurationRefreshRateInMinutes);
			PushNotificationService.ConfigWatcher.OnChangeEvent += PushNotificationService.RestartPushNotificationAppPool;
			PushNotificationService.Configuration = PushNotificationService.ConfigWatcher.Start();
			PushNotificationPublisherManagerBuilder pushNotificationPublisherManagerBuilder = new PushNotificationPublisherManagerBuilder(new List<PushNotificationPlatform>
			{
				PushNotificationPlatform.APNS,
				PushNotificationPlatform.PendingGet,
				PushNotificationPlatform.WNS,
				PushNotificationPlatform.GCM,
				PushNotificationPlatform.WebApp,
				PushNotificationPlatform.Azure,
				PushNotificationPlatform.AzureHubCreation,
				PushNotificationPlatform.AzureChallengeRequest,
				PushNotificationPlatform.AzureDeviceRegistration
			});
			PushNotificationService.PublisherManager = pushNotificationPublisherManagerBuilder.Build(PushNotificationService.Configuration, DeviceThrottlingManager.Default, new AzureHubEventHandler());
			AppDomain.CurrentDomain.DomainUnload += delegate(object sender, EventArgs e)
			{
				PushNotificationService.DisposeStaticResources();
			};
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003C06 File Offset: 0x00001E06
		public PushNotificationService() : base(Microsoft.Exchange.WorkloadManagement.UserWorkloadManager.Singleton)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003C13 File Offset: 0x00001E13
		[PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "LocalAdministrators")]
		[PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "UserService")]
		public IAsyncResult BeginPublishNotifications(MailboxNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginServiceCommand(new PublishNotifications(notifications, PushNotificationService.PublisherManager, asyncCallback, asyncState));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003C28 File Offset: 0x00001E28
		public void EndPublishNotifications(IAsyncResult result)
		{
			base.EndServiceCommand<ServiceCommandResultNone>(result);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003C32 File Offset: 0x00001E32
		[PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "LocalAdministrators")]
		[PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "UserService")]
		public IAsyncResult BeginPublishOutlookNotifications(OutlookNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginServiceCommand(new PublishOutlookNotifications(notifications, PushNotificationService.PublisherManager, asyncCallback, asyncState));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003C47 File Offset: 0x00001E47
		public void EndPublishOutlookNotifications(IAsyncResult result)
		{
			base.EndServiceCommand<ServiceCommandResultNone>(result);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003C51 File Offset: 0x00001E51
		public IAsyncResult BeginPublishOnPremNotifications(MailboxNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginServiceCommand(new PublishProxyNotifications(notifications, PushNotificationService.PublisherManager, asyncCallback, asyncState));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003C66 File Offset: 0x00001E66
		public void EndPublishOnPremNotifications(IAsyncResult result)
		{
			base.EndServiceCommand<ServiceCommandResultNone>(result);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003C70 File Offset: 0x00001E70
		[PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "LocalAdministrators")]
		[PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "UserService")]
		public IAsyncResult BeginPublishUserNotifications(LocalUserNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginServiceCommand(new PublishLocalUserNotifications(notifications, PushNotificationService.PublisherManager, asyncCallback, asyncState));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003C85 File Offset: 0x00001E85
		public void EndPublishUserNotifications(IAsyncResult result)
		{
			base.EndServiceCommand<ServiceCommandResultNone>(result);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003C8F File Offset: 0x00001E8F
		public IAsyncResult BeginPublishUserNotification(RemoteUserNotification notification, AsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginServiceCommand(new PublishUserNotification(notification, PushNotificationService.PublisherManager, asyncCallback, asyncState));
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public void EndPublishUserNotification(IAsyncResult result)
		{
			base.EndServiceCommand<ServiceCommandResultNone>(result);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003CAE File Offset: 0x00001EAE
		public IAsyncResult BeginCreateHub(AzureHubDefinition hubDefinition, AsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginServiceCommand(new CreateAzureHub(hubDefinition, PushNotificationService.PublisherManager, asyncCallback, asyncState));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003CC3 File Offset: 0x00001EC3
		public void EndCreateHub(IAsyncResult result)
		{
			base.EndServiceCommand<ServiceCommandResultNone>(result);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003CCD File Offset: 0x00001ECD
		public IAsyncResult BeginChallengeRequest(AzureChallengeRequestInfo issueSecret, AsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginServiceCommand(new IssueRegistrationChallenge(issueSecret, PushNotificationService.PublisherManager, asyncCallback, asyncState));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003CE2 File Offset: 0x00001EE2
		public void EndChallengeRequest(IAsyncResult result)
		{
			base.EndServiceCommand<ServiceCommandResultNone>(result);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003CEC File Offset: 0x00001EEC
		public IAsyncResult BeginDeviceRegistration(AzureDeviceRegistrationInfo deviceRegistrationInfo, AsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginServiceCommand(new CreateDeviceRegistration(deviceRegistrationInfo, PushNotificationService.PublisherManager, asyncCallback, asyncState));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003D01 File Offset: 0x00001F01
		public void EndDeviceRegistration(IAsyncResult result)
		{
			base.EndServiceCommand<ServiceCommandResultNone>(result);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003D0B File Offset: 0x00001F0B
		public IAsyncResult BeginGetAppConfigData(AzureAppConfigRequestInfo requestConfig, AsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginServiceCommand(new GetAppConfigData(requestConfig, PushNotificationService.PublisherManager, PushNotificationService.Configuration, asyncCallback, asyncState));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003D25 File Offset: 0x00001F25
		public AzureAppConfigResponseInfo EndGetAppConfigData(IAsyncResult result)
		{
			return base.EndServiceCommand<AzureAppConfigResponseInfo>(result);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003D30 File Offset: 0x00001F30
		internal override IStandardBudget AcquireBudget(IServiceCommand serviceCommand)
		{
			SecurityIdentifier user = OperationContext.Current.ServiceSecurityContext.WindowsIdentity.User;
			if (user == null)
			{
				OAuthIdentity oauthIdentity = OperationContext.Current.GetOAuthIdentity();
				if (oauthIdentity != null)
				{
					return StandardBudget.Acquire(new TenantBudgetKey(oauthIdentity.OrganizationId, BudgetType.PushNotificationTenant));
				}
				WindowsIdentity windowsIdentity = OperationContext.Current.GetWindowsIdentity();
				if (windowsIdentity == null)
				{
					base.ThrowServiceBusyException(serviceCommand.Description, new FailedToAcquireBudgetException(OperationContext.Current.ServiceSecurityContext.WindowsIdentity.Name, OperationContext.Current.GetPrincipal().ToNullableString(null)));
				}
				user = windowsIdentity.User;
			}
			if (!user.IsWellKnown(WellKnownSidType.LocalSystemSid))
			{
				return StandardBudget.Acquire(user, BudgetType.PushNotificationTenant, ADSessionSettings.FromRootOrgScopeSet());
			}
			return StandardBudget.Acquire(PushNotificationService.ServiceBudgetKey);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003DE8 File Offset: 0x00001FE8
		private static void RestartPushNotificationAppPool(object sender, ConfigurationChangedEventArgs config)
		{
			using (ServerManager serverManager = new ServerManager())
			{
				ApplicationPool applicationPool = serverManager.ApplicationPools["MSExchangePushNotificationsAppPool"];
				applicationPool.Recycle();
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003E30 File Offset: 0x00002030
		private static void DisposeStaticResources()
		{
			if (PushNotificationService.ConfigWatcher != null)
			{
				PushNotificationService.ConfigWatcher.OnChangeEvent -= PushNotificationService.RestartPushNotificationAppPool;
				PushNotificationService.ConfigWatcher.Dispose();
			}
			if (PushNotificationService.PublisherManager != null)
			{
				PushNotificationService.PublisherManager.Dispose();
			}
			PushNotificationService.ServerVersionTimer.Dispose();
		}

		// Token: 0x0400004D RID: 77
		internal static readonly LookupBudgetKey ServiceBudgetKey;

		// Token: 0x0400004E RID: 78
		private static readonly PushNotificationPublisherManager PublisherManager;

		// Token: 0x0400004F RID: 79
		private static readonly PublisherConfigurationWatcher ConfigWatcher;

		// Token: 0x04000050 RID: 80
		private static readonly PushNotificationPublisherConfiguration Configuration;

		// Token: 0x04000051 RID: 81
		private static readonly Timer ServerVersionTimer = new Timer(delegate(object _state)
		{
			PushNotificationsLogHelper.LogServerVersion();
		}, null, TimeSpan.Zero, TimeSpan.FromHours(6.0));
	}
}
