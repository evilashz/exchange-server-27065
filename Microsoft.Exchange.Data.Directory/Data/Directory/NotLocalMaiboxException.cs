using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ADB RID: 2779
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotLocalMaiboxException : ADOperationException
	{
		// Token: 0x06008104 RID: 33028 RVA: 0x001A6200 File Offset: 0x001A4400
		public NotLocalMaiboxException() : base(DirectoryStrings.NotLocalMaiboxException)
		{
		}

		// Token: 0x06008105 RID: 33029 RVA: 0x001A620D File Offset: 0x001A440D
		public NotLocalMaiboxException(Exception innerException) : base(DirectoryStrings.NotLocalMaiboxException, innerException)
		{
		}

		// Token: 0x06008106 RID: 33030 RVA: 0x001A621B File Offset: 0x001A441B
		protected NotLocalMaiboxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008107 RID: 33031 RVA: 0x001A6225 File Offset: 0x001A4425
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
