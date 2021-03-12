using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200015B RID: 347
	internal sealed class InfoMessage
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002BB4B File Offset: 0x00029D4B
		public Dictionary<string, string> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0002BB53 File Offset: 0x00029D53
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x0002BB5B File Offset: 0x00029D5B
		public byte[] Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002BB64 File Offset: 0x00029D64
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0002BB6C File Offset: 0x00029D6C
		public ContentType ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002BB75 File Offset: 0x00029D75
		public override string ToString()
		{
			if (this.body != null && this.body.Length > 0)
			{
				return Encoding.UTF8.GetString(this.body);
			}
			return string.Empty;
		}

		// Token: 0x0400094D RID: 2381
		private Dictionary<string, string> headers = new Dictionary<string, string>();

		// Token: 0x0400094E RID: 2382
		private ContentType contentType;

		// Token: 0x0400094F RID: 2383
		private byte[] body;

		// Token: 0x0200015C RID: 348
		internal class MessageReceivedEventArgs : EventArgs
		{
			// Token: 0x06000A4A RID: 2634 RVA: 0x0002BBB3 File Offset: 0x00029DB3
			public MessageReceivedEventArgs(InfoMessage message)
			{
				this.message = message;
			}

			// Token: 0x17000295 RID: 661
			// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0002BBC2 File Offset: 0x00029DC2
			public InfoMessage Message
			{
				get
				{
					return this.message;
				}
			}

			// Token: 0x04000950 RID: 2384
			private InfoMessage message;
		}

		// Token: 0x0200015D RID: 349
		internal sealed class PlatformMessageReceivedEventArgs : InfoMessage.MessageReceivedEventArgs
		{
			// Token: 0x06000A4C RID: 2636 RVA: 0x0002BBCA File Offset: 0x00029DCA
			public PlatformMessageReceivedEventArgs(PlatformCallInfo callInfo, InfoMessage message, bool isOptions) : base(message)
			{
				this.CallInfo = callInfo;
				this.IsOptions = isOptions;
			}

			// Token: 0x17000296 RID: 662
			// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002BBE1 File Offset: 0x00029DE1
			// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0002BBE9 File Offset: 0x00029DE9
			public PlatformCallInfo CallInfo { get; private set; }

			// Token: 0x17000297 RID: 663
			// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002BBF2 File Offset: 0x00029DF2
			// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0002BBFA File Offset: 0x00029DFA
			public bool IsOptions { get; private set; }

			// Token: 0x17000298 RID: 664
			// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0002BC03 File Offset: 0x00029E03
			// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0002BC0B File Offset: 0x00029E0B
			public int ResponseCode { get; set; }

			// Token: 0x17000299 RID: 665
			// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0002BC14 File Offset: 0x00029E14
			// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0002BC1C File Offset: 0x00029E1C
			public PlatformSipUri ResponseContactUri { get; set; }
		}
	}
}
