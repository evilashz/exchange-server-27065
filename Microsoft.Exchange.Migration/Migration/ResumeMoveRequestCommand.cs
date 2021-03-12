using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000167 RID: 359
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ResumeMoveRequestCommand : MrsAccessorCommand
	{
		// Token: 0x06001165 RID: 4453 RVA: 0x00049709 File Offset: 0x00047909
		public ResumeMoveRequestCommand() : base("Resume-MoveRequest", ResumeMoveRequestCommand.IgnoreExceptionTypes, null)
		{
		}

		// Token: 0x17000536 RID: 1334
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x0004971C File Offset: 0x0004791C
		public bool SuspendWhenReadyToComplete
		{
			set
			{
				base.AddParameter("SuspendWhenReadyToComplete", value);
			}
		}

		// Token: 0x04000607 RID: 1543
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletedPermanentException)
		};
	}
}
