using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200015D RID: 349
	internal sealed class IsClutterProperty : ComplexPropertyBase, IToServiceObjectCommand, IToXmlForPropertyBagCommand, IToServiceObjectForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x0002F873 File Offset: 0x0002DA73
		public IsClutterProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002F87C File Offset: 0x0002DA7C
		public static IsClutterProperty CreateCommand(CommandContext commandContext)
		{
			return new IsClutterProperty(commandContext);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002F884 File Offset: 0x0002DA84
		public void ToXml()
		{
			throw new InvalidOperationException("IsClutterProperty.ToXml should not be called.");
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002F890 File Offset: 0x0002DA90
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("IsClutterProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002F89C File Offset: 0x0002DA9C
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			bool flag;
			if (PropertyCommand.TryGetValueFromPropertyBag<bool>(commandSettings.PropertyBag, ItemSchema.IsClutter, out flag))
			{
				serviceObject.PropertyBag[this.commandContext.PropertyInformation] = flag;
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002F8E8 File Offset: 0x0002DAE8
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ItemResponseShape itemResponseShape = commandSettings.ResponseShape as ItemResponseShape;
			if (!itemResponseShape.InferenceEnabled)
			{
				return;
			}
			StoreObject storeObject = commandSettings.StoreObject;
			if (PropertyCommand.StorePropertyExists(storeObject, ItemSchema.IsClutter))
			{
				object propertyValueFromStoreObject = PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ItemSchema.IsClutter);
				if (propertyValueFromStoreObject != null)
				{
					bool flag = IsClutterProperty.ConvertStoreValueToServiceValue(propertyValueFromStoreObject);
					ServiceObject serviceObject = commandSettings.ServiceObject;
					PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
					serviceObject.PropertyBag[propertyInformation] = flag;
				}
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002F968 File Offset: 0x0002DB68
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			this.SetClutterFlag(commandSettings.ServiceObject, commandSettings.StoreObject);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002F98E File Offset: 0x0002DB8E
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			this.SetClutterFlag(setPropertyUpdate.ServiceObject, updateCommandSettings.StoreObject);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002F9A4 File Offset: 0x0002DBA4
		internal static bool GetFlagValueOrDefaultFromStorePropertyBag(IStorePropertyBag storePropertyBag, ItemResponseShape responseShape)
		{
			if (responseShape.InferenceEnabled)
			{
				object obj = storePropertyBag.TryGetProperty(ItemSchema.IsClutter);
				if (obj != null)
				{
					return IsClutterProperty.ConvertStoreValueToServiceValue(obj);
				}
			}
			return false;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002F9D0 File Offset: 0x0002DBD0
		private static bool ConvertStoreValueToServiceValue(object storeValue)
		{
			return storeValue is bool && (bool)storeValue;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002F9E4 File Offset: 0x0002DBE4
		private void SetClutterFlag(ServiceObject serviceObject, StoreObject storeObject)
		{
			bool valueOrDefault = serviceObject.GetValueOrDefault<bool>(this.commandContext.PropertyInformation);
			base.SetPropertyValueOnStoreObject(storeObject, ItemSchema.IsClutter, valueOrDefault);
			base.SetPropertyValueOnStoreObject(storeObject, MessageItemSchema.IsClutterOverridden, true);
		}
	}
}
