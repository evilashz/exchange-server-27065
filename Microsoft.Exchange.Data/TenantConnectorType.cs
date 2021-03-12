using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000AE RID: 174
	public enum TenantConnectorType
	{
		// Token: 0x040002A8 RID: 680
		[LocDescription(DataStrings.IDs.ConnectorTypeOnPremises)]
		OnPremises = 1,
		// Token: 0x040002A9 RID: 681
		[LocDescription(DataStrings.IDs.ConnectorTypePartner)]
		Partner
	}
}
