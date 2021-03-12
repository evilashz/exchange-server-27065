using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200004D RID: 77
	internal abstract class TransportAction : Action
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x00010CD8 File Offset: 0x0000EED8
		public TransportAction(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00010CE1 File Offset: 0x0000EEE1
		public virtual TransportActionType Type
		{
			get
			{
				return TransportActionType.NonRecipientRelated;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		internal static ExEventLog Logger
		{
			get
			{
				return TransportAction.logger.Value;
			}
		}

		// Token: 0x040001E3 RID: 483
		private static Lazy<ExEventLog> logger = new Lazy<ExEventLog>(() => new ExEventLog(new Guid("7D2A0005-2C75-42ac-B495-8FE62F3B4FCF"), "MSExchange Messaging Policies"));
	}
}
