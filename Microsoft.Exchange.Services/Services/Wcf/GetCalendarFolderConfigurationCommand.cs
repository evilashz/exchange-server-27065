using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000954 RID: 2388
	internal sealed class GetCalendarFolderConfigurationCommand : ServiceCommand<GetCalendarFolderConfigurationResponse>
	{
		// Token: 0x060044D4 RID: 17620 RVA: 0x000EE6DB File Offset: 0x000EC8DB
		public GetCalendarFolderConfigurationCommand(CallContext callContext, GetCalendarFolderConfigurationRequest request) : base(callContext)
		{
			this.request = request;
			this.request.ValidateRequest();
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x000EE6F8 File Offset: 0x000EC8F8
		protected override GetCalendarFolderConfigurationResponse InternalExecute()
		{
			IdAndSession idAndSession;
			try
			{
				idAndSession = base.IdConverter.ConvertFolderIdToIdAndSession(this.request.FolderId, IdConverter.ConvertOption.IgnoreChangeKey | IdConverter.ConvertOption.AllowKnownExternalUsers | IdConverter.ConvertOption.IsHierarchicalOperation);
			}
			catch (FolderNotFoundException)
			{
				return new GetCalendarFolderConfigurationResponse(CalendarActionError.CalendarActionUnableToFindCalendarFolder);
			}
			StoreSession session = idAndSession.Session;
			PropertyDefinition[] propsToReturn = session.IsPublicFolderSession ? new PropertyDefinition[]
			{
				StoreObjectSchema.EffectiveRights,
				FolderSchema.ReplicaList
			} : new PropertyDefinition[]
			{
				StoreObjectSchema.EffectiveRights
			};
			GetCalendarFolderConfigurationResponse result;
			using (Folder folder = Folder.Bind(session, idAndSession.Id, propsToReturn))
			{
				if (!(folder is CalendarFolder))
				{
					result = new GetCalendarFolderConfigurationResponse(CalendarActionError.CalendarActionFolderIdNotCalendarFolder);
				}
				else
				{
					EffectiveRights effectiveRights = folder.TryGetValueOrDefault(StoreObjectSchema.EffectiveRights, EffectiveRights.None);
					string[] replicaList = null;
					if (session.IsPublicFolderSession)
					{
						replicaList = ReplicaListProperty.GetReplicaListFromStoreObject(folder);
					}
					CalendarFolderType folder2 = new CalendarFolderType
					{
						FolderId = IdConverter.ConvertStoreFolderIdToFolderId(idAndSession.Id, session),
						EffectiveRights = EffectiveRightsProperty.GetFromEffectiveRights(effectiveRights, session),
						ReplicaList = replicaList
					};
					MasterCategoryListType masterCategoryList = this.GetMasterCategoryList(this.request.FolderId, session);
					result = new GetCalendarFolderConfigurationResponse(folder2, masterCategoryList);
				}
			}
			return result;
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x000EE834 File Offset: 0x000ECA34
		private MasterCategoryListType GetMasterCategoryList(BaseFolderId folderId, StoreSession storeSession)
		{
			if (storeSession.IsPublicFolderSession)
			{
				return null;
			}
			MailboxSession mclOwnerMailboxSession;
			if (base.MailboxIdentityMailboxSession.IsGroupMailbox())
			{
				mclOwnerMailboxSession = base.MailboxIdentityMailboxSession;
			}
			else
			{
				if (!(storeSession is MailboxSession))
				{
					throw new InvalidReferenceItemException();
				}
				mclOwnerMailboxSession = (storeSession as MailboxSession);
			}
			return new GetMasterCategoryList(mclOwnerMailboxSession).Execute();
		}

		// Token: 0x04002816 RID: 10262
		private readonly GetCalendarFolderConfigurationRequest request;
	}
}
