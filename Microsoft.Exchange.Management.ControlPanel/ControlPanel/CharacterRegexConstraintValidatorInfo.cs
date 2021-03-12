using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006D7 RID: 1751
	[DataContract]
	public class CharacterRegexConstraintValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A26 RID: 18982 RVA: 0x000E3401 File Offset: 0x000E1601
		internal CharacterRegexConstraintValidatorInfo(CharacterRegexConstraint constraint) : base("CharacterRegexConstraintValidator")
		{
			this.Pattern = constraint.Pattern;
		}

		// Token: 0x17002814 RID: 10260
		// (get) Token: 0x06004A27 RID: 18983 RVA: 0x000E341A File Offset: 0x000E161A
		// (set) Token: 0x06004A28 RID: 18984 RVA: 0x000E3422 File Offset: 0x000E1622
		[DataMember]
		public string Pattern { get; set; }
	}
}
