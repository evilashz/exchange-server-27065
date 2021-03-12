using System;
using Microsoft.Exchange.Diagnostics.Components.SenderId;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x0200001D RID: 29
	internal sealed class RedirectSpfModifier : SpfTerm
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000040A6 File Offset: 0x000022A6
		public RedirectSpfModifier(SenderIdValidationContext context, MacroTermSpfNode domainSpec) : base(context)
		{
			this.domainSpec = domainSpec;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000040B6 File Offset: 0x000022B6
		public override void Process()
		{
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "Processing redirect modifier");
			SpfMacro.BeginExpandDomainSpec(this.context, this.domainSpec, new AsyncCallback(this.ExpandDomainSpecCallback), null);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000040F0 File Offset: 0x000022F0
		private void ExpandDomainSpecCallback(IAsyncResult ar)
		{
			SpfMacro.ExpandedMacro expandedMacro = SpfMacro.EndExpandDomainSpec(ar);
			if (expandedMacro.IsValid)
			{
				string value = expandedMacro.Value;
				ExTraceGlobals.ValidationTracer.TraceDebug<string>((long)this.GetHashCode(), "Using expanded RedirectDomain for SenderId validation: {0}", value);
				this.context.BaseContext.SenderIdValidator.BeginCheckHost(this.context, value, true, new AsyncCallback(this.RedirectCallback), null);
				return;
			}
			this.context.ValidationCompleted(SenderIdStatus.PermError);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004164 File Offset: 0x00002364
		private void RedirectCallback(IAsyncResult ar)
		{
			SenderIdResult senderIdResult = this.context.BaseContext.SenderIdValidator.EndCheckHost(ar);
			ExTraceGlobals.ValidationTracer.TraceDebug<SenderIdStatus>((long)this.GetHashCode(), "Result of redirect: {0}", senderIdResult.Status);
			this.context.ValidationCompleted(senderIdResult);
		}

		// Token: 0x0400004D RID: 77
		private MacroTermSpfNode domainSpec;
	}
}
