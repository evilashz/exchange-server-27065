using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class EasServerCommand<TRequest, TResponse, TStatus> : EasCommand<TRequest, TResponse> where TResponse : IEasServerResponse<TStatus>, new() where TStatus : struct, IConvertible
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003377 File Offset: 0x00001577
		protected internal EasServerCommand(Command command, EasConnectionSettings easConnectionSettings) : base(command, easConnectionSettings)
		{
			base.InitializeExpectedHttpStatusCodes(typeof(HttpStatus));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003394 File Offset: 0x00001594
		internal override TResponse Execute(TRequest request)
		{
			TResponse tresponse = base.Execute(request);
			TStatus status = tresponse.ConvertStatusToEnum();
			tresponse.ThrowIfStatusIsFailed(status);
			return tresponse;
		}
	}
}
