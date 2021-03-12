using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200019C RID: 412
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConnectionSettingsNotSerializedException : MigrationPermanentException
	{
		// Token: 0x0600175F RID: 5983 RVA: 0x00070987 File Offset: 0x0006EB87
		public ConnectionSettingsNotSerializedException() : base(Strings.ErrorConnectionSettingsNotSerialized)
		{
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00070994 File Offset: 0x0006EB94
		public ConnectionSettingsNotSerializedException(Exception innerException) : base(Strings.ErrorConnectionSettingsNotSerialized, innerException)
		{
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000709A2 File Offset: 0x0006EBA2
		protected ConnectionSettingsNotSerializedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000709AC File Offset: 0x0006EBAC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
