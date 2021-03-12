using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E3C RID: 3644
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrganizationDoesNotExistException : OrganizationTaskException
	{
		// Token: 0x0600A637 RID: 42551 RVA: 0x0028755B File Offset: 0x0028575B
		public OrganizationDoesNotExistException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A638 RID: 42552 RVA: 0x00287564 File Offset: 0x00285764
		public OrganizationDoesNotExistException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A639 RID: 42553 RVA: 0x0028756E File Offset: 0x0028576E
		protected OrganizationDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A63A RID: 42554 RVA: 0x00287578 File Offset: 0x00285778
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
