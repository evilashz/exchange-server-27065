using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000978 RID: 2424
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MembershipSynchronizer : Synchronizer
	{
		// Token: 0x060059BD RID: 22973 RVA: 0x001736E8 File Offset: 0x001718E8
		public MembershipSynchronizer(TeamMailboxSyncJob job, MailboxSession mailboxSession, OrganizationId orgId, ITeamMailboxSecurityRefresher teamMailboxSecurityRefresher, IResourceMonitor resourceMonitor, string siteUrl, ICredentials credential, bool isOAuthCredential, bool enableHttpDebugProxy, Stream syncCycleLogStream) : base(job, mailboxSession, resourceMonitor, siteUrl, credential, isOAuthCredential, enableHttpDebugProxy, syncCycleLogStream)
		{
			if (teamMailboxSecurityRefresher == null)
			{
				throw new ArgumentNullException("teamMailboxSecurityRefresher");
			}
			if (orgId == null)
			{
				throw new ArgumentNullException("orgId");
			}
			this.orgId = orgId;
			this.teamMailboxSecurityRefresher = teamMailboxSecurityRefresher;
			this.workLoadSize = 20;
			this.loggingComponent = ProtocolLog.Component.MembershipSync;
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x00173755 File Offset: 0x00171955
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.httpClient != null)
			{
				this.httpClient.Dispose();
				this.httpClient = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x0017377B File Offset: 0x0017197B
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MembershipSynchronizer>(this);
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x00173784 File Offset: 0x00171984
		public override IAsyncResult BeginExecute(AsyncCallback executeCallback, object state)
		{
			this.executionAsyncResult = new LazyAsyncResult(null, state, executeCallback);
			this.performanceCounter.Start(OperationType.EndToEnd);
			try
			{
				this.InitializeSyncMetadata();
				this.isFirstSync = (base.GetSyncMetadataValue("FirstAttemptedSyncTime") == null);
				base.UpdateSyncMetadataOnBeginSync();
			}
			catch (StorageTransientException ex)
			{
				ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "MembershipSynchronizer.BeginExecute:failed with StorageTransientException", ex);
				this.executionAsyncResult.InvokeCallback(ex);
				return this.executionAsyncResult;
			}
			catch (StoragePermanentException ex2)
			{
				ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "MembershipSynchronizer.BeginExecute:failed with StoragePermanentException", ex2);
				this.executionAsyncResult.InvokeCallback(ex2);
				return this.executionAsyncResult;
			}
			base.InitializeHttpClient("GET");
			this.newOwners = new List<ADObjectId>();
			this.newMembers = new List<ADObjectId>();
			this.doneDownloadingOwners = false;
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, CultureInfo.InvariantCulture.LCID, false, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.orgId), 237, "BeginExecute", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\LinkedFolder\\MembershipSynchronizer.cs");
			this.recipientSession.SessionSettings.IncludeSoftDeletedObjectLinks = true;
			ProtocolLog.LogInformation(this.loggingComponent, this.loggingContext, "MembershipSynchronizer.BeginExecute");
			this.BeginDownloadUsers(new AsyncCallback(this.OnGetUsersComplete), true);
			return this.executionAsyncResult;
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x001738F0 File Offset: 0x00171AF0
		public override void EndExecute(IAsyncResult asyncResult)
		{
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new InvalidOperationException("EndExecute: asyncResult or the AsynState cannot be null here.");
			}
			if (!lazyAsyncResult.IsCompleted)
			{
				lazyAsyncResult.InternalWaitForCompletion();
			}
			this.performanceCounter.Stop(OperationType.EndToEnd, 1);
			this.resourceMonitor.ResetBudget();
			base.LastError = (lazyAsyncResult.Result as Exception);
			base.SaveSyncMetadata();
			if (base.LastError == null)
			{
				ProtocolLog.LogInformation(this.loggingComponent, this.loggingContext, "MembershipSynchronizer.EndExecute: Succeeded");
			}
			else
			{
				ProtocolLog.LogError(this.loggingComponent, this.loggingContext, "MembershipSynchronizer.EndExecute: Failed", base.LastError);
			}
			string[] logLine = this.performanceCounter.GetLogLine();
			foreach (string str in logLine)
			{
				ProtocolLog.LogStatistics(this.loggingComponent, this.loggingContext, "MembershipSynchronizer.Statistics:" + str);
			}
			base.Dispose();
		}

		// Token: 0x060059C2 RID: 22978 RVA: 0x001739D3 File Offset: 0x00171BD3
		protected override void InitializeSyncMetadata()
		{
			if (this.syncMetadata == null)
			{
				this.syncMetadata = UserConfigurationHelper.GetMailboxConfiguration(this.mailboxSession, "MembershipSynchronizerConfigurations", UserConfigurationTypes.Dictionary, true);
			}
		}

		// Token: 0x060059C3 RID: 22979 RVA: 0x001739F5 File Offset: 0x00171BF5
		protected override LocalizedString GetSyncIssueEmailErrorString(string error, out LocalizedString body)
		{
			body = ClientStrings.FailedToSynchronizeMembershipFromSharePointText(this.siteUri.AbsoluteUri, error);
			return ClientStrings.FailedToSynchronizeMembershipFromSharePoint(this.siteUri.AbsoluteUri);
		}

		// Token: 0x060059C4 RID: 22980 RVA: 0x00173A20 File Offset: 0x00171C20
		protected virtual void BeginDownloadUsers(AsyncCallback callback, bool forOwners = true)
		{
			if (this.isOAuthCredential)
			{
				base.SetCommonOauthRequestHeaders();
			}
			string str = string.Format("/_vti_bin/client.svc/web/Associated{0}Group/Users", forOwners ? "Owner" : "Member");
			this.httpClient.BeginDownload(new Uri(this.siteUri.AbsoluteUri + str), this.httpSessionConfig, base.WrapCallbackWithUnhandledExceptionAndSendReportEx(callback), null);
		}

		// Token: 0x060059C5 RID: 22981 RVA: 0x00173A88 File Offset: 0x00171C88
		protected virtual void OnGetUsersComplete(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new InvalidOperationException("OnGetUsersComplete: asyncResult or the AsynState cannot be null here.");
			}
			if (base.HandleShutDown())
			{
				return;
			}
			Exception ex = null;
			XmlReader xmlReader = this.EndGetUsers((ICancelableAsyncResult)asyncResult, out ex);
			if (ex != null)
			{
				this.executionAsyncResult.InvokeCallback(ex);
				return;
			}
			List<ADObjectId> list = new List<ADObjectId>();
			bool flag = false;
			Guid mailboxGuid = this.Job.SyncInfoEntry.MailboxGuid;
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			try
			{
				using (xmlReader)
				{
					MembershipSynchronizer.ParseUsers(xmlReader, MembershipSynchronizer.isMultiTenant, out list2, out list3);
				}
			}
			catch (XmlException value)
			{
				this.executionAsyncResult.InvokeCallback(value);
				return;
			}
			foreach (string text in list2)
			{
				try
				{
					Exception ex2;
					ADRawEntry adrawEntry = TeamMailboxNameIdResolver.Resolve(this.recipientSession, text, out ex2);
					if (ex2 != null)
					{
						ProtocolLog.LogError(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.OnGetUsersComplete: Skip the update for user {0} because we failed to resolve it", text), ex2);
					}
					else if (adrawEntry != null)
					{
						if (TeamMailboxMembershipHelper.IsUserQualifiedType(adrawEntry))
						{
							if (this.newOwners.Count + list.Count >= 1800)
							{
								flag = true;
								break;
							}
							list.Add(adrawEntry.Id);
						}
						else
						{
							ProtocolLog.LogStatistics(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.OnGetUsersComplete: Drop unqualified owner {0} for team mailbox {1}", adrawEntry.Id, mailboxGuid));
						}
					}
				}
				catch (NonUniqueRecipientException exception)
				{
					ProtocolLog.LogError(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.OnGetUsersComplete: Skip the update for user {0} because of NonUniqueRecipientException", text), exception);
				}
			}
			if (!this.doneDownloadingOwners)
			{
				this.newOwners = list;
				this.doneDownloadingOwners = true;
				this.BeginDownloadUsers(new AsyncCallback(this.OnGetUsersComplete), false);
				return;
			}
			this.newMembers = list;
			if (flag)
			{
				ProtocolLog.LogError(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.OnGetUsersComplete: the total user count is more than TotalOwnersAndMembersLimit ({0}), updated {1} users only for team mailbox {2}.", 1800, 1800, mailboxGuid), new NotSupportedException());
			}
			Exception value2 = this.ProcessNewOwnersAndMembers(mailboxGuid, this.newOwners, this.newMembers);
			this.executionAsyncResult.InvokeCallback(value2);
		}

		// Token: 0x060059C6 RID: 22982 RVA: 0x00173CE8 File Offset: 0x00171EE8
		protected virtual Exception ProcessNewOwnersAndMembers(Guid mailboxGuid, List<ADObjectId> newOwners, List<ADObjectId> newMembers)
		{
			Exception ex = null;
			try
			{
				this.mailbox = (this.recipientSession.FindByExchangeGuid(mailboxGuid) as ADUser);
				if (this.mailbox == null)
				{
					throw new ObjectNotFoundException(new LocalizedString("Cannot find the team mailbox by mailbox guid " + mailboxGuid));
				}
				this.tm = TeamMailbox.FromDataObject(this.mailbox);
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (ADExternalException ex3)
			{
				ex = ex3;
			}
			catch (ADOperationException ex4)
			{
				ex = ex4;
			}
			catch (DataValidationException ex5)
			{
				ex = ex5;
			}
			catch (ObjectNotFoundException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				ProtocolLog.LogError(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.ProcessNewOwnersAndMembers: Failed to find team mailbox {0}", (this.mailbox == null) ? mailboxGuid.ToString() : this.mailbox.PrimarySmtpAddress.ToString()), ex);
				return ex;
			}
			this.membershipHelper = new TeamMailboxMembershipHelper(this.tm, this.recipientSession);
			TeamMailboxNotificationHelper teamMailboxNotificationHelper = null;
			if (this.isFirstSync)
			{
				teamMailboxNotificationHelper = new TeamMailboxNotificationHelper(this.tm, this.recipientSession);
				ADObjectId adobjectId = (this.tm.OwnersAndMembers.Count > 0) ? this.tm.OwnersAndMembers[0] : null;
				try
				{
					if (adobjectId != null)
					{
						List<ADObjectId> list = MembershipSynchronizer.ListForSingleId(adobjectId);
						ProtocolLog.LogStatistics(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.ProcessNewOwnersAndMembers: the following users have been added to team mailbox {0}: {1}", this.mailbox.PrimarySmtpAddress, MembershipSynchronizer.GetUsersString(list)));
						int count = this.tm.Owners.Count;
						teamMailboxNotificationHelper.SendNotification(list, TeamMailboxNotificationType.MemberInvitation);
					}
				}
				catch (StorageTransientException ex7)
				{
					ex = ex7;
				}
				catch (StoragePermanentException ex8)
				{
					ex = ex8;
				}
				if (ex != null)
				{
					ProtocolLog.LogError(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.ProcessNewOwnersAndMembers: failed sending notifications for team mailbox {0}", this.mailbox.PrimarySmtpAddress), ex);
				}
			}
			new List<ADObjectId>();
			IList<ADObjectId> newUserList = TeamMailbox.MergeUsers(newOwners, newMembers);
			IList<ADObjectId> userList;
			IList<ADObjectId> list2;
			bool flag = this.membershipHelper.UpdateTeamMailboxUserList(this.tm.Owners, newOwners, out userList, out list2);
			IList<ADObjectId> list3;
			IList<ADObjectId> list4;
			bool flag2 = this.membershipHelper.UpdateTeamMailboxUserList(this.tm.OwnersAndMembers, newUserList, out list3, out list4);
			if (flag || flag2)
			{
				TeamMailbox.DiffUsers(list3, userList);
				try
				{
					this.membershipHelper.SetTeamMailboxUserPermissions(list3, list4, null, true);
				}
				catch (OverflowException ex9)
				{
					ex = ex9;
				}
				catch (COMException ex10)
				{
					ex = ex10;
				}
				catch (UnauthorizedAccessException ex11)
				{
					ex = ex11;
				}
				catch (TransientException ex12)
				{
					ex = ex12;
				}
				catch (DataSourceOperationException ex13)
				{
					ex = ex13;
				}
				if (ex != null)
				{
					ProtocolLog.LogError(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.ProcessNewOwnersAndMembers: failed setting AD permissions for team mailbox {0}", this.mailbox.PrimarySmtpAddress), ex);
					return ex;
				}
				ProtocolLog.LogStatistics(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.ProcessNewOwnersAndMembers: the following users have been added to team mailbox {0}: {1}", this.mailbox.PrimarySmtpAddress, MembershipSynchronizer.GetUsersString(list3)));
				ProtocolLog.LogStatistics(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.ProcessNewOwnersAndMembers: the following users have been removed from team mailbox {0}: {1}", this.mailbox.PrimarySmtpAddress, MembershipSynchronizer.GetUsersString(list4)));
				try
				{
					this.teamMailboxSecurityRefresher.Refresh(this.mailbox, this.recipientSession);
				}
				catch (DatabaseNotFoundException ex14)
				{
					ex = ex14;
				}
				catch (ObjectNotFoundException ex15)
				{
					ex = ex15;
				}
				if (ex != null)
				{
					ProtocolLog.LogError(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.ProcessNewOwnersAndMembers: failed setting team mailbox {0} store permissions for userToAdd {1} and usersToRemove {2}", this.mailbox.PrimarySmtpAddress, MembershipSynchronizer.GetUsersString(list3), MembershipSynchronizer.GetUsersString(list4)), ex);
				}
				if (teamMailboxNotificationHelper == null)
				{
					teamMailboxNotificationHelper = new TeamMailboxNotificationHelper(this.tm, this.recipientSession);
				}
				try
				{
					teamMailboxNotificationHelper.SendNotification(list3, TeamMailboxNotificationType.MemberInvitation);
				}
				catch (StorageTransientException ex16)
				{
					ex = ex16;
				}
				catch (StoragePermanentException ex17)
				{
					ex = ex17;
				}
				if (ex != null)
				{
					ProtocolLog.LogError(this.loggingComponent, this.loggingContext, string.Format("MembershipSynchronizer.ProcessNewOwnersAndMembers: failed sending member invitation for team mailbox {0}", this.mailbox.PrimarySmtpAddress), ex);
				}
				this.usersToUpdateShowInMyClient = new MembershipSynchronizer.UsersToUpdateShowInMyClient(list3, list4, this);
				while (this.usersToUpdateShowInMyClient.UpdateNextBatch())
				{
					TimeSpan timeSpan;
					this.IsThrottleDelayNeeded(out timeSpan);
				}
			}
			return ex;
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x0017418C File Offset: 0x0017238C
		internal static void ParseUsers(XmlReader reader, bool isMultiTenant, out List<string> qualifiedUsers, out List<string> droppedUsers)
		{
			qualifiedUsers = new List<string>();
			droppedUsers = new List<string>();
			while (reader.ReadToFollowing("entry"))
			{
				if (reader.ReadToFollowing("content") && reader.ReadToFollowing("m:properties") && reader.ReadToFollowing("d:UserId"))
				{
					string text = null;
					string text2 = null;
					bool flag = true;
					reader.Read();
					while (flag)
					{
						if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("d:NameId", StringComparison.OrdinalIgnoreCase))
						{
							text = reader.ReadElementContentAsString();
						}
						else if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("d:NameIdIssuer", StringComparison.OrdinalIgnoreCase))
						{
							text2 = reader.ReadElementContentAsString();
						}
						else
						{
							flag = ((reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("d:UserId", StringComparison.OrdinalIgnoreCase)) && reader.Read());
						}
					}
					if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
					{
						string text3 = text2.ToLowerInvariant();
						SecurityIdentifier securityIdentifier;
						if (isMultiTenant)
						{
							NetID netID;
							if ((text3.Equals("urn:federation:microsoftonline") || text3.Equals("urn:office:idp:orgid")) && NetID.TryParse(text, out netID))
							{
								qualifiedUsers.Add(text);
							}
							else
							{
								droppedUsers.Add(text);
							}
						}
						else if (text3.Equals("urn:office:idp:activedirectory") && MembershipSynchronizer.TryParseSid(text, out securityIdentifier))
						{
							qualifiedUsers.Add(text);
						}
						else
						{
							droppedUsers.Add(text);
						}
					}
				}
			}
		}

		// Token: 0x060059C8 RID: 22984 RVA: 0x001742EC File Offset: 0x001724EC
		private static List<ADObjectId> ListForSingleId(ADObjectId id)
		{
			return new List<ADObjectId>
			{
				id
			};
		}

		// Token: 0x060059C9 RID: 22985 RVA: 0x00174308 File Offset: 0x00172508
		private static bool TryParseSid(string sidStr, out SecurityIdentifier sid)
		{
			sid = null;
			try
			{
				sid = new SecurityIdentifier(sidStr);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060059CA RID: 22986 RVA: 0x0017433C File Offset: 0x0017253C
		private static string GetUsersString(IList<ADObjectId> users)
		{
			if (users != null && users.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (ADObjectId arg in users)
				{
					stringBuilder.Append(arg + "; ");
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x060059CB RID: 22987 RVA: 0x001743B0 File Offset: 0x001725B0
		private void PutMessageInSyncIssueFolder(string error)
		{
			StoreObjectId destFolderId = Utils.EnsureSyncIssueFolder(this.mailboxSession);
			using (MessageItem messageItem = MessageItem.Create(this.mailboxSession, destFolderId))
			{
				LocalizedString empty = LocalizedString.Empty;
				messageItem.From = new Participant(this.Job.SyncInfoEntry.MailboxPrincipal);
				messageItem.Subject = this.GetSyncIssueEmailErrorString(error, out empty);
				using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextHtml))
				{
					messageItem.Body.Reset();
					using (HtmlWriter htmlWriter = new HtmlWriter(textWriter))
					{
						htmlWriter.WriteStartTag(HtmlTagId.Html);
						htmlWriter.WriteStartTag(HtmlTagId.Body);
						htmlWriter.WriteStartTag(HtmlTagId.P);
						htmlWriter.WriteText(empty);
						htmlWriter.WriteEndTag(HtmlTagId.P);
						htmlWriter.WriteEndTag(HtmlTagId.Body);
						htmlWriter.WriteEndTag(HtmlTagId.Html);
					}
				}
				messageItem.IsDraft = false;
				messageItem.MarkAsUnread(true);
				messageItem.Save(SaveMode.NoConflictResolutionForceSave);
			}
		}

		// Token: 0x060059CC RID: 22988 RVA: 0x001744D4 File Offset: 0x001726D4
		private XmlTextReader EndGetUsers(ICancelableAsyncResult asyncResult, out Exception exception)
		{
			try
			{
				exception = null;
				DownloadResult downloadResult = this.httpClient.EndDownload(asyncResult);
				if (!downloadResult.IsSucceeded)
				{
					WebException ex = downloadResult.Exception as WebException;
					if (ex != null)
					{
						exception = new SharePointException((this.httpClient.LastKnownRequestedUri != null) ? this.httpClient.LastKnownRequestedUri.AbsoluteUri : string.Empty, ex, true);
					}
					else
					{
						exception = downloadResult.Exception;
					}
				}
				else
				{
					if (downloadResult.ResponseStream == null)
					{
						exception = new SharePointException((this.httpClient.LastKnownRequestedUri != null) ? this.httpClient.LastKnownRequestedUri.AbsoluteUri : string.Empty, ServerStrings.ErrorTeamMailboxGetUsersNullResponse);
						return null;
					}
					downloadResult.ResponseStream.Position = 0L;
					return SafeXmlFactory.CreateSafeXmlTextReader(downloadResult.ResponseStream);
				}
			}
			finally
			{
				if (this.httpSessionConfig.RequestStream != null)
				{
					this.httpSessionConfig.RequestStream.Flush();
					this.httpSessionConfig.RequestStream.Dispose();
					this.httpSessionConfig.RequestStream = null;
				}
			}
			return null;
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x001745FC File Offset: 0x001727FC
		private bool IsThrottleDelayNeeded(out TimeSpan delay)
		{
			delay = TimeSpan.Zero;
			if (this.performanceCounter.CurrentIoOperations >= this.workLoadSize)
			{
				this.performanceCounter.CurrentIoOperations = 0;
				DelayInfo delay2 = this.resourceMonitor.GetDelay();
				if (delay2.Delay > TimeSpan.Zero)
				{
					delay = delay2.Delay;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003144 RID: 12612
		private const int DefaultWorkLoadSize = 20;

		// Token: 0x04003145 RID: 12613
		private static readonly bool isMultiTenant = VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;

		// Token: 0x04003146 RID: 12614
		private readonly PerformanceCounter performanceCounter = new PerformanceCounter();

		// Token: 0x04003147 RID: 12615
		protected int workLoadSize;

		// Token: 0x04003148 RID: 12616
		protected List<ADObjectId> newOwners;

		// Token: 0x04003149 RID: 12617
		protected List<ADObjectId> newMembers;

		// Token: 0x0400314A RID: 12618
		protected bool doneDownloadingOwners;

		// Token: 0x0400314B RID: 12619
		private MembershipSynchronizer.UsersToUpdateShowInMyClient usersToUpdateShowInMyClient;

		// Token: 0x0400314C RID: 12620
		private ITeamMailboxSecurityRefresher teamMailboxSecurityRefresher;

		// Token: 0x0400314D RID: 12621
		private OrganizationId orgId;

		// Token: 0x0400314E RID: 12622
		private IRecipientSession recipientSession;

		// Token: 0x0400314F RID: 12623
		private ADUser mailbox;

		// Token: 0x04003150 RID: 12624
		private TeamMailbox tm;

		// Token: 0x04003151 RID: 12625
		private bool isFirstSync;

		// Token: 0x04003152 RID: 12626
		private TeamMailboxMembershipHelper membershipHelper;

		// Token: 0x02000979 RID: 2425
		private class UsersToUpdateShowInMyClient
		{
			// Token: 0x060059CF RID: 22991 RVA: 0x0017468C File Offset: 0x0017288C
			public UsersToUpdateShowInMyClient(IList<ADObjectId> usersToAdd, IList<ADObjectId> usersToRemove, MembershipSynchronizer membershipSynchronizer)
			{
				this.membershipSynchronizer = membershipSynchronizer;
				foreach (ADObjectId id in usersToAdd)
				{
					this.Add(id, true);
				}
				foreach (ADObjectId id2 in usersToRemove)
				{
					this.Add(id2, false);
				}
				this.usersEnumerator = this.users.GetEnumerator();
			}

			// Token: 0x060059D0 RID: 22992 RVA: 0x0017473C File Offset: 0x0017293C
			public void Add(ADObjectId id, bool isAdd)
			{
				MembershipSynchronizer.UsersToUpdateShowInMyClient.UserToUpdate item;
				item.Id = id;
				item.IsAdd = isAdd;
				this.users.Add(item);
			}

			// Token: 0x060059D1 RID: 22993 RVA: 0x00174768 File Offset: 0x00172968
			public bool UpdateNextBatch()
			{
				for (int i = 0; i < this.membershipSynchronizer.workLoadSize; i++)
				{
					if (!this.usersEnumerator.MoveNext())
					{
						return false;
					}
					MembershipSynchronizer.UsersToUpdateShowInMyClient.UserToUpdate userToUpdate = this.usersEnumerator.Current;
					bool flag;
					Exception ex;
					this.membershipSynchronizer.membershipHelper.SetShowInMyClient(userToUpdate.Id, userToUpdate.IsAdd, out flag, out ex);
					if (ex != null)
					{
						ProtocolLog.LogError(this.membershipSynchronizer.loggingComponent, this.membershipSynchronizer.loggingContext, string.Format("MembershipSynchronizer.SetShowInMyClient: failed to SetShowInMyClient for user {0}", userToUpdate.Id), ex);
					}
				}
				return true;
			}

			// Token: 0x04003153 RID: 12627
			private readonly MembershipSynchronizer membershipSynchronizer;

			// Token: 0x04003154 RID: 12628
			private List<MembershipSynchronizer.UsersToUpdateShowInMyClient.UserToUpdate> users = new List<MembershipSynchronizer.UsersToUpdateShowInMyClient.UserToUpdate>();

			// Token: 0x04003155 RID: 12629
			private IEnumerator<MembershipSynchronizer.UsersToUpdateShowInMyClient.UserToUpdate> usersEnumerator;

			// Token: 0x0200097A RID: 2426
			private struct UserToUpdate
			{
				// Token: 0x04003156 RID: 12630
				public ADObjectId Id;

				// Token: 0x04003157 RID: 12631
				public bool IsAdd;
			}
		}
	}
}
