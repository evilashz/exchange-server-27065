using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200018E RID: 398
	internal class QuarantinedMessageRecipientBatchSchema
	{
		// Token: 0x04000796 RID: 1942
		internal static readonly HygienePropertyDefinition BatchAddressesProperty = new HygienePropertyDefinition("batchProperties", typeof(BatchPropertyTable));

		// Token: 0x04000797 RID: 1943
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = CommonMessageTraceSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000798 RID: 1944
		public static readonly HygienePropertyDefinition FssCopyIdProp = DalHelper.FssCopyIdProp;
	}
}
