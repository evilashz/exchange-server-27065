using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x02000013 RID: 19
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidFilePathException : BitlockerUtilException
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000047D5 File Offset: 0x000029D5
		public InvalidFilePathException(string filePath) : base(DiskManagementStrings.InvalidFilePathError(filePath))
		{
			this.filePath = filePath;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000047EF File Offset: 0x000029EF
		public InvalidFilePathException(string filePath, Exception innerException) : base(DiskManagementStrings.InvalidFilePathError(filePath), innerException)
		{
			this.filePath = filePath;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000480A File Offset: 0x00002A0A
		protected InvalidFilePathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004834 File Offset: 0x00002A34
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000484F File Offset: 0x00002A4F
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x04000052 RID: 82
		private readonly string filePath;
	}
}
