using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E6D RID: 3693
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveIdAlreadyExistsException : RecipientTaskException
	{
		// Token: 0x0600A702 RID: 42754 RVA: 0x00287F10 File Offset: 0x00286110
		public LiveIdAlreadyExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A703 RID: 42755 RVA: 0x00287F19 File Offset: 0x00286119
		public LiveIdAlreadyExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A704 RID: 42756 RVA: 0x00287F23 File Offset: 0x00286123
		protected LiveIdAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A705 RID: 42757 RVA: 0x00287F2D File Offset: 0x0028612D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
