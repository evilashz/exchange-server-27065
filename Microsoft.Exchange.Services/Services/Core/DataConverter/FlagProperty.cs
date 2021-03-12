using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000156 RID: 342
	internal sealed class FlagProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x0002DD99 File Offset: 0x0002BF99
		public FlagProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0002DDA2 File Offset: 0x0002BFA2
		public static FlagProperty CreateCommand(CommandContext commandContext)
		{
			return new FlagProperty(commandContext);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002DDAA File Offset: 0x0002BFAA
		public void ToXml()
		{
			throw new InvalidOperationException("FlagProperty.ToXml should not be called.");
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0002DDB6 File Offset: 0x0002BFB6
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("FlagProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0002DDC4 File Offset: 0x0002BFC4
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			Item storeItem = (Item)commandSettings.StoreObject;
			serviceObject[this.commandContext.PropertyInformation] = FlagProperty.GetFlag(storeItem);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0002DE04 File Offset: 0x0002C004
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			serviceObject[this.commandContext.PropertyInformation] = FlagProperty.GetFlag(propertyBag);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0002DE40 File Offset: 0x0002C040
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			Item item = (Item)commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			this.SetFlag(serviceObject, item);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0002DE70 File Offset: 0x0002C070
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			Item item = (Item)updateCommandSettings.StoreObject;
			this.SetFlag(setPropertyUpdate.ServiceObject, item);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0002DE98 File Offset: 0x0002C098
		internal static FlagType GetFlagForItemPart(ItemPart itemPart)
		{
			FlagStatus valueOrDefault = itemPart.StorePropertyBag.GetValueOrDefault<FlagStatus>(ItemSchema.FlagStatus, FlagStatus.NotFlagged);
			FlagType flagType = new FlagType
			{
				FlagStatus = valueOrDefault
			};
			ExDateTime systemDateTime;
			if (flagType.FlagStatus == FlagStatus.Flagged)
			{
				if (FlagProperty.TryGetDateTimeProperty(itemPart, TaskSchema.StartDate, out systemDateTime))
				{
					flagType.StartDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime);
				}
				if (FlagProperty.TryGetDateTimeProperty(itemPart, TaskSchema.DueDate, out systemDateTime))
				{
					flagType.DueDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime);
				}
			}
			else if (flagType.FlagStatus == FlagStatus.Complete && FlagProperty.TryGetDateTimeProperty(itemPart, ItemSchema.CompleteDate, out systemDateTime))
			{
				flagType.CompleteDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime);
			}
			return flagType;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0002DF2C File Offset: 0x0002C12C
		private static FlagType GetFlag(IDictionary<PropertyDefinition, object> propertyBag)
		{
			FlagType flagType = new FlagType();
			FlagStatus flagStatus;
			if (PropertyCommand.TryGetValueFromPropertyBag<FlagStatus>(propertyBag, ItemSchema.FlagStatus, out flagStatus))
			{
				flagType.FlagStatus = flagStatus;
				ExDateTime systemDateTime;
				if (flagType.FlagStatus == FlagStatus.Flagged)
				{
					if (PropertyCommand.TryGetValueFromPropertyBag<ExDateTime>(propertyBag, TaskSchema.StartDate, out systemDateTime))
					{
						flagType.StartDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime);
					}
					if (PropertyCommand.TryGetValueFromPropertyBag<ExDateTime>(propertyBag, TaskSchema.DueDate, out systemDateTime))
					{
						flagType.DueDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime);
					}
				}
				if (flagType.FlagStatus == FlagStatus.Complete && PropertyCommand.TryGetValueFromPropertyBag<ExDateTime>(propertyBag, ItemSchema.CompleteDate, out systemDateTime))
				{
					flagType.CompleteDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(systemDateTime);
				}
			}
			return flagType;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0002DFB9 File Offset: 0x0002C1B9
		private static bool TryGetDateTimeProperty(ItemPart itemPart, PropertyDefinition propDef, out ExDateTime exDate)
		{
			exDate = itemPart.StorePropertyBag.GetValueOrDefault<ExDateTime>(propDef, ExDateTime.MaxValue);
			return exDate != ExDateTime.MaxValue;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0002DFE4 File Offset: 0x0002C1E4
		private static FlagType GetFlag(Item storeItem)
		{
			FlagType flagType = new FlagType();
			object obj = null;
			if (FlagProperty.TryGetProperty(storeItem, ItemSchema.FlagStatus, out obj))
			{
				flagType.FlagStatus = (FlagStatus)obj;
				if (flagType.FlagStatus == FlagStatus.Flagged)
				{
					if (FlagProperty.TryGetProperty(storeItem, TaskSchema.StartDate, out obj))
					{
						flagType.StartDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime((ExDateTime)obj);
					}
					if (FlagProperty.TryGetProperty(storeItem, TaskSchema.DueDate, out obj))
					{
						flagType.DueDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime((ExDateTime)obj);
					}
				}
				else if (flagType.FlagStatus == FlagStatus.Complete && FlagProperty.TryGetProperty(storeItem, ItemSchema.CompleteDate, out obj))
				{
					flagType.CompleteDate = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime((ExDateTime)obj);
				}
			}
			return flagType;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002E08C File Offset: 0x0002C28C
		private static bool TryGetProperty(StoreObject storeObject, PropertyDefinition propDef, out object value)
		{
			object obj = storeObject.TryGetProperty(propDef);
			if (obj is PropertyError)
			{
				value = null;
				return false;
			}
			value = obj;
			return true;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
		private void SetFlag(ServiceObject serviceObject, Item item)
		{
			FlagType valueOrDefault = serviceObject.GetValueOrDefault<FlagType>(this.commandContext.PropertyInformation);
			if (valueOrDefault != null)
			{
				if (valueOrDefault.FlagStatus == FlagStatus.Flagged)
				{
					ExDateTime? startDate = null;
					ExDateTime? dueDate = null;
					if ((valueOrDefault.StartDate == null && valueOrDefault.DueDate != null) || (valueOrDefault.StartDate != null && valueOrDefault.DueDate == null) || valueOrDefault.CompleteDate != null)
					{
						throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
					}
					if (valueOrDefault.StartDate != null && valueOrDefault.DueDate != null)
					{
						startDate = new ExDateTime?(ExDateTimeConverter.ParseTimeZoneRelated(valueOrDefault.StartDate, EWSSettings.RequestTimeZone));
						dueDate = new ExDateTime?(ExDateTimeConverter.ParseTimeZoneRelated(valueOrDefault.DueDate, EWSSettings.RequestTimeZone));
					}
					item.SetFlag(CoreResources.FlagForFollowUp, startDate, dueDate);
					return;
				}
				else if (valueOrDefault.FlagStatus == FlagStatus.Complete)
				{
					ExDateTime? completeTime = null;
					if (valueOrDefault.StartDate != null || valueOrDefault.DueDate != null)
					{
						throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
					}
					if (valueOrDefault.CompleteDate != null)
					{
						completeTime = new ExDateTime?(ExDateTimeConverter.ParseTimeZoneRelated(valueOrDefault.CompleteDate, EWSSettings.RequestTimeZone));
					}
					item.CompleteFlag(completeTime);
					return;
				}
				else
				{
					if (valueOrDefault.StartDate != null || valueOrDefault.DueDate != null || valueOrDefault.CompleteDate != null)
					{
						throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
					}
					item.ClearFlag();
				}
			}
		}
	}
}
