using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000103 RID: 259
	internal class DateTimeProperty : SimpleProperty
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x00024499 File Offset: 0x00022699
		protected DateTimeProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000244A2 File Offset: 0x000226A2
		public new static DateTimeProperty CreateCommand(CommandContext commandContext)
		{
			return new DateTimeProperty(commandContext);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000244AA File Offset: 0x000226AA
		protected override string ToString(object propertyValue)
		{
			return ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime((ExDateTime)propertyValue);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000244B8 File Offset: 0x000226B8
		protected override object Parse(string propertyString)
		{
			ExTimeZone exTimeZone = null;
			SetCommandSettings setCommandSettings = this.commandContext.CommandSettings as SetCommandSettings;
			if (setCommandSettings != null)
			{
				exTimeZone = setCommandSettings.StoreObject.Session.ExTimeZone;
			}
			else
			{
				UpdateCommandSettings updateCommandSettings = this.commandContext.CommandSettings as UpdateCommandSettings;
				if (updateCommandSettings != null)
				{
					exTimeZone = updateCommandSettings.StoreObject.Session.ExTimeZone;
				}
			}
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				ExDateTime exDateTime = ExDateTimeConverter.ParseTimeZoneRelated(propertyString, EWSSettings.RequestTimeZone);
				return exDateTime;
			}
			ExDateTime exDateTime2 = ExDateTimeConverter.Parse(propertyString);
			if (exTimeZone == null)
			{
				return exDateTime2;
			}
			return exTimeZone.ConvertDateTime(exDateTime2);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00024558 File Offset: 0x00022758
		protected override void SetProperty(ServiceObject serviceObject, StoreObject storeObject)
		{
			string propertyString = serviceObject[this.commandContext.PropertyInformation] as string;
			object value = this.Parse(propertyString);
			base.SetPropertyValueOnStoreObject(storeObject, this.propertyDefinition, value);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00024594 File Offset: 0x00022794
		protected override void WriteServiceProperty(object propertyValue, ServiceObject serviceObject, PropertyInformation propInfo)
		{
			string propertyValue2 = this.ToString(propertyValue);
			base.WriteServiceProperty(propertyValue2, serviceObject, propInfo);
		}
	}
}
