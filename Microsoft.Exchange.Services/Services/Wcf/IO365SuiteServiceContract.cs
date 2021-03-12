using System;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020008FD RID: 2301
	public interface IO365SuiteServiceContract
	{
		// Token: 0x0600411D RID: 16669
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		SuiteStorageJsonResponse ProcessO365SuiteStorage(SuiteStorageJsonRequest request);
	}
}
