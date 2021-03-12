using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200028B RID: 651
	internal class WriteReservation : MailboxReservation
	{
		// Token: 0x06001FFF RID: 8191 RVA: 0x00043F38 File Offset: 0x00042138
		protected override IEnumerable<ResourceBase> GetDependentResources()
		{
			if (MapiUtils.FindServerForMdb(base.ResourceId, null, null, FindServerFlags.None).IsOnThisServer)
			{
				yield return DatabaseWriteResource.Cache.GetInstance(base.ResourceId, base.WorkloadType);
				yield return LocalServerWriteResource.Cache.GetInstance(LocalServerResource.ResourceId, base.WorkloadType);
			}
			if (base.Flags.HasFlag(ReservationFlags.Move))
			{
				yield return MailboxMoveTargetResource.Cache.GetInstance(base.MailboxGuid);
			}
			if (base.Flags.HasFlag(ReservationFlags.Merge))
			{
				yield return MailboxMergeTargetResource.Cache.GetInstance(base.MailboxGuid);
			}
			yield break;
		}
	}
}
