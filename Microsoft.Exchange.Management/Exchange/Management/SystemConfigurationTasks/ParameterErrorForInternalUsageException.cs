using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F7B RID: 3963
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParameterErrorForInternalUsageException : LocalizedException
	{
		// Token: 0x0600AC4D RID: 44109 RVA: 0x0029023B File Offset: 0x0028E43B
		public ParameterErrorForInternalUsageException() : base(Strings.ParameterErrorForInternalUsage)
		{
		}

		// Token: 0x0600AC4E RID: 44110 RVA: 0x00290248 File Offset: 0x0028E448
		public ParameterErrorForInternalUsageException(Exception innerException) : base(Strings.ParameterErrorForInternalUsage, innerException)
		{
		}

		// Token: 0x0600AC4F RID: 44111 RVA: 0x00290256 File Offset: 0x0028E456
		protected ParameterErrorForInternalUsageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC50 RID: 44112 RVA: 0x00290260 File Offset: 0x0028E460
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
