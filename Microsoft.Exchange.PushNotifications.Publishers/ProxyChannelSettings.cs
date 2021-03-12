using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000C2 RID: 194
	internal sealed class ProxyChannelSettings : PushNotificationSettingsBase
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x00015007 File Offset: 0x00013207
		public ProxyChannelSettings(string appId, string serviceUri, string organization, DateTime? lastUpdated, int publishRetryMax, int publishRetryDelay, int publishStepTimeout, int backOffTime) : base(appId)
		{
			this.serviceUriString = serviceUri;
			this.LastUpdated = lastUpdated;
			this.Organization = organization;
			this.PublishRetryMax = publishRetryMax;
			this.PublishRetryDelay = publishRetryDelay;
			this.PublishStepTimeout = publishStepTimeout;
			this.BackOffTimeInSeconds = backOffTime;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00015046 File Offset: 0x00013246
		public Uri ServiceUri
		{
			get
			{
				if (!base.IsSuitable)
				{
					throw new InvalidOperationException("ServiceUri can only be accessed if the instance is suitable");
				}
				return this.serviceUri;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00015061 File Offset: 0x00013261
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x00015069 File Offset: 0x00013269
		public DateTime? LastUpdated { get; private set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00015072 File Offset: 0x00013272
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0001507A File Offset: 0x0001327A
		public string Organization { get; private set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00015083 File Offset: 0x00013283
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0001508B File Offset: 0x0001328B
		public int BackOffTimeInSeconds { get; private set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00015094 File Offset: 0x00013294
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x0001509C File Offset: 0x0001329C
		public int PublishRetryMax { get; private set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x000150A5 File Offset: 0x000132A5
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x000150AD File Offset: 0x000132AD
		public int PublishRetryDelay { get; private set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x000150B6 File Offset: 0x000132B6
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x000150BE File Offset: 0x000132BE
		public int PublishStepTimeout { get; private set; }

		// Token: 0x0600068E RID: 1678 RVA: 0x000150C8 File Offset: 0x000132C8
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			if (string.IsNullOrWhiteSpace(this.serviceUriString))
			{
				errors.Add(Strings.ValidationErrorEmptyString("ServiceUri"));
			}
			else
			{
				try
				{
					this.serviceUri = new Uri(this.serviceUriString, UriKind.Absolute);
				}
				catch (UriFormatException ex)
				{
					errors.Add(Strings.ValidationErrorInvalidUri("ServiceUri", this.serviceUriString, ex.Message));
				}
			}
			if (string.IsNullOrWhiteSpace(this.Organization))
			{
				errors.Add(Strings.ValidationErrorEmptyString("Organization"));
			}
			else if (!SmtpAddress.IsValidDomain(this.Organization))
			{
				errors.Add(DataStrings.InvalidSmtpDomainName(this.Organization));
			}
			if (this.PublishRetryMax < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("PublishRetryMax", this.PublishRetryMax));
			}
			if (this.PublishRetryDelay < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("PublishRetryDelay", this.PublishRetryDelay));
			}
			if (this.PublishStepTimeout < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("PublishStepTimeout", this.PublishStepTimeout));
			}
			if (this.BackOffTimeInSeconds < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("BackOffTimeInSeconds", this.BackOffTimeInSeconds));
			}
		}

		// Token: 0x0400033D RID: 829
		public const int DefaultPublishRetryMax = 3;

		// Token: 0x0400033E RID: 830
		public const int DefaultPublishRetryDelay = 1500;

		// Token: 0x0400033F RID: 831
		public const int DefaultBackOffTimeInSeconds = 600;

		// Token: 0x04000340 RID: 832
		public const int DefaultPublishStepTimeout = 500;

		// Token: 0x04000341 RID: 833
		private readonly string serviceUriString;

		// Token: 0x04000342 RID: 834
		private Uri serviceUri;
	}
}
