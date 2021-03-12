using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001BF RID: 447
	internal sealed class UpdatePropertyList : PropertyList
	{
		// Token: 0x06000C48 RID: 3144 RVA: 0x0003DA43 File Offset: 0x0003BC43
		public UpdatePropertyList(Shape shape, PropertyUpdate[] propertyUpdates, StoreObject storeObject, IdConverter idConverter, bool suppressReadReceipts, IFeaturesManager featuresManager) : base(shape)
		{
			this.propertyUpdates = propertyUpdates;
			this.storeObject = storeObject;
			this.CreatePropertyList(idConverter, suppressReadReceipts, featuresManager);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0003DA68 File Offset: 0x0003BC68
		private static void ValidateUpdate(PropertyUpdate propertyUpdate, PropertyInformation propertyInformation)
		{
			SetPropertyUpdate setPropertyUpdate = null;
			AppendPropertyUpdate setPropertyUpdate2 = null;
			DeletePropertyUpdate deletePropertyUpdate = null;
			if (UpdatePropertyList.TryGetPropertyUpdate<AppendPropertyUpdate>(propertyUpdate, out setPropertyUpdate2))
			{
				if (propertyInformation.ImplementsAppendUpdateCommand)
				{
					UpdatePropertyList.ValidatePropertyUpdate(setPropertyUpdate2, propertyInformation);
					return;
				}
				throw new InvalidPropertyAppendException(propertyInformation.PropertyPath);
			}
			else if (UpdatePropertyList.TryGetPropertyUpdate<SetPropertyUpdate>(propertyUpdate, out setPropertyUpdate))
			{
				if (propertyInformation.ImplementsSetUpdateCommand)
				{
					UpdatePropertyList.ValidatePropertyUpdate(setPropertyUpdate, propertyInformation);
					return;
				}
				throw new InvalidPropertySetException(propertyInformation.PropertyPath);
			}
			else
			{
				if (UpdatePropertyList.TryGetPropertyUpdate<DeletePropertyUpdate>(propertyUpdate, out deletePropertyUpdate) && !propertyInformation.ImplementsDeleteUpdateCommand)
				{
					throw new InvalidPropertyDeleteException(propertyInformation.PropertyPath);
				}
				return;
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0003DAE8 File Offset: 0x0003BCE8
		private static void ValidatePropertyUpdate(SetPropertyUpdate setPropertyUpdate, PropertyInformation propertyInformation)
		{
			ServiceObject serviceObject = setPropertyUpdate.ServiceObject;
			List<PropertyInformation> loadedProperties = serviceObject.LoadedProperties;
			if (loadedProperties.Count != 1)
			{
				throw new IncorrectUpdatePropertyCountException();
			}
			if (string.CompareOrdinal(loadedProperties[0].LocalName, propertyInformation.LocalName) != 0)
			{
				throw new UpdatePropertyMismatchException(setPropertyUpdate.PropertyPath);
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0003DB38 File Offset: 0x0003BD38
		private void CreatePropertyList(IdConverter idConverter, bool suppressReadReceipts, IFeaturesManager featuresManager)
		{
			this.commandContexts = new List<CommandContext>();
			foreach (PropertyUpdate propertyUpdate in this.propertyUpdates)
			{
				PropertyInformation propertyInformation = null;
				if (!this.shape.TryGetPropertyInformation(propertyUpdate.PropertyPath, out propertyInformation))
				{
					throw new InvalidPropertyRequestException(propertyUpdate.PropertyPath);
				}
				UpdatePropertyList.ValidateUpdate(propertyUpdate, propertyInformation);
				this.commandContexts.Add(new CommandContext(new UpdateCommandSettings(propertyUpdate, this.storeObject, suppressReadReceipts, featuresManager), propertyInformation, idConverter));
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0003DBB5 File Offset: 0x0003BDB5
		public static bool TryGetPropertyUpdate<T>(PropertyUpdate propertyUpdate, out T typedPropertyUpdate) where T : PropertyUpdate
		{
			typedPropertyUpdate = (propertyUpdate as T);
			return typedPropertyUpdate != null;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0003DBDC File Offset: 0x0003BDDC
		public IList<IUpdateCommand> CreatePropertyCommands()
		{
			List<IUpdateCommand> list = new List<IUpdateCommand>();
			foreach (CommandContext commandContext in this.commandContexts)
			{
				if (!(this.storeObject is CalendarItemBase) || commandContext.PropertyInformation != ItemSchema.MimeContent)
				{
					list.Add((IUpdateCommand)commandContext.PropertyInformation.CreatePropertyCommand(commandContext));
				}
			}
			return list;
		}

		// Token: 0x0400097B RID: 2427
		private List<CommandContext> commandContexts;

		// Token: 0x0400097C RID: 2428
		private PropertyUpdate[] propertyUpdates;

		// Token: 0x0400097D RID: 2429
		private StoreObject storeObject;
	}
}
