using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000678 RID: 1656
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TransitionTargetType
	{
		// Token: 0x060032DD RID: 13021 RVA: 0x000B8201 File Offset: 0x000B6401
		public TransitionTargetType()
		{
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000B8209 File Offset: 0x000B6409
		public TransitionTargetType(TransitionTargetKindType kind, string target)
		{
			this.Kind = kind;
			this.Value = target;
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000B821F File Offset: 0x000B641F
		// (set) Token: 0x060032E0 RID: 13024 RVA: 0x000B8227 File Offset: 0x000B6427
		[XmlAttribute]
		[IgnoreDataMember]
		public TransitionTargetKindType Kind { get; set; }

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x000B8230 File Offset: 0x000B6430
		// (set) Token: 0x060032E2 RID: 13026 RVA: 0x000B8242 File Offset: 0x000B6442
		[DataMember(Name = "Kind", EmitDefaultValue = false, Order = 0)]
		[XmlIgnore]
		public string KindString
		{
			get
			{
				return this.Kind.ToString();
			}
			set
			{
				this.Kind = (TransitionTargetKindType)Enum.Parse(typeof(TransitionTargetKindType), value);
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060032E3 RID: 13027 RVA: 0x000B825F File Offset: 0x000B645F
		// (set) Token: 0x060032E4 RID: 13028 RVA: 0x000B8267 File Offset: 0x000B6467
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlText]
		public string Value { get; set; }
	}
}
