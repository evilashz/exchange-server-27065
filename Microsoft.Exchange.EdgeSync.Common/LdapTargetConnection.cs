using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Net;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.MessageSecurity;
using Microsoft.Exchange.MessageSecurity.EdgeSync;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x0200001D RID: 29
	internal class LdapTargetConnection : TargetConnection
	{
		// Token: 0x060000CF RID: 207 RVA: 0x0000421F File Offset: 0x0000241F
		public LdapTargetConnection(int localServerVersion, TargetServerConfig config, EdgeSyncLogSession logSession) : base(localServerVersion, config)
		{
			this.logSession = logSession;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004230 File Offset: 0x00002430
		public LdapTargetConnection(int localServerVersion, TargetServerConfig config, NetworkCredential credentials, SyncTreeType type, EdgeSyncLogSession logSession) : base(localServerVersion, config)
		{
			this.type = type;
			this.logSession = logSession;
			LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier(string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				base.Host,
				base.Port
			}));
			ExTraceGlobals.ConnectionTracer.TraceDebug<string>((long)this.GetHashCode(), "Attempting to connect to {0}", base.Host);
			try
			{
				this.connection = new LdapConnection(identifier);
			}
			catch (LdapException ex)
			{
				ExTraceGlobals.ConnectionTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to create a LdapConnection to {0} because {1}", base.Host, ex.Message);
				throw new ExDirectoryException(ex);
			}
			this.connection.SessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback(this.VerifyServerCertificate);
			this.connection.SessionOptions.SecureSocketLayer = true;
			this.connection.SessionOptions.Signing = false;
			this.connection.SessionOptions.Sealing = false;
			this.connection.SessionOptions.ReferralChasing = ReferralChasingOptions.None;
			this.connection.SessionOptions.ProtocolVersion = 3;
			this.connection.AuthType = AuthType.Basic;
			try
			{
				this.connection.Bind(credentials);
			}
			catch (DirectoryException ex2)
			{
				ExTraceGlobals.ConnectionTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to connect to {0} because {1}", base.Host, ex2.Message);
				throw new ExDirectoryException(ex2);
			}
			SearchResponse searchResponse = (SearchResponse)this.SendRequest(new SearchRequest(string.Empty, Schema.Query.QueryAll, System.DirectoryServices.Protocols.SearchScope.Base, null));
			if (searchResponse.Entries.Count == 1)
			{
				SearchResultAttributeCollection attributes = searchResponse.Entries[0].Attributes;
				this.adamConfigurationNamingContext = (string)attributes["configurationNamingContext"][0];
				this.adamRootOrgContainerDN = "CN=First Organization,CN=Microsoft Exchange,CN=Services," + this.adamConfigurationNamingContext;
				string ldapFilter = "(&(objectClass=msExchExchangeServer)(networkAddress=ncacn_ip_tcp:" + base.Host + "))";
				SearchRequest request = new SearchRequest(this.adamConfigurationNamingContext, ldapFilter, System.DirectoryServices.Protocols.SearchScope.Subtree, new string[]
				{
					"name"
				});
				searchResponse = (SearchResponse)this.SendRequest(request);
				if (searchResponse.Entries.Count > 0)
				{
					this.serverDistinguishedName = searchResponse.Entries[0].DistinguishedName;
					this.serverName = (string)searchResponse.Entries[0].Attributes["name"][0];
					return;
				}
				throw new ExDirectoryException(ResultCode.NoSuchAttribute, "Could not read Edge server config object");
			}
			else
			{
				if (searchResponse.Entries.Count == 0)
				{
					throw new ExDirectoryException(ResultCode.NoSuchAttribute, "Could not read ADAM config naming context");
				}
				throw new InvalidOperationException("expected single result");
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000044F4 File Offset: 0x000026F4
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000044FC File Offset: 0x000026FC
		public SyncTreeType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004505 File Offset: 0x00002705
		public string AdamConfigurationNamingContext
		{
			get
			{
				return this.adamConfigurationNamingContext;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000450D File Offset: 0x0000270D
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004518 File Offset: 0x00002718
		public override bool TryReadCookie(out Dictionary<string, Cookie> cookies)
		{
			DirectoryAttribute directoryAttribute = null;
			cookies = new Dictionary<string, Cookie>();
			try
			{
				directoryAttribute = this.ReadMultivalueAttribute(this.serverDistinguishedName, "msExchEdgeSyncCookies");
			}
			catch (ExDirectoryException ex)
			{
				ExTraceGlobals.ConnectionTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to read cookie from {0} because {1}", this.serverDistinguishedName, ex.Message);
				return false;
			}
			try
			{
				if (directoryAttribute != null)
				{
					foreach (object obj in directoryAttribute)
					{
						byte[] bytes = (byte[])obj;
						Cookie cookie = Cookie.Deserialize(Encoding.ASCII.GetString(bytes));
						cookies.Add(cookie.BaseDN, cookie);
					}
				}
			}
			catch (FormatException ex2)
			{
				ExTraceGlobals.ConnectionTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to deserialize cookie from {0} because {1}", this.serverDistinguishedName, ex2.Message);
				return false;
			}
			catch (ArgumentNullException ex3)
			{
				ExTraceGlobals.ConnectionTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to deserialize cookie from {0} because {1}", this.serverDistinguishedName, ex3.Message);
				return false;
			}
			return true;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004654 File Offset: 0x00002854
		public override bool TrySaveCookie(Dictionary<string, Cookie> cookies)
		{
			object[] array = new object[cookies.Count];
			int num = 0;
			foreach (Cookie cookie in cookies.Values)
			{
				array[num++] = cookie.Serialize();
			}
			ModifyRequest request = new ModifyRequest(this.serverDistinguishedName, DirectoryAttributeOperation.Replace, "msExchEdgeSyncCookies", array);
			try
			{
				this.SendRequest(request);
			}
			catch (ExDirectoryException ex)
			{
				ExTraceGlobals.ConnectionTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to save cookie to {0} because {1}", this.serverDistinguishedName, ex.Message);
				return false;
			}
			return true;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004714 File Offset: 0x00002914
		public override void SetLease(LeaseToken newLeaseToken)
		{
			string stringForm = newLeaseToken.StringForm;
			ModifyRequest modifyRequest = new ModifyRequest();
			modifyRequest.DistinguishedName = this.serverDistinguishedName;
			DirectoryAttributeModification directoryAttributeModification = new DirectoryAttributeModification();
			directoryAttributeModification.Operation = DirectoryAttributeOperation.Replace;
			directoryAttributeModification.Name = "msExchEdgeSyncLease";
			directoryAttributeModification.Add(stringForm);
			modifyRequest.Modifications.Add(directoryAttributeModification);
			this.SendRequest(modifyRequest);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004770 File Offset: 0x00002970
		public override LeaseToken GetLease()
		{
			return LeaseToken.Parse(this.ReadSingleStringAttribute(this.serverDistinguishedName, "msExchEdgeSyncLease"));
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004788 File Offset: 0x00002988
		public override bool CanTakeOverLease(bool force, LeaseToken lease, DateTime now)
		{
			if (force)
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Forcefully take over the lease from {0}", lease.StringForm);
				return true;
			}
			if (lease.Expiry < now)
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Can take over expired lease {0}", lease.StringForm);
				return true;
			}
			ServerVersion serverVersion = new ServerVersion(base.LocalServerVersion);
			ServerVersion serverVersion2 = new ServerVersion(lease.Version);
			if ((serverVersion.Major == 14 || serverVersion.Major == 15) && serverVersion2.Major == 8 && serverVersion2.Minor == 2)
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Can take over E12 Sp2 lease {0}", lease.StringForm);
				return true;
			}
			return false;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004844 File Offset: 0x00002A44
		public override bool OnSynchronizing()
		{
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug((long)this.GetHashCode(), "Start synchronizing");
			return true;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000485D File Offset: 0x00002A5D
		public override bool OnSynchronized()
		{
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug((long)this.GetHashCode(), "Finished synchronization");
			return true;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004876 File Offset: 0x00002A76
		public override void OnConnectedToSource(Connection sourceConnection)
		{
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Connected to the source Domain Controller {0}", sourceConnection.Fqdn);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004894 File Offset: 0x00002A94
		public override SyncResult OnAddEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes)
		{
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "About to add to target from source DN {0}", entry.DistinguishedName);
			string targetPath = this.GetTargetPath(entry);
			try
			{
				AddRequest addRequest = new AddRequest(targetPath, entry.ObjectClass);
				foreach (DirectoryAttribute directoryAttribute in sourceAttributes.Values)
				{
					if (!directoryAttribute.Name.Equals("objectClass", StringComparison.OrdinalIgnoreCase))
					{
						if (directoryAttribute.Count == 0)
						{
							ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Skip attribute {0} because it is null", directoryAttribute.Name);
						}
						else
						{
							addRequest.Attributes.Add(directoryAttribute);
							if (ExTraceGlobals.SynchronizationJobTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								this.TraceAttributeValue(directoryAttribute);
							}
						}
					}
				}
				DirectoryAttribute attribute = new DirectoryAttribute("msExchEdgeSyncSourceGuid", new object[]
				{
					entry.Attributes["objectGUID"][0]
				});
				addRequest.Attributes.Add(attribute);
				if (ExTraceGlobals.SynchronizationJobTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					Guid guid = new Guid((byte[])entry.Attributes["objectGUID"][0]);
					ExTraceGlobals.SynchronizationJobTracer.TraceError<string>((long)this.GetHashCode(), "Adding sourceGuid {0}", guid.ToString());
				}
				DirectoryAttribute attribute2 = new DirectoryAttribute("systemFlags", 1107296256.ToString());
				addRequest.Attributes.Add(attribute2);
				this.SendRequest(addRequest);
				this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, "Added: " + entry.DistinguishedName, "Succcessfully Added Entry");
				ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Added entry {0}", targetPath);
			}
			catch (ExDirectoryException ex)
			{
				ResultCode resultCode = ex.ResultCode;
				if (resultCode == ResultCode.NoSuchObject)
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}'s parent container doesn't exist. Create one and retry", targetPath);
					this.EnsureTargetContainer(entry);
					return this.OnAddEntry(entry, sourceAttributes);
				}
				if (resultCode == ResultCode.EntryAlreadyExists)
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Add entry already exists for {0}. Try to modify instead.", targetPath);
					this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, entry.DistinguishedName, "Convert Add to Update");
					return this.OnModifyEntry(entry, sourceAttributes);
				}
				ExTraceGlobals.SynchronizationJobTracer.TraceError<string, ExDirectoryException>((long)this.GetHashCode(), "Failed to add entry {0} because {1}", targetPath, ex);
				throw;
			}
			return SyncResult.Added;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004B28 File Offset: 0x00002D28
		public override SyncResult OnModifyEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes)
		{
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "About to modify target from source DN {0}", entry.DistinguishedName);
			ModifyRequest modifyRequest = new ModifyRequest();
			modifyRequest.DistinguishedName = this.GetTargetPath(entry);
			this.SendRequest(this.AddAttributeModifications(entry, sourceAttributes, modifyRequest));
			this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, "Updated: " + entry.DistinguishedName, "Succcessfully Updated Entry");
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "Modified entry {0}", modifyRequest.DistinguishedName);
			return SyncResult.Modified;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public override SyncResult OnDeleteEntry(ExSearchResultEntry entry)
		{
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string>((long)this.GetHashCode(), "About to delete target with source DN {0}", entry.DistinguishedName);
			DirectoryAttribute directoryAttribute = entry.Attributes["objectGUID"];
			byte[] array = (byte[])directoryAttribute.GetValues(typeof(byte[]))[0];
			SyncResult syncResult = this.OnDeleteEntry(array);
			if (syncResult == SyncResult.Deleted)
			{
				this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, string.Format(CultureInfo.InvariantCulture, "Deleted: DN:{0}, Guid:{1}", new object[]
				{
					entry.DistinguishedName,
					new Guid(array).ToString()
				}), "Succcessfully Deleted Entry");
			}
			return syncResult;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004C60 File Offset: 0x00002E60
		public override SyncResult OnRenameEntry(ExSearchResultEntry entry)
		{
			if (this.type != SyncTreeType.Configuration)
			{
				return SyncResult.None;
			}
			ExSearchResultEntry targetEntry = this.GetTargetEntry(entry);
			if (targetEntry == null)
			{
				Guid empty = Guid.Empty;
				DirectoryAttribute directoryAttribute = null;
				if (entry.Attributes.TryGetValue("objectGUID", out directoryAttribute))
				{
					byte[] b = (byte[])directoryAttribute.GetValues(typeof(byte[]))[0];
					empty = new Guid(b);
				}
				this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, string.Format(CultureInfo.InvariantCulture, "Rename convert into Add/Modify for {0} because can't locate target object based on sourceGuid {1}", new object[]
				{
					entry.DistinguishedName,
					empty
				}), "Rename");
				return SyncResult.None;
			}
			string text = DistinguishedName.ExtractRDN(entry.DistinguishedName);
			ModifyDNRequest modifyDNRequest = new ModifyDNRequest();
			modifyDNRequest.NewName = text;
			modifyDNRequest.DistinguishedName = targetEntry.DistinguishedName;
			modifyDNRequest.NewParentDistinguishedName = DistinguishedName.Parent(targetEntry.DistinguishedName);
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, string>((long)this.GetHashCode(), "About to rename target DN {0} with new RDN {1}", targetEntry.DistinguishedName, text);
			this.SendRequest(modifyDNRequest);
			this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, string.Format(CultureInfo.InvariantCulture, "Rename: Target:{0}, NewRDN:{1}", new object[]
			{
				targetEntry.DistinguishedName,
				text
			}), "Successfully Renamed Entry");
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Rename target {0} with new RDN {1}", targetEntry.DistinguishedName, text);
			return SyncResult.Renamed;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000050E0 File Offset: 0x000032E0
		public virtual IEnumerable<ExSearchResultEntry> PagedScan(string absolutePath, string query, System.DirectoryServices.Protocols.SearchScope scope, params string[] attributes)
		{
			byte[] lastPageCookie = null;
			if (string.IsNullOrEmpty(absolutePath))
			{
				absolutePath = this.GetTargetBaseSearchPath();
			}
			do
			{
				SearchRequest request = new SearchRequest(absolutePath, query, scope, attributes);
				request.Attributes.Add("objectClass");
				PageResultRequestControl pageControl = (lastPageCookie == null) ? new PageResultRequestControl() : new PageResultRequestControl(lastPageCookie);
				pageControl.PageSize = 1000;
				pageControl.IsCritical = false;
				request.Controls.Add(pageControl);
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

		// Token: 0x060000E2 RID: 226 RVA: 0x0000511C File Offset: 0x0000331C
		public ExSearchResultEntry ReadObjectEntry(string absolutePath, params string[] attributes)
		{
			ExSearchResultEntry result = null;
			try
			{
				SearchResponse searchResponse = (SearchResponse)this.SendRequest(new SearchRequest(absolutePath, Schema.Query.QueryAll, System.DirectoryServices.Protocols.SearchScope.Base, attributes)
				{
					Attributes = 
					{
						"objectClass"
					}
				});
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

		// Token: 0x060000E3 RID: 227 RVA: 0x0000519C File Offset: 0x0000339C
		public SyncResult OnDeleteEntry(byte[] guid)
		{
			string str = ADValueConvertor.EscapeBinaryValue(guid);
			SearchRequest request = new SearchRequest(this.GetTargetBaseSearchPath(), "(msExchEdgeSyncSourceGuid=" + str + ")", System.DirectoryServices.Protocols.SearchScope.Subtree, new string[0]);
			SearchResultEntry searchResultEntry = null;
			SearchResponse searchResponse = (SearchResponse)this.SendRequest(request);
			if (searchResponse.Entries.Count > 0)
			{
				searchResultEntry = searchResponse.Entries[0];
				try
				{
					DeleteRequest request2 = new DeleteRequest(searchResultEntry.DistinguishedName);
					this.SendRequest(request2);
				}
				catch (ExDirectoryException ex)
				{
					if (ex.ResultCode != ResultCode.NoSuchObject)
					{
						if (ExTraceGlobals.SynchronizationJobTracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							ExTraceGlobals.SynchronizationJobTracer.TraceError<string, Guid>((long)this.GetHashCode(), "Failed to delete entry {0} with sourceGuid {1}", searchResultEntry.DistinguishedName, new Guid(guid));
						}
						throw;
					}
					this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, string.Format(CultureInfo.InvariantCulture, "DN:{0}, Guid:{1}", new object[]
					{
						searchResultEntry.DistinguishedName,
						new Guid(guid).ToString()
					}), "Skip deleting non-existing entry after sourceGuid has been confirmed. It must have been deleted in between.");
					return SyncResult.None;
				}
				if (ExTraceGlobals.SynchronizationJobTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "Deleted object {0} with sourceGuid {1}", searchResultEntry.DistinguishedName, new Guid(guid));
				}
				return SyncResult.Deleted;
			}
			if (ExTraceGlobals.SynchronizationJobTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SynchronizationJobTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Delete non-existing entry with sourceGUID {0}", new Guid(guid));
			}
			this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, new Guid(guid).ToString(), "Skip deleting non-existing entry");
			return SyncResult.None;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005340 File Offset: 0x00003540
		public ActiveDirectorySecurity ReadSecurityDescriptorDacl(string absolutePath)
		{
			byte[] array = (byte[])this.ReadSingleObjectAttribute(absolutePath, "nTSecurityDescriptor", new DirectoryControl[]
			{
				new SecurityDescriptorFlagControl(System.DirectoryServices.Protocols.SecurityMasks.Dacl)
			});
			if (array == null)
			{
				return null;
			}
			ActiveDirectorySecurity activeDirectorySecurity = new ActiveDirectorySecurity();
			activeDirectorySecurity.SetSecurityDescriptorBinaryForm(array, AccessControlSections.Access);
			return activeDirectorySecurity;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005384 File Offset: 0x00003584
		public void WriteSecurityDescriptorDacl(string absolutePath, ActiveDirectorySecurity acl)
		{
			DirectoryAttributeModification directoryAttributeModification = new DirectoryAttributeModification();
			directoryAttributeModification.Name = "nTSecurityDescriptor";
			directoryAttributeModification.Operation = DirectoryAttributeOperation.Replace;
			directoryAttributeModification.Add(acl.GetSecurityDescriptorBinaryForm());
			this.SendRequest(new ModifyRequest(absolutePath, new DirectoryAttributeModification[]
			{
				directoryAttributeModification
			})
			{
				Controls = 
				{
					new SecurityDescriptorFlagControl(System.DirectoryServices.Protocols.SecurityMasks.Dacl)
				}
			});
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000053E4 File Offset: 0x000035E4
		public void WriteSourceGuid(string absolutePath, byte[] guidValue)
		{
			DirectoryAttributeModification directoryAttributeModification = new DirectoryAttributeModification();
			directoryAttributeModification.Name = "msExchEdgeSyncSourceGuid";
			directoryAttributeModification.Operation = DirectoryAttributeOperation.Replace;
			directoryAttributeModification.Add(guidValue);
			ModifyRequest request = new ModifyRequest(absolutePath, new DirectoryAttributeModification[]
			{
				directoryAttributeModification
			});
			this.SendRequest(request);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000542C File Offset: 0x0000362C
		public override void Dispose()
		{
			if (this.connection != null)
			{
				if (this.connection is PooledLdapConnection)
				{
					((PooledLdapConnection)this.connection).ReturnToPool();
				}
				else
				{
					this.connection.Dispose();
				}
				this.connection = null;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005468 File Offset: 0x00003668
		public virtual string GetTargetPath(ExSearchResultEntry entry)
		{
			string text;
			if (this.type == SyncTreeType.Recipients)
			{
				DirectoryAttribute directoryAttribute = entry.Attributes["objectGUID"];
				Guid guid = new Guid((byte[])directoryAttribute.GetValues(typeof(byte[]))[0]);
				text = "cn=" + guid.ToString() + ",CN=Recipients,OU=MSExchangeGateway";
			}
			else
			{
				if (LdapTargetConnection.rootOrgContainerDN == null)
				{
					LdapTargetConnection.rootOrgContainerDN = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().DistinguishedName;
				}
				string text2 = entry.DistinguishedName;
				int startIndex = -1;
				int count = 0;
				if (entry.IsCollisionObject(out startIndex, out count))
				{
					text2 = text2.Remove(startIndex, count);
				}
				text = text2.Replace(LdapTargetConnection.rootOrgContainerDN, this.adamRootOrgContainerDN);
			}
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Translate source DN {0} to target DN {1}", entry.DistinguishedName, text);
			return text;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005538 File Offset: 0x00003738
		internal bool Exists(string absolutePath)
		{
			bool result;
			try
			{
				this.SendRequest(new SearchRequest(absolutePath, Schema.Query.QueryAll, System.DirectoryServices.Protocols.SearchScope.Base, null));
				result = true;
			}
			catch (ExDirectoryException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005574 File Offset: 0x00003774
		protected virtual DirectoryResponse SendRequest(DirectoryRequest request)
		{
			DirectoryResponse result;
			try
			{
				result = this.connection.SendRequest(request, LdapTargetConnection.requestTimeout);
			}
			catch (DirectoryOperationException ex)
			{
				ExTraceGlobals.ConnectionTracer.TraceError<string, ResultCode>((long)this.GetHashCode(), "DirectoryOperationException with response {0}, resultcode {1}", ex.Response.ErrorMessage, ex.Response.ResultCode);
				throw new ExDirectoryException(ex);
			}
			catch (DirectoryException ex2)
			{
				ExTraceGlobals.ConnectionTracer.TraceError<string>((long)this.GetHashCode(), "DirectoryException {0}", ex2.Message);
				throw new ExDirectoryException(ex2);
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000560C File Offset: 0x0000380C
		protected virtual ExSearchResultEntry GetTargetEntry(ExSearchResultEntry entry)
		{
			DirectoryAttribute directoryAttribute = entry.Attributes["objectGUID"];
			byte[] value = (byte[])directoryAttribute.GetValues(typeof(byte[]))[0];
			string str = ADValueConvertor.EscapeBinaryValue(value);
			SearchRequest request = new SearchRequest(this.GetTargetBaseSearchPath(), "(msExchEdgeSyncSourceGuid=" + str + ")", System.DirectoryServices.Protocols.SearchScope.Subtree, new string[0]);
			SearchResponse searchResponse = (SearchResponse)this.SendRequest(request);
			if (searchResponse.Entries.Count > 0)
			{
				return new ExSearchResultEntry(searchResponse.Entries[0]);
			}
			return null;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000569D File Offset: 0x0000389D
		protected virtual string GetTargetBaseSearchPath()
		{
			if (this.type == SyncTreeType.Recipients)
			{
				return "CN=Recipients,OU=MSExchangeGateway";
			}
			return this.adamConfigurationNamingContext;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000056B4 File Offset: 0x000038B4
		protected virtual void EnsureTargetContainer(ExSearchResultEntry entry)
		{
			if (this.type == SyncTreeType.Recipients)
			{
				SearchResponse searchResponse = (SearchResponse)this.SendRequest(new SearchRequest(DistinguishedName.Parent("CN=Recipients,OU=MSExchangeGateway"), Schema.Query.QueryRecipientsContainer, System.DirectoryServices.Protocols.SearchScope.OneLevel, null));
				if (searchResponse.Entries.Count == 0)
				{
					AddRequest request = new AddRequest("CN=Recipients,OU=MSExchangeGateway", "msExchContainer");
					this.SendRequest(request);
					return;
				}
			}
			else
			{
				string text = DistinguishedName.Parent(this.GetTargetPath(entry));
				if (!this.Exists(text))
				{
					AddRequest request2 = new AddRequest(text, "msExchContainer");
					this.SendRequest(request2);
				}
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000573C File Offset: 0x0000393C
		private object ReadSingleObjectAttribute(string absolutePath, string attributeName, params DirectoryControl[] controls)
		{
			SearchRequest searchRequest = new SearchRequest(absolutePath, Schema.Query.QueryAll, System.DirectoryServices.Protocols.SearchScope.Base, new string[]
			{
				attributeName
			});
			foreach (DirectoryControl control in controls)
			{
				searchRequest.Controls.Add(control);
			}
			SearchResponse searchResponse = (SearchResponse)this.SendRequest(searchRequest);
			if (searchResponse.Entries.Count > 0)
			{
				SearchResultAttributeCollection attributes = searchResponse.Entries[0].Attributes;
				if (attributes.Contains(attributeName))
				{
					return attributes[attributeName][0];
				}
			}
			return null;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000057D4 File Offset: 0x000039D4
		private ModifyRequest AddAttributeModifications(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> attributes, ModifyRequest modificationRequest)
		{
			DirectoryAttributeModification directoryAttributeModification = null;
			foreach (KeyValuePair<string, DirectoryAttribute> keyValuePair in attributes)
			{
				directoryAttributeModification = new DirectoryAttributeModification();
				directoryAttributeModification.Name = keyValuePair.Key;
				directoryAttributeModification.Operation = DirectoryAttributeOperation.Replace;
				foreach (object obj in keyValuePair.Value)
				{
					if (obj is byte[])
					{
						directoryAttributeModification.Add((byte[])obj);
					}
					else
					{
						directoryAttributeModification.Add((string)obj);
					}
					if (ExTraceGlobals.SynchronizationJobTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						this.TraceAttributeValue(keyValuePair.Value);
					}
				}
				modificationRequest.Modifications.Add(directoryAttributeModification);
			}
			directoryAttributeModification = new DirectoryAttributeModification();
			directoryAttributeModification.Name = "msExchEdgeSyncSourceGuid";
			directoryAttributeModification.Operation = DirectoryAttributeOperation.Replace;
			directoryAttributeModification.Add((byte[])entry.Attributes["objectGUID"][0]);
			modificationRequest.Modifications.Add(directoryAttributeModification);
			return modificationRequest;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005910 File Offset: 0x00003B10
		private bool VerifyServerCertificate(LdapConnection conn, X509Certificate cert)
		{
			SecurityIdentifier left = DirectTrust.MapCertToSecurityIdentifier(new X509Certificate2(cert));
			if (left != WellKnownSids.EdgeTransportServers)
			{
				EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_FailedDirectTrustMatch, null, new object[]
				{
					base.Host
				});
				this.logSession.LogFailedDirectTrust(base.Host, "Failed: Microsoft Exchange couldn't match the certificate thumbprint. The connection was stopped.", new X509Certificate2(cert));
				return false;
			}
			return true;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005978 File Offset: 0x00003B78
		private string ReadSingleStringAttribute(string absolutePath, string attributeName)
		{
			object obj = this.ReadSingleObjectAttribute(absolutePath, attributeName, new DirectoryControl[0]);
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000059A4 File Offset: 0x00003BA4
		private DirectoryAttribute ReadMultivalueAttribute(string absolutePath, string attributeName)
		{
			SearchRequest request = new SearchRequest(absolutePath, Schema.Query.QueryAll, System.DirectoryServices.Protocols.SearchScope.Base, new string[]
			{
				attributeName
			});
			SearchResponse searchResponse = (SearchResponse)this.SendRequest(request);
			if (searchResponse.Entries.Count > 0)
			{
				SearchResultAttributeCollection attributes = searchResponse.Entries[0].Attributes;
				if (attributes.Contains(attributeName))
				{
					return attributes[attributeName];
				}
			}
			return null;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005A0C File Offset: 0x00003C0C
		private void TraceAttributeValue(DirectoryAttribute attr)
		{
			ExTraceGlobals.SynchronizationJobTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Adding attribute {0} with value count {1}", attr.Name, attr.Count);
			foreach (object obj in attr)
			{
				if (obj is byte[])
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Adding binary value with length {0} and ASCII value {1}", ((byte[])obj).Length, Encoding.ASCII.GetString((byte[])obj));
				}
				else
				{
					ExTraceGlobals.SynchronizationJobTracer.TraceDebug((long)this.GetHashCode(), "Adding string value {0}", new object[]
					{
						obj
					});
				}
			}
		}

		// Token: 0x0400005C RID: 92
		private const int Exchange2012MajorVersion = 15;

		// Token: 0x0400005D RID: 93
		private const int Exchange2010MajorVersion = 14;

		// Token: 0x0400005E RID: 94
		private const int Exchange2007MajorVersion = 8;

		// Token: 0x0400005F RID: 95
		private const int Exchange2007SP2MinorVersion = 2;

		// Token: 0x04000060 RID: 96
		private static readonly TimeSpan requestTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000061 RID: 97
		private static string rootOrgContainerDN;

		// Token: 0x04000062 RID: 98
		private string adamRootOrgContainerDN;

		// Token: 0x04000063 RID: 99
		private SyncTreeType type;

		// Token: 0x04000064 RID: 100
		private string adamConfigurationNamingContext;

		// Token: 0x04000065 RID: 101
		private string serverDistinguishedName;

		// Token: 0x04000066 RID: 102
		private string serverName;

		// Token: 0x04000067 RID: 103
		private LdapConnection connection;

		// Token: 0x04000068 RID: 104
		private EdgeSyncLogSession logSession;
	}
}
