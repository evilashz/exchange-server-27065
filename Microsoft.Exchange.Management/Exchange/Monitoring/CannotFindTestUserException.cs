using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F20 RID: 3872
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotFindTestUserException : LocalizedException
	{
		// Token: 0x0600AA91 RID: 43665 RVA: 0x0028D9F6 File Offset: 0x0028BBF6
		public CannotFindTestUserException() : base(Strings.CannotFindTestUser)
		{
		}

		// Token: 0x0600AA92 RID: 43666 RVA: 0x0028DA03 File Offset: 0x0028BC03
		public CannotFindTestUserException(Exception innerException) : base(Strings.CannotFindTestUser, innerException)
		{
		}

		// Token: 0x0600AA93 RID: 43667 RVA: 0x0028DA11 File Offset: 0x0028BC11
		protected CannotFindTestUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AA94 RID: 43668 RVA: 0x0028DA1B File Offset: 0x0028BC1B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
