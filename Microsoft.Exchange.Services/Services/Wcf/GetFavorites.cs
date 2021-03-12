using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200091B RID: 2331
	internal class GetFavorites : ServiceCommand<GetFavoritesResponse>
	{
		// Token: 0x06004388 RID: 17288 RVA: 0x000E4AEC File Offset: 0x000E2CEC
		public GetFavorites(CallContext context) : base(context)
		{
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x000E4AF8 File Offset: 0x000E2CF8
		protected override GetFavoritesResponse InternalExecute()
		{
			FavoriteFolderCollection favoritesCollection = FavoriteFolderCollection.GetFavoritesCollection(base.MailboxIdentityMailboxSession, FolderTreeDataSection.First);
			return new GetFavoritesResponse
			{
				Favorites = favoritesCollection.FavoriteFolders
			};
		}
	}
}
