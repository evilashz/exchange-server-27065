using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000162 RID: 354
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationCSVParsingException : MigrationPermanentException
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x0006F461 File Offset: 0x0006D661
		public MigrationCSVParsingException(int rowIndex, string errorMessage) : base(Strings.ErrorParsingCSV(rowIndex, errorMessage))
		{
			this.rowIndex = rowIndex;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0006F47E File Offset: 0x0006D67E
		public MigrationCSVParsingException(int rowIndex, string errorMessage, Exception innerException) : base(Strings.ErrorParsingCSV(rowIndex, errorMessage), innerException)
		{
			this.rowIndex = rowIndex;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0006F49C File Offset: 0x0006D69C
		protected MigrationCSVParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.rowIndex = (int)info.GetValue("rowIndex", typeof(int));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0006F4F1 File Offset: 0x0006D6F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("rowIndex", this.rowIndex);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0006F51D File Offset: 0x0006D71D
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0006F525 File Offset: 0x0006D725
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04000AF4 RID: 2804
		private readonly int rowIndex;

		// Token: 0x04000AF5 RID: 2805
		private readonly string errorMessage;
	}
}
