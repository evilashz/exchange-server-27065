using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020001A0 RID: 416
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NSPIDiscoveryFailedTransientException : MigrationTransientException
	{
		// Token: 0x06001773 RID: 6003 RVA: 0x00070B76 File Offset: 0x0006ED76
		public NSPIDiscoveryFailedTransientException() : base(Strings.CouldNotDiscoverNSPISettings)
		{
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00070B83 File Offset: 0x0006ED83
		public NSPIDiscoveryFailedTransientException(Exception innerException) : base(Strings.CouldNotDiscoverNSPISettings, innerException)
		{
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00070B91 File Offset: 0x0006ED91
		protected NSPIDiscoveryFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00070B9B File Offset: 0x0006ED9B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
