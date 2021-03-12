using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006D4 RID: 1748
	[KnownType(typeof(ADObjectNameCharacterValidatorInfo))]
	[DataContract]
	[KnownType(typeof(RequiredFieldValidatorInfo))]
	[KnownType(typeof(NotNullOrEmptyValidatorInfo))]
	[KnownType(typeof(AsciiCharactersOnlyValidatorInfo))]
	[KnownType(typeof(CharacterRegexConstraintValidatorInfo))]
	[KnownType(typeof(CharactersConstraintValidatorInfo))]
	[KnownType(typeof(ComputerNameCharacterValidatorInfo))]
	[KnownType(typeof(ContainingNonWhitespaceValidatorInfo))]
	[KnownType(typeof(NoLeadingOrTrailingWhitespaceValidatorInfo))]
	[KnownType(typeof(NoTrailingSpecificCharacterValidatorInfo))]
	[KnownType(typeof(StringLengthValidatorInfo))]
	[KnownType(typeof(ADObjectNameStringLengthValidatorInfo))]
	[KnownType(typeof(RegexValidatorInfo))]
	[KnownType(typeof(UriKindValidatorInfo))]
	[KnownType(typeof(RangeNumberValidatorInfo))]
	[KnownType(typeof(CompareStringValidatorInfo))]
	[KnownType(typeof(CollectionValidatorInfo))]
	[KnownType(typeof(CollectionItemValidatorInfo))]
	public class ValidatorInfo
	{
		// Token: 0x06004A21 RID: 18977 RVA: 0x000E33C7 File Offset: 0x000E15C7
		public ValidatorInfo(string typeName)
		{
			this.Type = typeName;
		}

		// Token: 0x17002813 RID: 10259
		// (get) Token: 0x06004A22 RID: 18978 RVA: 0x000E33D6 File Offset: 0x000E15D6
		// (set) Token: 0x06004A23 RID: 18979 RVA: 0x000E33DE File Offset: 0x000E15DE
		[DataMember]
		public string Type { get; private set; }
	}
}
