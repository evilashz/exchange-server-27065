using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000134 RID: 308
	internal sealed class InstantMessageChat
	{
		// Token: 0x06000A1E RID: 2590 RVA: 0x00045D61 File Offset: 0x00043F61
		internal InstantMessageChat(string contentType, string message)
		{
			this.contentType = contentType;
			this.message = message;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00045D77 File Offset: 0x00043F77
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00045D7F File Offset: 0x00043F7F
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x04000786 RID: 1926
		private string contentType;

		// Token: 0x04000787 RID: 1927
		private string message;
	}
}
