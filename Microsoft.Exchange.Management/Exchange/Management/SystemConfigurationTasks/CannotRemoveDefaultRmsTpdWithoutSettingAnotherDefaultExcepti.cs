using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010F2 RID: 4338
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveDefaultRmsTpdWithoutSettingAnotherDefaultException : LocalizedException
	{
		// Token: 0x0600B3A6 RID: 45990 RVA: 0x0029B891 File Offset: 0x00299A91
		public CannotRemoveDefaultRmsTpdWithoutSettingAnotherDefaultException() : base(Strings.CannotRemoveDefaultRmsTpdWithoutSettingAnotherDefault)
		{
		}

		// Token: 0x0600B3A7 RID: 45991 RVA: 0x0029B89E File Offset: 0x00299A9E
		public CannotRemoveDefaultRmsTpdWithoutSettingAnotherDefaultException(Exception innerException) : base(Strings.CannotRemoveDefaultRmsTpdWithoutSettingAnotherDefault, innerException)
		{
		}

		// Token: 0x0600B3A8 RID: 45992 RVA: 0x0029B8AC File Offset: 0x00299AAC
		protected CannotRemoveDefaultRmsTpdWithoutSettingAnotherDefaultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B3A9 RID: 45993 RVA: 0x0029B8B6 File Offset: 0x00299AB6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
