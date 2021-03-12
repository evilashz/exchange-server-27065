using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200016D RID: 365
	[Serializable]
	internal class LocalLongFullPathLengthConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000C2C RID: 3116 RVA: 0x000259D8 File Offset: 0x00023BD8
		private LocalLongFullPathLengthConstraint(LocalLongFullPathLengthConstraint.LocalLongFullPathLengthValidationType validationType)
		{
			this.validationType = validationType;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000259E8 File Offset: 0x00023BE8
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value != null)
			{
				try
				{
					if (this.validationType == LocalLongFullPathLengthConstraint.LocalLongFullPathLengthValidationType.Directory)
					{
						((LocalLongFullPath)value).ValidateDirectoryPathLength();
					}
					else
					{
						((LocalLongFullPath)value).ValidateFilePathLength();
					}
				}
				catch (FormatException ex)
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationPathLength(value.ToString(), ex.Message), propertyDefinition, value, this);
				}
			}
			return null;
		}

		// Token: 0x04000754 RID: 1876
		private LocalLongFullPathLengthConstraint.LocalLongFullPathLengthValidationType validationType;

		// Token: 0x04000755 RID: 1877
		public static LocalLongFullPathLengthConstraint LocalLongFullDirectoryPathLengthConstraint = new LocalLongFullPathLengthConstraint(LocalLongFullPathLengthConstraint.LocalLongFullPathLengthValidationType.Directory);

		// Token: 0x04000756 RID: 1878
		public static LocalLongFullPathLengthConstraint LocalLongFullFilePathLengthConstraint = new LocalLongFullPathLengthConstraint(LocalLongFullPathLengthConstraint.LocalLongFullPathLengthValidationType.File);

		// Token: 0x0200016E RID: 366
		private enum LocalLongFullPathLengthValidationType
		{
			// Token: 0x04000758 RID: 1880
			Directory,
			// Token: 0x04000759 RID: 1881
			File
		}
	}
}
