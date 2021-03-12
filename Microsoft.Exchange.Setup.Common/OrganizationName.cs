using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class OrganizationName : IOrganizationName
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000B0FB File Offset: 0x000092FB
		public OrganizationName(string name)
		{
			this.Initialize(name);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B10A File Offset: 0x0000930A
		public OrganizationName(AdName adName)
		{
			if (null == adName)
			{
				throw new ArgumentNullException("adName");
			}
			this.Initialize(adName.UnescapedName);
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000B132 File Offset: 0x00009332
		protected static char[] ConstrainedCharacters
		{
			get
			{
				return OrganizationName.constrainedCharacters;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000B139 File Offset: 0x00009339
		protected virtual ADPropertyDefinition ADPropertyDefinition
		{
			get
			{
				return OrganizationName.OrgName;
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000B140 File Offset: 0x00009340
		private void Initialize(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new FormatException(Strings.SpecifyExchangeOrganizationName);
			}
			PropertyValidationError propertyValidationError = this.ADPropertyDefinition.ValidateValue(name, false);
			if (propertyValidationError != null)
			{
				throw new FormatException(propertyValidationError.Description);
			}
			this.adName = new AdName("CN", name);
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000B198 File Offset: 0x00009398
		public string UnescapedName
		{
			get
			{
				return this.adName.UnescapedName;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000B1A5 File Offset: 0x000093A5
		public string EscapedName
		{
			get
			{
				return this.adName.EscapedName;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B1B2 File Offset: 0x000093B2
		public override string ToString()
		{
			return this.adName.ToString();
		}

		// Token: 0x040000C2 RID: 194
		private static readonly char[] constrainedCharacters = new char[]
		{
			'~',
			'`',
			'!',
			'@',
			'#',
			'$',
			'%',
			'^',
			'&',
			'*',
			'(',
			')',
			'_',
			'+',
			'=',
			'{',
			'}',
			'[',
			']',
			'|',
			'\\',
			':',
			';',
			'"',
			'\'',
			'<',
			'>',
			',',
			'.',
			'?',
			'/'
		};

		// Token: 0x040000C3 RID: 195
		private static readonly ADPropertyDefinition OrgName = new ADPropertyDefinition("Organization Name", ExchangeObjectVersion.Exchange2003, typeof(string), "Organization Name", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint(),
			new ADObjectNameStringLengthConstraint(1, 64),
			new ContainingNonWhitespaceConstraint(),
			new ADObjectNameCharacterConstraint(OrganizationName.ConstrainedCharacters)
		}, null, null);

		// Token: 0x040000C4 RID: 196
		private AdName adName;
	}
}
