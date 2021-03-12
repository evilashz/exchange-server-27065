using System;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.SenderId;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000015 RID: 21
	internal sealed class ASpfMechanism : SpfMechanismWithDomainSpec
	{
		// Token: 0x0600006B RID: 107 RVA: 0x0000382F File Offset: 0x00001A2F
		public ASpfMechanism(SenderIdValidationContext context, SenderIdStatus prefix, MacroTermSpfNode domainSpec, int ip4CidrLength, int ip6CidrLength) : base(context, prefix, domainSpec)
		{
			this.ip4CidrLength = ip4CidrLength;
			this.ip6CidrLength = ip6CidrLength;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000384C File Offset: 0x00001A4C
		protected override void ProcessWithExpandedDomainSpec(string expandedDomain)
		{
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "Process A mechanism");
			ExTraceGlobals.ValidationTracer.TraceDebug<string>((long)this.GetHashCode(), "Looking up A record for domain {0}", expandedDomain);
			if (!Util.AsyncDns.IsValidName(expandedDomain))
			{
				this.context.ValidationCompleted(SenderIdStatus.PermError);
				return;
			}
			Util.AsyncDns.BeginARecordQuery(expandedDomain, this.context.BaseContext.IPAddress.AddressFamily, new AsyncCallback(this.ACallback), null);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000038C4 File Offset: 0x00001AC4
		private void ACallback(IAsyncResult ar)
		{
			IPAddress[] array;
			DnsStatus status = Util.AsyncDns.EndARecordQuery(ar, out array);
			if (Util.AsyncDns.IsAcceptable(status))
			{
				IPNetwork ipnetwork = IPNetwork.Create(this.context.BaseContext.IPAddress, this.ip4CidrLength, this.ip6CidrLength);
				foreach (IPAddress address in array)
				{
					if (ipnetwork.Contains(address))
					{
						base.SetMatchResult();
						return;
					}
				}
				base.ProcessNextTerm();
				return;
			}
			this.context.ValidationCompleted(SenderIdStatus.TempError);
		}

		// Token: 0x04000041 RID: 65
		private int ip4CidrLength;

		// Token: 0x04000042 RID: 66
		private int ip6CidrLength;
	}
}
