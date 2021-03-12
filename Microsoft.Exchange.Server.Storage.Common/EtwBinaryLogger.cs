using System;
using System.ComponentModel;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000038 RID: 56
	public class EtwBinaryLogger : DisposableBase, IBinaryLogger, IDisposable
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		private EtwBinaryLogger(string name, Guid providerGuid)
		{
			this.name = name;
			this.providerGuid = providerGuid;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000C2B6 File Offset: 0x0000A4B6
		public bool IsLoggingEnabled
		{
			get
			{
				return this.sessionHandle != null && !this.sessionHandle.IsInvalid;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		public static EtwBinaryLogger Create(string name, Guid providerGuid)
		{
			return new EtwBinaryLogger(name, providerGuid);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		public void Start()
		{
			try
			{
				this.registrationHandle = DiagnosticsNativeMethods.CriticalTraceRegistrationHandle.RegisterTrace(this.providerGuid, new DiagnosticsNativeMethods.ControlCallback(this.TraceControlCallback));
			}
			catch (Win32Exception ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				throw new StoreException((LID)46200U, ErrorCodeValue.CallFailed, "RegisterTrace failed", ex);
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000C340 File Offset: 0x0000A540
		public void Stop()
		{
			if (this.registrationHandle != null)
			{
				this.registrationHandle.Dispose();
				this.registrationHandle = null;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000C35C File Offset: 0x0000A55C
		public bool TryWrite(TraceBuffer buffer, int retries, TimeSpan timeToWait)
		{
			for (int i = 0; i < retries; i++)
			{
				if (this.TryWrite(buffer))
				{
					return true;
				}
				Thread.Sleep(timeToWait);
			}
			return false;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000C387 File Offset: 0x0000A587
		public bool TryWrite(TraceBuffer buffer)
		{
			return this.TryWrite(buffer.RecordGuid, buffer.Data, buffer.Length);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000C3A4 File Offset: 0x0000A5A4
		internal bool TryWrite(Guid recordGuid, byte[] buffer, int retries, TimeSpan timeToWait)
		{
			for (int i = 0; i < retries; i++)
			{
				if (this.TryWrite(recordGuid, buffer, buffer.Length))
				{
					return true;
				}
				Thread.Sleep(timeToWait);
			}
			return false;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		internal unsafe bool TryWrite(Guid recordGuid, byte[] buffer, int bytesToWrite)
		{
			if (buffer.Length < bytesToWrite)
			{
				return false;
			}
			if (!this.IsLoggingEnabled)
			{
				return false;
			}
			fixed (byte* ptr = buffer)
			{
				int num = 0;
				while (bytesToWrite > 0)
				{
					int num2 = Math.Min(bytesToWrite, 8064);
					uint num3 = DiagnosticsNativeMethods.TraceMessage(this.sessionHandle.DangerousGetHandle(), 43U, ref recordGuid, 0, ptr + num, num2, IntPtr.Zero, 0);
					bool result;
					if (num3 == 8U)
					{
						DiagnosticContext.TraceLocation((LID)49624U);
						result = false;
					}
					else if (num3 == 14U)
					{
						DiagnosticContext.TraceLocation((LID)52952U);
						result = false;
					}
					else if (num3 == 6U)
					{
						DiagnosticContext.TraceLocation((LID)47228U);
						result = false;
					}
					else
					{
						if (num3 == 0U)
						{
							num += num2;
							bytesToWrite -= num2;
							continue;
						}
						Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TraceMessageFailed, new object[]
						{
							recordGuid,
							num3,
							buffer.Length,
							bytesToWrite,
							num2,
							num
						});
						result = false;
					}
					return result;
				}
			}
			return true;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000C504 File Offset: 0x0000A704
		private uint TraceControlCallback(int requestCode, IntPtr context, IntPtr reserved, IntPtr buffer)
		{
			DiagnosticsNativeMethods.CriticalTraceHandle criticalTraceHandle = null;
			int currentProcessId = DiagnosticsNativeMethods.GetCurrentProcessId();
			try
			{
				if (requestCode == 4)
				{
					try
					{
						criticalTraceHandle = DiagnosticsNativeMethods.CriticalTraceHandle.Attach(buffer);
					}
					catch (Win32Exception ex)
					{
						NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
						Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TraceLoggerFailed, new object[]
						{
							this.name,
							this.providerGuid,
							currentProcessId,
							ex
						});
						return 0U;
					}
					DiagnosticsNativeMethods.CriticalTraceHandle criticalTraceHandle2 = this.sessionHandle;
					this.sessionHandle = criticalTraceHandle;
					criticalTraceHandle = criticalTraceHandle2;
					Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TraceLoggerStarted, new object[]
					{
						this.name,
						this.providerGuid,
						currentProcessId
					});
				}
				else if (requestCode == 5)
				{
					criticalTraceHandle = this.sessionHandle;
					this.sessionHandle = null;
					Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TraceLoggerStopped, new object[]
					{
						this.name,
						this.providerGuid,
						currentProcessId
					});
				}
			}
			finally
			{
				if (criticalTraceHandle != null)
				{
					criticalTraceHandle.Dispose();
					criticalTraceHandle = null;
				}
			}
			return 0U;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000C63C File Offset: 0x0000A83C
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.Stop();
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000C647 File Offset: 0x0000A847
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EtwBinaryLogger>(this);
		}

		// Token: 0x040004CB RID: 1227
		internal const int TraceMessageMaxSize = 8064;

		// Token: 0x040004CC RID: 1228
		private readonly string name;

		// Token: 0x040004CD RID: 1229
		private readonly Guid providerGuid;

		// Token: 0x040004CE RID: 1230
		private DiagnosticsNativeMethods.CriticalTraceHandle sessionHandle;

		// Token: 0x040004CF RID: 1231
		private DiagnosticsNativeMethods.CriticalTraceRegistrationHandle registrationHandle;
	}
}
