using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Diagnostics.Components.SenderId;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000016 RID: 22
	internal sealed class ExistsSpfMechanism : SpfMechanismWithDomainSpec
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003944 File Offset: 0x00001B44
		public ExistsSpfMechanism(SenderIdValidationContext context, SenderIdStatus prefix, MacroTermSpfNode domainSpec) : base(context, prefix, domainSpec)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003950 File Offset: 0x00001B50
		protected override void ProcessWithExpandedDomainSpec(string expandedDomain)
		{
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "Processing Exists mechansim");
			ExTraceGlobals.ValidationTracer.TraceDebug<string>((long)this.GetHashCode(), "Looking up A record for domain {0}", expandedDomain);
			if (!Util.AsyncDns.IsValidName(expandedDomain))
			{
				this.context.ValidationCompleted(SenderIdStatus.PermError);
				return;
			}
			Util.AsyncDns.BeginARecordQuery(expandedDomain, AddressFamily.InterNetwork, new AsyncCallback(this.ACallback), null);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000039B4 File Offset: 0x00001BB4
		private void ACallback(IAsyncResult ar)
		{
			IPAddress[] array;
			DnsStatus status = Util.AsyncDns.EndARecordQuery(ar, out array);
			if (!Util.AsyncDns.IsAcceptable(status))
			{
				this.context.ValidationCompleted(SenderIdStatus.TempError);
				return;
			}
			if (array.Length > 0)
			{
				base.SetMatchResult();
				return;
			}
			base.ProcessNextTerm();
		}
	}
}
