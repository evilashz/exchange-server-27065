using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000941 RID: 2369
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FavoritePublicFolder : IFavoritePublicFolder
	{
		// Token: 0x06005841 RID: 22593 RVA: 0x0016AC9C File Offset: 0x00168E9C
		public FavoritePublicFolder(StoreObjectId folderId, string displayName)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			this.FolderId = folderId;
			this.DisplayName = displayName;
		}

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x06005842 RID: 22594 RVA: 0x0016ACCE File Offset: 0x00168ECE
		// (set) Token: 0x06005843 RID: 22595 RVA: 0x0016ACD6 File Offset: 0x00168ED6
		public StoreObjectId FolderId { get; private set; }

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x06005844 RID: 22596 RVA: 0x0016ACDF File Offset: 0x00168EDF
		// (set) Token: 0x06005845 RID: 22597 RVA: 0x0016ACE7 File Offset: 0x00168EE7
		public string DisplayName { get; private set; }

		// Token: 0x06005846 RID: 22598 RVA: 0x0016ACF0 File Offset: 0x00168EF0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				": FolderId=",
				this.FolderId.ToBase64String(),
				", DisplayName=",
				this.DisplayName
			});
		}
	}
}
