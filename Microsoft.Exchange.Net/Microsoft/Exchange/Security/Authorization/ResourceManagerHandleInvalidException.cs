using System;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200002A RID: 42
	internal class ResourceManagerHandleInvalidException : Exception
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00005FF7 File Offset: 0x000041F7
		public ResourceManagerHandleInvalidException(string message) : base(message)
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006000 File Offset: 0x00004200
		public ResourceManagerHandleInvalidException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
