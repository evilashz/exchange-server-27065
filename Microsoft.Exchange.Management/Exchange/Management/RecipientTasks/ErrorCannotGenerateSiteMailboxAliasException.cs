using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000EE1 RID: 3809
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorCannotGenerateSiteMailboxAliasException : LocalizedException
	{
		// Token: 0x0600A944 RID: 43332 RVA: 0x0028B608 File Offset: 0x00289808
		public ErrorCannotGenerateSiteMailboxAliasException() : base(Strings.ErrorCannotGenerateSiteMailboxAlias)
		{
		}

		// Token: 0x0600A945 RID: 43333 RVA: 0x0028B615 File Offset: 0x00289815
		public ErrorCannotGenerateSiteMailboxAliasException(Exception innerException) : base(Strings.ErrorCannotGenerateSiteMailboxAlias, innerException)
		{
		}

		// Token: 0x0600A946 RID: 43334 RVA: 0x0028B623 File Offset: 0x00289823
		protected ErrorCannotGenerateSiteMailboxAliasException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A947 RID: 43335 RVA: 0x0028B62D File Offset: 0x0028982D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
