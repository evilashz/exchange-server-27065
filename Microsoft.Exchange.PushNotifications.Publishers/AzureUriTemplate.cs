using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000047 RID: 71
	internal abstract class AzureUriTemplate
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00009F90 File Offset: 0x00008190
		protected AzureUriTemplate(string uriTemplate)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("uriTemplate", uriTemplate);
			this.ValidateUriTemplate(uriTemplate);
			this.UriTemplate = uriTemplate;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00009FB1 File Offset: 0x000081B1
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00009FB9 File Offset: 0x000081B9
		public string UriTemplate { get; private set; }

		// Token: 0x060002AF RID: 687 RVA: 0x00009FC2 File Offset: 0x000081C2
		public static AzureUriTemplate CreateUriTemplate(string uriTemplate, string additionalParameters = null)
		{
			if (uriTemplate != null)
			{
				if (uriTemplate.Contains("{3}"))
				{
					return new AzurePartitionUriTemplate(uriTemplate, additionalParameters);
				}
				if (uriTemplate.Contains("{2}"))
				{
					return new AzureVariableHubUriTemplate(uriTemplate);
				}
			}
			return new AzureFixedHubUriTemplate(uriTemplate);
		}

		// Token: 0x060002B0 RID: 688
		public abstract string CreateSendNotificationStringUri(AzureNotification notification);

		// Token: 0x060002B1 RID: 689
		public abstract string CreateReadRegistrationStringUri(AzureNotification notification);

		// Token: 0x060002B2 RID: 690
		public abstract string CreateReadRegistrationStringUri(AzureDeviceRegistrationNotification notification);

		// Token: 0x060002B3 RID: 691
		public abstract string CreateNewRegistrationStringUri(string appId, string hubName);

		// Token: 0x060002B4 RID: 692
		public abstract string CreateNewRegistrationStringUri(AzureNotification notification);

		// Token: 0x060002B5 RID: 693
		public abstract string CreateOrUpdateRegistrationStringUri(AzureDeviceRegistrationNotification notification, string registrationId);

		// Token: 0x060002B6 RID: 694
		public abstract string CreateNewRegistrationIdStringUri(AzureDeviceRegistrationNotification notification);

		// Token: 0x060002B7 RID: 695 RVA: 0x00009FF6 File Offset: 0x000081F6
		public virtual string CreateIssueRegistrationSecretStringUri(string targetAppId, string hubName)
		{
			return string.Format(this.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(targetAppId), hubName, "issueregistrationsecret");
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000A00F File Offset: 0x0000820F
		public virtual string CreateTargetHubCreationStringUri(AzureHubCreationNotification notification)
		{
			return string.Format(this.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId), notification.HubName);
		}

		// Token: 0x060002B9 RID: 697
		public abstract string CreateOnPremResourceStringUri(string appId, string hubName);

		// Token: 0x060002BA RID: 698 RVA: 0x0000A02D File Offset: 0x0000822D
		public override string ToString()
		{
			return this.UriTemplate;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000A035 File Offset: 0x00008235
		internal static string ConvertAppIdToValidNamespace(string appId)
		{
			if (appId.Contains("."))
			{
				return appId.Replace('.', '-');
			}
			return appId;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000A06C File Offset: 0x0000826C
		protected virtual void ValidateUriTemplate(string uriTemplate)
		{
			ArgumentValidator.ThrowIfInvalidValue<string>("uriTemplate-Format", uriTemplate, (string x) => x.Contains("{0}") && x.Contains("{1}"));
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A096 File Offset: 0x00008296
		protected string CreateReadRegistrationAction(string azureTag)
		{
			return string.Format("tags/{0}/registrations", azureTag);
		}

		// Token: 0x04000128 RID: 296
		protected const string SendingAction = "messages";

		// Token: 0x04000129 RID: 297
		protected const string ReadRegisteringActionTemplate = "tags/{0}/registrations";

		// Token: 0x0400012A RID: 298
		protected const string NewRegisteringActionTemplate = "registrations";

		// Token: 0x0400012B RID: 299
		protected const string NewRegistrationIdActionTemplate = "registrationIDs";

		// Token: 0x0400012C RID: 300
		protected const string IssueRegistrationSecretTemplate = "issueregistrationsecret";
	}
}
