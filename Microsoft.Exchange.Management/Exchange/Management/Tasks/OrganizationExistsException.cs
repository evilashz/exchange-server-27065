using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E3B RID: 3643
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrganizationExistsException : OrganizationTaskException
	{
		// Token: 0x0600A633 RID: 42547 RVA: 0x00287534 File Offset: 0x00285734
		public OrganizationExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A634 RID: 42548 RVA: 0x0028753D File Offset: 0x0028573D
		public OrganizationExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A635 RID: 42549 RVA: 0x00287547 File Offset: 0x00285747
		protected OrganizationExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A636 RID: 42550 RVA: 0x00287551 File Offset: 0x00285751
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
