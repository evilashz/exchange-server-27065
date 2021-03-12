using System;
using Microsoft.Exchange.Diagnostics.Components.SenderId;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000012 RID: 18
	internal abstract class SpfMechanism : SpfTerm
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000036A4 File Offset: 0x000018A4
		protected SpfMechanism(SenderIdValidationContext context, SenderIdStatus prefix) : base(context)
		{
			this.prefix = prefix;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000036B4 File Offset: 0x000018B4
		protected void SetMatchResult()
		{
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "Mechanism matched");
			if (this.prefix != SenderIdStatus.Fail)
			{
				this.context.ValidationCompleted(this.prefix);
				return;
			}
			if (this.context.ProcessExpModifier && this.context.Exp != null)
			{
				this.context.Exp.BeginGetExplanation(new AsyncCallback(this.ExplanationCallback), null);
				return;
			}
			this.context.ValidationCompleted(SenderIdStatus.Fail, SenderIdFailReason.NotPermitted, string.Empty);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003740 File Offset: 0x00001940
		private void ExplanationCallback(IAsyncResult ar)
		{
			string explanation = this.context.Exp.EndGetExplanation(ar);
			this.context.ValidationCompleted(SenderIdStatus.Fail, SenderIdFailReason.NotPermitted, explanation);
		}

		// Token: 0x0400003F RID: 63
		private SenderIdStatus prefix;
	}
}
