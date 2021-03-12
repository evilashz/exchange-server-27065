using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E49 RID: 3657
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserWithMatchingWindowsLiveIdExistsException : RecipientTaskException
	{
		// Token: 0x0600A66B RID: 42603 RVA: 0x00287756 File Offset: 0x00285956
		public UserWithMatchingWindowsLiveIdExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A66C RID: 42604 RVA: 0x0028775F File Offset: 0x0028595F
		public UserWithMatchingWindowsLiveIdExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A66D RID: 42605 RVA: 0x00287769 File Offset: 0x00285969
		protected UserWithMatchingWindowsLiveIdExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A66E RID: 42606 RVA: 0x00287773 File Offset: 0x00285973
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
