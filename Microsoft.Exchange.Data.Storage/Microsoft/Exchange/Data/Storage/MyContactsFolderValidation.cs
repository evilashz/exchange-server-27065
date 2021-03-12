using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000686 RID: 1670
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MyContactsFolderValidation : SearchFolderValidation
	{
		// Token: 0x0600447A RID: 17530 RVA: 0x00123F48 File Offset: 0x00122148
		internal MyContactsFolderValidation(ContactsSearchFolderCriteria contactsSearchFolderCriteria) : base(new IValidator[]
		{
			new MatchMapiFolderType(FolderType.Search)
		})
		{
			this.contactsSearchFolderCriteria = contactsSearchFolderCriteria;
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x00123F74 File Offset: 0x00122174
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			base.SetPropertiesInternal(context, folder);
			folder.Save();
			SearchFolder searchFolder = (SearchFolder)folder;
			StoreObjectId[] folderScope;
			if (MailboxSession.DefaultFoldersToForceInit != null && MailboxSession.DefaultFoldersToForceInit.Contains(DefaultFolderType.MyContacts))
			{
				folderScope = this.contactsSearchFolderCriteria.GetExistingDefaultFolderScope(context);
			}
			else
			{
				folderScope = this.contactsSearchFolderCriteria.GetDefaultFolderScope(context.Session, true);
			}
			SearchFolderCriteria searchFolderCriteria = ContactsSearchFolderCriteria.CreateSearchCriteria(folderScope);
			ContactsSearchFolderCriteria.ApplyContinuousSearchFolderCriteria(XSOFactory.Default, context.Session, searchFolder, searchFolderCriteria);
			bool flag = context.Session.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.GroupMailbox || !context.Session.ClientInfoString.Contains("Client=WebServices;Action=ConfigureGroupMailbox");
			MyContactsFolderValidation.Tracer.TraceDebug<string, RecipientTypeDetails, bool>((long)context.Session.GetHashCode(), "SearchFolder criteria applied. ClientInfoString={0}, RecipientTypeDetails={1}, ShouldWaitForNotification={2}", context.Session.ClientInfoString, context.Session.MailboxOwner.RecipientTypeDetails, flag);
			if (flag)
			{
				ContactsSearchFolderCriteria.WaitForSearchFolderPopulation(XSOFactory.Default, context.Session, searchFolder);
			}
			folder.Load(null);
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x00124070 File Offset: 0x00122270
		internal override bool EnsureIsValid(DefaultFolderContext context, StoreObjectId folderId, Dictionary<string, DefaultFolderManager.FolderData> folderDataDictionary)
		{
			if (!base.EnsureIsValid(context, folderId, folderDataDictionary))
			{
				return false;
			}
			string idString = Convert.ToBase64String(folderId.ProviderLevelItemId);
			return base.BindAndValidateFolder(context, folderId, idString);
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x001240A0 File Offset: 0x001222A0
		internal override bool EnsureIsValid(DefaultFolderContext context, Folder folder)
		{
			if (!base.EnsureIsValid(context, folder))
			{
				return false;
			}
			SearchFolder searchFolder = folder as SearchFolder;
			if (searchFolder == null)
			{
				return false;
			}
			if (SearchFolderValidation.TryGetSearchCriteria(searchFolder) == null)
			{
				this.SetPropertiesInternal(context, folder);
			}
			return true;
		}

		// Token: 0x04002554 RID: 9556
		private static readonly Trace Tracer = ExTraceGlobals.MyContactsFolderTracer;

		// Token: 0x04002555 RID: 9557
		private readonly ContactsSearchFolderCriteria contactsSearchFolderCriteria;
	}
}
