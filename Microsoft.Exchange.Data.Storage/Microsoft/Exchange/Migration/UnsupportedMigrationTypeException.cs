using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000169 RID: 361
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnsupportedMigrationTypeException : MigrationPermanentException
	{
		// Token: 0x06001674 RID: 5748 RVA: 0x0006F6E5 File Offset: 0x0006D8E5
		public UnsupportedMigrationTypeException(MigrationType type) : base(Strings.UnsupportedMigrationTypeError(type))
		{
			this.type = type;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0006F6FA File Offset: 0x0006D8FA
		public UnsupportedMigrationTypeException(MigrationType type, Exception innerException) : base(Strings.UnsupportedMigrationTypeError(type), innerException)
		{
			this.type = type;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0006F710 File Offset: 0x0006D910
		protected UnsupportedMigrationTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (MigrationType)info.GetValue("type", typeof(MigrationType));
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0006F73A File Offset: 0x0006D93A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x0006F75A File Offset: 0x0006D95A
		public MigrationType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000AF8 RID: 2808
		private readonly MigrationType type;
	}
}
