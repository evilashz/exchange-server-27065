using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E6E RID: 3694
	internal class AttendeesODataConverter : IODataPropertyValueConverter
	{
		// Token: 0x06006031 RID: 24625 RVA: 0x0012C704 File Offset: 0x0012A904
		public object FromODataPropertyValue(object odataPropertyValue)
		{
			ODataCollectionValue odataCollectionValue = (ODataCollectionValue)odataPropertyValue;
			IEnumerable<Attendee> source = from ODataComplexValue x in odataCollectionValue.Items
			select AttendeeODataConverter.ODataValueToAttendee(x);
			return source.ToArray<Attendee>();
		}

		// Token: 0x06006032 RID: 24626 RVA: 0x0012C754 File Offset: 0x0012A954
		public object ToODataPropertyValue(object rawValue)
		{
			Attendee[] array = ((Attendee[])rawValue) ?? new Attendee[0];
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			odataCollectionValue.TypeName = typeof(Attendee).MakeODataCollectionTypeName();
			odataCollectionValue.Items = Array.ConvertAll<Attendee, ODataValue>(array, (Attendee x) => AttendeeODataConverter.AttendeeToODataValue(x));
			return odataCollectionValue;
		}
	}
}
