using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ABB RID: 2747
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FfoSizeLimitReachedException : DataSourceOperationException
	{
		// Token: 0x06008064 RID: 32868 RVA: 0x001A52B2 File Offset: 0x001A34B2
		public FfoSizeLimitReachedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008065 RID: 32869 RVA: 0x001A52BB File Offset: 0x001A34BB
		public FfoSizeLimitReachedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008066 RID: 32870 RVA: 0x001A52C5 File Offset: 0x001A34C5
		protected FfoSizeLimitReachedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008067 RID: 32871 RVA: 0x001A52CF File Offset: 0x001A34CF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
