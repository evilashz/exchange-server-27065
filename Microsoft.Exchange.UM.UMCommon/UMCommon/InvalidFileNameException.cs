using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001A6 RID: 422
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidFileNameException : LocalizedException
	{
		// Token: 0x06000E6A RID: 3690 RVA: 0x00035016 File Offset: 0x00033216
		public InvalidFileNameException(int fileNameMaximumLength) : base(Strings.InvalidFileNameException(fileNameMaximumLength))
		{
			this.fileNameMaximumLength = fileNameMaximumLength;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003502B File Offset: 0x0003322B
		public InvalidFileNameException(int fileNameMaximumLength, Exception innerException) : base(Strings.InvalidFileNameException(fileNameMaximumLength), innerException)
		{
			this.fileNameMaximumLength = fileNameMaximumLength;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00035041 File Offset: 0x00033241
		protected InvalidFileNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileNameMaximumLength = (int)info.GetValue("fileNameMaximumLength", typeof(int));
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0003506B File Offset: 0x0003326B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileNameMaximumLength", this.fileNameMaximumLength);
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00035086 File Offset: 0x00033286
		public int FileNameMaximumLength
		{
			get
			{
				return this.fileNameMaximumLength;
			}
		}

		// Token: 0x04000782 RID: 1922
		private readonly int fileNameMaximumLength;
	}
}
