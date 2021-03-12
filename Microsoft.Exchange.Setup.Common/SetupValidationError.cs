using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200005A RID: 90
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SetupValidationError : ValidationError
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x0000FA1E File Offset: 0x0000DC1E
		public SetupValidationError(LocalizedString description) : base(description)
		{
		}
	}
}
