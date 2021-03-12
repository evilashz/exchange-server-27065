using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000E6 RID: 230
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CsvUnknownColumnsException : CsvValidationException
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x0001AFE4 File Offset: 0x000191E4
		public CsvUnknownColumnsException(string columns, IEnumerable<string> unknownColumns) : base(DataStrings.UnknownColumns(columns, unknownColumns))
		{
			this.columns = columns;
			this.unknownColumns = unknownColumns;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001B001 File Offset: 0x00019201
		public CsvUnknownColumnsException(string columns, IEnumerable<string> unknownColumns, Exception innerException) : base(DataStrings.UnknownColumns(columns, unknownColumns), innerException)
		{
			this.columns = columns;
			this.unknownColumns = unknownColumns;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001B020 File Offset: 0x00019220
		protected CsvUnknownColumnsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.columns = (string)info.GetValue("columns", typeof(string));
			this.unknownColumns = (IEnumerable<string>)info.GetValue("unknownColumns", typeof(IEnumerable<string>));
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001B075 File Offset: 0x00019275
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("columns", this.columns);
			info.AddValue("unknownColumns", this.unknownColumns);
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001B0A1 File Offset: 0x000192A1
		public string Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001B0A9 File Offset: 0x000192A9
		public IEnumerable<string> UnknownColumns
		{
			get
			{
				return this.unknownColumns;
			}
		}

		// Token: 0x04000588 RID: 1416
		private readonly string columns;

		// Token: 0x04000589 RID: 1417
		private readonly IEnumerable<string> unknownColumns;
	}
}
