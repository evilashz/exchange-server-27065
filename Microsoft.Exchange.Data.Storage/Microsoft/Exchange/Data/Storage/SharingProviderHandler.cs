using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DD6 RID: 3542
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SharingProviderHandler
	{
		// Token: 0x060079D9 RID: 31193
		internal abstract void FillSharingMessageProvider(SharingContext context, SharingMessageProvider sharingMessageProvider);

		// Token: 0x060079DA RID: 31194
		internal abstract void ParseSharingMessageProvider(SharingContext context, SharingMessageProvider sharingMessageProvider);

		// Token: 0x060079DB RID: 31195
		protected abstract bool InternalValidateCompatibility(Folder folderToShare);

		// Token: 0x060079DC RID: 31196 RVA: 0x0021AB50 File Offset: 0x00218D50
		internal bool ValidateCompatibility(Folder folderToShare)
		{
			Util.ThrowOnNullArgument(folderToShare, "folderToShare");
			if (folderToShare.Session is PublicFolderSession)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Cannot share public folder.", folderToShare.Session.UserLegacyDN);
				throw new CannotShareFolderException(ServerStrings.CannotSharePublicFolder);
			}
			if (folderToShare.Id.ObjectId.ObjectType == StoreObjectType.SearchFolder)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Cannot share search folder.", folderToShare.Session.UserLegacyDN);
				throw new CannotShareFolderException(ServerStrings.CannotShareSearchFolder);
			}
			object obj = folderToShare.TryGetProperty(FolderSchema.ExtendedFolderFlags);
			if (!(obj is PropertyError))
			{
				int num = (int)obj;
				if ((num & 8192) != 0)
				{
					ExTraceGlobals.SharingTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Cannot share other people's personal folder.", folderToShare.Session.UserLegacyDN);
					throw new CannotShareFolderException(ServerStrings.CannotShareOtherPersonalFolder);
				}
			}
			return this.InternalValidateCompatibility(folderToShare);
		}

		// Token: 0x060079DD RID: 31197
		protected abstract ValidRecipient InternalCheckOneRecipient(ADRecipient mailboxOwner, string recipient, IRecipientSession recipientSession);

		// Token: 0x060079DE RID: 31198 RVA: 0x0021AC48 File Offset: 0x00218E48
		internal CheckRecipientsResults CheckRecipients(ADRecipient mailboxOwner, string[] recipients)
		{
			Util.ThrowOnNullArgument(mailboxOwner, "mailboxOwner");
			Util.ThrowOnNullArgument(recipients, "recipients");
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.FullyConsistent, mailboxOwner.Session.NetworkCredential, mailboxOwner.OrganizationId.ToADSessionSettings(), 127, "CheckRecipients", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Sharing\\SharingProviderHandler.cs");
			List<ValidRecipient> list = new List<ValidRecipient>(recipients.Length);
			List<string> list2 = new List<string>(recipients.Length);
			foreach (string text in recipients)
			{
				ValidRecipient validRecipient = this.InternalCheckOneRecipient(mailboxOwner, text, tenantOrRootOrgRecipientSession);
				if (validRecipient != null)
				{
					ExTraceGlobals.SharingTracer.TraceDebug<ADRecipient, string, SharingProviderHandler>((long)this.GetHashCode(), "{0}: {1} is a valid recipient for sharing handler {2}.", mailboxOwner, text, this);
					list.Add(validRecipient);
				}
				else
				{
					ExTraceGlobals.SharingTracer.TraceDebug<ADRecipient, string, SharingProviderHandler>((long)this.GetHashCode(), "{0}: {1} is an invalid recipient for sharing handler {2}.", mailboxOwner, text, this);
					list2.Add(text);
				}
			}
			return new CheckRecipientsResults(list.ToArray(), list2.ToArray());
		}

		// Token: 0x060079DF RID: 31199
		protected abstract PerformInvitationResults InternalPerformInvitation(MailboxSession mailboxSession, SharingContext context, ValidRecipient[] recipients, IFrontEndLocator frontEndLocator);

		// Token: 0x060079E0 RID: 31200 RVA: 0x0021AD2C File Offset: 0x00218F2C
		internal PerformInvitationResults PerformInvitation(MailboxSession mailboxSession, SharingContext context, ValidRecipient[] recipients, IFrontEndLocator frontEndLocator)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			Util.ThrowOnNullArgument(context, "context");
			Util.ThrowOnNullArgument(recipients, "recipients");
			Util.ThrowOnNullArgument(frontEndLocator, "frontEndLocator");
			if (recipients.Length == 0)
			{
				return PerformInvitationResults.Ignored;
			}
			if (!context.SharingMessageType.IsInvitationOrAcceptOfRequest)
			{
				return PerformInvitationResults.Ignored;
			}
			return this.InternalPerformInvitation(mailboxSession, context, recipients, frontEndLocator);
		}

		// Token: 0x060079E1 RID: 31201
		protected abstract void InternalPerformRevocation(MailboxSession mailboxSession, SharingContext context);

		// Token: 0x060079E2 RID: 31202 RVA: 0x0021AD90 File Offset: 0x00218F90
		internal void PerformRevocation(MailboxSession mailboxSession, SharingContext context)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			Util.ThrowOnNullArgument(context, "context");
			if (!context.SharingMessageType.IsRequest)
			{
				throw new InvalidOperationException("Only can revoke on request message.");
			}
			context.FolderId = mailboxSession.GetDefaultFolderId(context.DataType.DefaultFolderType);
			this.InternalPerformRevocation(mailboxSession, context);
		}

		// Token: 0x060079E3 RID: 31203
		protected abstract SubscribeResults InternalPerformSubscribe(MailboxSession mailboxSession, SharingContext context);

		// Token: 0x060079E4 RID: 31204 RVA: 0x0021ADEA File Offset: 0x00218FEA
		internal SubscribeResults PerformSubscribe(MailboxSession mailboxSession, SharingContext context)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			Util.ThrowOnNullArgument(context, "context");
			if (!context.SharingMessageType.IsInvitationOrAcceptOfRequest)
			{
				throw new InvalidOperationException("Only can subscribe on invitation or acceptofrequest message.");
			}
			return this.InternalPerformSubscribe(mailboxSession, context);
		}

		// Token: 0x04005417 RID: 21527
		private const int MaxRetryTime = 3;
	}
}
