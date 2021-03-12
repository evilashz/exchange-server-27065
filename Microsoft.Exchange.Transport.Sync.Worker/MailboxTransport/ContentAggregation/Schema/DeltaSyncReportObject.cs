using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.Protocols.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000053 RID: 83
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncReportObject : ISyncReportObject
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x00010A35 File Offset: 0x0000EC35
		internal DeltaSyncReportObject(DeltaSyncMail deltaSyncMail)
		{
			SyncUtilities.ThrowIfArgumentNull("deltaSyncMail", deltaSyncMail);
			this.deltaSyncMail = deltaSyncMail;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00010A4F File Offset: 0x0000EC4F
		public string FolderName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00010A52 File Offset: 0x0000EC52
		public string Sender
		{
			get
			{
				return this.deltaSyncMail.From;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00010A5F File Offset: 0x0000EC5F
		public string Subject
		{
			get
			{
				return this.deltaSyncMail.Subject;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00010A6C File Offset: 0x0000EC6C
		public string MessageClass
		{
			get
			{
				return this.deltaSyncMail.MessageClass;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00010A79 File Offset: 0x0000EC79
		public int? MessageSize
		{
			get
			{
				return new int?(this.deltaSyncMail.Size);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00010A8C File Offset: 0x0000EC8C
		public ExDateTime? DateSent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00010AA2 File Offset: 0x0000ECA2
		public ExDateTime? DateReceived
		{
			get
			{
				return new ExDateTime?(this.deltaSyncMail.DateReceived);
			}
		}

		// Token: 0x040001EE RID: 494
		private DeltaSyncMail deltaSyncMail;
	}
}
