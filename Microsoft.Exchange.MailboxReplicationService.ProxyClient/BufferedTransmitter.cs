using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000003 RID: 3
	internal class BufferedTransmitter : DisposableWrapper<IDataImport>, IDataImport, IDisposable
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000021F8 File Offset: 0x000003F8
		public BufferedTransmitter(IDataImport destination, int exportBufferSizeFromMrsKB, bool ownsDestination, bool useBuffering, bool useCompression) : base(destination, ownsDestination)
		{
			this.useBuffering = useBuffering;
			this.useCompression = useCompression;
			this.minBatchSize = this.GetExportBufferSize(exportBufferSizeFromMrsKB);
			this.dataBuffer = new MemoryStream();
			this.gzipStream = null;
			this.writer = null;
			this.ResetWriter();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000224C File Offset: 0x0000044C
		void IDataImport.SendMessage(IDataMessage message)
		{
			if (!this.useBuffering)
			{
				base.WrappedObject.SendMessage(message);
				return;
			}
			DataMessageOpcode value;
			byte[] array;
			message.Serialize(this.useCompression, out value, out array);
			this.writer.Write((int)value);
			if (array != null)
			{
				this.writer.Write(array.Length);
				this.writer.Write(array);
				this.uncompressedSize += 8 + array.Length;
			}
			else
			{
				this.writer.Write(0);
				this.uncompressedSize += 8;
			}
			this.bufferCount++;
			if (this.gzipStream != null)
			{
				this.gzipStream.Flush();
			}
			if (this.dataBuffer.Length >= (long)this.minBatchSize)
			{
				this.FlushBuffers(false);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002311 File Offset: 0x00000511
		IDataMessage IDataImport.SendMessageAndWaitForReply(IDataMessage message)
		{
			if (!this.useBuffering)
			{
				return base.WrappedObject.SendMessageAndWaitForReply(message);
			}
			if (message is FlushMessage)
			{
				this.FlushBuffers(true);
				return null;
			}
			return base.WrappedObject.SendMessageAndWaitForReply(message);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002348 File Offset: 0x00000548
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (this.writer != null)
			{
				this.writer.Close();
				this.writer = null;
			}
			if (this.gzipStream != null)
			{
				this.gzipStream.Dispose();
				this.gzipStream = null;
			}
			if (this.dataBuffer != null)
			{
				this.dataBuffer.Dispose();
				this.dataBuffer = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000023AC File Offset: 0x000005AC
		private void ResetWriter()
		{
			if (this.gzipStream != null)
			{
				this.gzipStream.Close();
				this.gzipStream = null;
			}
			this.dataBuffer.SetLength(0L);
			this.dataBuffer.Capacity = this.minBatchSize + this.minBatchSize / 10;
			this.dataBuffer.Position = 0L;
			this.uncompressedSize = 0;
			this.bufferCount = 0;
			Stream output;
			if (this.useCompression)
			{
				this.gzipStream = new GZipStream(this.dataBuffer, CompressionMode.Compress, true);
				output = this.gzipStream;
			}
			else
			{
				output = this.dataBuffer;
			}
			this.writer = new BinaryWriter(output);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002450 File Offset: 0x00000650
		private void FlushBuffers(bool doDestinationFlush)
		{
			if (this.gzipStream != null)
			{
				this.gzipStream.Dispose();
				this.gzipStream = null;
			}
			this.dataBuffer.Flush();
			byte[] data;
			if (this.dataBuffer.Length > 0L)
			{
				data = this.dataBuffer.ToArray();
				MrsTracer.ProxyClient.Debug("Flushing {0} bytes.", new object[]
				{
					this.dataBuffer.Length
				});
			}
			else
			{
				data = null;
			}
			this.ResetWriter();
			base.WrappedObject.SendMessage(new BufferBatchMessage(data, doDestinationFlush));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024E4 File Offset: 0x000006E4
		private int GetExportBufferSize(int exportBufferSizeFromMrsKB)
		{
			int num = ConfigBase<MRSConfigSchema>.GetConfig<int>("ExportBufferSizeOverrideKB");
			if (num == 0)
			{
				if (exportBufferSizeFromMrsKB > 0)
				{
					num = exportBufferSizeFromMrsKB;
				}
				else
				{
					num = ConfigBase<MRSConfigSchema>.GetConfig<int>("ExportBufferSizeKB");
				}
			}
			return num * 1024;
		}

		// Token: 0x04000003 RID: 3
		private readonly int minBatchSize;

		// Token: 0x04000004 RID: 4
		private bool useCompression;

		// Token: 0x04000005 RID: 5
		private bool useBuffering;

		// Token: 0x04000006 RID: 6
		private MemoryStream dataBuffer;

		// Token: 0x04000007 RID: 7
		private GZipStream gzipStream;

		// Token: 0x04000008 RID: 8
		private BinaryWriter writer;

		// Token: 0x04000009 RID: 9
		private int uncompressedSize;

		// Token: 0x0400000A RID: 10
		private int bufferCount;
	}
}
