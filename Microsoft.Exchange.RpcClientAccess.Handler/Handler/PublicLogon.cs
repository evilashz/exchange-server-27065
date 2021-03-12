using System;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200008B RID: 139
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PublicLogon : Logon
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x00027C10 File Offset: 0x00025E10
		internal PublicLogon(PublicFolderSession session, ClientSecurityContext delegatedClientSecurityContext, ConnectionHandler connectionHandler, NotificationHandler notificationHandler, OpenFlags openFlags, byte logonId, LocaleInfo? localeInfo) : base(session, delegatedClientSecurityContext, connectionHandler, notificationHandler, openFlags, logonId, ProtocolLogLogonType.Public)
		{
			this.sessionReference = new ReferenceCount<PublicFolderSession>(session);
			this.localeInfo = localeInfo;
			if (!this.PublicFolderSession.IsPrimaryHierarchySession)
			{
				if (delegatedClientSecurityContext != null)
				{
					throw new InvalidOperationException("Delegated client security context only valid when connecting to primary hierarchy mailbox");
				}
				this.PrimaryHierarchyProvider = new RPCPrimaryHierarchyHandler(this.PublicFolderSession, this.PublicFolderProxyUserDn, base.Connection.AccessingClientSecurityContext);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00027C80 File Offset: 0x00025E80
		private string PublicFolderProxyUserDn
		{
			get
			{
				if (base.Connection.OrganizationId != null && base.Connection.OrganizationId.OrganizationalUnit != null)
				{
					return string.Format("{0}{1};{2}", "DomDn:", base.Connection.OrganizationId.OrganizationalUnit.Name, base.Connection.ActAsLegacyDN);
				}
				return base.Connection.ActAsLegacyDN;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00027CED File Offset: 0x00025EED
		public override StoreSession Session
		{
			get
			{
				return this.PublicFolderSession;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00027CF5 File Offset: 0x00025EF5
		internal PublicFolderSession PublicFolderSession
		{
			get
			{
				return this.sessionReference.ReferencedObject;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00027D02 File Offset: 0x00025F02
		internal StoreId FreeBusyFolderId
		{
			get
			{
				return StoreId.Empty;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00027D09 File Offset: 0x00025F09
		internal bool IsPrimaryHierarchyLogon
		{
			get
			{
				return this.PublicFolderSession.IsPrimaryHierarchySession;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00027D16 File Offset: 0x00025F16
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00027D1E File Offset: 0x00025F1E
		internal IPrimaryHierarchyHandler PrimaryHierarchyProvider { get; private set; }

		// Token: 0x060005A9 RID: 1449 RVA: 0x00027D27 File Offset: 0x00025F27
		internal static PublicFolderSession CreatePublicFolderSession(IConnection connection, OpenFlags openFlags, LogonExtendedRequestFlags extendedFlags, ExchangePrincipal publicFolderMailboxPrincipal, ClientSecurityContext delegatedClientSecurityContext, string applicationId)
		{
			return Logon.CreateStoreSession<PublicFolderSession>(connection, openFlags, extendedFlags, publicFolderMailboxPrincipal, delegatedClientSecurityContext, applicationId, new Func<ExchangePrincipal, ClientSecurityContext, OpenFlags, IConnection, string, PublicFolderSession>(PublicLogon.OpenPublicFolderSession));
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00027D44 File Offset: 0x00025F44
		internal static ExchangePrincipal GetPublicFolderMailboxPrincipal(MiniRecipient connectAsMiniRecipient, string rpcServerTarget, MailboxId? mailboxId, LogonFlags logonFlags, OpenFlags openFlags)
		{
			Guid hierarchyMailboxGuidAlias = PublicFolderSession.HierarchyMailboxGuidAlias;
			LegacyDN legacyDN;
			if (mailboxId != null && LegacyDN.TryParse(mailboxId.Value.LegacyDn, out legacyDN))
			{
				string text;
				string address;
				legacyDN.GetParentLegacyDN(out text, out address);
				GuidHelper.TryParseGuid(new SmtpAddress(address).Local, out hierarchyMailboxGuidAlias);
			}
			if (hierarchyMailboxGuidAlias == Guid.Empty)
			{
				GuidHelper.TryParseGuid(new SmtpAddress(rpcServerTarget).Local, out hierarchyMailboxGuidAlias);
			}
			if (hierarchyMailboxGuidAlias == Guid.Empty && PublicLogon.IsTestTopology(Configuration.ServiceConfiguration.ThisServerFqdn.ToString()) && (logonFlags.HasFlag(LogonFlags.Ghosted) || openFlags.HasFlag(OpenFlags.IgnoreHomeMdb)) && !PublicFolderSession.TryGetContentMailboxGuid(connectAsMiniRecipient.OrganizationId, out hierarchyMailboxGuidAlias))
			{
				throw new RopExecutionException(string.Format("Test topology: Ghosted public logon is not supported since there is only one public folder mailbox in the organization.", new object[0]), (ErrorCode)2147746065U);
			}
			ExchangePrincipal result = null;
			if (!PublicFolderSession.TryGetPublicFolderMailboxPrincipal(connectAsMiniRecipient.OrganizationId, hierarchyMailboxGuidAlias, true, out result))
			{
				if (logonFlags.HasFlag(LogonFlags.Ghosted) || openFlags.HasFlag(OpenFlags.IgnoreHomeMdb))
				{
					throw new RopExecutionException(string.Format("Public folder mailbox {0} not found", hierarchyMailboxGuidAlias), (ErrorCode)2147746065U);
				}
				if (!PublicFolderSession.TryGetHierarchyMailboxGuidForUser(connectAsMiniRecipient.OrganizationId, connectAsMiniRecipient.ExchangeGuid, connectAsMiniRecipient.DefaultPublicFolderMailbox, out hierarchyMailboxGuidAlias) || !PublicFolderSession.TryGetPublicFolderMailboxPrincipal(connectAsMiniRecipient.OrganizationId, hierarchyMailboxGuidAlias, true, out result))
				{
					return null;
				}
			}
			return result;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00027EC4 File Offset: 0x000260C4
		internal static string GetRedirectionServerLegacyDN(IConnection connection, ExchangePrincipal publicFolderMailboxPrincipal, DatabaseLocationInfo publicFolderDatabaseLocationInfo, OpenFlags openFlags)
		{
			string reason;
			string text;
			if (publicFolderDatabaseLocationInfo != null)
			{
				reason = "user's home public folder server is E12/E14";
				text = publicFolderDatabaseLocationInfo.RpcClientAccessServerLegacyDN;
			}
			else if ((openFlags & OpenFlags.AlternateServer) == OpenFlags.AlternateServer)
			{
				Feature.Stubbed(168738, "OpenFlags.AlternateServer is not supported. Returning the current server/array to tell the client there're no alternatives.");
				reason = "alternate server requested";
				text = Logon.CreatePersonalizedServerRedirectLegacyDN(publicFolderMailboxPrincipal).ToString();
			}
			else
			{
				Guid guid;
				GuidHelper.TryParseGuid(new SmtpAddress(connection.RpcServerTarget).Local, out guid);
				if (guid == publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid)
				{
					return null;
				}
				if (guid == Guid.Empty && (connection.ConnectionFlags & ConnectionFlags.UseDelegatedAuthPrivilege) == ConnectionFlags.UseDelegatedAuthPrivilege)
				{
					return null;
				}
				if (guid == Guid.Empty && PublicLogon.IsTestTopology(publicFolderMailboxPrincipal.MailboxInfo.Location.ServerFqdn))
				{
					return null;
				}
				reason = string.Format("guid {0} from rpcServerTarget {1} is different from public folder mailbox guid {2} to which the client intends to logon", guid, connection.RpcServerTarget, publicFolderMailboxPrincipal.MailboxInfo.MailboxGuid);
				text = Logon.CreatePersonalizedServerRedirectLegacyDN(publicFolderMailboxPrincipal).ToString();
			}
			ProtocolLog.LogLogonRedirect(reason, text);
			return text;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00027FD0 File Offset: 0x000261D0
		internal static void ValidatePublicLogonSettings(LogonFlags logonFlags, OpenFlags openFlags, LogonExtendedRequestFlags extendedFlags, MailboxId? mailboxId, AuthenticationContext authenticationContext)
		{
			if ((byte)(logonFlags & LogonFlags.Private) == 1 || (byte)(logonFlags & (LogonFlags.Private | LogonFlags.Mapi0 | LogonFlags.MbxGuids | LogonFlags.NoRules)) != 0)
			{
				throw new RopExecutionException(string.Format("Unsupported public LogonFlags. LogonFlags = {0}.", logonFlags), (ErrorCode)2147942487U);
			}
			if ((openFlags & OpenFlags.Public) != OpenFlags.Public)
			{
				throw new RopExecutionException(string.Format("OpenFlags.Public is not set. OpenFlags = {0}.", openFlags), (ErrorCode)2147942487U);
			}
			if ((extendedFlags & ~(LogonExtendedRequestFlags.UseLocaleInfo | LogonExtendedRequestFlags.SetAuthContext | LogonExtendedRequestFlags.ApplicationId | LogonExtendedRequestFlags.ReturnLocaleInfo)) != LogonExtendedRequestFlags.None)
			{
				throw new RopExecutionException(string.Format("Unsupported public LogonExtendedRequestFlags. LogonExtendedRequestFlags = {0}.", extendedFlags), (ErrorCode)2147942487U);
			}
			if ((byte)(logonFlags & LogonFlags.Extended) == 0 && (extendedFlags & (LogonExtendedRequestFlags.UseLocaleInfo | LogonExtendedRequestFlags.SetAuthContext | LogonExtendedRequestFlags.ApplicationId | LogonExtendedRequestFlags.ReturnLocaleInfo)) != LogonExtendedRequestFlags.None)
			{
				throw new RopExecutionException(string.Format("LogonExtendedRequestFlags can be set only when LogonFlags.Extended is set. LogonFlags = {0}, LogonExtendedRequestFlags = {1}.", logonFlags, extendedFlags), (ErrorCode)2147942487U);
			}
			if ((byte)(logonFlags & LogonFlags.Extended) == 64 && authenticationContext == null)
			{
				throw new RopExecutionException(string.Format("LogonFlags.Extended can be set only when authenticationContext is present and the user has permission to pass authenticationContext. LogonFlags = {0},", logonFlags), (ErrorCode)2147942487U);
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000280A4 File Offset: 0x000262A4
		internal static ReplicaServerInfo? GetReplicaServerInfo(PublicFolderSession publicFolderSession, StoreId folderId, bool onlyIfGhosted)
		{
			if (folderId == StoreId.Empty)
			{
				throw new RopExecutionException(string.Format("FolderId {0} invalid.", folderId), (ErrorCode)2147942487U);
			}
			ReplicaServerInfo? replicaServerInfo;
			using (CoreFolder coreFolder = CoreFolder.Bind(publicFolderSession, publicFolderSession.IdConverter.CreateFolderId(folderId)))
			{
				replicaServerInfo = PublicLogon.GetReplicaServerInfo(coreFolder, onlyIfGhosted);
			}
			return replicaServerInfo;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0002811C File Offset: 0x0002631C
		internal static ReplicaServerInfo? GetReplicaServerInfo(CoreFolder coreFolder, bool onlyIfGhosted)
		{
			if (onlyIfGhosted && coreFolder.IsContentAvailable())
			{
				return null;
			}
			PublicFolderSession publicFolderSession = coreFolder.Session as PublicFolderSession;
			Guid mailboxGuid;
			if (coreFolder.IsContentAvailable())
			{
				mailboxGuid = publicFolderSession.MailboxGuid;
			}
			else
			{
				PublicFolderContentMailboxInfo contentMailboxInfo = coreFolder.GetContentMailboxInfo();
				if (!contentMailboxInfo.IsValid)
				{
					return null;
				}
				mailboxGuid = contentMailboxInfo.MailboxGuid;
			}
			ExchangePrincipal exchangePrincipal;
			if (!PublicFolderSession.TryGetPublicFolderMailboxPrincipal(publicFolderSession.OrganizationId, mailboxGuid, false, out exchangePrincipal))
			{
				return null;
			}
			return new ReplicaServerInfo?(new ReplicaServerInfo(Database.GetDatabaseLegacyDNFromRcaLegacyDN(Logon.CreatePersonalizedServerRedirectLegacyDN(exchangePrincipal), true).ToString()));
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000281B4 File Offset: 0x000263B4
		internal static int GetBestFitForEFormsRegistryFolder(int logonLcid, int eFormsLcid)
		{
			int num = 1023;
			int result = 1;
			int lcid = CultureInfo.InstalledUICulture.LCID;
			if (logonLcid == eFormsLcid)
			{
				result = 5;
			}
			else if ((logonLcid & num) == (eFormsLcid & num))
			{
				result = 4;
			}
			else if (eFormsLcid == lcid)
			{
				result = 3;
			}
			else if ((lcid & num) == (eFormsLcid & num))
			{
				result = 2;
			}
			return result;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000281FC File Offset: 0x000263FC
		internal override StoreId[] GetDefaultFolderIds()
		{
			StoreId[] array = new StoreId[13];
			array[0] = this.StoreObjectIdToFolderId(this.PublicFolderSession.GetPublicFolderRootId());
			array[1] = this.StoreObjectIdToFolderId(this.PublicFolderSession.GetIpmSubtreeFolderId());
			array[2] = this.StoreObjectIdToFolderId(this.PublicFolderSession.GetNonIpmSubtreeFolderId());
			array[3] = this.StoreObjectIdToFolderId(this.PublicFolderSession.GetEFormsRegistryFolderId());
			array[4] = StoreId.Empty;
			array[5] = StoreId.Empty;
			array[6] = this.GetUserLocaleEFormsRegistryFolder();
			array[7] = StoreId.Empty;
			array[8] = StoreId.Empty;
			return array;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000282DC File Offset: 0x000264DC
		protected override void FixBodyPropertiesIfNeeded(PropertyValue[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i].PropertyTag.PropertyId == PropertyTag.MailboxOwnerName.PropertyId)
				{
					values[i] = new PropertyValue(new PropertyTag(values[i].PropertyTag.PropertyId, PropertyType.Error), (ErrorCode)2147942405U);
				}
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00028350 File Offset: 0x00026550
		protected override IResourceTracker CreateResourceTracker()
		{
			return new ResourceTracker(10485760);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002836E File Offset: 0x0002656E
		protected override StreamSource GetStreamSource()
		{
			return new StreamSource<PublicFolderSession>(this.sessionReference, (PublicFolderSession publicFolderSession) => publicFolderSession.Mailbox.CoreObject.PropertyBag);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00028398 File Offset: 0x00026598
		private static PublicFolderSession OpenPublicFolderSession(ExchangePrincipal publicFolderMailboxPrincipal, ClientSecurityContext delegatedClientSecurityContext, OpenFlags openFlags, IConnection connection, string applicationId)
		{
			if ((openFlags & OpenFlags.UseAdminPrivilege) != OpenFlags.UseAdminPrivilege)
			{
				return PublicFolderSession.Open(connection.ActAsLegacyDN, connection.MiniRecipient, publicFolderMailboxPrincipal, delegatedClientSecurityContext ?? connection.AccessingClientSecurityContext, connection.CultureInfo, applicationId);
			}
			if (connection.IsFederatedSystemAttendant)
			{
				throw new RopExecutionException("Unable to open public folders using federated system attendant.", (ErrorCode)2147746050U);
			}
			return PublicFolderSession.OpenAsAdmin(connection.ActAsLegacyDN, connection.MiniRecipient, publicFolderMailboxPrincipal, delegatedClientSecurityContext ?? connection.AccessingClientSecurityContext, connection.CultureInfo, applicationId);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0002840E File Offset: 0x0002660E
		private static bool IsTestTopology(string domainName)
		{
			return !string.IsNullOrEmpty(domainName) && (domainName.EndsWith(".extest.microsoft.com", StringComparison.OrdinalIgnoreCase) || domainName.EndsWith(".extest.net", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00028436 File Offset: 0x00026636
		private StoreId StoreObjectIdToFolderId(StoreObjectId id)
		{
			return new StoreId(this.PublicFolderSession.IdConverter.GetFidFromId(id));
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0002858C File Offset: 0x0002678C
		private StoreId GetUserLocaleEFormsRegistryFolder()
		{
			int logonLcid = (this.localeInfo != null) ? this.localeInfo.Value.StringLocaleId : -1;
			StoreId result = StoreId.Empty;
			using (Folder folder = Folder.Bind(this.PublicFolderSession, this.PublicFolderSession.GetEFormsRegistryFolderId()))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
				{
					FolderSchema.Id,
					FolderSchema.EformsLocaleId
				}))
				{
					int num = 0;
					var enumerable = from item in queryResult.GetRows(queryResult.EstimatedRowCount)
					select new
					{
						Id = (item[0] as VersionedId),
						Lcid = ((item[1] is PropertyError) ? -1 : ((int)item[1]))
					};
					foreach (var <>f__AnonymousType in enumerable)
					{
						int bestFitForEFormsRegistryFolder = PublicLogon.GetBestFitForEFormsRegistryFolder(logonLcid, <>f__AnonymousType.Lcid);
						if (bestFitForEFormsRegistryFolder > num)
						{
							num = bestFitForEFormsRegistryFolder;
							result = this.StoreObjectIdToFolderId(<>f__AnonymousType.Id.ObjectId);
						}
						if (num == 5)
						{
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000286D4 File Offset: 0x000268D4
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PublicLogon>(this);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000286DC File Offset: 0x000268DC
		protected override void InternalDispose()
		{
			this.sessionReference.Release();
			base.InternalDispose();
		}

		// Token: 0x04000255 RID: 597
		private readonly ReferenceCount<PublicFolderSession> sessionReference;

		// Token: 0x04000256 RID: 598
		private LocaleInfo? localeInfo;

		// Token: 0x0200008C RID: 140
		internal enum DefaultFolderIndex
		{
			// Token: 0x0400025B RID: 603
			Root,
			// Token: 0x0400025C RID: 604
			IpmSubtree,
			// Token: 0x0400025D RID: 605
			NonIpmSubtree,
			// Token: 0x0400025E RID: 606
			EFormsRegistry,
			// Token: 0x0400025F RID: 607
			FreeBusy,
			// Token: 0x04000260 RID: 608
			OfflineAddressBook,
			// Token: 0x04000261 RID: 609
			UserLocaleEFormsRegistry,
			// Token: 0x04000262 RID: 610
			LocalSiteFreeBusy,
			// Token: 0x04000263 RID: 611
			LocalSiteOfflineAddressBook
		}
	}
}
