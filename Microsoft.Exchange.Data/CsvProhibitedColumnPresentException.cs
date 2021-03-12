using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000E4 RID: 228
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CsvProhibitedColumnPresentException : CsvValidationException
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x0001AEF4 File Offset: 0x000190F4
		public CsvProhibitedColumnPresentException(string prohibitedColumn) : base(DataStrings.ProhibitedColumnPresent(prohibitedColumn))
		{
			this.prohibitedColumn = prohibitedColumn;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001AF09 File Offset: 0x00019109
		public CsvProhibitedColumnPresentException(string prohibitedColumn, Exception innerException) : base(DataStrings.ProhibitedColumnPresent(prohibitedColumn), innerException)
		{
			this.prohibitedColumn = prohibitedColumn;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001AF1F File Offset: 0x0001911F
		protected CsvProhibitedColumnPresentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.prohibitedColumn = (string)info.GetValue("prohibitedColumn", typeof(string));
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001AF49 File Offset: 0x00019149
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("prohibitedColumn", this.prohibitedColumn);
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001AF64 File Offset: 0x00019164
		public string ProhibitedColumn
		{
			get
			{
				return this.prohibitedColumn;
			}
		}

		// Token: 0x04000586 RID: 1414
		private readonly string prohibitedColumn;
	}
}
