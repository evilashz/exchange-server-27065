using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000439 RID: 1081
	public abstract class InboxRulesRequest : BaseRequest
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x000A10A2 File Offset: 0x0009F2A2
		// (set) Token: 0x06001FB6 RID: 8118 RVA: 0x000A10AA File Offset: 0x0009F2AA
		[XmlElement(Order = 0)]
		public string MailboxSmtpAddress { get; set; }

		// Token: 0x06001FB7 RID: 8119 RVA: 0x000A10B3 File Offset: 0x0009F2B3
		protected InboxRulesRequest(Trace tracer, bool isWriteOperation)
		{
			this.tracer = tracer;
			this.isWriteOperation = isWriteOperation;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x000A10CC File Offset: 0x0009F2CC
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			this.tracer.TraceDebug((long)this.GetHashCode(), "InboxRulesRequest.GetProxyInfo(callContext) called;");
			BaseServerIdInfo result;
			try
			{
				if (callContext is ExternalCallContext)
				{
					result = null;
				}
				else if (!string.IsNullOrEmpty(this.MailboxSmtpAddress))
				{
					result = MailboxIdServerInfo.Create(this.MailboxSmtpAddress);
				}
				else if (callContext.AccessingPrincipal == null)
				{
					result = null;
				}
				else
				{
					result = callContext.GetServerInfoForEffectiveCaller();
				}
			}
			finally
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "InboxRulesRequest.GetProxyInfo(callContext) call finished;");
			}
			return result;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000A1158 File Offset: 0x0009F358
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(this.isWriteOperation, callContext);
		}

		// Token: 0x040013F7 RID: 5111
		private readonly bool isWriteOperation;

		// Token: 0x040013F8 RID: 5112
		private Trace tracer;
	}
}
