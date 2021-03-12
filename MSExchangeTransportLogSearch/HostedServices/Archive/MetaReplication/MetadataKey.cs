using System;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x02000055 RID: 85
	public abstract class MetadataKey<T> : IMetaReplicationKey, IEquatable<IMetaReplicationKey> where T : MetadataKey<T>, IEquatable<T>
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000BC27 File Offset: 0x00009E27
		public override string ToString()
		{
			return XmlStringSerializer.ToString<T>(this);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000BC2F File Offset: 0x00009E2F
		public override int GetHashCode()
		{
			return this.ComputeHashCode();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000BC38 File Offset: 0x00009E38
		public bool Equals(IMetaReplicationKey other)
		{
			T t = other as T;
			return t != null && this.Equals(t);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000BC64 File Offset: 0x00009E64
		public override bool Equals(object obj)
		{
			T t = obj as T;
			return t != null && this.Equals(t);
		}

		// Token: 0x060001CB RID: 459
		public abstract bool Equals(T other);

		// Token: 0x060001CC RID: 460
		protected abstract int ComputeHashCode();
	}
}
