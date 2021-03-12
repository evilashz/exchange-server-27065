using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200014B RID: 331
	internal interface IExceptionsProperty : IMultivaluedProperty<ExceptionInstance>, IProperty, IEnumerable<ExceptionInstance>, IEnumerable, IDataObjectGeneratorContainer
	{
	}
}
