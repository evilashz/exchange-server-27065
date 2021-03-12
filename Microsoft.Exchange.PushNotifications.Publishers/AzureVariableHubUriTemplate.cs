using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200005A RID: 90
	internal sealed class AzureVariableHubUriTemplate : AzureUriTemplate
	{
		// Token: 0x06000363 RID: 867 RVA: 0x0000B987 File Offset: 0x00009B87
		public AzureVariableHubUriTemplate(string uriTemplate) : base(uriTemplate)
		{
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000B990 File Offset: 0x00009B90
		public override string CreateSendNotificationStringUri(AzureNotification notification)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId), notification.HubName, "messages");
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000B9B3 File Offset: 0x00009BB3
		public override string CreateReadRegistrationStringUri(AzureNotification notification)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId), notification.HubName, base.CreateReadRegistrationAction(notification.RecipientId));
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000B9DD File Offset: 0x00009BDD
		public override string CreateReadRegistrationStringUri(AzureDeviceRegistrationNotification notification)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.TargetAppId), notification.HubName, base.CreateReadRegistrationAction(notification.RecipientId));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000BA07 File Offset: 0x00009C07
		public override string CreateNewRegistrationStringUri(string appId, string hubName)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(appId), hubName, "registrations");
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000BA20 File Offset: 0x00009C20
		public override string CreateNewRegistrationStringUri(AzureNotification notification)
		{
			return this.CreateNewRegistrationStringUri(notification.AppId, notification.HubName);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000BA34 File Offset: 0x00009C34
		public override string CreateNewRegistrationIdStringUri(AzureDeviceRegistrationNotification notification)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.TargetAppId), notification.HubName, "registrationIDs");
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000BA57 File Offset: 0x00009C57
		public override string CreateOrUpdateRegistrationStringUri(AzureDeviceRegistrationNotification notification, string registrationId)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.TargetAppId), notification.HubName, string.Format("{0}/{1}", "registrations", registrationId));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000BA85 File Offset: 0x00009C85
		public override string CreateOnPremResourceStringUri(string appId, string hubName)
		{
			return string.Format(base.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(appId), hubName, string.Empty);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000BB08 File Offset: 0x00009D08
		protected override void ValidateUriTemplate(string uriTemplate)
		{
			base.ValidateUriTemplate(uriTemplate);
			string formatedString = string.Format(uriTemplate, "ns", "hb", "ac");
			ArgumentValidator.ThrowIfInvalidValue<string>("uriTemplate-Uri", uriTemplate, (string x) => Uri.IsWellFormedUriString(formatedString, UriKind.Absolute));
			ArgumentValidator.ThrowIfInvalidValue<string>("uriTemplate-Order", uriTemplate, (string x) => formatedString.IndexOf("ns") < formatedString.IndexOf("hb") && formatedString.IndexOf("hb") < formatedString.IndexOf("ac"));
		}
	}
}
