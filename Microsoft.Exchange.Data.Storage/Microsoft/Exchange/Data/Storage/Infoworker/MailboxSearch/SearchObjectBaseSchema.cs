using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D28 RID: 3368
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SearchObjectBaseSchema : ObjectSchema
	{
		// Token: 0x04005132 RID: 20786
		internal static readonly char[] NameReservedChars = new char[]
		{
			'*',
			'?',
			'\\',
			'/'
		};

		// Token: 0x04005133 RID: 20787
		public static readonly ADPropertyDefinition Id = new ADPropertyDefinition("Id", ExchangeObjectVersion.Exchange2007, typeof(SearchObjectId), "distinguishedName", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005134 RID: 20788
		public static readonly ADPropertyDefinition ExchangeVersion = new ADPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2007, typeof(ExchangeObjectVersion), "exchangeVersion", ADPropertyDefinitionFlags.DoNotProvisionalClone, ExchangeObjectVersion.Exchange2007, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005135 RID: 20789
		public static readonly ADPropertyDefinition ObjectState = new ADPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2007, typeof(ObjectState), "objectState", ADPropertyDefinitionFlags.ReadOnly, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005136 RID: 20790
		public static readonly ADPropertyDefinition Name = new ADPropertyDefinition("Name", ExchangeObjectVersion.Exchange2007, typeof(string), "name", ADPropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NotNullOrEmptyConstraint(),
			new NoSurroundingWhiteSpaceConstraint(),
			new CharacterConstraint(SearchObjectBaseSchema.NameReservedChars, false),
			new StringLengthConstraint(1, 64)
		}, null, null);
	}
}
