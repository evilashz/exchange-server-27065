using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	internal class PropertyConstraintViolationError : PropertyValidationError
	{
		// Token: 0x0600011B RID: 283 RVA: 0x000054E9 File Offset: 0x000036E9
		public PropertyConstraintViolationError(LocalizedString description, PropertyDefinition propertyDefinition, object invalidData, PropertyDefinitionConstraint constraint) : base(description, propertyDefinition, invalidData)
		{
			if (constraint == null)
			{
				throw new ArgumentNullException("constraint");
			}
			this.constraint = constraint;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000550B File Offset: 0x0000370B
		public PropertyDefinitionConstraint Constraint
		{
			get
			{
				return this.constraint;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005513 File Offset: 0x00003713
		public bool Equals(PropertyConstraintViolationError other)
		{
			return other != null && object.Equals(this.Constraint, other.Constraint) && base.Equals(other);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005536 File Offset: 0x00003736
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PropertyConstraintViolationError);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005544 File Offset: 0x00003744
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.Constraint.GetHashCode();
		}

		// Token: 0x0400005E RID: 94
		private PropertyDefinitionConstraint constraint;
	}
}
