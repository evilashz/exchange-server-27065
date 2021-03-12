using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011ED RID: 4589
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GatewayIPAddressFamilyInconsistentException : LocalizedException
	{
		// Token: 0x0600B97D RID: 47485 RVA: 0x002A6009 File Offset: 0x002A4209
		public GatewayIPAddressFamilyInconsistentException() : base(Strings.GatewayIPAddressFamilyInconsistentException)
		{
		}

		// Token: 0x0600B97E RID: 47486 RVA: 0x002A6016 File Offset: 0x002A4216
		public GatewayIPAddressFamilyInconsistentException(Exception innerException) : base(Strings.GatewayIPAddressFamilyInconsistentException, innerException)
		{
		}

		// Token: 0x0600B97F RID: 47487 RVA: 0x002A6024 File Offset: 0x002A4224
		protected GatewayIPAddressFamilyInconsistentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B980 RID: 47488 RVA: 0x002A602E File Offset: 0x002A422E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
