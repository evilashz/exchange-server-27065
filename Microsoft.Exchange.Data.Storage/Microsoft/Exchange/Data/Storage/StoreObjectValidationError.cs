using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EB9 RID: 3769
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StoreObjectValidationError : ValidationError
	{
		// Token: 0x06008259 RID: 33369 RVA: 0x002395B9 File Offset: 0x002377B9
		internal StoreObjectValidationError(ValidationContext context, PropertyDefinition propertyDefinition, object invalidData, StoreObjectConstraint constraint) : base(new LocalizedString(ServerStrings.ExStoreObjectValidationError))
		{
			this.propertyDefinition = propertyDefinition;
			this.invalidData = invalidData;
			this.constraint = constraint;
		}

		// Token: 0x17002289 RID: 8841
		// (get) Token: 0x0600825A RID: 33370 RVA: 0x002395E6 File Offset: 0x002377E6
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x1700228A RID: 8842
		// (get) Token: 0x0600825B RID: 33371 RVA: 0x002395EE File Offset: 0x002377EE
		public object InvalidData
		{
			get
			{
				return this.invalidData;
			}
		}

		// Token: 0x1700228B RID: 8843
		// (get) Token: 0x0600825C RID: 33372 RVA: 0x002395F6 File Offset: 0x002377F6
		public StoreObjectConstraint Constraint
		{
			get
			{
				return this.constraint;
			}
		}

		// Token: 0x0600825D RID: 33373 RVA: 0x002395FE File Offset: 0x002377FE
		public override string ToString()
		{
			return string.Format("Object Violation. Invalid property = {0}. Invalid data for that property = {1}. Constraint violated = {2}.", this.propertyDefinition, this.invalidData, this.constraint);
		}

		// Token: 0x04005777 RID: 22391
		private readonly PropertyDefinition propertyDefinition;

		// Token: 0x04005778 RID: 22392
		private readonly object invalidData;

		// Token: 0x04005779 RID: 22393
		private readonly StoreObjectConstraint constraint;
	}
}
