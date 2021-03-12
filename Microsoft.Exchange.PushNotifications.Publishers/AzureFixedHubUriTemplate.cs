using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000048 RID: 72
	internal sealed class AzureFixedHubUriTemplate : AzureUriTemplate
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000A0A3 File Offset: 0x000082A3
		public AzureFixedHubUriTemplate(string uriTemplate) : base(uriTemplate)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000A0AC File Offset: 0x000082AC
		public override string CreateSendNotificationStringUri(AzureNotification notification)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId), "messages");
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000A0C9 File Offset: 0x000082C9
		public override string CreateReadRegistrationStringUri(AzureNotification notification)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId), base.CreateReadRegistrationAction(notification.RecipientId));
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000A0ED File Offset: 0x000082ED
		public override string CreateReadRegistrationStringUri(AzureDeviceRegistrationNotification notification)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.TargetAppId), base.CreateReadRegistrationAction(notification.RecipientId));
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A111 File Offset: 0x00008311
		public override string CreateNewRegistrationStringUri(string appId, string hubName)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(appId), "registrations");
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000A129 File Offset: 0x00008329
		public override string CreateNewRegistrationStringUri(AzureNotification notification)
		{
			return this.CreateNewRegistrationStringUri(notification.AppId, notification.HubName);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000A13D File Offset: 0x0000833D
		public override string CreateIssueRegistrationSecretStringUri(string targetAppId, string hubName)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(targetAppId), "issueregistrationsecret");
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000A155 File Offset: 0x00008355
		public override string CreateNewRegistrationIdStringUri(AzureDeviceRegistrationNotification notification)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.TargetAppId), "registrationIDs");
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A172 File Offset: 0x00008372
		public override string CreateOrUpdateRegistrationStringUri(AzureDeviceRegistrationNotification notification, string registrationId)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.TargetAppId), string.Format("{0}/{1}", "registrations", registrationId));
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A1A8 File Offset: 0x000083A8
		public override string CreateOnPremResourceStringUri(string appId, string hubName)
		{
			ArgumentValidator.ThrowIfInvalidValue<string>("hubName", hubName, (string x) => base.UriTemplate.Contains(x));
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(appId), string.Empty);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A214 File Offset: 0x00008414
		protected override void ValidateUriTemplate(string uriTemplate)
		{
			base.ValidateUriTemplate(uriTemplate);
			string formatedString = string.Format(uriTemplate, "ns", "ac");
			ArgumentValidator.ThrowIfInvalidValue<string>("uriTemplate-Uri", uriTemplate, (string x) => Uri.IsWellFormedUriString(formatedString, UriKind.Absolute));
			ArgumentValidator.ThrowIfInvalidValue<string>("uriTemplate-Order", uriTemplate, (string x) => formatedString.IndexOf("ns") < formatedString.IndexOf("ac"));
		}
	}
}
