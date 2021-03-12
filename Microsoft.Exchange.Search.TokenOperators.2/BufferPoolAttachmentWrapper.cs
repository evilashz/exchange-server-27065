using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000006 RID: 6
	internal class BufferPoolAttachmentWrapper : IAttachment, IDisposable
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00003320 File Offset: 0x00001520
		public Stream GetContentStream()
		{
			return this.lohFriendlyStream.GetReference();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000332D File Offset: 0x0000152D
		public void Dispose()
		{
			if (this.lohFriendlyStream != null)
			{
				this.lohFriendlyStream.Dispose();
				this.lohFriendlyStream = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003350 File Offset: 0x00001550
		protected void InitAttachmentStream(Stream attachmentStream)
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				LohFriendlyStream lohFriendlyStream = attachmentStream as LohFriendlyStream;
				if (lohFriendlyStream != null)
				{
					this.lohFriendlyStream = lohFriendlyStream.GetReference();
				}
				else if (attachmentStream == null)
				{
					this.lohFriendlyStream = new LohFriendlyStream(0);
				}
				else
				{
					this.lohFriendlyStream = new LohFriendlyStream((int)attachmentStream.Length);
					disposeGuard.Add<LohFriendlyStream>(this.lohFriendlyStream);
					this.CopyStreamContents(attachmentStream, this.lohFriendlyStream);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000033E4 File Offset: 0x000015E4
		private void CopyStreamContents(Stream sourceStream, Stream targetStream)
		{
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(BufferPoolCollection.BufferSize.Size16K);
			byte[] array = bufferPool.Acquire();
			try
			{
				for (;;)
				{
					int num = sourceStream.Read(array, 0, array.Length);
					if (num <= 0)
					{
						break;
					}
					targetStream.Write(array, 0, num);
				}
			}
			finally
			{
				if (bufferPool != null)
				{
					bufferPool.Release(array);
				}
				targetStream.Position = 0L;
			}
		}

		// Token: 0x0400001B RID: 27
		private LohFriendlyStream lohFriendlyStream;
	}
}
