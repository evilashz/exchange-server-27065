using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000082 RID: 130
	internal class AzureHubCreationNotification : PushNotification
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x0000F364 File Offset: 0x0000D564
		public AzureHubCreationNotification(string appId, string hubName, string partition, AzureHubPayload payload) : base(appId, OrganizationId.ForestWideOrgId)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("hubName", hubName);
			ArgumentValidator.ThrowIfNull("payload", payload);
			this.AzureNamespace = appId;
			this.HubName = hubName;
			this.Partition = partition;
			this.Payload = payload;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000F3B1 File Offset: 0x0000D5B1
		public override string RecipientId
		{
			get
			{
				return this.HubName;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000F3B9 File Offset: 0x0000D5B9
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0000F3C1 File Offset: 0x0000D5C1
		public string AzureNamespace { get; private set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000F3CA File Offset: 0x0000D5CA
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x0000F3D2 File Offset: 0x0000D5D2
		public AzureHubPayload Payload { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000F3DB File Offset: 0x0000D5DB
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0000F3E3 File Offset: 0x0000D5E3
		public string HubName { get; private set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		public string Partition { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000F3FD File Offset: 0x0000D5FD
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0000F405 File Offset: 0x0000D605
		public string SerializedPaylod { get; private set; }

		// Token: 0x06000495 RID: 1173 RVA: 0x0000F410 File Offset: 0x0000D610
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			this.SerializedPaylod = this.ToAzureHubCreationFormat();
			if (Encoding.UTF8.GetByteCount(this.SerializedPaylod) > 8192)
			{
				errors.Add(Strings.InvalidAzurePayloadLength(8192, this.SerializedPaylod));
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000F460 File Offset: 0x0000D660
		protected override string InternalToFullString()
		{
			return string.Format("{0}; hub:{1}; partition: {2}; payload:{3}", new object[]
			{
				base.InternalToFullString(),
				this.HubName,
				this.Partition,
				this.Payload.ToString()
			});
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		private string ToAzureHubCreationFormat()
		{
			AzureHubPayloadWriter azureHubPayloadWriter = new AzureHubPayloadWriter();
			if (this.Payload != null)
			{
				this.Payload.WriteAzureHubPayload(azureHubPayloadWriter);
			}
			return azureHubPayloadWriter.ToString();
		}

		// Token: 0x0400023C RID: 572
		private const int MaxPayloadSize = 8192;
	}
}
