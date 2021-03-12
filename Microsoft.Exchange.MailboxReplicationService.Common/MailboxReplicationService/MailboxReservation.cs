using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200027A RID: 634
	internal abstract class MailboxReservation : ReservationBase
	{
		// Token: 0x06001F85 RID: 8069 RVA: 0x000421CC File Offset: 0x000403CC
		public MailboxReservation()
		{
			this.expirationTimestamp = DateTime.UtcNow + ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("ReservationExpirationInterval");
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x0004221C File Offset: 0x0004041C
		public override bool IsActive
		{
			get
			{
				bool result;
				lock (this.locker)
				{
					result = (this.activeMailboxes.Count != 0 || !(DateTime.UtcNow > this.expirationTimestamp));
				}
				return result;
			}
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x0004227C File Offset: 0x0004047C
		public void Activate(Guid mailboxGuid)
		{
			lock (this.locker)
			{
				if (base.IsDisposed)
				{
					throw new ExpiredReservationException();
				}
				this.activeMailboxes.Add(mailboxGuid);
				this.expirationTimestamp = DateTime.MaxValue;
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000422DC File Offset: 0x000404DC
		public void DisconnectOrphanedSession(Guid mailboxGuid)
		{
			lock (this.locker)
			{
				if (this.activeMailboxes.Contains(mailboxGuid))
				{
					Action<Guid> action;
					if (this.disconnectOrphanedSessionActions.TryGetValue(mailboxGuid, out action))
					{
						action(mailboxGuid);
						this.disconnectOrphanedSessionActions.Remove(mailboxGuid);
					}
				}
			}
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0004234C File Offset: 0x0004054C
		public void Deactivate(Guid mailboxGuid)
		{
			lock (this.locker)
			{
				this.activeMailboxes.Remove(mailboxGuid);
				this.disconnectOrphanedSessionActions.Remove(mailboxGuid);
				if (this.activeMailboxes.Count == 0)
				{
					this.expirationTimestamp = DateTime.UtcNow + ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("ReservationExpirationInterval");
				}
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000423C8 File Offset: 0x000405C8
		public void RegisterDisconnectOrphanedSessionAction(Guid mailboxGuid, Action<Guid> disconnectAction)
		{
			lock (this.locker)
			{
				this.disconnectOrphanedSessionActions[mailboxGuid] = disconnectAction;
			}
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x00042410 File Offset: 0x00040610
		protected override void GetDiagnosticInfoInternal(XElement root)
		{
			base.GetDiagnosticInfoInternal(root);
			if (this.activeMailboxes.Count == 0)
			{
				root.Add(new XAttribute("ExpirationTS", this.expirationTimestamp));
			}
		}

		// Token: 0x04000CB5 RID: 3253
		private object locker = new object();

		// Token: 0x04000CB6 RID: 3254
		private HashSet<Guid> activeMailboxes = new HashSet<Guid>();

		// Token: 0x04000CB7 RID: 3255
		private Dictionary<Guid, Action<Guid>> disconnectOrphanedSessionActions = new Dictionary<Guid, Action<Guid>>();

		// Token: 0x04000CB8 RID: 3256
		private DateTime expirationTimestamp;
	}
}
