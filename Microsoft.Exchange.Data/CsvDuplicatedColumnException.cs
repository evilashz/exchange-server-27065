using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000E5 RID: 229
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CsvDuplicatedColumnException : CsvValidationException
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x0001AF6C File Offset: 0x0001916C
		public CsvDuplicatedColumnException(string duplicatedColumn) : base(DataStrings.DuplicatedColumn(duplicatedColumn))
		{
			this.duplicatedColumn = duplicatedColumn;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001AF81 File Offset: 0x00019181
		public CsvDuplicatedColumnException(string duplicatedColumn, Exception innerException) : base(DataStrings.DuplicatedColumn(duplicatedColumn), innerException)
		{
			this.duplicatedColumn = duplicatedColumn;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001AF97 File Offset: 0x00019197
		protected CsvDuplicatedColumnException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.duplicatedColumn = (string)info.GetValue("duplicatedColumn", typeof(string));
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001AFC1 File Offset: 0x000191C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("duplicatedColumn", this.duplicatedColumn);
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001AFDC File Offset: 0x000191DC
		public string DuplicatedColumn
		{
			get
			{
				return this.duplicatedColumn;
			}
		}

		// Token: 0x04000587 RID: 1415
		private readonly string duplicatedColumn;
	}
}
