using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001B0 RID: 432
	internal abstract class InputOutputRop : Rop
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0001D9C9 File Offset: 0x0001BBC9
		internal byte ReturnHandleTableIndex
		{
			get
			{
				return this.returnObjectHandleIndex;
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001D9D4 File Offset: 0x0001BBD4
		internal sealed override void Execute(IConnectionInformation connection, IRopDriver ropDriver, ServerObjectHandleTable handleTable, ArraySegment<byte> outputBuffer)
		{
			IServerObject serverObject = null;
			ErrorCode errorCode = ErrorCode.None;
			bool flag = false;
			ServerObjectHandle value = ServerObjectHandle.None;
			ServerObjectMap serverObjectMap;
			if (ropDriver.TryGetServerObjectMap(base.LogonIndex, out serverObjectMap, out errorCode))
			{
				ServerObjectHandle handle = handleTable[(int)base.HandleTableIndex];
				if (serverObjectMap.TryGetValue(handle, out serverObject, out errorCode))
				{
					flag = true;
					this.ResolveString8Values(serverObject.String8Encoding);
					try
					{
						this.PeekReturnServerObjectHandleValue = serverObjectMap.NextHandleValue;
						this.InternalExecute(serverObject, ropDriver.RopHandler, outputBuffer);
						if (base.Result.ReturnObject != null)
						{
							base.Result.String8Encoding = base.Result.ReturnObject.String8Encoding;
						}
						value = base.Result.GetServerObjectHandle(serverObjectMap);
						uint handleValue = this.PeekReturnServerObjectHandleValue.HandleValue;
						uint handleValue2 = value.HandleValue;
					}
					finally
					{
						if (base.Result != null && base.Result.ReturnObject != null)
						{
							ropDriver.RopHandler.Release(base.Result.ReturnObject);
							base.Result.ReturnObject = null;
						}
					}
				}
			}
			if (!flag)
			{
				IResultFactory defaultResultFactory = this.GetDefaultResultFactory(connection, outputBuffer);
				base.Result = defaultResultFactory.CreateStandardFailedResult(errorCode);
			}
			handleTable[(int)this.ReturnHandleTableIndex] = value;
			base.Result.SetServerObjectHandleIndex(this.ReturnHandleTableIndex);
			if (base.Result.String8Encoding == null)
			{
				base.Result.String8Encoding = connection.String8Encoding;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0001DB3C File Offset: 0x0001BD3C
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0001DB44 File Offset: 0x0001BD44
		private protected ServerObjectHandle PeekReturnServerObjectHandleValue { protected get; private set; }

		// Token: 0x06000896 RID: 2198 RVA: 0x0001DB4D File Offset: 0x0001BD4D
		protected sealed override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			if (!logonTracker.ParseIsValidLogon(base.LogonIndex))
			{
				throw new BufferParseException("Invalid Logon");
			}
			this.InternalParseInput(reader, serverObjectHandleTable);
			logonTracker.ParseRecordInputOutput(base.HandleTableIndex);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001DB85 File Offset: 0x0001BD85
		protected virtual void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			this.returnObjectHandleIndex = Rop.ReadHandleTableIndex(reader, serverObjectHandleTable);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001DB94 File Offset: 0x0001BD94
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte(this.returnObjectHandleIndex);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001DBAA File Offset: 0x0001BDAA
		protected void SetCommonInput(byte logonIndex, byte inputHandleTableIndex, byte outputHandleTableIndex)
		{
			base.SetCommonInput(logonIndex, inputHandleTableIndex);
			this.returnObjectHandleIndex = outputHandleTableIndex;
		}

		// Token: 0x0600089A RID: 2202
		protected abstract void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer);

		// Token: 0x0400040E RID: 1038
		private byte returnObjectHandleIndex;
	}
}
