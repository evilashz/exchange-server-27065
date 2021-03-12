using System;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002F2 RID: 754
	internal class CancelAttachment : ServiceCommand<bool>
	{
		// Token: 0x06001967 RID: 6503 RVA: 0x00058AB4 File Offset: 0x00056CB4
		public CancelAttachment(CallContext callContext, string cancellationId) : base(callContext)
		{
			if (string.IsNullOrEmpty(cancellationId))
			{
				throw new ArgumentException("The parameter cannot be null or empty.", "cancellationId");
			}
			this.cancellationId = cancellationId;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00058ADC File Offset: 0x00056CDC
		protected override bool InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			OwaServerTraceLogger.AppendToLog(new TraceLogEvent("CancelAttachment.InternalExecute", userContext, "InternalExecute", string.Format("Attempting to cancel. CancellationId: {0}", this.cancellationId)));
			return userContext.CancelAttachmentManager.CancelAttachment(this.cancellationId, 30000);
		}

		// Token: 0x04000DFB RID: 3579
		private readonly string cancellationId;
	}
}
