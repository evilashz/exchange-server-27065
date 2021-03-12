using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000190 RID: 400
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SensitiveInformationCmdletException : LocalizedException
	{
		// Token: 0x06000FBB RID: 4027 RVA: 0x0003689A File Offset: 0x00034A9A
		public SensitiveInformationCmdletException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000368A3 File Offset: 0x00034AA3
		public SensitiveInformationCmdletException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000368AD File Offset: 0x00034AAD
		protected SensitiveInformationCmdletException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000368B7 File Offset: 0x00034AB7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
