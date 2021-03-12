using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200006F RID: 111
	internal class AzureDeviceRegistrationPayload
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0000D93B File Offset: 0x0000BB3B
		public AzureDeviceRegistrationPayload(string deviceId, string registrationTemplate, string tag)
		{
			this.DeviceId = deviceId;
			this.RegistrationTemplate = registrationTemplate;
			this.AzureTag = tag;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000D958 File Offset: 0x0000BB58
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000D960 File Offset: 0x0000BB60
		public string DeviceId { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000D969 File Offset: 0x0000BB69
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000D971 File Offset: 0x0000BB71
		public string RegistrationTemplate { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000D97A File Offset: 0x0000BB7A
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000D982 File Offset: 0x0000BB82
		public string AzureTag { get; private set; }

		// Token: 0x06000403 RID: 1027 RVA: 0x0000D98B File Offset: 0x0000BB8B
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Format("{{deviceId:{0}; tag:{1}; template:{2}}}", this.DeviceId, this.AzureTag, this.RegistrationTemplate);
			}
			return this.toString;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000D9BD File Offset: 0x0000BBBD
		internal void WriteAzureDeviceRegistrationPayload(AzureDeviceRegistrationPayloadWriter apw)
		{
			ArgumentValidator.ThrowIfNull("apw", apw);
			apw.AddRegistrationTemplate(this.RegistrationTemplate);
			apw.AddAzureTag(this.AzureTag);
			apw.AddDeviceId(this.DeviceId);
		}

		// Token: 0x040001D0 RID: 464
		private string toString;
	}
}
