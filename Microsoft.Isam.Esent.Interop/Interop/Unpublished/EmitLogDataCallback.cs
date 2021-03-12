using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Isam.Esent.Interop.Implementation;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000009 RID: 9
	public class EmitLogDataCallback : EsentResource
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000025CC File Offset: 0x000007CC
		public EmitLogDataCallback(JET_INSTANCE instance, JET_PFNEMITLOGDATA wrappedCallback, IntPtr emitContext)
		{
			this.instance = instance;
			this.wrappedCallback = wrappedCallback;
			this.wrapperCallback = new NATIVE_JET_PFNEMITLOGDATA(this.NativeEmitLogDataCallback);
			this.bytesGranule = new byte[1024];
			if (this.wrappedCallback != null)
			{
				RuntimeHelpers.PrepareMethod(this.wrappedCallback.Method.MethodHandle);
			}
			RuntimeHelpers.PrepareMethod(typeof(EmitLogDataCallback).GetMethod("NativeEmitLogDataCallback", BindingFlags.Instance | BindingFlags.NonPublic).MethodHandle);
			InstanceParameters instanceParameters = new InstanceParameters(this.instance);
			instanceParameters.SetEmitLogDataCallback(this.wrapperCallback);
			instanceParameters.EmitLogDataCallbackCtx = emitContext;
			base.ResourceWasAllocated();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002674 File Offset: 0x00000874
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "EmitLogDataCallback({0})", new object[]
			{
				this.instance.ToString()
			});
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026AC File Offset: 0x000008AC
		public void End()
		{
			base.CheckObjectIsNotDisposed();
			this.ReleaseResource();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026BA File Offset: 0x000008BA
		protected override void ReleaseResource()
		{
			this.instance = JET_INSTANCE.Nil;
			base.ResourceWasReleased();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026D0 File Offset: 0x000008D0
		private JET_err NativeEmitLogDataCallback(IntPtr instance, ref NATIVE_EMITDATACTX emitLogDataCtx, IntPtr logData, uint logDataSize, IntPtr callbackCtx)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			JET_err result;
			try
			{
				JET_INSTANCE rhs = new JET_INSTANCE
				{
					Value = instance
				};
				if (this.instance != rhs)
				{
					result = JET_err.CallbackFailed;
				}
				else
				{
					JET_EMITDATACTX jet_EMITDATACTX = new JET_EMITDATACTX();
					jet_EMITDATACTX.SetFromNative(ref emitLogDataCtx);
					if (logDataSize > 0U)
					{
						if (this.bytesGranule.Length < (int)logDataSize)
						{
							this.bytesGranule = new byte[logDataSize];
						}
						Marshal.Copy(logData, this.bytesGranule, 0, (int)logDataSize);
					}
					result = this.wrappedCallback(rhs, jet_EMITDATACTX, this.bytesGranule, (int)logDataSize, callbackCtx);
				}
			}
			catch (Exception exception)
			{
				JetApi.ReportUnhandledException(exception, "Unhandled exception during NativeEmitLogDataCallback");
				result = JET_err.CallbackFailed;
			}
			return result;
		}

		// Token: 0x04000023 RID: 35
		private static readonly TraceSwitch TraceSwitch = new TraceSwitch("ESENT EmitLogDataCallback", "Wrapper around unmanaged ESENT granule callback");

		// Token: 0x04000024 RID: 36
		private JET_INSTANCE instance;

		// Token: 0x04000025 RID: 37
		private JET_PFNEMITLOGDATA wrappedCallback;

		// Token: 0x04000026 RID: 38
		private NATIVE_JET_PFNEMITLOGDATA wrapperCallback;

		// Token: 0x04000027 RID: 39
		private byte[] bytesGranule;
	}
}
