using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000031 RID: 49
	internal class ApnsNotification : PushNotification
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00007CCE File Offset: 0x00005ECE
		public ApnsNotification(string appId, OrganizationId tenantId, string deviceToken, int badge, DateTime lastSubscriptionUpdate) : this(appId, tenantId, deviceToken, new ApnsPayload(new ApnsPayloadBasicData(new int?(badge), null, null, 0), null, null), 0, lastSubscriptionUpdate)
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007CF4 File Offset: 0x00005EF4
		public ApnsNotification(string appId, OrganizationId tenantId, string deviceToken, ApnsPayload payload, int expiration, DateTime lastSubscriptionUpdate) : base(appId, tenantId)
		{
			this.DeviceToken = deviceToken;
			this.Payload = payload;
			this.Expiration = expiration;
			this.LastSubscriptionUpdate = lastSubscriptionUpdate;
			if (payload != null)
			{
				payload.NotificationId = base.Identifier;
				base.IsBackgroundSyncAvailable = !string.IsNullOrEmpty(payload.BackgroundSyncType);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00007D4E File Offset: 0x00005F4E
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00007D56 File Offset: 0x00005F56
		public string DeviceToken { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00007D5F File Offset: 0x00005F5F
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00007D67 File Offset: 0x00005F67
		public ApnsPayload Payload { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00007D70 File Offset: 0x00005F70
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00007D78 File Offset: 0x00005F78
		public int Expiration { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00007D81 File Offset: 0x00005F81
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00007D89 File Offset: 0x00005F89
		public DateTime LastSubscriptionUpdate { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00007D92 File Offset: 0x00005F92
		public override string RecipientId
		{
			get
			{
				return this.DeviceToken;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00007D9A File Offset: 0x00005F9A
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00007DA2 File Offset: 0x00005FA2
		public ExDateTime SentTime { get; set; }

		// Token: 0x060001FC RID: 508 RVA: 0x00007DAB File Offset: 0x00005FAB
		public byte[] ConvertToApnsBinaryFormat()
		{
			base.Validate();
			return this.toApnsBinaryFormat;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007DBC File Offset: 0x00005FBC
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if ((ExDateTime)this.LastSubscriptionUpdate > ExDateTime.UtcNow)
			{
				errors.Add(Strings.InvalidLastSubscriptionUpdate(this.LastSubscriptionUpdate.ToNullableString(null)));
			}
			int num = 0;
			byte[] array = null;
			byte[] array2 = null;
			byte[] array3 = null;
			byte[] array4 = null;
			byte[] array5 = null;
			byte b = 1;
			num++;
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(base.SequenceNumber));
			num += bytes.Length;
			if (this.Expiration < 0)
			{
				ExTraceGlobals.NotificationFormatTracer.TraceError((long)this.GetHashCode(), "[ApnsNotification::RunValidationCheck] Expiration should be set to a positive integer by now");
				errors.Add(Strings.InvalidExpiration(this.Expiration));
			}
			else
			{
				array = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(this.Expiration));
				num += array.Length;
			}
			if (string.IsNullOrEmpty(this.DeviceToken) || this.DeviceToken.Length / 2 != 32)
			{
				ExTraceGlobals.NotificationFormatTracer.TraceError<string>((long)this.GetHashCode(), "[ApnsNotification::RunValidationCheck] DeviceToken is not right: '{0}'", this.DeviceToken.ToNullableString());
				errors.Add(Strings.InvalidDeviceToken(this.DeviceToken.ToNullableString()));
			}
			else
			{
				try
				{
					array2 = HexConverter.HexStringToByteArray(this.DeviceToken);
					array3 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Convert.ToInt16(array2.Length)));
					num += array2.Length + array3.Length;
				}
				catch (FormatException)
				{
					ExTraceGlobals.NotificationFormatTracer.TraceError<string>((long)this.GetHashCode(), "[ApnsNotification::RunValidationCheck] DeviceToken is not right: '{0}'", this.DeviceToken);
					errors.Add(Strings.InvalidDeviceToken(this.DeviceToken));
				}
			}
			if (this.Payload == null || this.Payload.Aps == null)
			{
				ExTraceGlobals.NotificationFormatTracer.TraceError((long)this.GetHashCode(), "[ApnsNotification::RunValidationCheck] Payload and Payload.Aps should be set by now.");
				errors.Add(Strings.InvalidPayload);
			}
			else
			{
				try
				{
					string text = this.Payload.ToJson();
					array4 = Encoding.UTF8.GetBytes(text);
					if (array4.Length > 256)
					{
						ExTraceGlobals.NotificationFormatTracer.TraceError<string>((long)this.GetHashCode(), "[ApnsNotification::RunValidationCheck] Payload is too long: '{0}'", text);
						errors.Add(Strings.InvalidPayloadLength(array4.Length, text));
					}
					else
					{
						array5 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Convert.ToInt16(array4.Length)));
						num += array4.Length + array5.Length;
					}
				}
				catch (SerializationException exception)
				{
					string text2 = exception.ToTraceString();
					ExTraceGlobals.NotificationFormatTracer.TraceError<string>((long)this.GetHashCode(), "[ApnsNotification::RunValidationCheck] Unable to convert the payload to JSON: '{0}'.", text2);
					errors.Add(Strings.InvalidPayloadFormat(text2));
				}
			}
			if (errors.Count == 0)
			{
				this.toApnsBinaryFormat = new byte[num];
				this.toApnsBinaryFormat[0] = b;
				int num2 = 1;
				foreach (byte[] array7 in new byte[][]
				{
					bytes,
					array,
					array3,
					array2,
					array5,
					array4
				})
				{
					Buffer.BlockCopy(array7, 0, this.toApnsBinaryFormat, num2, array7.Length);
					num2 += array7.Length;
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000080B0 File Offset: 0x000062B0
		protected override string InternalToFullString()
		{
			return string.Format("{0}; token:{1}; payload:{2}; exp:{3}; lastSubscriptionUpdate:{4}", new object[]
			{
				base.InternalToFullString(),
				this.DeviceToken.ToNullableString(),
				this.Payload.ToNullableString(null),
				this.Expiration,
				this.LastSubscriptionUpdate.ToNullableString(null)
			});
		}

		// Token: 0x040000BE RID: 190
		public const int DeviceTokenBinaryLength = 32;

		// Token: 0x040000BF RID: 191
		private const int PayloadBinaryLengthMax = 256;

		// Token: 0x040000C0 RID: 192
		private byte[] toApnsBinaryFormat;

		// Token: 0x02000032 RID: 50
		private enum CommandType : byte
		{
			// Token: 0x040000C7 RID: 199
			SimpleFormat,
			// Token: 0x040000C8 RID: 200
			EnhancedFormat
		}
	}
}
