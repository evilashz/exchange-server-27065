using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001DB RID: 475
	[Serializable]
	internal class UriKindConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06001093 RID: 4243 RVA: 0x000322BF File Offset: 0x000304BF
		public UriKindConstraint(UriKind expectedUriKind)
		{
			this.expectedUriKind = expectedUriKind;
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x000322CE File Offset: 0x000304CE
		public UriKind ExpectedUriKind
		{
			get
			{
				return this.expectedUriKind;
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000322D8 File Offset: 0x000304D8
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			Uri uri = value as Uri;
			if (uri != null && !this.IsUriOfExpectedKind(uri))
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationInvalidUriKind(uri, this.expectedUriKind), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00032314 File Offset: 0x00030514
		private bool IsUriOfExpectedKind(Uri uri)
		{
			if (this.expectedUriKind == UriKind.Absolute)
			{
				return uri.IsAbsoluteUri;
			}
			return this.expectedUriKind != UriKind.Relative || !uri.IsAbsoluteUri;
		}

		// Token: 0x040009D6 RID: 2518
		private readonly UriKind expectedUriKind;
	}
}
