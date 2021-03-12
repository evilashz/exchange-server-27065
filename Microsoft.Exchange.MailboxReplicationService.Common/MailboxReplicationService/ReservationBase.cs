using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000279 RID: 633
	internal abstract class ReservationBase : DisposeTrackableBase, IReservation, IDisposable
	{
		// Token: 0x06001F69 RID: 8041 RVA: 0x00041C76 File Offset: 0x0003FE76
		public ReservationBase()
		{
			this.ReservationId = Guid.NewGuid();
			this.ReservedResources = new List<ResourceBase>();
			this.releaseActions = new List<Action<ReservationBase>>();
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x00041CAA File Offset: 0x0003FEAA
		// (set) Token: 0x06001F6B RID: 8043 RVA: 0x00041CB2 File Offset: 0x0003FEB2
		public Guid ReservationId { get; private set; }

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x00041CBB File Offset: 0x0003FEBB
		// (set) Token: 0x06001F6D RID: 8045 RVA: 0x00041CC3 File Offset: 0x0003FEC3
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x00041CCC File Offset: 0x0003FECC
		// (set) Token: 0x06001F6F RID: 8047 RVA: 0x00041CD4 File Offset: 0x0003FED4
		public TenantPartitionHint PartitionHint { get; private set; }

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x00041CDD File Offset: 0x0003FEDD
		// (set) Token: 0x06001F71 RID: 8049 RVA: 0x00041CE5 File Offset: 0x0003FEE5
		public Guid ResourceId { get; private set; }

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x00041CEE File Offset: 0x0003FEEE
		// (set) Token: 0x06001F73 RID: 8051 RVA: 0x00041CF6 File Offset: 0x0003FEF6
		public ReservationFlags Flags { get; private set; }

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x00041CFF File Offset: 0x0003FEFF
		// (set) Token: 0x06001F75 RID: 8053 RVA: 0x00041D07 File Offset: 0x0003FF07
		public string ClientName { get; private set; }

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x00041D10 File Offset: 0x0003FF10
		// (set) Token: 0x06001F77 RID: 8055 RVA: 0x00041D18 File Offset: 0x0003FF18
		public List<ResourceBase> ReservedResources { get; private set; }

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06001F78 RID: 8056
		public abstract bool IsActive { get; }

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x00041D24 File Offset: 0x0003FF24
		public WorkloadType WorkloadType
		{
			get
			{
				if (this.Flags.HasFlag(ReservationFlags.HighPriority))
				{
					return WorkloadType.MailboxReplicationServiceHighPriority;
				}
				if (this.Flags.HasFlag(ReservationFlags.Interactive))
				{
					return WorkloadType.MailboxReplicationServiceInteractive;
				}
				if (this.Flags.HasFlag(ReservationFlags.InternalMaintenance))
				{
					return WorkloadType.MailboxReplicationServiceInternalMaintenance;
				}
				return WorkloadType.MailboxReplicationService;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x00041D8B File Offset: 0x0003FF8B
		Guid IReservation.Id
		{
			get
			{
				return this.ReservationId;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x00041D93 File Offset: 0x0003FF93
		ReservationFlags IReservation.Flags
		{
			get
			{
				return this.Flags;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x00041D9B File Offset: 0x0003FF9B
		Guid IReservation.ResourceId
		{
			get
			{
				return this.ResourceId;
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x00041DA4 File Offset: 0x0003FFA4
		public static ReservationBase CreateReservation(Guid mailboxGuid, TenantPartitionHint partitionHint, Guid resourceId, ReservationFlags flags, string clientName)
		{
			ReservationBase result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				SettingsContextBase settingsContextBase = new MailboxSettingsContext(mailboxGuid, null);
				if (partitionHint != null)
				{
					settingsContextBase = new OrganizationSettingsContext(OrganizationId.FromExternalDirectoryOrganizationId(partitionHint.GetExternalDirectoryOrganizationId()), settingsContextBase);
				}
				ReservationBase reservationBase;
				if (resourceId == MRSResource.Id.ObjectGuid)
				{
					reservationBase = new MRSReservation();
				}
				else
				{
					if (flags.HasFlag(ReservationFlags.Read))
					{
						reservationBase = new ReadReservation();
					}
					else
					{
						reservationBase = new WriteReservation();
					}
					settingsContextBase = new DatabaseSettingsContext(resourceId, settingsContextBase);
				}
				disposeGuard.Add<ReservationBase>(reservationBase);
				settingsContextBase = new GenericSettingsContext("WorkloadType", reservationBase.WorkloadType.ToString(), settingsContextBase);
				reservationBase.MailboxGuid = mailboxGuid;
				reservationBase.PartitionHint = partitionHint;
				reservationBase.ResourceId = resourceId;
				reservationBase.Flags = flags;
				reservationBase.ClientName = clientName;
				using (settingsContextBase.Activate())
				{
					reservationBase.ReserveResources();
				}
				disposeGuard.Success();
				result = reservationBase;
			}
			return result;
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x00041EB4 File Offset: 0x000400B4
		public void AddReleaseAction(Action<ReservationBase> releaseAction)
		{
			lock (this.locker)
			{
				this.releaseActions.Add(releaseAction);
			}
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x00041EFC File Offset: 0x000400FC
		public XElement GetDiagnosticInfo(MRSDiagnosticArgument arguments)
		{
			XElement xelement = new XElement("Reservation");
			this.GetDiagnosticInfoInternal(xelement);
			return xelement;
		}

		// Token: 0x06001F80 RID: 8064
		protected abstract IEnumerable<ResourceBase> GetDependentResources();

		// Token: 0x06001F81 RID: 8065 RVA: 0x00041F24 File Offset: 0x00040124
		protected virtual void GetDiagnosticInfoInternal(XElement root)
		{
			root.Add(new XAttribute("Id", this.ReservationId));
			root.Add(new XAttribute("Flags", this.Flags.ToString()));
			root.Add(new XAttribute("MbxGuid", this.MailboxGuid));
			if (this.PartitionHint != null)
			{
				root.Add(new XAttribute("Partition", this.PartitionHint.ToString()));
			}
			foreach (ResourceBase resourceBase in this.ReservedResources)
			{
				root.Add(new XElement("Resource", new object[]
				{
					new XAttribute("Type", resourceBase.ResourceType),
					new XAttribute("Name", resourceBase.ResourceName)
				}));
			}
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00042068 File Offset: 0x00040268
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				lock (this.locker)
				{
					using (List<Action<ReservationBase>>.Enumerator enumerator = this.releaseActions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Action<ReservationBase> releaseAction = enumerator.Current;
							CommonUtils.CatchKnownExceptions(delegate
							{
								releaseAction(this);
							}, null);
						}
					}
					this.releaseActions.Clear();
				}
			}
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0004211C File Offset: 0x0004031C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ReservationBase>(this);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00042124 File Offset: 0x00040324
		private void ReserveResources()
		{
			foreach (ResourceBase resourceBase in this.GetDependentResources())
			{
				resourceBase.Reserve(this);
				this.ReservedResources.Add(resourceBase);
			}
			MrsTracer.Throttling.Debug("Successful reservation: Id {0}, mbxGuid {1}, resourceID {2}, flags {3}", new object[]
			{
				this.ReservationId,
				this.MailboxGuid,
				this.ResourceId,
				this.Flags
			});
		}

		// Token: 0x04000CAC RID: 3244
		private List<Action<ReservationBase>> releaseActions;

		// Token: 0x04000CAD RID: 3245
		private object locker = new object();
	}
}
