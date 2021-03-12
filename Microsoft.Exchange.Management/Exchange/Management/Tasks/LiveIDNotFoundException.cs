using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E7A RID: 3706
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveIDNotFoundException : RecipientTaskException
	{
		// Token: 0x0600A736 RID: 42806 RVA: 0x0028810B File Offset: 0x0028630B
		public LiveIDNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A737 RID: 42807 RVA: 0x00288114 File Offset: 0x00286314
		public LiveIDNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A738 RID: 42808 RVA: 0x0028811E File Offset: 0x0028631E
		protected LiveIDNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A739 RID: 42809 RVA: 0x00288128 File Offset: 0x00286328
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
