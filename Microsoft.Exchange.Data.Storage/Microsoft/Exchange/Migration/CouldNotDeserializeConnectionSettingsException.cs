using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200019D RID: 413
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CouldNotDeserializeConnectionSettingsException : MigrationPermanentException
	{
		// Token: 0x06001763 RID: 5987 RVA: 0x000709B6 File Offset: 0x0006EBB6
		public CouldNotDeserializeConnectionSettingsException() : base(Strings.ErrorCouldNotDeserializeConnectionSettings)
		{
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x000709C3 File Offset: 0x0006EBC3
		public CouldNotDeserializeConnectionSettingsException(Exception innerException) : base(Strings.ErrorCouldNotDeserializeConnectionSettings, innerException)
		{
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000709D1 File Offset: 0x0006EBD1
		protected CouldNotDeserializeConnectionSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000709DB File Offset: 0x0006EBDB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
