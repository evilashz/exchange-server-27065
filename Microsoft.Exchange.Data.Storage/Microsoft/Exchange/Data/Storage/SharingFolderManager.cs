using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DD0 RID: 3536
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SharingFolderManager : SharingFolderManagerBase<SharingSubscriptionData>
	{
		// Token: 0x06007972 RID: 31090 RVA: 0x00218F04 File Offset: 0x00217104
		public SharingFolderManager(MailboxSession mailboxSession) : base(mailboxSession)
		{
		}

		// Token: 0x17002081 RID: 8321
		// (get) Token: 0x06007973 RID: 31091 RVA: 0x00218F0D File Offset: 0x0021710D
		protected override ExtendedFolderFlags SharingFolderFlags
		{
			get
			{
				return ExtendedFolderFlags.ReadOnly | ExtendedFolderFlags.SharedIn | ExtendedFolderFlags.PersonalShare | ExtendedFolderFlags.ExclusivelyBound | ExtendedFolderFlags.ExchangeCrossOrgShareFolder;
			}
		}

		// Token: 0x06007974 RID: 31092 RVA: 0x00218F14 File Offset: 0x00217114
		protected override void CreateOrUpdateSharingBinding(SharingSubscriptionData sharingSubscriptionData, string localFolderName, StoreObjectId folderId)
		{
			SharingBindingData bindingData = SharingBindingData.CreateSharingBindingData(sharingSubscriptionData.DataType, sharingSubscriptionData.SharerName, sharingSubscriptionData.SharerIdentity, sharingSubscriptionData.RemoteFolderName, sharingSubscriptionData.RemoteFolderId, localFolderName, folderId, sharingSubscriptionData.IsPrimary);
			new SharingBindingManager(base.MailboxSession).CreateOrUpdateSharingBinding(bindingData);
		}

		// Token: 0x06007975 RID: 31093 RVA: 0x00218F60 File Offset: 0x00217160
		private string ResolveInContacts(string smtpAddress)
		{
			using (ContactsFolder contactsFolder = ContactsFolder.Bind(base.MailboxSession, DefaultFolderType.Contacts))
			{
				using (FindInfo<Contact> findInfo = contactsFolder.FindByEmailAddress(smtpAddress, new PropertyDefinition[]
				{
					StoreObjectSchema.DisplayName
				}))
				{
					if (findInfo.FindStatus != FindStatus.NotFound && !string.IsNullOrEmpty(findInfo.Result.DisplayName))
					{
						ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, string, string>((long)this.GetHashCode(), "{0}: {1} is resolved as {2}", base.MailboxSession.MailboxOwner, smtpAddress, findInfo.Result.DisplayName);
						return findInfo.Result.DisplayName;
					}
				}
			}
			return smtpAddress;
		}

		// Token: 0x06007976 RID: 31094 RVA: 0x00219028 File Offset: 0x00217228
		protected override LocalizedString CreateLocalFolderName(SharingSubscriptionData sharingSubscriptionData)
		{
			string text = this.ResolveInContacts(sharingSubscriptionData.SharerIdentity).ToString(base.MailboxSession.InternalCulture);
			return sharingSubscriptionData.IsPrimary ? new LocalizedString(text) : ServerStrings.SharingFolderName(sharingSubscriptionData.RemoteFolderName.ToString(base.MailboxSession.InternalCulture), text);
		}

		// Token: 0x040053F8 RID: 21496
		private const ExtendedFolderFlags ExternalSharingFolderFlags = ExtendedFolderFlags.ReadOnly | ExtendedFolderFlags.SharedIn | ExtendedFolderFlags.PersonalShare | ExtendedFolderFlags.ExclusivelyBound | ExtendedFolderFlags.ExchangeCrossOrgShareFolder;
	}
}
