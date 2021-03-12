using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MigrationDataProviderEmailHandler : IMigrationEmailHandler
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000036BB File Offset: 0x000018BB
		public MigrationDataProviderEmailHandler(IMigrationDataProvider dataProvider)
		{
			this.dataProvider = dataProvider;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000036CA File Offset: 0x000018CA
		public IMigrationEmailMessageItem CreateEmailMessage()
		{
			return this.dataProvider.CreateEmailMessage();
		}

		// Token: 0x04000026 RID: 38
		private readonly IMigrationDataProvider dataProvider;
	}
}
