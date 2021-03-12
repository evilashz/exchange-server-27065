using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000083 RID: 131
	internal class EtwSessionInfo
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002F0 RID: 752 RVA: 0x0000A898 File Offset: 0x00008A98
		// (remove) Token: 0x060002F1 RID: 753 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		public event Action OnTraceStateChange;

		// Token: 0x060002F2 RID: 754 RVA: 0x0000A905 File Offset: 0x00008B05
		public EtwSessionInfo()
		{
			this.callback = new DiagnosticsNativeMethods.ControlCallback(this.TraceControlCallback);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000A91F File Offset: 0x00008B1F
		public bool TracingEnabled
		{
			get
			{
				return this.tracingEnabled;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000A927 File Offset: 0x00008B27
		public DiagnosticsNativeMethods.CriticalTraceHandle Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000A92F File Offset: 0x00008B2F
		public DiagnosticsNativeMethods.ControlCallback ControlCallback
		{
			get
			{
				return this.callback;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000A938 File Offset: 0x00008B38
		private static DiagnosticsNativeMethods.EventTraceProperties CreateEventTraceProperties()
		{
			DiagnosticsNativeMethods.EventTraceProperties eventTraceProperties = default(DiagnosticsNativeMethods.EventTraceProperties);
			eventTraceProperties.etp.wnode.bufferSize = (uint)Marshal.SizeOf(eventTraceProperties);
			eventTraceProperties.etp.wnode.flags = 131072U;
			eventTraceProperties.etp.logFileNameOffset = (uint)((int)Marshal.OffsetOf(typeof(DiagnosticsNativeMethods.EventTraceProperties), "logFileName"));
			eventTraceProperties.etp.loggerNameOffset = (uint)((int)Marshal.OffsetOf(typeof(DiagnosticsNativeMethods.EventTraceProperties), "loggerName"));
			eventTraceProperties.logFileName = null;
			eventTraceProperties.loggerName = null;
			return eventTraceProperties;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000A9DC File Offset: 0x00008BDC
		private uint TraceControlCallback(int requestCode, IntPtr context, IntPtr reserved, IntPtr buffer)
		{
			if (requestCode == 4)
			{
				try
				{
					this.session = DiagnosticsNativeMethods.CriticalTraceHandle.Attach(buffer);
				}
				catch (Win32Exception)
				{
					return 0U;
				}
				if (DiagnosticsNativeMethods.ControlTrace(this.session.DangerousGetHandle(), null, ref EtwSessionInfo.properties, 0U) == 0U)
				{
					this.tracingEnabled = true;
					this.InvokeOnTraceStateChange();
					return 0U;
				}
				return 0U;
			}
			if (requestCode == 5)
			{
				this.tracingEnabled = false;
				if (this.session != null)
				{
					this.session.Dispose();
				}
				this.InvokeOnTraceStateChange();
			}
			return 0U;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000AA64 File Offset: 0x00008C64
		private void InvokeOnTraceStateChange()
		{
			Action onTraceStateChange = this.OnTraceStateChange;
			if (onTraceStateChange != null)
			{
				onTraceStateChange();
			}
		}

		// Token: 0x040002B3 RID: 691
		private static DiagnosticsNativeMethods.EventTraceProperties properties = EtwSessionInfo.CreateEventTraceProperties();

		// Token: 0x040002B4 RID: 692
		private DiagnosticsNativeMethods.CriticalTraceHandle session;

		// Token: 0x040002B5 RID: 693
		private bool tracingEnabled;

		// Token: 0x040002B6 RID: 694
		private DiagnosticsNativeMethods.ControlCallback callback;
	}
}
