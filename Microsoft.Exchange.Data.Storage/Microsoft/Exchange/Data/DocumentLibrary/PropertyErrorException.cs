using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006CC RID: 1740
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PropertyErrorException : DocumentLibraryException
	{
		// Token: 0x06004590 RID: 17808 RVA: 0x00127CD5 File Offset: 0x00125ED5
		internal PropertyErrorException(PropertyError propertyError) : this(propertyError, null)
		{
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x00127CDF File Offset: 0x00125EDF
		internal PropertyErrorException(PropertyError propertyError, Exception innerException) : base(propertyError.PropertyErrorDescription, innerException)
		{
			this.propertyError = propertyError;
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x00127CF5 File Offset: 0x00125EF5
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyError.PropertyDefinition;
			}
		}

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x06004593 RID: 17811 RVA: 0x00127D02 File Offset: 0x00125F02
		public PropertyErrorCode PropertyErrorCode
		{
			get
			{
				return this.propertyError.PropertyErrorCode;
			}
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x00127D10 File Offset: 0x00125F10
		internal static PropertyErrorException GetExceptionFromError(PropertyError propertyError)
		{
			switch (propertyError.PropertyErrorCode)
			{
			case PropertyErrorCode.NotFound:
				return new PropertyErrorNotFoundException(propertyError);
			case PropertyErrorCode.NotSupported:
				return new PropertyErrorNotSupportedException(propertyError);
			case PropertyErrorCode.CorruptData:
				return new PropertyErrorCorruptDataException(propertyError);
			}
			return new PropertyErrorException(propertyError);
		}

		// Token: 0x04002617 RID: 9751
		private readonly PropertyError propertyError;
	}
}
