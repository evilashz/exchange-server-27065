using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200069D RID: 1693
	internal class JsonMetadataDocument
	{
		// Token: 0x04003585 RID: 13701
		public string id;

		// Token: 0x04003586 RID: 13702
		public string version;

		// Token: 0x04003587 RID: 13703
		public string name;

		// Token: 0x04003588 RID: 13704
		public string realm;

		// Token: 0x04003589 RID: 13705
		public string serviceName;

		// Token: 0x0400358A RID: 13706
		public string issuer;

		// Token: 0x0400358B RID: 13707
		public string[] allowedAudiences;

		// Token: 0x0400358C RID: 13708
		public JsonKey[] keys;

		// Token: 0x0400358D RID: 13709
		public JsonEndpoint[] endpoints;
	}
}
