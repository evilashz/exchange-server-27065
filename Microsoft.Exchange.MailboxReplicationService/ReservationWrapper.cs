using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000A9 RID: 169
	internal class ReservationWrapper : WrapperBase<IReservation>, IReservation, IDisposable
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x0003B44A File Offset: 0x0003964A
		public ReservationWrapper(IReservation reservation) : base(reservation, null)
		{
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0003B454 File Offset: 0x00039654
		Guid IReservation.Id
		{
			get
			{
				return base.WrappedObject.Id;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0003B461 File Offset: 0x00039661
		ReservationFlags IReservation.Flags
		{
			get
			{
				return base.WrappedObject.Flags;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0003B46E File Offset: 0x0003966E
		Guid IReservation.ResourceId
		{
			get
			{
				return base.WrappedObject.ResourceId;
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0003B4F4 File Offset: 0x000396F4
		public static IReservation CreateReservation(string serverFQDN, NetworkCredential credentials, Guid mailboxGuid, TenantPartitionHint partitionHint, Guid resourceId, ReservationFlags flags)
		{
			if (serverFQDN == null)
			{
				TestIntegration testIntegration = new TestIntegration(false);
				if ((flags.HasFlag(ReservationFlags.Read) && testIntegration.UseRemoteForSource) || (flags.HasFlag(ReservationFlags.Write) && testIntegration.UseRemoteForDestination))
				{
					serverFQDN = CommonUtils.LocalComputerName;
				}
			}
			IReservation result = null;
			ExecutionContext.Create(new DataContext[]
			{
				new OperationDataContext("IReservationManager.ReserveResources", OperationType.None),
				new SimpleValueDataContext("MailboxGuid", mailboxGuid),
				new SimpleValueDataContext("ResourceId", resourceId),
				new SimpleValueDataContext("Flags", flags)
			}).Execute(delegate
			{
				if (serverFQDN == null)
				{
					result = ReservationManager.CreateReservation(mailboxGuid, partitionHint, resourceId, flags, CommonUtils.LocalComputerName);
					return;
				}
				result = RemoteReservation.Create(serverFQDN, credentials, mailboxGuid, partitionHint, resourceId, flags);
			});
			return new ReservationWrapper(result);
		}
	}
}
