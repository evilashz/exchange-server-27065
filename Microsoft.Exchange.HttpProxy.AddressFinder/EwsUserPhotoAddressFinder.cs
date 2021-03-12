using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x0200000D RID: 13
	internal class EwsUserPhotoAddressFinder : IAddressFinder
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002A18 File Offset: 0x00000C18
		IRoutingKey[] IAddressFinder.Find(AddressFinderSource source, IAddressFinderDiagnostics diagnostics)
		{
			AddressFinderHelper.ThrowIfNull(source, diagnostics);
			string text = source.QueryString["email"];
			if (!string.IsNullOrEmpty(text) && SmtpAddress.IsValidSmtpAddress(text))
			{
				IRoutingKey routingKey = new SmtpRoutingKey(new SmtpAddress(text));
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
