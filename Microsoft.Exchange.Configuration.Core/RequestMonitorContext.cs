using System;
using System.Web;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000018 RID: 24
	internal sealed class RequestMonitorContext
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00004FDA File Offset: 0x000031DA
		internal RequestMonitorContext(Guid requestId)
		{
			this[RequestMonitorMetadata.StartTime] = DateTime.UtcNow;
			this[RequestMonitorMetadata.RequestId] = requestId;
			RequestMonitorContext.Current = this;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005018 File Offset: 0x00003218
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00005048 File Offset: 0x00003248
		internal static RequestMonitorContext Current
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					return null;
				}
				return (RequestMonitorContext)httpContext.Items["X-RequestMonitor-Item-Key"];
			}
			set
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					httpContext.Items["X-RequestMonitor-Item-Key"] = value;
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000506F File Offset: 0x0000326F
		internal object[] Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x17000024 RID: 36
		internal object this[RequestMonitorMetadata key]
		{
			get
			{
				return this.fields[(int)key];
			}
			set
			{
				this.fields[(int)key] = value;
			}
		}

		// Token: 0x17000025 RID: 37
		internal object this[int index]
		{
			get
			{
				return this.fields[index];
			}
			set
			{
				this.fields[index] = value;
			}
		}

		// Token: 0x04000065 RID: 101
		private const string HttpContextItemKey = "X-RequestMonitor-Item-Key";

		// Token: 0x04000066 RID: 102
		private static readonly int Length = Enum.GetNames(typeof(RequestMonitorMetadata)).Length;

		// Token: 0x04000067 RID: 103
		private object[] fields = new object[RequestMonitorContext.Length];
	}
}
