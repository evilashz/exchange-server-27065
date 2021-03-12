using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000246 RID: 582
	internal abstract class FastTransferSourceGetBufferResultBase : RopResult
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00027CA0 File Offset: 0x00025EA0
		internal FastTransferState State
		{
			get
			{
				return this.ResultData.State;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00027CAD File Offset: 0x00025EAD
		internal bool IsMoveUser
		{
			get
			{
				return this.ResultData.IsMoveUser;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00027CBA File Offset: 0x00025EBA
		internal ArraySegment<byte> Data
		{
			get
			{
				return this.ResultData.Data;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00027CC7 File Offset: 0x00025EC7
		internal uint BackOffTime
		{
			get
			{
				return this.ResultData.BackOffTime;
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00027CD4 File Offset: 0x00025ED4
		internal FastTransferSourceGetBufferResultBase(RopId ropId, ErrorCode errorCode, FastTransferSourceGetBufferData resultData) : base(ropId, errorCode, null)
		{
			this.ResultData = resultData;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00027CE6 File Offset: 0x00025EE6
		internal FastTransferSourceGetBufferResultBase(Reader reader, bool isServerBusy, bool isExtendedRop) : base(reader)
		{
			this.ResultData = new FastTransferSourceGetBufferData(reader, isExtendedRop, isServerBusy);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00027CFD File Offset: 0x00025EFD
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.ResultData.Serialize(writer);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00027D12 File Offset: 0x00025F12
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			this.ResultData.AppendToString(stringBuilder);
		}

		// Token: 0x040006EC RID: 1772
		protected readonly FastTransferSourceGetBufferData ResultData;
	}
}
