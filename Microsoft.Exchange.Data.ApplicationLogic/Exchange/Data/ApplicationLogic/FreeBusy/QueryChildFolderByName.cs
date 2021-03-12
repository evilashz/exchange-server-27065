using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.FreeBusy
{
	// Token: 0x02000136 RID: 310
	internal static class QueryChildFolderByName
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x0003498C File Offset: 0x00032B8C
		public static StoreObjectId Query(Folder parentFolder, string childFolderName)
		{
			StoreObjectId objectId;
			using (QueryResult queryResult = QueryChildFolderByName.CreateQueryResult(parentFolder))
			{
				VersionedId versionedId;
				for (;;)
				{
					object[][] rows = queryResult.GetRows(100);
					if (rows == null || rows.Length == 0)
					{
						break;
					}
					foreach (object[] array2 in rows)
					{
						string text = array2[0] as string;
						versionedId = (array2[1] as VersionedId);
						if (text != null && versionedId != null && StringComparer.InvariantCultureIgnoreCase.Equals(text, childFolderName))
						{
							goto Block_5;
						}
					}
				}
				return null;
				Block_5:
				objectId = versionedId.ObjectId;
			}
			return objectId;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00034A24 File Offset: 0x00032C24
		private static QueryResult CreateQueryResult(Folder folder)
		{
			return folder.FolderQuery(FolderQueryFlags.None, null, null, QueryChildFolderByName.properties);
		}

		// Token: 0x040006B8 RID: 1720
		private const int DisplayNameIndex = 0;

		// Token: 0x040006B9 RID: 1721
		private const int IdIndex = 1;

		// Token: 0x040006BA RID: 1722
		private const int QueryRowBatch = 100;

		// Token: 0x040006BB RID: 1723
		private static readonly PropertyDefinition[] properties = new PropertyDefinition[]
		{
			FolderSchema.DisplayName,
			FolderSchema.Id
		};
	}
}
