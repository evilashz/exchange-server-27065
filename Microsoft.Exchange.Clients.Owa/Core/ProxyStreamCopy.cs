using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000218 RID: 536
	internal class ProxyStreamCopy
	{
		// Token: 0x06001222 RID: 4642 RVA: 0x0006E50C File Offset: 0x0006C70C
		internal ProxyStreamCopy(object source, Stream destination, StreamCopyMode requestedCopyMode)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Type type = source.GetType();
			if (type.IsSubclassOf(typeof(Stream)))
			{
				this.sourceType = ProxyStreamCopy.SourceType.Stream;
			}
			else
			{
				if (!(type == typeof(string)))
				{
					throw new ArgumentException("Source is of an invalid type");
				}
				this.sourceType = ProxyStreamCopy.SourceType.String;
			}
			if ((this.copyMode & (StreamCopyMode)4) != (StreamCopyMode)0 && this.sourceType == ProxyStreamCopy.SourceType.String)
			{
				throw new ArgumentException("Async read is not supported for string source");
			}
			this.source = source;
			this.destination = destination;
			this.copyMode = requestedCopyMode;
			this.completedSynchronously = true;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0006E5BD File Offset: 0x0006C7BD
		internal IAsyncResult BeginCopy(AsyncCallback callback, object extraData)
		{
			this.result = new OwaAsyncResult(callback, extraData);
			if (this.ProcessNextSyncBatch(ProxyStreamCopy.CopyStage.BeginRead, null))
			{
				this.completedSynchronously = true;
				this.result.CompleteRequest(this.completedSynchronously);
			}
			return this.result;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0006E5F8 File Offset: 0x0006C7F8
		internal int EndCopy(IAsyncResult result)
		{
			OwaAsyncResult owaAsyncResult = (OwaAsyncResult)result;
			if (owaAsyncResult.Exception != null)
			{
				throw owaAsyncResult.Exception;
			}
			return this.totalBytesCopied;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0006E624 File Offset: 0x0006C824
		private bool ProcessNextSyncBatch(ProxyStreamCopy.CopyStage stage, IAsyncResult result)
		{
			ProxyStreamCopy.CopyStage copyStage = stage;
			IAsyncResult asyncResult = result;
			Stream stream = this.source as Stream;
			Stream stream2 = this.destination;
			for (;;)
			{
				if (copyStage == ProxyStreamCopy.CopyStage.EndWrite)
				{
					stream2.EndWrite(asyncResult);
				}
				if (copyStage == ProxyStreamCopy.CopyStage.BeginRead || copyStage == ProxyStreamCopy.CopyStage.EndWrite)
				{
					if (this.sourceType == ProxyStreamCopy.SourceType.String)
					{
						this.bytesRead = this.ReadNextStringChunk();
					}
					else if ((this.copyMode & (StreamCopyMode)1) != (StreamCopyMode)0)
					{
						this.bytesRead = stream.Read(this.Buffer, 0, this.Buffer.Length);
						copyStage = ProxyStreamCopy.CopyStage.BeginWrite;
						asyncResult = null;
					}
					else
					{
						IAsyncResult asyncResult2 = stream.BeginRead(this.Buffer, 0, this.Buffer.Length, new AsyncCallback(ProxyStreamCopy.ReadCallback), this);
						if (!asyncResult2.CompletedSynchronously)
						{
							break;
						}
						copyStage = ProxyStreamCopy.CopyStage.EndRead;
						asyncResult = asyncResult2;
					}
				}
				if (copyStage == ProxyStreamCopy.CopyStage.EndRead)
				{
					this.bytesRead = stream.EndRead(asyncResult);
				}
				if (this.bytesRead <= 0)
				{
					return true;
				}
				this.totalBytesCopied += this.bytesRead;
				bool flag;
				if ((this.copyMode & (StreamCopyMode)2) != (StreamCopyMode)0)
				{
					stream2.Write(this.Buffer, 0, this.bytesRead);
					copyStage = ProxyStreamCopy.CopyStage.BeginRead;
					asyncResult = null;
					flag = true;
				}
				else
				{
					IAsyncResult asyncResult3 = stream2.BeginWrite(this.Buffer, 0, this.bytesRead, new AsyncCallback(ProxyStreamCopy.WriteCallback), this);
					if (!asyncResult3.CompletedSynchronously)
					{
						goto IL_158;
					}
					copyStage = ProxyStreamCopy.CopyStage.EndWrite;
					asyncResult = asyncResult3;
					flag = true;
				}
				if (!flag)
				{
					return true;
				}
			}
			this.completedSynchronously = false;
			return false;
			IL_158:
			this.completedSynchronously = false;
			return false;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0006E79C File Offset: 0x0006C99C
		private void InternalStageCallback(ProxyStreamCopy.CopyStage nextStage, IAsyncResult result)
		{
			bool flag = false;
			if (result.CompletedSynchronously)
			{
				return;
			}
			try
			{
				if (this.ProcessNextSyncBatch(nextStage, result))
				{
					flag = true;
				}
			}
			catch (Exception exception)
			{
				this.result.CompleteRequest(false, exception);
				return;
			}
			if (flag)
			{
				this.result.CompleteRequest(false);
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0006E7F4 File Offset: 0x0006C9F4
		public static void ReadCallback(IAsyncResult result)
		{
			ProxyStreamCopy proxyStreamCopy = (ProxyStreamCopy)result.AsyncState;
			proxyStreamCopy.InternalStageCallback(ProxyStreamCopy.CopyStage.EndRead, result);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0006E81C File Offset: 0x0006CA1C
		public static void WriteCallback(IAsyncResult result)
		{
			ProxyStreamCopy proxyStreamCopy = (ProxyStreamCopy)result.AsyncState;
			proxyStreamCopy.InternalStageCallback(ProxyStreamCopy.CopyStage.EndWrite, result);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0006E844 File Offset: 0x0006CA44
		private int ReadNextStringChunk()
		{
			string text = (string)this.source;
			if (this.totalCharsRead >= text.Length)
			{
				return 0;
			}
			int num = 4096;
			if (this.totalCharsRead + num > text.Length)
			{
				num = text.Length - this.totalCharsRead;
			}
			char[] chars = text.ToCharArray(this.totalCharsRead, num);
			int byteCount = this.Encoding.GetByteCount(chars, 0, num);
			if (byteCount > this.Buffer.Length)
			{
				this.buffer = new byte[byteCount];
			}
			int bytes = this.Encoding.GetBytes(chars, 0, num, this.Buffer, 0);
			this.totalCharsRead += num;
			return bytes;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0006E8EC File Offset: 0x0006CAEC
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x0006E907 File Offset: 0x0006CB07
		public Encoding Encoding
		{
			get
			{
				if (this.encoding == null)
				{
					this.encoding = Encoding.UTF8;
				}
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0006E910 File Offset: 0x0006CB10
		private byte[] Buffer
		{
			get
			{
				if (this.buffer == null)
				{
					this.buffer = new byte[4096];
				}
				return this.buffer;
			}
		}

		// Token: 0x04000C59 RID: 3161
		private const int BufferSize = 4096;

		// Token: 0x04000C5A RID: 3162
		private object source;

		// Token: 0x04000C5B RID: 3163
		private Stream destination;

		// Token: 0x04000C5C RID: 3164
		private ProxyStreamCopy.SourceType sourceType;

		// Token: 0x04000C5D RID: 3165
		private OwaAsyncResult result;

		// Token: 0x04000C5E RID: 3166
		private int bytesRead;

		// Token: 0x04000C5F RID: 3167
		private int totalBytesCopied;

		// Token: 0x04000C60 RID: 3168
		private bool completedSynchronously;

		// Token: 0x04000C61 RID: 3169
		private StreamCopyMode copyMode;

		// Token: 0x04000C62 RID: 3170
		private byte[] buffer;

		// Token: 0x04000C63 RID: 3171
		private Encoding encoding;

		// Token: 0x04000C64 RID: 3172
		private int totalCharsRead;

		// Token: 0x02000219 RID: 537
		private enum SourceType
		{
			// Token: 0x04000C66 RID: 3174
			Stream,
			// Token: 0x04000C67 RID: 3175
			String
		}

		// Token: 0x0200021A RID: 538
		[Flags]
		private enum CopyStage
		{
			// Token: 0x04000C69 RID: 3177
			Read = 1,
			// Token: 0x04000C6A RID: 3178
			Write = 2,
			// Token: 0x04000C6B RID: 3179
			Begin = 256,
			// Token: 0x04000C6C RID: 3180
			End = 512,
			// Token: 0x04000C6D RID: 3181
			BeginRead = 257,
			// Token: 0x04000C6E RID: 3182
			EndRead = 513,
			// Token: 0x04000C6F RID: 3183
			BeginWrite = 258,
			// Token: 0x04000C70 RID: 3184
			EndWrite = 514
		}
	}
}
