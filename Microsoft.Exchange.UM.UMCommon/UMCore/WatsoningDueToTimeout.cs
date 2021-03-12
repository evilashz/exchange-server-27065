using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000207 RID: 519
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class WatsoningDueToTimeout : LocalizedException
	{
		// Token: 0x060010E1 RID: 4321 RVA: 0x00039587 File Offset: 0x00037787
		public WatsoningDueToTimeout() : base(Strings.WatsoningDueToTimeout)
		{
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00039594 File Offset: 0x00037794
		public WatsoningDueToTimeout(Exception innerException) : base(Strings.WatsoningDueToTimeout, innerException)
		{
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x000395A2 File Offset: 0x000377A2
		protected WatsoningDueToTimeout(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000395AC File Offset: 0x000377AC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
