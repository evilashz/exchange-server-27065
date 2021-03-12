using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AEE RID: 2798
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuthMetadataParserException : LocalizedException
	{
		// Token: 0x06008151 RID: 33105 RVA: 0x001A655E File Offset: 0x001A475E
		public AuthMetadataParserException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008152 RID: 33106 RVA: 0x001A6567 File Offset: 0x001A4767
		public AuthMetadataParserException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008153 RID: 33107 RVA: 0x001A6571 File Offset: 0x001A4771
		protected AuthMetadataParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008154 RID: 33108 RVA: 0x001A657B File Offset: 0x001A477B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
