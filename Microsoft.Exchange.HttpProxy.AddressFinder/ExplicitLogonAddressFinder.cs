using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x0200000E RID: 14
	internal class ExplicitLogonAddressFinder : IAddressFinder
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002A84 File Offset: 0x00000C84
		IRoutingKey[] IAddressFinder.Find(AddressFinderSource source, IAddressFinderDiagnostics diagnostics)
		{
			AddressFinderHelper.ThrowIfNull(source, diagnostics);
			bool selectedNodeIsLast;
			string explicitLogonNode = ProtocolHelper.GetExplicitLogonNode(source.ApplicationPath, source.FilePath, ExplicitLogonNode.Second, out selectedNodeIsLast);
			string address;
			if (ProtocolHelper.TryGetValidNormalizedExplicitLogonAddress(explicitLogonNode, selectedNodeIsLast, out address))
			{
				IRoutingKey routingKey = new SmtpRoutingKey(new SmtpAddress(address));
				diagnostics.AddRoutingkey(routingKey, "ExplicitLogon-SMTP");
				return AddressFinderHelper.GetRoutingKeyArray(new IRoutingKey[]
				{
					routingKey
				});
			}
			return AddressFinderHelper.EmptyRoutingKeyArray;
		}
	}
}
