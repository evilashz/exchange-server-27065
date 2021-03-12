using System;
using System.IO;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000004 RID: 4
	public class SubStream : StreamWrapper
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000262C File Offset: 0x0000082C
		public SubStream(Stream sourceStream, long offset, long length)
		{
			if (sourceStream == null)
			{
				throw new ArgumentNullException("sourceStream is null");
			}
			if (sourceStream.Length < offset + length)
			{
				throw new ArgumentException(string.Format("The substream points beyond {0} + {1} the real stream length {2}.", offset, length, sourceStream.Length));
			}
			sourceStream.Seek(offset, SeekOrigin.Begin);
			byte[] array = SubStream.bufferPool.Acquire();
			try
			{
				do
				{
					int count = (int)Math.Min((long)array.Length, length);
					sourceStream.Read(array, 0, count);
					this.Write(array, 0, count);
					length -= (long)array.Length;
				}
				while (length > 0L);
				this.Seek(0L, SeekOrigin.Begin);
			}
			finally
			{
				if (array != null)
				{
					SubStream.bufferPool.Release(array);
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026EC File Offset: 0x000008EC
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x04000003 RID: 3
		private static BufferPool bufferPool = new BufferPool(4096);
	}
}
