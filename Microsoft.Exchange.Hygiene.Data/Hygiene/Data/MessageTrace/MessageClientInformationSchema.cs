using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000154 RID: 340
	internal class MessageClientInformationSchema
	{
		// Token: 0x0400067B RID: 1659
		internal static readonly HygienePropertyDefinition ClientInformationIdProperty = new HygienePropertyDefinition("ClientInformationId", typeof(Guid));

		// Token: 0x0400067C RID: 1660
		internal static readonly HygienePropertyDefinition DataClassificationIdProperty = new HygienePropertyDefinition("DataClassificationId", typeof(Guid));

		// Token: 0x0400067D RID: 1661
		internal static readonly HygienePropertyDefinition ExMessageIdProperty = CommonMessageTraceSchema.ExMessageIdProperty;
	}
}
