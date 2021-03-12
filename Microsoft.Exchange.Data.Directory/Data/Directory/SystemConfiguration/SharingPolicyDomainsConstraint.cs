using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200059E RID: 1438
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	internal sealed class SharingPolicyDomainsConstraint : CollectionPropertyDefinitionConstraint
	{
		// Token: 0x060042C7 RID: 17095 RVA: 0x000FB860 File Offset: 0x000F9A60
		public override PropertyConstraintViolationError Validate(IEnumerable value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				return null;
			}
			HashSet<string> hashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			StringBuilder stringBuilder = new StringBuilder(128);
			foreach (object obj in value)
			{
				SharingPolicyDomain sharingPolicyDomain = (SharingPolicyDomain)obj;
				if (hashSet.Contains(sharingPolicyDomain.Domain))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(sharingPolicyDomain.Domain);
				}
				else
				{
					hashSet.Add(sharingPolicyDomain.Domain);
				}
			}
			if (stringBuilder.Length > 0)
			{
				return new PropertyConstraintViolationError(DirectoryStrings.SharingPolicyDuplicateDomain(stringBuilder.ToString()), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x04002D63 RID: 11619
		public static readonly PropertyDefinitionConstraint[] Constrains = new PropertyDefinitionConstraint[]
		{
			new SharingPolicyDomainsConstraint()
		};
	}
}
