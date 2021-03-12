using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PingAsyncOperation : AsyncOperation
	{
		// Token: 0x06000296 RID: 662 RVA: 0x00010A0F File Offset: 0x0000EC0F
		public PingAsyncOperation(HttpContextBase context) : base(context, "/mapi/", AsyncOperationCookieFlags.AllowSession)
		{
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00010A1E File Offset: 0x0000EC1E
		public override string RequestType
		{
			get
			{
				return "PING";
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00010A25 File Offset: 0x0000EC25
		public override void ParseRequest(WorkBuffer requestBuffer)
		{
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00010A27 File Offset: 0x0000EC27
		public override Task ExecuteAsync()
		{
			return Task.FromResult<bool>(true);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00010A2F File Offset: 0x0000EC2F
		public override void SerializeResponse(out WorkBuffer[] responseBuffers)
		{
			responseBuffers = null;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00010A34 File Offset: 0x0000EC34
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PingAsyncOperation>(this);
		}
	}
}
