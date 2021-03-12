using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.Tracking;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002D0 RID: 720
	[DataContract]
	public class MessageTrackingSearchResultRow : BaseRow
	{
		// Token: 0x06002C7C RID: 11388 RVA: 0x00089535 File Offset: 0x00087735
		public MessageTrackingSearchResultRow(MessageTrackingSearchResult messageTrackingSearchResult) : base(new Identity(messageTrackingSearchResult.MessageTrackingReportId.RawIdentity, messageTrackingSearchResult.Subject), messageTrackingSearchResult)
		{
			this.MessageTrackingSearchResult = messageTrackingSearchResult;
		}

		// Token: 0x17001DDF RID: 7647
		// (get) Token: 0x06002C7D RID: 11389 RVA: 0x0008955B File Offset: 0x0008775B
		// (set) Token: 0x06002C7E RID: 11390 RVA: 0x00089563 File Offset: 0x00087763
		public MessageTrackingSearchResult MessageTrackingSearchResult { get; private set; }

		// Token: 0x17001DE0 RID: 7648
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x0008956C File Offset: 0x0008776C
		// (set) Token: 0x06002C80 RID: 11392 RVA: 0x00089579 File Offset: 0x00087779
		[DataMember]
		public string Subject
		{
			get
			{
				return this.MessageTrackingSearchResult.Subject;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DE1 RID: 7649
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x00089580 File Offset: 0x00087780
		// (set) Token: 0x06002C82 RID: 11394 RVA: 0x0008958D File Offset: 0x0008778D
		public DateTime SubmittedUTCDateTime
		{
			get
			{
				return this.MessageTrackingSearchResult.SubmittedDateTime;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DE2 RID: 7650
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x00089594 File Offset: 0x00087794
		// (set) Token: 0x06002C84 RID: 11396 RVA: 0x000895A6 File Offset: 0x000877A6
		[DataMember]
		public string SubmittedDateTime
		{
			get
			{
				return this.MessageTrackingSearchResult.SubmittedDateTime.UtcToUserDateTimeString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DE3 RID: 7651
		// (get) Token: 0x06002C85 RID: 11397 RVA: 0x000895AD File Offset: 0x000877AD
		// (set) Token: 0x06002C86 RID: 11398 RVA: 0x000895C4 File Offset: 0x000877C4
		[DataMember]
		public string RecipientDisplayNames
		{
			get
			{
				return this.MessageTrackingSearchResult.RecipientDisplayNames.StringArrayJoin(";");
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DE4 RID: 7652
		// (get) Token: 0x06002C87 RID: 11399 RVA: 0x000895CB File Offset: 0x000877CB
		// (set) Token: 0x06002C88 RID: 11400 RVA: 0x000895D8 File Offset: 0x000877D8
		[DataMember]
		public string FromDisplayName
		{
			get
			{
				return this.MessageTrackingSearchResult.FromDisplayName;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}
	}
}
