using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001B1 RID: 433
	internal class UserAddressBatchSchema
	{
		// Token: 0x040008B3 RID: 2227
		internal static readonly HygienePropertyDefinition BatchAddressesProperty = new HygienePropertyDefinition("tvp_UserAddressProperties", typeof(BatchPropertyTable));

		// Token: 0x040008B4 RID: 2228
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = CommonMessageTraceSchema.OrganizationalUnitRootProperty;

		// Token: 0x040008B5 RID: 2229
		public static readonly HygienePropertyDefinition FssCopyIdProp = DalHelper.FssCopyIdProp;
	}
}
