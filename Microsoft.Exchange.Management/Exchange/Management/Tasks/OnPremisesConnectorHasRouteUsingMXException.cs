using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200102B RID: 4139
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OnPremisesConnectorHasRouteUsingMXException : LocalizedException
	{
		// Token: 0x0600AF91 RID: 44945 RVA: 0x0029492C File Offset: 0x00292B2C
		public OnPremisesConnectorHasRouteUsingMXException() : base(Strings.OnPremisesConnectorHasRouteUsingMXId)
		{
		}

		// Token: 0x0600AF92 RID: 44946 RVA: 0x00294939 File Offset: 0x00292B39
		public OnPremisesConnectorHasRouteUsingMXException(Exception innerException) : base(Strings.OnPremisesConnectorHasRouteUsingMXId, innerException)
		{
		}

		// Token: 0x0600AF93 RID: 44947 RVA: 0x00294947 File Offset: 0x00292B47
		protected OnPremisesConnectorHasRouteUsingMXException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AF94 RID: 44948 RVA: 0x00294951 File Offset: 0x00292B51
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
