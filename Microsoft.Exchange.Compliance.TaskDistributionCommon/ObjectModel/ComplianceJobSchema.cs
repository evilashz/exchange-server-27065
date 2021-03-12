using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x02000026 RID: 38
	internal class ComplianceJobSchema : ObjectSchema
	{
		// Token: 0x04000078 RID: 120
		public static readonly SimpleProviderPropertyDefinition DisplayName = new SimpleProviderPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000079 RID: 121
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2007, typeof(ComplianceJobId), PropertyDefinitionFlags.None, new ComplianceJobId(), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400007A RID: 122
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2007, typeof(ObjectState), PropertyDefinitionFlags.None, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400007B RID: 123
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2007, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.PersistDefaultValue, ExchangeObjectVersion.Exchange2007, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400007C RID: 124
		public static readonly SimpleProviderPropertyDefinition CreatedTime = new SimpleProviderPropertyDefinition("CreatedTime", ExchangeObjectVersion.Exchange2007, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400007D RID: 125
		public static readonly SimpleProviderPropertyDefinition LastModifiedTime = new SimpleProviderPropertyDefinition("LastModifiedTime", ExchangeObjectVersion.Exchange2007, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400007E RID: 126
		public static readonly SimpleProviderPropertyDefinition JobStartTime = new SimpleProviderPropertyDefinition("JobStartTime", ExchangeObjectVersion.Exchange2007, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400007F RID: 127
		public static readonly SimpleProviderPropertyDefinition JobEndTime = new SimpleProviderPropertyDefinition("JobEndTime", ExchangeObjectVersion.Exchange2007, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000080 RID: 128
		public static readonly SimpleProviderPropertyDefinition Description = new SimpleProviderPropertyDefinition("Description", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000081 RID: 129
		public static readonly SimpleProviderPropertyDefinition JobType = new SimpleProviderPropertyDefinition("JobType", ExchangeObjectVersion.Exchange2007, typeof(ComplianceJobType), PropertyDefinitionFlags.None, ComplianceJobType.UnknownType, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000082 RID: 130
		public static readonly SimpleProviderPropertyDefinition CreatedBy = new SimpleProviderPropertyDefinition("CreatedBy", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000083 RID: 131
		public static readonly SimpleProviderPropertyDefinition RunBy = new SimpleProviderPropertyDefinition("RunBy", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000084 RID: 132
		public static readonly SimpleProviderPropertyDefinition JobObjectVersion = new SimpleProviderPropertyDefinition("JobObjectVersion", ExchangeObjectVersion.Exchange2007, typeof(ComplianceJobObjectVersion), PropertyDefinitionFlags.None, ComplianceJobObjectVersion.VersionUnknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000085 RID: 133
		public static readonly SimpleProviderPropertyDefinition TenantId = new SimpleProviderPropertyDefinition("TenantId", ExchangeObjectVersion.Exchange2007, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000086 RID: 134
		public static readonly SimpleProviderPropertyDefinition NumBindings = new SimpleProviderPropertyDefinition("NumBindings", ExchangeObjectVersion.Exchange2007, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000087 RID: 135
		public static readonly SimpleProviderPropertyDefinition NumBindingsFailed = new SimpleProviderPropertyDefinition("NumBindingsFailed", ExchangeObjectVersion.Exchange2007, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000088 RID: 136
		public static readonly SimpleProviderPropertyDefinition JobStatus = new SimpleProviderPropertyDefinition("JobStatus", ExchangeObjectVersion.Exchange2007, typeof(ComplianceJobStatus), PropertyDefinitionFlags.None, ComplianceJobStatus.NotStarted, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000089 RID: 137
		public static readonly SimpleProviderPropertyDefinition ExchangeBindings = new SimpleProviderPropertyDefinition("ExchangeBindings", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400008A RID: 138
		public static readonly SimpleProviderPropertyDefinition PublicFolderBindings = new SimpleProviderPropertyDefinition("PublicFolderBindings", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400008B RID: 139
		public static readonly SimpleProviderPropertyDefinition SharePointBindings = new SimpleProviderPropertyDefinition("SharePointBindings", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400008C RID: 140
		public static readonly SimpleProviderPropertyDefinition AllExchangeBindings = new SimpleProviderPropertyDefinition("AllExchangeBindings", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400008D RID: 141
		public static readonly SimpleProviderPropertyDefinition AllSharePointBindings = new SimpleProviderPropertyDefinition("AllSharePointBindings", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400008E RID: 142
		public static readonly SimpleProviderPropertyDefinition AllPublicFolderBindings = new SimpleProviderPropertyDefinition("AllPublicFolderBindings", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400008F RID: 143
		public static readonly SimpleProviderPropertyDefinition Resume = new SimpleProviderPropertyDefinition("Resume", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000090 RID: 144
		public static readonly SimpleProviderPropertyDefinition JobRunId = new SimpleProviderPropertyDefinition("JobRunId", ExchangeObjectVersion.Exchange2007, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000091 RID: 145
		public static readonly SimpleProviderPropertyDefinition TenantInfo = new SimpleProviderPropertyDefinition("TenantInfo", ExchangeObjectVersion.Exchange2007, typeof(byte[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
