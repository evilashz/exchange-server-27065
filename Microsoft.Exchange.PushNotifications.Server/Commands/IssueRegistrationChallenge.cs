using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x02000010 RID: 16
	internal class IssueRegistrationChallenge : PublishNotificationsBase<AzureChallengeRequestInfo>
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00002DC1 File Offset: 0x00000FC1
		public IssueRegistrationChallenge(AzureChallengeRequestInfo issueRegistrationInfo, PushNotificationPublisherManager publisherManager, AsyncCallback asyncCallback, object asyncState) : base(issueRegistrationInfo, publisherManager, asyncCallback, asyncState)
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002DCE File Offset: 0x00000FCE
		public IssueRegistrationChallenge(AzureChallengeRequestInfo issueRegistrationInfo, PushNotificationPublisherManager publisherManager, string hubName, AsyncCallback asyncCallback, object asyncState) : base(issueRegistrationInfo, publisherManager, asyncCallback, asyncState)
		{
			this.HubName = hubName;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002DE3 File Offset: 0x00000FE3
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002DEB File Offset: 0x00000FEB
		protected string HubName { get; set; }

		// Token: 0x06000071 RID: 113 RVA: 0x00002DF4 File Offset: 0x00000FF4
		protected override void Publish()
		{
			base.PublisherManager.Publish(base.RequestInstance, new PushNotificationPublishingContext(base.NotificationSource, OrganizationId.ForestWideOrgId, false, this.HubName));
		}
	}
}
