using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000014 RID: 20
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnknownColumnsInCsvException : LocalizedException
	{
		// Token: 0x06001872 RID: 6258 RVA: 0x0004B7FF File Offset: 0x000499FF
		public UnknownColumnsInCsvException(string columns) : base(Strings.UnknownColumnsInCsv(columns))
		{
			this.columns = columns;
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0004B814 File Offset: 0x00049A14
		public UnknownColumnsInCsvException(string columns, Exception innerException) : base(Strings.UnknownColumnsInCsv(columns), innerException)
		{
			this.columns = columns;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0004B82A File Offset: 0x00049A2A
		protected UnknownColumnsInCsvException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.columns = (string)info.GetValue("columns", typeof(string));
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0004B854 File Offset: 0x00049A54
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("columns", this.columns);
		}

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0004B86F File Offset: 0x00049A6F
		public string Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x04001854 RID: 6228
		private readonly string columns;
	}
}
