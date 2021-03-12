using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005DA RID: 1498
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class FlagType
	{
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002D1A RID: 11546 RVA: 0x000B1CD3 File Offset: 0x000AFED3
		// (set) Token: 0x06002D1B RID: 11547 RVA: 0x000B1CDB File Offset: 0x000AFEDB
		[IgnoreDataMember]
		[XmlElement]
		public FlagStatus FlagStatus { get; set; }

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000B1CE4 File Offset: 0x000AFEE4
		// (set) Token: 0x06002D1D RID: 11549 RVA: 0x000B1CF1 File Offset: 0x000AFEF1
		[DataMember(Name = "FlagStatus", Order = 1)]
		[XmlIgnore]
		public string FlagStatusString
		{
			get
			{
				return EnumUtilities.ToString<FlagStatus>(this.FlagStatus);
			}
			set
			{
				this.FlagStatus = EnumUtilities.Parse<FlagStatus>(value);
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x000B1CFF File Offset: 0x000AFEFF
		// (set) Token: 0x06002D1F RID: 11551 RVA: 0x000B1D07 File Offset: 0x000AFF07
		[DataMember(EmitDefaultValue = false, Order = 2)]
		[DateTimeString]
		public string StartDate { get; set; }

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002D20 RID: 11552 RVA: 0x000B1D10 File Offset: 0x000AFF10
		// (set) Token: 0x06002D21 RID: 11553 RVA: 0x000B1D18 File Offset: 0x000AFF18
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string DueDate { get; set; }

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002D22 RID: 11554 RVA: 0x000B1D21 File Offset: 0x000AFF21
		// (set) Token: 0x06002D23 RID: 11555 RVA: 0x000B1D29 File Offset: 0x000AFF29
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string CompleteDate { get; set; }

		// Token: 0x06002D24 RID: 11556 RVA: 0x000B1D34 File Offset: 0x000AFF34
		public bool IsValid()
		{
			switch (this.FlagStatus)
			{
			case FlagStatus.NotFlagged:
				return string.IsNullOrEmpty(this.StartDate) && string.IsNullOrEmpty(this.DueDate) && string.IsNullOrEmpty(this.CompleteDate);
			case FlagStatus.Complete:
				return string.IsNullOrEmpty(this.StartDate) && string.IsNullOrEmpty(this.DueDate);
			case FlagStatus.Flagged:
				return string.IsNullOrEmpty(this.CompleteDate);
			default:
				return false;
			}
		}
	}
}
