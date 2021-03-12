using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E40 RID: 3648
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotDetermineManagementEndpointException : ManagementEndpointTaskException
	{
		// Token: 0x0600A647 RID: 42567 RVA: 0x002875F7 File Offset: 0x002857F7
		public CannotDetermineManagementEndpointException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A648 RID: 42568 RVA: 0x00287600 File Offset: 0x00285800
		public CannotDetermineManagementEndpointException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A649 RID: 42569 RVA: 0x0028760A File Offset: 0x0028580A
		protected CannotDetermineManagementEndpointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A64A RID: 42570 RVA: 0x00287614 File Offset: 0x00285814
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
