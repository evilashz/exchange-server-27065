using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000145 RID: 325
	internal sealed class ClutterCountProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IToXmlForPropertyBagCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x0002BCE3 File Offset: 0x00029EE3
		public ClutterCountProperty(CommandContext commandContext) : base(commandContext)
		{
			this.linkedSearchFolderIdProperty = this.propertyDefinitions[0];
			this.folderCountProperty = this.propertyDefinitions[1];
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002BD08 File Offset: 0x00029F08
		public static ClutterCountProperty CreateCommand(CommandContext commandContext)
		{
			return new ClutterCountProperty(commandContext);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002BD10 File Offset: 0x00029F10
		public void ToXml()
		{
			throw new InvalidOperationException("ClutterCountProperty.ToXml should not be called.");
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002BD1C File Offset: 0x00029F1C
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("ClutterCountProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002BD28 File Offset: 0x00029F28
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			byte[] array = null;
			if (PropertyCommand.TryGetValueFromPropertyBag<byte[]>(commandSettings.PropertyBag, this.linkedSearchFolderIdProperty, out array) && array != null)
			{
				int num = -1;
				if (this.TryGetFolderCount(commandSettings.IdAndSession, array, out num))
				{
					serviceObject.PropertyBag[this.commandContext.PropertyInformation] = num;
				}
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002BD8C File Offset: 0x00029F8C
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			if (PropertyCommand.StorePropertyExists(storeObject, this.linkedSearchFolderIdProperty))
			{
				byte[] array = PropertyCommand.GetPropertyValueFromStoreObject(storeObject, this.linkedSearchFolderIdProperty) as byte[];
				if (array != null)
				{
					int num = -1;
					if (this.TryGetFolderCount(commandSettings.IdAndSession, array, out num))
					{
						ServiceObject serviceObject = commandSettings.ServiceObject;
						serviceObject.PropertyBag[this.commandContext.PropertyInformation] = num;
					}
				}
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0002BE04 File Offset: 0x0002A004
		private bool TryGetFolderCount(IdAndSession idAndSession, byte[] entryId, out int folderCount)
		{
			bool result = false;
			folderCount = 0;
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			if (mailboxSession != null)
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.SearchFolders);
				using (Folder folder = Folder.Bind(mailboxSession, defaultFolderId))
				{
					List<object[]> list = new List<object[]>();
					QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.EntryId, entryId);
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, queryFilter, null, new PropertyDefinition[]
					{
						this.folderCountProperty
					}))
					{
						list = SearchUtil.FetchRowsFromQueryResult(queryResult, 10000);
						if (list.Count > 0)
						{
							folderCount = (int)list[0][0];
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x04000777 RID: 1911
		private readonly PropertyDefinition linkedSearchFolderIdProperty;

		// Token: 0x04000778 RID: 1912
		private readonly PropertyDefinition folderCountProperty;
	}
}
