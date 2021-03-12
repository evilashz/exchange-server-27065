using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011FA RID: 4602
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_InvalidIPAddressReceived : LocalizedException
	{
		// Token: 0x0600B9BA RID: 47546 RVA: 0x002A651F File Offset: 0x002A471F
		public TUC_InvalidIPAddressReceived() : base(Strings.InvalidIPAddressReceived)
		{
		}

		// Token: 0x0600B9BB RID: 47547 RVA: 0x002A652C File Offset: 0x002A472C
		public TUC_InvalidIPAddressReceived(Exception innerException) : base(Strings.InvalidIPAddressReceived, innerException)
		{
		}

		// Token: 0x0600B9BC RID: 47548 RVA: 0x002A653A File Offset: 0x002A473A
		protected TUC_InvalidIPAddressReceived(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B9BD RID: 47549 RVA: 0x002A6544 File Offset: 0x002A4744
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
