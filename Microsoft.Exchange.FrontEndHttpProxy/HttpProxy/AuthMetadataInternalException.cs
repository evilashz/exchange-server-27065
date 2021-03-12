using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000DF RID: 223
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuthMetadataInternalException : AuthMetadataBuilderException
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x0002EF55 File Offset: 0x0002D155
		public AuthMetadataInternalException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0002EF5E File Offset: 0x0002D15E
		public AuthMetadataInternalException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0002EF68 File Offset: 0x0002D168
		protected AuthMetadataInternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0002EF72 File Offset: 0x0002D172
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
