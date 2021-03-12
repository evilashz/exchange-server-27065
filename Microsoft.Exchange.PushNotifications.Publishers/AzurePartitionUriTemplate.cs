using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200004D RID: 77
	internal sealed class AzurePartitionUriTemplate : AzureUriTemplate
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000A751 File Offset: 0x00008951
		public AzurePartitionUriTemplate(string uriTemplate, string partitionName) : base(uriTemplate)
		{
			this.partitionName = partitionName;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000A764 File Offset: 0x00008964
		public override string CreateSendNotificationStringUri(AzureNotification notification)
		{
			return string.Format(base.UriTemplate, new object[]
			{
				AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId),
				this.partitionName,
				notification.HubName,
				"messages"
			});
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000A7AC File Offset: 0x000089AC
		public override string CreateReadRegistrationStringUri(AzureNotification notification)
		{
			return string.Format(base.UriTemplate, new object[]
			{
				AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId),
				this.partitionName,
				notification.HubName,
				base.CreateReadRegistrationAction(notification.RecipientId)
			});
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000A7FC File Offset: 0x000089FC
		public override string CreateReadRegistrationStringUri(AzureDeviceRegistrationNotification notification)
		{
			return string.Format(base.UriTemplate, new object[]
			{
				AzureUriTemplate.ConvertAppIdToValidNamespace(notification.TargetAppId),
				this.partitionName,
				notification.HubName,
				base.CreateReadRegistrationAction(notification.RecipientId)
			});
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000A84C File Offset: 0x00008A4C
		public override string CreateNewRegistrationStringUri(string appId, string hubName)
		{
			return string.Format(base.UriTemplate, new object[]
			{
				AzureUriTemplate.ConvertAppIdToValidNamespace(appId),
				this.partitionName,
				hubName,
				"registrations"
			});
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000A88A File Offset: 0x00008A8A
		public override string CreateNewRegistrationStringUri(AzureNotification notification)
		{
			return this.CreateNewRegistrationStringUri(notification.AppId, notification.HubName);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000A8A0 File Offset: 0x00008AA0
		public override string CreateNewRegistrationIdStringUri(AzureDeviceRegistrationNotification notification)
		{
			return string.Format(base.UriTemplate, new object[]
			{
				AzureUriTemplate.ConvertAppIdToValidNamespace(notification.TargetAppId),
				this.partitionName,
				notification.HubName,
				"registrationIDs"
			});
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		public override string CreateOrUpdateRegistrationStringUri(AzureDeviceRegistrationNotification notification, string registrationId)
		{
			return string.Format(base.UriTemplate, new object[]
			{
				AzureUriTemplate.ConvertAppIdToValidNamespace(notification.TargetAppId),
				this.partitionName,
				notification.HubName,
				string.Format("{0}/{1}", "registrations", registrationId)
			});
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000A93C File Offset: 0x00008B3C
		public override string CreateTargetHubCreationStringUri(AzureHubCreationNotification notification)
		{
			return string.Format(base.UriTemplate, new object[]
			{
				AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId),
				notification.Partition,
				notification.HubName,
				string.Empty
			});
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000A984 File Offset: 0x00008B84
		public override string CreateIssueRegistrationSecretStringUri(string targetAppId, string hubName)
		{
			return string.Format(base.UriTemplate, new object[]
			{
				AzureUriTemplate.ConvertAppIdToValidNamespace(targetAppId),
				this.partitionName,
				hubName,
				"issueregistrationsecret"
			});
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000A9C4 File Offset: 0x00008BC4
		public override string CreateOnPremResourceStringUri(string appId, string hubName)
		{
			return string.Format(base.UriTemplate, new object[]
			{
				AzureUriTemplate.ConvertAppIdToValidNamespace(appId),
				this.partitionName,
				hubName,
				string.Empty
			});
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000AA90 File Offset: 0x00008C90
		protected override void ValidateUriTemplate(string uriTemplate)
		{
			base.ValidateUriTemplate(uriTemplate);
			string formatedString = string.Format(uriTemplate, new object[]
			{
				"ns",
				"pn",
				"hb",
				"ac"
			});
			ArgumentValidator.ThrowIfInvalidValue<string>("uriTemplate-Uri", uriTemplate, (string x) => Uri.IsWellFormedUriString(formatedString, UriKind.Absolute));
			ArgumentValidator.ThrowIfInvalidValue<string>("uriTemplate-Order", uriTemplate, (string x) => formatedString.IndexOf("ns") < formatedString.IndexOf("pn") && formatedString.IndexOf("pn") < formatedString.IndexOf("hb") && formatedString.IndexOf("hb") < formatedString.IndexOf("ac"));
		}

		// Token: 0x0400013C RID: 316
		private readonly string partitionName;
	}
}
