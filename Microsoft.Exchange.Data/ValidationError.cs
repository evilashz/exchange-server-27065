using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	public abstract class ValidationError : ProviderError
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000051ED File Offset: 0x000033ED
		public static ValidationError[] None
		{
			get
			{
				return new ValidationError[0];
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000051F5 File Offset: 0x000033F5
		public ValidationError(LocalizedString description)
		{
			this.description = description;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005204 File Offset: 0x00003404
		public ValidationError(LocalizedString description, string propertyName)
		{
			this.description = description;
			this.propertyName = propertyName;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000521A File Offset: 0x0000341A
		public ValidationError(LocalizedString description, PropertyDefinition propertyDefinition)
		{
			this.description = description;
			if (propertyDefinition != null)
			{
				this.propertyName = propertyDefinition.Name;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005238 File Offset: 0x00003438
		public LocalizedString Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005240 File Offset: 0x00003440
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005248 File Offset: 0x00003448
		public bool Equals(ValidationError other)
		{
			return other != null && string.Equals(this.Description, other.Description, StringComparison.OrdinalIgnoreCase) && string.Equals(this.PropertyName, other.PropertyName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005284 File Offset: 0x00003484
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ValidationError);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005294 File Offset: 0x00003494
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				this.hashCode = ((this.Description ?? string.Empty).ToLowerInvariant().GetHashCode() ^ (this.PropertyName ?? string.Empty).ToLowerInvariant().GetHashCode());
			}
			return this.hashCode;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000052F0 File Offset: 0x000034F0
		public static LocalizedString CombineErrorDescriptions(List<ValidationError> errors)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < errors.Count; i++)
			{
				if (i == errors.Count - 1)
				{
					stringBuilder.Append(errors[i].Description);
				}
				else
				{
					stringBuilder.AppendFormat("{0}\r\n", errors[i].Description);
				}
			}
			return new LocalizedString(stringBuilder.ToString());
		}

		// Token: 0x04000055 RID: 85
		private readonly string propertyName;

		// Token: 0x04000056 RID: 86
		private LocalizedString description;

		// Token: 0x04000057 RID: 87
		private int hashCode;
	}
}
