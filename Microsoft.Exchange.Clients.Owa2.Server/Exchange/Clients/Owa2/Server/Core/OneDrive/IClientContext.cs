using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Sharing;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000003 RID: 3
	public interface IClientContext : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		// (set) Token: 0x06000004 RID: 4
		ICredentials Credentials { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5
		// (set) Token: 0x06000006 RID: 6
		bool FormDigestHandlingEnabled { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7
		// (set) Token: 0x06000008 RID: 8
		string TraceCorrelationId { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9
		string Url { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10
		IWeb Web { get; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000B RID: 11
		// (remove) Token: 0x0600000C RID: 12
		event EventHandler<WebRequestEventArgs> ExecutingWebRequest;

		// Token: 0x0600000D RID: 13
		void ExecuteQuery();

		// Token: 0x0600000E RID: 14
		Uri WebUrlFromFolderUrlDirect(Uri folderFullUrl);

		// Token: 0x0600000F RID: 15
		Uri WebUrlFromPageUrlDirect(Uri fileUri);

		// Token: 0x06000010 RID: 16
		IList<IUserSharingResult> DocumentSharingManagerUpdateDocumentSharingInfo(string resourceAddress, IList<UserRoleAssignment> userRoleAssignments, bool validateExistingPermissions, bool additiveMode, bool sendServerManagedNotification, string customMessage, bool includeAnonymousLinksInNotification);
	}
}
