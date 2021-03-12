using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EBC RID: 3772
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidValueForPreventCompletionException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A887 RID: 43143 RVA: 0x0028A311 File Offset: 0x00288511
		public InvalidValueForPreventCompletionException() : base(Strings.ErrorInvalidValueForPreventCompletion)
		{
		}

		// Token: 0x0600A888 RID: 43144 RVA: 0x0028A31E File Offset: 0x0028851E
		public InvalidValueForPreventCompletionException(Exception innerException) : base(Strings.ErrorInvalidValueForPreventCompletion, innerException)
		{
		}

		// Token: 0x0600A889 RID: 43145 RVA: 0x0028A32C File Offset: 0x0028852C
		protected InvalidValueForPreventCompletionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A88A RID: 43146 RVA: 0x0028A336 File Offset: 0x00288536
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
