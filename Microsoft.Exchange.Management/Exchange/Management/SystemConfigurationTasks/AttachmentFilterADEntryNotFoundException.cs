using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F44 RID: 3908
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AttachmentFilterADEntryNotFoundException : LocalizedException
	{
		// Token: 0x0600AB4E RID: 43854 RVA: 0x0028EDF8 File Offset: 0x0028CFF8
		public AttachmentFilterADEntryNotFoundException() : base(Strings.AttachmentFilterADEntryNotFound)
		{
		}

		// Token: 0x0600AB4F RID: 43855 RVA: 0x0028EE05 File Offset: 0x0028D005
		public AttachmentFilterADEntryNotFoundException(Exception innerException) : base(Strings.AttachmentFilterADEntryNotFound, innerException)
		{
		}

		// Token: 0x0600AB50 RID: 43856 RVA: 0x0028EE13 File Offset: 0x0028D013
		protected AttachmentFilterADEntryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AB51 RID: 43857 RVA: 0x0028EE1D File Offset: 0x0028D01D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
