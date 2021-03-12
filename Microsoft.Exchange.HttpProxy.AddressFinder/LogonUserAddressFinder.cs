using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x0200000F RID: 15
	internal class LogonUserAddressFinder : IAddressFinder
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002AF4 File Offset: 0x00000CF4
		IRoutingKey[] IAddressFinder.Find(AddressFinderSource source, IAddressFinderDiagnostics diagnostics)
		{
			AddressFinderHelper.ThrowIfNull(source, diagnostics);
			string text = source.Items["WLID-MemberName"] as string;
			if (string.IsNullOrEmpty(text))
			{
				return AddressFinderHelper.EmptyRoutingKeyArray;
			}
			if (!SmtpAddress.IsValidSmtpAddress(text))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "[LogonUserAddressFinder::Find]: Malformed memberName {0}.", text);
				return AddressFinderHelper.EmptyRoutingKeyArray;
			}
			string text2 = source.Items["WLID-OrganizationContext"] as string;
			if (!string.IsNullOrEmpty(text2) && !SmtpAddress.IsValidDomain(text2))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "[LogonUserAddressFinder::Find]: Malformed organizationContext {0}.", text2);
				text2 = null;
			}
			IRoutingKey routingKey = new LiveIdMemberNameRoutingKey(new SmtpAddress(text), text2);
			diagnostics.AddRoutingkey(routingKey, "LogonUser");
			return AddressFinderHelper.GetRoutingKeyArray(new IRoutingKey[]
			{
				routingKey
			});
		}
	}
}
