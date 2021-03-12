using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	public class PropertyValidationError : ValidationError
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00005428 File Offset: 0x00003628
		public PropertyValidationError(LocalizedString description, PropertyDefinition propertyDefinition, object invalidData) : base(description, propertyDefinition)
		{
			this.invalidData = invalidData;
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005440 File Offset: 0x00003640
		public object InvalidData
		{
			get
			{
				return this.invalidData;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005448 File Offset: 0x00003648
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005450 File Offset: 0x00003650
		public bool Equals(PropertyValidationError other)
		{
			return other != null && object.Equals(this.PropertyDefinition, other.PropertyDefinition) && object.Equals(this.InvalidData, other.InvalidData) && base.Equals(other);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005486 File Offset: 0x00003686
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PropertyValidationError);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005494 File Offset: 0x00003694
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				this.hashCode = (base.GetHashCode() ^ ((this.PropertyDefinition == null) ? 0 : this.PropertyDefinition.GetHashCode()) ^ ((this.InvalidData == null) ? 0 : this.InvalidData.GetHashCode()));
			}
			return this.hashCode;
		}

		// Token: 0x0400005B RID: 91
		private object invalidData;

		// Token: 0x0400005C RID: 92
		private PropertyDefinition propertyDefinition;

		// Token: 0x0400005D RID: 93
		private int hashCode;
	}
}
