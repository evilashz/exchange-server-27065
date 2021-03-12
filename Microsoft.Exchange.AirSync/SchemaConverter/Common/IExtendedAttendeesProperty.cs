using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000152 RID: 338
	internal interface IExtendedAttendeesProperty : IMultivaluedProperty<ExtendedAttendeeData>, IProperty, IEnumerable<ExtendedAttendeeData>, IEnumerable
	{
	}
}
