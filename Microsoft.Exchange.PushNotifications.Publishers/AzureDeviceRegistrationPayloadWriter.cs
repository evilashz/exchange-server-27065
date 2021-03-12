using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000070 RID: 112
	internal class AzureDeviceRegistrationPayloadWriter
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000D9F6 File Offset: 0x0000BBF6
		public void AddDeviceId(string deviceId)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("deviceId", deviceId);
			this.deviceId = deviceId;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000DA0A File Offset: 0x0000BC0A
		public void AddAzureTag(string tag)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("tag", tag);
			this.azureTag = tag;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000DA1E File Offset: 0x0000BC1E
		public void AddRegistrationTemplate(string registrationTemplate)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("registrationTemplate", registrationTemplate);
			this.registrationTemplate = registrationTemplate;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000DA32 File Offset: 0x0000BC32
		public override string ToString()
		{
			if (!string.IsNullOrWhiteSpace(this.registrationTemplate))
			{
				return string.Format(this.registrationTemplate, this.azureTag, this.deviceId);
			}
			return string.Empty;
		}

		// Token: 0x040001D4 RID: 468
		private const string RequestBodyTemplate = "{{\"Channel\":\"{0}\",\"ApplicationPlatform\":\"{1}\",\"DeviceChallenge\":\"{2}\"}}";

		// Token: 0x040001D5 RID: 469
		private string azureTag;

		// Token: 0x040001D6 RID: 470
		private string deviceId;

		// Token: 0x040001D7 RID: 471
		private string registrationTemplate;
	}
}
