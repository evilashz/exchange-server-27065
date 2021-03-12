using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A70 RID: 2672
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADTimelimitExceededException : ADOperationException
	{
		// Token: 0x06007F1D RID: 32541 RVA: 0x001A3E05 File Offset: 0x001A2005
		public ADTimelimitExceededException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F1E RID: 32542 RVA: 0x001A3E0E File Offset: 0x001A200E
		public ADTimelimitExceededException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F1F RID: 32543 RVA: 0x001A3E18 File Offset: 0x001A2018
		protected ADTimelimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F20 RID: 32544 RVA: 0x001A3E22 File Offset: 0x001A2022
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
