using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiSeekEntriesClientAsyncOperation : ClientAsyncOperation
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00007692 File Offset: 0x00005892
		public NspiSeekEntriesClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(context, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000769D File Offset: 0x0000589D
		protected override string RequestType
		{
			get
			{
				return "SeekEntries";
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000076A4 File Offset: 0x000058A4
		public void Begin(NspiSeekEntriesRequest request)
		{
			base.InternalBegin(new Action<Writer>(request.Serialize));
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000076DC File Offset: 0x000058DC
		public ErrorCode End(out NspiSeekEntriesResponse response)
		{
			NspiSeekEntriesResponse localResponse = null;
			ErrorCode result = base.InternalEnd(delegate(Reader reader)
			{
				localResponse = new NspiSeekEntriesResponse(reader);
				return (int)localResponse.ReturnCode;
			});
			response = localResponse;
			return result;
		}
	}
}
