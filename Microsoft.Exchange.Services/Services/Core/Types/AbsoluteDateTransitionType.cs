using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000677 RID: 1655
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "AbsoluteDateTransition")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AbsoluteDateTransitionType : TransitionType
	{
		// Token: 0x060032D7 RID: 13015 RVA: 0x000B819F File Offset: 0x000B639F
		public AbsoluteDateTransitionType()
		{
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000B81A7 File Offset: 0x000B63A7
		public AbsoluteDateTransitionType(TransitionTargetType to, DateTime dateTime) : base(to)
		{
			this.DateTime = dateTime;
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x000B81B7 File Offset: 0x000B63B7
		// (set) Token: 0x060032DA RID: 13018 RVA: 0x000B81BF File Offset: 0x000B63BF
		[IgnoreDataMember]
		public DateTime DateTime { get; set; }

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x000B81C8 File Offset: 0x000B63C8
		// (set) Token: 0x060032DC RID: 13020 RVA: 0x000B81EE File Offset: 0x000B63EE
		[DataMember(Name = "DateTime", IsRequired = false, EmitDefaultValue = false)]
		[DateTimeString]
		[XmlIgnore]
		public string DateTimeString
		{
			get
			{
				ExDateTime dateTime = (ExDateTime)this.DateTime;
				return ExDateTimeConverter.ToOffsetXsdDateTime(dateTime, dateTime.TimeZone);
			}
			set
			{
				this.DateTime = (DateTime)ExDateTimeConverter.Parse(value);
			}
		}
	}
}
