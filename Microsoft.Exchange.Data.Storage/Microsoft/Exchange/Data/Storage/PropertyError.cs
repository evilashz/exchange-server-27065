using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000765 RID: 1893
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PropertyError
	{
		// Token: 0x0600487B RID: 18555 RVA: 0x001310A1 File Offset: 0x0012F2A1
		public PropertyError(PropertyDefinition propertyDefinition, PropertyErrorCode error, string errorDescription)
		{
			EnumValidator.ThrowIfInvalid<PropertyErrorCode>(error, "error");
			this.propertyDefinition = propertyDefinition;
			this.error = error;
			this.errorDescription = errorDescription;
		}

		// Token: 0x0600487C RID: 18556 RVA: 0x001310C9 File Offset: 0x0012F2C9
		public PropertyError(PropertyDefinition propertyDefinition, PropertyErrorCode error) : this(propertyDefinition, error, string.Empty)
		{
		}

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x001310D8 File Offset: 0x0012F2D8
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x0600487E RID: 18558 RVA: 0x001310E0 File Offset: 0x0012F2E0
		public PropertyErrorCode PropertyErrorCode
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x0600487F RID: 18559 RVA: 0x001310E8 File Offset: 0x0012F2E8
		public string PropertyErrorDescription
		{
			get
			{
				return this.errorDescription;
			}
		}

		// Token: 0x06004880 RID: 18560 RVA: 0x001310F0 File Offset: 0x0012F2F0
		public static bool IsPropertyNotFound(object propertyValue)
		{
			PropertyError propertyError = propertyValue as PropertyError;
			return propertyError != null && propertyError.PropertyErrorCode == PropertyErrorCode.NotFound;
		}

		// Token: 0x06004881 RID: 18561 RVA: 0x00131114 File Offset: 0x0012F314
		public static bool IsPropertyValueTooBig(object propertyValue)
		{
			PropertyError propertyError = propertyValue as PropertyError;
			return propertyError != null && (propertyError.PropertyErrorCode == PropertyErrorCode.NotEnoughMemory || propertyError.PropertyErrorCode == PropertyErrorCode.RequireStreamed);
		}

		// Token: 0x06004882 RID: 18562 RVA: 0x00131141 File Offset: 0x0012F341
		public static bool IsPropertyError(object propertyValue)
		{
			return propertyValue is PropertyError;
		}

		// Token: 0x06004883 RID: 18563 RVA: 0x0013114C File Offset: 0x0012F34C
		public static Exception ToException(params PropertyError[] propertyErrors)
		{
			return PropertyError.ToException(LocalizedString.Empty, propertyErrors);
		}

		// Token: 0x06004884 RID: 18564 RVA: 0x0013115C File Offset: 0x0012F35C
		public static StoragePermanentException ToException(LocalizedString message, params PropertyError[] propertyErrors)
		{
			foreach (PropertyError propertyError in propertyErrors)
			{
				if (propertyError.PropertyErrorCode == PropertyErrorCode.NotEnoughMemory && propertyError.PropertyDefinition is StorePropertyDefinition && (((StorePropertyDefinition)propertyError.PropertyDefinition).PropertyFlags & PropertyFlags.Streamable) == PropertyFlags.None)
				{
					throw new PropertyTooBigException(propertyError.PropertyDefinition);
				}
			}
			if (!(message != LocalizedString.Empty))
			{
				return PropertyErrorException.FromPropertyErrorsInternal(propertyErrors);
			}
			return PropertyErrorException.FromPropertyErrorsInternal(message, propertyErrors);
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x001311CF File Offset: 0x0012F3CF
		public LocalizedString ToLocalizedString()
		{
			return ServerStrings.PropertyErrorString((this.PropertyDefinition == null) ? "<null>" : this.PropertyDefinition.ToString(), this.PropertyErrorCode, this.PropertyErrorDescription);
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x001311FC File Offset: 0x0012F3FC
		public override string ToString()
		{
			return this.ToLocalizedString().ToString();
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x00131220 File Offset: 0x0012F420
		public override bool Equals(object obj)
		{
			PropertyError propertyError = obj as PropertyError;
			return propertyError != null && this.error == propertyError.error && this.propertyDefinition.Equals(propertyError.propertyDefinition);
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x00131258 File Offset: 0x0012F458
		public override int GetHashCode()
		{
			return (int)(this.error ^ (PropertyErrorCode)this.propertyDefinition.GetHashCode());
		}

		// Token: 0x04002758 RID: 10072
		private readonly PropertyDefinition propertyDefinition;

		// Token: 0x04002759 RID: 10073
		private readonly PropertyErrorCode error;

		// Token: 0x0400275A RID: 10074
		private readonly string errorDescription;
	}
}
