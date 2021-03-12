using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000660 RID: 1632
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MessageClassBasedDefaultFolderCreator : DefaultFolderCreator
	{
		// Token: 0x060043AE RID: 17326 RVA: 0x0011EE3B File Offset: 0x0011D03B
		internal MessageClassBasedDefaultFolderCreator(DefaultFolderType container, string containerClass, bool bindByNameIfAlreadyExists = true) : base(container, StoreObjectType.Folder, bindByNameIfAlreadyExists)
		{
			this.containerClass = containerClass;
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x0011EE50 File Offset: 0x0011D050
		internal override Folder Create(DefaultFolderContext context, string folderName, StoreObjectId parentId, out bool hasCreatedNew)
		{
			hasCreatedNew = false;
			bool flag = false;
			Folder folder = null;
			try
			{
				using (Folder folder2 = Folder.Bind(context.Session, parentId))
				{
					using (QueryResult queryResult = folder2.FolderQuery(FolderQueryFlags.None, null, null, MessageClassBasedDefaultFolderCreator.LoadProperties))
					{
						ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ContainerClass, this.containerClass);
						StoreObjectId storeObjectId = null;
						ExDateTime t = ExDateTime.MaxValue;
						while (queryResult.SeekToCondition(SeekReference.OriginCurrent, seekFilter))
						{
							object[][] rows = queryResult.GetRows(1);
							ExDateTime exDateTime = (ExDateTime)rows[0][2];
							if (exDateTime < t)
							{
								storeObjectId = ((VersionedId)rows[0][0]).ObjectId;
								t = exDateTime;
							}
						}
						if (storeObjectId != null)
						{
							folder = Folder.Bind(context.Session, storeObjectId);
						}
					}
				}
				if (folder == null)
				{
					folder = base.Create(context, folderName, parentId, out hasCreatedNew);
					if (hasCreatedNew)
					{
						this.StampExtraPropertiesOnNewlyCreatedFolder(folder);
					}
				}
				flag = true;
			}
			finally
			{
				if (!flag && folder != null)
				{
					folder.Dispose();
					folder = null;
				}
			}
			return folder;
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x0011EF64 File Offset: 0x0011D164
		protected virtual void StampExtraPropertiesOnNewlyCreatedFolder(Folder folder)
		{
		}

		// Token: 0x040024D6 RID: 9430
		private static readonly PropertyDefinition[] LoadProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.ContainerClass,
			StoreObjectSchema.CreationTime
		};

		// Token: 0x040024D7 RID: 9431
		private readonly string containerClass;
	}
}
