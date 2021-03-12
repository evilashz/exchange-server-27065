using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200099B RID: 2459
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TeamMailboxMembershipHelper
	{
		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x06005AC7 RID: 23239 RVA: 0x0017B89A File Offset: 0x00179A9A
		// (set) Token: 0x06005AC8 RID: 23240 RVA: 0x0017B8A2 File Offset: 0x00179AA2
		public TeamMailbox TeamMailbox { get; private set; }

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x06005AC9 RID: 23241 RVA: 0x0017B8AB File Offset: 0x00179AAB
		// (set) Token: 0x06005ACA RID: 23242 RVA: 0x0017B8B3 File Offset: 0x00179AB3
		public IRecipientSession DataSession { get; private set; }

		// Token: 0x06005ACB RID: 23243 RVA: 0x0017B8BC File Offset: 0x00179ABC
		public TeamMailboxMembershipHelper(TeamMailbox tm, IRecipientSession dataSession)
		{
			if (tm == null)
			{
				throw new ArgumentNullException("tm");
			}
			if (dataSession == null)
			{
				throw new ArgumentNullException("dataSession");
			}
			this.TeamMailbox = tm;
			this.DataSession = dataSession;
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x0017B8EE File Offset: 0x00179AEE
		public static bool IsUserQualifiedType(ADUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return TeamMailboxMembershipHelper.IsUserQualifiedType(user);
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x0017B904 File Offset: 0x00179B04
		public static bool IsUserQualifiedType(ADRawEntry user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return user[ADRecipientSchema.RecipientTypeDetails] != null && (user[ADRecipientSchema.RecipientTypeDetails].Equals(RecipientTypeDetails.UserMailbox) || user[ADRecipientSchema.RecipientTypeDetails].Equals(RecipientTypeDetails.LinkedMailbox) || user[ADRecipientSchema.RecipientTypeDetails].Equals((RecipientTypeDetails)((ulong)int.MinValue)) || user[ADRecipientSchema.RecipientTypeDetails].Equals(RecipientTypeDetails.MailUser));
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x0017B99C File Offset: 0x00179B9C
		public bool UpdateTeamMailboxUserList(IList<ADObjectId> currentUserList, IList<ADObjectId> newUserList, out IList<ADObjectId> usersToAdd, out IList<ADObjectId> usersToRemove)
		{
			if (currentUserList == null)
			{
				throw new ArgumentNullException("currentList");
			}
			if (newUserList == null)
			{
				throw new ArgumentNullException("newUserList");
			}
			usersToRemove = TeamMailbox.DiffUsers(currentUserList, newUserList);
			usersToAdd = TeamMailbox.DiffUsers(newUserList, currentUserList);
			foreach (ADObjectId item in usersToRemove)
			{
				currentUserList.Remove(item);
			}
			foreach (ADObjectId item2 in usersToAdd)
			{
				currentUserList.Add(item2);
			}
			return usersToRemove.Count > 0 || usersToAdd.Count > 0;
		}

		// Token: 0x06005ACF RID: 23247 RVA: 0x0017BA68 File Offset: 0x00179C68
		public bool ClearClosedAndDeletedTeamMailboxesInShowInMyClientList(ADUser userMailbox)
		{
			if (userMailbox == null)
			{
				throw new ArgumentNullException("userMailbox");
			}
			bool result = false;
			List<ADObjectId> list = new List<ADObjectId>();
			foreach (ADObjectId item in userMailbox.TeamMailboxShowInClientList)
			{
				list.Add(item);
			}
			foreach (ADObjectId adobjectId in list)
			{
				Exception ex;
				ADUser aduser = TeamMailboxADUserResolver.Resolve(this.DataSession, adobjectId, out ex);
				if (ex == null && (aduser == null || !TeamMailbox.FromDataObject(aduser).Active))
				{
					userMailbox.TeamMailboxShowInClientList.Remove(adobjectId);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06005AD0 RID: 23248 RVA: 0x0017BB40 File Offset: 0x00179D40
		public bool SetShowInMyClient(ADObjectId userId, bool show, out bool maxPinnedReached, out Exception ex)
		{
			if (userId == null)
			{
				throw new ArgumentNullException("userId");
			}
			bool flag = false;
			maxPinnedReached = false;
			ex = null;
			ADUser aduser = TeamMailboxADUserResolver.ResolveBypassCache(this.DataSession, userId, out ex);
			if (ex == null && aduser != null && !aduser.RecipientTypeDetails.Equals(RecipientTypeDetails.MailUser))
			{
				ADObjectId id = this.TeamMailbox.Id;
				try
				{
					flag = this.ClearClosedAndDeletedTeamMailboxesInShowInMyClientList(aduser);
					maxPinnedReached = (show && aduser.TeamMailboxShowInClientList.Count >= 10);
					if (!maxPinnedReached)
					{
						if (show && !aduser.TeamMailboxShowInClientList.Contains(id))
						{
							aduser.TeamMailboxShowInClientList.Add(id);
							flag = true;
						}
						else if (!show && aduser.TeamMailboxShowInClientList.Contains(id))
						{
							aduser.TeamMailboxShowInClientList.Remove(id);
							flag = true;
						}
					}
					if (flag)
					{
						try
						{
							this.DataSession.Save(aduser);
						}
						catch (TransientException arg)
						{
							ex = new Exception(string.Format("When setting ShowInMyClient for user {0}, an error happened: {1}", aduser.DisplayName, arg));
						}
						catch (DataSourceOperationException arg2)
						{
							ex = new Exception(string.Format("When setting ShowInMyClient for user {0}, an error happened: {1}", aduser.DisplayName, arg2));
						}
					}
				}
				catch (InvalidOperationException arg3)
				{
					ex = new Exception(string.Format("When setting ShowInMyClient for user {0}, an error happened: {1}", aduser.DisplayName, arg3));
				}
			}
			return flag;
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x0017BCA8 File Offset: 0x00179EA8
		public void SetShowInMyClient(IList<ADObjectId> usersToAdd, IList<ADObjectId> usersToRemove, out IList<Exception> exceptions)
		{
			exceptions = new List<Exception>();
			if (usersToAdd != null)
			{
				foreach (ADObjectId userId in usersToAdd)
				{
					bool flag;
					Exception ex;
					this.SetShowInMyClient(userId, true, out flag, out ex);
					if (ex != null)
					{
						exceptions.Add(ex);
					}
				}
			}
			if (usersToRemove != null)
			{
				foreach (ADObjectId userId2 in usersToRemove)
				{
					bool flag;
					Exception ex;
					this.SetShowInMyClient(userId2, false, out flag, out ex);
					if (ex != null)
					{
						exceptions.Add(ex);
					}
				}
			}
		}

		// Token: 0x06005AD2 RID: 23250 RVA: 0x0017BD60 File Offset: 0x00179F60
		public void SetTeamMailboxUserPermissions(IList<ADObjectId> usersToAdd, IList<ADObjectId> usersToRemove, SecurityIdentifier[] additionalSids, bool save = true)
		{
			ADUser aduser = (ADUser)this.TeamMailbox.DataObject;
			RawSecurityDescriptor exchangeSecurityDescriptor = aduser.ExchangeSecurityDescriptor;
			ActiveDirectorySecurity activeDirectorySecurity = SecurityDescriptorConverter.ConvertToActiveDirectorySecurity(exchangeSecurityDescriptor);
			if (usersToAdd != null)
			{
				foreach (ADObjectId userId in usersToAdd)
				{
					SecurityIdentifier userSid = this.GetUserSid(userId);
					if (userSid != null)
					{
						activeDirectorySecurity.AddAccessRule(new ActiveDirectoryAccessRule(userSid, ActiveDirectoryRights.CreateChild, AccessControlType.Allow, Guid.Empty, ActiveDirectorySecurityInheritance.All, Guid.Empty));
					}
				}
			}
			if (usersToRemove != null)
			{
				foreach (ADObjectId userId2 in usersToRemove)
				{
					SecurityIdentifier userSid2 = this.GetUserSid(userId2);
					if (userSid2 != null)
					{
						activeDirectorySecurity.RemoveAccessRule(new ActiveDirectoryAccessRule(userSid2, ActiveDirectoryRights.CreateChild, AccessControlType.Allow, Guid.Empty, ActiveDirectorySecurityInheritance.All, Guid.Empty));
					}
				}
			}
			if (additionalSids != null)
			{
				foreach (SecurityIdentifier identity in additionalSids)
				{
					activeDirectorySecurity.AddAccessRule(new ActiveDirectoryAccessRule(identity, ActiveDirectoryRights.CreateChild, AccessControlType.Allow, Guid.Empty, ActiveDirectorySecurityInheritance.All, Guid.Empty));
				}
			}
			aduser.ExchangeSecurityDescriptor = new RawSecurityDescriptor(activeDirectorySecurity.GetSecurityDescriptorBinaryForm(), 0);
			if (save)
			{
				this.DataSession.Save(aduser);
			}
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x0017BEBC File Offset: 0x0017A0BC
		private SecurityIdentifier GetUserSid(ADObjectId userId)
		{
			if (userId == null)
			{
				throw new ArgumentNullException("userId");
			}
			Exception ex;
			ADUser aduser = TeamMailboxADUserResolver.Resolve(this.DataSession, userId, out ex);
			if (aduser == null)
			{
				return null;
			}
			if (null != aduser.MasterAccountSid && !aduser.MasterAccountSid.IsWellKnown(WellKnownSidType.SelfSid))
			{
				return aduser.MasterAccountSid;
			}
			return aduser.Sid;
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x0017BF15 File Offset: 0x0017A115
		private ADUser ResolveUserFunction(ADObjectId id, out Exception ex)
		{
			return TeamMailboxADUserResolver.Resolve(this.DataSession, id, out ex);
		}

		// Token: 0x0400320D RID: 12813
		private const ActiveDirectoryRights MailboxRightsFullAccess = ActiveDirectoryRights.CreateChild;
	}
}
