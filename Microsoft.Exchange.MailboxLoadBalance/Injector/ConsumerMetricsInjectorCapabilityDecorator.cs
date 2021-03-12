using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Injector
{
	// Token: 0x0200008E RID: 142
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConsumerMetricsInjectorCapabilityDecorator : MissingCapabilityInjectorClientDecorator
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0000D46A File Offset: 0x0000B66A
		public ConsumerMetricsInjectorCapabilityDecorator(IInjectorService service) : base(service)
		{
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000D47C File Offset: 0x0000B67C
		public override void InjectMoves(Guid targetDatabase, string batchName, IEnumerable<LoadEntity> mailboxes)
		{
			base.InjectMoves(targetDatabase, batchName, from m in mailboxes
			select m.ToSerializationFormat(true));
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000D4A9 File Offset: 0x0000B6A9
		public override void InjectSingleMove(Guid targetDatabase, string batchName, LoadEntity mailbox)
		{
			base.InjectSingleMove(targetDatabase, batchName, mailbox.ToSerializationFormat(true));
		}
	}
}
