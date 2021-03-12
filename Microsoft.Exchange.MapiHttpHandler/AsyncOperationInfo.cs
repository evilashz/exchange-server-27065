using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000038 RID: 56
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AsyncOperationInfo
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000C218 File Offset: 0x0000A418
		public AsyncOperationInfo(string requestType, string requestId, string sequenceCookie, string sourceCafeServer, string cafeActivityId, string clientAddress)
		{
			this.StartTime = ExDateTime.UtcNow;
			this.RequestType = requestType;
			this.RequestId = requestId;
			this.SequenceCookie = sequenceCookie;
			this.SourceCafeServer = sourceCafeServer;
			this.CafeActivityId = cafeActivityId;
			this.ClientAddress = clientAddress;
			this.LastPendingTime = null;
			this.PendingCount = 0;
			this.FailureException = null;
			this.EndTime = null;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000C28F File Offset: 0x0000A48F
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000C297 File Offset: 0x0000A497
		public ExDateTime? LastPendingTime { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000C2A8 File Offset: 0x0000A4A8
		public int PendingCount { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000C2B1 File Offset: 0x0000A4B1
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000C2B9 File Offset: 0x0000A4B9
		public Exception FailureException { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000C2C2 File Offset: 0x0000A4C2
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000C2CA File Offset: 0x0000A4CA
		public ExDateTime? EndTime { get; private set; }

		// Token: 0x0600020B RID: 523 RVA: 0x0000C2D3 File Offset: 0x0000A4D3
		public void OnPendingSent()
		{
			this.PendingCount++;
			this.LastPendingTime = new ExDateTime?(ExDateTime.UtcNow);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000C2F3 File Offset: 0x0000A4F3
		public void OnComplete(Exception failureException)
		{
			this.FailureException = failureException;
			this.EndTime = new ExDateTime?(ExDateTime.UtcNow);
		}

		// Token: 0x040000DF RID: 223
		public readonly ExDateTime StartTime;

		// Token: 0x040000E0 RID: 224
		public readonly string RequestType;

		// Token: 0x040000E1 RID: 225
		public readonly string RequestId;

		// Token: 0x040000E2 RID: 226
		public readonly string SequenceCookie;

		// Token: 0x040000E3 RID: 227
		public readonly string SourceCafeServer;

		// Token: 0x040000E4 RID: 228
		public readonly string CafeActivityId;

		// Token: 0x040000E5 RID: 229
		public readonly string ClientAddress;
	}
}
