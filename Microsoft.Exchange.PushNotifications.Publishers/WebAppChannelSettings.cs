using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000CC RID: 204
	internal sealed class WebAppChannelSettings : PushNotificationSettingsBase
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x0001596C File Offset: 0x00013B6C
		public WebAppChannelSettings(string appId, int requestTimeout, int requestStepTimeout, int backOffTimeInSeconds) : base(appId)
		{
			this.RequestTimeout = requestTimeout;
			this.RequestStepTimeout = requestStepTimeout;
			this.BackOffTimeInSeconds = backOffTimeInSeconds;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001598B File Offset: 0x00013B8B
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x00015993 File Offset: 0x00013B93
		public int RequestTimeout { get; private set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001599C File Offset: 0x00013B9C
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x000159A4 File Offset: 0x00013BA4
		public int RequestStepTimeout { get; private set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x000159AD File Offset: 0x00013BAD
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x000159B5 File Offset: 0x00013BB5
		public int BackOffTimeInSeconds { get; private set; }

		// Token: 0x060006C9 RID: 1737 RVA: 0x000159C0 File Offset: 0x00013BC0
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
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

		// Token: 0x04000366 RID: 870
		public const int DefaultRequestTimeout = 60000;

		// Token: 0x04000367 RID: 871
		public const int DefaultRequestStepTimeout = 500;

		// Token: 0x04000368 RID: 872
		public const int DefaultBackOffTimeInSeconds = 300;
	}
}
