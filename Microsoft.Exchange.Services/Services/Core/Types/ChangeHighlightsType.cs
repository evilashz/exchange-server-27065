using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005AB RID: 1451
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "ChangeHighlightsType")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ChangeHighlights")]
	[Serializable]
	public class ChangeHighlightsType
	{
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x000AEBE4 File Offset: 0x000ACDE4
		// (set) Token: 0x06002ACE RID: 10958 RVA: 0x000AEBEC File Offset: 0x000ACDEC
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public bool HasLocationChanged { get; set; }

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002ACF RID: 10959 RVA: 0x000AEBF5 File Offset: 0x000ACDF5
		// (set) Token: 0x06002AD0 RID: 10960 RVA: 0x000AEBFD File Offset: 0x000ACDFD
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string Location { get; set; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x000AEC06 File Offset: 0x000ACE06
		// (set) Token: 0x06002AD2 RID: 10962 RVA: 0x000AEC0E File Offset: 0x000ACE0E
		[XmlIgnore]
		[IgnoreDataMember]
		public bool LocationSpecified
		{
			get
			{
				return this.HasLocationChanged;
			}
			set
			{
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x000AEC10 File Offset: 0x000ACE10
		// (set) Token: 0x06002AD4 RID: 10964 RVA: 0x000AEC18 File Offset: 0x000ACE18
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public bool HasStartTimeChanged { get; set; }

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x000AEC21 File Offset: 0x000ACE21
		// (set) Token: 0x06002AD6 RID: 10966 RVA: 0x000AEC29 File Offset: 0x000ACE29
		[DataMember(EmitDefaultValue = false, Order = 4)]
		[DateTimeString]
		public string Start { get; set; }

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x000AEC32 File Offset: 0x000ACE32
		// (set) Token: 0x06002AD8 RID: 10968 RVA: 0x000AEC3A File Offset: 0x000ACE3A
		[XmlIgnore]
		[IgnoreDataMember]
		public bool StartSpecified
		{
			get
			{
				return this.HasStartTimeChanged;
			}
			set
			{
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x000AEC3C File Offset: 0x000ACE3C
		// (set) Token: 0x06002ADA RID: 10970 RVA: 0x000AEC44 File Offset: 0x000ACE44
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public bool HasEndTimeChanged { get; set; }

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000AEC4D File Offset: 0x000ACE4D
		// (set) Token: 0x06002ADC RID: 10972 RVA: 0x000AEC55 File Offset: 0x000ACE55
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public string End { get; set; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x000AEC5E File Offset: 0x000ACE5E
		// (set) Token: 0x06002ADE RID: 10974 RVA: 0x000AEC66 File Offset: 0x000ACE66
		[XmlIgnore]
		[IgnoreDataMember]
		public bool EndSpecified
		{
			get
			{
				return this.HasEndTimeChanged;
			}
			set
			{
			}
		}
	}
}
