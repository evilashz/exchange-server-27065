using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000824 RID: 2084
	internal class SyncContactSchema : SyncOrgPersonSchema
	{
		// Token: 0x1700245C RID: 9308
		// (get) Token: 0x06006701 RID: 26369 RVA: 0x0016CFD1 File Offset: 0x0016B1D1
		public override DirectoryObjectClass DirectoryObjectClass
		{
			get
			{
				return DirectoryObjectClass.Contact;
			}
		}

		// Token: 0x04004442 RID: 17474
		public static SyncPropertyDefinition Manager = SyncUserSchema.Manager;
	}
}
