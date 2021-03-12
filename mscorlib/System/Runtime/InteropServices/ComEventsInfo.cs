using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000980 RID: 2432
	[SecurityCritical]
	internal class ComEventsInfo
	{
		// Token: 0x060061F6 RID: 25078 RVA: 0x0014D114 File Offset: 0x0014B314
		private ComEventsInfo(object rcw)
		{
			this._rcw = rcw;
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x0014D124 File Offset: 0x0014B324
		[SecuritySafeCritical]
		~ComEventsInfo()
		{
			this._sinks = ComEventsSink.RemoveAll(this._sinks);
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x0014D15C File Offset: 0x0014B35C
		[SecurityCritical]
		internal static ComEventsInfo Find(object rcw)
		{
			return (ComEventsInfo)Marshal.GetComObjectData(rcw, typeof(ComEventsInfo));
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x0014D174 File Offset: 0x0014B374
		[SecurityCritical]
		internal static ComEventsInfo FromObject(object rcw)
		{
			ComEventsInfo comEventsInfo = ComEventsInfo.Find(rcw);
			if (comEventsInfo == null)
			{
				comEventsInfo = new ComEventsInfo(rcw);
				Marshal.SetComObjectData(rcw, typeof(ComEventsInfo), comEventsInfo);
			}
			return comEventsInfo;
		}

		// Token: 0x060061FA RID: 25082 RVA: 0x0014D1A5 File Offset: 0x0014B3A5
		internal ComEventsSink FindSink(ref Guid iid)
		{
			return ComEventsSink.Find(this._sinks, ref iid);
		}

		// Token: 0x060061FB RID: 25083 RVA: 0x0014D1B4 File Offset: 0x0014B3B4
		internal ComEventsSink AddSink(ref Guid iid)
		{
			ComEventsSink sink = new ComEventsSink(this._rcw, iid);
			this._sinks = ComEventsSink.Add(this._sinks, sink);
			return this._sinks;
		}

		// Token: 0x060061FC RID: 25084 RVA: 0x0014D1EB File Offset: 0x0014B3EB
		[SecurityCritical]
		internal ComEventsSink RemoveSink(ComEventsSink sink)
		{
			this._sinks = ComEventsSink.Remove(this._sinks, sink);
			return this._sinks;
		}

		// Token: 0x04002BFA RID: 11258
		private ComEventsSink _sinks;

		// Token: 0x04002BFB RID: 11259
		private object _rcw;
	}
}
