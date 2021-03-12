using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002B1 RID: 689
	internal sealed class StatusCallbackWrapper
	{
		// Token: 0x06000C4D RID: 3149 RVA: 0x000189FE File Offset: 0x00016BFE
		static StatusCallbackWrapper()
		{
			RuntimeHelpers.PrepareMethod(typeof(StatusCallbackWrapper).GetMethod("CallbackImpl", BindingFlags.Instance | BindingFlags.NonPublic).MethodHandle);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00018A34 File Offset: 0x00016C34
		public StatusCallbackWrapper(JET_PFNSTATUS wrappedCallback)
		{
			this.wrappedCallback = wrappedCallback;
			this.nativeCallback = ((wrappedCallback != null) ? new NATIVE_PFNSTATUS(this.CallbackImpl) : null);
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00018A5B File Offset: 0x00016C5B
		public NATIVE_PFNSTATUS NativeCallback
		{
			get
			{
				return this.nativeCallback;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00018A63 File Offset: 0x00016C63
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x00018A6B File Offset: 0x00016C6B
		private Exception SavedException { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00018A74 File Offset: 0x00016C74
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x00018A7C File Offset: 0x00016C7C
		private bool ThreadWasAborted { get; set; }

		// Token: 0x06000C54 RID: 3156 RVA: 0x00018A85 File Offset: 0x00016C85
		public void ThrowSavedException()
		{
			if (this.ThreadWasAborted)
			{
				Thread.CurrentThread.Abort();
			}
			if (this.SavedException != null)
			{
				throw this.SavedException;
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00018AA8 File Offset: 0x00016CA8
		private JET_err CallbackImpl(IntPtr nativeSesid, uint nativeSnp, uint nativeSnt, IntPtr nativeData)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			JET_err result;
			try
			{
				JET_SESID sesid = new JET_SESID
				{
					Value = nativeSesid
				};
				object managedData = CallbackDataConverter.GetManagedData(nativeData, (JET_SNP)nativeSnp, (JET_SNT)nativeSnt);
				result = this.wrappedCallback(sesid, (JET_SNP)nativeSnp, (JET_SNT)nativeSnt, managedData);
			}
			catch (ThreadAbortException)
			{
				this.ThreadWasAborted = true;
				LibraryHelpers.ThreadResetAbort();
				result = JET_err.CallbackFailed;
			}
			catch (Exception savedException)
			{
				this.SavedException = savedException;
				result = JET_err.CallbackFailed;
			}
			return result;
		}

		// Token: 0x040007D4 RID: 2004
		private static readonly TraceSwitch TraceSwitch = new TraceSwitch("ESENT StatusCallbackWrapper", "Wrapper around unmanaged ESENT status callback");

		// Token: 0x040007D5 RID: 2005
		private readonly JET_PFNSTATUS wrappedCallback;

		// Token: 0x040007D6 RID: 2006
		private readonly NATIVE_PFNSTATUS nativeCallback;
	}
}
