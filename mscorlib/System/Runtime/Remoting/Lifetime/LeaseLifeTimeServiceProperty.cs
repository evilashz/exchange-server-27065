using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x020007F8 RID: 2040
	[Serializable]
	internal class LeaseLifeTimeServiceProperty : IContextProperty, IContributeObjectSink
	{
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x0600584A RID: 22602 RVA: 0x00136525 File Offset: 0x00134725
		public string Name
		{
			[SecurityCritical]
			get
			{
				return "LeaseLifeTimeServiceProperty";
			}
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x0013652C File Offset: 0x0013472C
		[SecurityCritical]
		public bool IsNewContextOK(Context newCtx)
		{
			return true;
		}

		// Token: 0x0600584C RID: 22604 RVA: 0x0013652F File Offset: 0x0013472F
		[SecurityCritical]
		public void Freeze(Context newContext)
		{
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x00136534 File Offset: 0x00134734
		[SecurityCritical]
		public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
		{
			bool flag;
			ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(obj, out flag);
			if (serverIdentity.IsSingleCall())
			{
				return nextSink;
			}
			object obj2 = obj.InitializeLifetimeService();
			if (obj2 == null)
			{
				return nextSink;
			}
			if (!(obj2 is ILease))
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_ILeaseReturn", new object[]
				{
					obj2
				}));
			}
			ILease lease = (ILease)obj2;
			if (lease.InitialLeaseTime.CompareTo(TimeSpan.Zero) <= 0)
			{
				if (lease is Lease)
				{
					((Lease)lease).Remove();
				}
				return nextSink;
			}
			Lease lease2 = null;
			ServerIdentity obj3 = serverIdentity;
			lock (obj3)
			{
				if (serverIdentity.Lease != null)
				{
					lease2 = serverIdentity.Lease;
					lease2.Renew(lease2.InitialLeaseTime);
				}
				else
				{
					if (!(lease is Lease))
					{
						lease2 = (Lease)LifetimeServices.GetLeaseInitial(obj);
						if (lease2.CurrentState == LeaseState.Initial)
						{
							lease2.InitialLeaseTime = lease.InitialLeaseTime;
							lease2.RenewOnCallTime = lease.RenewOnCallTime;
							lease2.SponsorshipTimeout = lease.SponsorshipTimeout;
						}
					}
					else
					{
						lease2 = (Lease)lease;
					}
					serverIdentity.Lease = lease2;
					if (serverIdentity.ObjectRef != null)
					{
						lease2.ActivateLease();
					}
				}
			}
			if (lease2.RenewOnCallTime > TimeSpan.Zero)
			{
				return new LeaseSink(lease2, nextSink);
			}
			return nextSink;
		}
	}
}
