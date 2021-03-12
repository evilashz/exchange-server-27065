using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200018B RID: 395
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TroubleshootingCmdletException : LocalizedException
	{
		// Token: 0x06000FA4 RID: 4004 RVA: 0x000366E4 File Offset: 0x000348E4
		public TroubleshootingCmdletException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x000366ED File Offset: 0x000348ED
		public TroubleshootingCmdletException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000366F7 File Offset: 0x000348F7
		protected TroubleshootingCmdletException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00036701 File Offset: 0x00034901
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
