using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000E9 RID: 233
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CsvWrongNumberOfColumnsException : CsvValidationException
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x0001B158 File Offset: 0x00019358
		public CsvWrongNumberOfColumnsException(int rowNumber, int expectedColumnCount, int actualColumnCount) : base(DataStrings.WrongNumberOfColumns(rowNumber, expectedColumnCount, actualColumnCount))
		{
			this.rowNumber = rowNumber;
			this.expectedColumnCount = expectedColumnCount;
			this.actualColumnCount = actualColumnCount;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001B17D File Offset: 0x0001937D
		public CsvWrongNumberOfColumnsException(int rowNumber, int expectedColumnCount, int actualColumnCount, Exception innerException) : base(DataStrings.WrongNumberOfColumns(rowNumber, expectedColumnCount, actualColumnCount), innerException)
		{
			this.rowNumber = rowNumber;
			this.expectedColumnCount = expectedColumnCount;
			this.actualColumnCount = actualColumnCount;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001B1A4 File Offset: 0x000193A4
		protected CsvWrongNumberOfColumnsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.rowNumber = (int)info.GetValue("rowNumber", typeof(int));
			this.expectedColumnCount = (int)info.GetValue("expectedColumnCount", typeof(int));
			this.actualColumnCount = (int)info.GetValue("actualColumnCount", typeof(int));
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001B219 File Offset: 0x00019419
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("rowNumber", this.rowNumber);
			info.AddValue("expectedColumnCount", this.expectedColumnCount);
			info.AddValue("actualColumnCount", this.actualColumnCount);
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001B256 File Offset: 0x00019456
		public int RowNumber
		{
			get
			{
				return this.rowNumber;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0001B25E File Offset: 0x0001945E
		public int ExpectedColumnCount
		{
			get
			{
				return this.expectedColumnCount;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001B266 File Offset: 0x00019466
		public int ActualColumnCount
		{
			get
			{
				return this.actualColumnCount;
			}
		}

		// Token: 0x0400058B RID: 1419
		private readonly int rowNumber;

		// Token: 0x0400058C RID: 1420
		private readonly int expectedColumnCount;

		// Token: 0x0400058D RID: 1421
		private readonly int actualColumnCount;
	}
}
