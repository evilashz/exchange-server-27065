using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000E7 RID: 231
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CsvFileIsEmptyException : CsvValidationException
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x0001B0B1 File Offset: 0x000192B1
		public CsvFileIsEmptyException() : base(DataStrings.FileIsEmpty)
		{
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001B0BE File Offset: 0x000192BE
		public CsvFileIsEmptyException(Exception innerException) : base(DataStrings.FileIsEmpty, innerException)
		{
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001B0CC File Offset: 0x000192CC
		protected CsvFileIsEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001B0D6 File Offset: 0x000192D6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
