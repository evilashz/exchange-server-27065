using System;
using System.Security.AccessControl;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000174 RID: 372
	[Serializable]
	internal class NoInheritedACEsConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000C62 RID: 3170 RVA: 0x000265F4 File Offset: 0x000247F4
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			RawSecurityDescriptor rawSecurityDescriptor = (RawSecurityDescriptor)value;
			if (rawSecurityDescriptor == null)
			{
				return null;
			}
			foreach (GenericAce genericAce in rawSecurityDescriptor.DiscretionaryAcl)
			{
				if ((byte)(genericAce.AceFlags & AceFlags.Inherited) == 16)
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationSecurityDescriptorContainsInheritedACEs(rawSecurityDescriptor.GetSddlForm(AccessControlSections.All)), propertyDefinition, value, this);
				}
			}
			return null;
		}
	}
}
