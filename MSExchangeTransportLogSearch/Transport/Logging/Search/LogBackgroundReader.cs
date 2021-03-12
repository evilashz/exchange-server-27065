using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Exchange.Rpc.LogSearch;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000004 RID: 4
	internal class LogBackgroundReader
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000025C7 File Offset: 0x000007C7
		public LogBackgroundReader(LogFilter filter, int bufferSize)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch constructs LogBackgroundReader");
			this.filter = filter;
			this.backgroundBuffer = new byte[bufferSize];
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002604 File Offset: 0x00000804
		public int Read(byte[] dest, int offset, int count, out bool more, out int progress, bool continueInBackground)
		{
			ThreadPriority priority = Thread.CurrentThread.Priority;
			int result;
			try
			{
				Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogBackgroundReader Read");
				more = true;
				this.continueInBackground = continueInBackground;
				int num = offset;
				int num2 = offset + count;
				lock (this.sync)
				{
					if (this.backgroundBytes > 0)
					{
						int num3 = Math.Min(this.backgroundBytes, num2 - num);
						Buffer.BlockCopy(this.backgroundBuffer, 0, dest, num, num3);
						num += num3;
						this.backgroundBytes -= num3;
						progress = this.progress;
						return num - offset;
					}
					if (this.backgroundRead)
					{
						Monitor.Wait(this.sync);
						if (this.canceled)
						{
							throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_SESSION_CANCELED);
						}
						if (this.backgroundBytes > 0)
						{
							int num4 = Math.Min(this.backgroundBytes, num2 - num);
							Buffer.BlockCopy(this.backgroundBuffer, 0, dest, num, num4);
							num += num4;
							this.backgroundBytes -= num4;
						}
						progress = this.progress;
						return num - offset;
					}
				}
				num += this.filter.Read(dest, num, num2 - num, out more);
				this.progress = this.filter.Progress;
				if (this.continueInBackground)
				{
					this.backgroundRead = true;
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.Background));
				}
				progress = this.progress;
				result = num - offset;
			}
			finally
			{
				Thread.CurrentThread.Priority = priority;
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027CC File Offset: 0x000009CC
		private void Background(object state)
		{
			ThreadPriority priority = Thread.CurrentThread.Priority;
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogBackgroundReader Background");
			int num;
			lock (this.sync)
			{
				num = this.backgroundBuffer.Length - this.backgroundBytes;
			}
			try
			{
				Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
				bool flag2 = true;
				while (this.continueInBackground && num > 0 && flag2)
				{
					int num2 = this.filter.Read(this.backgroundBuffer, this.backgroundBuffer.Length - num, num, out flag2);
					lock (this.sync)
					{
						if (this.backgroundBytes + num < this.backgroundBuffer.Length)
						{
							Buffer.BlockCopy(this.backgroundBuffer, this.backgroundBuffer.Length - num, this.backgroundBuffer, this.backgroundBytes, num2);
							this.backgroundBytes += num2;
							num = this.backgroundBuffer.Length - this.backgroundBytes;
						}
						else
						{
							num -= num2;
						}
						this.progress = this.filter.Progress;
						Monitor.PulseAll(this.sync);
					}
				}
			}
			catch (LogSearchException)
			{
				ExTraceGlobals.ServiceTracer.TraceError((long)this.GetHashCode(), "MsExchangeLogSearch LogBackgroundReader failed with LogSearchException");
				this.canceled = true;
			}
			finally
			{
				Thread.CurrentThread.Priority = priority;
				lock (this.sync)
				{
					this.backgroundRead = false;
					Monitor.PulseAll(this.sync);
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000029A8 File Offset: 0x00000BA8
		public void BeginClose()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogBackgroundReader BeginClose");
			this.filter.BeginClose();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000029CC File Offset: 0x00000BCC
		public void EndClose()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogBackgroundReader EndClose");
			lock (this.sync)
			{
				while (this.backgroundRead)
				{
					Monitor.Wait(this.sync);
				}
			}
			this.filter.EndClose();
		}

		// Token: 0x04000009 RID: 9
		private LogFilter filter;

		// Token: 0x0400000A RID: 10
		private byte[] backgroundBuffer;

		// Token: 0x0400000B RID: 11
		private int backgroundBytes;

		// Token: 0x0400000C RID: 12
		private bool backgroundRead;

		// Token: 0x0400000D RID: 13
		private bool continueInBackground;

		// Token: 0x0400000E RID: 14
		private bool canceled;

		// Token: 0x0400000F RID: 15
		private int progress;

		// Token: 0x04000010 RID: 16
		private object sync = new object();
	}
}
