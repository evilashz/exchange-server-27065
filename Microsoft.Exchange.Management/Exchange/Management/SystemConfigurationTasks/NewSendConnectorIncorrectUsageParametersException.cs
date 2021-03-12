using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F9D RID: 3997
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NewSendConnectorIncorrectUsageParametersException : LocalizedException
	{
		// Token: 0x0600ACE3 RID: 44259 RVA: 0x00290C77 File Offset: 0x0028EE77
		public NewSendConnectorIncorrectUsageParametersException() : base(Strings.NewSendConnectorIncorrectUsageParameters)
		{
		}

		// Token: 0x0600ACE4 RID: 44260 RVA: 0x00290C84 File Offset: 0x0028EE84
		public NewSendConnectorIncorrectUsageParametersException(Exception innerException) : base(Strings.NewSendConnectorIncorrectUsageParameters, innerException)
		{
		}

		// Token: 0x0600ACE5 RID: 44261 RVA: 0x00290C92 File Offset: 0x0028EE92
		protected NewSendConnectorIncorrectUsageParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACE6 RID: 44262 RVA: 0x00290C9C File Offset: 0x0028EE9C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
