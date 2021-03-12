using System;
using System.Collections;
using System.Diagnostics;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x020007F5 RID: 2037
	internal class LeaseManager
	{
		// Token: 0x0600582C RID: 22572 RVA: 0x00135C94 File Offset: 0x00133E94
		internal static bool IsInitialized()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			LeaseManager leaseManager = remotingData.LeaseManager;
			return leaseManager != null;
		}

		// Token: 0x0600582D RID: 22573 RVA: 0x00135CB8 File Offset: 0x00133EB8
		[SecurityCritical]
		internal static LeaseManager GetLeaseManager(TimeSpan pollTime)
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			LeaseManager leaseManager = remotingData.LeaseManager;
			if (leaseManager == null)
			{
				DomainSpecificRemotingData obj = remotingData;
				lock (obj)
				{
					if (remotingData.LeaseManager == null)
					{
						remotingData.LeaseManager = new LeaseManager(pollTime);
					}
					leaseManager = remotingData.LeaseManager;
				}
			}
			return leaseManager;
		}

		// Token: 0x0600582E RID: 22574 RVA: 0x00135D20 File Offset: 0x00133F20
		internal static LeaseManager GetLeaseManager()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			return remotingData.LeaseManager;
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x00135D40 File Offset: 0x00133F40
		[SecurityCritical]
		private LeaseManager(TimeSpan pollTime)
		{
			this.pollTime = pollTime;
			this.leaseTimeAnalyzerDelegate = new TimerCallback(this.LeaseTimeAnalyzer);
			this.waitHandle = new AutoResetEvent(false);
			this.leaseTimer = new Timer(this.leaseTimeAnalyzerDelegate, null, -1, -1);
			this.leaseTimer.Change((int)pollTime.TotalMilliseconds, -1);
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x00135DC8 File Offset: 0x00133FC8
		internal void ChangePollTime(TimeSpan pollTime)
		{
			this.pollTime = pollTime;
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x00135DD4 File Offset: 0x00133FD4
		internal void ActivateLease(Lease lease)
		{
			Hashtable obj = this.leaseToTimeTable;
			lock (obj)
			{
				this.leaseToTimeTable[lease] = lease.leaseTime;
			}
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x00135E28 File Offset: 0x00134028
		internal void DeleteLease(Lease lease)
		{
			Hashtable obj = this.leaseToTimeTable;
			lock (obj)
			{
				this.leaseToTimeTable.Remove(lease);
			}
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x00135E70 File Offset: 0x00134070
		[Conditional("_LOGGING")]
		internal void DumpLeases(Lease[] leases)
		{
			for (int i = 0; i < leases.Length; i++)
			{
			}
		}

		// Token: 0x06005834 RID: 22580 RVA: 0x00135E8C File Offset: 0x0013408C
		internal ILease GetLease(MarshalByRefObject obj)
		{
			bool flag = true;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			if (identity == null)
			{
				return null;
			}
			return identity.Lease;
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x00135EB0 File Offset: 0x001340B0
		internal void ChangedLeaseTime(Lease lease, DateTime newTime)
		{
			Hashtable obj = this.leaseToTimeTable;
			lock (obj)
			{
				this.leaseToTimeTable[lease] = newTime;
			}
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x00135EFC File Offset: 0x001340FC
		internal void RegisterSponsorCall(Lease lease, object sponsorId, TimeSpan sponsorshipTimeOut)
		{
			Hashtable obj = this.sponsorTable;
			lock (obj)
			{
				DateTime sponsorWaitTime = DateTime.UtcNow.Add(sponsorshipTimeOut);
				this.sponsorTable[sponsorId] = new LeaseManager.SponsorInfo(lease, sponsorId, sponsorWaitTime);
			}
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x00135F5C File Offset: 0x0013415C
		internal void DeleteSponsor(object sponsorId)
		{
			Hashtable obj = this.sponsorTable;
			lock (obj)
			{
				this.sponsorTable.Remove(sponsorId);
			}
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x00135FA4 File Offset: 0x001341A4
		[SecurityCritical]
		private void LeaseTimeAnalyzer(object state)
		{
			DateTime utcNow = DateTime.UtcNow;
			Hashtable obj = this.leaseToTimeTable;
			lock (obj)
			{
				IDictionaryEnumerator enumerator = this.leaseToTimeTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DateTime dateTime = (DateTime)enumerator.Value;
					Lease value = (Lease)enumerator.Key;
					if (dateTime.CompareTo(utcNow) < 0)
					{
						this.tempObjects.Add(value);
					}
				}
				for (int i = 0; i < this.tempObjects.Count; i++)
				{
					Lease key = (Lease)this.tempObjects[i];
					this.leaseToTimeTable.Remove(key);
				}
			}
			for (int j = 0; j < this.tempObjects.Count; j++)
			{
				Lease lease = (Lease)this.tempObjects[j];
				if (lease != null)
				{
					lease.LeaseExpired(utcNow);
				}
			}
			this.tempObjects.Clear();
			Hashtable obj2 = this.sponsorTable;
			lock (obj2)
			{
				IDictionaryEnumerator enumerator2 = this.sponsorTable.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					object key2 = enumerator2.Key;
					LeaseManager.SponsorInfo sponsorInfo = (LeaseManager.SponsorInfo)enumerator2.Value;
					if (sponsorInfo.sponsorWaitTime.CompareTo(utcNow) < 0)
					{
						this.tempObjects.Add(sponsorInfo);
					}
				}
				for (int k = 0; k < this.tempObjects.Count; k++)
				{
					LeaseManager.SponsorInfo sponsorInfo2 = (LeaseManager.SponsorInfo)this.tempObjects[k];
					this.sponsorTable.Remove(sponsorInfo2.sponsorId);
				}
			}
			for (int l = 0; l < this.tempObjects.Count; l++)
			{
				LeaseManager.SponsorInfo sponsorInfo3 = (LeaseManager.SponsorInfo)this.tempObjects[l];
				if (sponsorInfo3 != null && sponsorInfo3.lease != null)
				{
					sponsorInfo3.lease.SponsorTimeout(sponsorInfo3.sponsorId);
					this.tempObjects[l] = null;
				}
			}
			this.tempObjects.Clear();
			this.leaseTimer.Change((int)this.pollTime.TotalMilliseconds, -1);
		}

		// Token: 0x040027EE RID: 10222
		private Hashtable leaseToTimeTable = new Hashtable();

		// Token: 0x040027EF RID: 10223
		private Hashtable sponsorTable = new Hashtable();

		// Token: 0x040027F0 RID: 10224
		private TimeSpan pollTime;

		// Token: 0x040027F1 RID: 10225
		private AutoResetEvent waitHandle;

		// Token: 0x040027F2 RID: 10226
		private TimerCallback leaseTimeAnalyzerDelegate;

		// Token: 0x040027F3 RID: 10227
		private volatile Timer leaseTimer;

		// Token: 0x040027F4 RID: 10228
		private ArrayList tempObjects = new ArrayList(10);

		// Token: 0x02000C41 RID: 3137
		internal class SponsorInfo
		{
			// Token: 0x06006F8B RID: 28555 RVA: 0x0017FD06 File Offset: 0x0017DF06
			internal SponsorInfo(Lease lease, object sponsorId, DateTime sponsorWaitTime)
			{
				this.lease = lease;
				this.sponsorId = sponsorId;
				this.sponsorWaitTime = sponsorWaitTime;
			}

			// Token: 0x04003719 RID: 14105
			internal Lease lease;

			// Token: 0x0400371A RID: 14106
			internal object sponsorId;

			// Token: 0x0400371B RID: 14107
			internal DateTime sponsorWaitTime;
		}
	}
}
