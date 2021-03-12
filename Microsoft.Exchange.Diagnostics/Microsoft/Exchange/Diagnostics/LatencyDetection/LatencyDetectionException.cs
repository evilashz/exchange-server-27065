using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000166 RID: 358
	public class LatencyDetectionException : Exception
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x0002668F File Offset: 0x0002488F
		internal LatencyDetectionException(LatencyDetectionContext trigger) : this(trigger.Location.Identity, trigger.StackTraceContext.ToString(), trigger.Elapsed, trigger.Latencies, string.Empty)
		{
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000266BE File Offset: 0x000248BE
		internal LatencyDetectionException(LatencyDetectionContext trigger, IPerformanceDataProvider provider) : this(trigger.Location.Identity, trigger.StackTraceContext.ToString(), trigger.Elapsed, trigger.Latencies, provider.Name)
		{
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000266F0 File Offset: 0x000248F0
		private LatencyDetectionException(string context, string stackTraceContext, TimeSpan total, ICollection<LabeledTimeSpan> times, string nameOfDataProvider)
		{
			this.context = context;
			this.total = total;
			this.times = times;
			Type type = base.GetType();
			this.watsonExceptionName = ((!string.IsNullOrEmpty(nameOfDataProvider)) ? (type.Namespace + "." + nameOfDataProvider + type.Name) : type.FullName);
			this.stackTrace = new StackTrace(3, true);
			if (Uri.IsWellFormedUriString(stackTraceContext, UriKind.RelativeOrAbsolute))
			{
				this.watsonMethodName = stackTraceContext;
				return;
			}
			this.SetWatsonMethodNameFromStackTrace();
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00026783 File Offset: 0x00024983
		public override string StackTrace
		{
			get
			{
				return this.stackTrace.ToString();
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00026790 File Offset: 0x00024990
		public override string Message
		{
			get
			{
				if (string.IsNullOrEmpty(this.message))
				{
					int capacity = "High latency seen in \"".Length + this.context.Length + 16 + this.times.Count * 26;
					StringBuilder stringBuilder = new StringBuilder(capacity);
					stringBuilder.Append("High latency seen in \"").Append(this.context).Append("\" context. Total: ").Append(this.total);
					foreach (LabeledTimeSpan labeledTimeSpan in this.times)
					{
						stringBuilder.Append("; ").Append(labeledTimeSpan.Label).Append(": ").Append(labeledTimeSpan.TimeSpan);
					}
					this.message = stringBuilder.ToString();
				}
				return this.message;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0002688C File Offset: 0x00024A8C
		internal string WatsonExceptionName
		{
			get
			{
				return this.watsonExceptionName;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00026894 File Offset: 0x00024A94
		internal string WatsonMethodName
		{
			get
			{
				return this.watsonMethodName;
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002689C File Offset: 0x00024A9C
		private void SetWatsonMethodNameFromStackTrace()
		{
			int num = 0;
			string arg = string.Empty;
			string arg2 = string.Empty;
			StackFrame frame;
			MethodBase method;
			for (;;)
			{
				frame = this.stackTrace.GetFrame(num);
				method = frame.GetMethod();
				string name = method.Name;
				if (name.IndexOf("log", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					break;
				}
				num++;
				if (num >= this.stackTrace.FrameCount)
				{
					return;
				}
			}
			num += 2;
			frame = this.stackTrace.GetFrame(num);
			method = frame.GetMethod();
			arg = method.Name;
			arg2 = WatsonReport.GetShortParameter(method.DeclaringType.FullName);
			this.watsonMethodName = arg2 + '.' + arg;
		}

		// Token: 0x040006F2 RID: 1778
		private const string MessagePart1 = "High latency seen in \"";

		// Token: 0x040006F3 RID: 1779
		private const string MessagePart2 = "\" context. Total: ";

		// Token: 0x040006F4 RID: 1780
		private readonly string watsonExceptionName;

		// Token: 0x040006F5 RID: 1781
		private readonly StackTrace stackTrace;

		// Token: 0x040006F6 RID: 1782
		private readonly ICollection<LabeledTimeSpan> times;

		// Token: 0x040006F7 RID: 1783
		private readonly string context;

		// Token: 0x040006F8 RID: 1784
		private readonly TimeSpan total;

		// Token: 0x040006F9 RID: 1785
		private string watsonMethodName;

		// Token: 0x040006FA RID: 1786
		private string message = string.Empty;
	}
}
