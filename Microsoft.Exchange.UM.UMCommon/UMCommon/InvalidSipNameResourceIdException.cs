using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001C2 RID: 450
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidSipNameResourceIdException : LocalizedException
	{
		// Token: 0x06000EF3 RID: 3827 RVA: 0x00035CE6 File Offset: 0x00033EE6
		public InvalidSipNameResourceIdException() : base(Strings.ExceptionInvalidSipNameResourceId)
		{
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00035CF3 File Offset: 0x00033EF3
		public InvalidSipNameResourceIdException(Exception innerException) : base(Strings.ExceptionInvalidSipNameResourceId, innerException)
		{
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00035D01 File Offset: 0x00033F01
		protected InvalidSipNameResourceIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00035D0B File Offset: 0x00033F0B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
