using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200021C RID: 540
	internal class RopExecutionHistory
	{
		// Token: 0x06000BBE RID: 3006 RVA: 0x00025447 File Offset: 0x00023647
		public RopExecutionHistory(uint historyLength)
		{
			if (historyLength > 0U)
			{
				this.ropExecutionHistory = new RopExecutionHistory.RopHistoryEntry[historyLength];
			}
			else
			{
				this.ropExecutionHistory = null;
			}
			this.currentRopExecutionHistorySlot = 0U;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00025470 File Offset: 0x00023670
		public void OnBeforeRopExecution(Rop rop, ServerObjectHandleTable serverObjectHandleTable)
		{
			if (this.ropExecutionHistory == null)
			{
				return;
			}
			this.currentRopExecutionHistorySlot += 1U;
			if ((ulong)this.currentRopExecutionHistorySlot == (ulong)((long)this.ropExecutionHistory.Length))
			{
				this.currentRopExecutionHistorySlot = 0U;
			}
			InputRop inputRop = rop as InputRop;
			byte key;
			if (inputRop != null)
			{
				key = inputRop.InputHandleTableIndex;
			}
			else
			{
				key = rop.HandleTableIndex;
			}
			ServerObjectHandle inputObjectHandle = serverObjectHandleTable[(int)key];
			this.ropExecutionHistory[(int)((UIntPtr)this.currentRopExecutionHistorySlot)].RopType = rop.RopId;
			this.ropExecutionHistory[(int)((UIntPtr)this.currentRopExecutionHistorySlot)].InputObjectHandle = inputObjectHandle;
			this.ropExecutionHistory[(int)((UIntPtr)this.currentRopExecutionHistorySlot)].ErrorCode = (ErrorCode)4294967295U;
			this.ropExecutionHistory[(int)((UIntPtr)this.currentRopExecutionHistorySlot)].OutputObjectHandle = ServerObjectHandle.None;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00025538 File Offset: 0x00023738
		public void OnAfterRopExecution(Rop rop, IRopDriver ropDriver, ServerObjectHandleTable serverObjectHandleTable)
		{
			if (this.ropExecutionHistory == null)
			{
				return;
			}
			if (rop.Result != null)
			{
				this.ropExecutionHistory[(int)((UIntPtr)this.currentRopExecutionHistorySlot)].ErrorCode = rop.Result.ErrorCode;
				ErrorCode errorCode = ErrorCode.None;
				ServerObjectMap map;
				if (ropDriver.TryGetServerObjectMap(rop.LogonIndex, out map, out errorCode))
				{
					this.ropExecutionHistory[(int)((UIntPtr)this.currentRopExecutionHistorySlot)].OutputObjectHandle = rop.Result.GetServerObjectHandle(map);
				}
			}
		}

		// Token: 0x04000696 RID: 1686
		private readonly RopExecutionHistory.RopHistoryEntry[] ropExecutionHistory;

		// Token: 0x04000697 RID: 1687
		private uint currentRopExecutionHistorySlot;

		// Token: 0x0200021D RID: 541
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct RopHistoryEntry
		{
			// Token: 0x04000698 RID: 1688
			public RopId RopType;

			// Token: 0x04000699 RID: 1689
			public ServerObjectHandle InputObjectHandle;

			// Token: 0x0400069A RID: 1690
			public ServerObjectHandle OutputObjectHandle;

			// Token: 0x0400069B RID: 1691
			public ErrorCode ErrorCode;
		}
	}
}
