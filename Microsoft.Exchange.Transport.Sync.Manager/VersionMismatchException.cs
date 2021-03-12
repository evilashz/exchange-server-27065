using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class VersionMismatchException : SerializationException
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002550 File Offset: 0x00000750
		public VersionMismatchException(string message) : base(message)
		{
		}
	}
}
