using System;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000041 RID: 65
	internal interface ISubmissionConfiguration
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000298 RID: 664
		IAppConfiguration App { get; }

		// Token: 0x06000299 RID: 665
		void Load();

		// Token: 0x0600029A RID: 666
		void Unload();

		// Token: 0x0600029B RID: 667
		void ConfigUpdate();
	}
}
