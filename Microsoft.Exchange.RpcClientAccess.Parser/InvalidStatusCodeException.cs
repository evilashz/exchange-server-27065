using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020000A9 RID: 169
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidStatusCodeException : InvalidOperationException
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0000E164 File Offset: 0x0000C364
		public InvalidStatusCodeException(string message, uint statusCode) : base(message)
		{
			this.statusCode = statusCode;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000E174 File Offset: 0x0000C374
		public uint StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x04000273 RID: 627
		private readonly uint statusCode;
	}
}
