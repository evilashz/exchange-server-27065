using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011AD RID: 4525
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserNotAllowedForUMEnabledException : LocalizedException
	{
		// Token: 0x0600B84F RID: 47183 RVA: 0x002A4700 File Offset: 0x002A2900
		public UserNotAllowedForUMEnabledException() : base(Strings.ExceptionUserNotAllowedForUMEnabled)
		{
		}

		// Token: 0x0600B850 RID: 47184 RVA: 0x002A470D File Offset: 0x002A290D
		public UserNotAllowedForUMEnabledException(Exception innerException) : base(Strings.ExceptionUserNotAllowedForUMEnabled, innerException)
		{
		}

		// Token: 0x0600B851 RID: 47185 RVA: 0x002A471B File Offset: 0x002A291B
		protected UserNotAllowedForUMEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B852 RID: 47186 RVA: 0x002A4725 File Offset: 0x002A2925
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
