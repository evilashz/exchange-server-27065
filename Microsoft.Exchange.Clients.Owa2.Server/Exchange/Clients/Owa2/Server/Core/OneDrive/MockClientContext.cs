using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Sharing;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000005 RID: 5
	public class MockClientContext : IClientContext, IDisposable
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002314 File Offset: 0x00000514
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000231C File Offset: 0x0000051C
		public ICredentials Credentials { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002325 File Offset: 0x00000525
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000232D File Offset: 0x0000052D
		public bool FormDigestHandlingEnabled { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002336 File Offset: 0x00000536
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000233E File Offset: 0x0000053E
		public string TraceCorrelationId { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002347 File Offset: 0x00000547
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000234F File Offset: 0x0000054F
		public string Url { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002358 File Offset: 0x00000558
		public IWeb Web
		{
			get
			{
				MockWeb result;
				if ((result = this.web) == null)
				{
					result = (this.web = new MockWeb(this));
				}
				return result;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600002D RID: 45 RVA: 0x00002380 File Offset: 0x00000580
		// (remove) Token: 0x0600002E RID: 46 RVA: 0x000023B8 File Offset: 0x000005B8
		public event EventHandler<WebRequestEventArgs> ExecutingWebRequest;

		// Token: 0x0600002F RID: 47 RVA: 0x000023ED File Offset: 0x000005ED
		public MockClientContext(string url)
		{
			this.Url = url;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000023FC File Offset: 0x000005FC
		public void ExecuteQuery()
		{
			EventHandler<WebRequestEventArgs> executingWebRequest = this.ExecutingWebRequest;
			if (executingWebRequest != null)
			{
				executingWebRequest(this, null);
			}
			this.TraceCorrelationId = Guid.NewGuid().ToString();
			if (this.mockClientObjectsToLoad != null)
			{
				foreach (MockClientObject mockClientObject in this.mockClientObjectsToLoad)
				{
					mockClientObject.LoadMockData();
				}
				this.mockClientObjectsToLoad.Clear();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000248C File Offset: 0x0000068C
		public Uri WebUrlFromFolderUrlDirect(Uri folderFullUrl)
		{
			return new Uri(this.Url);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002499 File Offset: 0x00000699
		public Uri WebUrlFromPageUrlDirect(Uri fileUri)
		{
			return new Uri(this.Url);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000024A8 File Offset: 0x000006A8
		public IList<IUserSharingResult> DocumentSharingManagerUpdateDocumentSharingInfo(string resourceAddress, IList<UserRoleAssignment> userRoleAssignments, bool validateExistingPermissions, bool additiveMode, bool sendServerManagedNotification, string customMessage, bool includeAnonymousLinksInNotification)
		{
			List<IUserSharingResult> list = new List<IUserSharingResult>();
			foreach (UserRoleAssignment userRoleAssignment in userRoleAssignments)
			{
				list.Add(new MockUserSharingResult
				{
					Status = true,
					User = userRoleAssignment.UserId
				});
			}
			return list;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002510 File Offset: 0x00000710
		public void Load(MockClientObject clientObject)
		{
			if (this.mockClientObjectsToLoad == null)
			{
				this.mockClientObjectsToLoad = new List<MockClientObject>();
			}
			this.mockClientObjectsToLoad.Add(clientObject);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002534 File Offset: 0x00000734
		public void Dispose()
		{
			if (this.disposeList != null)
			{
				foreach (IDisposable disposable in this.disposeList)
				{
					disposable.Dispose();
				}
				this.disposeList.Clear();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000259C File Offset: 0x0000079C
		public void AddToDisposeList(IDisposable toBeDisposed)
		{
			if (this.disposeList == null)
			{
				this.disposeList = new List<IDisposable>();
			}
			this.disposeList.Add(toBeDisposed);
		}

		// Token: 0x04000005 RID: 5
		private const string DefaultMockAttachmentDataProviderFilePath = "C:\\MockAttachmentDataProvider";

		// Token: 0x04000006 RID: 6
		internal static readonly string MockAttachmentDataProviderFilePath = new StringAppSettingsEntry("MockAttachmentDataProviderFilePath", "C:\\MockAttachmentDataProvider", ExTraceGlobals.AttachmentHandlingTracer).Value;

		// Token: 0x04000007 RID: 7
		private MockWeb web;

		// Token: 0x04000008 RID: 8
		private List<MockClientObject> mockClientObjectsToLoad;

		// Token: 0x04000009 RID: 9
		private List<IDisposable> disposeList;
	}
}
