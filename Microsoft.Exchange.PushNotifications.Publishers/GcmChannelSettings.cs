using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200009A RID: 154
	internal sealed class GcmChannelSettings : PushNotificationSettingsBase
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x00012437 File Offset: 0x00010637
		public GcmChannelSettings(string appId, string senderId, string senderAuthToken, bool isAuthTokenEncrypted, string serviceUri, int requestTimeout, int requestStepTimeout, int backOffTimeInSeconds) : base(appId)
		{
			this.SenderId = senderId;
			this.authTokenString = senderAuthToken;
			this.serviceUriString = serviceUri;
			this.isAuthTokenEncrypted = isAuthTokenEncrypted;
			this.RequestTimeout = requestTimeout;
			this.RequestStepTimeout = requestStepTimeout;
			this.BackOffTimeInSeconds = backOffTimeInSeconds;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00012476 File Offset: 0x00010676
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0001247E File Offset: 0x0001067E
		public string SenderId { get; private set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00012487 File Offset: 0x00010687
		public SecureString SenderAuthToken
		{
			get
			{
				if (!base.IsSuitable)
				{
					throw new InvalidOperationException("SenderAuthToken can only be accessed if the instance is suitable");
				}
				return this.authToken;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000124A2 File Offset: 0x000106A2
		public Uri ServiceUri
		{
			get
			{
				if (!base.IsValid)
				{
					throw new InvalidOperationException("ServiceUri can only be accessed if the instance is valid");
				}
				return this.serviceUri;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000124BD File Offset: 0x000106BD
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x000124C5 File Offset: 0x000106C5
		public int RequestTimeout { get; private set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000124CE File Offset: 0x000106CE
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x000124D6 File Offset: 0x000106D6
		public int RequestStepTimeout { get; private set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x000124DF File Offset: 0x000106DF
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x000124E7 File Offset: 0x000106E7
		public int BackOffTimeInSeconds { get; private set; }

		// Token: 0x06000579 RID: 1401 RVA: 0x000124F0 File Offset: 0x000106F0
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (string.IsNullOrWhiteSpace(this.SenderId))
			{
				errors.Add(Strings.ValidationErrorEmptyString("SenderId"));
			}
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
			if (this.RequestTimeout < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("RequestTimeout", this.RequestTimeout));
			}
			if (this.RequestStepTimeout < 0 || this.RequestStepTimeout > this.RequestTimeout)
			{
				errors.Add(Strings.ValidationErrorRangeInteger("RequestStepTimeout", 0, this.RequestTimeout, this.RequestStepTimeout));
			}
			if (this.BackOffTimeInSeconds < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("BackOffTimeInSeconds", this.BackOffTimeInSeconds));
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000125F8 File Offset: 0x000107F8
		protected override bool RunSuitabilityCheck()
		{
			bool result = base.RunSuitabilityCheck();
			try
			{
				this.authToken = (this.isAuthTokenEncrypted ? PushNotificationDataProtector.Default.Decrypt(this.authTokenString) : this.authTokenString.AsSecureString());
				if (this.authToken == null)
				{
					throw new PushNotificationConfigurationException(Strings.ValidationErrorEmptyString("SenderAuthToken"));
				}
			}
			catch (PushNotificationConfigurationException exception)
			{
				string text = exception.ToTraceString();
				PushNotificationsCrimsonEvents.PushNotificationPublisherConfigurationError.Log<string, string, string>(base.AppId, string.Empty, text);
				ExTraceGlobals.PublisherManagerTracer.TraceError<string, string>((long)this.GetHashCode(), "[GcmChannelSettings:RunSuitabilityCheck] Channel configuration for '{0}' has suitability errors: {1}", base.AppId, text);
				result = false;
			}
			return result;
		}

		// Token: 0x0400029E RID: 670
		public const string DefaultServiceUri = "https://android.googleapis.com/gcm/send";

		// Token: 0x0400029F RID: 671
		public const bool DefaultIsAuthTokenEncrypted = true;

		// Token: 0x040002A0 RID: 672
		public const int DefaultRequestTimeout = 60000;

		// Token: 0x040002A1 RID: 673
		public const int DefaultRequestStepTimeout = 500;

		// Token: 0x040002A2 RID: 674
		public const int DefaultBackOffTimeInSeconds = 600;

		// Token: 0x040002A3 RID: 675
		private readonly string serviceUriString;

		// Token: 0x040002A4 RID: 676
		private readonly string authTokenString;

		// Token: 0x040002A5 RID: 677
		private readonly bool isAuthTokenEncrypted;

		// Token: 0x040002A6 RID: 678
		private Uri serviceUri;

		// Token: 0x040002A7 RID: 679
		private SecureString authToken;
	}
}
