using System;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000462 RID: 1122
	internal class QuitSmtpProxyCommand : QuitSmtpCommand
	{
		// Token: 0x060033F4 RID: 13300 RVA: 0x000D1859 File Offset: 0x000CFA59
		public QuitSmtpProxyCommand(ISmtpSession session) : base(session)
		{
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000D1862 File Offset: 0x000CFA62
		internal override void InboundParseCommand()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000D1869 File Offset: 0x000CFA69
		internal override void InboundProcessCommand()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000D1870 File Offset: 0x000CFA70
		internal override void OutboundProcessResponse()
		{
			SmtpOutProxySession smtpOutProxySession = (SmtpOutProxySession)base.SmtpSession;
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Inititating Disconnect with remote host");
			smtpOutProxySession.Disconnect();
		}
	}
}
