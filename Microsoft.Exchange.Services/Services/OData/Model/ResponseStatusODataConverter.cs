using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E7B RID: 3707
	internal class ResponseStatusODataConverter : IODataPropertyValueConverter
	{
		// Token: 0x06006068 RID: 24680 RVA: 0x0012D002 File Offset: 0x0012B202
		public object FromODataPropertyValue(object odataPropertyValue)
		{
			return ResponseStatusODataConverter.ODataValueToResponseStatus((ODataComplexValue)odataPropertyValue);
		}

		// Token: 0x06006069 RID: 24681 RVA: 0x0012D00F File Offset: 0x0012B20F
		public object ToODataPropertyValue(object rawValue)
		{
			return ResponseStatusODataConverter.ResponseStatusToODataValue((ResponseStatus)rawValue);
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x0012D01C File Offset: 0x0012B21C
		internal static ODataValue ResponseStatusToODataValue(ResponseStatus responseStatus)
		{
			if (responseStatus == null)
			{
				return null;
			}
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			odataComplexValue.TypeName = typeof(ResponseStatus).FullName;
			List<ODataProperty> properties = new List<ODataProperty>
			{
				new ODataProperty
				{
					Name = "Response",
					Value = EnumConverter.ToODataEnumValue(responseStatus.Response)
				},
				new ODataProperty
				{
					Name = "Time",
					Value = responseStatus.Time
				}
			};
			odataComplexValue.Properties = properties;
			return odataComplexValue;
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x0012D0B4 File Offset: 0x0012B2B4
		internal static ResponseStatus ODataValueToResponseStatus(ODataComplexValue complexValue)
		{
			if (complexValue == null)
			{
				return null;
			}
			return new ResponseStatus
			{
				Response = EnumConverter.FromODataEnumValue<ResponseType>(complexValue.GetPropertyValue("Response", null)),
				Time = complexValue.GetPropertyValue("Time", default(DateTimeOffset))
			};
		}
	}
}
