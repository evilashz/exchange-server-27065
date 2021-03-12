using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020001A2 RID: 418
	internal interface IStoreUserInformationReader
	{
		// Token: 0x060011B7 RID: 4535
		object[] ReadUserInformation(Guid databaseGuid, Guid userInformationGuid, uint[] propertyTags);
	}
}
