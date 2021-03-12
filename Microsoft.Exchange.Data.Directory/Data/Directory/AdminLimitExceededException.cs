using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AA4 RID: 2724
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdminLimitExceededException : ADOperationException
	{
		// Token: 0x06007FFD RID: 32765 RVA: 0x001A4B4C File Offset: 0x001A2D4C
		public AdminLimitExceededException() : base(DirectoryStrings.ExceptionAdminLimitExceeded)
		{
		}

		// Token: 0x06007FFE RID: 32766 RVA: 0x001A4B59 File Offset: 0x001A2D59
		public AdminLimitExceededException(Exception innerException) : base(DirectoryStrings.ExceptionAdminLimitExceeded, innerException)
		{
		}

		// Token: 0x06007FFF RID: 32767 RVA: 0x001A4B67 File Offset: 0x001A2D67
		protected AdminLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008000 RID: 32768 RVA: 0x001A4B71 File Offset: 0x001A2D71
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
