using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000121 RID: 289
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class BlockedDatabaseException : StorageTransientException
	{
		// Token: 0x06001431 RID: 5169 RVA: 0x0006A5BB File Offset: 0x000687BB
		public BlockedDatabaseException(Guid resourceIdentity) : base(ServerStrings.idResourceQuarantinedDueToBlackHole(resourceIdentity))
		{
			this.resourceIdentity = resourceIdentity;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0006A5D0 File Offset: 0x000687D0
		public BlockedDatabaseException(Guid resourceIdentity, Exception innerException) : base(ServerStrings.idResourceQuarantinedDueToBlackHole(resourceIdentity), innerException)
		{
			this.resourceIdentity = resourceIdentity;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0006A5E6 File Offset: 0x000687E6
		protected BlockedDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.resourceIdentity = (Guid)info.GetValue("resourceIdentity", typeof(Guid));
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0006A610 File Offset: 0x00068810
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("resourceIdentity", this.resourceIdentity);
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x0006A630 File Offset: 0x00068830
		public Guid ResourceIdentity
		{
			get
			{
				return this.resourceIdentity;
			}
		}

		// Token: 0x040009AD RID: 2477
		private readonly Guid resourceIdentity;
	}
}
