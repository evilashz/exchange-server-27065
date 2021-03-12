using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x02000009 RID: 9
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuthMetadataInternalException : AuthMetadataBuilderException
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00003462 File Offset: 0x00001662
		public AuthMetadataInternalException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000346B File Offset: 0x0000166B
		public AuthMetadataInternalException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003475 File Offset: 0x00001675
		protected AuthMetadataInternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000347F File Offset: 0x0000167F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
