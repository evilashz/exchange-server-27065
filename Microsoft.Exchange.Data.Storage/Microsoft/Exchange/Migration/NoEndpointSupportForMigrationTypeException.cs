using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200019A RID: 410
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NoEndpointSupportForMigrationTypeException : MigrationPermanentException
	{
		// Token: 0x06001755 RID: 5973 RVA: 0x0007088D File Offset: 0x0006EA8D
		public NoEndpointSupportForMigrationTypeException(MigrationType migrationType) : base(Strings.ErrorNoEndpointSupportForMigrationType(migrationType))
		{
			this.migrationType = migrationType;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000708A2 File Offset: 0x0006EAA2
		public NoEndpointSupportForMigrationTypeException(MigrationType migrationType, Exception innerException) : base(Strings.ErrorNoEndpointSupportForMigrationType(migrationType), innerException)
		{
			this.migrationType = migrationType;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000708B8 File Offset: 0x0006EAB8
		protected NoEndpointSupportForMigrationTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.migrationType = (MigrationType)info.GetValue("migrationType", typeof(MigrationType));
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000708E2 File Offset: 0x0006EAE2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("migrationType", this.migrationType);
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x00070902 File Offset: 0x0006EB02
		public MigrationType MigrationType
		{
			get
			{
				return this.migrationType;
			}
		}

		// Token: 0x04000B15 RID: 2837
		private readonly MigrationType migrationType;
	}
}
