using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000227 RID: 551
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SyncFailedDependencyException : Exception
	{
		// Token: 0x060013C1 RID: 5057 RVA: 0x000431B5 File Offset: 0x000413B5
		public SyncFailedDependencyException(string message) : this(message, null)
		{
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000431BF File Offset: 0x000413BF
		public SyncFailedDependencyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000431C9 File Offset: 0x000413C9
		public SyncFailedDependencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
