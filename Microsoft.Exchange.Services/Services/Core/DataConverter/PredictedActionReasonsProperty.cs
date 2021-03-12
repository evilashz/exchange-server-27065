using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000118 RID: 280
	internal sealed class PredictedActionReasonsProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x00027995 File Offset: 0x00025B95
		public PredictedActionReasonsProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002799E File Offset: 0x00025B9E
		public static PredictedActionReasonsProperty CreateCommand(CommandContext commandContext)
		{
			return new PredictedActionReasonsProperty(commandContext);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000279A6 File Offset: 0x00025BA6
		public void ToXml()
		{
			throw new InvalidOperationException("PredictedActionReasonsProperty.ToXml should not be called.");
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000279B2 File Offset: 0x00025BB2
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("PredictedActionReasonsProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000279C0 File Offset: 0x00025BC0
		public void ToServiceObject()
		{
			PredictedActionReasonType[] array = new PredictedActionReasonType[1];
			PredictedActionReasonType[] value = array;
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			if (PropertyCommand.StorePropertyExists(storeObject, ItemSchema.IsClutter))
			{
				bool flag = (bool)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ItemSchema.IsClutter);
				if (flag && PropertyCommand.StorePropertyExists(storeObject, ItemSchema.InferencePredictedDeleteReasons))
				{
					byte[] rawPredictedActionReasonsArray = (byte[])PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ItemSchema.InferencePredictedDeleteReasons);
					value = PredictedActionReasonsProperty.ConvertStoreValueToServiceValue(rawPredictedActionReasonsArray);
				}
			}
			serviceObject[this.commandContext.PropertyInformation] = value;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00027A4C File Offset: 0x00025C4C
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			PredictedActionReasonType[] array = new PredictedActionReasonType[1];
			PredictedActionReasonType[] value = array;
			object obj;
			if (propertyBag.TryGetValue(ItemSchema.IsClutter, out obj) && obj is bool)
			{
				bool flag = (bool)obj;
				object obj2 = null;
				if (flag && propertyBag.TryGetValue(ItemSchema.InferencePredictedDeleteReasons, out obj2))
				{
					value = PredictedActionReasonsProperty.ConvertStoreValueToServiceValue((byte[])obj2);
				}
			}
			serviceObject[this.commandContext.PropertyInformation] = value;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00027AD0 File Offset: 0x00025CD0
		internal static PredictedActionReasonType[] ToServiceObjectForStorePropertyBag(IStorePropertyBag storePropertyBag)
		{
			bool valueOrDefault = storePropertyBag.GetValueOrDefault<bool>(ItemSchema.IsClutter, false);
			if (valueOrDefault)
			{
				try
				{
					object valueOrDefault2 = storePropertyBag.GetValueOrDefault<object>(ItemSchema.InferencePredictedDeleteReasons, null);
					if (valueOrDefault2 != null)
					{
						return PredictedActionReasonsProperty.ConvertStoreValueToServiceValue(valueOrDefault2 as byte[]);
					}
				}
				catch (NotInBagPropertyErrorException)
				{
				}
			}
			return null;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00027B24 File Offset: 0x00025D24
		internal static PredictedActionReasonType[] ConvertStoreValueToServiceValue(byte[] rawPredictedActionReasonsArray)
		{
			PredictedActionReasonType[] array = new PredictedActionReasonType[1];
			PredictedActionReasonType[] array2 = array;
			if (rawPredictedActionReasonsArray == null || rawPredictedActionReasonsArray.Length <= 3)
			{
				return array2;
			}
			ushort[] predictedActionReasonsArray = PredictedActionReasonsProperty.GetPredictedActionReasonsArray(rawPredictedActionReasonsArray);
			if (predictedActionReasonsArray != null && predictedActionReasonsArray.Length > 0)
			{
				array2 = new PredictedActionReasonType[predictedActionReasonsArray.Length];
				for (int i = 0; i < predictedActionReasonsArray.Length; i++)
				{
					array2[i] = PredictedActionReasonConverter.ConvertToServiceObjectValue(predictedActionReasonsArray[i]);
				}
			}
			return array2;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00027B78 File Offset: 0x00025D78
		private static ushort[] GetPredictedActionReasonsArray(byte[] rawPredictedActionReasonsArray)
		{
			if (rawPredictedActionReasonsArray == null || rawPredictedActionReasonsArray.Length <= 3)
			{
				return null;
			}
			ushort[] array = new ushort[(rawPredictedActionReasonsArray.Length - 2) / 2];
			int num = 0;
			byte[] array2 = new byte[2];
			for (int i = 2; i < rawPredictedActionReasonsArray.Length; i += 2)
			{
				array2[0] = rawPredictedActionReasonsArray[i];
				array2[1] = rawPredictedActionReasonsArray[i + 1];
				array[num] = BitConverter.ToUInt16(array2, 0);
				num++;
			}
			return array;
		}
	}
}
