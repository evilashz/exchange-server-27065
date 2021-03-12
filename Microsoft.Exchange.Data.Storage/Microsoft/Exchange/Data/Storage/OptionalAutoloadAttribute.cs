using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C1C RID: 3100
	[AttributeUsage(AttributeTargets.Field)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OptionalAutoloadAttribute : Attribute
	{
	}
}
