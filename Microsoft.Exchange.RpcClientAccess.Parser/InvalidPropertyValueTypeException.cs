using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020000A8 RID: 168
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidPropertyValueTypeException : ArgumentException
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0000E15A File Offset: 0x0000C35A
		public InvalidPropertyValueTypeException(string message, string propertyName) : base(message, propertyName)
		{
		}
	}
}
