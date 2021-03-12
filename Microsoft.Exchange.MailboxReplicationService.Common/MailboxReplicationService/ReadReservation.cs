using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000280 RID: 640
	internal class ReadReservation : MailboxReservation
	{
		// Token: 0x06001F98 RID: 8088 RVA: 0x00042A7C File Offset: 0x00040C7C
		protected override IEnumerable<ResourceBase> GetDependentResources()
		{
			if (MapiUtils.FindServerForMdb(base.ResourceId, null, null, FindServerFlags.None).IsOnThisServer)
			{
				yield return DatabaseReadResource.Cache.GetInstance(base.ResourceId, base.WorkloadType);
				yield return LocalServerReadResource.Cache.GetInstance(LocalServerResource.ResourceId, base.WorkloadType);
			}
			if (base.Flags.HasFlag(ReservationFlags.Move))
			{
				yield return MailboxMoveSourceResource.Cache.GetInstance(base.MailboxGuid);
			}
			if (base.Flags.HasFlag(ReservationFlags.Merge))
			{
				yield return MailboxMergeSourceResource.Cache.GetInstance(base.MailboxGuid);
			}
			yield break;
		}
	}
}
