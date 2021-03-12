using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200009C RID: 156
	internal abstract class DualInputRop : Rop
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000D866 File Offset: 0x0000BA66
		internal virtual byte SourceHandleTableIndex
		{
			get
			{
				return base.HandleTableIndex;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000D86E File Offset: 0x0000BA6E
		internal byte DestinationHandleTableIndex
		{
			get
			{
				return this.destinationHandleTableIndex;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000D878 File Offset: 0x0000BA78
		internal sealed override void Execute(IConnectionInformation connection, IRopDriver ropDriver, ServerObjectHandleTable handleTable, ArraySegment<byte> outputBuffer)
		{
			IServerObject serverObject = null;
			IServerObject destinationServerObject = null;
			ErrorCode errorCode = ErrorCode.None;
			ServerObjectHandle handle = handleTable[(int)this.SourceHandleTableIndex];
			ServerObjectHandle handle2 = handleTable[(int)this.DestinationHandleTableIndex];
			if (ropDriver.TryGetServerObject(base.LogonIndex, handle, out serverObject, out errorCode))
			{
				this.ResolveString8Values(serverObject.String8Encoding);
				if (ropDriver.TryGetServerObject(base.LogonIndex, handle2, out destinationServerObject, out errorCode))
				{
					this.InternalExecute(serverObject, destinationServerObject, ropDriver.RopHandler, outputBuffer);
					base.Result.String8Encoding = serverObject.String8Encoding;
				}
				else if (ErrorCode.NullObject == errorCode)
				{
					errorCode = ErrorCode.DestinationNullObject;
				}
			}
			if (errorCode != ErrorCode.None)
			{
				IResultFactory defaultResultFactory = this.GetDefaultResultFactory(connection, outputBuffer);
				base.Result = defaultResultFactory.CreateStandardFailedResult(errorCode);
			}
			base.Result.SetServerObjectHandleIndex(base.HandleTableIndex);
			if (base.Result.String8Encoding == null)
			{
				base.Result.String8Encoding = connection.String8Encoding;
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000D955 File Offset: 0x0000BB55
		protected sealed override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			if (!logonTracker.ParseIsValidLogon(base.LogonIndex))
			{
				throw new BufferParseException("Invalid Logon");
			}
			this.InternalParseInput(reader, serverObjectHandleTable);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000D981 File Offset: 0x0000BB81
		protected virtual void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			this.destinationHandleTableIndex = Rop.ReadHandleTableIndex(reader, serverObjectHandleTable);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000D990 File Offset: 0x0000BB90
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte(this.destinationHandleTableIndex);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000D9A6 File Offset: 0x0000BBA6
		protected void SetCommonInput(byte logonIndex, byte sourceHandleTableIndex, byte destinationHandleTableIndex)
		{
			base.SetCommonInput(logonIndex, sourceHandleTableIndex);
			this.destinationHandleTableIndex = destinationHandleTableIndex;
		}

		// Token: 0x060003D6 RID: 982
		protected abstract void InternalExecute(IServerObject sourceServerObject, IServerObject destinationServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer);

		// Token: 0x0400025F RID: 607
		private byte destinationHandleTableIndex;
	}
}
