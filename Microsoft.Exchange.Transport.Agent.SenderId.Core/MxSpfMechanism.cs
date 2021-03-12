using System;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.SenderId;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000019 RID: 25
	internal sealed class MxSpfMechanism : SpfMechanismWithDomainSpec
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003B81 File Offset: 0x00001D81
		public MxSpfMechanism(SenderIdValidationContext context, SenderIdStatus prefix, MacroTermSpfNode domainSpec, int ip4CidrLength, int ip6CidrLength) : base(context, prefix, domainSpec)
		{
			this.ip4CidrLength = ip4CidrLength;
			this.ip6CidrLength = ip6CidrLength;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003B9C File Offset: 0x00001D9C
		protected override void ProcessWithExpandedDomainSpec(string expandedDomain)
		{
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "Process MX mechanism");
			ExTraceGlobals.ValidationTracer.TraceDebug<string>((long)this.GetHashCode(), "Looking up MX record for domain {0}", expandedDomain);
			if (!Util.AsyncDns.IsValidName(expandedDomain))
			{
				this.context.ValidationCompleted(SenderIdStatus.PermError);
				return;
			}
			Util.AsyncDns.BeginMxRecordQuery(expandedDomain, this.context.BaseContext.IPAddress.AddressFamily, new AsyncCallback(this.MxCallback), null);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003C14 File Offset: 0x00001E14
		private void MxCallback(IAsyncResult ar)
		{
			TargetHost[] array;
			DnsStatus status = Util.AsyncDns.EndMxRecordQuery(ar, out array);
			if (Util.AsyncDns.IsAcceptable(status))
			{
				IPNetwork ipnetwork = IPNetwork.Create(this.context.BaseContext.IPAddress, this.ip4CidrLength, this.ip6CidrLength);
				foreach (TargetHost targetHost in array)
				{
					foreach (IPAddress address in targetHost.IPAddresses)
					{
						if (ipnetwork.Contains(address))
						{
							base.SetMatchResult();
							return;
						}
					}
				}
				base.ProcessNextTerm();
				return;
			}
			this.context.ValidationCompleted(SenderIdStatus.TempError);
		}

		// Token: 0x04000044 RID: 68
		private int ip4CidrLength;

		// Token: 0x04000045 RID: 69
		private int ip6CidrLength;
	}
}
