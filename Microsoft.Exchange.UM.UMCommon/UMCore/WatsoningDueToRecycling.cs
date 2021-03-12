using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000206 RID: 518
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class WatsoningDueToRecycling : LocalizedException
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x00039558 File Offset: 0x00037758
		public WatsoningDueToRecycling() : base(Strings.WatsoningDueToRecycling)
		{
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00039565 File Offset: 0x00037765
		public WatsoningDueToRecycling(Exception innerException) : base(Strings.WatsoningDueToRecycling, innerException)
		{
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00039573 File Offset: 0x00037773
		protected WatsoningDueToRecycling(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003957D File Offset: 0x0003777D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
