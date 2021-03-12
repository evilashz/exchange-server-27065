using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200005D RID: 93
	internal interface IDemandJobNotification
	{
		// Token: 0x060002CE RID: 718
		void OnBeforeDemandJob(Guid mailboxGuid, Guid databaseGuid);
	}
}
