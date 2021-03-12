using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DD8 RID: 3544
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SharingProviderHandlerInternal : SharingProviderHandler
	{
		// Token: 0x060079F8 RID: 31224 RVA: 0x0021B7C2 File Offset: 0x002199C2
		internal SharingProviderHandlerInternal()
		{
		}

		// Token: 0x060079F9 RID: 31225 RVA: 0x0021B7CA File Offset: 0x002199CA
		internal override void FillSharingMessageProvider(SharingContext context, SharingMessageProvider sharingMessageProvider)
		{
			sharingMessageProvider.FolderId = context.FolderId.ToHexEntryId();
			sharingMessageProvider.MailboxId = HexConverter.ByteArrayToHexString(context.MailboxId);
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x0021B7F0 File Offset: 0x002199F0
		internal override void ParseSharingMessageProvider(SharingContext context, SharingMessageProvider sharingMessageProvider)
		{
			if (!AddressBookEntryId.IsAddressBookEntryId(context.InitiatorEntryId))
			{
				ExTraceGlobals.SharingTracer.TraceError<string>((long)this.GetHashCode(), "{0}: InitiatorEntryId is invalid.", context.UserLegacyDN);
				throw new InvalidSharingDataException("InitiatorEntryId", HexConverter.ByteArrayToHexString(context.InitiatorEntryId));
			}
			if (!string.IsNullOrEmpty(sharingMessageProvider.FolderId))
			{
				try
				{
					context.FolderId = StoreObjectId.FromHexEntryId(sharingMessageProvider.FolderId, ObjectClass.GetObjectType(context.FolderClass));
				}
				catch (CorruptDataException)
				{
					ExTraceGlobals.SharingTracer.TraceError<string, string>((long)this.GetHashCode(), "{0}: FolderId is invalid: {1}", context.UserLegacyDN, sharingMessageProvider.FolderId);
					throw new InvalidSharingDataException("FolderId", sharingMessageProvider.FolderId);
				}
			}
			if (!string.IsNullOrEmpty(sharingMessageProvider.MailboxId))
			{
				byte[] array = HexConverter.HexStringToByteArray(sharingMessageProvider.MailboxId);
				if (StoreEntryId.TryParseStoreEntryIdMailboxDN(array) == null)
				{
					ExTraceGlobals.SharingTracer.TraceError<string, string>((long)this.GetHashCode(), "{0}: MailboxId is invalid: {1}", context.UserLegacyDN, sharingMessageProvider.MailboxId);
					throw new InvalidSharingDataException("MailboxId", sharingMessageProvider.MailboxId);
				}
				context.MailboxId = array;
			}
		}

		// Token: 0x060079FB RID: 31227 RVA: 0x0021B908 File Offset: 0x00219B08
		protected override bool InternalValidateCompatibility(Folder folderToShare)
		{
			return SharingDataType.FromContainerClass(folderToShare.ClassName) != null;
		}

		// Token: 0x060079FC RID: 31228 RVA: 0x0021B91C File Offset: 0x00219B1C
		protected override ValidRecipient InternalCheckOneRecipient(ADRecipient mailboxOwner, string recipient, IRecipientSession recipientSession)
		{
			ADRecipient adrecipient = recipientSession.FindByProxyAddress(new SmtpProxyAddress(recipient, false));
			bool flag;
			if (adrecipient == null)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<ADRecipient, string>((long)this.GetHashCode(), "{0}: {1} is not found from AD in {0}'s organization.", mailboxOwner, recipient);
				flag = false;
			}
			else
			{
				ExTraceGlobals.SharingTracer.TraceDebug((long)this.GetHashCode(), "{0}: {1} is found from AD in {0}'s organization: RecipientType = {2}; RecipientDisplayType = {3}; IsValidSecurityPrincipal = {4}.", new object[]
				{
					mailboxOwner,
					recipient,
					adrecipient.RecipientType,
					adrecipient.RecipientDisplayType,
					adrecipient.IsValidSecurityPrincipal
				});
				flag = adrecipient.IsValidSecurityPrincipal;
			}
			if (!flag)
			{
				return null;
			}
			return new ValidRecipient(recipient, adrecipient);
		}

		// Token: 0x060079FD RID: 31229 RVA: 0x0021B9BC File Offset: 0x00219BBC
		protected override PerformInvitationResults InternalPerformInvitation(MailboxSession mailboxSession, SharingContext context, ValidRecipient[] recipients, IFrontEndLocator frontEndLocator)
		{
			PermissionLevel permissionLevel = this.GetPermissionLevel(context);
			FreeBusyAccess? freeBusy = this.GetFreeBusy(context);
			using (FolderPermissionContext current = FolderPermissionContext.GetCurrent(mailboxSession, context))
			{
				foreach (ValidRecipient recipient in recipients)
				{
					PermissionSecurityPrincipal principal = this.CreatePermissionSecurityPrincipal(recipient, mailboxSession.MailboxOwner);
					current.AddOrChangePermission(principal, permissionLevel, freeBusy);
				}
			}
			return new PerformInvitationResults(recipients);
		}

		// Token: 0x060079FE RID: 31230 RVA: 0x0021BA38 File Offset: 0x00219C38
		protected override void InternalPerformRevocation(MailboxSession mailboxSession, SharingContext context)
		{
			using (FolderPermissionContext current = FolderPermissionContext.GetCurrent(mailboxSession, context))
			{
				ValidRecipient recipient = new ValidRecipient(context.InitiatorSmtpAddress, null);
				PermissionSecurityPrincipal principal = this.CreatePermissionSecurityPrincipal(recipient, mailboxSession.MailboxOwner);
				current.RemovePermission(principal);
			}
		}

		// Token: 0x060079FF RID: 31231 RVA: 0x0021BA8C File Offset: 0x00219C8C
		protected override SubscribeResults InternalPerformSubscribe(MailboxSession mailboxSession, SharingContext context)
		{
			return new SubscribeResultsInternal(context.DataType, context.InitiatorSmtpAddress, context.InitiatorName, context.FolderName, context.FolderId, context.MailboxId);
		}

		// Token: 0x06007A00 RID: 31232 RVA: 0x0021BAB8 File Offset: 0x00219CB8
		private PermissionSecurityPrincipal CreatePermissionSecurityPrincipal(ValidRecipient recipient, IExchangePrincipal mailboxPrincipal)
		{
			ADRecipient adrecipient = recipient.ADRecipient;
			if (adrecipient == null)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.FullyConsistent, null, mailboxPrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 248, "CreatePermissionSecurityPrincipal", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Sharing\\SharingProviderHandlerInternal.cs");
				adrecipient = tenantOrRootOrgRecipientSession.FindByProxyAddress(new SmtpProxyAddress(recipient.SmtpAddress, false));
			}
			return new PermissionSecurityPrincipal(adrecipient);
		}

		// Token: 0x06007A01 RID: 31233 RVA: 0x0021BB18 File Offset: 0x00219D18
		private PermissionLevel GetPermissionLevel(SharingContext context)
		{
			SharingContextPermissions sharingPermissions = context.SharingPermissions;
			if (sharingPermissions != SharingContextPermissions.Reviewer)
			{
				if (sharingPermissions != SharingContextPermissions.Editor)
				{
					throw new ArgumentOutOfRangeException("context");
				}
				if (!context.IsPrimary)
				{
					return PermissionLevel.Editor;
				}
				if (context.DataType == SharingDataType.Calendar || context.SharingDetail == SharingContextDetailLevel.Editor)
				{
					return PermissionLevel.Editor;
				}
				throw new NotSupportedException("Cannot retrieve permission level when having an editor on defualt non calendar folder");
			}
			else
			{
				if (context.DataType != SharingDataType.Calendar || context.SharingDetail == SharingContextDetailLevel.FullDetails)
				{
					return PermissionLevel.Reviewer;
				}
				return PermissionLevel.None;
			}
		}

		// Token: 0x06007A02 RID: 31234 RVA: 0x0021BB8C File Offset: 0x00219D8C
		private FreeBusyAccess? GetFreeBusy(SharingContext context)
		{
			if (context.DataType != SharingDataType.Calendar)
			{
				return null;
			}
			if (context.SharingPermissions == SharingContextPermissions.Editor)
			{
				return new FreeBusyAccess?(FreeBusyAccess.Details);
			}
			SharingContextDetailLevel sharingDetail = context.SharingDetail;
			switch (sharingDetail)
			{
			case SharingContextDetailLevel.AvailabilityOnly:
				return new FreeBusyAccess?(FreeBusyAccess.Basic);
			case SharingContextDetailLevel.Limited:
				return new FreeBusyAccess?(FreeBusyAccess.Details);
			case (SharingContextDetailLevel)3:
				goto IL_63;
			case SharingContextDetailLevel.FullDetails:
				break;
			default:
				if (sharingDetail != SharingContextDetailLevel.Editor)
				{
					goto IL_63;
				}
				break;
			}
			return new FreeBusyAccess?(FreeBusyAccess.Details);
			IL_63:
			throw new ArgumentOutOfRangeException("context");
		}
	}
}
