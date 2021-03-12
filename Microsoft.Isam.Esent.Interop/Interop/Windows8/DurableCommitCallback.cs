using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Isam.Esent.Interop.Implementation;

namespace Microsoft.Isam.Esent.Interop.Windows8
{
	// Token: 0x020000A3 RID: 163
	public class DurableCommitCallback : EsentResource
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x00011144 File Offset: 0x0000F344
		public DurableCommitCallback(JET_INSTANCE instance, JET_PFNDURABLECOMMITCALLBACK wrappedCallback)
		{
			this.instance = instance;
			this.wrappedCallback = wrappedCallback;
			this.wrapperCallback = new NATIVE_JET_PFNDURABLECOMMITCALLBACK(this.NativeDurableCommitCallback);
			if (this.wrappedCallback != null)
			{
				RuntimeHelpers.PrepareMethod(this.wrappedCallback.Method.MethodHandle);
			}
			RuntimeHelpers.PrepareMethod(typeof(DurableCommitCallback).GetMethod("NativeDurableCommitCallback", BindingFlags.Instance | BindingFlags.NonPublic).MethodHandle);
			InstanceParameters instanceParameters = new InstanceParameters(this.instance);
			instanceParameters.SetDurableCommitCallback(this.wrapperCallback);
			base.ResourceWasAllocated();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000111D4 File Offset: 0x0000F3D4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "DurableCommitCallback({0})", new object[]
			{
				this.instance.ToString()
			});
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001120C File Offset: 0x0000F40C
		public void End()
		{
			base.CheckObjectIsNotDisposed();
			this.ReleaseResource();
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001121A File Offset: 0x0000F41A
		protected override void ReleaseResource()
		{
			this.instance = JET_INSTANCE.Nil;
			this.wrappedCallback = null;
			this.wrapperCallback = null;
			base.ResourceWasReleased();
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001123C File Offset: 0x0000F43C
		private JET_err NativeDurableCommitCallback(IntPtr instance, ref NATIVE_COMMIT_ID commitIdSeen, uint grbit)
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
					JET_COMMIT_ID pCommitIdSeen = new JET_COMMIT_ID(commitIdSeen);
					result = this.wrappedCallback(rhs, pCommitIdSeen, (DurableCommitCallbackGrbit)grbit);
				}
			}
			catch (Exception exception)
			{
				JetApi.ReportUnhandledException(exception, "Unhandled exception during NativeDurableCommitCallback");
				result = JET_err.CallbackFailed;
			}
			return result;
		}

		// Token: 0x04000335 RID: 821
		private static readonly TraceSwitch TraceSwitch = new TraceSwitch("ESENT DurableCommitCallback", "Wrapper around unmanaged ESENT durable commit callback");

		// Token: 0x04000336 RID: 822
		private JET_INSTANCE instance;

		// Token: 0x04000337 RID: 823
		private JET_PFNDURABLECOMMITCALLBACK wrappedCallback;

		// Token: 0x04000338 RID: 824
		private NATIVE_JET_PFNDURABLECOMMITCALLBACK wrapperCallback;
	}
}
