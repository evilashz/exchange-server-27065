using System;
using System.Diagnostics;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000093 RID: 147
	internal sealed class ExTraceListener : TraceListener
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000C48C File Offset: 0x0000A68C
		private Trace ExchangeTracer
		{
			get
			{
				if (this.tracer == null)
				{
					lock (this)
					{
						if (this.tracer == null)
						{
							this.tracer = new Trace(SystemLoggingTags.guid, this.traceTag);
						}
					}
				}
				return this.tracer;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		public ExTraceListener(int traceTag, string source) : base(source)
		{
			this.line = new StringBuilder(1024);
			this.traceData = new StringBuilder(1024);
			this.traceTag = traceTag;
			base.TraceOutputOptions = TraceOptions.None;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000C527 File Offset: 0x0000A727
		public override void Write(string message)
		{
			if (ExTraceInternal.AreAnyTraceProvidersEnabled)
			{
				this.line.Append(message);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000C53D File Offset: 0x0000A73D
		public override void WriteLine(string message)
		{
			if (ExTraceInternal.AreAnyTraceProvidersEnabled)
			{
				this.line.Append(message);
				this.ExchangeTracer.TraceDebug<StringBuilder>((long)this.GetHashCode(), "{0}", this.line);
				this.line.Length = 0;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C57C File Offset: 0x0000A77C
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (data == null)
			{
				return;
			}
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			for (int i = 0; i < data.Length; i++)
			{
				if (i != 0)
				{
					this.traceData.Append(", ");
				}
				if (data[i] != null)
				{
					this.traceData.Append(data[i].ToString());
				}
			}
			this.TraceEvent(eventCache, source, eventType, id, this.traceData.ToString());
			this.traceData.Length = 0;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C60E File Offset: 0x0000A80E
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (data == null)
			{
				return;
			}
			this.TraceEvent(eventCache, source, eventType, id, data.ToString());
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000C628 File Offset: 0x0000A828
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
			{
				return;
			}
			long id2 = (id == 0) ? ((long)this.GetHashCode()) : ((long)id);
			switch (eventType)
			{
			case TraceEventType.Critical:
			case TraceEventType.Error:
				this.ExchangeTracer.TraceError(id2, message);
				return;
			case (TraceEventType)3:
				break;
			case TraceEventType.Warning:
				this.ExchangeTracer.TraceWarning(id2, message);
				return;
			default:
				if (eventType == TraceEventType.Information)
				{
					this.ExchangeTracer.TraceInformation(0, id2, message);
					return;
				}
				break;
			}
			this.ExchangeTracer.TraceDebug(id2, message);
		}

		// Token: 0x0400030B RID: 779
		private int traceTag;

		// Token: 0x0400030C RID: 780
		private Trace tracer;

		// Token: 0x0400030D RID: 781
		private StringBuilder line;

		// Token: 0x0400030E RID: 782
		private StringBuilder traceData;
	}
}
