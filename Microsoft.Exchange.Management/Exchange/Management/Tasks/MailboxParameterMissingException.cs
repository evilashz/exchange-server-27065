using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020011A2 RID: 4514
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxParameterMissingException : LocalizedException
	{
		// Token: 0x0600B719 RID: 46873 RVA: 0x002A0E04 File Offset: 0x0029F004
		public MailboxParameterMissingException() : base(Strings.MailboxParameterMissingException)
		{
		}

		// Token: 0x0600B71A RID: 46874 RVA: 0x002A0E11 File Offset: 0x0029F011
		public MailboxParameterMissingException(Exception innerException) : base(Strings.MailboxParameterMissingException, innerException)
		{
		}

		// Token: 0x0600B71B RID: 46875 RVA: 0x002A0E1F File Offset: 0x0029F01F
		protected MailboxParameterMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B71C RID: 46876 RVA: 0x002A0E29 File Offset: 0x0029F029
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
