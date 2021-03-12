using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using Microsoft.Win32;
using Windows.Foundation.Diagnostics;

namespace System.Threading.Tasks
{
	// Token: 0x02000553 RID: 1363
	[FriendAccessAllowed]
	internal static class AsyncCausalityTracer
	{
		// Token: 0x0600411E RID: 16670 RVA: 0x000F1EFB File Offset: 0x000F00FB
		internal static void EnableToETW(bool enabled)
		{
			if (enabled)
			{
				AsyncCausalityTracer.f_LoggingOn |= AsyncCausalityTracer.Loggers.ETW;
				return;
			}
			AsyncCausalityTracer.f_LoggingOn &= ~AsyncCausalityTracer.Loggers.ETW;
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x000F1F1D File Offset: 0x000F011D
		[FriendAccessAllowed]
		internal static bool LoggingOn
		{
			[FriendAccessAllowed]
			get
			{
				return AsyncCausalityTracer.f_LoggingOn > (AsyncCausalityTracer.Loggers)0;
			}
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x000F1F28 File Offset: 0x000F0128
		[SecuritySafeCritical]
		static AsyncCausalityTracer()
		{
			if (!Environment.IsWinRTSupported)
			{
				return;
			}
			string activatableClassId = "Windows.Foundation.Diagnostics.AsyncCausalityTracer";
			Guid guid = new Guid(1350896422, 9854, 17691, 168, 144, 171, 106, 55, 2, 69, 238);
			object obj = null;
			try
			{
				int num = Microsoft.Win32.UnsafeNativeMethods.RoGetActivationFactory(activatableClassId, ref guid, out obj);
				if (num >= 0 && obj != null)
				{
					AsyncCausalityTracer.s_TracerFactory = (IAsyncCausalityTracerStatics)obj;
					EventRegistrationToken eventRegistrationToken = AsyncCausalityTracer.s_TracerFactory.add_TracingStatusChanged(new EventHandler<TracingStatusChangedEventArgs>(AsyncCausalityTracer.TracingStatusChangedHandler));
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x000F1FFC File Offset: 0x000F01FC
		[SecuritySafeCritical]
		private static void TracingStatusChangedHandler(object sender, TracingStatusChangedEventArgs args)
		{
			if (args.Enabled)
			{
				AsyncCausalityTracer.f_LoggingOn |= AsyncCausalityTracer.Loggers.CausalityTracer;
				return;
			}
			AsyncCausalityTracer.f_LoggingOn &= ~AsyncCausalityTracer.Loggers.CausalityTracer;
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x000F2024 File Offset: 0x000F0224
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCreation(CausalityTraceLevel traceLevel, int taskId, string operationName, ulong relatedContext)
		{
			try
			{
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers)0)
				{
					TplEtwProvider.Log.TraceOperationBegin(taskId, operationName, (long)relatedContext);
				}
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) != (AsyncCausalityTracer.Loggers)0)
				{
					AsyncCausalityTracer.s_TracerFactory.TraceOperationCreation((CausalityTraceLevel)traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint)taskId), operationName, relatedContext);
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x000F2084 File Offset: 0x000F0284
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCompletion(CausalityTraceLevel traceLevel, int taskId, AsyncCausalityStatus status)
		{
			try
			{
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers)0)
				{
					TplEtwProvider.Log.TraceOperationEnd(taskId, status);
				}
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) != (AsyncCausalityTracer.Loggers)0)
				{
					AsyncCausalityTracer.s_TracerFactory.TraceOperationCompletion((CausalityTraceLevel)traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint)taskId), (AsyncCausalityStatus)status);
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x000F20E4 File Offset: 0x000F02E4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationRelation(CausalityTraceLevel traceLevel, int taskId, CausalityRelation relation)
		{
			try
			{
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers)0)
				{
					TplEtwProvider.Log.TraceOperationRelation(taskId, relation);
				}
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) != (AsyncCausalityTracer.Loggers)0)
				{
					AsyncCausalityTracer.s_TracerFactory.TraceOperationRelation((CausalityTraceLevel)traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint)taskId), (CausalityRelation)relation);
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x000F2144 File Offset: 0x000F0344
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkStart(CausalityTraceLevel traceLevel, int taskId, CausalitySynchronousWork work)
		{
			try
			{
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers)0)
				{
					TplEtwProvider.Log.TraceSynchronousWorkBegin(taskId, work);
				}
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) != (AsyncCausalityTracer.Loggers)0)
				{
					AsyncCausalityTracer.s_TracerFactory.TraceSynchronousWorkStart((CausalityTraceLevel)traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint)taskId), (CausalitySynchronousWork)work);
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x000F21A4 File Offset: 0x000F03A4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkCompletion(CausalityTraceLevel traceLevel, CausalitySynchronousWork work)
		{
			try
			{
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers)0)
				{
					TplEtwProvider.Log.TraceSynchronousWorkEnd(work);
				}
				if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) != (AsyncCausalityTracer.Loggers)0)
				{
					AsyncCausalityTracer.s_TracerFactory.TraceSynchronousWorkCompletion((CausalityTraceLevel)traceLevel, CausalitySource.Library, (CausalitySynchronousWork)work);
				}
			}
			catch (Exception ex)
			{
				AsyncCausalityTracer.LogAndDisable(ex);
			}
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x000F21F8 File Offset: 0x000F03F8
		private static void LogAndDisable(Exception ex)
		{
			AsyncCausalityTracer.f_LoggingOn = (AsyncCausalityTracer.Loggers)0;
			Debugger.Log(0, "AsyncCausalityTracer", ex.ToString());
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x000F2211 File Offset: 0x000F0411
		private static ulong GetOperationId(uint taskId)
		{
			return (ulong)(((long)AppDomain.CurrentDomain.Id << 32) + (long)((ulong)taskId));
		}

		// Token: 0x04001AD1 RID: 6865
		private static readonly Guid s_PlatformId = new Guid(1258385830U, 62416, 16800, 155, 51, 2, 85, 6, 82, 185, 149);

		// Token: 0x04001AD2 RID: 6866
		private const CausalitySource s_CausalitySource = CausalitySource.Library;

		// Token: 0x04001AD3 RID: 6867
		private static IAsyncCausalityTracerStatics s_TracerFactory;

		// Token: 0x04001AD4 RID: 6868
		private static AsyncCausalityTracer.Loggers f_LoggingOn;

		// Token: 0x02000BF0 RID: 3056
		[Flags]
		private enum Loggers : byte
		{
			// Token: 0x040035F9 RID: 13817
			CausalityTracer = 1,
			// Token: 0x040035FA RID: 13818
			ETW = 2
		}
	}
}
