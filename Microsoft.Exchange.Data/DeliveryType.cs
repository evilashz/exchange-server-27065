using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000135 RID: 309
	[Serializable]
	public enum DeliveryType : short
	{
		// Token: 0x0400067A RID: 1658
		[LocDescription(DataStrings.IDs.DeliveryTypeUndefined)]
		Undefined,
		// Token: 0x0400067B RID: 1659
		[LocDescription(DataStrings.IDs.DeliveryTypeDnsConnectorDelivery)]
		DnsConnectorDelivery,
		// Token: 0x0400067C RID: 1660
		[LocDescription(DataStrings.IDs.DeliveryTypeMapiDelivery)]
		MapiDelivery,
		// Token: 0x0400067D RID: 1661
		[LocDescription(DataStrings.IDs.DeliveryTypeNonSmtpGatewayDelivery)]
		NonSmtpGatewayDelivery,
		// Token: 0x0400067E RID: 1662
		[LocDescription(DataStrings.IDs.DeliveryTypeSmartHostConnectorDelivery)]
		SmartHostConnectorDelivery,
		// Token: 0x0400067F RID: 1663
		[LocDescription(DataStrings.IDs.DeliveryTypeSmtpRelayToRemoteAdSite)]
		SmtpRelayToRemoteAdSite,
		// Token: 0x04000680 RID: 1664
		[LocDescription(DataStrings.IDs.DeliveryTypeSmtpRelayToTiRg)]
		SmtpRelayToTiRg,
		// Token: 0x04000681 RID: 1665
		[LocDescription(DataStrings.IDs.DeliveryTypeSmtpRelayWithinAdSite)]
		SmtpRelayWithinAdSite,
		// Token: 0x04000682 RID: 1666
		[LocDescription(DataStrings.IDs.DeliveryTypeSmtpRelayWithinAdSiteToEdge)]
		SmtpRelayWithinAdSiteToEdge,
		// Token: 0x04000683 RID: 1667
		[LocDescription(DataStrings.IDs.DeliveryTypeUnreachable)]
		Unreachable,
		// Token: 0x04000684 RID: 1668
		[LocDescription(DataStrings.IDs.DeliveryTypeShadowRedundancy)]
		ShadowRedundancy,
		// Token: 0x04000685 RID: 1669
		[LocDescription(DataStrings.IDs.DeliveryTypeHeartbeat)]
		Heartbeat,
		// Token: 0x04000686 RID: 1670
		[LocDescription(DataStrings.IDs.DeliveryTypeDeliveryAgent)]
		DeliveryAgent,
		// Token: 0x04000687 RID: 1671
		[LocDescription(DataStrings.IDs.DeliveryTypeSmtpDeliveryToMailbox)]
		SmtpDeliveryToMailbox,
		// Token: 0x04000688 RID: 1672
		[LocDescription(DataStrings.IDs.DeliveryTypeSmtpRelayToDag)]
		SmtpRelayToDag,
		// Token: 0x04000689 RID: 1673
		[LocDescription(DataStrings.IDs.DeliveryTypeSmtpRelayToMailboxDeliveryGroup)]
		SmtpRelayToMailboxDeliveryGroup,
		// Token: 0x0400068A RID: 1674
		[LocDescription(DataStrings.IDs.DeliveryTypeSmtpRelayToConnectorSourceServers)]
		SmtpRelayToConnectorSourceServers,
		// Token: 0x0400068B RID: 1675
		[LocDescription(DataStrings.IDs.DeliveryTypeSmtpRelayToServers)]
		SmtpRelayToServers
	}
}
