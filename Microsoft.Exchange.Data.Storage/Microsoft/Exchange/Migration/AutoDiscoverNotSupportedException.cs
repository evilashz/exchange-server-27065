using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000190 RID: 400
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AutoDiscoverNotSupportedException : MigrationPermanentException
	{
		// Token: 0x0600171E RID: 5918 RVA: 0x00070221 File Offset: 0x0006E421
		public AutoDiscoverNotSupportedException(MigrationType type) : base(Strings.ErrorAutoDiscoveryNotSupported(type))
		{
			this.type = type;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00070236 File Offset: 0x0006E436
		public AutoDiscoverNotSupportedException(MigrationType type, Exception innerException) : base(Strings.ErrorAutoDiscoveryNotSupported(type), innerException)
		{
			this.type = type;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0007024C File Offset: 0x0006E44C
		protected AutoDiscoverNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (MigrationType)info.GetValue("type", typeof(MigrationType));
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00070276 File Offset: 0x0006E476
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x00070296 File Offset: 0x0006E496
		public MigrationType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000B06 RID: 2822
		private readonly MigrationType type;
	}
}
