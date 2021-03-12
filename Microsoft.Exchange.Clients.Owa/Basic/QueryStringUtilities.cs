using System;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x020000B1 RID: 177
	internal static class QueryStringUtilities
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x0003465C File Offset: 0x0003285C
		public static StoreObjectId CreateStoreObjectId(MailboxSession mailboxSession, HttpRequest httpRequest, string idParameter, bool required)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(httpRequest, idParameter, required);
			if (string.IsNullOrEmpty(queryStringParameter))
			{
				return null;
			}
			return Utilities.CreateStoreObjectId(mailboxSession, queryStringParameter);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00034683 File Offset: 0x00032883
		public static StoreObjectId CreateItemStoreObjectId(MailboxSession mailboxSession, HttpRequest httpRequest, bool required)
		{
			return QueryStringUtilities.CreateStoreObjectId(mailboxSession, httpRequest, "id", required);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00034692 File Offset: 0x00032892
		public static StoreObjectId CreateItemStoreObjectId(MailboxSession mailboxSession, HttpRequest httpRequest)
		{
			return QueryStringUtilities.CreateStoreObjectId(mailboxSession, httpRequest, "id", true);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000346A1 File Offset: 0x000328A1
		public static StoreObjectId CreateFolderStoreObjectId(MailboxSession mailboxSession, HttpRequest httpRequest, bool required)
		{
			return QueryStringUtilities.CreateStoreObjectId(mailboxSession, httpRequest, "id", required);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000346B0 File Offset: 0x000328B0
		public static StoreObjectId CreateFolderStoreObjectId(MailboxSession mailboxSession, HttpRequest httpRequest)
		{
			return QueryStringUtilities.CreateStoreObjectId(mailboxSession, httpRequest, "id", true);
		}

		// Token: 0x04000493 RID: 1171
		public const string ItemMainIdParameter = "id";

		// Token: 0x04000494 RID: 1172
		public const string FolderMainIdParameter = "id";

		// Token: 0x04000495 RID: 1173
		public const string OwnerItemIdParameter = "oId";

		// Token: 0x04000496 RID: 1174
		public const string OwnerItemChangeKeyParameter = "oCk";

		// Token: 0x04000497 RID: 1175
		public const string OwnerItemTypeParameter = "oT";

		// Token: 0x04000498 RID: 1176
		public const string OwnerItemStateParameter = "oS";
	}
}
