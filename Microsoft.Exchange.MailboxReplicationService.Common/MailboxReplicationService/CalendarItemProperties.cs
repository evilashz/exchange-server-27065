using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000012 RID: 18
	[DataContract]
	internal class CalendarItemProperties : ItemPropertiesBase
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003710 File Offset: 0x00001910
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00003718 File Offset: 0x00001918
		[DataMember(Name = "ICalContents")]
		public string ICalContents { get; set; }

		// Token: 0x06000161 RID: 353 RVA: 0x00003721 File Offset: 0x00001921
		public CalendarItemProperties(string iCalContents)
		{
			this.ICalContents = iCalContents;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00003730 File Offset: 0x00001930
		public override void Apply(MailboxSession session, Item item)
		{
			if (string.IsNullOrEmpty(this.ICalContents))
			{
				return;
			}
			InboundConversionOptions scopedInboundConversionOptions = MapiUtils.GetScopedInboundConversionOptions(session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
			ItemConversion.ConvertICalToItem(item, scopedInboundConversionOptions, this.ICalContents);
		}
	}
}
