using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200105C RID: 4188
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskBothConfigurationOnlyAndQuorumOnlySpecifiedException : LocalizedException
	{
		// Token: 0x0600B09D RID: 45213 RVA: 0x002967B1 File Offset: 0x002949B1
		public TaskBothConfigurationOnlyAndQuorumOnlySpecifiedException() : base(Strings.TaskBothConfigurationOnlyAndQuorumOnlySpecified)
		{
		}

		// Token: 0x0600B09E RID: 45214 RVA: 0x002967BE File Offset: 0x002949BE
		public TaskBothConfigurationOnlyAndQuorumOnlySpecifiedException(Exception innerException) : base(Strings.TaskBothConfigurationOnlyAndQuorumOnlySpecified, innerException)
		{
		}

		// Token: 0x0600B09F RID: 45215 RVA: 0x002967CC File Offset: 0x002949CC
		protected TaskBothConfigurationOnlyAndQuorumOnlySpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B0A0 RID: 45216 RVA: 0x002967D6 File Offset: 0x002949D6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
