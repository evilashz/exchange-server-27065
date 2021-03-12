using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000009 RID: 9
	public static class AdminRpcInterface
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002450 File Offset: 0x00000650
		public static void StartAcceptingCalls()
		{
			using (LockManager.Lock(AdminRpcInterface.lockName, LockManager.LockType.AdminRpcInterfaceExclusive))
			{
				AdminRpcInterface.state = AdminRpcInterface.InterfaceState.Enabled;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002490 File Offset: 0x00000690
		public static void StopAcceptingCalls()
		{
			using (LockManager.Lock(AdminRpcInterface.lockName, LockManager.LockType.AdminRpcInterfaceExclusive))
			{
				AdminRpcInterface.state = AdminRpcInterface.InterfaceState.Stopped;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024D0 File Offset: 0x000006D0
		public static ErrorCode EcEnterRpcCall()
		{
			LockManager.GetLock(AdminRpcInterface.lockName, LockManager.LockType.AdminRpcInterfaceShared);
			ErrorCode errorCode;
			switch (AdminRpcInterface.state)
			{
			case AdminRpcInterface.InterfaceState.NotYetEnabled:
				errorCode = ErrorCode.CreateNotInitialized((LID)56984U);
				goto IL_50;
			case AdminRpcInterface.InterfaceState.Stopped:
				errorCode = ErrorCode.CreateExiting((LID)40600U);
				goto IL_50;
			}
			errorCode = ErrorCode.NoError;
			IL_50:
			if (errorCode != ErrorCode.NoError)
			{
				LockManager.ReleaseLock(AdminRpcInterface.lockName, LockManager.LockType.AdminRpcInterfaceShared);
			}
			return errorCode;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002547 File Offset: 0x00000747
		public static void ExitRpcCall()
		{
			LockManager.ReleaseLock(AdminRpcInterface.lockName, LockManager.LockType.AdminRpcInterfaceShared);
		}

		// Token: 0x0400003F RID: 63
		private static readonly LockName<Guid> lockName = new LockName<Guid>(Guid.NewGuid(), LockManager.LockLevel.AdminRpcInterface);

		// Token: 0x04000040 RID: 64
		private static AdminRpcInterface.InterfaceState state = AdminRpcInterface.InterfaceState.NotYetEnabled;

		// Token: 0x0200000A RID: 10
		private enum InterfaceState
		{
			// Token: 0x04000042 RID: 66
			NotYetEnabled,
			// Token: 0x04000043 RID: 67
			Enabled,
			// Token: 0x04000044 RID: 68
			Stopped
		}
	}
}
