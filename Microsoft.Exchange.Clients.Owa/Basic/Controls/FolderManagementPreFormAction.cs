using System;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200004A RID: 74
	internal sealed class FolderManagementPreFormAction : IPreFormAction
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x000128F8 File Offset: 0x00010AF8
		public PreFormActionResponse Execute(OwaContext owaContext, out ApplicationElement applicationElement, out string type, out string state, out string action)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			applicationElement = ApplicationElement.NotSet;
			type = string.Empty;
			action = string.Empty;
			state = string.Empty;
			this.userContext = owaContext.UserContext;
			this.httpRequest = owaContext.HttpContext.Request;
			if (!Utilities.IsPostRequest(this.httpRequest))
			{
				return this.userContext.LastClientViewState.ToPreFormActionResponse();
			}
			this.folderManagementHelper = new FolderManagementHelper(owaContext);
			this.module = RequestParser.GetNavigationModuleFromQueryString(this.httpRequest, NavigationModule.Mail, true);
			if ((this.module == NavigationModule.Calendar && !this.userContext.IsFeatureEnabled(Feature.Calendar)) || (this.module == NavigationModule.Contacts && !this.userContext.IsFeatureEnabled(Feature.Contacts)))
			{
				throw new OwaSegmentationException("The " + this.module.ToString() + " feature is disabled");
			}
			this.folderId = RequestParser.GetFolderIdFromQueryString(this.httpRequest, true);
			string action2;
			if ((action2 = owaContext.FormsRegistryContext.Action) != null)
			{
				PreFormActionResponse preFormActionResponse;
				if (!(action2 == "Create"))
				{
					if (!(action2 == "Rename"))
					{
						if (!(action2 == "Move"))
						{
							if (!(action2 == "Delete"))
							{
								goto IL_152;
							}
							preFormActionResponse = this.ExecuteDeleteAction();
						}
						else
						{
							preFormActionResponse = this.ExecuteMoveAction();
						}
					}
					else
					{
						preFormActionResponse = this.ExecuteRenameAction();
					}
				}
				else
				{
					preFormActionResponse = this.ExecuteCreateAction();
				}
				if (owaContext[OwaContextProperty.InfobarMessage] != null)
				{
					((InfobarMessage)owaContext[OwaContextProperty.InfobarMessage]).IsActionResult = true;
				}
				preFormActionResponse.ApplicationElement = ApplicationElement.Dialog;
				preFormActionResponse.Type = "FolderManagement";
				PreFormActionResponse preFormActionResponse2 = preFormActionResponse;
				string name = "m";
				int num = (int)this.module;
				preFormActionResponse2.AddParameter(name, num.ToString());
				preFormActionResponse.AddParameter("fid", this.folderId.ToBase64String());
				return preFormActionResponse;
			}
			IL_152:
			throw new OwaInvalidRequestException("Invalid action for folder management preformaction");
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00012AC0 File Offset: 0x00010CC0
		private PreFormActionResponse ExecuteCreateAction()
		{
			string formParameter = Utilities.GetFormParameter(this.httpRequest, "nnfc", true);
			StoreObjectId destinationId;
			string folderClass;
			switch (this.module)
			{
			case NavigationModule.Mail:
				destinationId = RequestParser.GetStoreObjectId(this.httpRequest, "ftci", true, ParameterIn.Form);
				folderClass = "IPF.Note";
				break;
			case NavigationModule.Calendar:
				destinationId = this.userContext.CalendarFolderId;
				folderClass = "IPF.Appointment";
				break;
			case NavigationModule.Contacts:
				destinationId = this.userContext.ContactsFolderId;
				folderClass = "IPF.Contact";
				break;
			default:
				throw new OwaInvalidRequestException("Invalid request for folder management preformaction");
			}
			this.folderManagementHelper.Create(destinationId, folderClass, formParameter);
			return new PreFormActionResponse();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00012B5C File Offset: 0x00010D5C
		private PreFormActionResponse ExecuteRenameAction()
		{
			StoreObjectId storeObjectId = RequestParser.GetStoreObjectId(this.httpRequest, "ftr", true, ParameterIn.Form);
			string formParameter = Utilities.GetFormParameter(this.httpRequest, "nnfr", true);
			this.folderManagementHelper.Rename(storeObjectId, formParameter);
			return new PreFormActionResponse();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00012BA4 File Offset: 0x00010DA4
		private PreFormActionResponse ExecuteMoveAction()
		{
			if (this.module != NavigationModule.Mail)
			{
				throw new OwaInvalidRequestException("Invalid module for folder management preformaction Move");
			}
			StoreObjectId storeObjectId = RequestParser.GetStoreObjectId(this.httpRequest, "ftm", true, ParameterIn.Form);
			StoreObjectId storeObjectId2 = RequestParser.GetStoreObjectId(this.httpRequest, "nlfm", true, ParameterIn.Form);
			this.folderManagementHelper.Move(storeObjectId, storeObjectId2);
			return new PreFormActionResponse();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00012C00 File Offset: 0x00010E00
		private PreFormActionResponse ExecuteDeleteAction()
		{
			StoreObjectId storeObjectId = RequestParser.GetStoreObjectId(this.httpRequest, "ftd", true, ParameterIn.Form);
			int num = RequestParser.TryGetIntValueFromQueryString(this.httpRequest, "hd", 0);
			bool flag = this.folderManagementHelper.Delete(storeObjectId, num == 1);
			PreFormActionResponse result = new PreFormActionResponse();
			if (flag && this.folderId.Equals(storeObjectId))
			{
				switch (this.module)
				{
				case NavigationModule.Mail:
					this.folderId = this.userContext.InboxFolderId;
					this.userContext.LastClientViewState = new MessageModuleViewState(this.userContext.InboxFolderId, "IPF.Note", SecondaryNavigationArea.Special, 1);
					break;
				case NavigationModule.Calendar:
					this.folderId = this.userContext.CalendarFolderId;
					this.userContext.LastClientViewState = new CalendarModuleViewState(this.userContext.CalendarFolderId, "IPF.Appointment", DateTimeUtilities.GetLocalTime().Date);
					break;
				case NavigationModule.Contacts:
					this.folderId = this.userContext.ContactsFolderId;
					this.userContext.LastClientViewState = new ContactModuleViewState(this.userContext.ContactsFolderId, "IPF.Contact", 1);
					break;
				default:
					throw new OwaInvalidRequestException("Invalid module for folder management preformaction");
				}
			}
			if (flag)
			{
				FolderMruCache.DeleteFromCache(storeObjectId, this.userContext);
			}
			return result;
		}

		// Token: 0x0400017A RID: 378
		private const string TypeFolderManagement = "FolderManagement";

		// Token: 0x0400017B RID: 379
		private UserContext userContext;

		// Token: 0x0400017C RID: 380
		private HttpRequest httpRequest;

		// Token: 0x0400017D RID: 381
		private FolderManagementHelper folderManagementHelper;

		// Token: 0x0400017E RID: 382
		private NavigationModule module;

		// Token: 0x0400017F RID: 383
		private StoreObjectId folderId;
	}
}
