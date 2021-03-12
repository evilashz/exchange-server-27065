using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011D3 RID: 4563
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DNSEntryNotFound : LocalizedException
	{
		// Token: 0x0600B8FD RID: 47357 RVA: 0x002A5425 File Offset: 0x002A3625
		public DNSEntryNotFound() : base(Strings.DNSEntryNotFound)
		{
		}

		// Token: 0x0600B8FE RID: 47358 RVA: 0x002A5432 File Offset: 0x002A3632
		public DNSEntryNotFound(Exception innerException) : base(Strings.DNSEntryNotFound, innerException)
		{
		}

		// Token: 0x0600B8FF RID: 47359 RVA: 0x002A5440 File Offset: 0x002A3640
		protected DNSEntryNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B900 RID: 47360 RVA: 0x002A544A File Offset: 0x002A364A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
