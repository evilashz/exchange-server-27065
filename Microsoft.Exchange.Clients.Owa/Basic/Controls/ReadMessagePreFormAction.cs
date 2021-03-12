using System;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200007F RID: 127
	internal sealed class ReadMessagePreFormAction : IPreFormAction
	{
		// Token: 0x0600038B RID: 907 RVA: 0x0002061C File Offset: 0x0001E81C
		public PreFormActionResponse Execute(OwaContext owaContext, out ApplicationElement applicationElement, out string type, out string state, out string action)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			applicationElement = ApplicationElement.NotSet;
			type = string.Empty;
			state = string.Empty;
			action = string.Empty;
			HttpContext httpContext = owaContext.HttpContext;
			UserContext userContext = owaContext.UserContext;
			HttpRequest request = httpContext.Request;
			if (!Utilities.IsPostRequest(request) && owaContext.FormsRegistryContext.Action != "Prev" && owaContext.FormsRegistryContext.Action != "Next")
			{
				return userContext.LastClientViewState.ToPreFormActionResponse();
			}
			string storeObjectId;
			string storeObjectId2;
			if (Utilities.IsPostRequest(request))
			{
				storeObjectId = Utilities.GetFormParameter(request, "hidfldid", true);
				storeObjectId2 = Utilities.GetFormParameter(request, "hidid", true);
			}
			else
			{
				storeObjectId = Utilities.GetQueryStringParameter(request, "fId", true);
				storeObjectId2 = Utilities.GetQueryStringParameter(request, "id", true);
			}
			StoreObjectId folderId = Utilities.CreateStoreObjectId(userContext.MailboxSession, storeObjectId);
			StoreObjectId storeObjectId3 = Utilities.CreateStoreObjectId(userContext.MailboxSession, storeObjectId2);
			ItemOperations.Result result = null;
			string action2;
			if ((action2 = owaContext.FormsRegistryContext.Action) != null)
			{
				if (!(action2 == "Prev"))
				{
					if (!(action2 == "Next"))
					{
						if (!(action2 == "Del"))
						{
							if (!(action2 == "Junk"))
							{
								if (!(action2 == "NotJunk"))
								{
									goto IL_1FA;
								}
								if (!userContext.IsJunkEmailEnabled)
								{
									throw new OwaInvalidRequestException(LocalizedStrings.GetNonEncoded(552277155));
								}
								owaContext[OwaContextProperty.InfobarMessage] = JunkEmailHelper.MarkAsNotJunk(userContext, new StoreObjectId[]
								{
									storeObjectId3
								});
								userContext.ForceNewSearch = true;
							}
							else
							{
								if (!userContext.IsJunkEmailEnabled)
								{
									throw new OwaInvalidRequestException(LocalizedStrings.GetNonEncoded(552277155));
								}
								owaContext[OwaContextProperty.InfobarMessage] = JunkEmailHelper.MarkAsJunk(userContext, new StoreObjectId[]
								{
									storeObjectId3
								});
								userContext.ForceNewSearch = true;
							}
						}
						else
						{
							result = ItemOperations.DeleteItem(userContext, storeObjectId3, folderId);
							userContext.ForceNewSearch = true;
						}
					}
					else
					{
						result = ItemOperations.GetNextViewItem(userContext, ItemOperations.Action.Next, storeObjectId3, folderId);
					}
				}
				else
				{
					result = ItemOperations.GetNextViewItem(userContext, ItemOperations.Action.Prev, storeObjectId3, folderId);
				}
				return ItemOperations.GetPreFormActionResponse(userContext, result);
			}
			IL_1FA:
			throw new OwaInvalidRequestException("Unknown command");
		}

		// Token: 0x040002C2 RID: 706
		private const string QueryStringMessageId = "id";

		// Token: 0x040002C3 RID: 707
		private const string QueryStringFolderId = "fId";

		// Token: 0x040002C4 RID: 708
		private const string FormMessageId = "hidid";

		// Token: 0x040002C5 RID: 709
		private const string FormFolderId = "hidfldid";

		// Token: 0x040002C6 RID: 710
		private const string PreviousItemAction = "Prev";

		// Token: 0x040002C7 RID: 711
		private const string NextItemAction = "Next";

		// Token: 0x040002C8 RID: 712
		private const string DeleteAction = "Del";

		// Token: 0x040002C9 RID: 713
		private const string JunkAction = "Junk";

		// Token: 0x040002CA RID: 714
		private const string NotJunkAction = "NotJunk";
	}
}
