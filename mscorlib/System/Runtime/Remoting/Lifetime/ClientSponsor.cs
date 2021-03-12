using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x020007F0 RID: 2032
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ClientSponsor : MarshalByRefObject, ISponsor
	{
		// Token: 0x060057F4 RID: 22516 RVA: 0x00135162 File Offset: 0x00133362
		public ClientSponsor()
		{
		}

		// Token: 0x060057F5 RID: 22517 RVA: 0x0013518B File Offset: 0x0013338B
		public ClientSponsor(TimeSpan renewalTime)
		{
			this.m_renewalTime = renewalTime;
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x060057F6 RID: 22518 RVA: 0x001351BB File Offset: 0x001333BB
		// (set) Token: 0x060057F7 RID: 22519 RVA: 0x001351C3 File Offset: 0x001333C3
		public TimeSpan RenewalTime
		{
			get
			{
				return this.m_renewalTime;
			}
			set
			{
				this.m_renewalTime = value;
			}
		}

		// Token: 0x060057F8 RID: 22520 RVA: 0x001351CC File Offset: 0x001333CC
		[SecurityCritical]
		public bool Register(MarshalByRefObject obj)
		{
			ILease lease = (ILease)obj.GetLifetimeService();
			if (lease == null)
			{
				return false;
			}
			lease.Register(this);
			Hashtable obj2 = this.sponsorTable;
			lock (obj2)
			{
				this.sponsorTable[obj] = lease;
			}
			return true;
		}

		// Token: 0x060057F9 RID: 22521 RVA: 0x0013522C File Offset: 0x0013342C
		[SecurityCritical]
		public void Unregister(MarshalByRefObject obj)
		{
			ILease lease = null;
			Hashtable obj2 = this.sponsorTable;
			lock (obj2)
			{
				lease = (ILease)this.sponsorTable[obj];
			}
			if (lease != null)
			{
				lease.Unregister(this);
			}
		}

		// Token: 0x060057FA RID: 22522 RVA: 0x00135284 File Offset: 0x00133484
		[SecurityCritical]
		public TimeSpan Renewal(ILease lease)
		{
			return this.m_renewalTime;
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x0013528C File Offset: 0x0013348C
		[SecurityCritical]
		public void Close()
		{
			Hashtable obj = this.sponsorTable;
			lock (obj)
			{
				IDictionaryEnumerator enumerator = this.sponsorTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					((ILease)enumerator.Value).Unregister(this);
				}
				this.sponsorTable.Clear();
			}
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x001352F8 File Offset: 0x001334F8
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x001352FC File Offset: 0x001334FC
		[SecuritySafeCritical]
		~ClientSponsor()
		{
		}

		// Token: 0x040027DF RID: 10207
		private Hashtable sponsorTable = new Hashtable(10);

		// Token: 0x040027E0 RID: 10208
		private TimeSpan m_renewalTime = TimeSpan.FromMinutes(2.0);
	}
}
