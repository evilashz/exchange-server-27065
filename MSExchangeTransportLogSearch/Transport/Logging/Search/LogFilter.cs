using System;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Exchange.Rpc.LogSearch;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200001E RID: 30
	internal class LogFilter
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00003FEC File Offset: 0x000021EC
		public LogFilter(Log log, Version clientSchemaVersion, LogEvaluator evaluator, DateTime begin, DateTime end)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "MsExchangeLogSearch constructs LogFilter with begin time {0} and end time {1}", begin.ToString(), end.ToString());
			this.clientSchemaVersion = clientSchemaVersion;
			this.cursor = log.GetCursor(begin, end, clientSchemaVersion);
			this.searchContext = new SearchContext(evaluator, this.cursor);
			this.firstReadRequest = true;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00004060 File Offset: 0x00002260
		public int Progress
		{
			get
			{
				return this.cursor.Progress;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004070 File Offset: 0x00002270
		public int Read(byte[] dest, int offset, int count, out bool more)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogFilter Read");
			int progress = this.Progress;
			bool flag = false;
			bool flag2 = false;
			DateTime t = DateTime.MaxValue;
			byte[] fieldNameList = this.cursor.FieldNameList;
			int num = offset;
			int num2 = offset + count;
			more = true;
			if (!(this.clientSchemaVersion < LogSearchConstants.LowestModernInterfaceBuildVersion) && this.firstReadRequest && count > fieldNameList.Length)
			{
				this.firstReadRequest = false;
				Buffer.BlockCopy(fieldNameList, 0, dest, num, fieldNameList.Length);
				num += fieldNameList.Length;
			}
			while (num < num2 && ((!flag && !flag2) || DateTime.UtcNow < t))
			{
				if (this.cancel)
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_SESSION_CANCELED);
				}
				if (this.copyRow)
				{
					int num3 = this.cursor.CopyRow(this.rowOffset, dest, num, num2 - num);
					if (num3 == 0)
					{
						this.copyRow = false;
						this.copyCRLF = true;
						this.crlfOffset = 0;
					}
					else
					{
						num += num3;
						this.rowOffset += num3;
						if (!flag2)
						{
							flag2 = true;
							t = DateTime.UtcNow + LogFilter.DataReportingDelay;
						}
					}
				}
				else if (this.copyCRLF)
				{
					if (this.crlfOffset < LogFilter.cRLF.Length)
					{
						dest[num++] = LogFilter.cRLF[this.crlfOffset++];
					}
					else
					{
						this.copyCRLF = false;
					}
				}
				else
				{
					if (!this.searchContext.MoveNext())
					{
						more = false;
						break;
					}
					this.copyRow = true;
					this.rowOffset = 0;
				}
			}
			return num - offset;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004215 File Offset: 0x00002415
		public void BeginClose()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogFilter BeginClose");
			this.cancel = true;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004234 File Offset: 0x00002434
		public void EndClose()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogFilter EndClose");
			this.cursor.Close();
			this.cursor = null;
		}

		// Token: 0x04000036 RID: 54
		private static readonly TimeSpan DataReportingDelay = new TimeSpan(0, 0, 0, 0, 250);

		// Token: 0x04000037 RID: 55
		private static readonly TimeSpan ProgressReportingDelay = new TimeSpan(0, 0, 10);

		// Token: 0x04000038 RID: 56
		private static byte[] cRLF = new byte[]
		{
			13,
			10
		};

		// Token: 0x04000039 RID: 57
		private LogCursor cursor;

		// Token: 0x0400003A RID: 58
		private SearchContext searchContext;

		// Token: 0x0400003B RID: 59
		private Version clientSchemaVersion;

		// Token: 0x0400003C RID: 60
		private bool copyRow;

		// Token: 0x0400003D RID: 61
		private int rowOffset;

		// Token: 0x0400003E RID: 62
		private bool copyCRLF;

		// Token: 0x0400003F RID: 63
		private int crlfOffset;

		// Token: 0x04000040 RID: 64
		private bool cancel;

		// Token: 0x04000041 RID: 65
		private bool firstReadRequest;
	}
}
