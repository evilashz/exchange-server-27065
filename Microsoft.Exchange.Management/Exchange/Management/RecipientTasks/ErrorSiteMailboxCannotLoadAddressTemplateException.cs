using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000EE2 RID: 3810
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorSiteMailboxCannotLoadAddressTemplateException : LocalizedException
	{
		// Token: 0x0600A948 RID: 43336 RVA: 0x0028B637 File Offset: 0x00289837
		public ErrorSiteMailboxCannotLoadAddressTemplateException() : base(Strings.ErrorSiteMailboxCannotLoadAddressTemplate)
		{
		}

		// Token: 0x0600A949 RID: 43337 RVA: 0x0028B644 File Offset: 0x00289844
		public ErrorSiteMailboxCannotLoadAddressTemplateException(Exception innerException) : base(Strings.ErrorSiteMailboxCannotLoadAddressTemplate, innerException)
		{
		}

		// Token: 0x0600A94A RID: 43338 RVA: 0x0028B652 File Offset: 0x00289852
		protected ErrorSiteMailboxCannotLoadAddressTemplateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A94B RID: 43339 RVA: 0x0028B65C File Offset: 0x0028985C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
