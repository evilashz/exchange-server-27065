using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F1B RID: 3867
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthSpecifyMailboxForResetCredentialsException : LocalizedException
	{
		// Token: 0x0600AA77 RID: 43639 RVA: 0x0028D749 File Offset: 0x0028B949
		public CasHealthSpecifyMailboxForResetCredentialsException() : base(Strings.CasHealthSpecifyMailboxForResetCredentials)
		{
		}

		// Token: 0x0600AA78 RID: 43640 RVA: 0x0028D756 File Offset: 0x0028B956
		public CasHealthSpecifyMailboxForResetCredentialsException(Exception innerException) : base(Strings.CasHealthSpecifyMailboxForResetCredentials, innerException)
		{
		}

		// Token: 0x0600AA79 RID: 43641 RVA: 0x0028D764 File Offset: 0x0028B964
		protected CasHealthSpecifyMailboxForResetCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AA7A RID: 43642 RVA: 0x0028D76E File Offset: 0x0028B96E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
