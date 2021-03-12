using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x020010A8 RID: 4264
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxParameterNotSpecifiedException : LocalizedException
	{
		// Token: 0x0600B241 RID: 45633 RVA: 0x002998A7 File Offset: 0x00297AA7
		public MailboxParameterNotSpecifiedException() : base(Strings.MailboxParameterNotSpecified)
		{
		}

		// Token: 0x0600B242 RID: 45634 RVA: 0x002998B4 File Offset: 0x00297AB4
		public MailboxParameterNotSpecifiedException(Exception innerException) : base(Strings.MailboxParameterNotSpecified, innerException)
		{
		}

		// Token: 0x0600B243 RID: 45635 RVA: 0x002998C2 File Offset: 0x00297AC2
		protected MailboxParameterNotSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B244 RID: 45636 RVA: 0x002998CC File Offset: 0x00297ACC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
