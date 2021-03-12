using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F79 RID: 3961
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParameterErrorForDefaultUsageException : LocalizedException
	{
		// Token: 0x0600AC45 RID: 44101 RVA: 0x002901DD File Offset: 0x0028E3DD
		public ParameterErrorForDefaultUsageException() : base(Strings.ParameterErrorForDefaultUsage)
		{
		}

		// Token: 0x0600AC46 RID: 44102 RVA: 0x002901EA File Offset: 0x0028E3EA
		public ParameterErrorForDefaultUsageException(Exception innerException) : base(Strings.ParameterErrorForDefaultUsage, innerException)
		{
		}

		// Token: 0x0600AC47 RID: 44103 RVA: 0x002901F8 File Offset: 0x0028E3F8
		protected ParameterErrorForDefaultUsageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC48 RID: 44104 RVA: 0x00290202 File Offset: 0x0028E402
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
