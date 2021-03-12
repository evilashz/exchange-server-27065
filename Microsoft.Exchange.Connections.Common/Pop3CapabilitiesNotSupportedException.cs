using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200004B RID: 75
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3CapabilitiesNotSupportedException : LocalizedException
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00004499 File Offset: 0x00002699
		public Pop3CapabilitiesNotSupportedException() : base(CXStrings.Pop3CapabilitiesNotSupportedMsg)
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000044A6 File Offset: 0x000026A6
		public Pop3CapabilitiesNotSupportedException(Exception innerException) : base(CXStrings.Pop3CapabilitiesNotSupportedMsg, innerException)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000044B4 File Offset: 0x000026B4
		protected Pop3CapabilitiesNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000044BE File Offset: 0x000026BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
