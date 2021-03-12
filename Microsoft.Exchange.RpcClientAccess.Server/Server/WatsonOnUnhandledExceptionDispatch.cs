using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200004A RID: 74
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class WatsonOnUnhandledExceptionDispatch : BaseObject, IRpcDispatch, IDisposable
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000E5A5 File Offset: 0x0000C7A5
		public WatsonOnUnhandledExceptionDispatch(IRpcDispatch innerDispatch) : this(innerDispatch, null, RpcDispatch.BuildDefaultConnectAuxOutBuffer())
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
		public WatsonOnUnhandledExceptionDispatch(IRpcDispatch innerDispatch, Action<bool, Exception> exceptionHandlerAction, ArraySegment<byte> defaultConnectAuxOutBuffer)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.innerDispatch = innerDispatch;
				this.exceptionHandlerAction = exceptionHandlerAction;
				this.defaultConnectAuxOutBuffer = defaultConnectAuxOutBuffer;
				disposeGuard.Success();
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000E60C File Offset: 0x0000C80C
		public static bool TryHandleGreyExceptionGuardFilterNoCustomHandlerAction(object exceptionObject)
		{
			Exception ex = exceptionObject as Exception;
			if (!WatsonOnUnhandledExceptionDispatch.IsExceptionInteresting(ex))
			{
				return false;
			}
			bool flag = !GrayException.IsGrayException(ex) && !WatsonOnUnhandledExceptionDispatch.IsLocalGrayException(ex);
			WatsonOnUnhandledExceptionDispatch.HandleGreyExceptionTakeWatsonActions(flag, ex);
			return !flag;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000E64C File Offset: 0x0000C84C
		public bool TryHandleGreyExceptionGuardFilter(object exceptionObject)
		{
			Exception ex = exceptionObject as Exception;
			if (!WatsonOnUnhandledExceptionDispatch.IsExceptionInteresting(ex))
			{
				return false;
			}
			bool flag = !GrayException.IsGrayException(ex) && !WatsonOnUnhandledExceptionDispatch.IsLocalGrayException(ex);
			this.exceptionHandlerAction(flag, ex);
			return !flag;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000E690 File Offset: 0x0000C890
		public static bool IsUnderWatsonSuiteTests
		{
			get
			{
				bool result = false;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(4083559741U, ref result);
				return result;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		public static bool IsUnderWatsonThrottlingTests
		{
			get
			{
				bool result = false;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(3278253373U, ref result);
				return result;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000E6D5 File Offset: 0x0000C8D5
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<WatsonOnUnhandledExceptionDispatch>(this);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000E6DD File Offset: 0x0000C8DD
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.innerDispatch);
			base.InternalDispose();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		private static void HandleGreyExceptionTakeWatsonActions(bool shouldTerminateProcess, Exception exception)
		{
			try
			{
				if (ExWatson.KillProcessAfterWatson || WatsonOnUnhandledExceptionDispatch.IsUnderWatsonSuiteTests)
				{
					ReportOptions reportOptions = ReportOptions.DoNotFreezeThreads;
					if (shouldTerminateProcess)
					{
						reportOptions |= ReportOptions.ReportTerminateAfterSend;
					}
					ProtocolLog.LogWatsonFailure(shouldTerminateProcess, exception);
					ExWatson.SendReport(exception, reportOptions, null);
				}
				else
				{
					ProtocolLog.LogWatsonFailure(shouldTerminateProcess, exception);
					ExTraceGlobals.UnhandledExceptionTracer.TraceInformation<Exception>(0, Activity.TraceId, "ExAEDbg will handle the crash after creation of a Watson report", exception);
					ExWatson.SendReportAndCrashOnAnotherThread(exception);
				}
			}
			finally
			{
				if (shouldTerminateProcess)
				{
					WatsonOnUnhandledExceptionDispatch.KillCurrentProcess();
				}
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000E764 File Offset: 0x0000C964
		private static void DoNothing(object unused)
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000E768 File Offset: 0x0000C968
		private static bool IsExceptionInteresting(object exGeneric)
		{
			Exception ex = exGeneric as Exception;
			if (ex == null)
			{
				return true;
			}
			bool flag = ex is RpcServiceException;
			return !flag;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000E790 File Offset: 0x0000C990
		private static void KillCurrentProcess()
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest(3546688829U);
			try
			{
				Process.GetCurrentProcess().Kill();
			}
			catch (Win32Exception)
			{
			}
			Environment.Exit(-559034355);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
		private static bool IsLocalGrayException(Exception exception)
		{
			return exception is OverflowException || (exception is MapiPermanentException || exception is MapiRetryableException) || (exception is DataValidationException || exception is DataSourceOperationException || exception is DataSourceTransientException);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000E811 File Offset: 0x0000CA11
		private static void Guard(TryDelegate body, FilterDelegate exceptionFilter)
		{
			ILUtil.DoTryFilterCatch(body, exceptionFilter, new CatchDelegate(null, (UIntPtr)ldftn(DoNothing)));
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000E826 File Offset: 0x0000CA26
		private FilterDelegate GetExceptionFilter()
		{
			if (this.exceptionHandlerAction != null)
			{
				return new FilterDelegate(this, (UIntPtr)ldftn(TryHandleGreyExceptionGuardFilter));
			}
			return new FilterDelegate(null, (UIntPtr)ldftn(TryHandleGreyExceptionGuardFilterNoCustomHandlerAction));
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000E86C File Offset: 0x0000CA6C
		int IRpcDispatch.MaximumConnections
		{
			get
			{
				WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass1 CS$<>8__locals1 = new WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass1();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.localResult = 0;
				WatsonOnUnhandledExceptionDispatch.Guard(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.RpcClientAccess.Server.IRpcDispatch.get_MaximumConnections>b__0)), this.GetExceptionFilter());
				return CS$<>8__locals1.localResult;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
		void IRpcDispatch.ReportBytesRead(long bytesRead, long uncompressedBytesRead)
		{
			WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass4 CS$<>8__locals1 = new WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass4();
			CS$<>8__locals1.bytesRead = bytesRead;
			CS$<>8__locals1.uncompressedBytesRead = uncompressedBytesRead;
			CS$<>8__locals1.<>4__this = this;
			WatsonOnUnhandledExceptionDispatch.Guard(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.RpcClientAccess.Server.IRpcDispatch.ReportBytesRead>b__3)), this.GetExceptionFilter());
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000E938 File Offset: 0x0000CB38
		void IRpcDispatch.ReportBytesWritten(long bytesWritten, long uncompressedBytesWritten)
		{
			WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass7 CS$<>8__locals1 = new WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass7();
			CS$<>8__locals1.bytesWritten = bytesWritten;
			CS$<>8__locals1.uncompressedBytesWritten = uncompressedBytesWritten;
			CS$<>8__locals1.<>4__this = this;
			WatsonOnUnhandledExceptionDispatch.Guard(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.RpcClientAccess.Server.IRpcDispatch.ReportBytesWritten>b__6)), this.GetExceptionFilter());
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000EA50 File Offset: 0x0000CC50
		int IRpcDispatch.Connect(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, out IntPtr contextHandle, string userDn, int flags, int connectionModulus, int codePage, int stringLocale, int sortLocale, out TimeSpan pollsMax, out int retryCount, out TimeSpan retryDelay, out string dnPrefix, out string displayName, short[] clientVersion, ArraySegment<byte> auxIn, ArraySegment<byte> auxOut, out int sizeAuxOut, IStandardBudget budget)
		{
			WatsonOnUnhandledExceptionDispatch.<>c__DisplayClassa CS$<>8__locals1 = new WatsonOnUnhandledExceptionDispatch.<>c__DisplayClassa();
			CS$<>8__locals1.protocolRequestInfo = protocolRequestInfo;
			CS$<>8__locals1.clientBinding = clientBinding;
			CS$<>8__locals1.userDn = userDn;
			CS$<>8__locals1.flags = flags;
			CS$<>8__locals1.connectionModulus = connectionModulus;
			CS$<>8__locals1.codePage = codePage;
			CS$<>8__locals1.stringLocale = stringLocale;
			CS$<>8__locals1.sortLocale = sortLocale;
			CS$<>8__locals1.clientVersion = clientVersion;
			CS$<>8__locals1.auxIn = auxIn;
			CS$<>8__locals1.auxOut = auxOut;
			CS$<>8__locals1.budget = budget;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = null;
			CS$<>8__locals1.localContextHandle = IntPtr.Zero;
			CS$<>8__locals1.localDnPrefix = null;
			CS$<>8__locals1.localDisplayName = null;
			CS$<>8__locals1.localPollsMax = TimeSpan.Zero;
			CS$<>8__locals1.localRetryCount = 0;
			CS$<>8__locals1.localRetryDelay = TimeSpan.Zero;
			CS$<>8__locals1.localSizeAuxOut = 0;
			if (CS$<>8__locals1.auxIn.Array == null)
			{
				throw new InvalidOperationException("Invalid auxIn ArraySegment; Array can't be null");
			}
			if (CS$<>8__locals1.auxOut.Array == null)
			{
				throw new InvalidOperationException("Invalid auxOut ArraySegment; Array can't be null");
			}
			WatsonOnUnhandledExceptionDispatch.Guard(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.RpcClientAccess.Server.IRpcDispatch.Connect>b__9)), this.GetExceptionFilter());
			if ((CS$<>8__locals1.localErrorCode == null || CS$<>8__locals1.localSizeAuxOut == 0) && this.defaultConnectAuxOutBuffer.Count <= CS$<>8__locals1.auxOut.Count)
			{
				Buffer.BlockCopy(this.defaultConnectAuxOutBuffer.Array, this.defaultConnectAuxOutBuffer.Offset, CS$<>8__locals1.auxOut.Array, CS$<>8__locals1.auxOut.Offset, this.defaultConnectAuxOutBuffer.Count);
				CS$<>8__locals1.localSizeAuxOut = this.defaultConnectAuxOutBuffer.Count;
			}
			contextHandle = CS$<>8__locals1.localContextHandle;
			pollsMax = CS$<>8__locals1.localPollsMax;
			retryCount = CS$<>8__locals1.localRetryCount;
			retryDelay = CS$<>8__locals1.localRetryDelay;
			dnPrefix = CS$<>8__locals1.localDnPrefix;
			displayName = CS$<>8__locals1.localDisplayName;
			sizeAuxOut = CS$<>8__locals1.localSizeAuxOut;
			int? localErrorCode = CS$<>8__locals1.localErrorCode;
			if (localErrorCode == null)
			{
				return -2147467259;
			}
			return localErrorCode.GetValueOrDefault();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000EC80 File Offset: 0x0000CE80
		int IRpcDispatch.Disconnect(ProtocolRequestInfo protocolRequestInfo, ref IntPtr contextHandle, bool rundown)
		{
			WatsonOnUnhandledExceptionDispatch.<>c__DisplayClassd CS$<>8__locals1 = new WatsonOnUnhandledExceptionDispatch.<>c__DisplayClassd();
			CS$<>8__locals1.protocolRequestInfo = protocolRequestInfo;
			CS$<>8__locals1.rundown = rundown;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = -2147467259;
			CS$<>8__locals1.localContextHandle = contextHandle;
			WatsonOnUnhandledExceptionDispatch.Guard(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.RpcClientAccess.Server.IRpcDispatch.Disconnect>b__c)), this.GetExceptionFilter());
			contextHandle = CS$<>8__locals1.localContextHandle;
			return CS$<>8__locals1.localErrorCode;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000ED98 File Offset: 0x0000CF98
		int IRpcDispatch.Execute(ProtocolRequestInfo protocolRequestInfo, ref IntPtr contextHandle, IList<ArraySegment<byte>> ropInArray, ArraySegment<byte> ropOut, out int sizeRopOut, ArraySegment<byte> auxIn, ArraySegment<byte> auxOut, out int sizeAuxOut, bool isFake, out byte[] fakeOut)
		{
			WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass10 CS$<>8__locals1 = new WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass10();
			CS$<>8__locals1.protocolRequestInfo = protocolRequestInfo;
			CS$<>8__locals1.ropInArray = ropInArray;
			CS$<>8__locals1.ropOut = ropOut;
			CS$<>8__locals1.auxIn = auxIn;
			CS$<>8__locals1.auxOut = auxOut;
			CS$<>8__locals1.isFake = isFake;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = -2147467259;
			CS$<>8__locals1.localContextHandle = contextHandle;
			CS$<>8__locals1.localSizeRopOut = 0;
			CS$<>8__locals1.localSizeAuxOut = 0;
			CS$<>8__locals1.localFakeOut = null;
			if (CS$<>8__locals1.ropOut.Array == null)
			{
				throw new InvalidOperationException("Invalid ropOut ArraySegment; Array can't be null");
			}
			if (CS$<>8__locals1.auxIn.Array == null)
			{
				throw new InvalidOperationException("Invalid auxIn ArraySegment; Array can't be null");
			}
			if (CS$<>8__locals1.auxOut.Array == null)
			{
				throw new InvalidOperationException("Invalid auxOut ArraySegment; Array can't be null");
			}
			WatsonOnUnhandledExceptionDispatch.Guard(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.RpcClientAccess.Server.IRpcDispatch.Execute>b__f)), this.GetExceptionFilter());
			contextHandle = CS$<>8__locals1.localContextHandle;
			sizeRopOut = CS$<>8__locals1.localSizeRopOut;
			sizeAuxOut = CS$<>8__locals1.localSizeAuxOut;
			fakeOut = CS$<>8__locals1.localFakeOut;
			return CS$<>8__locals1.localErrorCode;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000EECC File Offset: 0x0000D0CC
		int IRpcDispatch.NotificationConnect(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, out IntPtr asynchronousContextHandle)
		{
			WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass13 CS$<>8__locals1 = new WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass13();
			CS$<>8__locals1.protocolRequestInfo = protocolRequestInfo;
			CS$<>8__locals1.contextHandle = contextHandle;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = -2147467259;
			CS$<>8__locals1.localAsynchronousContextHandle = IntPtr.Zero;
			WatsonOnUnhandledExceptionDispatch.Guard(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.RpcClientAccess.Server.IRpcDispatch.NotificationConnect>b__12)), this.GetExceptionFilter());
			asynchronousContextHandle = CS$<>8__locals1.localAsynchronousContextHandle;
			return CS$<>8__locals1.localErrorCode;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000EF60 File Offset: 0x0000D160
		int IRpcDispatch.Dummy(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding)
		{
			WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass16 CS$<>8__locals1 = new WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass16();
			CS$<>8__locals1.protocolRequestInfo = protocolRequestInfo;
			CS$<>8__locals1.clientBinding = clientBinding;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = -2147467259;
			WatsonOnUnhandledExceptionDispatch.Guard(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.RpcClientAccess.Server.IRpcDispatch.Dummy>b__15)), this.GetExceptionFilter());
			return CS$<>8__locals1.localErrorCode;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		void IRpcDispatch.NotificationWait(ProtocolRequestInfo protocolRequestInfo, IntPtr asynchronousContextHandle, uint flags, Action<bool, int> completion)
		{
			WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass19 CS$<>8__locals1 = new WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass19();
			CS$<>8__locals1.protocolRequestInfo = protocolRequestInfo;
			CS$<>8__locals1.asynchronousContextHandle = asynchronousContextHandle;
			CS$<>8__locals1.flags = flags;
			CS$<>8__locals1.completion = completion;
			CS$<>8__locals1.<>4__this = this;
			WatsonOnUnhandledExceptionDispatch.Guard(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.RpcClientAccess.Server.IRpcDispatch.NotificationWait>b__18)), this.GetExceptionFilter());
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000F054 File Offset: 0x0000D254
		void IRpcDispatch.DroppedConnection(IntPtr asynchronousContextHandle)
		{
			WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass1c CS$<>8__locals1 = new WatsonOnUnhandledExceptionDispatch.<>c__DisplayClass1c();
			CS$<>8__locals1.asynchronousContextHandle = asynchronousContextHandle;
			CS$<>8__locals1.<>4__this = this;
			WatsonOnUnhandledExceptionDispatch.Guard(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.RpcClientAccess.Server.IRpcDispatch.DroppedConnection>b__1b)), this.GetExceptionFilter());
		}

		// Token: 0x04000165 RID: 357
		private const uint WatsonSuiteSetWatsonTestInProgress = 4083559741U;

		// Token: 0x04000166 RID: 358
		private const uint WatsonSuiteSetWatsonThrottlingTestInProgress = 3278253373U;

		// Token: 0x04000167 RID: 359
		private const uint WatsonSuiteProcessKilled = 3546688829U;

		// Token: 0x04000168 RID: 360
		private readonly IRpcDispatch innerDispatch;

		// Token: 0x04000169 RID: 361
		private readonly Action<bool, Exception> exceptionHandlerAction;

		// Token: 0x0400016A RID: 362
		private readonly ArraySegment<byte> defaultConnectAuxOutBuffer;
	}
}
