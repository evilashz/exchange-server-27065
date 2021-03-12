using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001119 RID: 4377
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ValidateNotSupportedException : LocalizedException
	{
		// Token: 0x0600B46B RID: 46187 RVA: 0x0029CBE8 File Offset: 0x0029ADE8
		public ValidateNotSupportedException() : base(Strings.MigrationValidateNotSupported)
		{
		}

		// Token: 0x0600B46C RID: 46188 RVA: 0x0029CBF5 File Offset: 0x0029ADF5
		public ValidateNotSupportedException(Exception innerException) : base(Strings.MigrationValidateNotSupported, innerException)
		{
		}

		// Token: 0x0600B46D RID: 46189 RVA: 0x0029CC03 File Offset: 0x0029AE03
		protected ValidateNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B46E RID: 46190 RVA: 0x0029CC0D File Offset: 0x0029AE0D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
