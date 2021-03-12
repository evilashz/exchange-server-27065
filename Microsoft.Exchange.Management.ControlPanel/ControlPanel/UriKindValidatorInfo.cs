using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006E3 RID: 1763
	[DataContract]
	public class UriKindValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A56 RID: 19030 RVA: 0x000E36E8 File Offset: 0x000E18E8
		internal UriKindValidatorInfo(UriKindConstraint constraint) : base("UriKindValidator")
		{
			this.ExpectedUriKind = Enum.GetName(typeof(UriKind), constraint.ExpectedUriKind);
		}

		// Token: 0x17002822 RID: 10274
		// (get) Token: 0x06004A57 RID: 19031 RVA: 0x000E3715 File Offset: 0x000E1915
		// (set) Token: 0x06004A58 RID: 19032 RVA: 0x000E371D File Offset: 0x000E191D
		[DataMember]
		public string ExpectedUriKind { get; set; }
	}
}
