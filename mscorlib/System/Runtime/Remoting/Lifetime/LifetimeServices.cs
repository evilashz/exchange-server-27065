using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x020007F7 RID: 2039
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class LifetimeServices
	{
		// Token: 0x06005839 RID: 22585 RVA: 0x001361EC File Offset: 0x001343EC
		private static TimeSpan GetTimeSpan(ref long ticks)
		{
			return TimeSpan.FromTicks(Volatile.Read(ref ticks));
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x001361F9 File Offset: 0x001343F9
		private static void SetTimeSpan(ref long ticks, TimeSpan value)
		{
			Volatile.Write(ref ticks, value.Ticks);
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x0600583B RID: 22587 RVA: 0x00136208 File Offset: 0x00134408
		private static object LifetimeSyncObject
		{
			get
			{
				if (LifetimeServices.s_LifetimeSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref LifetimeServices.s_LifetimeSyncObject, value, null);
				}
				return LifetimeServices.s_LifetimeSyncObject;
			}
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x00136234 File Offset: 0x00134434
		[Obsolete("Do not create instances of the LifetimeServices class.  Call the static methods directly on this type instead", true)]
		public LifetimeServices()
		{
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x0600583D RID: 22589 RVA: 0x0013623C File Offset: 0x0013443C
		// (set) Token: 0x0600583E RID: 22590 RVA: 0x00136248 File Offset: 0x00134448
		public static TimeSpan LeaseTime
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_leaseTimeTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					if (LifetimeServices.s_isLeaseTime)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", new object[]
						{
							"LeaseTime"
						}));
					}
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_leaseTimeTicks, value);
					LifetimeServices.s_isLeaseTime = true;
				}
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x0600583F RID: 22591 RVA: 0x001362B8 File Offset: 0x001344B8
		// (set) Token: 0x06005840 RID: 22592 RVA: 0x001362C4 File Offset: 0x001344C4
		public static TimeSpan RenewOnCallTime
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_renewOnCallTimeTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					if (LifetimeServices.s_isRenewOnCallTime)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", new object[]
						{
							"RenewOnCallTime"
						}));
					}
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_renewOnCallTimeTicks, value);
					LifetimeServices.s_isRenewOnCallTime = true;
				}
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06005841 RID: 22593 RVA: 0x00136334 File Offset: 0x00134534
		// (set) Token: 0x06005842 RID: 22594 RVA: 0x00136340 File Offset: 0x00134540
		public static TimeSpan SponsorshipTimeout
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_sponsorshipTimeoutTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					if (LifetimeServices.s_isSponsorshipTimeout)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", new object[]
						{
							"SponsorshipTimeout"
						}));
					}
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_sponsorshipTimeoutTicks, value);
					LifetimeServices.s_isSponsorshipTimeout = true;
				}
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06005843 RID: 22595 RVA: 0x001363B0 File Offset: 0x001345B0
		// (set) Token: 0x06005844 RID: 22596 RVA: 0x001363BC File Offset: 0x001345BC
		public static TimeSpan LeaseManagerPollTime
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_pollTimeTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_pollTimeTicks, value);
					if (LeaseManager.IsInitialized())
					{
						LeaseManager.GetLeaseManager().ChangePollTime(value);
					}
				}
			}
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x00136414 File Offset: 0x00134614
		[SecurityCritical]
		internal static ILease GetLeaseInitial(MarshalByRefObject obj)
		{
			LeaseManager leaseManager = LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime);
			ILease lease = leaseManager.GetLease(obj);
			if (lease == null)
			{
				lease = LifetimeServices.CreateLease(obj);
			}
			return lease;
		}

		// Token: 0x06005846 RID: 22598 RVA: 0x00136444 File Offset: 0x00134644
		[SecurityCritical]
		internal static ILease GetLease(MarshalByRefObject obj)
		{
			LeaseManager leaseManager = LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime);
			return leaseManager.GetLease(obj);
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x00136467 File Offset: 0x00134667
		[SecurityCritical]
		internal static ILease CreateLease(MarshalByRefObject obj)
		{
			return LifetimeServices.CreateLease(LifetimeServices.LeaseTime, LifetimeServices.RenewOnCallTime, LifetimeServices.SponsorshipTimeout, obj);
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x0013647E File Offset: 0x0013467E
		[SecurityCritical]
		internal static ILease CreateLease(TimeSpan leaseTime, TimeSpan renewOnCallTime, TimeSpan sponsorshipTimeout, MarshalByRefObject obj)
		{
			LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime);
			return new Lease(leaseTime, renewOnCallTime, sponsorshipTimeout, obj);
		}

		// Token: 0x040027FB RID: 10235
		private static bool s_isLeaseTime = false;

		// Token: 0x040027FC RID: 10236
		private static bool s_isRenewOnCallTime = false;

		// Token: 0x040027FD RID: 10237
		private static bool s_isSponsorshipTimeout = false;

		// Token: 0x040027FE RID: 10238
		private static long s_leaseTimeTicks = TimeSpan.FromMinutes(5.0).Ticks;

		// Token: 0x040027FF RID: 10239
		private static long s_renewOnCallTimeTicks = TimeSpan.FromMinutes(2.0).Ticks;

		// Token: 0x04002800 RID: 10240
		private static long s_sponsorshipTimeoutTicks = TimeSpan.FromMinutes(2.0).Ticks;

		// Token: 0x04002801 RID: 10241
		private static long s_pollTimeTicks = TimeSpan.FromMilliseconds(10000.0).Ticks;

		// Token: 0x04002802 RID: 10242
		private static object s_LifetimeSyncObject = null;
	}
}
