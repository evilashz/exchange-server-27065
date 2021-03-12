using System;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004C3 RID: 1219
	internal struct MetadataGetAllRecord
	{
		// Token: 0x04001FAE RID: 8110
		public MBIdentifier Identifier;

		// Token: 0x04001FAF RID: 8111
		public MBAttributes Attributes;

		// Token: 0x04001FB0 RID: 8112
		public MBUserType UserType;

		// Token: 0x04001FB1 RID: 8113
		public MBDataType DataType;

		// Token: 0x04001FB2 RID: 8114
		public int DataLen;

		// Token: 0x04001FB3 RID: 8115
		public int DataOffset;

		// Token: 0x04001FB4 RID: 8116
		public int DataTag;
	}
}
