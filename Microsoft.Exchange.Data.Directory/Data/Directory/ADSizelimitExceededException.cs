using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A6F RID: 2671
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADSizelimitExceededException : ADOperationException
	{
		// Token: 0x06007F19 RID: 32537 RVA: 0x001A3DDE File Offset: 0x001A1FDE
		public ADSizelimitExceededException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F1A RID: 32538 RVA: 0x001A3DE7 File Offset: 0x001A1FE7
		public ADSizelimitExceededException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F1B RID: 32539 RVA: 0x001A3DF1 File Offset: 0x001A1FF1
		protected ADSizelimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F1C RID: 32540 RVA: 0x001A3DFB File Offset: 0x001A1FFB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
