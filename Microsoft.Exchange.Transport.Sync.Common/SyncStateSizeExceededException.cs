using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000057 RID: 87
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SyncStateSizeExceededException : LocalizedException
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000677B File Offset: 0x0000497B
		public SyncStateSizeExceededException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006784 File Offset: 0x00004984
		public SyncStateSizeExceededException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000678E File Offset: 0x0000498E
		protected SyncStateSizeExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006798 File Offset: 0x00004998
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
