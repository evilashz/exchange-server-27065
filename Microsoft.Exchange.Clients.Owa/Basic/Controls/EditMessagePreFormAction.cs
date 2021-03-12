using System;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000042 RID: 66
	internal sealed class EditMessagePreFormAction : IPreFormAction
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00010840 File Offset: 0x0000EA40
		public PreFormActionResponse Execute(OwaContext owaContext, out ApplicationElement applicationElement, out string type, out string state, out string action)
		{
			MessageItem messageItem = null;
			PreFormActionResponse result;
			try
			{
				if (owaContext == null)
				{
					throw new ArgumentNullException("owaContext");
				}
				applicationElement = ApplicationElement.NotSet;
				type = string.Empty;
				action = string.Empty;
				state = string.Empty;
				PreFormActionResponse preFormActionResponse = new PreFormActionResponse();
				HttpContext httpContext = owaContext.HttpContext;
				HttpRequest request = httpContext.Request;
				UserContext userContext = owaContext.UserContext;
				if (!Utilities.IsPostRequest(request))
				{
					result = userContext.LastClientViewState.ToPreFormActionResponse();
				}
				else
				{
					string formParameter = Utilities.GetFormParameter(request, "hidid", false);
					string formParameter2 = Utilities.GetFormParameter(request, "hidchk", false);
					if (Utilities.IsPostRequest(request) && !string.IsNullOrEmpty(formParameter) && !string.IsNullOrEmpty(formParameter2))
					{
						messageItem = Utilities.GetItem<MessageItem>(userContext, formParameter, formParameter2, new PropertyDefinition[0]);
					}
					string formParameter3 = Utilities.GetFormParameter(request, "hidcmdpst", false);
					if (!string.IsNullOrEmpty(formParameter3))
					{
						if (messageItem == null)
						{
							messageItem = EditMessageHelper.CreateDraft(userContext);
						}
						string text = null;
						string key;
						switch (key = formParameter3)
						{
						case "snd":
						{
							bool flag = EditMessageHelper.UpdateMessage(messageItem, userContext, request, out text);
							if (!string.IsNullOrEmpty(text))
							{
								EditMessagePreFormAction.RedirectToCompose(owaContext, preFormActionResponse, text, messageItem);
								goto IL_2EA;
							}
							if (!flag && Utilities.GetFormParameter(request, "hidunrslrcp", false) == "1")
							{
								flag = true;
							}
							if (flag)
							{
								EditMessagePreFormAction.RedirectToCompose(owaContext, preFormActionResponse, LocalizedStrings.GetNonEncoded(-2019438132), messageItem);
								goto IL_2EA;
							}
							text = EditMessageHelper.SendMessage(userContext, messageItem);
							if (text != null)
							{
								EditMessagePreFormAction.RedirectToCompose(owaContext, preFormActionResponse, text, messageItem);
								goto IL_2EA;
							}
							userContext.ForceNewSearch = true;
							preFormActionResponse = userContext.LastClientViewState.ToPreFormActionResponse();
							goto IL_2EA;
						}
						case "cls":
							preFormActionResponse = userContext.LastClientViewState.ToPreFormActionResponse();
							goto IL_2EA;
						case "attch":
							EditMessageHelper.UpdateMessage(messageItem, userContext, request, out text);
							if (!string.IsNullOrEmpty(text))
							{
								EditMessagePreFormAction.RedirectToCompose(owaContext, preFormActionResponse, text, messageItem);
								goto IL_2EA;
							}
							EditMessagePreFormAction.RedirectToAttachmentManager(owaContext, preFormActionResponse, messageItem);
							goto IL_2EA;
						case "addrBook":
							EditMessageHelper.UpdateMessage(messageItem, userContext, request, out text);
							if (!string.IsNullOrEmpty(text))
							{
								EditMessagePreFormAction.RedirectToCompose(owaContext, preFormActionResponse, text, messageItem);
								goto IL_2EA;
							}
							preFormActionResponse = EditMessageHelper.RedirectToPeoplePicker(owaContext, messageItem, AddressBook.Mode.EditMessage);
							goto IL_2EA;
						case "viewRcptWhenEdt":
							EditMessageHelper.UpdateMessage(messageItem, userContext, request, out text);
							if (!string.IsNullOrEmpty(text))
							{
								EditMessagePreFormAction.RedirectToCompose(owaContext, preFormActionResponse, text, messageItem);
								goto IL_2EA;
							}
							preFormActionResponse = EditMessageHelper.RedirectToRecipient(owaContext, messageItem, AddressBook.Mode.EditMessage);
							goto IL_2EA;
						case "autoSave":
							EditMessageHelper.UpdateMessage(messageItem, userContext, request, out text);
							if (!string.IsNullOrEmpty(text))
							{
								preFormActionResponse.AddParameter("aserr", "1");
								EditMessagePreFormAction.RedirectToCompose(owaContext, preFormActionResponse, text, messageItem);
								goto IL_2EA;
							}
							preFormActionResponse = this.RedirectToAutoSaveInfo(owaContext, messageItem);
							goto IL_2EA;
						}
						throw new OwaInvalidRequestException("Invalid command form parameter");
					}
					IL_2EA:
					result = preFormActionResponse;
				}
			}
			finally
			{
				if (messageItem != null)
				{
					messageItem.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00010B64 File Offset: 0x0000ED64
		private static void RedirectToCompose(OwaContext owaContext, PreFormActionResponse response, string errorMessage, MessageItem message)
		{
			if (!string.IsNullOrEmpty(errorMessage))
			{
				owaContext[OwaContextProperty.InfobarMessage] = InfobarMessage.CreateText(errorMessage, InfobarMessageType.Error);
			}
			response.ApplicationElement = ApplicationElement.Item;
			response.Type = "IPM.Note";
			response.Action = "Open";
			response.State = "Draft";
			response.AddParameter("id", message.Id.ObjectId.ToBase64String());
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00010BCA File Offset: 0x0000EDCA
		private static void RedirectToAttachmentManager(OwaContext owaContext, PreFormActionResponse response, MessageItem message)
		{
			response.ApplicationElement = ApplicationElement.Dialog;
			response.Type = "Attach";
			response.AddParameter("id", message.Id.ObjectId.ToBase64String());
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00010BFC File Offset: 0x0000EDFC
		private PreFormActionResponse RedirectToAutoSaveInfo(OwaContext owaContext, MessageItem message)
		{
			PreFormActionResponse preFormActionResponse = new PreFormActionResponse();
			preFormActionResponse.ApplicationElement = ApplicationElement.Item;
			preFormActionResponse.Type = "IPM.Note";
			preFormActionResponse.Action = "AutoSaveInfo";
			preFormActionResponse.State = string.Empty;
			preFormActionResponse.AddParameter("id", message.Id.ObjectId.ToBase64String());
			return preFormActionResponse;
		}

		// Token: 0x04000158 RID: 344
		private const string CommandFormParameter = "hidcmdpst";

		// Token: 0x04000159 RID: 345
		private const string MessageIdFormParameter = "hidid";

		// Token: 0x0400015A RID: 346
		private const string ChangeKeyFormParameter = "hidchk";

		// Token: 0x0400015B RID: 347
		private const string UnresolvedRecipientsFormParameter = "hidunrslrcp";

		// Token: 0x0400015C RID: 348
		private const string AutosaveErrorQuerystringParameter = "aserr";
	}
}
