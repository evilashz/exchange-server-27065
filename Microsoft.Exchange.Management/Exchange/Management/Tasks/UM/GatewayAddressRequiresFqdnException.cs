using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011EB RID: 4587
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GatewayAddressRequiresFqdnException : LocalizedException
	{
		// Token: 0x0600B975 RID: 47477 RVA: 0x002A5FAB File Offset: 0x002A41AB
		public GatewayAddressRequiresFqdnException() : base(Strings.GatewayAddressRequiresFqdn)
		{
		}

		// Token: 0x0600B976 RID: 47478 RVA: 0x002A5FB8 File Offset: 0x002A41B8
		public GatewayAddressRequiresFqdnException(Exception innerException) : base(Strings.GatewayAddressRequiresFqdn, innerException)
		{
		}

		// Token: 0x0600B977 RID: 47479 RVA: 0x002A5FC6 File Offset: 0x002A41C6
		protected GatewayAddressRequiresFqdnException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B978 RID: 47480 RVA: 0x002A5FD0 File Offset: 0x002A41D0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
