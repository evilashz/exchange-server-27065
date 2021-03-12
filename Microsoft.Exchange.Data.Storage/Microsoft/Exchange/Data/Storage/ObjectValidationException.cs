using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200075B RID: 1883
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ObjectValidationException : CorruptDataException
	{
		// Token: 0x06004862 RID: 18530 RVA: 0x00130EE5 File Offset: 0x0012F0E5
		public ObjectValidationException(LocalizedString message, IList<StoreObjectValidationError> errors) : base(message)
		{
			this.errors = errors;
		}

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x06004863 RID: 18531 RVA: 0x00130EF5 File Offset: 0x0012F0F5
		public IList<StoreObjectValidationError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0400274F RID: 10063
		private readonly IList<StoreObjectValidationError> errors;
	}
}
