using System;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008E4 RID: 2276
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConversationBodyTextReader : BodyTextReader
	{
		// Token: 0x0600555C RID: 21852 RVA: 0x001621CB File Offset: 0x001603CB
		internal ConversationBodyTextReader(ICoreItem coreItem, BodyReadConfiguration configuration, Stream inputStream, long bytesLoadedForConversation, long maxBytesForConversation) : base(coreItem, configuration, inputStream)
		{
			this.bytesLoadedForConversation = bytesLoadedForConversation;
			this.maxBytes = maxBytesForConversation;
		}

		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x0600555D RID: 21853 RVA: 0x001621EE File Offset: 0x001603EE
		public long BytesRead
		{
			get
			{
				this.CheckDisposed();
				return this.bytesRead;
			}
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x001621FC File Offset: 0x001603FC
		public override int Read()
		{
			int num = base.Read();
			if (num != -1)
			{
				this.bytesRead += 2L;
				if (this.maxBytes > -1L && this.bytesRead + this.bytesLoadedForConversation > this.maxBytes)
				{
					throw new MessageLoadFailedInConversationException(new LocalizedString("Message body size exceeded the conversation threshold for loading"));
				}
			}
			return num;
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x00162254 File Offset: 0x00160454
		public override int Read(char[] buffer, int index, int count)
		{
			int num = base.Read(buffer, index, count);
			if (num > 0)
			{
				this.bytesRead += (long)(num * 2);
				if (this.maxBytes > -1L && this.bytesRead + this.bytesLoadedForConversation > this.maxBytes)
				{
					throw new MessageLoadFailedInConversationException(new LocalizedString("Message body size exceeded the conversation threshold for loading"));
				}
			}
			return num;
		}

		// Token: 0x06005560 RID: 21856 RVA: 0x001622B0 File Offset: 0x001604B0
		private void CheckDisposed()
		{
			if (base.IsDisposed())
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04002DD9 RID: 11737
		private readonly long bytesLoadedForConversation;

		// Token: 0x04002DDA RID: 11738
		private readonly long maxBytes = -1L;

		// Token: 0x04002DDB RID: 11739
		private long bytesRead;
	}
}
