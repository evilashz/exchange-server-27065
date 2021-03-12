using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E41 RID: 3649
	internal class OperationSelector
	{
		// Token: 0x06005DFC RID: 24060 RVA: 0x001248E4 File Offset: 0x00122AE4
		public OperationSelector(ODataContext odataContext)
		{
			ArgumentValidator.ThrowIfNull("odataContext", odataContext);
			this.ODataContext = odataContext;
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06005DFD RID: 24061 RVA: 0x001248FE File Offset: 0x00122AFE
		// (set) Token: 0x06005DFE RID: 24062 RVA: 0x00124906 File Offset: 0x00122B06
		public ODataContext ODataContext { get; private set; }

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06005DFF RID: 24063 RVA: 0x0012490F File Offset: 0x00122B0F
		private ODataPathWrapper ODataPath
		{
			get
			{
				return this.ODataContext.ODataPath;
			}
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x0012491C File Offset: 0x00122B1C
		public ODataRequest SelectOperation()
		{
			ODataRequest odataRequest = null;
			string a;
			if ((a = this.ODataContext.HttpContext.Request.HttpMethod.ToUpper()) != null)
			{
				if (!(a == "GET"))
				{
					if (!(a == "POST"))
					{
						if (!(a == "DELETE"))
						{
							if (a == "PATCH" || a == "MERGE")
							{
								odataRequest = this.SelectPatchOperation();
							}
						}
						else
						{
							odataRequest = this.SelectDeleteOperation();
						}
					}
					else
					{
						odataRequest = this.SelectPostOperation();
					}
				}
				else
				{
					odataRequest = this.SelectGetOperation();
				}
			}
			if (odataRequest == null)
			{
				throw new RequestUnsupportedException();
			}
			return odataRequest;
		}

		// Token: 0x06005E01 RID: 24065 RVA: 0x001249B8 File Offset: 0x00122BB8
		protected ODataRequest SelectGetOperation()
		{
			ODataRequest result = null;
			if (this.ODataContext.EntityType.Equals(User.EdmEntityType))
			{
				if (this.ODataPath.EntitySegment is KeySegment || this.ODataPath.EntitySegment is SingletonSegment)
				{
					result = new GetUserRequest(this.ODataContext);
				}
				else
				{
					result = new FindUsersRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(Folder.EdmEntityType))
			{
				if (this.ODataPath.EntitySegment is KeySegment)
				{
					result = new GetFolderRequest(this.ODataContext);
				}
				else if (this.ODataPath.EntitySegment is NavigationPropertySegment)
				{
					string propertyName = this.ODataPath.EntitySegment.GetPropertyName();
					if (string.Equals(propertyName, FolderSchema.ChildFolders.Name) || string.Equals(propertyName, UserSchema.Folders.Name))
					{
						result = new FindFoldersRequest(this.ODataContext);
					}
					else
					{
						result = new GetFolderRequest(this.ODataContext);
					}
				}
				else
				{
					result = new FindFoldersRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(Message.EdmEntityType))
			{
				if (this.ODataPath.EntitySegment is KeySegment)
				{
					result = new GetMessageRequest(this.ODataContext);
				}
				else
				{
					result = new FindMessagesRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(Attachment.EdmEntityType))
			{
				if (this.ODataPath.EntitySegment is KeySegment && this.ODataPath.GrandParentOfEntitySegment is KeySegment)
				{
					KeySegment keySegment = this.ODataPath.GrandParentOfEntitySegment as KeySegment;
					if (keySegment.EdmType.Equals(Event.EdmEntityType))
					{
						result = new GetEventAttachmentRequest(this.ODataContext);
					}
					else if (keySegment.EdmType.Equals(Contact.EdmEntityType))
					{
						result = new GetContactAttachmentRequest(this.ODataContext);
					}
					else
					{
						result = new GetMessageAttachmentRequest(this.ODataContext);
					}
				}
				else if (this.ODataPath.EntitySegment is NavigationPropertySegment && this.ODataPath.ParentOfEntitySegment is KeySegment)
				{
					KeySegment keySegment2 = this.ODataPath.ParentOfEntitySegment as KeySegment;
					if (keySegment2.EdmType.Equals(Event.EdmEntityType))
					{
						result = new FindEventAttachmentsRequest(this.ODataContext);
					}
					else if (keySegment2.EdmType.Equals(Contact.EdmEntityType))
					{
						result = new FindContactAttachmentsRequest(this.ODataContext);
					}
					else
					{
						result = new FindMessageAttachmentsRequest(this.ODataContext);
					}
				}
			}
			else if (this.ODataContext.EntityType.Equals(Calendar.EdmEntityType))
			{
				string b = (this.ODataPath.EntitySegment is NavigationPropertySegment) ? this.ODataPath.EntitySegment.GetPropertyName() : null;
				if (UserSchema.Calendar.Name == b || EventSchema.Calendar.Name == b)
				{
					result = new GetCalendarRequest(this.ODataContext);
				}
				else if (this.ODataPath.EntitySegment is KeySegment)
				{
					result = new GetCalendarRequest(this.ODataContext);
				}
				else
				{
					result = new FindCalendarsRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(CalendarGroup.EdmEntityType))
			{
				if (this.ODataPath.EntitySegment is KeySegment)
				{
					result = new GetCalendarGroupRequest(this.ODataContext);
				}
				else
				{
					result = new FindCalendarGroupsRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(Event.EdmEntityType))
			{
				if (this.ODataPath.EntitySegment is KeySegment)
				{
					result = new GetEventRequest(this.ODataContext);
				}
				else
				{
					result = new FindEventsRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(Contact.EdmEntityType))
			{
				if (this.ODataPath.EntitySegment is KeySegment)
				{
					result = new GetContactRequest(this.ODataContext);
				}
				else if (this.ODataPath.EntitySegment is NavigationPropertySegment)
				{
					result = new FindContactsRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(ContactFolder.EdmEntityType))
			{
				if (this.ODataPath.EntitySegment is KeySegment)
				{
					result = new GetContactFolderRequest(this.ODataContext);
				}
				else if (this.ODataPath.EntitySegment is NavigationPropertySegment)
				{
					result = new FindContactFoldersRequest(this.ODataContext);
				}
			}
			return result;
		}

		// Token: 0x06005E02 RID: 24066 RVA: 0x00124E50 File Offset: 0x00123050
		protected ODataRequest SelectPostOperation()
		{
			ODataRequest result = null;
			if (this.ODataContext.EntityType.Equals(Folder.EdmEntityType))
			{
				if (this.ODataPath.EntitySegment is OperationSegment)
				{
					string actionName = this.ODataPath.EntitySegment.GetActionName();
					string a;
					if ((a = actionName) != null)
					{
						if (!(a == "Copy"))
						{
							if (a == "Move")
							{
								result = new MoveFolderRequest(this.ODataContext);
							}
						}
						else
						{
							result = new CopyFolderRequest(this.ODataContext);
						}
					}
				}
				else
				{
					result = new CreateFolderRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(Message.EdmEntityType))
			{
				if (this.ODataPath.EntitySegment is OperationSegment)
				{
					string actionName2 = this.ODataPath.EntitySegment.GetActionName();
					string key;
					switch (key = actionName2)
					{
					case "Copy":
						result = new CopyMessageRequest(this.ODataContext);
						break;
					case "Move":
						result = new MoveMessageRequest(this.ODataContext);
						break;
					case "CreateReply":
					case "CreateReplyAll":
					case "CreateForward":
						result = new CreateMessageResponseDraftRequest(this.ODataContext);
						break;
					case "Reply":
					case "ReplyAll":
					case "Forward":
						result = new RespondToMessageRequest(this.ODataContext);
						break;
					case "Send":
						result = new SendMessageRequest(this.ODataContext);
						break;
					}
				}
				else
				{
					result = new CreateMessageRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(Attachment.EdmEntityType) && this.ODataPath.ParentOfEntitySegment is KeySegment)
			{
				KeySegment keySegment = this.ODataPath.ParentOfEntitySegment as KeySegment;
				if (keySegment.EdmType.Equals(Event.EdmEntityType))
				{
					result = new CreateEventAttachmentRequest(this.ODataContext);
				}
				else if (keySegment.EdmType.Equals(Contact.EdmEntityType))
				{
					result = new CreateContactAttachmentRequest(this.ODataContext);
				}
				else
				{
					result = new CreateMessageAttachmentRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(Event.EdmEntityType))
			{
				if (this.ODataPath.EntitySegment is OperationSegment)
				{
					string actionName3 = this.ODataPath.EntitySegment.GetActionName();
					string a2;
					if ((a2 = actionName3) != null && (a2 == "Accept" || a2 == "Decline" || a2 == "TentativelyAccept"))
					{
						result = new RespondToEventRequest(this.ODataContext);
					}
				}
				else
				{
					result = new CreateEventRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(Contact.EdmEntityType))
			{
				result = new CreateContactRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(CalendarGroup.EdmEntityType))
			{
				result = new CreateCalendarGroupRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(Calendar.EdmEntityType))
			{
				result = new CreateCalendarRequest(this.ODataContext);
			}
			return result;
		}

		// Token: 0x06005E03 RID: 24067 RVA: 0x00125200 File Offset: 0x00123400
		protected ODataRequest SelectDeleteOperation()
		{
			ODataRequest result = null;
			if (this.ODataContext.EntityType.Equals(Folder.EdmEntityType))
			{
				result = new DeleteFolderRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(Message.EdmEntityType))
			{
				result = new DeleteMessageRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(Attachment.EdmEntityType) && this.ODataPath.GrandParentOfEntitySegment is KeySegment)
			{
				KeySegment keySegment = this.ODataPath.GrandParentOfEntitySegment as KeySegment;
				if (keySegment.EdmType.Equals(Event.EdmEntityType))
				{
					result = new DeleteEventAttachmentRequest(this.ODataContext);
				}
				else if (keySegment.EdmType.Equals(Contact.EdmEntityType))
				{
					result = new DeleteContactAttachmentRequest(this.ODataContext);
				}
				else
				{
					result = new DeleteMessageAttachmentRequest(this.ODataContext);
				}
			}
			else if (this.ODataContext.EntityType.Equals(Event.EdmEntityType))
			{
				result = new DeleteEventRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(Contact.EdmEntityType))
			{
				result = new DeleteContactRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(CalendarGroup.EdmEntityType))
			{
				result = new DeleteCalendarGroupRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(Calendar.EdmEntityType))
			{
				result = new DeleteCalendarRequest(this.ODataContext);
			}
			return result;
		}

		// Token: 0x06005E04 RID: 24068 RVA: 0x00125384 File Offset: 0x00123584
		protected ODataRequest SelectPatchOperation()
		{
			ODataRequest result = null;
			if (this.ODataContext.EntityType.Equals(Folder.EdmEntityType))
			{
				result = new UpdateFolderRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(Message.EdmEntityType))
			{
				result = new UpdateMessageRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(Event.EdmEntityType))
			{
				result = new UpdateEventRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(Contact.EdmEntityType))
			{
				result = new UpdateContactRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(CalendarGroup.EdmEntityType))
			{
				result = new UpdateCalendarGroupRequest(this.ODataContext);
			}
			else if (this.ODataContext.EntityType.Equals(Calendar.EdmEntityType))
			{
				result = new UpdateCalendarRequest(this.ODataContext);
			}
			return result;
		}
	}
}
