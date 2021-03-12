using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Sharing;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000004 RID: 4
	public class ClientContextWrapper : IClientContext, IDisposable
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002102 File Offset: 0x00000302
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000210A File Offset: 0x0000030A
		public ClientContext BackingClientContext { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002113 File Offset: 0x00000313
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002120 File Offset: 0x00000320
		public ICredentials Credentials
		{
			get
			{
				return this.BackingClientContext.Credentials;
			}
			set
			{
				this.BackingClientContext.Credentials = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000212E File Offset: 0x0000032E
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000213B File Offset: 0x0000033B
		public bool FormDigestHandlingEnabled
		{
			get
			{
				return this.BackingClientContext.FormDigestHandlingEnabled;
			}
			set
			{
				this.BackingClientContext.FormDigestHandlingEnabled = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002149 File Offset: 0x00000349
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002156 File Offset: 0x00000356
		public string TraceCorrelationId
		{
			get
			{
				return this.BackingClientContext.TraceCorrelationId;
			}
			set
			{
				this.BackingClientContext.TraceCorrelationId = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002164 File Offset: 0x00000364
		public string Url
		{
			get
			{
				return this.BackingClientContext.Url;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002174 File Offset: 0x00000374
		public IWeb Web
		{
			get
			{
				WebWrapper result;
				if ((result = this.web) == null)
				{
					result = (this.web = new WebWrapper(this.BackingClientContext.Web));
				}
				return result;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001B RID: 27 RVA: 0x000021A4 File Offset: 0x000003A4
		// (remove) Token: 0x0600001C RID: 28 RVA: 0x000021DC File Offset: 0x000003DC
		public event EventHandler<WebRequestEventArgs> ExecutingWebRequest;

		// Token: 0x0600001D RID: 29 RVA: 0x00002234 File Offset: 0x00000434
		public ClientContextWrapper(string url)
		{
			this.BackingClientContext = new ClientContext(url);
			this.BackingClientContext.ExecutingWebRequest += delegate(object sender, WebRequestEventArgs args)
			{
				EventHandler<WebRequestEventArgs> executingWebRequest = this.ExecutingWebRequest;
				if (executingWebRequest != null)
				{
					executingWebRequest(this, args);
				}
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002271 File Offset: 0x00000471
		public void Dispose()
		{
			this.BackingClientContext.Dispose();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000227E File Offset: 0x0000047E
		public void ExecuteQuery()
		{
			this.BackingClientContext.ExecuteQuery();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000228B File Offset: 0x0000048B
		public Uri WebUrlFromFolderUrlDirect(Uri folderFullUrl)
		{
			return Microsoft.SharePoint.Client.Web.WebUrlFromFolderUrlDirect(this.BackingClientContext, folderFullUrl);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002299 File Offset: 0x00000499
		public Uri WebUrlFromPageUrlDirect(Uri fileUri)
		{
			return Microsoft.SharePoint.Client.Web.WebUrlFromPageUrlDirect(this.BackingClientContext, fileUri);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022A8 File Offset: 0x000004A8
		public IList<IUserSharingResult> DocumentSharingManagerUpdateDocumentSharingInfo(string resourceAddress, IList<UserRoleAssignment> userRoleAssignments, bool validateExistingPermissions, bool additiveMode, bool sendServerManagedNotification, string customMessage, bool includeAnonymousLinksInNotification)
		{
			IList<UserSharingResult> list = DocumentSharingManager.UpdateDocumentSharingInfo(this.BackingClientContext, resourceAddress, userRoleAssignments, validateExistingPermissions, additiveMode, sendServerManagedNotification, customMessage, includeAnonymousLinksInNotification);
			List<IUserSharingResult> list2 = new List<IUserSharingResult>();
			foreach (UserSharingResult result in list)
			{
				list2.Add(new UserSharingResultWrapper(result));
			}
			return list2;
		}

		// Token: 0x04000002 RID: 2
		private WebWrapper web;
	}
}
