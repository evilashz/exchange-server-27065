using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ExRpcModule
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x0000A378 File Offset: 0x00008578
		public static void Bind()
		{
			if (ExRpcModule.exrpcInited)
			{
				return;
			}
			lock (ExRpcModule.syncLock)
			{
				if (!ExRpcModule.exrpcInited)
				{
					if (ComponentTrace<MapiNetTags>.CheckEnabled(60))
					{
						ComponentTrace<MapiNetTags>.Trace(9010, 60, 0L, "ExRpcModule.Bind");
					}
					try
					{
						int num = NativeMethods.EcInitProvider(out ExRpcModule.pPerfData);
						if (num != 0)
						{
							throw MapiExceptionHelper.LowLevelInitializationFailureException(string.Format("ec={0} (0x{1})", num.ToString(), num.ToString("x")));
						}
						ExRpcModule.exrpcInited = true;
					}
					catch (DllNotFoundException)
					{
						throw MapiExceptionHelper.LowLevelInitializationFailureException("Unable to load exrpc32.dll or one of its dependent DLLs (extrace.dll, exchmem.dll, msvcr90.dll, etc)");
					}
				}
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000A430 File Offset: 0x00008630
		public static IExRpcManageInterface GetExRpcManage()
		{
			ExRpcModule.Bind();
			SafeExRpcManageHandle result = null;
			int num = NativeMethods.EcGetExRpcManage(out result);
			if (num != 0)
			{
				MapiExceptionHelper.ThrowIfError("Unable to create ExRpcManage interface.", num);
			}
			return result;
		}

		// Token: 0x0400043E RID: 1086
		private static bool exrpcInited = false;

		// Token: 0x0400043F RID: 1087
		internal static IntPtr pPerfData;

		// Token: 0x04000440 RID: 1088
		private static object syncLock = new object();
	}
}
