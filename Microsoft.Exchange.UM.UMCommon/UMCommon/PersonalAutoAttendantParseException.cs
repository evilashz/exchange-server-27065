using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001D2 RID: 466
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PersonalAutoAttendantParseException : LocalizedException
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x000362C1 File Offset: 0x000344C1
		public PersonalAutoAttendantParseException() : base(Strings.InvalidPAA)
		{
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000362CE File Offset: 0x000344CE
		public PersonalAutoAttendantParseException(Exception innerException) : base(Strings.InvalidPAA, innerException)
		{
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000362DC File Offset: 0x000344DC
		protected PersonalAutoAttendantParseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000362E6 File Offset: 0x000344E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
