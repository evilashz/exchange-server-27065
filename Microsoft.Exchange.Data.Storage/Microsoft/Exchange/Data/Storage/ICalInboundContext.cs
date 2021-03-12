using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200082B RID: 2091
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ICalInboundContext : ICalContext
	{
		// Token: 0x06004DE9 RID: 19945 RVA: 0x00145A94 File Offset: 0x00143C94
		public ICalInboundContext(Charset charset, IList<LocalizedString> errorStream, InboundAddressCache addressCache, InboundConversionOptions options, CalendarReader reader, uint? maxBodyLength, bool hasExceptionPromotion) : base(charset, errorStream, addressCache)
		{
			Util.ThrowOnNullArgument(options, "options");
			if (!options.IgnoreImceaDomain)
			{
				Util.ThrowOnNullOrEmptyArgument(options.ImceaEncapsulationDomain, "options.ImceaEncapsulationDomain");
			}
			Util.ThrowOnNullArgument(reader, "reader");
			this.Options = options;
			this.Reader = reader;
			this.MaxBodyLength = maxBodyLength;
			this.HasExceptionPromotion = hasExceptionPromotion;
			this.DeclaredTimeZones = new Dictionary<string, ExTimeZone>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06004DEA RID: 19946 RVA: 0x00145B0C File Offset: 0x00143D0C
		internal InboundAddressCache AddressCache
		{
			get
			{
				return (InboundAddressCache)base.BaseAddressCache;
			}
		}

		// Token: 0x04002A8F RID: 10895
		internal readonly InboundConversionOptions Options;

		// Token: 0x04002A90 RID: 10896
		internal readonly CalendarReader Reader;

		// Token: 0x04002A91 RID: 10897
		internal readonly uint? MaxBodyLength;

		// Token: 0x04002A92 RID: 10898
		internal readonly bool HasExceptionPromotion;

		// Token: 0x04002A93 RID: 10899
		internal readonly Dictionary<string, ExTimeZone> DeclaredTimeZones;
	}
}
