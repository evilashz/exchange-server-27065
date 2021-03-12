using System;
using System.Xml;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E92 RID: 3730
	internal class DateTimePropertyProvider : EwsPropertyProvider
	{
		// Token: 0x06006132 RID: 24882 RVA: 0x0012EF3C File Offset: 0x0012D13C
		public DateTimePropertyProvider(PropertyInformation propertyInformation) : base(propertyInformation)
		{
		}

		// Token: 0x06006133 RID: 24883 RVA: 0x0012EF48 File Offset: 0x0012D148
		protected override void GetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			string valueOrDefault = ewsObject.GetValueOrDefault<string>(base.PropertyInformation);
			if (!string.IsNullOrEmpty(valueOrDefault))
			{
				entity[property] = new DateTimeOffset(DateTime.Parse(valueOrDefault).ToUniversalTime());
				return;
			}
			if (property.EdmType.IsNullable)
			{
				entity[property] = null;
			}
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x0012EFA0 File Offset: 0x0012D1A0
		protected override void SetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			DateTimeOffset left = (DateTimeOffset)entity[property];
			if (left != DateTimeOffset.MinValue)
			{
				ewsObject[base.PropertyInformation] = XmlConvert.ToString(left.UtcDateTime, XmlDateTimeSerializationMode.Utc);
			}
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x0012EFE0 File Offset: 0x0012D1E0
		public override string GetQueryConstant(object value)
		{
			if (value is DateTimeOffset)
			{
				DateTimeOffset value2 = (DateTimeOffset)value;
				return XmlConvert.ToString(value2);
			}
			return null;
		}
	}
}
