using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004E RID: 78
	internal static class FastTransferSendOptionExtensions
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0000750D File Offset: 0x0000570D
		public static bool UseCpidOrUnicode(this FastTransferSendOption sendOptions)
		{
			return (byte)(sendOptions & (FastTransferSendOption.Unicode | FastTransferSendOption.UseCpId | FastTransferSendOption.ForceUnicode)) != 0;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000751A File Offset: 0x0000571A
		public static bool UseCpid(this FastTransferSendOption sendOptions)
		{
			return (byte)(sendOptions & FastTransferSendOption.UseCpId) != 0;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007526 File Offset: 0x00005726
		public static bool IsUpload(this FastTransferSendOption sendOptions)
		{
			return (byte)(sendOptions & FastTransferSendOption.Upload) == 3;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000752F File Offset: 0x0000572F
		public static bool WantUnicode(this FastTransferSendOption sendOptions)
		{
			return (byte)(sendOptions & (FastTransferSendOption.Unicode | FastTransferSendOption.ForceUnicode)) != 0;
		}
	}
}
