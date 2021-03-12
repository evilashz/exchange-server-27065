using System;
using Microsoft.Exchange.Diagnostics.Components.SenderId;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000017 RID: 23
	internal sealed class IncludeSpfMechanism : SpfMechanismWithDomainSpec
	{
		// Token: 0x06000071 RID: 113 RVA: 0x000039F2 File Offset: 0x00001BF2
		public IncludeSpfMechanism(SenderIdValidationContext context, SenderIdStatus prefix, MacroTermSpfNode domainSpec) : base(context, prefix, domainSpec)
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003A00 File Offset: 0x00001C00
		protected override void ProcessWithExpandedDomainSpec(string expandedDomain)
		{
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "Processing Include mechanism");
			ExTraceGlobals.ValidationTracer.TraceDebug<string>((long)this.GetHashCode(), "Doing recursive Sender Id check on domain {0}", expandedDomain);
			this.context.BaseContext.SenderIdValidator.BeginCheckHost(this.context, expandedDomain, false, new AsyncCallback(this.IncludeCallback), null);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003A68 File Offset: 0x00001C68
		private void IncludeCallback(IAsyncResult ar)
		{
			SenderIdResult senderIdResult = this.context.BaseContext.SenderIdValidator.EndCheckHost(ar);
			ExTraceGlobals.ValidationTracer.TraceDebug<SenderIdStatus>((long)this.GetHashCode(), "Result of SenderId check: {0}", senderIdResult.Status);
			switch (senderIdResult.Status)
			{
			case SenderIdStatus.Pass:
				base.SetMatchResult();
				return;
			case SenderIdStatus.Neutral:
			case SenderIdStatus.SoftFail:
			case SenderIdStatus.Fail:
				base.ProcessNextTerm();
				return;
			case SenderIdStatus.None:
			case SenderIdStatus.PermError:
				this.context.ValidationCompleted(SenderIdStatus.PermError);
				return;
			case SenderIdStatus.TempError:
				this.context.ValidationCompleted(SenderIdStatus.TempError);
				return;
			default:
				ExTraceGlobals.ValidationTracer.TraceError<SenderIdStatus>((long)this.GetHashCode(), "Invalid result status: {0}", senderIdResult.Status);
				throw new InvalidOperationException("Invalid result status.");
			}
		}
	}
}
