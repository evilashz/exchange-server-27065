using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200082C RID: 2092
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ICalOutboundContext : ICalContext
	{
		// Token: 0x06004DEB RID: 19947 RVA: 0x00145B1C File Offset: 0x00143D1C
		internal ICalOutboundContext(Charset charset, IList<LocalizedString> errorStream, OutboundAddressCache addressCache, OutboundConversionOptions options, CalendarWriter calendarWriter, string calendarName, ReadOnlyCollection<AttachmentLink> attachmentLinks, bool suppressExceptionAndAttachmentDemotion) : base(charset, errorStream, addressCache)
		{
			Util.ThrowOnNullArgument(options, "options");
			Util.ThrowOnNullOrEmptyArgument(options.ImceaEncapsulationDomain, "options.ImceaEncapsulationDomain");
			Util.ThrowOnNullArgument(calendarWriter, "calendarWriter");
			this.Options = options;
			this.Writer = calendarWriter;
			this.CalendarName = calendarName;
			this.AttachmentLinks = attachmentLinks;
			this.SuppressExceptionAndAttachmentDemotion = suppressExceptionAndAttachmentDemotion;
		}

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06004DEC RID: 19948 RVA: 0x00145B83 File Offset: 0x00143D83
		internal OutboundAddressCache AddressCache
		{
			get
			{
				return (OutboundAddressCache)base.BaseAddressCache;
			}
		}

		// Token: 0x04002A94 RID: 10900
		internal readonly OutboundConversionOptions Options;

		// Token: 0x04002A95 RID: 10901
		internal readonly CalendarWriter Writer;

		// Token: 0x04002A96 RID: 10902
		internal readonly ReadOnlyCollection<AttachmentLink> AttachmentLinks;

		// Token: 0x04002A97 RID: 10903
		internal readonly bool SuppressExceptionAndAttachmentDemotion;
	}
}
