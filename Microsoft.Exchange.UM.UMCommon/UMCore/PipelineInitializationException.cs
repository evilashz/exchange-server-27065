using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001FD RID: 509
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PipelineInitializationException : LocalizedException
	{
		// Token: 0x060010B6 RID: 4278 RVA: 0x000392D6 File Offset: 0x000374D6
		public PipelineInitializationException() : base(Strings.PipelineInitialization)
		{
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000392E3 File Offset: 0x000374E3
		public PipelineInitializationException(Exception innerException) : base(Strings.PipelineInitialization, innerException)
		{
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000392F1 File Offset: 0x000374F1
		protected PipelineInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000392FB File Offset: 0x000374FB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
