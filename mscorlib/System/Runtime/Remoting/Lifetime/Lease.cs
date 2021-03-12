using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x020007F3 RID: 2035
	internal class Lease : MarshalByRefObject, ILease
	{
		// Token: 0x0600580B RID: 22539 RVA: 0x00135324 File Offset: 0x00133524
		internal Lease(TimeSpan initialLeaseTime, TimeSpan renewOnCallTime, TimeSpan sponsorshipTimeout, MarshalByRefObject managedObject)
		{
			this.id = Lease.nextId++;
			this.renewOnCallTime = renewOnCallTime;
			this.sponsorshipTimeout = sponsorshipTimeout;
			this.initialLeaseTime = initialLeaseTime;
			this.managedObject = managedObject;
			this.leaseManager = LeaseManager.GetLeaseManager();
			this.sponsorTable = new Hashtable(10);
			this.state = LeaseState.Initial;
		}

		// Token: 0x0600580C RID: 22540 RVA: 0x0013538C File Offset: 0x0013358C
		internal void ActivateLease()
		{
			this.leaseTime = DateTime.UtcNow.Add(this.initialLeaseTime);
			this.state = LeaseState.Active;
			this.leaseManager.ActivateLease(this);
		}

		// Token: 0x0600580D RID: 22541 RVA: 0x001353C5 File Offset: 0x001335C5
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x0600580E RID: 22542 RVA: 0x001353C8 File Offset: 0x001335C8
		// (set) Token: 0x0600580F RID: 22543 RVA: 0x001353D0 File Offset: 0x001335D0
		public TimeSpan RenewOnCallTime
		{
			[SecurityCritical]
			get
			{
				return this.renewOnCallTime;
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				if (this.state == LeaseState.Initial)
				{
					this.renewOnCallTime = value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateRenewOnCall", new object[]
				{
					this.state.ToString()
				}));
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06005810 RID: 22544 RVA: 0x0013540B File Offset: 0x0013360B
		// (set) Token: 0x06005811 RID: 22545 RVA: 0x00135413 File Offset: 0x00133613
		public TimeSpan SponsorshipTimeout
		{
			[SecurityCritical]
			get
			{
				return this.sponsorshipTimeout;
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				if (this.state == LeaseState.Initial)
				{
					this.sponsorshipTimeout = value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateSponsorshipTimeout", new object[]
				{
					this.state.ToString()
				}));
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06005812 RID: 22546 RVA: 0x0013544E File Offset: 0x0013364E
		// (set) Token: 0x06005813 RID: 22547 RVA: 0x00135458 File Offset: 0x00133658
		public TimeSpan InitialLeaseTime
		{
			[SecurityCritical]
			get
			{
				return this.initialLeaseTime;
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				if (this.state != LeaseState.Initial)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateInitialLeaseTime", new object[]
					{
						this.state.ToString()
					}));
				}
				this.initialLeaseTime = value;
				if (TimeSpan.Zero.CompareTo(value) >= 0)
				{
					this.state = LeaseState.Null;
					return;
				}
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06005814 RID: 22548 RVA: 0x001354B7 File Offset: 0x001336B7
		public TimeSpan CurrentLeaseTime
		{
			[SecurityCritical]
			get
			{
				return this.leaseTime.Subtract(DateTime.UtcNow);
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06005815 RID: 22549 RVA: 0x001354C9 File Offset: 0x001336C9
		public LeaseState CurrentState
		{
			[SecurityCritical]
			get
			{
				return this.state;
			}
		}

		// Token: 0x06005816 RID: 22550 RVA: 0x001354D1 File Offset: 0x001336D1
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public void Register(ISponsor obj)
		{
			this.Register(obj, TimeSpan.Zero);
		}

		// Token: 0x06005817 RID: 22551 RVA: 0x001354E0 File Offset: 0x001336E0
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public void Register(ISponsor obj, TimeSpan renewalTime)
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired && !(this.sponsorshipTimeout == TimeSpan.Zero))
				{
					object sponsorId = this.GetSponsorId(obj);
					Hashtable obj2 = this.sponsorTable;
					lock (obj2)
					{
						if (renewalTime > TimeSpan.Zero)
						{
							this.AddTime(renewalTime);
						}
						if (!this.sponsorTable.ContainsKey(sponsorId))
						{
							this.sponsorTable[sponsorId] = new Lease.SponsorStateInfo(renewalTime, Lease.SponsorState.Initial);
						}
					}
				}
			}
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x00135598 File Offset: 0x00133798
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public void Unregister(ISponsor sponsor)
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired)
				{
					object sponsorId = this.GetSponsorId(sponsor);
					Hashtable obj = this.sponsorTable;
					lock (obj)
					{
						if (sponsorId != null)
						{
							this.leaseManager.DeleteSponsor(sponsorId);
							Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
							this.sponsorTable.Remove(sponsorId);
						}
					}
				}
			}
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x00135638 File Offset: 0x00133838
		[SecurityCritical]
		private object GetSponsorId(ISponsor obj)
		{
			object result = null;
			if (obj != null)
			{
				if (RemotingServices.IsTransparentProxy(obj))
				{
					result = RemotingServices.GetRealProxy(obj);
				}
				else
				{
					result = obj;
				}
			}
			return result;
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x00135660 File Offset: 0x00133860
		[SecurityCritical]
		private ISponsor GetSponsorFromId(object sponsorId)
		{
			RealProxy realProxy = sponsorId as RealProxy;
			object obj;
			if (realProxy != null)
			{
				obj = realProxy.GetTransparentProxy();
			}
			else
			{
				obj = sponsorId;
			}
			return (ISponsor)obj;
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x0013568A File Offset: 0x0013388A
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public TimeSpan Renew(TimeSpan renewalTime)
		{
			return this.RenewInternal(renewalTime);
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x00135694 File Offset: 0x00133894
		internal TimeSpan RenewInternal(TimeSpan renewalTime)
		{
			TimeSpan result;
			lock (this)
			{
				if (this.state == LeaseState.Expired)
				{
					result = TimeSpan.Zero;
				}
				else
				{
					this.AddTime(renewalTime);
					result = this.leaseTime.Subtract(DateTime.UtcNow);
				}
			}
			return result;
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x001356F4 File Offset: 0x001338F4
		internal void Remove()
		{
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			this.state = LeaseState.Expired;
			this.leaseManager.DeleteLease(this);
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x00135714 File Offset: 0x00133914
		[SecurityCritical]
		internal void Cancel()
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired)
				{
					this.Remove();
					RemotingServices.Disconnect(this.managedObject, false);
					RemotingServices.Disconnect(this);
				}
			}
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x00135770 File Offset: 0x00133970
		internal void RenewOnCall()
		{
			lock (this)
			{
				if (this.state != LeaseState.Initial && this.state != LeaseState.Expired)
				{
					this.AddTime(this.renewOnCallTime);
				}
			}
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x001357C8 File Offset: 0x001339C8
		[SecurityCritical]
		internal void LeaseExpired(DateTime now)
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired)
				{
					if (this.leaseTime.CompareTo(now) < 0)
					{
						this.ProcessNextSponsor();
					}
				}
			}
		}

		// Token: 0x06005821 RID: 22561 RVA: 0x00135820 File Offset: 0x00133A20
		[SecurityCritical]
		internal void SponsorCall(ISponsor sponsor)
		{
			bool flag = false;
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			Hashtable obj = this.sponsorTable;
			lock (obj)
			{
				try
				{
					object sponsorId = this.GetSponsorId(sponsor);
					this.sponsorCallThread = Thread.CurrentThread.GetHashCode();
					Lease.AsyncRenewal asyncRenewal = new Lease.AsyncRenewal(sponsor.Renewal);
					Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
					sponsorStateInfo.sponsorState = Lease.SponsorState.Waiting;
					IAsyncResult asyncResult = asyncRenewal.BeginInvoke(this, new AsyncCallback(this.SponsorCallback), null);
					if (sponsorStateInfo.sponsorState == Lease.SponsorState.Waiting && this.state != LeaseState.Expired)
					{
						this.leaseManager.RegisterSponsorCall(this, sponsorId, this.sponsorshipTimeout);
					}
					this.sponsorCallThread = 0;
				}
				catch (Exception)
				{
					flag = true;
					this.sponsorCallThread = 0;
				}
			}
			if (flag)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
			}
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x00135914 File Offset: 0x00133B14
		[SecurityCritical]
		internal void SponsorTimeout(object sponsorId)
		{
			lock (this)
			{
				if (this.sponsorTable.ContainsKey(sponsorId))
				{
					Hashtable obj = this.sponsorTable;
					lock (obj)
					{
						Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
						if (sponsorStateInfo.sponsorState == Lease.SponsorState.Waiting)
						{
							this.Unregister(this.GetSponsorFromId(sponsorId));
							this.ProcessNextSponsor();
						}
					}
				}
			}
		}

		// Token: 0x06005823 RID: 22563 RVA: 0x001359B0 File Offset: 0x00133BB0
		[SecurityCritical]
		private void ProcessNextSponsor()
		{
			object obj = null;
			TimeSpan timeSpan = TimeSpan.Zero;
			Hashtable obj2 = this.sponsorTable;
			lock (obj2)
			{
				IDictionaryEnumerator enumerator = this.sponsorTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					object key = enumerator.Key;
					Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)enumerator.Value;
					if (sponsorStateInfo.sponsorState == Lease.SponsorState.Initial && timeSpan == TimeSpan.Zero)
					{
						timeSpan = sponsorStateInfo.renewalTime;
						obj = key;
					}
					else if (sponsorStateInfo.renewalTime > timeSpan)
					{
						timeSpan = sponsorStateInfo.renewalTime;
						obj = key;
					}
				}
			}
			if (obj != null)
			{
				this.SponsorCall(this.GetSponsorFromId(obj));
				return;
			}
			this.Cancel();
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x00135A78 File Offset: 0x00133C78
		[SecurityCritical]
		internal void SponsorCallback(object obj)
		{
			this.SponsorCallback((IAsyncResult)obj);
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x00135A88 File Offset: 0x00133C88
		[SecurityCritical]
		internal void SponsorCallback(IAsyncResult iar)
		{
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			int hashCode = Thread.CurrentThread.GetHashCode();
			if (hashCode == this.sponsorCallThread)
			{
				WaitCallback callBack = new WaitCallback(this.SponsorCallback);
				ThreadPool.QueueUserWorkItem(callBack, iar);
				return;
			}
			AsyncResult asyncResult = (AsyncResult)iar;
			Lease.AsyncRenewal asyncRenewal = (Lease.AsyncRenewal)asyncResult.AsyncDelegate;
			ISponsor sponsor = (ISponsor)asyncRenewal.Target;
			Lease.SponsorStateInfo sponsorStateInfo = null;
			if (!iar.IsCompleted)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
				return;
			}
			bool flag = false;
			TimeSpan renewalTime = TimeSpan.Zero;
			try
			{
				renewalTime = asyncRenewal.EndInvoke(iar);
			}
			catch (Exception)
			{
				flag = true;
			}
			if (flag)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
				return;
			}
			object sponsorId = this.GetSponsorId(sponsor);
			Hashtable obj = this.sponsorTable;
			lock (obj)
			{
				if (this.sponsorTable.ContainsKey(sponsorId))
				{
					sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
					sponsorStateInfo.sponsorState = Lease.SponsorState.Completed;
					sponsorStateInfo.renewalTime = renewalTime;
				}
			}
			if (sponsorStateInfo == null)
			{
				this.ProcessNextSponsor();
				return;
			}
			if (sponsorStateInfo.renewalTime == TimeSpan.Zero)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
				return;
			}
			this.RenewInternal(sponsorStateInfo.renewalTime);
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x00135BE8 File Offset: 0x00133DE8
		private void AddTime(TimeSpan renewalSpan)
		{
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = this.leaseTime;
			DateTime dateTime2 = utcNow.Add(renewalSpan);
			if (this.leaseTime.CompareTo(dateTime2) < 0)
			{
				this.leaseManager.ChangedLeaseTime(this, dateTime2);
				this.leaseTime = dateTime2;
				this.state = LeaseState.Active;
			}
		}

		// Token: 0x040027E1 RID: 10209
		internal int id;

		// Token: 0x040027E2 RID: 10210
		internal DateTime leaseTime;

		// Token: 0x040027E3 RID: 10211
		internal TimeSpan initialLeaseTime;

		// Token: 0x040027E4 RID: 10212
		internal TimeSpan renewOnCallTime;

		// Token: 0x040027E5 RID: 10213
		internal TimeSpan sponsorshipTimeout;

		// Token: 0x040027E6 RID: 10214
		internal Hashtable sponsorTable;

		// Token: 0x040027E7 RID: 10215
		internal int sponsorCallThread;

		// Token: 0x040027E8 RID: 10216
		internal LeaseManager leaseManager;

		// Token: 0x040027E9 RID: 10217
		internal MarshalByRefObject managedObject;

		// Token: 0x040027EA RID: 10218
		internal LeaseState state;

		// Token: 0x040027EB RID: 10219
		internal static volatile int nextId;

		// Token: 0x02000C3E RID: 3134
		// (Invoke) Token: 0x06006F87 RID: 28551
		internal delegate TimeSpan AsyncRenewal(ILease lease);

		// Token: 0x02000C3F RID: 3135
		[Serializable]
		internal enum SponsorState
		{
			// Token: 0x04003714 RID: 14100
			Initial,
			// Token: 0x04003715 RID: 14101
			Waiting,
			// Token: 0x04003716 RID: 14102
			Completed
		}

		// Token: 0x02000C40 RID: 3136
		internal sealed class SponsorStateInfo
		{
			// Token: 0x06006F8A RID: 28554 RVA: 0x0017FCF0 File Offset: 0x0017DEF0
			internal SponsorStateInfo(TimeSpan renewalTime, Lease.SponsorState sponsorState)
			{
				this.renewalTime = renewalTime;
				this.sponsorState = sponsorState;
			}

			// Token: 0x04003717 RID: 14103
			internal TimeSpan renewalTime;

			// Token: 0x04003718 RID: 14104
			internal Lease.SponsorState sponsorState;
		}
	}
}
