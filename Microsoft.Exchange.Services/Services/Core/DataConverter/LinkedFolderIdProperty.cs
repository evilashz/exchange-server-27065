using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200014C RID: 332
	internal sealed class LinkedFolderIdProperty : FolderIdProperty
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x0002C60C File Offset: 0x0002A80C
		private LinkedFolderIdProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0002C615 File Offset: 0x0002A815
		public static LinkedFolderIdProperty Create(CommandContext commandContext)
		{
			return new LinkedFolderIdProperty(commandContext);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0002C620 File Offset: 0x0002A820
		public override void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			byte[] entryId = null;
			if (PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, this.propertyDefinitions[0], out entryId))
			{
				StoreId storeId = StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Folder);
				if (storeId != null && IdConverter.GetAsStoreObjectId(storeId).ProviderLevelItemId.Length > 0)
				{
					ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, idAndSession, null);
					serviceObject[propertyInformation] = this.CreateServiceObjectId(concatenatedId.Id, concatenatedId.ChangeKey);
				}
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0002C6B4 File Offset: 0x0002A8B4
		protected override StoreId GetIdFromObject(StoreObject storeObject)
		{
			StoreId result = null;
			object obj = storeObject.TryGetProperty(this.propertyDefinitions[0]);
			if (obj != null)
			{
				byte[] array = obj as byte[];
				if (array != null)
				{
					result = StoreObjectId.FromProviderSpecificId(array, StoreObjectType.Folder);
				}
			}
			return result;
		}
	}
}
