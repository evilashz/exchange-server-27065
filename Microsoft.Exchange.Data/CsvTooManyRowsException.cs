using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000E8 RID: 232
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CsvTooManyRowsException : CsvValidationException
	{
		// Token: 0x06000824 RID: 2084 RVA: 0x0001B0E0 File Offset: 0x000192E0
		public CsvTooManyRowsException(int maximumRowCount) : base(DataStrings.TooManyRows(maximumRowCount))
		{
			this.maximumRowCount = maximumRowCount;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001B0F5 File Offset: 0x000192F5
		public CsvTooManyRowsException(int maximumRowCount, Exception innerException) : base(DataStrings.TooManyRows(maximumRowCount), innerException)
		{
			this.maximumRowCount = maximumRowCount;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001B10B File Offset: 0x0001930B
		protected CsvTooManyRowsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.maximumRowCount = (int)info.GetValue("maximumRowCount", typeof(int));
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001B135 File Offset: 0x00019335
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("maximumRowCount", this.maximumRowCount);
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001B150 File Offset: 0x00019350
		public int MaximumRowCount
		{
			get
			{
				return this.maximumRowCount;
			}
		}

		// Token: 0x0400058A RID: 1418
		private readonly int maximumRowCount;
	}
}
