using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020008CA RID: 2250
	internal class DelegateUserCollectionHandler
	{
		// Token: 0x06003FA4 RID: 16292 RVA: 0x000DC016 File Offset: 0x000DA216
		public DelegateUserCollectionHandler(MailboxSession session, ADRecipientSessionContext adRecipientSessionContext)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(adRecipientSessionContext, "adRecipientSessionContext");
			this.MailboxOwner = session.MailboxOwner;
			this.xsoDelegateUsers = new DelegateUserCollection(session);
			this.adRecipientSessionContext = adRecipientSessionContext;
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000DC054 File Offset: 0x000DA254
		public DelegateUser AddDelegate(DelegateUserType delegateUser)
		{
			Dictionary<DefaultFolderType, PermissionLevel> folderPermissions = new Dictionary<DefaultFolderType, PermissionLevel>();
			return this.AddDelegate(delegateUser, folderPermissions);
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000DC070 File Offset: 0x000DA270
		public DelegateUser UpdateDelegate(DelegateUserType delegateUser)
		{
			Util.ThrowOnNullArgument(delegateUser, "delegateUser");
			DelegateUser delegateUser2 = DelegateUtilities.GetDelegateUser(delegateUser.UserId, this.xsoDelegateUsers, this.adRecipientSessionContext);
			DelegateUtilities.UpdateXsoDelegateUser(delegateUser2, delegateUser);
			return delegateUser2;
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x000DC0A8 File Offset: 0x000DA2A8
		public void RemoveDelegate(DelegateUser xsoDelegateUser)
		{
			Util.ThrowOnNullArgument(xsoDelegateUser, "xsoDelegateUser");
			this.xsoDelegateUsers.Remove(xsoDelegateUser);
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x000DC0C2 File Offset: 0x000DA2C2
		public DelegateUser GetDelegateUser(ADRecipient adRecipient)
		{
			Util.ThrowOnNullArgument(adRecipient, "adRecipient");
			return DelegateUtilities.GetDelegateUser(adRecipient, this.xsoDelegateUsers);
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000DC0DC File Offset: 0x000DA2DC
		public void RemoveDelegate(UserId user)
		{
			Util.ThrowOnNullArgument(user, "user");
			DelegateUser delegateUser = DelegateUtilities.GetDelegateUser(user, this.xsoDelegateUsers, this.adRecipientSessionContext);
			this.xsoDelegateUsers.Remove(delegateUser);
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x000DC114 File Offset: 0x000DA314
		public void SetDelegateOptions(DeliverMeetingRequestsType deliverMeetingRequests)
		{
			this.xsoDelegateUsers.DelegateRuleType = DelegateUtilities.ConvertToDeliverRuleType(deliverMeetingRequests);
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x000DC127 File Offset: 0x000DA327
		public DeliverMeetingRequestsType GetMeetingRequestDeliveryOptionForDelegateUsers()
		{
			return DelegateUtilities.ConvertToDeliverMeetingRequestType(this.xsoDelegateUsers.DelegateRuleType);
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06003FAC RID: 16300 RVA: 0x000DC139 File Offset: 0x000DA339
		public int DelegateUsersCount
		{
			get
			{
				return this.xsoDelegateUsers.Count;
			}
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x000DC146 File Offset: 0x000DA346
		public DelegateUserCollectionSaveResult SaveDelegate(bool removeUnknown)
		{
			return this.xsoDelegateUsers.Save(removeUnknown);
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x000DC154 File Offset: 0x000DA354
		public void AddDelegateWithCalendarEditorPermission(UserId user, bool viewPrivateAppointments)
		{
			Util.ThrowOnNullArgument(user, "user");
			this.AddDelegate(new DelegateUserType(user)
			{
				DelegatePermissions = new DelegatePermissionsType
				{
					CalendarFolderPermissionLevel = DelegateFolderPermissionLevelType.Editor
				},
				ViewPrivateItems = new bool?(viewPrivateAppointments),
				ReceiveCopiesOfMeetingMessages = new bool?(true)
			}, new Dictionary<DefaultFolderType, PermissionLevel>
			{
				{
					DefaultFolderType.Calendar,
					PermissionLevel.Editor
				}
			});
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x000DC1B8 File Offset: 0x000DA3B8
		private DelegateUser AddDelegate(DelegateUserType delegateUser, Dictionary<DefaultFolderType, PermissionLevel> folderPermissions)
		{
			Util.ThrowOnNullArgument(delegateUser, "delegateUser");
			DelegateUser delegateUser2 = null;
			try
			{
				ExchangePrincipal exchangePrincipal = DelegateUtilities.GetExchangePrincipal(delegateUser.UserId, this.adRecipientSessionContext);
				delegateUser2 = DelegateUser.Create(exchangePrincipal, folderPermissions);
			}
			catch (DelegateExceptionInvalidDelegateUser)
			{
				VariantConfigurationSnapshot configuration = this.MailboxOwner.GetConfiguration();
				if (!configuration.DataStorage.CrossPremiseDelegate.Enabled)
				{
					throw;
				}
				delegateUser2 = DelegateUser.InternalCreate(delegateUser.UserId.DisplayName, delegateUser.UserId.PrimarySmtpAddress, folderPermissions);
			}
			DelegateUtilities.UpdateXsoDelegateUser(delegateUser2, delegateUser);
			try
			{
				this.xsoDelegateUsers.Add(delegateUser2);
			}
			catch (DelegateUserValidationException ex)
			{
				if (ex.Problem == DelegateValidationProblem.Duplicate)
				{
					throw new DelegateExceptionAlreadyExists(ex);
				}
				if (ex.Problem == DelegateValidationProblem.IsOwner)
				{
					throw new DelegateExceptionCannotAddOwner(ex);
				}
				throw new DelegateExceptionValidationFailed(ex);
			}
			return delegateUser2;
		}

		// Token: 0x04002468 RID: 9320
		private readonly DelegateUserCollection xsoDelegateUsers;

		// Token: 0x04002469 RID: 9321
		private readonly ADRecipientSessionContext adRecipientSessionContext;

		// Token: 0x0400246A RID: 9322
		private readonly IExchangePrincipal MailboxOwner;
	}
}
