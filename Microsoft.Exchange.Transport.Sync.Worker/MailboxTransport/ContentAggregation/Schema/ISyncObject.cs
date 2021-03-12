using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x0200004F RID: 79
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISyncObject : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000379 RID: 889
		SchemaType Type { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600037A RID: 890
		ExDateTime? LastModifiedTime { get; }
	}
}
