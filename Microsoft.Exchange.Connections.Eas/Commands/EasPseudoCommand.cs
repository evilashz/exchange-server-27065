using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class EasPseudoCommand<TRequest, TResponse> : EasCommand<TRequest, TResponse> where TResponse : IHaveAnHttpStatus, new()
	{
		// Token: 0x0600008E RID: 142 RVA: 0x0000336D File Offset: 0x0000156D
		protected internal EasPseudoCommand(Command command, EasConnectionSettings easConnectionSettings) : base(command, easConnectionSettings)
		{
		}
	}
}
