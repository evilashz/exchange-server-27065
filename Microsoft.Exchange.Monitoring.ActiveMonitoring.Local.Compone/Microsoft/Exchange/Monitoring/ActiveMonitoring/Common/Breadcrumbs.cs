using System;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200011B RID: 283
	public class Breadcrumbs
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x00032048 File Offset: 0x00030248
		public Breadcrumbs(int size, TracingContext tracingContext) : this(size)
		{
			this.trace = tracingContext;
			this.isTraceInitialized = true;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00032060 File Offset: 0x00030260
		public Breadcrumbs(int size)
		{
			size = Math.Max(size, 512);
			this.size = size;
			this.Delimiter = "\r\n";
			this.InsertTimestamp = true;
			this.InsertThreadId = false;
			this.UseSmartTruncation = true;
			this.sb = new StringBuilder(size / 2);
			this.timeStarted = DateTime.UtcNow;
			this.isTraceInitialized = false;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000320C7 File Offset: 0x000302C7
		public Breadcrumbs(TracingContext tracingContext) : this(int.MaxValue)
		{
			this.trace = tracingContext;
			this.isTraceInitialized = true;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x000320E2 File Offset: 0x000302E2
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x000320EA File Offset: 0x000302EA
		public string Delimiter { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x000320F3 File Offset: 0x000302F3
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x000320FB File Offset: 0x000302FB
		public bool InsertThreadId { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00032104 File Offset: 0x00030304
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x0003210C File Offset: 0x0003030C
		public bool InsertTimestamp { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00032115 File Offset: 0x00030315
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0003211D File Offset: 0x0003031D
		public bool UseSmartTruncation { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00032126 File Offset: 0x00030326
		public TimeSpan ElapsedTime
		{
			get
			{
				return DateTime.UtcNow - this.timeStarted;
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00032138 File Offset: 0x00030338
		public void Drop(string s)
		{
			bool flag = this.sb.Length > this.size;
			if (this.InsertTimestamp)
			{
				double totalSeconds = this.ElapsedTime.TotalSeconds;
				this.sb.AppendFormat("[{0:000.000}] ", totalSeconds);
			}
			if (this.InsertThreadId)
			{
				int managedThreadId = Thread.CurrentThread.ManagedThreadId;
				this.sb.AppendFormat("[{0:X8}] ", managedThreadId);
			}
			this.sb.Append(s + this.Delimiter);
			if (!flag && this.sb.Length > this.size && this.isTraceInitialized)
			{
				WTFDiagnostics.TraceWarning<int>(ExTraceGlobals.CommonComponentsTracer, this.trace, "Breadcrumbs.Drop: exceeded defined limit of {0} characters.", this.size, null, "Drop", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\Breadcrumbs.cs", 175);
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00032214 File Offset: 0x00030414
		public void Drop(string formatString, params object[] args)
		{
			string s = string.Format(formatString, args);
			this.Drop(s);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00032230 File Offset: 0x00030430
		public void Clear()
		{
			this.sb.Clear();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00032240 File Offset: 0x00030440
		public override string ToString()
		{
			if (this.sb.Length <= this.size)
			{
				return this.sb.ToString();
			}
			if (!this.UseSmartTruncation)
			{
				return this.sb.ToString(0, this.size);
			}
			string text = this.sb.ToString();
			int num = text.IndexOf(this.Delimiter);
			if (num == -1)
			{
				return text.Substring(0, this.size);
			}
			num += this.Delimiter.Length;
			int num2 = text.Length - this.size + "...TRUNCATED...".Length;
			if (num2 < 0)
			{
				return text.Substring(text.Length - this.size, this.size);
			}
			int startIndex = num + num2;
			return text.Substring(0, num) + "...TRUNCATED..." + text.Substring(startIndex);
		}

		// Token: 0x040005CD RID: 1485
		public const string DefaultDelimiter = "\r\n";

		// Token: 0x040005CE RID: 1486
		public const string DelimiterForActiveMonitoring = " ++ ";

		// Token: 0x040005CF RID: 1487
		private const string Truncated = "...TRUNCATED...";

		// Token: 0x040005D0 RID: 1488
		private readonly int size;

		// Token: 0x040005D1 RID: 1489
		private readonly DateTime timeStarted;

		// Token: 0x040005D2 RID: 1490
		private readonly bool isTraceInitialized;

		// Token: 0x040005D3 RID: 1491
		private StringBuilder sb;

		// Token: 0x040005D4 RID: 1492
		private TracingContext trace;
	}
}
