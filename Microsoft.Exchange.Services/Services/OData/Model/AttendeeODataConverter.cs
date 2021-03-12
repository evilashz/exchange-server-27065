using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E6D RID: 3693
	internal class AttendeeODataConverter : IODataPropertyValueConverter
	{
		// Token: 0x0600602C RID: 24620 RVA: 0x0012C586 File Offset: 0x0012A786
		public object FromODataPropertyValue(object odataPropertyValue)
		{
			return AttendeeODataConverter.ODataValueToAttendee((ODataComplexValue)odataPropertyValue);
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x0012C593 File Offset: 0x0012A793
		public object ToODataPropertyValue(object rawValue)
		{
			return AttendeeODataConverter.AttendeeToODataValue((Attendee)rawValue);
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x0012C5A0 File Offset: 0x0012A7A0
		internal static ODataValue AttendeeToODataValue(Attendee attendee)
		{
			if (attendee == null)
			{
				return null;
			}
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			odataComplexValue.TypeName = typeof(Attendee).FullName;
			List<ODataProperty> properties = new List<ODataProperty>
			{
				new ODataProperty
				{
					Name = "Name",
					Value = attendee.Name
				},
				new ODataProperty
				{
					Name = "Address",
					Value = attendee.Address
				},
				new ODataProperty
				{
					Name = "Status",
					Value = ResponseStatusODataConverter.ResponseStatusToODataValue(attendee.Status)
				},
				new ODataProperty
				{
					Name = "Type",
					Value = EnumConverter.ToODataEnumValue(attendee.Type)
				}
			};
			odataComplexValue.Properties = properties;
			return odataComplexValue;
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x0012C688 File Offset: 0x0012A888
		internal static Attendee ODataValueToAttendee(ODataComplexValue complexValue)
		{
			if (complexValue == null)
			{
				return null;
			}
			return new Attendee
			{
				Name = complexValue.GetPropertyValue("Name", null),
				Address = complexValue.GetPropertyValue("Address", null),
				Status = ResponseStatusODataConverter.ODataValueToResponseStatus(complexValue.GetPropertyValue("Status", null)),
				Type = EnumConverter.FromODataEnumValue<AttendeeType>(complexValue.GetPropertyValue("Type", null))
			};
		}
	}
}
