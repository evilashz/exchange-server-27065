using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006AD RID: 1709
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropertyError
	{
		// Token: 0x06004540 RID: 17728 RVA: 0x00126F26 File Offset: 0x00125126
		public PropertyError(PropertyDefinition propertyDefinition, PropertyErrorCode error, string errorDescription)
		{
			this.propertyDefinition = propertyDefinition;
			this.error = error;
			this.errorDescription = errorDescription;
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x00126F43 File Offset: 0x00125143
		public PropertyError(PropertyDefinition propertyDefinition, PropertyErrorCode error) : this(propertyDefinition, error, string.Empty)
		{
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x00126F52 File Offset: 0x00125152
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x00126F5A File Offset: 0x0012515A
		public PropertyErrorCode PropertyErrorCode
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06004544 RID: 17732 RVA: 0x00126F62 File Offset: 0x00125162
		public string PropertyErrorDescription
		{
			get
			{
				return this.errorDescription;
			}
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x00126F6A File Offset: 0x0012516A
		public override string ToString()
		{
			return string.Format("Property = {0}, PropertyErrorCode = {1}, PropertyErrorCode = {2}", (this.PropertyDefinition == null) ? "<null>" : this.PropertyDefinition.ToString(), this.PropertyErrorCode, this.PropertyErrorDescription);
		}

		// Token: 0x040025F0 RID: 9712
		private const string StringValue = "Property = {0}, PropertyErrorCode = {1}, PropertyErrorCode = {2}";

		// Token: 0x040025F1 RID: 9713
		private readonly PropertyDefinition propertyDefinition;

		// Token: 0x040025F2 RID: 9714
		private readonly PropertyErrorCode error;

		// Token: 0x040025F3 RID: 9715
		private readonly string errorDescription;
	}
}
