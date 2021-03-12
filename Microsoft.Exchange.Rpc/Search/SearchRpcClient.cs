using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Search
{
	// Token: 0x020003EB RID: 1003
	internal class SearchRpcClient : RpcClientBase
	{
		// Token: 0x0600112C RID: 4396 RVA: 0x00056234 File Offset: 0x00055634
		public SearchRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00056248 File Offset: 0x00055648
		[HandleProcessCorruptedStateExceptions]
		protected void RecordDocumentProcessing(Guid mdbGuid, Guid flowInstance, Guid correlationId, long docId)
		{
			base.ResetRetryCounter();
			int num = 0;
			try
			{
				do
				{
					try
					{
						_GUID guid = <Module>.ToGUID(ref correlationId);
						_GUID guid2 = <Module>.ToGUID(ref flowInstance);
						_GUID guid3 = <Module>.ToGUID(ref mdbGuid);
						<Module>.cli_RecordDocumentProcessing(base.BindingHandle, guid3, guid2, guid, docId);
					}
					catch when (endfilter(true))
					{
						num = Marshal.GetExceptionCode();
					}
					if (num >= 0)
					{
						goto IL_5B;
					}
				}
				while (base.RetryRpcCall(num, RpcRetryType.ServerBusy) != 0);
				<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num, "cli_RecordDocumentProcessing");
				IL_5B:;
			}
			finally
			{
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000562E8 File Offset: 0x000556E8
		[HandleProcessCorruptedStateExceptions]
		protected unsafe void RecordDocumentFailure(Guid mdbGuid, Guid correlationId, long docId, string errorMessage)
		{
			base.ResetRetryCounter();
			int num = 0;
			using (SafeMarshalHGlobalHandle safeMarshalHGlobalHandle = new SafeMarshalHGlobalHandle(Marshal.StringToHGlobalUni(errorMessage)))
			{
				do
				{
					try
					{
						IntPtr intPtr = safeMarshalHGlobalHandle.DangerousGetHandle();
						_GUID guid = <Module>.ToGUID(ref correlationId);
						_GUID guid2 = <Module>.ToGUID(ref mdbGuid);
						<Module>.cli_RecordDocumentFailure(base.BindingHandle, guid2, guid, docId, (ushort*)intPtr.ToPointer());
					}
					catch when (endfilter(true))
					{
						num = Marshal.GetExceptionCode();
					}
					if (num >= 0)
					{
						goto IL_6D;
					}
				}
				while (base.RetryRpcCall(num, RpcRetryType.ServerBusy) != 0);
				<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num, "cli_RecordDocumentFailure");
				IL_6D:;
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x000563A4 File Offset: 0x000557A4
		[HandleProcessCorruptedStateExceptions]
		protected void UpdateIndexSystems()
		{
			base.ResetRetryCounter();
			int num = 0;
			try
			{
				do
				{
					try
					{
						<Module>.cli_UpdateIndexSystems(base.BindingHandle);
					}
					catch when (endfilter(true))
					{
						num = Marshal.GetExceptionCode();
					}
					if (num >= 0)
					{
						goto IL_3E;
					}
				}
				while (base.RetryRpcCall(num, RpcRetryType.ServerBusy) != 0);
				<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num, "cli_SuspendIndexing");
				IL_3E:;
			}
			finally
			{
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00056428 File Offset: 0x00055828
		[HandleProcessCorruptedStateExceptions]
		protected void ResumeIndexing(Guid databaseGuid)
		{
			base.ResetRetryCounter();
			int num = 0;
			try
			{
				do
				{
					try
					{
						_GUID guid = <Module>.ToGUID(ref databaseGuid);
						<Module>.cli_ResumeIndexing(base.BindingHandle, guid);
					}
					catch when (endfilter(true))
					{
						num = Marshal.GetExceptionCode();
					}
					if (num >= 0)
					{
						goto IL_47;
					}
				}
				while (base.RetryRpcCall(num, RpcRetryType.ServerBusy) != 0);
				<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num, "cli_ResumeIndexing");
				IL_47:;
			}
			finally
			{
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000564B4 File Offset: 0x000558B4
		[HandleProcessCorruptedStateExceptions]
		protected void RebuildIndexSystem(Guid databaseGuid)
		{
			base.ResetRetryCounter();
			int num = 0;
			try
			{
				do
				{
					try
					{
						_GUID guid = <Module>.ToGUID(ref databaseGuid);
						<Module>.cli_RebuildIndexSystem(base.BindingHandle, guid);
					}
					catch when (endfilter(true))
					{
						num = Marshal.GetExceptionCode();
					}
					if (num >= 0)
					{
						goto IL_47;
					}
				}
				while (base.RetryRpcCall(num, RpcRetryType.ServerBusy) != 0);
				<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num, "cli_RebuildIndexSystem");
				IL_47:;
			}
			finally
			{
			}
		}
	}
}
