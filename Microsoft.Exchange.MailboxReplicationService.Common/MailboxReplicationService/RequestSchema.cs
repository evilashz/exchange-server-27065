using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001E3 RID: 483
	internal class RequestSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000A3C RID: 2620
		public static readonly SimpleProviderPropertyDefinition Name = RequestJobSchema.Name;

		// Token: 0x04000A3D RID: 2621
		public static readonly SimpleProviderPropertyDefinition ExchangeGuid = RequestJobSchema.ExchangeGuid;

		// Token: 0x04000A3E RID: 2622
		public static readonly SimpleProviderPropertyDefinition Flags = RequestJobSchema.Flags;

		// Token: 0x04000A3F RID: 2623
		public static readonly SimpleProviderPropertyDefinition RemoteHostName = RequestJobSchema.RemoteHostName;

		// Token: 0x04000A40 RID: 2624
		public static readonly SimpleProviderPropertyDefinition BatchName = RequestJobSchema.BatchName;

		// Token: 0x04000A41 RID: 2625
		public static readonly SimpleProviderPropertyDefinition Status = RequestJobSchema.Status;

		// Token: 0x04000A42 RID: 2626
		public static readonly SimpleProviderPropertyDefinition SourceDatabase = RequestJobSchema.SourceDatabase;

		// Token: 0x04000A43 RID: 2627
		public static readonly SimpleProviderPropertyDefinition TargetDatabase = RequestJobSchema.TargetDatabase;

		// Token: 0x04000A44 RID: 2628
		public static readonly SimpleProviderPropertyDefinition FilePath = RequestJobSchema.FilePath;

		// Token: 0x04000A45 RID: 2629
		public static readonly SimpleProviderPropertyDefinition SourceMailbox = RequestJobSchema.SourceUserId;

		// Token: 0x04000A46 RID: 2630
		public static readonly SimpleProviderPropertyDefinition TargetMailbox = RequestJobSchema.TargetUserId;

		// Token: 0x04000A47 RID: 2631
		public static readonly SimpleProviderPropertyDefinition RequestGuid = RequestJobSchema.RequestGuid;

		// Token: 0x04000A48 RID: 2632
		public static readonly SimpleProviderPropertyDefinition RequestQueue = RequestJobSchema.RequestQueue;

		// Token: 0x04000A49 RID: 2633
		public static readonly SimpleProviderPropertyDefinition OrganizationId = new SimpleProviderPropertyDefinition("OrganizationId", ExchangeObjectVersion.Exchange2010, typeof(OrganizationId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A4A RID: 2634
		public static readonly SimpleProviderPropertyDefinition WhenChanged = new SimpleProviderPropertyDefinition("WhenChanged", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A4B RID: 2635
		public static readonly SimpleProviderPropertyDefinition WhenCreated = new SimpleProviderPropertyDefinition("WhenCreated", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A4C RID: 2636
		public static readonly SimpleProviderPropertyDefinition WhenChangedUTC = new SimpleProviderPropertyDefinition("WhenChangedUTC", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000A4D RID: 2637
		public static readonly SimpleProviderPropertyDefinition WhenCreatedUTC = new SimpleProviderPropertyDefinition("WhenCreatedUTC", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
