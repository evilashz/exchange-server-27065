using System;
using Microsoft.Exchange.Connections.Eas.Commands.Autodiscover;
using Microsoft.Exchange.Connections.Eas.Commands.Options;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Connect
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class ConnectResponse : IHaveAnHttpStatus
	{
		// Token: 0x060000FD RID: 253 RVA: 0x0000448D File Offset: 0x0000268D
		public ConnectResponse()
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004498 File Offset: 0x00002698
		public ConnectResponse(AutodiscoverResponse autodiscoverResponse, AutodiscoverOption autodiscoverOption)
		{
			this.AutodiscoverResponse = autodiscoverResponse;
			this.AutodiscoverOption = autodiscoverOption;
			this.HttpStatus = autodiscoverResponse.HttpStatus;
			this.ConnectStatus = ((autodiscoverResponse.HttpStatus == HttpStatus.OK && autodiscoverResponse.AutodiscoverStatus == AutodiscoverStatus.Success) ? ConnectStatus.Success : ConnectStatus.AutodiscoverFailed);
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000044E5 File Offset: 0x000026E5
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000044ED File Offset: 0x000026ED
		public HttpStatus HttpStatus { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000044F6 File Offset: 0x000026F6
		public string HttpStatusString
		{
			get
			{
				return this.HttpStatus.ToString();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004508 File Offset: 0x00002708
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00004510 File Offset: 0x00002710
		public ConnectStatus ConnectStatus { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000451C File Offset: 0x0000271C
		public string ConnectStatusString
		{
			get
			{
				if (this.ConnectStatus != ConnectStatus.AutodiscoverFailed)
				{
					return this.ConnectStatus.ToString();
				}
				return this.ConnectStatus.ToString() + ":" + this.AutodiscoverResponse.AutodiscoverSteps;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004568 File Offset: 0x00002768
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00004570 File Offset: 0x00002770
		public string UserSmtpAddressString { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004579 File Offset: 0x00002779
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00004581 File Offset: 0x00002781
		public AutodiscoverResponse AutodiscoverResponse { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000458A File Offset: 0x0000278A
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00004592 File Offset: 0x00002792
		public AutodiscoverOption AutodiscoverOption { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000459B File Offset: 0x0000279B
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000045A3 File Offset: 0x000027A3
		public OptionsResponse OptionsResponse { get; set; }
	}
}
