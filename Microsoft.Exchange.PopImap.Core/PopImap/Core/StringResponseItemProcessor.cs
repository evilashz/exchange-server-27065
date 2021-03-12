using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200003A RID: 58
	internal class StringResponseItemProcessor : IDisposeTrackable, IDisposable
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x00011230 File Offset: 0x0000F430
		public StringResponseItemProcessor()
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.bufferResponseItem = new BufferResponseItem();
			this.pooledMemoryStream = new PooledMemoryStream(1024);
			this.streamWriter = new StreamWriter(this.pooledMemoryStream, Encoding.ASCII);
			this.streamWriter.AutoFlush = true;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001128C File Offset: 0x0000F48C
		public bool DataBound
		{
			get
			{
				return this.stringResponseItem != null;
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001129C File Offset: 0x0000F49C
		public void BindData(StringResponseItem stringResponseItem)
		{
			this.stringResponseItem = stringResponseItem;
			this.bufferResponseItem.ClearData();
			this.pooledMemoryStream.Position = 0L;
			this.streamWriter.Write(this.stringResponseItem.StringResponse);
			if (!string.IsNullOrEmpty(this.stringResponseItem.StringResponse) && !this.stringResponseItem.StringResponse.EndsWith(Strings.CRLF, StringComparison.OrdinalIgnoreCase))
			{
				this.streamWriter.Write(Strings.CRLF);
			}
			this.bufferResponseItem.BindData(this.pooledMemoryStream.GetBuffer(), 0, (int)this.pooledMemoryStream.Position, this.stringResponseItem.SendCompleteDelegate);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00011346 File Offset: 0x0000F546
		public void UnbindData()
		{
			this.stringResponseItem = null;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00011350 File Offset: 0x0000F550
		public int GetNextChunk(StringResponseItem stringResponseItem, BaseSession session, out byte[] buffer, out int offset)
		{
			if (!this.DataBound)
			{
				buffer = null;
				offset = 0;
				return 0;
			}
			if (!object.ReferenceEquals(this.stringResponseItem, stringResponseItem))
			{
				throw new InvalidOperationException(string.Format("StringResponseItemProcessor won't support concurrent use for two StringResponseItem instances. \r\n                            Original: '{0}',\r\n                            Current: '{1}'", this.stringResponseItem.StringResponse, stringResponseItem.StringResponse));
			}
			return this.bufferResponseItem.GetNextChunk(session, out buffer, out offset);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000113AC File Offset: 0x0000F5AC
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StringResponseItemProcessor>(this);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000113B4 File Offset: 0x0000F5B4
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000113D0 File Offset: 0x0000F5D0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000113E0 File Offset: 0x0000F5E0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.streamWriter != null)
			{
				this.streamWriter.Dispose();
				this.streamWriter = null;
				this.pooledMemoryStream = null;
			}
			if (this.stringResponseItem != null)
			{
				this.stringResponseItem.Dispose();
				this.stringResponseItem = null;
			}
		}

		// Token: 0x040001FD RID: 509
		private BufferResponseItem bufferResponseItem;

		// Token: 0x040001FE RID: 510
		private StreamWriter streamWriter;

		// Token: 0x040001FF RID: 511
		private PooledMemoryStream pooledMemoryStream;

		// Token: 0x04000200 RID: 512
		private StringResponseItem stringResponseItem;

		// Token: 0x04000201 RID: 513
		private DisposeTracker disposeTracker;
	}
}
