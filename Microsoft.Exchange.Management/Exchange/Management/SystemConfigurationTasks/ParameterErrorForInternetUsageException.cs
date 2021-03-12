using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F7A RID: 3962
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParameterErrorForInternetUsageException : LocalizedException
	{
		// Token: 0x0600AC49 RID: 44105 RVA: 0x0029020C File Offset: 0x0028E40C
		public ParameterErrorForInternetUsageException() : base(Strings.ParameterErrorForInternetUsage)
		{
		}

		// Token: 0x0600AC4A RID: 44106 RVA: 0x00290219 File Offset: 0x0028E419
		public ParameterErrorForInternetUsageException(Exception innerException) : base(Strings.ParameterErrorForInternetUsage, innerException)
		{
		}

		// Token: 0x0600AC4B RID: 44107 RVA: 0x00290227 File Offset: 0x0028E427
		protected ParameterErrorForInternetUsageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC4C RID: 44108 RVA: 0x00290231 File Offset: 0x0028E431
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
