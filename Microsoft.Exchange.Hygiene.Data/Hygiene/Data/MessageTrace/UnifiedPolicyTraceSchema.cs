using System;
using System.Data.SqlTypes;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001AC RID: 428
	internal class UnifiedPolicyTraceSchema
	{
		// Token: 0x04000896 RID: 2198
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = UnifiedPolicyCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000897 RID: 2199
		internal static readonly HygienePropertyDefinition ObjectIdProperty = UnifiedPolicyCommonSchema.ObjectIdProperty;

		// Token: 0x04000898 RID: 2200
		internal static readonly HygienePropertyDefinition DataSourceProperty = UnifiedPolicyCommonSchema.DataSourceProperty;

		// Token: 0x04000899 RID: 2201
		internal static readonly HygienePropertyDefinition FileIdProperty = new HygienePropertyDefinition("FileId", typeof(Guid));

		// Token: 0x0400089A RID: 2202
		internal static readonly HygienePropertyDefinition FileNameProperty = new HygienePropertyDefinition("FileName", typeof(string));

		// Token: 0x0400089B RID: 2203
		internal static readonly HygienePropertyDefinition SizeProperty = new HygienePropertyDefinition("Size", typeof(long), 0L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400089C RID: 2204
		internal static readonly HygienePropertyDefinition SiteIdProperty = new HygienePropertyDefinition("SiteId", typeof(Guid));

		// Token: 0x0400089D RID: 2205
		internal static readonly HygienePropertyDefinition FileUrlProperty = new HygienePropertyDefinition("FileUrl", typeof(string));

		// Token: 0x0400089E RID: 2206
		internal static readonly HygienePropertyDefinition OwnerProperty = new HygienePropertyDefinition("Owner", typeof(string));

		// Token: 0x0400089F RID: 2207
		internal static readonly HygienePropertyDefinition IsViewableByExternalUsersProperty = new HygienePropertyDefinition("IsViewableByExternalUsers", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008A0 RID: 2208
		internal static readonly HygienePropertyDefinition LastModifiedByProperty = new HygienePropertyDefinition("LastModifiedBy", typeof(string));

		// Token: 0x040008A1 RID: 2209
		internal static readonly HygienePropertyDefinition CreateTimeProperty = new HygienePropertyDefinition("CreateTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008A2 RID: 2210
		internal static readonly HygienePropertyDefinition LastModifiedTimeProperty = new HygienePropertyDefinition("LastModifiedTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008A3 RID: 2211
		internal static readonly HygienePropertyDefinition PolicyMatchTimeProperty = new HygienePropertyDefinition("PolicyMatchTime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
