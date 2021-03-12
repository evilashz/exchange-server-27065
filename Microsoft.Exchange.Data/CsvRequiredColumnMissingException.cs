using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000E3 RID: 227
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CsvRequiredColumnMissingException : CsvValidationException
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x0001AE7C File Offset: 0x0001907C
		public CsvRequiredColumnMissingException(string missingColumn) : base(DataStrings.RequiredColumnMissing(missingColumn))
		{
			this.missingColumn = missingColumn;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001AE91 File Offset: 0x00019091
		public CsvRequiredColumnMissingException(string missingColumn, Exception innerException) : base(DataStrings.RequiredColumnMissing(missingColumn), innerException)
		{
			this.missingColumn = missingColumn;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001AEA7 File Offset: 0x000190A7
		protected CsvRequiredColumnMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.missingColumn = (string)info.GetValue("missingColumn", typeof(string));
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001AED1 File Offset: 0x000190D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("missingColumn", this.missingColumn);
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001AEEC File Offset: 0x000190EC
		public string MissingColumn
		{
			get
			{
				return this.missingColumn;
			}
		}

		// Token: 0x04000585 RID: 1413
		private readonly string missingColumn;
	}
}
