using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001A1 RID: 417
	internal class UnifiedPolicyCommonSchema
	{
		// Token: 0x0400085A RID: 2138
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = CommonMessageTraceSchema.OrganizationalUnitRootProperty;

		// Token: 0x0400085B RID: 2139
		internal static readonly HygienePropertyDefinition ObjectIdProperty = new HygienePropertyDefinition("ObjectId", typeof(Guid));

		// Token: 0x0400085C RID: 2140
		internal static readonly HygienePropertyDefinition DataSourceProperty = new HygienePropertyDefinition("DataSource", typeof(string));

		// Token: 0x0400085D RID: 2141
		internal static readonly HygienePropertyDefinition HashBucketProperty = DalHelper.HashBucketProp;

		// Token: 0x0400085E RID: 2142
		internal static readonly HygienePropertyDefinition PhysicalInstanceKeyProp = DalHelper.PhysicalInstanceKeyProp;

		// Token: 0x0400085F RID: 2143
		internal static readonly HygienePropertyDefinition FssCopyIdProp = DalHelper.FssCopyIdProp;

		// Token: 0x04000860 RID: 2144
		internal static readonly HygienePropertyDefinition IntValue1Prop = new HygienePropertyDefinition("IntValue1", typeof(int?));

		// Token: 0x04000861 RID: 2145
		internal static readonly HygienePropertyDefinition IntValue2Prop = new HygienePropertyDefinition("IntValue2", typeof(int?));

		// Token: 0x04000862 RID: 2146
		internal static readonly HygienePropertyDefinition IntValue3Prop = new HygienePropertyDefinition("IntValue3", typeof(int?));

		// Token: 0x04000863 RID: 2147
		internal static readonly HygienePropertyDefinition LongValue1Prop = new HygienePropertyDefinition("LongValue1", typeof(long?));

		// Token: 0x04000864 RID: 2148
		internal static readonly HygienePropertyDefinition LongValue2Prop = new HygienePropertyDefinition("LongValue2", typeof(long?));

		// Token: 0x04000865 RID: 2149
		internal static readonly HygienePropertyDefinition GuidValue1Prop = new HygienePropertyDefinition("GuidValue1", typeof(Guid?));

		// Token: 0x04000866 RID: 2150
		internal static readonly HygienePropertyDefinition GuidValue2Prop = new HygienePropertyDefinition("GuidValue2", typeof(Guid?));

		// Token: 0x04000867 RID: 2151
		internal static readonly HygienePropertyDefinition StringValue1Prop = new HygienePropertyDefinition("StringValue1", typeof(string));

		// Token: 0x04000868 RID: 2152
		internal static readonly HygienePropertyDefinition StringValue2Prop = new HygienePropertyDefinition("StringValue2", typeof(string));

		// Token: 0x04000869 RID: 2153
		internal static readonly HygienePropertyDefinition StringValue3Prop = new HygienePropertyDefinition("StringValue3", typeof(string));

		// Token: 0x0400086A RID: 2154
		internal static readonly HygienePropertyDefinition StringValue4Prop = new HygienePropertyDefinition("StringValue4", typeof(string));

		// Token: 0x0400086B RID: 2155
		internal static readonly HygienePropertyDefinition StringValue5Prop = new HygienePropertyDefinition("StringValue5", typeof(string));

		// Token: 0x0400086C RID: 2156
		internal static readonly HygienePropertyDefinition ByteValue1Prop = new HygienePropertyDefinition("ByteValue1", typeof(byte[]));

		// Token: 0x0400086D RID: 2157
		internal static readonly HygienePropertyDefinition ByteValue2Prop = new HygienePropertyDefinition("ByteValue2", typeof(byte[]));
	}
}
