using System;
using Microsoft.Exchange.Diagnostics.Components.SenderId;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000014 RID: 20
	internal abstract class SpfMechanismWithDomainSpec : SpfMechanism
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003795 File Offset: 0x00001995
		public SpfMechanismWithDomainSpec(SenderIdValidationContext context, SenderIdStatus prefix, MacroTermSpfNode domainSpec) : base(context, prefix)
		{
			this.domainSpec = domainSpec;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000037A6 File Offset: 0x000019A6
		public override void Process()
		{
			if (this.domainSpec != null)
			{
				SpfMacro.BeginExpandDomainSpec(this.context, this.domainSpec, new AsyncCallback(this.ExpandDomainSpecCallback), null);
				return;
			}
			this.ProcessWithExpandedDomainSpec(this.context.PurportedResponsibleDomain);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000037E4 File Offset: 0x000019E4
		private void ExpandDomainSpecCallback(IAsyncResult ar)
		{
			SpfMacro.ExpandedMacro expandedMacro = SpfMacro.EndExpandDomainSpec(ar);
			if (expandedMacro.IsValid)
			{
				this.ProcessWithExpandedDomainSpec(expandedMacro.Value);
				return;
			}
			ExTraceGlobals.ValidationTracer.TraceError((long)this.GetHashCode(), "Domain spec could not be expanded");
			this.context.ValidationCompleted(SenderIdStatus.PermError);
		}

		// Token: 0x0600006A RID: 106
		protected abstract void ProcessWithExpandedDomainSpec(string expandedDomain);

		// Token: 0x04000040 RID: 64
		protected MacroTermSpfNode domainSpec;
	}
}
