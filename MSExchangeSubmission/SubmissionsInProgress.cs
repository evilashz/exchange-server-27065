using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxTransport.Submission
{
	// Token: 0x0200000C RID: 12
	internal sealed class SubmissionsInProgress
	{
		// Token: 0x06000058 RID: 88 RVA: 0x0000455B File Offset: 0x0000275B
		public SubmissionsInProgress(int capacity)
		{
			this.map = new SynchronizedDictionary<Thread, SubmissionsInProgress.Entry>(capacity);
		}

		// Token: 0x17000013 RID: 19
		public SubmissionsInProgress.Entry this[Thread thread]
		{
			get
			{
				return this.map[thread];
			}
			set
			{
				this.map[thread] = value;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000458C File Offset: 0x0000278C
		public void Remove(Thread thread)
		{
			this.map.Remove(thread);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000465C File Offset: 0x0000285C
		public bool DetectHangAndLog(TimeSpan limit)
		{
			bool hang = false;
			ExDateTime utcNow = ExDateTime.UtcNow;
			this.map.ForEach((SubmissionsInProgress.Entry singleEntry) => default(ExDateTime) != singleEntry.StartTime && limit < utcNow - singleEntry.StartTime, delegate(Thread perEntryThread, SubmissionsInProgress.Entry singleEntry)
			{
				hang = true;
				MailboxTransportSubmissionEventLogger.LogEvent(MSExchangeSubmissionEventLogConstants.Tuple_SubmissionHang, string.Format("SubmissionHang-MDB:{0}", singleEntry.MdbGuid), new object[]
				{
					singleEntry.EventCounter,
					singleEntry.MailboxGuid,
					singleEntry.MdbGuid,
					limit
				});
			});
			return hang;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000049A4 File Offset: 0x00002BA4
		public XElement GetDiagnosticInfo()
		{
			XElement root = new XElement("CurrentSubmissions");
			this.map.ForEach(null, delegate(Thread thread, SubmissionsInProgress.Entry entry)
			{
				thread.Suspend();
				StackTrace content;
				try
				{
					content = new StackTrace(thread, true);
				}
				finally
				{
					thread.Resume();
				}
				XElement xelement = new XElement("Submission");
				xelement.Add(new object[]
				{
					new XElement("ThreadID", thread.ManagedThreadId),
					new XElement("Duration", (default(ExDateTime) == entry.StartTime) ? TimeSpan.Zero : (ExDateTime.UtcNow - entry.StartTime)),
					new XElement("EventCounter", entry.EventCounter),
					new XElement("EntryID", (entry.EntryID == null) ? null : BitConverter.ToString(entry.EntryID)),
					new XElement("DatabaseWatermark", new object[]
					{
						new XElement("EventCounter", (entry.DatabaseWatermark == null) ? 0L : entry.DatabaseWatermark.EventCounter),
						new XElement("MailboxGuid", (entry.DatabaseWatermark == null) ? Guid.Empty : entry.DatabaseWatermark.MailboxGuid)
					}),
					new XElement("MailboxGuid", entry.MailboxGuid),
					new XElement("ObjectClass", entry.ObjectClass),
					new XElement("ClientType", entry.ClientType),
					new XElement("CreateTime", entry.CreateTime),
					new XElement("DocumentID", entry.DocumentID),
					new XElement("EventFlags", entry.EventFlags),
					new XElement("EventMask", entry.EventMask),
					new XElement("ExtendedEventFlags", entry.ExtendedEventFlags),
					new XElement("SecurityID", entry.SecurityID),
					new XElement("Sender", entry.Sender),
					new XElement("MdbGuid", entry.MdbGuid),
					new XElement("StackTrace", content),
					(entry.LatencyTracker == null) ? null : LatencyFormatter.GetDiagnosticInfo(entry.LatencyTracker)
				});
				root.Add(xelement);
			});
			return root;
		}

		// Token: 0x0400005F RID: 95
		private SynchronizedDictionary<Thread, SubmissionsInProgress.Entry> map;

		// Token: 0x0200000D RID: 13
		internal class Entry
		{
			// Token: 0x0600005E RID: 94 RVA: 0x000049EA File Offset: 0x00002BEA
			internal Entry(ExDateTime startTime, Guid mdbGuid, MapiEvent mapiEvent)
			{
				this.StartTime = startTime;
				this.MdbGuid = mdbGuid;
				this.mapiEvent = mapiEvent;
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600005F RID: 95 RVA: 0x00004A07 File Offset: 0x00002C07
			// (set) Token: 0x06000060 RID: 96 RVA: 0x00004A0F File Offset: 0x00002C0F
			internal ExDateTime StartTime { get; private set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000061 RID: 97 RVA: 0x00004A18 File Offset: 0x00002C18
			internal long EventCounter
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.EventCounter;
					}
					return 0L;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000062 RID: 98 RVA: 0x00004A30 File Offset: 0x00002C30
			internal byte[] EntryID
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.ItemEntryId;
					}
					return null;
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000063 RID: 99 RVA: 0x00004A47 File Offset: 0x00002C47
			// (set) Token: 0x06000064 RID: 100 RVA: 0x00004A4F File Offset: 0x00002C4F
			internal Guid MdbGuid { get; private set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000065 RID: 101 RVA: 0x00004A58 File Offset: 0x00002C58
			internal Watermark DatabaseWatermark
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.DatabaseWatermark;
					}
					return null;
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000066 RID: 102 RVA: 0x00004A6F File Offset: 0x00002C6F
			internal Guid MailboxGuid
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.MailboxGuid;
					}
					return Guid.Empty;
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000067 RID: 103 RVA: 0x00004A8A File Offset: 0x00002C8A
			internal string ObjectClass
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.ObjectClass;
					}
					return null;
				}
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000068 RID: 104 RVA: 0x00004AA1 File Offset: 0x00002CA1
			internal MapiEventClientTypes ClientType
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.ClientType;
					}
					return (MapiEventClientTypes)0;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000069 RID: 105 RVA: 0x00004AB8 File Offset: 0x00002CB8
			internal DateTime CreateTime
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.CreateTime;
					}
					return default(DateTime);
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x0600006A RID: 106 RVA: 0x00004AE2 File Offset: 0x00002CE2
			internal int DocumentID
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.DocumentId;
					}
					return 0;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600006B RID: 107 RVA: 0x00004AF9 File Offset: 0x00002CF9
			internal MapiEventFlags EventFlags
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.EventFlags;
					}
					return MapiEventFlags.None;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x0600006C RID: 108 RVA: 0x00004B10 File Offset: 0x00002D10
			internal MapiEventTypeFlags EventMask
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.EventMask;
					}
					return (MapiEventTypeFlags)0;
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x0600006D RID: 109 RVA: 0x00004B27 File Offset: 0x00002D27
			internal MapiExtendedEventFlags ExtendedEventFlags
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.ExtendedEventFlags;
					}
					return MapiExtendedEventFlags.None;
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x0600006E RID: 110 RVA: 0x00004B3F File Offset: 0x00002D3F
			internal SecurityIdentifier SecurityID
			{
				get
				{
					if (this.mapiEvent != null)
					{
						return this.mapiEvent.Sid;
					}
					return null;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x0600006F RID: 111 RVA: 0x00004B56 File Offset: 0x00002D56
			// (set) Token: 0x06000070 RID: 112 RVA: 0x00004B5E File Offset: 0x00002D5E
			internal string Sender { get; private set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000071 RID: 113 RVA: 0x00004B67 File Offset: 0x00002D67
			// (set) Token: 0x06000072 RID: 114 RVA: 0x00004B6F File Offset: 0x00002D6F
			internal LatencyTracker LatencyTracker { get; set; }

			// Token: 0x04000060 RID: 96
			private MapiEvent mapiEvent;
		}
	}
}
