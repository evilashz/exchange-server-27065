using System;
using System.Configuration;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200001D RID: 29
	internal class LightWeightLogSession
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00005E14 File Offset: 0x00004014
		internal LightWeightLogSession(LightWeightLog lightLog, LogRowFormatter row)
		{
			this.lightLog = lightLog;
			this.row = row;
			this.row[2] = 0;
			this.Context = new StringBuilder(1024);
			string text = ConfigurationManager.AppSettings["ProxyTrafficBatchDuration"];
			int num;
			if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num) && (double)num <= TimeSpan.FromMinutes(15.0).TotalSeconds && num >= 0)
			{
				this.proxyTrafficInterval = num;
			}
			else
			{
				ProtocolBaseServices.ServerTracer.TraceDebug<string>(0L, "Invalid Config value '{0}' for ProxyTrafficBatchDuration.  Use Default value.", text);
			}
			this.flushProxyTrafficTime = DateTime.UtcNow.AddSeconds((double)this.proxyTrafficInterval);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00005EDF File Offset: 0x000040DF
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00005EE7 File Offset: 0x000040E7
		public string User { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00005EF0 File Offset: 0x000040F0
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00005EF8 File Offset: 0x000040F8
		public string OrganizationId
		{
			get
			{
				return this.organizationId;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(this.organizationId))
				{
					this.organizationId = value;
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005F16 File Offset: 0x00004116
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00005F1E File Offset: 0x0000411E
		public int ProcessingTime { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00005F27 File Offset: 0x00004127
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00005F2F File Offset: 0x0000412F
		public long RequestSize { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00005F38 File Offset: 0x00004138
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00005F40 File Offset: 0x00004140
		public long ResponseSize { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005F49 File Offset: 0x00004149
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00005F51 File Offset: 0x00004151
		public byte[] Command { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005F5A File Offset: 0x0000415A
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00005F62 File Offset: 0x00004162
		public string Parameters
		{
			get
			{
				return this.parameters;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && value.Length > 2048)
				{
					this.parameters = value.Remove(2048);
					return;
				}
				this.parameters = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00005F92 File Offset: 0x00004192
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00005F9A File Offset: 0x0000419A
		public string Result { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005FA3 File Offset: 0x000041A3
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00005FAB File Offset: 0x000041AB
		public ActivityScope ActivityScope { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005FB4 File Offset: 0x000041B4
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00005FBC File Offset: 0x000041BC
		public IBudget Budget { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005FC5 File Offset: 0x000041C5
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00005FCD File Offset: 0x000041CD
		public int? RowsProcessed { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005FD6 File Offset: 0x000041D6
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00005FDE File Offset: 0x000041DE
		public int? Recent { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005FE7 File Offset: 0x000041E7
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00005FEF File Offset: 0x000041EF
		public int? Unseen { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005FF8 File Offset: 0x000041F8
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00006000 File Offset: 0x00004200
		public int? ItemsDeleted { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006009 File Offset: 0x00004209
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00006011 File Offset: 0x00004211
		public long? TotalSize { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000601A File Offset: 0x0000421A
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00006022 File Offset: 0x00004222
		public int? FolderCount { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000602B File Offset: 0x0000422B
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00006033 File Offset: 0x00004233
		public int? SearchType { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000603C File Offset: 0x0000423C
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00006044 File Offset: 0x00004244
		public string ClientIp { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000604D File Offset: 0x0000424D
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00006055 File Offset: 0x00004255
		public string CafeActivityId { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000605E File Offset: 0x0000425E
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00006066 File Offset: 0x00004266
		public Exception ExceptionCaught { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000606F File Offset: 0x0000426F
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00006078 File Offset: 0x00004278
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.message = string.Empty;
					return;
				}
				if (string.IsNullOrEmpty(this.message))
				{
					this.message = value;
					return;
				}
				this.message += ";" + value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000060CA File Offset: 0x000042CA
		// (set) Token: 0x06000193 RID: 403 RVA: 0x000060D4 File Offset: 0x000042D4
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.errorMessage = string.Empty;
					return;
				}
				if (string.IsNullOrEmpty(this.errorMessage))
				{
					this.errorMessage = value;
					return;
				}
				this.errorMessage += ":" + value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006126 File Offset: 0x00004326
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000612E File Offset: 0x0000432E
		public string LiveIdAuthResult { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006137 File Offset: 0x00004337
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0000613F File Offset: 0x0000433F
		public string ProxyDestination { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00006148 File Offset: 0x00004348
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00006150 File Offset: 0x00004350
		private StringBuilder Context { get; set; }

		// Token: 0x0600019A RID: 410 RVA: 0x00006159 File Offset: 0x00004359
		public void BeginCommand(byte[] command)
		{
			this.Command = command;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006164 File Offset: 0x00004364
		public void FlushProxyTraffic()
		{
			if (this.flushProxyTrafficTime <= DateTime.UtcNow)
			{
				lock (this)
				{
					if (this.flushProxyTrafficTime <= DateTime.UtcNow)
					{
						this.CompleteCommand();
						this.Command = LightWeightLogSession.ProxyBuf;
						this.Parameters = this.ProxyDestination;
						this.flushProxyTrafficTime = DateTime.UtcNow.AddSeconds((double)this.proxyTrafficInterval);
					}
				}
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000061F4 File Offset: 0x000043F4
		public void CompleteCommand()
		{
			if (this.Command == null && this.Result == null)
			{
				return;
			}
			this.AppendContextString("R", this.Result);
			this.AppendContextInt("Rows", this.RowsProcessed);
			this.AppendContextInt("Recent", this.Recent);
			this.AppendContextLong("TotalSize", this.TotalSize);
			this.AppendContextInt("Search", this.SearchType);
			this.AppendContextString("ClientIp", this.ClientIp);
			this.AppendContextString("Msg", this.Message);
			this.AppendContextInt("Unseen", this.Unseen);
			this.AppendContextInt("FolderCount", this.FolderCount);
			this.AppendContextInt("ItemsDeleted", this.ItemsDeleted);
			if (string.IsNullOrEmpty(this.ProxyDestination))
			{
				this.AppendContextString("CafeActivityId", this.CafeActivityId);
			}
			if (!string.IsNullOrEmpty(this.ErrorMessage))
			{
				this.AppendContextString("ErrMsg", this.ErrorMessage);
			}
			if (!string.IsNullOrEmpty(this.LiveIdAuthResult))
			{
				this.AppendContextString("LiveIdAR", this.LiveIdAuthResult);
			}
			if (!string.IsNullOrEmpty(this.organizationId))
			{
				this.AppendContextString("Oid", this.organizationId);
			}
			if (this.ExceptionCaught != null)
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				for (Exception ex = this.ExceptionCaught; ex != null; ex = ex.InnerException)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append('/');
					}
					else
					{
						stringBuilder.Append(ex.Message);
						stringBuilder.Append('-');
					}
					stringBuilder.Append(ex.GetType().FullName);
				}
				this.AppendContextString("Excpt", stringBuilder.ToString());
				this.AppendContextString("ExStk", ExceptionTools.GetCompressedStackTrace(this.ExceptionCaught));
			}
			if (this.ActivityScope != null)
			{
				if (string.IsNullOrEmpty(this.ProxyDestination))
				{
					string text = LogRowFormatter.FormatCollection(this.ActivityScope.GetFormattableStatistics());
					text = text.Replace(';', ',');
					this.AppendContextString("ActivityContextData", text);
				}
				else
				{
					this.AppendContextString("ActivityContextData", this.ActivityScope.ActivityId.ToString());
				}
			}
			if (this.Budget != null && string.IsNullOrEmpty(this.ProxyDestination))
			{
				this.AppendContextString("Budget", this.Budget.ToString());
			}
			this.WriteLog();
			this.Clear();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000645C File Offset: 0x0000465C
		private void WriteLog()
		{
			lock (this.loggingLock)
			{
				this.row[5] = this.User;
				this.row[6] = this.ProcessingTime;
				this.row[7] = this.RequestSize;
				this.row[8] = this.ResponseSize;
				this.row[9] = this.Command;
				this.row[10] = this.Parameters;
				this.row[11] = this.Context.ToString();
				this.lightLog.Append(this.row);
				this.row[2] = (int)this.row[2] + 1;
				this.row[6] = null;
				this.row[7] = null;
				this.row[8] = null;
				this.row[9] = null;
				this.row[10] = null;
				this.row[11] = null;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000065C0 File Offset: 0x000047C0
		private void AppendContextString(string dataName, string dataValue)
		{
			if (!string.IsNullOrEmpty(dataValue))
			{
				if (this.Context.Length > 0)
				{
					this.Context.Append(';');
				}
				if (dataValue.IndexOfAny(LightWeightLogSession.QuotableChars) > -1)
				{
					this.Context.AppendFormat("{0}=\"{1}\"", dataName, dataValue);
					return;
				}
				this.Context.AppendFormat("{0}={1}", dataName, dataValue);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006628 File Offset: 0x00004828
		private void AppendContextInt(string dataName, int dataValue)
		{
			if (dataValue != 0)
			{
				if (this.Context.Length > 0)
				{
					this.Context.Append(';');
				}
				this.Context.Append(dataName);
				this.Context.Append('=');
				this.Context.Append(dataValue);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000667C File Offset: 0x0000487C
		private void AppendContextInt(string dataName, int? dataValue)
		{
			if (dataValue != null)
			{
				if (this.Context.Length > 0)
				{
					this.Context.Append(';');
				}
				this.Context.Append(dataName);
				this.Context.Append('=');
				this.Context.Append(dataValue);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000066DC File Offset: 0x000048DC
		private void AppendContextLong(string dataName, long dataValue)
		{
			if (dataValue != 0L)
			{
				if (this.Context.Length > 0)
				{
					this.Context.Append(';');
				}
				this.Context.Append(dataName);
				this.Context.Append('=');
				this.Context.Append(dataValue);
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006734 File Offset: 0x00004934
		private void AppendContextLong(string dataName, long? dataValue)
		{
			if (dataValue != null)
			{
				if (this.Context.Length > 0)
				{
					this.Context.Append(';');
				}
				this.Context.Append(dataName);
				this.Context.Append('=');
				this.Context.Append(dataValue);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006793 File Offset: 0x00004993
		private void AppendContextUint(string dataName, uint dataValue)
		{
			this.AppendContextInt(dataName, (int)dataValue);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000067A0 File Offset: 0x000049A0
		private void Clear()
		{
			this.Context.Length = 0;
			this.ProcessingTime = 0;
			this.RequestSize = 0L;
			this.ResponseSize = 0L;
			this.Command = null;
			this.Parameters = null;
			this.Result = null;
			this.ActivityScope = null;
			this.RowsProcessed = null;
			this.Recent = null;
			this.Unseen = null;
			this.ItemsDeleted = null;
			this.TotalSize = null;
			this.FolderCount = null;
			this.SearchType = null;
			this.ClientIp = null;
			this.CafeActivityId = null;
			this.ExceptionCaught = null;
			this.Message = null;
			this.ErrorMessage = null;
			this.LiveIdAuthResult = null;
		}

		// Token: 0x040000D5 RID: 213
		internal static readonly byte[] ProxyBuf = Encoding.ASCII.GetBytes("proxy");

		// Token: 0x040000D6 RID: 214
		private static readonly char[] QuotableChars = new char[]
		{
			' ',
			'"',
			',',
			';'
		};

		// Token: 0x040000D7 RID: 215
		private LightWeightLog lightLog;

		// Token: 0x040000D8 RID: 216
		private LogRowFormatter row;

		// Token: 0x040000D9 RID: 217
		private string parameters;

		// Token: 0x040000DA RID: 218
		private string message;

		// Token: 0x040000DB RID: 219
		private string errorMessage;

		// Token: 0x040000DC RID: 220
		private string organizationId;

		// Token: 0x040000DD RID: 221
		private object loggingLock = new object();

		// Token: 0x040000DE RID: 222
		private DateTime flushProxyTrafficTime;

		// Token: 0x040000DF RID: 223
		private readonly int proxyTrafficInterval = 30;
	}
}
