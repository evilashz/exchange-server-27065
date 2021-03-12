using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001D6 RID: 470
	public interface IUploadHandler
	{
		// Token: 0x17001B91 RID: 7057
		// (get) Token: 0x0600257C RID: 9596
		Type SetParameterType { get; }

		// Token: 0x0600257D RID: 9597
		PowerShellResults ProcessUpload(UploadFileContext context, WebServiceParameters parameters);

		// Token: 0x17001B92 RID: 7058
		// (get) Token: 0x0600257E RID: 9598
		int MaxFileSize { get; }
	}
}
