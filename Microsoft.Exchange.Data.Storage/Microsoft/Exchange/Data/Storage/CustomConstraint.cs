using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EA9 RID: 3753
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CustomConstraint : StoreObjectConstraint
	{
		// Token: 0x06008224 RID: 33316 RVA: 0x00238C2F File Offset: 0x00236E2F
		internal CustomConstraint(string constraintDescription, PropertyDefinition[] relevantProperties, IsObjectValidDelegate validationDelegate, bool objectIsValidIfDelegateIsTrue) : base(relevantProperties)
		{
			this.constraintDescription = constraintDescription;
			this.validationDelegate = validationDelegate;
			this.propertyDefinition = relevantProperties[0];
			this.objectIsValidIfDelegateIsTrue = objectIsValidIfDelegateIsTrue;
		}

		// Token: 0x06008225 RID: 33317 RVA: 0x00238C57 File Offset: 0x00236E57
		internal CustomConstraint(CustomConstraintDelegateEnum delegateEnum, bool objectIsValidIfDelegateIsTrue) : this(delegateEnum.ToString(), CustomConstraint.delegateDictionary[delegateEnum].RelevantProperties, CustomConstraint.delegateDictionary[delegateEnum].IsObjectValidDelegate, objectIsValidIfDelegateIsTrue)
		{
		}

		// Token: 0x06008226 RID: 33318 RVA: 0x00238C8B File Offset: 0x00236E8B
		internal override StoreObjectValidationError Validate(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			if (this.validationDelegate(context, validatablePropertyBag) == this.objectIsValidIfDelegateIsTrue)
			{
				return null;
			}
			return new StoreObjectValidationError(context, this.propertyDefinition, validatablePropertyBag.TryGetProperty(this.propertyDefinition), this);
		}

		// Token: 0x06008227 RID: 33319 RVA: 0x00238CC0 File Offset: 0x00236EC0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.objectIsValidIfDelegateIsTrue)
			{
				stringBuilder.Append("NOT");
			}
			stringBuilder.AppendFormat("({0})", this.constraintDescription);
			return stringBuilder.ToString();
		}

		// Token: 0x06008228 RID: 33320 RVA: 0x00238D00 File Offset: 0x00236F00
		private static Dictionary<CustomConstraintDelegateEnum, CustomConstraint.CustomConstraintAttributes> CreateDelegateDictionary()
		{
			return new Dictionary<CustomConstraintDelegateEnum, CustomConstraint.CustomConstraintAttributes>
			{
				{
					CustomConstraintDelegateEnum.IsNotConfigurationFolder,
					new CustomConstraint.CustomConstraintAttributes(new IsObjectValidDelegate(Folder.IsNotConfigurationFolder), new PropertyDefinition[]
					{
						FolderSchema.Id
					})
				},
				{
					CustomConstraintDelegateEnum.IsStartDateDefined,
					new CustomConstraint.CustomConstraintAttributes(new IsObjectValidDelegate(Task.IsStartDateDefined), new PropertyDefinition[]
					{
						InternalSchema.StartDate
					})
				},
				{
					CustomConstraintDelegateEnum.DoesFolderHaveFixedDisplayName,
					new CustomConstraint.CustomConstraintAttributes(new IsObjectValidDelegate(Folder.DoesFolderHaveFixedDisplayName), new PropertyDefinition[]
					{
						FolderSchema.Id
					})
				}
			};
		}

		// Token: 0x0400575F RID: 22367
		private readonly IsObjectValidDelegate validationDelegate;

		// Token: 0x04005760 RID: 22368
		private readonly PropertyDefinition propertyDefinition;

		// Token: 0x04005761 RID: 22369
		private readonly string constraintDescription;

		// Token: 0x04005762 RID: 22370
		private readonly bool objectIsValidIfDelegateIsTrue;

		// Token: 0x04005763 RID: 22371
		private static readonly Dictionary<CustomConstraintDelegateEnum, CustomConstraint.CustomConstraintAttributes> delegateDictionary = CustomConstraint.CreateDelegateDictionary();

		// Token: 0x02000EAA RID: 3754
		private struct CustomConstraintAttributes
		{
			// Token: 0x0600822A RID: 33322 RVA: 0x00238D98 File Offset: 0x00236F98
			public CustomConstraintAttributes(IsObjectValidDelegate isObjectValidDelegate, PropertyDefinition[] relevantProperties)
			{
				this.IsObjectValidDelegate = isObjectValidDelegate;
				this.RelevantProperties = relevantProperties;
			}

			// Token: 0x04005764 RID: 22372
			public readonly IsObjectValidDelegate IsObjectValidDelegate;

			// Token: 0x04005765 RID: 22373
			public readonly PropertyDefinition[] RelevantProperties;
		}
	}
}
