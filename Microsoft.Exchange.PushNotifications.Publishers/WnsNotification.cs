using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000D8 RID: 216
	internal abstract class WnsNotification : PushNotification
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x00015FC0 File Offset: 0x000141C0
		static WnsNotification()
		{
			List<string> list = new List<string>
			{
				"localhost",
				"127.0.0.1"
			};
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
				list.Add(hostEntry.HostName);
				foreach (IPAddress ipaddress in hostEntry.AddressList)
				{
					list.Add(ipaddress.ToString());
				}
			}
			catch (SocketException)
			{
			}
			WnsNotification.LocalHostIds = list.ToArray();
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00016050 File Offset: 0x00014250
		public WnsNotification(string appId, OrganizationId tenantId, string deviceUri) : base(appId, tenantId)
		{
			this.DeviceUri = deviceUri;
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00016061 File Offset: 0x00014261
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x00016069 File Offset: 0x00014269
		public string DeviceUri { get; private set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00016072 File Offset: 0x00014272
		public override string RecipientId
		{
			get
			{
				return this.DeviceUri;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001607C File Offset: 0x0001427C
		internal WnsRequest CreateWnsRequest()
		{
			base.Validate();
			WnsRequest wnsRequest = new WnsRequest();
			wnsRequest.Uri = this.uriCache;
			wnsRequest.RequestStream = new MemoryStream(this.payloadBytesCache);
			this.PrepareWnsRequest(wnsRequest);
			return wnsRequest;
		}

		// Token: 0x06000707 RID: 1799
		protected abstract void PrepareWnsRequest(WnsRequest wnsRequest);

		// Token: 0x06000708 RID: 1800
		protected abstract string GetSerializedPayload(List<LocalizedString> errors);

		// Token: 0x06000709 RID: 1801 RVA: 0x000160BC File Offset: 0x000142BC
		protected override string InternalToFullString()
		{
			return string.Format("{0}; uri:{1}", base.InternalToFullString(), this.DeviceUri.ToNullableString());
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000160DC File Offset: 0x000142DC
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (string.IsNullOrWhiteSpace(this.DeviceUri))
			{
				errors.Add(Strings.InvalidWnsDeviceUri(this.DeviceUri.ToNullableString(), string.Empty));
			}
			else
			{
				try
				{
					this.uriCache = new Uri(this.DeviceUri, UriKind.Absolute);
					if (!this.uriCache.DnsSafeHost.EndsWith("notify.windows.com", StringComparison.OrdinalIgnoreCase) && !WnsNotification.LocalHostIds.Contains(this.uriCache.DnsSafeHost, StringComparer.OrdinalIgnoreCase))
					{
						errors.Add(Strings.InvalidWnsDeviceUri(this.DeviceUri, string.Empty));
					}
				}
				catch (UriFormatException exception)
				{
					errors.Add(Strings.InvalidWnsDeviceUri(this.DeviceUri, new LocalizedString(exception.ToTraceString())));
				}
			}
			string serializedPayload = this.GetSerializedPayload(errors);
			byte[] bytes = Encoding.UTF8.GetBytes(serializedPayload);
			if (bytes.Length > 5000)
			{
				errors.Add(Strings.InvalidWnsPayloadLength(5000, serializedPayload));
				return;
			}
			this.payloadBytesCache = bytes;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000161E8 File Offset: 0x000143E8
		protected void ValidateTimeToLive(int? timeToLive, List<LocalizedString> errors)
		{
			if (timeToLive != null && timeToLive.Value < 60)
			{
				errors.Add(Strings.InvalidWnsTimeToLive(timeToLive.Value));
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00016210 File Offset: 0x00014410
		protected void ValidateTemplate(WnsBinding binding, List<LocalizedString> errors)
		{
			int num = (binding.Texts != null) ? binding.Texts.Length : 0;
			int num2 = (binding.Images != null) ? binding.Images.Length : 0;
			if (binding.TemplateDescription.MaxNumOfTexts < num || binding.TemplateDescription.MaxNumOfImages < num2)
			{
				errors.Add(Strings.InvalidWnsTemplate(binding.ToString()));
			}
		}

		// Token: 0x04000387 RID: 903
		private const string WnsDeviceUriDomain = "notify.windows.com";

		// Token: 0x04000388 RID: 904
		private const int MaxPayloadSize = 5000;

		// Token: 0x04000389 RID: 905
		private const int MinTimeToLive = 60;

		// Token: 0x0400038A RID: 906
		private static readonly string[] LocalHostIds;

		// Token: 0x0400038B RID: 907
		private Uri uriCache;

		// Token: 0x0400038C RID: 908
		private byte[] payloadBytesCache;
	}
}
