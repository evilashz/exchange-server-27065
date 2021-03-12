using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020001A2 RID: 418
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationVersionMismatchException : MigrationTransientException
	{
		// Token: 0x0600177D RID: 6013 RVA: 0x00070C71 File Offset: 0x0006EE71
		public MigrationVersionMismatchException(long version, long expectedVersion) : base(Strings.MigrationVersionMismatch(version, expectedVersion))
		{
			this.version = version;
			this.expectedVersion = expectedVersion;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00070C8E File Offset: 0x0006EE8E
		public MigrationVersionMismatchException(long version, long expectedVersion, Exception innerException) : base(Strings.MigrationVersionMismatch(version, expectedVersion), innerException)
		{
			this.version = version;
			this.expectedVersion = expectedVersion;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00070CAC File Offset: 0x0006EEAC
		protected MigrationVersionMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.version = (long)info.GetValue("version", typeof(long));
			this.expectedVersion = (long)info.GetValue("expectedVersion", typeof(long));
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00070D01 File Offset: 0x0006EF01
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("version", this.version);
			info.AddValue("expectedVersion", this.expectedVersion);
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x00070D2D File Offset: 0x0006EF2D
		public long Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x00070D35 File Offset: 0x0006EF35
		public long ExpectedVersion
		{
			get
			{
				return this.expectedVersion;
			}
		}

		// Token: 0x04000B1D RID: 2845
		private readonly long version;

		// Token: 0x04000B1E RID: 2846
		private readonly long expectedVersion;
	}
}
