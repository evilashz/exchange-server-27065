using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002D9 RID: 729
	internal sealed class JetCallbackWrapper
	{
		// Token: 0x06000D52 RID: 3410 RVA: 0x0001AD5E File Offset: 0x00018F5E
		static JetCallbackWrapper()
		{
			RuntimeHelpers.PrepareMethod(typeof(StatusCallbackWrapper).GetMethod("CallbackImpl", BindingFlags.Instance | BindingFlags.NonPublic).MethodHandle);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0001AD94 File Offset: 0x00018F94
		public JetCallbackWrapper(JET_CALLBACK callback)
		{
			this.wrappedCallback = new WeakReference(callback);
			this.nativeCallback = new NATIVE_CALLBACK(this.CallbackImpl);
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0001ADBA File Offset: 0x00018FBA
		public bool IsAlive
		{
			get
			{
				return this.wrappedCallback.IsAlive;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0001ADC7 File Offset: 0x00018FC7
		public NATIVE_CALLBACK NativeCallback
		{
			get
			{
				return this.nativeCallback;
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0001ADCF File Offset: 0x00018FCF
		public bool IsWrapping(JET_CALLBACK callback)
		{
			return callback.Equals(this.wrappedCallback.Target);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0001ADE4 File Offset: 0x00018FE4
		private JET_err CallbackImpl(IntPtr nativeSesid, uint nativeDbid, IntPtr nativeTableid, uint nativeCbtyp, IntPtr arg1, IntPtr arg2, IntPtr nativeContext, IntPtr unused)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			JET_err result;
			try
			{
				JET_SESID sesid = new JET_SESID
				{
					Value = nativeSesid
				};
				JET_DBID dbid = new JET_DBID
				{
					Value = nativeDbid
				};
				JET_TABLEID tableid = new JET_TABLEID
				{
					Value = nativeTableid
				};
				JET_CALLBACK jet_CALLBACK = (JET_CALLBACK)this.wrappedCallback.Target;
				result = jet_CALLBACK(sesid, dbid, tableid, (JET_cbtyp)nativeCbtyp, null, null, nativeContext, IntPtr.Zero);
			}
			catch (Exception)
			{
				result = JET_err.CallbackFailed;
			}
			return result;
		}

		// Token: 0x040008FE RID: 2302
		private static readonly TraceSwitch TraceSwitch = new TraceSwitch("ESENT JetCallbackWrapper", "Wrapper around unmanaged ESENT callback");

		// Token: 0x040008FF RID: 2303
		private readonly WeakReference wrappedCallback;

		// Token: 0x04000900 RID: 2304
		private readonly NATIVE_CALLBACK nativeCallback;
	}
}
