using System;
using System.IO;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class Pop3Email : DisposeTrackableBase
	{
		// Token: 0x0600009E RID: 158 RVA: 0x000055B8 File Offset: 0x000037B8
		internal Pop3Email(ILog log, ExDateTime receivedTime, Stream mimeStream)
		{
			this.log = log;
			this.receivedTime = receivedTime;
			this.mimeStream = mimeStream;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000055D5 File Offset: 0x000037D5
		public ILog Log
		{
			get
			{
				return this.log;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000055DD File Offset: 0x000037DD
		public bool? IsRead
		{
			get
			{
				return new bool?(false);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000055E8 File Offset: 0x000037E8
		public MessageResponseType? MessageResponseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000055FE File Offset: 0x000037FE
		public string From
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005601 File Offset: 0x00003801
		public string Subject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005604 File Offset: 0x00003804
		public ExDateTime? ReceivedTime
		{
			get
			{
				return new ExDateTime?(this.receivedTime);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005611 File Offset: 0x00003811
		public string MessageClass
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005614 File Offset: 0x00003814
		public MessageImportance? MessageImportance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000562A File Offset: 0x0000382A
		public string ConversationTopic
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000562D File Offset: 0x0000382D
		public string ConversationIndex
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00005630 File Offset: 0x00003830
		public MessageSensitivity? MessageSensitivity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00005648 File Offset: 0x00003848
		public int? Size
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00005660 File Offset: 0x00003860
		public bool? HasAttachments
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00005678 File Offset: 0x00003878
		public bool? IsDraft
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000568E File Offset: 0x0000388E
		public string InternetMessageId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005694 File Offset: 0x00003894
		public ExDateTime? LastModifiedTime
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000056AA File Offset: 0x000038AA
		public Stream MimeStream
		{
			get
			{
				return this.mimeStream;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000056B2 File Offset: 0x000038B2
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000056B4 File Offset: 0x000038B4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Pop3Email>(this);
		}

		// Token: 0x04000083 RID: 131
		private ILog log;

		// Token: 0x04000084 RID: 132
		private ExDateTime receivedTime;

		// Token: 0x04000085 RID: 133
		private Stream mimeStream;
	}
}
