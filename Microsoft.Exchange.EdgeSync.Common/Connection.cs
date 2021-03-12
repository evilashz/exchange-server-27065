using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x0200001B RID: 27
	internal class Connection : IDisposable
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00003516 File Offset: 0x00001716
		public Connection()
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000351E File Offset: 0x0000171E
		public Connection(PooledLdapConnection connection)
		{
			this.connection = connection;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000352D File Offset: 0x0000172D
		public Connection(PooledLdapConnection connection, EdgeSyncAppConfig appConfig)
		{
			if (appConfig == null)
			{
				throw new ArgumentNullException("appConfig");
			}
			this.connection = connection;
			this.appConfig = appConfig;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003551 File Offset: 0x00001751
		public virtual string Fqdn
		{
			get
			{
				return this.connection.ServerName;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000355E File Offset: 0x0000175E
		public IEnumerable<ExSearchResultEntry> PagedScan(string baseDN, string query, params string[] attributes)
		{
			return this.PagedScan(baseDN, query, SearchScope.Subtree, attributes);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000038A4 File Offset: 0x00001AA4
		public virtual IEnumerable<ExSearchResultEntry> PagedScan(string baseDN, string query, SearchScope scope, params string[] attributes)
		{
			byte[] lastPageCookie = null;
			do
			{
				SearchRequest request = new SearchRequest(baseDN, query, scope, attributes);
				request.Attributes.Add("objectClass");
				PageResultRequestControl pageControl = (lastPageCookie == null) ? new PageResultRequestControl() : new PageResultRequestControl(lastPageCookie);
				pageControl.PageSize = 1000;
				pageControl.IsCritical = false;
				request.Controls.Add(pageControl);
				request.TimeLimit = Connection.DefaultSearchRequestTimeout;
				this.connection.Timeout = request.TimeLimit + Connection.ConnectionTimeoutPadding;
				SearchResponse response;
				try
				{
					response = (SearchResponse)this.SendRequest(request);
				}
				catch (ExDirectoryException ex)
				{
					if (ex.ResultCode == ResultCode.NoSuchObject)
					{
						yield break;
					}
					throw;
				}
				foreach (object obj in response.Entries)
				{
					SearchResultEntry resultEntry = (SearchResultEntry)obj;
					yield return new ExSearchResultEntry(resultEntry);
				}
				if (response.Controls.Length == 0)
				{
					break;
				}
				PageResultResponseControl pagedResponse = (PageResultResponseControl)response.Controls[0];
				lastPageCookie = pagedResponse.Cookie;
			}
			while (lastPageCookie != null && lastPageCookie.Length != 0);
			yield break;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public virtual IEnumerable<ExSearchResultEntry> DirSyncScan(Cookie cookie, string query, SearchScope scope, params string[] attributes)
		{
			SearchRequest request = new SearchRequest(cookie.BaseDN, query, scope, attributes);
			DirSyncRequestControl dirsyncControl = new DirSyncRequestControl(null, DirectorySynchronizationOptions.ObjectSecurity);
			dirsyncControl.IsCritical = true;
			dirsyncControl.Cookie = cookie.CookieValue;
			request.Controls.Add(dirsyncControl);
			DirSyncResponseControl dirSyncResponseControl = null;
			bool hasDirSyncResponseControl = false;
			for (;;)
			{
				ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Connection.DirSyncScan sending LDAP query: {0}", query);
				SearchResponse response;
				try
				{
					response = (SearchResponse)this.SendRequest(request);
				}
				catch (ExDirectoryException ex)
				{
					if (ex.ResultCode == ResultCode.NoSuchObject)
					{
						yield break;
					}
					throw;
				}
				foreach (DirectoryControl directoryControl in response.Controls)
				{
					if (directoryControl is DirSyncResponseControl)
					{
						hasDirSyncResponseControl = true;
						dirSyncResponseControl = (DirSyncResponseControl)directoryControl;
						dirsyncControl.Cookie = dirSyncResponseControl.Cookie;
						cookie.CookieValue = dirSyncResponseControl.Cookie;
					}
				}
				if (!hasDirSyncResponseControl)
				{
					break;
				}
				int entriesWithNullDN = 0;
				int entriesWithEmptyDN = 0;
				foreach (object obj in response.Entries)
				{
					SearchResultEntry resultEntry = (SearchResultEntry)obj;
					if (resultEntry.DistinguishedName == null)
					{
						entriesWithNullDN++;
					}
					else if (resultEntry.DistinguishedName == string.Empty)
					{
						entriesWithEmptyDN++;
					}
					else
					{
						yield return new ExSearchResultEntry(resultEntry);
					}
				}
				if (entriesWithNullDN > 0)
				{
					ExTraceGlobals.ConnectionTracer.TraceWarning<int>((long)this.GetHashCode(), "Connection.DirSyncScan encountered {0} search results with null DistinguishedName", entriesWithNullDN);
				}
				if (entriesWithEmptyDN > 0)
				{
					ExTraceGlobals.ConnectionTracer.TraceWarning<int>((long)this.GetHashCode(), "Connection.DirSyncScan encountered {0} search results with empty DistinguishedName", entriesWithEmptyDN);
				}
				if (dirSyncResponseControl == null || !dirSyncResponseControl.MoreData)
				{
					goto IL_29E;
				}
			}
			throw new InvalidOperationException("No DirSync control retured from DirSync");
			IL_29E:
			yield break;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003D22 File Offset: 0x00001F22
		public virtual ExSearchResultEntry ReadObjectEntry(string absolutePath, params string[] attributes)
		{
			return this.ReadObjectEntry(absolutePath, false, attributes);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003D30 File Offset: 0x00001F30
		public virtual ExSearchResultEntry ReadObjectEntry(string absolutePath, bool readDeleted, params string[] attributes)
		{
			ExSearchResultEntry result = null;
			try
			{
				SearchRequest searchRequest = new SearchRequest(absolutePath, Schema.Query.QueryAll, SearchScope.Base, attributes);
				searchRequest.Attributes.Add("objectClass");
				if (readDeleted)
				{
					searchRequest.Controls.Add(new ShowDeletedControl());
				}
				SearchResponse searchResponse = (SearchResponse)this.SendRequest(searchRequest);
				if (searchResponse.Entries.Count > 0)
				{
					result = new ExSearchResultEntry(searchResponse.Entries[0]);
				}
			}
			catch (ExDirectoryException ex)
			{
				if (ex.ResultCode != ResultCode.NoSuchObject)
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public virtual Dictionary<string, DirectoryAttribute> ReadObjectAttribute(string absolutePath, bool supportDeleted, params string[] attributes)
		{
			Dictionary<string, DirectoryAttribute> dictionary = new Dictionary<string, DirectoryAttribute>();
			try
			{
				SearchRequest searchRequest = new SearchRequest(absolutePath, Schema.Query.QueryAll, SearchScope.Base, attributes);
				if (supportDeleted)
				{
					DirectoryControl control = new DirectoryControl("1.2.840.113556.1.4.417", null, true, true);
					searchRequest.Controls.Add(control);
				}
				SearchResponse searchResponse = (SearchResponse)this.SendRequest(searchRequest);
				if (searchResponse.Entries.Count > 0)
				{
					foreach (object obj in searchResponse.Entries[0].Attributes.Values)
					{
						DirectoryAttribute directoryAttribute = (DirectoryAttribute)obj;
						dictionary.Add(directoryAttribute.Name, directoryAttribute);
					}
				}
			}
			catch (ExDirectoryException ex)
			{
				if (ex.ResultCode != ResultCode.NoSuchObject)
				{
					throw;
				}
			}
			return dictionary;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003EAC File Offset: 0x000020AC
		public void Dispose()
		{
			if (this.connection != null)
			{
				this.connection.ReturnToPool();
				this.connection = null;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003EC8 File Offset: 0x000020C8
		internal virtual DirectoryResponse SendRequest(DirectoryRequest request)
		{
			DirectoryResponse result;
			try
			{
				if (this.appConfig != null && this.appConfig.DelayLdapEnabled)
				{
					this.DelayLdapIfNeeded(request);
				}
				result = this.connection.SendRequest(request, Connection.requestTimeout);
			}
			catch (DirectoryOperationException e)
			{
				throw new ExDirectoryException(e);
			}
			catch (DirectoryException e2)
			{
				throw new ExDirectoryException(e2);
			}
			return result;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003F34 File Offset: 0x00002134
		private static void DelayLdapOrTimeout(TimeSpan delayTime)
		{
			if (delayTime == TimeSpan.MaxValue)
			{
				throw new DirectoryException("LDAP request timeout under injection.");
			}
			Thread.Sleep(delayTime);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003F54 File Offset: 0x00002154
		private void DelayLdapIfNeeded(DirectoryRequest request)
		{
			SearchRequest searchRequest = request as SearchRequest;
			if (searchRequest != null)
			{
				if (this.appConfig.DelayLdapSearchRequest != TimeSpan.MinValue)
				{
					Connection.DelayLdapOrTimeout(this.appConfig.DelayLdapSearchRequest);
					return;
				}
			}
			else if (this.appConfig.DelayLdapUpdateRequest != TimeSpan.MinValue && !string.IsNullOrEmpty(this.appConfig.DelayLdapUpdateRequestContainingString))
			{
				AddRequest addRequest = request as AddRequest;
				if (addRequest != null && !string.IsNullOrEmpty(addRequest.DistinguishedName) && addRequest.DistinguishedName.IndexOf(this.appConfig.DelayLdapUpdateRequestContainingString, StringComparison.OrdinalIgnoreCase) != -1)
				{
					Connection.DelayLdapOrTimeout(this.appConfig.DelayLdapUpdateRequest);
					return;
				}
				DeleteRequest deleteRequest = request as DeleteRequest;
				if (deleteRequest != null && !string.IsNullOrEmpty(deleteRequest.DistinguishedName) && deleteRequest.DistinguishedName.IndexOf(this.appConfig.DelayLdapUpdateRequestContainingString, StringComparison.OrdinalIgnoreCase) != -1)
				{
					Connection.DelayLdapOrTimeout(this.appConfig.DelayLdapUpdateRequest);
					return;
				}
				ModifyRequest modifyRequest = request as ModifyRequest;
				if (modifyRequest != null && !string.IsNullOrEmpty(modifyRequest.DistinguishedName) && modifyRequest.DistinguishedName.IndexOf(this.appConfig.DelayLdapUpdateRequestContainingString, StringComparison.OrdinalIgnoreCase) != -1)
				{
					Connection.DelayLdapOrTimeout(this.appConfig.DelayLdapUpdateRequest);
				}
			}
		}

		// Token: 0x04000053 RID: 83
		private const string ShowDeletedObjectOid = "1.2.840.113556.1.4.417";

		// Token: 0x04000054 RID: 84
		private static readonly TimeSpan ConnectionTimeoutPadding = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000055 RID: 85
		private static readonly TimeSpan DefaultSearchRequestTimeout = TimeSpan.FromSeconds(45.0);

		// Token: 0x04000056 RID: 86
		private static readonly TimeSpan requestTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000057 RID: 87
		private readonly EdgeSyncAppConfig appConfig;

		// Token: 0x04000058 RID: 88
		private PooledLdapConnection connection;
	}
}
