using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001B1 RID: 433
	internal abstract class InputRop : Rop
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0001DBC3 File Offset: 0x0001BDC3
		internal virtual byte InputHandleTableIndex
		{
			get
			{
				return base.HandleTableIndex;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		internal sealed override void Execute(IConnectionInformation connection, IRopDriver ropDriver, ServerObjectHandleTable handleTable, ArraySegment<byte> outputBuffer)
		{
			IServerObject serverObject = null;
			ErrorCode errorCode = ErrorCode.None;
			ServerObjectHandle handle = handleTable[(int)this.InputHandleTableIndex];
			if (ropDriver.TryGetServerObject(base.LogonIndex, handle, out serverObject, out errorCode))
			{
				this.ResolveString8Values(serverObject.String8Encoding);
				this.InternalExecute(serverObject, ropDriver.RopHandler, outputBuffer);
				base.Result.String8Encoding = serverObject.String8Encoding;
			}
			else
			{
				IResultFactory defaultResultFactory = this.GetDefaultResultFactory(connection, outputBuffer);
				base.Result = defaultResultFactory.CreateStandardFailedResult(errorCode);
				base.Result.String8Encoding = connection.String8Encoding;
			}
			base.Result.SetServerObjectHandleIndex(base.HandleTableIndex);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001DC64 File Offset: 0x0001BE64
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			if (!logonTracker.ParseIsValidLogon(base.LogonIndex))
			{
				throw new BufferParseException("Invalid Logon");
			}
			this.InternalParseInput(reader, serverObjectHandleTable);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001DC90 File Offset: 0x0001BE90
		protected virtual void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
		}

		// Token: 0x060008A0 RID: 2208
		protected abstract void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer);
	}
}
