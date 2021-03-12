using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000E88 RID: 3720
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveApprovalApplicationWithMailboxes : LocalizedException
	{
		// Token: 0x0600A77D RID: 42877 RVA: 0x0028880D File Offset: 0x00286A0D
		public CannotRemoveApprovalApplicationWithMailboxes() : base(Strings.CannotRemoveApprovalApplicationWithMailboxes)
		{
		}

		// Token: 0x0600A77E RID: 42878 RVA: 0x0028881A File Offset: 0x00286A1A
		public CannotRemoveApprovalApplicationWithMailboxes(Exception innerException) : base(Strings.CannotRemoveApprovalApplicationWithMailboxes, innerException)
		{
		}

		// Token: 0x0600A77F RID: 42879 RVA: 0x00288828 File Offset: 0x00286A28
		protected CannotRemoveApprovalApplicationWithMailboxes(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A780 RID: 42880 RVA: 0x00288832 File Offset: 0x00286A32
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
