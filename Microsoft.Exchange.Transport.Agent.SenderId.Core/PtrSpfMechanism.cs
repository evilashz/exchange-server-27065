using System;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.SenderId;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x0200001A RID: 26
	internal sealed class PtrSpfMechanism : SpfMechanismWithDomainSpec
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003CDC File Offset: 0x00001EDC
		public PtrSpfMechanism(SenderIdValidationContext context, SenderIdStatus prefix, MacroTermSpfNode domainSpec) : base(context, prefix, domainSpec)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003D00 File Offset: 0x00001F00
		protected override void ProcessWithExpandedDomainSpec(string expandedDomain)
		{
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "Processing PTR mechanism");
			ExTraceGlobals.ValidationTracer.TraceDebug<string>((long)this.GetHashCode(), "Looking up PTR record for domain: {0}", expandedDomain);
			this.domain = expandedDomain;
			Util.AsyncDns.BeginPtrRecordQuery(this.context.BaseContext.IPAddress, new AsyncCallback(this.PtrCallback), null);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003D64 File Offset: 0x00001F64
		private void PtrCallback(IAsyncResult ar)
		{
			string[] array;
			DnsStatus status = Util.AsyncDns.EndPtrRecordQuery(ar, out array);
			if (Util.AsyncDns.IsAcceptable(status))
			{
				this.validatedHosts = array;
				this.validatedHostIndex = 0;
				this.LookupRemainingValidatedHostIPs();
				return;
			}
			base.ProcessNextTerm();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003DA0 File Offset: 0x00001FA0
		private void LookupRemainingValidatedHostIPs()
		{
			bool flag = false;
			while (!flag)
			{
				if (this.validatedHostIndex >= 10)
				{
					this.context.ValidationCompleted(SenderIdStatus.PermError);
					return;
				}
				if (this.validatedHostIndex >= this.validatedHosts.Length)
				{
					base.ProcessNextTerm();
					return;
				}
				if (Util.AsyncDns.IsValidName(this.validatedHosts[this.validatedHostIndex]))
				{
					flag = true;
					break;
				}
				this.validatedHostIndex++;
			}
			if (flag)
			{
				Util.AsyncDns.BeginARecordQuery(this.validatedHosts[this.validatedHostIndex], this.context.BaseContext.IPAddress.AddressFamily, new AsyncCallback(this.ACallback), null);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003E40 File Offset: 0x00002040
		private void ACallback(IAsyncResult ar)
		{
			IPAddress[] array;
			DnsStatus status = Util.AsyncDns.EndARecordQuery(ar, out array);
			if (Util.AsyncDns.IsAcceptable(status))
			{
				IPAddress[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					IPAddress ipaddress = array2[i];
					if (ipaddress.Equals(this.context.BaseContext.IPAddress))
					{
						if (this.validatedHosts[this.validatedHostIndex].EndsWith(this.domain, StringComparison.OrdinalIgnoreCase))
						{
							base.SetMatchResult();
							return;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			this.validatedHostIndex++;
			this.LookupRemainingValidatedHostIPs();
		}

		// Token: 0x04000046 RID: 70
		public const int MaxPtrNames = 10;

		// Token: 0x04000047 RID: 71
		private string[] validatedHosts = new string[0];

		// Token: 0x04000048 RID: 72
		private int validatedHostIndex;

		// Token: 0x04000049 RID: 73
		private string domain = string.Empty;
	}
}
