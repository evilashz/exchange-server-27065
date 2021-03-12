using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.LogSearch;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000028 RID: 40
	internal class LogSearchStream : Stream
	{
		// Token: 0x0600007A RID: 122 RVA: 0x0000734C File Offset: 0x0000554C
		public LogSearchStream(string server, ServerVersion version, string logName, LogQuery query, IProgressReport progressReport)
		{
			ExTraceGlobals.CommonTracer.TraceDebug<string, string>((long)this.GetHashCode(), "MsExchangeLogSearch construct LogSearchStream with server name {0} and log name {1}", server, logName);
			this.buffer = new byte[32768];
			this.progressReport = progressReport;
			this.server = server;
			this.serverVersion = version;
			this.logName = logName;
			this.query = query;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000073B7 File Offset: 0x000055B7
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000073BA File Offset: 0x000055BA
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000073BD File Offset: 0x000055BD
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000073C0 File Offset: 0x000055C0
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000073C7 File Offset: 0x000055C7
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000073CE File Offset: 0x000055CE
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000073D8 File Offset: 0x000055D8
		public override int Read(byte[] dest, int offset, int count)
		{
			int i = offset;
			int num = offset + count;
			if (this.client == null)
			{
				ExTraceGlobals.CommonTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSearchStream Read create new LogSearchClient");
				this.client = new LogSearchClient(this.server, this.serverVersion);
				this.bufferEnd = this.client.Search(this.logName, this.query, true, this.buffer, out this.sessionId, out this.more, out this.progress);
			}
			while (i < num)
			{
				if (this.bufferPos < this.bufferEnd)
				{
					int num2 = Math.Min(num - i, this.bufferEnd - this.bufferPos);
					Buffer.BlockCopy(this.buffer, this.bufferPos, dest, i, num2);
					this.bufferPos += num2;
					i += num2;
					return i - offset;
				}
				if (this.more)
				{
					this.bufferPos = 0;
					bool flag = false;
					try
					{
						int num3;
						this.bufferEnd = this.client.Continue(this.sessionId, true, this.buffer, out this.more, out num3);
						flag = true;
						if (num3 > this.progress)
						{
							this.progress = num3;
							if (this.progressReport != null)
							{
								this.progressReport.Report(this.progress);
							}
						}
						continue;
					}
					finally
					{
						if (!flag)
						{
							ExTraceGlobals.CommonTracer.TraceError((long)this.GetHashCode(), "MsExchangeLogSearch LogSearchStream Read client continue failed");
							this.more = false;
						}
					}
				}
				return i - offset;
			}
			return i - offset;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00007558 File Offset: 0x00005758
		public override void Write(byte[] src, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000755F File Offset: 0x0000575F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00007566 File Offset: 0x00005766
		public override void SetLength(long length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000756D File Offset: 0x0000576D
		public override void Flush()
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00007570 File Offset: 0x00005770
		public void Cancel()
		{
			ExTraceGlobals.CommonTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch logSearchStream Cancel");
			lock (this.sync)
			{
				if (this.client != null)
				{
					try
					{
						this.client.Cancel(this.sessionId);
					}
					catch (LogSearchException ex)
					{
						ExTraceGlobals.CommonTracer.TraceError<int>((long)this.GetHashCode(), "MsExchangeLogSearch logSearchStream Cancel client cancel failed with LogSearchException. The error code is {0}", ex.ErrorCode);
					}
					catch (RpcException ex2)
					{
						ExTraceGlobals.CommonTracer.TraceError<string>((long)this.GetHashCode(), "MsExchangeLogSearch logSearchStream Cancel client cancel faied with RpcException. The error message is {0}", ex2.Message);
					}
					Monitor.Wait(this.sync);
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00007640 File Offset: 0x00005840
		public override void Close()
		{
			ExTraceGlobals.CommonTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch logSearchStream Close");
			lock (this.sync)
			{
				if (this.client != null)
				{
					if (this.more)
					{
						try
						{
							this.client.Cancel(this.sessionId);
						}
						catch (LogSearchException ex)
						{
							ExTraceGlobals.CommonTracer.TraceError<int>((long)this.GetHashCode(), "MsExchangeLogSearch logSearchStream Close client cancel failed with LogSearchException. The error code is {0}", ex.ErrorCode);
						}
						catch (RpcException ex2)
						{
							ExTraceGlobals.CommonTracer.TraceError<string>((long)this.GetHashCode(), "MsExchangeLogSearch logSearchStream Close client cancel faied with RpcException. The error message is {0}", ex2.Message);
						}
						this.more = false;
					}
					((IDisposable)this.client).Dispose();
					this.client = null;
					Monitor.PulseAll(this.sync);
				}
			}
		}

		// Token: 0x04000077 RID: 119
		private byte[] buffer;

		// Token: 0x04000078 RID: 120
		private int bufferPos;

		// Token: 0x04000079 RID: 121
		private int bufferEnd;

		// Token: 0x0400007A RID: 122
		private LogSearchClient client;

		// Token: 0x0400007B RID: 123
		private Guid sessionId;

		// Token: 0x0400007C RID: 124
		private bool more;

		// Token: 0x0400007D RID: 125
		private int progress;

		// Token: 0x0400007E RID: 126
		private object sync = new object();

		// Token: 0x0400007F RID: 127
		private IProgressReport progressReport;

		// Token: 0x04000080 RID: 128
		private string server;

		// Token: 0x04000081 RID: 129
		private ServerVersion serverVersion;

		// Token: 0x04000082 RID: 130
		private string logName;

		// Token: 0x04000083 RID: 131
		private LogQuery query;
	}
}
