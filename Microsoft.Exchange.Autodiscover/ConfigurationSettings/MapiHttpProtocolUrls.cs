using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Net.MapiHttp;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x02000035 RID: 53
	internal class MapiHttpProtocolUrls
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000834C File Offset: 0x0000654C
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00008354 File Offset: 0x00006554
		public Uri InternalBaseUrl { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000835D File Offset: 0x0000655D
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00008365 File Offset: 0x00006565
		public Uri ExternalBaseUrl { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000836E File Offset: 0x0000656E
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00008376 File Offset: 0x00006576
		public string MailboxId { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000837F File Offset: 0x0000657F
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00008387 File Offset: 0x00006587
		public DateTime LastConfigurationTime { get; private set; }

		// Token: 0x06000182 RID: 386 RVA: 0x00008390 File Offset: 0x00006590
		public bool TryGetProtocolUrl(ClientAccessType clientAccessType, MapiHttpProtocolUrls.Protocol mapiHttpProtocol, out Uri protocolUrl)
		{
			Uri uri = (clientAccessType == ClientAccessType.Internal) ? this.InternalBaseUrl : this.ExternalBaseUrl;
			if (uri != null)
			{
				if (mapiHttpProtocol == MapiHttpProtocolUrls.Protocol.Emsmdb)
				{
					protocolUrl = new Uri(MapiHttpEndpoints.GetMailboxUrl(uri.Host, this.MailboxId));
					return true;
				}
				if (mapiHttpProtocol == MapiHttpProtocolUrls.Protocol.Nspi)
				{
					protocolUrl = new Uri(MapiHttpEndpoints.GetAddressBookUrl(uri.Host, this.MailboxId));
					return true;
				}
			}
			protocolUrl = null;
			return false;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000083FC File Offset: 0x000065FC
		public bool HasUrls
		{
			get
			{
				return this.InternalBaseUrl != null || this.ExternalBaseUrl != null;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000841A File Offset: 0x0000661A
		public MapiHttpProtocolUrls(Uri internalUrl, Uri externalUrl, string mailboxId, DateTime lastConfigurationTime)
		{
			if (string.IsNullOrEmpty(mailboxId))
			{
				throw new ArgumentException("mailboxId is empty or null.", "mailboxId");
			}
			this.InternalBaseUrl = internalUrl;
			this.ExternalBaseUrl = externalUrl;
			this.MailboxId = mailboxId;
			this.LastConfigurationTime = lastConfigurationTime;
		}

		// Token: 0x02000036 RID: 54
		public enum Protocol
		{
			// Token: 0x04000192 RID: 402
			Nspi,
			// Token: 0x04000193 RID: 403
			Emsmdb
		}
	}
}
