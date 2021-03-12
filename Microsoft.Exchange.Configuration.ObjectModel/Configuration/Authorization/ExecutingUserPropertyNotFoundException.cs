using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000229 RID: 553
	internal class ExecutingUserPropertyNotFoundException : Exception
	{
		// Token: 0x060013B8 RID: 5048 RVA: 0x00045ED6 File Offset: 0x000440D6
		public ExecutingUserPropertyNotFoundException(string propertyName) : base(Strings.ExecutingUserPropertyNotFound(propertyName))
		{
		}
	}
}
