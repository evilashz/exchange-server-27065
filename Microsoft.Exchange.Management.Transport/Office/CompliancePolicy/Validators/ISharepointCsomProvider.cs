using System;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Query;

namespace Microsoft.Office.CompliancePolicy.Validators
{
	// Token: 0x02000140 RID: 320
	internal interface ISharepointCsomProvider
	{
		// Token: 0x06000E04 RID: 3588
		void LoadWebInfo(ClientContext context, out string webUrl, out string webTitle, out Guid siteId, out Guid webId);

		// Token: 0x06000E05 RID: 3589
		ResultTableCollection ExecuteSearch(ClientContext context, string location, bool searchOnlySiteCollection);

		// Token: 0x06000E06 RID: 3590
		ResultTableCollection ExecuteSearch(ClientContext context, Guid webId, Guid siteId);
	}
}
