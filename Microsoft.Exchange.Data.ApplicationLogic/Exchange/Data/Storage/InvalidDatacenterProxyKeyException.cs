using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200013A RID: 314
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidDatacenterProxyKeyException : LocalizedException
	{
		// Token: 0x06000CE3 RID: 3299 RVA: 0x00035960 File Offset: 0x00033B60
		public InvalidDatacenterProxyKeyException() : base(Strings.DatacenterSecretIsMissingOrInvalid)
		{
		}
	}
}
