using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000153 RID: 339
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationJobNotFoundException : MigrationPermanentException
	{
		// Token: 0x06001606 RID: 5638 RVA: 0x0006EC4D File Offset: 0x0006CE4D
		public MigrationJobNotFoundException(Guid identity) : base(Strings.ErrorMigrationJobNotFound(identity))
		{
			this.identity = identity;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0006EC62 File Offset: 0x0006CE62
		public MigrationJobNotFoundException(Guid identity, Exception innerException) : base(Strings.ErrorMigrationJobNotFound(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x0006EC78 File Offset: 0x0006CE78
		protected MigrationJobNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (Guid)info.GetValue("identity", typeof(Guid));
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0006ECA2 File Offset: 0x0006CEA2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0006ECC2 File Offset: 0x0006CEC2
		public Guid Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04000AE2 RID: 2786
		private readonly Guid identity;
	}
}
