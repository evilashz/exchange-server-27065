using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.EseBack
{
	// Token: 0x02000102 RID: 258
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal sealed class CLogShipContext : SafeHandle
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00002444 File Offset: 0x00001844
		public unsafe static uint Open(string serverName, string sgGuid, string clientId, string sgBaseName, string targetDir, [MarshalAs(UnmanagedType.U1)] bool fCircularLogging, ReplicaType replicaType, uint rpcTimeoutMsecs, out uint dwExtError, out CLogShipContext logshipContext)
		{
			void* ptr = null;
			CLogShipContext clogShipContext = null;
			dwExtError = 0U;
			ExTraceGlobals.CLogShipContextTracer.TraceDebug(0L, "enter CLogShipContext::Open");
			int num;
			if (!string.IsNullOrEmpty(serverName) && !string.IsNullOrEmpty(sgGuid) && !string.IsNullOrEmpty(sgBaseName) && !string.IsNullOrEmpty(targetDir))
			{
				ushort* ptr2 = (ushort*)Marshal.StringToHGlobalUni(serverName).ToPointer();
				ushort* ptr3 = (ushort*)Marshal.StringToHGlobalUni(sgGuid).ToPointer();
				ushort* ptr4 = (ushort*)Marshal.StringToHGlobalUni(clientId).ToPointer();
				ushort* ptr5 = (ushort*)Marshal.StringToHGlobalUni(sgBaseName).ToPointer();
				ushort* ptr6 = (ushort*)Marshal.StringToHGlobalUni(targetDir).ToPointer();
				object[] args = new object[]
				{
					serverName,
					sgGuid,
					sgBaseName,
					targetDir,
					fCircularLogging
				};
				ExTraceGlobals.CLogShipContextTracer.TraceDebug(0L, "HrLogShipOpen with serverName={0}, sgGuid={1}, sgBaseName={2}, targetDir={3}, fCircularLogging={4}", args);
				clogShipContext = new CLogShipContext();
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					num = <Module>.HrESELogShipOpenEx(ptr2, (ushort*)(&<Module>.?A0x885d7a58.g_wszEndpointAnnotation), ptr3, ptr4, ptr5, ptr6, fCircularLogging ? 1 : 0, replicaType, rpcTimeoutMsecs, &ptr);
					if (null == ptr)
					{
						ExTraceGlobals.CLogShipContextTracer.TraceDebug<string>(0L, "Failed to create CLogShipContext for SG {0}. Attempting connect to multi-process server", sgGuid);
						ushort* wszLogShipServer = ptr2;
						ushort* ptr7 = ptr3;
						num = <Module>.HrESELogShipOpenEx(wszLogShipServer, ptr7, ptr7, ptr4, ptr5, ptr6, fCircularLogging ? 1 : 0, replicaType, rpcTimeoutMsecs, &ptr);
						if (null == ptr)
						{
							goto IL_15A;
						}
					}
					IntPtr handle = (IntPtr)ptr;
					clogShipContext.SetHandle(handle);
					ExTraceGlobals.CLogShipContextTracer.TraceDebug<string>((long)clogShipContext.GetHashCode(), "created CLogShipContext for SG {0}", sgGuid);
					IL_15A:;
				}
				if (null == ptr || -939585531 == num || -939585532 == num)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					dwExtError = (uint)lastWin32Error;
					ExTraceGlobals.CLogShipContextTracer.TraceError<int, uint>(0L, "HrLogShipOpen returned hr={0:X8}, dwExtError={1:X8}", num, (uint)lastWin32Error);
				}
				IntPtr hglobal = new IntPtr((void*)ptr2);
				Marshal.FreeHGlobal(hglobal);
				IntPtr hglobal2 = new IntPtr((void*)ptr3);
				Marshal.FreeHGlobal(hglobal2);
				IntPtr hglobal3 = new IntPtr((void*)ptr4);
				Marshal.FreeHGlobal(hglobal3);
				IntPtr hglobal4 = new IntPtr((void*)ptr5);
				Marshal.FreeHGlobal(hglobal4);
				IntPtr hglobal5 = new IntPtr((void*)ptr6);
				Marshal.FreeHGlobal(hglobal5);
			}
			else
			{
				num = -939587631;
			}
			ExTraceGlobals.CLogShipContextTracer.TraceDebug<int>(0L, "leave CLogShipContext::Open, hr = {0:X8}", num);
			logshipContext = clogShipContext;
			return num;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000266C File Offset: 0x00001A6C
		public static CLogShipContext InvalidHandle
		{
			get
			{
				return new CLogShipContext(IntPtr.Zero);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002230 File Offset: 0x00001630
		public unsafe uint LogShipNotify(long lgenShipped, ref long lgenTruncated, out uint dwExtError)
		{
			int num = 0;
			dwExtError = 0U;
			ExTraceGlobals.CLogShipContextTracer.TraceDebug<long>((long)this.GetHashCode(), "enter LogShipNotify. lgenShipped=0x{0:X}", lgenShipped);
			int num2 = <Module>.HrESELogShipSuccessful((void*)this.handle, (int)lgenShipped, &num, 2);
			lgenTruncated = num;
			if (-939585531 == num2 || -939585532 == num2)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				dwExtError = (uint)lastWin32Error;
				ExTraceGlobals.CLogShipContextTracer.TraceError<int, uint>(0L, "HrESELogShipSuccessful (notify) returned hr={0:X8}, dwExtError={1:X8}", num2, (uint)lastWin32Error);
			}
			ExTraceGlobals.CLogShipContextTracer.TraceDebug<int, long>((long)this.GetHashCode(), "HrESELogShipSuccessful (notify) returned hr={0:X8}. lgenTruncated=0x{1:X}", num2, lgenTruncated);
			return num2;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000022C0 File Offset: 0x000016C0
		public unsafe uint LogShipTruncate(long lgenShipped, ref long lgenTruncated, out uint dwExtError)
		{
			int num = 0;
			dwExtError = 0U;
			ExTraceGlobals.CLogShipContextTracer.TraceDebug<long>((long)this.GetHashCode(), "enter LogShipTruncate. lgenShipped=0x{0:X}", lgenShipped);
			int num2 = <Module>.HrESELogShipSuccessful((void*)this.handle, (int)lgenShipped, &num, 1);
			lgenTruncated = num;
			if (-939585531 == num2 || -939585532 == num2)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				dwExtError = (uint)lastWin32Error;
				ExTraceGlobals.CLogShipContextTracer.TraceError<int, uint>(0L, "HrESELogShipSuccessful (truncate) returned hr={0:X8}, dwExtError={1:X8}", num2, (uint)lastWin32Error);
			}
			ExTraceGlobals.CLogShipContextTracer.TraceDebug<int, long>((long)this.GetHashCode(), "HrESELogShipSuccessful (truncate) returned hr={0:X8}. lgenTruncated=0x{1:X}", num2, lgenTruncated);
			return num2;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002350 File Offset: 0x00001750
		public override bool IsInvalid
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002374 File Offset: 0x00001774
		[return: MarshalAs(UnmanagedType.U1)]
		protected unsafe sealed override bool ReleaseHandle()
		{
			int num = 0;
			ExTraceGlobals.CLogShipContextTracer.TraceDebug((long)this.GetHashCode(), "enter ReleaseHandle");
			if (!this.IsInvalid)
			{
				num = <Module>.HrESELogShipClose((void*)this.handle);
			}
			this.handle = IntPtr.Zero;
			ExTraceGlobals.CLogShipContextTracer.TraceDebug<int>((long)this.GetHashCode(), "leave ReleaseHandle. HrESELogShipClose returned hr={0:X8}", num);
			return ((num == 0) ? 1 : 0) != 0;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000023FC File Offset: 0x000017FC
		private CLogShipContext(IntPtr handle) : base(IntPtr.Zero, true)
		{
			try
			{
				base.SetHandle(handle);
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000023E0 File Offset: 0x000017E0
		private CLogShipContext() : base(IntPtr.Zero, true)
		{
		}
	}
}
