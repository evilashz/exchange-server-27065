using System;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004AF RID: 1199
	[OwaEventNamespace("EditPost")]
	internal sealed class EditPostEventHandler : ItemEventHandler
	{
		// Token: 0x06002DFC RID: 11772 RVA: 0x00105501 File Offset: 0x00103701
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(EditPostEventHandler));
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x00105514 File Offset: 0x00103714
		[OwaEventParameter("Body", typeof(string), false, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, false)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("Text", typeof(bool), false, false)]
		[OwaEvent("Post")]
		[OwaEventParameter("Subj", typeof(string), false, true)]
		[OwaEventParameter("Imp", typeof(Importance), false, true)]
		[OwaEventParameter("CK", typeof(string), false, true)]
		public void Post()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "EditPostEventHandler.Post");
			PostItem postItem = null;
			bool flag = base.IsParameterSet("Id");
			try
			{
				OwaStoreObjectId owaStoreObjectId = base.GetParameter("fId") as OwaStoreObjectId;
				if (owaStoreObjectId == null)
				{
					throw new OwaInvalidRequestException("FolderID can not be null. ");
				}
				if (flag)
				{
					postItem = base.GetRequestItem<PostItem>(new PropertyDefinition[0]);
				}
				else
				{
					postItem = Utilities.CreateItem<PostItem>(owaStoreObjectId);
					if (Globals.ArePerfCountersEnabled)
					{
						OwaSingleCounters.ItemsCreated.Increment();
					}
				}
				ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "EditPostEventHandler::Post");
				object parameter = base.GetParameter("Subj");
				if (parameter != null)
				{
					string subject = (string)parameter;
					try
					{
						postItem.Subject = subject;
						string text = postItem.TryGetProperty(ItemSchema.ConversationTopic) as string;
						bool flag2 = text == null;
						if (!flag || flag2)
						{
							postItem.ConversationTopic = postItem.Subject;
						}
					}
					catch (PropertyValidationException ex)
					{
						throw new OwaInvalidRequestException(ex.Message, ex);
					}
				}
				parameter = base.GetParameter("Imp");
				if (parameter != null)
				{
					postItem.Importance = (Importance)parameter;
				}
				parameter = base.GetParameter("Body");
				object parameter2 = base.GetParameter("Text");
				if (parameter != null && parameter2 != null)
				{
					Markup markup = ((bool)parameter2) ? Markup.PlainText : Markup.Html;
					BodyConversionUtilities.SetBody(postItem, (string)parameter, markup, StoreObjectType.Post, base.UserContext);
				}
				postItem[MessageItemSchema.IsDraft] = false;
				Utilities.SetPostSender(postItem, base.UserContext, owaStoreObjectId.IsPublic);
				Utilities.SaveItem(postItem, flag);
				if (flag)
				{
					base.MoveItemToDestinationFolderIfInScratchPad(postItem);
				}
			}
			finally
			{
				if (postItem != null)
				{
					postItem.Dispose();
					postItem = null;
				}
			}
		}

		// Token: 0x04001F3A RID: 7994
		public const string EventNamespace = "EditPost";

		// Token: 0x04001F3B RID: 7995
		public const string Subject = "Subj";

		// Token: 0x04001F3C RID: 7996
		public const string Importance = "Imp";

		// Token: 0x04001F3D RID: 7997
		public const string Body = "Body";

		// Token: 0x04001F3E RID: 7998
		public const string Text = "Text";

		// Token: 0x04001F3F RID: 7999
		public const string MethodPost = "Post";
	}
}
