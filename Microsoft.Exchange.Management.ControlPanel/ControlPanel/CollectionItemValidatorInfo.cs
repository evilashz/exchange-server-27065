using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006E9 RID: 1769
	[KnownType(typeof(UriKindValidatorInfo))]
	[KnownType(typeof(NoLeadingOrTrailingWhitespaceValidatorInfo))]
	[KnownType(typeof(NotNullOrEmptyValidatorInfo))]
	[KnownType(typeof(CharacterRegexConstraintValidatorInfo))]
	[KnownType(typeof(AsciiCharactersOnlyValidatorInfo))]
	[KnownType(typeof(CharactersConstraintValidatorInfo))]
	[KnownType(typeof(ADObjectNameCharacterValidatorInfo))]
	[KnownType(typeof(ComputerNameCharacterValidatorInfo))]
	[KnownType(typeof(ContainingNonWhitespaceValidatorInfo))]
	[KnownType(typeof(ADObjectNameStringLengthValidatorInfo))]
	[KnownType(typeof(NoTrailingSpecificCharacterValidatorInfo))]
	[KnownType(typeof(StringLengthValidatorInfo))]
	[KnownType(typeof(StringLengthValidatorExInfo))]
	[KnownType(typeof(RangeNumberValidatorInfo))]
	[KnownType(typeof(RegexValidatorInfo))]
	[KnownType(typeof(CompareStringValidatorInfo))]
	[DataContract]
	public class CollectionItemValidatorInfo : CollectionValidatorInfo
	{
		// Token: 0x06004A6C RID: 19052 RVA: 0x000E384B File Offset: 0x000E1A4B
		public CollectionItemValidatorInfo() : base("CollectionItemValidator")
		{
		}

		// Token: 0x17002828 RID: 10280
		// (get) Token: 0x06004A6D RID: 19053 RVA: 0x000E3858 File Offset: 0x000E1A58
		// (set) Token: 0x06004A6E RID: 19054 RVA: 0x000E3860 File Offset: 0x000E1A60
		[DataMember]
		public ValidatorInfo ItemValidator { get; set; }
	}
}
