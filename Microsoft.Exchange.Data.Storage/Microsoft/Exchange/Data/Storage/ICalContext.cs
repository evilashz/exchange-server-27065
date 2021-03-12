using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200082A RID: 2090
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ICalContext
	{
		// Token: 0x06004DE5 RID: 19941 RVA: 0x00145A18 File Offset: 0x00143C18
		public ICalContext(Charset charset, IList<LocalizedString> errorStream, ConversionAddressCache addressCache)
		{
			Util.ThrowOnNullArgument(charset, "charset");
			Util.ThrowOnNullArgument(errorStream, "errorStream");
			Util.ThrowOnNullArgument(addressCache, "addressCache");
			this.Charset = charset;
			this.errors = errorStream;
			this.BaseAddressCache = addressCache;
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x00145A73 File Offset: 0x00143C73
		internal void AddError(LocalizedString error)
		{
			this.errors.Add(error);
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06004DE7 RID: 19943 RVA: 0x00145A81 File Offset: 0x00143C81
		// (set) Token: 0x06004DE8 RID: 19944 RVA: 0x00145A89 File Offset: 0x00143C89
		private protected ConversionAddressCache BaseAddressCache { protected get; private set; }

		// Token: 0x04002A89 RID: 10889
		internal readonly Charset Charset;

		// Token: 0x04002A8A RID: 10890
		internal CalendarMethod Method = CalendarMethod.Publish;

		// Token: 0x04002A8B RID: 10891
		internal CalendarType Type;

		// Token: 0x04002A8C RID: 10892
		internal string CalendarName = string.Empty;

		// Token: 0x04002A8D RID: 10893
		private readonly IList<LocalizedString> errors;
	}
}
