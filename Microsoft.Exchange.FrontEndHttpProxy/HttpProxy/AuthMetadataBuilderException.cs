using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000DE RID: 222
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuthMetadataBuilderException : LocalizedException
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0002EF2E File Offset: 0x0002D12E
		public AuthMetadataBuilderException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0002EF37 File Offset: 0x0002D137
		public AuthMetadataBuilderException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0002EF41 File Offset: 0x0002D141
		protected AuthMetadataBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002EF4B File Offset: 0x0002D14B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
