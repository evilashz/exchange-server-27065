using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.DelegatedAuthentication.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DelegatedSecurityTokenExpiredException : LocalizedException
	{
		// Token: 0x06000077 RID: 119 RVA: 0x000047E7 File Offset: 0x000029E7
		public DelegatedSecurityTokenExpiredException() : base(Strings.SecurityTokenExpired)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000047F4 File Offset: 0x000029F4
		public DelegatedSecurityTokenExpiredException(Exception innerException) : base(Strings.SecurityTokenExpired, innerException)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004802 File Offset: 0x00002A02
		protected DelegatedSecurityTokenExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000480C File Offset: 0x00002A0C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
