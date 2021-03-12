using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AEC RID: 2796
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuthServerNotFoundException : LocalizedException
	{
		// Token: 0x06008149 RID: 33097 RVA: 0x001A6510 File Offset: 0x001A4710
		public AuthServerNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600814A RID: 33098 RVA: 0x001A6519 File Offset: 0x001A4719
		public AuthServerNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600814B RID: 33099 RVA: 0x001A6523 File Offset: 0x001A4723
		protected AuthServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600814C RID: 33100 RVA: 0x001A652D File Offset: 0x001A472D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
