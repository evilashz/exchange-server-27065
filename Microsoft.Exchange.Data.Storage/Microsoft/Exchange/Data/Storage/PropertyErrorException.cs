using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000766 RID: 1894
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropertyErrorException : StoragePermanentException
	{
		// Token: 0x06004889 RID: 18569 RVA: 0x0013126C File Offset: 0x0012F46C
		private PropertyErrorException(params PropertyError[] errors) : base(PropertyErrorException.DescribePropertyErrors(errors))
		{
			this.errors = errors;
		}

		// Token: 0x0600488A RID: 18570 RVA: 0x00131284 File Offset: 0x0012F484
		private PropertyErrorException(LocalizedString message, params PropertyError[] errors) : base(LocalizedString.Join(string.Empty, new object[]
		{
			message,
			PropertyErrorException.DescribePropertyErrors(errors)
		}))
		{
			this.errors = errors;
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x0600488B RID: 18571 RVA: 0x001312C7 File Offset: 0x0012F4C7
		public PropertyError[] PropertyErrors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x001312CF File Offset: 0x0012F4CF
		internal static PropertyErrorException FromPropertyErrorsInternal(params PropertyError[] errors)
		{
			return new PropertyErrorException(errors);
		}

		// Token: 0x0600488D RID: 18573 RVA: 0x001312D7 File Offset: 0x0012F4D7
		internal static PropertyErrorException FromPropertyErrorsInternal(LocalizedString message, params PropertyError[] errors)
		{
			return new PropertyErrorException(message, errors);
		}

		// Token: 0x0600488E RID: 18574 RVA: 0x001312E0 File Offset: 0x0012F4E0
		private static LocalizedString DescribePropertyErrors(PropertyError[] errors)
		{
			object[] array = new object[errors.Length];
			for (int i = 0; i < errors.Length; i++)
			{
				array[i] = errors[i].ToLocalizedString();
			}
			return LocalizedString.Join(", ", array);
		}

		// Token: 0x0400275B RID: 10075
		private readonly PropertyError[] errors;
	}
}
