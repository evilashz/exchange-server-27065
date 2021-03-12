using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000136 RID: 310
	internal class TransportDeliveryTypes
	{
		// Token: 0x0400068C RID: 1676
		internal static DeliveryType[] internalDeliveryTypes = new DeliveryType[]
		{
			DeliveryType.Undefined,
			DeliveryType.MapiDelivery,
			DeliveryType.SmtpRelayToRemoteAdSite,
			DeliveryType.SmtpRelayToTiRg,
			DeliveryType.SmtpRelayWithinAdSite,
			DeliveryType.SmtpRelayWithinAdSiteToEdge,
			DeliveryType.Unreachable,
			DeliveryType.ShadowRedundancy,
			DeliveryType.Heartbeat,
			DeliveryType.SmtpDeliveryToMailbox,
			DeliveryType.SmtpRelayToDag,
			DeliveryType.SmtpRelayToMailboxDeliveryGroup,
			DeliveryType.SmtpRelayToConnectorSourceServers,
			DeliveryType.SmtpRelayToServers
		};

		// Token: 0x0400068D RID: 1677
		internal static DeliveryType[] externalDeliveryTypes = new DeliveryType[]
		{
			DeliveryType.DnsConnectorDelivery,
			DeliveryType.NonSmtpGatewayDelivery,
			DeliveryType.SmartHostConnectorDelivery,
			DeliveryType.DeliveryAgent
		};
	}
}
