using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200007B RID: 123
	internal sealed class AcsUriTemplate
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x0000E418 File Offset: 0x0000C618
		public AcsUriTemplate(string uriTemplate, string scopeTemplate)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("uriTemplate", uriTemplate);
			ArgumentValidator.ThrowIfInvalidValue<string>("uriTemplate-Format", uriTemplate, (string x) => x.Contains("{0}"));
			if (uriTemplate.Contains("{1}"))
			{
				this.isPartitionTemplate = true;
			}
			if (this.isPartitionTemplate)
			{
				ArgumentValidator.ThrowIfInvalidValue<string>("uriTemplate-Uri", uriTemplate, (string x) => Uri.IsWellFormedUriString(string.Format(x, "ns", "pn"), UriKind.Absolute));
				ArgumentValidator.ThrowIfNullOrWhiteSpace("scopeTemplate", scopeTemplate);
				ArgumentValidator.ThrowIfInvalidValue<string>("scopeTemplate-Format", scopeTemplate, (string x) => x.Contains("{0}") && x.Contains("{1}"));
				ArgumentValidator.ThrowIfInvalidValue<string>("scopeTemplate-Uri", scopeTemplate, (string x) => Uri.IsWellFormedUriString(string.Format(x, "ns", "pn"), UriKind.Absolute));
			}
			else
			{
				ArgumentValidator.ThrowIfInvalidValue<string>("uriTemplate-Uri", uriTemplate, (string x) => Uri.IsWellFormedUriString(string.Format(x, "ns"), UriKind.Absolute));
				ArgumentValidator.ThrowIfNullOrWhiteSpace("scopeTemplate", scopeTemplate);
				ArgumentValidator.ThrowIfInvalidValue<string>("scopeTemplate-Format", scopeTemplate, (string x) => x.Contains("{0}"));
				ArgumentValidator.ThrowIfInvalidValue<string>("scopeTemplate-Uri", scopeTemplate, (string x) => Uri.IsWellFormedUriString(string.Format(x, "ns"), UriKind.Absolute));
			}
			this.UriTemplate = uriTemplate;
			this.ScopeTemplate = scopeTemplate;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000E596 File Offset: 0x0000C796
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000E59E File Offset: 0x0000C79E
		public string UriTemplate { get; private set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000E5A7 File Offset: 0x0000C7A7
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0000E5AF File Offset: 0x0000C7AF
		public string ScopeTemplate { get; private set; }

		// Token: 0x06000452 RID: 1106 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		public Uri CreateAcsTokenRequestUri(AzureHubCreationNotification notification)
		{
			if (this.isPartitionTemplate)
			{
				return new Uri(new Uri(string.Format(this.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId), notification.Partition)), "WRAPv0.9");
			}
			return new Uri(new Uri(string.Format(this.UriTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId))), "WRAPv0.9");
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000E61E File Offset: 0x0000C81E
		public string CreateScopeUriString(AzureHubCreationNotification notification)
		{
			if (this.isPartitionTemplate)
			{
				return string.Format(this.ScopeTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId), notification.Partition);
			}
			return string.Format(this.ScopeTemplate, AzureUriTemplate.ConvertAppIdToValidNamespace(notification.AppId));
		}

		// Token: 0x04000208 RID: 520
		public const string WrapResourceName = "WRAPv0.9";

		// Token: 0x04000209 RID: 521
		private readonly bool isPartitionTemplate;
	}
}
