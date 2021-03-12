using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Security.Compliance;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000049 RID: 73
	internal class AzureNotification : PushNotification
	{
		// Token: 0x060002CB RID: 715 RVA: 0x0000A274 File Offset: 0x00008474
		public AzureNotification(string appId, string recipient, OrganizationId tenantId, AzurePayload payload, bool enableDeviceRegistration = false) : base(appId, tenantId)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("recipient", recipient);
			ArgumentValidator.ThrowIfNull("payload", payload);
			if (AzureNotification.IsLegacyRecipientId(recipient))
			{
				this.recipientId = AzureNotification.ComputeHashTag(recipient);
			}
			else
			{
				this.recipientId = recipient;
			}
			this.DeviceId = recipient;
			this.Payload = payload;
			this.IsRegistrationEnabled = enableDeviceRegistration;
			if (payload != null)
			{
				payload.NotificationId = base.Identifier;
				base.IsBackgroundSyncAvailable = payload.IsBackground;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public AzureNotification(string appId, string recipient, string hubName, AzurePayload payload, bool enableDeviceRegistration = false) : base(appId, OrganizationId.ForestWideOrgId)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("recipient", recipient);
			ArgumentValidator.ThrowIfNull("payload", payload);
			if (AzureNotification.IsLegacyRecipientId(recipient))
			{
				this.recipientId = AzureNotification.ComputeHashTag(recipient);
			}
			else
			{
				this.recipientId = recipient;
			}
			this.DeviceId = recipient;
			this.HubName = hubName;
			this.Payload = payload;
			this.IsRegistrationEnabled = enableDeviceRegistration;
			if (payload != null)
			{
				payload.NotificationId = base.Identifier;
				base.IsBackgroundSyncAvailable = payload.IsBackground;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A37D File Offset: 0x0000857D
		public override string RecipientId
		{
			get
			{
				return this.recipientId;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000A385 File Offset: 0x00008585
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000A38D File Offset: 0x0000858D
		public AzurePayload Payload { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000A396 File Offset: 0x00008596
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000A39E File Offset: 0x0000859E
		public string HubName { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000A3A7 File Offset: 0x000085A7
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000A3AF File Offset: 0x000085AF
		public string SerializedPaylod { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000A3B8 File Offset: 0x000085B8
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public bool IsRegistrationEnabled { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000A3C9 File Offset: 0x000085C9
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000A3D1 File Offset: 0x000085D1
		public string DeviceId { get; private set; }

		// Token: 0x060002D8 RID: 728 RVA: 0x0000A3DC File Offset: 0x000085DC
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			try
			{
				if (this.HubName == null)
				{
					this.HubName = base.TenantId.ToExternalDirectoryOrganizationId();
				}
			}
			catch (CannotResolveExternalDirectoryOrganizationIdException exception)
			{
				errors.Add(Strings.AzureCannotResolveExternalOrgId(exception.ToTraceString()));
			}
			if (string.IsNullOrWhiteSpace(this.recipientId))
			{
				errors.Add(Strings.AzureInvalidRecipientId(this.recipientId));
			}
			this.SerializedPaylod = this.ToAzureTemplateFormat();
			if (Encoding.UTF8.GetByteCount(this.SerializedPaylod) > 8192)
			{
				errors.Add(Strings.InvalidAzurePayloadLength(8192, this.SerializedPaylod));
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000A488 File Offset: 0x00008688
		protected override string InternalToFullString()
		{
			return string.Format("{0}; recipientId:{1}; hub:{2}; payload:{3};", new object[]
			{
				base.InternalToFullString(),
				this.RecipientId,
				this.HubName,
				this.Payload.ToString()
			});
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000A4D0 File Offset: 0x000086D0
		private static string ComputeHashTag(string recipientId)
		{
			string result;
			using (HMACSHA256Cng hmacsha256Cng = new HMACSHA256Cng(Encoding.UTF8.GetBytes("O7sfRRXL7dbltiobjozqaSO6qVSIm94OUJrlC5fsGGG=")))
			{
				result = HexConverter.ByteArrayToHexString(hmacsha256Cng.ComputeHash(Encoding.UTF8.GetBytes(recipientId)));
			}
			return result;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A528 File Offset: 0x00008728
		private static bool IsLegacyRecipientId(string recipientId)
		{
			return recipientId.Length >= 64;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000A538 File Offset: 0x00008738
		private string ToAzureTemplateFormat()
		{
			AzurePayloadWriter azurePayloadWriter = new AzurePayloadWriter();
			if (this.Payload != null)
			{
				this.Payload.WriteAzurePayload(azurePayloadWriter);
			}
			return azurePayloadWriter.ToString();
		}

		// Token: 0x0400012F RID: 303
		private const int MaxPayloadSize = 8192;

		// Token: 0x04000130 RID: 304
		private const int ApnsDeviceTokenSize = 64;

		// Token: 0x04000131 RID: 305
		private const string HashingKey = "O7sfRRXL7dbltiobjozqaSO6qVSIm94OUJrlC5fsGGG=";

		// Token: 0x04000132 RID: 306
		private readonly string recipientId;
	}
}
