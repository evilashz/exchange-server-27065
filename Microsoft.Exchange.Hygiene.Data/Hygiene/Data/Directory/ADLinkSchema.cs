using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000BE RID: 190
	internal class ADLinkSchema : ADObjectSchema
	{
		// Token: 0x040003D7 RID: 983
		public static readonly HygienePropertyDefinition SourceIdProperty = new HygienePropertyDefinition("SourceId", typeof(ADObjectId));

		// Token: 0x040003D8 RID: 984
		public static readonly HygienePropertyDefinition DestinationIdProperty = new HygienePropertyDefinition("DestinationId", typeof(ADObjectId));

		// Token: 0x040003D9 RID: 985
		public static readonly HygienePropertyDefinition SourceTypeProperty = new HygienePropertyDefinition("SourceType", typeof(DirectoryObjectClass));

		// Token: 0x040003DA RID: 986
		public static readonly HygienePropertyDefinition DestinationTypeProperty = new HygienePropertyDefinition("DestinationType", typeof(DirectoryObjectClass));

		// Token: 0x040003DB RID: 987
		public static readonly HygienePropertyDefinition LinkTypeProperty = new HygienePropertyDefinition("LinkType", typeof(LinkType));
	}
}
