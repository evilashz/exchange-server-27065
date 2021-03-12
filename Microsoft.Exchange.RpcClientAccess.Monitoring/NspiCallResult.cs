using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NspiCallResult : RpcCallResult
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000033EC File Offset: 0x000015EC
		public NspiCallResult(RpcException exception) : base(exception, ErrorCode.None, null)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000033F7 File Offset: 0x000015F7
		public NspiCallResult(NspiDataException exception) : base(null, ErrorCode.None, null)
		{
			this.nspiException = exception;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003409 File Offset: 0x00001609
		public NspiCallResult(NspiStatus nspiStatus) : base(null, (ErrorCode)nspiStatus, null)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003414 File Offset: 0x00001614
		private NspiCallResult() : base(null, ErrorCode.None, null)
		{
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000341F File Offset: 0x0000161F
		public override bool IsSuccessful
		{
			get
			{
				return base.IsSuccessful && this.nspiException == null;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003434 File Offset: 0x00001634
		public NspiDataException NspiException
		{
			get
			{
				return this.nspiException;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000343C File Offset: 0x0000163C
		public static NspiCallResult CreateSuccessfulResult()
		{
			return NspiCallResult.successResult;
		}

		// Token: 0x04000034 RID: 52
		private static readonly NspiCallResult successResult = new NspiCallResult();

		// Token: 0x04000035 RID: 53
		private readonly NspiDataException nspiException;
	}
}
