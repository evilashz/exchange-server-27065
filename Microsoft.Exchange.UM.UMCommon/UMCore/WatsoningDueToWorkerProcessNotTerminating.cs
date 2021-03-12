using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000208 RID: 520
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class WatsoningDueToWorkerProcessNotTerminating : LocalizedException
	{
		// Token: 0x060010E5 RID: 4325 RVA: 0x000395B6 File Offset: 0x000377B6
		public WatsoningDueToWorkerProcessNotTerminating() : base(Strings.WatsoningDueToWorkerProcessNotTerminating)
		{
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x000395C3 File Offset: 0x000377C3
		public WatsoningDueToWorkerProcessNotTerminating(Exception innerException) : base(Strings.WatsoningDueToWorkerProcessNotTerminating, innerException)
		{
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000395D1 File Offset: 0x000377D1
		protected WatsoningDueToWorkerProcessNotTerminating(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000395DB File Offset: 0x000377DB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
