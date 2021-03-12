using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000324 RID: 804
	internal sealed class RopRelease : Rop
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00033C48 File Offset: 0x00031E48
		internal override RopId RopId
		{
			get
			{
				return RopId.Release;
			}
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00033C4B File Offset: 0x00031E4B
		internal static Rop CreateRop()
		{
			return new RopRelease();
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00033C52 File Offset: 0x00031E52
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00033C5C File Offset: 0x00031E5C
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00033C66 File Offset: 0x00031E66
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00033C68 File Offset: 0x00031E68
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			throw new InvalidOperationException("Results are not supported for RopRelease");
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00033C74 File Offset: 0x00031E74
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			logonTracker.ParseRecordRelease(base.HandleTableIndex);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00033C8C File Offset: 0x00031E8C
		internal sealed override void Execute(IConnectionInformation connectionInfo, IRopDriver ropDriver, ServerObjectHandleTable handleTable, ArraySegment<byte> outputBuffer)
		{
			ServerObjectHandle handleToRelease = handleTable[(int)base.HandleTableIndex];
			ropDriver.ReleaseHandle(base.LogonIndex, handleToRelease);
			handleTable[(int)base.HandleTableIndex] = ServerObjectHandle.ReleasedObject;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00033CC4 File Offset: 0x00031EC4
		protected override void InternalSerializeOutput(Writer writer)
		{
		}

		// Token: 0x04000A3E RID: 2622
		private const RopId RopType = RopId.Release;
	}
}
