using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E76 RID: 3702
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxInvalidOperationException : RecipientTaskException
	{
		// Token: 0x0600A726 RID: 42790 RVA: 0x0028806F File Offset: 0x0028626F
		public GroupMailboxInvalidOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A727 RID: 42791 RVA: 0x00288078 File Offset: 0x00286278
		public GroupMailboxInvalidOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A728 RID: 42792 RVA: 0x00288082 File Offset: 0x00286282
		protected GroupMailboxInvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A729 RID: 42793 RVA: 0x0028808C File Offset: 0x0028628C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
