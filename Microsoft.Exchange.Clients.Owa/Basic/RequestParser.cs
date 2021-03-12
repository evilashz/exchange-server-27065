using System;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x020000B4 RID: 180
	internal sealed class RequestParser
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x00035233 File Offset: 0x00033433
		public static int GetIntValueFromQueryString(HttpRequest request, string parameterName)
		{
			return RequestParser.GetIntValueFromRequest(request, parameterName, ParameterIn.QueryString, true, 0);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0003523F File Offset: 0x0003343F
		public static int TryGetIntValueFromQueryString(HttpRequest request, string parameterName, int defaultValue)
		{
			return RequestParser.GetIntValueFromRequest(request, parameterName, ParameterIn.QueryString, false, defaultValue);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0003524B File Offset: 0x0003344B
		public static int GetIntValueFromForm(HttpRequest request, string parameterName)
		{
			return RequestParser.GetIntValueFromRequest(request, parameterName, ParameterIn.Form, true, 0);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00035257 File Offset: 0x00033457
		public static int TryGetIntValueFromForm(HttpRequest request, string parameterName, int defaultValue)
		{
			return RequestParser.GetIntValueFromRequest(request, parameterName, ParameterIn.Form, false, defaultValue);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00035264 File Offset: 0x00033464
		public static int GetIntValueFromRequest(HttpRequest request, string parameterName, ParameterIn parameterIn, bool required, int defaultValue)
		{
			if (request == null)
			{
				throw new ArgumentNullException("HttpRequest");
			}
			if (string.IsNullOrEmpty(parameterName))
			{
				throw new ArgumentException("parameterName is null or empty.");
			}
			string text = null;
			if (parameterIn == ParameterIn.QueryString)
			{
				text = Utilities.GetQueryStringParameter(request, parameterName, required);
				if (text == null)
				{
					return defaultValue;
				}
			}
			else if (parameterIn == ParameterIn.Form)
			{
				text = Utilities.GetFormParameter(request, parameterName, required);
				if (text == null || (!required && string.IsNullOrEmpty(text)))
				{
					return defaultValue;
				}
			}
			int result;
			if (!int.TryParse(text, out result))
			{
				throw new OwaInvalidRequestException(parameterName + " should be a valid number");
			}
			return result;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000352E0 File Offset: 0x000334E0
		public static NavigationModule GetNavigationModuleFromQueryString(HttpRequest request, NavigationModule defaultModule, bool required)
		{
			return (NavigationModule)RequestParser.GetIntValueFromRequest(request, "m", ParameterIn.QueryString, required, (int)defaultModule);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000352F0 File Offset: 0x000334F0
		public static StoreObjectId GetFolderIdFromQueryString(HttpRequest request, bool required)
		{
			return RequestParser.GetStoreObjectId(request, "fid", required, ParameterIn.QueryString);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000352FF File Offset: 0x000334FF
		public static StoreObjectId GetTargetFolderIdFromQueryString(HttpRequest request, bool required)
		{
			return RequestParser.GetStoreObjectId(request, "tfId", required, ParameterIn.Form);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00035310 File Offset: 0x00033510
		public static StoreObjectId GetStoreObjectId(HttpRequest request, string parameterName, bool required, ParameterIn parameterIn)
		{
			string text;
			if (parameterIn == ParameterIn.QueryString)
			{
				text = Utilities.GetQueryStringParameter(request, parameterName, required);
			}
			else
			{
				text = Utilities.GetFormParameter(request, parameterName, required);
			}
			if (text == null)
			{
				return null;
			}
			return Utilities.CreateStoreObjectId(UserContextManager.GetUserContext().MailboxSession, text);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0003534B File Offset: 0x0003354B
		public static StoreObjectId[] GetStoreObjectIdsFromForm(HttpRequest request, bool required)
		{
			return RequestParser.GetStoreObjectIdsFromForm(request, "hidid", required);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0003535C File Offset: 0x0003355C
		public static StoreObjectId[] GetStoreObjectIdsFromForm(HttpRequest request, string parameterName, bool required)
		{
			UserContext userContext = UserContextManager.GetUserContext();
			string formParameter = Utilities.GetFormParameter(request, parameterName, required);
			if (formParameter == null && !required)
			{
				return null;
			}
			string[] array = formParameter.Split(new char[]
			{
				','
			});
			if (userContext.UserOptions.BasicViewRowCount < array.Length)
			{
				throw new OwaInvalidRequestException("According to the user's option, at most " + userContext.UserOptions.BasicViewRowCount + " items are allow to be selected at one time");
			}
			StoreObjectId[] array2 = new StoreObjectId[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = Utilities.CreateStoreObjectId(userContext.MailboxSession, array[i]);
			}
			return array2;
		}

		// Token: 0x0400049C RID: 1180
		public const string FolderIdQueryParameter = "fid";

		// Token: 0x0400049D RID: 1181
		public const string ItemIdQueryParameter = "id";

		// Token: 0x0400049E RID: 1182
		public const string ItemIdFormParameter = "hidid";

		// Token: 0x0400049F RID: 1183
		public const string TargetFolderIdFormParameter = "tfId";

		// Token: 0x040004A0 RID: 1184
		public const string NavigationModuleQueryParameter = "m";

		// Token: 0x040004A1 RID: 1185
		public const string ItemClassNameFormParameter = "hidt";
	}
}
