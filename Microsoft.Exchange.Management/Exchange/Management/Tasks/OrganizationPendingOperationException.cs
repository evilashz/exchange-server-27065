using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E3D RID: 3645
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrganizationPendingOperationException : OrganizationTaskException
	{
		// Token: 0x0600A63B RID: 42555 RVA: 0x00287582 File Offset: 0x00285782
		public OrganizationPendingOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A63C RID: 42556 RVA: 0x0028758B File Offset: 0x0028578B
		public OrganizationPendingOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A63D RID: 42557 RVA: 0x00287595 File Offset: 0x00285795
		protected OrganizationPendingOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A63E RID: 42558 RVA: 0x0028759F File Offset: 0x0028579F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
