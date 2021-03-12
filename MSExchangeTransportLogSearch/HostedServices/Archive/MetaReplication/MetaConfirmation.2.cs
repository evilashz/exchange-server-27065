using System;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x02000054 RID: 84
	public abstract class MetaConfirmation<T, K> : MetaConfirmation, IMetaConfirmation, IConfirmableReplicationItem, IEquatable<T> where T : MetaConfirmation<T, K>, IEquatable<T> where K : IMetaReplicationKey
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000BB57 File Offset: 0x00009D57
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000BB5F File Offset: 0x00009D5F
		public K Key { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000BB68 File Offset: 0x00009D68
		IMetaReplicationKey IMetaConfirmation.Key
		{
			get
			{
				return this.Key;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000BB78 File Offset: 0x00009D78
		public bool Equals(T other)
		{
			if (other == null)
			{
				return false;
			}
			bool flag;
			if (this.Key == null)
			{
				flag = (other.Key == null);
			}
			else
			{
				K key = this.Key;
				flag = key.Equals(other.Key);
			}
			return flag && base.Status == other.Status && base.CustomerId == other.CustomerId && base.DatacenterName == other.DatacenterName;
		}
	}
}
