using System;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000043 RID: 67
	internal class MapiHttpDispatchedCallResult
	{
		// Token: 0x06000277 RID: 631 RVA: 0x0000E45F File Offset: 0x0000C65F
		public MapiHttpDispatchedCallResult(uint statusCode, Exception exception)
		{
			this.statusCode = statusCode;
			if (this.statusCode != 0U)
			{
				Util.ThrowOnNullArgument(exception, "exception");
				this.exception = exception;
				return;
			}
			if (exception != null)
			{
				throw new ArgumentException("Exception must be null if the status code is 0 (success).", "exception");
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000E49C File Offset: 0x0000C69C
		public uint StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
		public MapiHttpResponse CreateResponse(Func<ArraySegment<byte>> bufferAcquisitionDelegate, Func<MapiHttpResponse> successfulResponseDelegate)
		{
			if (this.statusCode == 0U)
			{
				return successfulResponseDelegate();
			}
			return new MapiHttpFailureResponse(this.statusCode, this.SerializeExceptionIntoAuxOut(this.exception, bufferAcquisitionDelegate()));
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		private ArraySegment<byte> SerializeExceptionIntoAuxOut(Exception exception, ArraySegment<byte> auxOut)
		{
			ExceptionTraceAuxiliaryBlock outputBlock = new ExceptionTraceAuxiliaryBlock(0U, exception.ToString());
			AuxiliaryData emptyAuxiliaryData = AuxiliaryData.GetEmptyAuxiliaryData();
			emptyAuxiliaryData.AppendOutput(outputBlock);
			return emptyAuxiliaryData.Serialize(auxOut);
		}

		// Token: 0x0400010B RID: 267
		private readonly uint statusCode;

		// Token: 0x0400010C RID: 268
		private readonly Exception exception;
	}
}
