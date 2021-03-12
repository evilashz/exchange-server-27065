using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000200 RID: 512
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PipelineCleanupGeneratedWatson : LocalizedException
	{
		// Token: 0x060010C2 RID: 4290 RVA: 0x00039363 File Offset: 0x00037563
		public PipelineCleanupGeneratedWatson() : base(Strings.PipelineCleanupGeneratedWatson)
		{
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00039370 File Offset: 0x00037570
		public PipelineCleanupGeneratedWatson(Exception innerException) : base(Strings.PipelineCleanupGeneratedWatson, innerException)
		{
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0003937E File Offset: 0x0003757E
		protected PipelineCleanupGeneratedWatson(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00039388 File Offset: 0x00037588
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
