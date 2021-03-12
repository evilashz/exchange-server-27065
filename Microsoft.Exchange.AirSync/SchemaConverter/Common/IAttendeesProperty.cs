using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200012A RID: 298
	internal interface IAttendeesProperty : IMultivaluedProperty<AttendeeData>, IProperty, IEnumerable<AttendeeData>, IEnumerable
	{
	}
}
