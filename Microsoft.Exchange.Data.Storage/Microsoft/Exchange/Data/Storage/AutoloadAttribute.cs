using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C1B RID: 3099
	[AttributeUsage(AttributeTargets.Field)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AutoloadAttribute : Attribute
	{
	}
}
