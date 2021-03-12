using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F77 RID: 3959
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LongAddressesEnabledOnEdgeException : LocalizedException
	{
		// Token: 0x0600AC3D RID: 44093 RVA: 0x0029017F File Offset: 0x0028E37F
		public LongAddressesEnabledOnEdgeException() : base(Strings.LongAddressesEnabledOnEdge)
		{
		}

		// Token: 0x0600AC3E RID: 44094 RVA: 0x0029018C File Offset: 0x0028E38C
		public LongAddressesEnabledOnEdgeException(Exception innerException) : base(Strings.LongAddressesEnabledOnEdge, innerException)
		{
		}

		// Token: 0x0600AC3F RID: 44095 RVA: 0x0029019A File Offset: 0x0028E39A
		protected LongAddressesEnabledOnEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC40 RID: 44096 RVA: 0x002901A4 File Offset: 0x0028E3A4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
