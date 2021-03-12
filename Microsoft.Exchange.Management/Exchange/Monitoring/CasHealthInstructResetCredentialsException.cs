using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F19 RID: 3865
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthInstructResetCredentialsException : LocalizedException
	{
		// Token: 0x0600AA6B RID: 43627 RVA: 0x0028D5A9 File Offset: 0x0028B7A9
		public CasHealthInstructResetCredentialsException() : base(Strings.CasHealthInstructResetCredentials)
		{
		}

		// Token: 0x0600AA6C RID: 43628 RVA: 0x0028D5B6 File Offset: 0x0028B7B6
		public CasHealthInstructResetCredentialsException(Exception innerException) : base(Strings.CasHealthInstructResetCredentials, innerException)
		{
		}

		// Token: 0x0600AA6D RID: 43629 RVA: 0x0028D5C4 File Offset: 0x0028B7C4
		protected CasHealthInstructResetCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AA6E RID: 43630 RVA: 0x0028D5CE File Offset: 0x0028B7CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
