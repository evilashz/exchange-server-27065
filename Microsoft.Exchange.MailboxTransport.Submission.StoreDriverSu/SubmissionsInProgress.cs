using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000010 RID: 16
	internal sealed class SubmissionsInProgress
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00004D96 File Offset: 0x00002F96
		public SubmissionsInProgress(int capacity)
		{
			this.map = new SynchronizedDictionary<Thread, MailItemSubmitter>(capacity);
		}

		// Token: 0x1700000C RID: 12
		public MailItemSubmitter this[Thread thread]
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

		// Token: 0x06000068 RID: 104 RVA: 0x00004DC7 File Offset: 0x00002FC7
		public void Remove(Thread thread)
		{
			this.map.Remove(thread);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004E44 File Offset: 0x00003044
		public bool DetectHang(TimeSpan limit, out Thread thread, out MailItemSubmitter mailItemSubmitter)
		{
			bool hang = false;
			Thread hangThread = null;
			MailItemSubmitter hangMailItemSubmitter = null;
			DateTime utcNow = DateTime.UtcNow;
			this.map.ForEach((MailItemSubmitter perEntryMailItemSubmitter) => !hang, delegate(Thread perEntryThread, MailItemSubmitter perEntryMailItemSubmitter)
			{
				if (default(DateTime) != perEntryMailItemSubmitter.StartTime && limit < utcNow - perEntryMailItemSubmitter.StartTime)
				{
					hang = true;
					hangThread = perEntryThread;
					hangMailItemSubmitter = perEntryMailItemSubmitter;
				}
			});
			thread = hangThread;
			mailItemSubmitter = hangMailItemSubmitter;
			return hang;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000051B8 File Offset: 0x000033B8
		public XElement GetDiagnosticInfo()
		{
			XElement root = new XElement("CurrentSubmissions");
			this.map.ForEach(null, delegate(Thread thread, MailItemSubmitter mailItemSubmitter)
			{
				SubmissionInfo submissionInfo = mailItemSubmitter.SubmissionInfo;
				string mailboxHopLatency = submissionInfo.MailboxHopLatency;
				Guid mdbGuid = submissionInfo.MdbGuid;
				IPAddress networkAddress = submissionInfo.NetworkAddress;
				DateTime originalCreateTime = submissionInfo.OriginalCreateTime;
				string content = null;
				string content2 = null;
				long num = 0L;
				byte[] array = null;
				MapiSubmissionInfo mapiSubmissionInfo = (MapiSubmissionInfo)mailItemSubmitter.SubmissionInfo;
				num = mapiSubmissionInfo.EventCounter;
				array = mapiSubmissionInfo.EntryId;
				content = mailItemSubmitter.Result.MessageId;
				content2 = mailItemSubmitter.Result.Sender;
				thread.Suspend();
				StackTrace content3;
				try
				{
					content3 = new StackTrace(thread, true);
				}
				finally
				{
					thread.Resume();
				}
				XElement xelement = new XElement("Submission");
				xelement.Add(new object[]
				{
					new XElement("ThreadID", thread.ManagedThreadId),
					new XElement("ConnectionID", mailItemSubmitter.SubmissionConnectionId),
					new XElement("Duration", (default(DateTime) == mailItemSubmitter.StartTime) ? TimeSpan.Zero : (DateTime.UtcNow - mailItemSubmitter.StartTime)),
					new XElement("MailboxServer", submissionInfo.MailboxFqdn),
					new XElement("MailboxServerIP", submissionInfo.NetworkAddress),
					new XElement("MdbName", submissionInfo.DatabaseName),
					new XElement("MdbGuid", submissionInfo.MdbGuid),
					new XElement("OriginalCreationTime", submissionInfo.OriginalCreateTime),
					new XElement("MessageID", content),
					new XElement("Sender", content2),
					new XElement("EventCounter", num),
					new XElement("EntryID", (array == null) ? null : BitConverter.ToString(array)),
					new XElement("Stage", mailItemSubmitter.Stage),
					new XElement("ErrorCode", mailItemSubmitter.ErrorCode),
					new XElement("MessageSize", mailItemSubmitter.MessageSize),
					new XElement("RecipientCount", mailItemSubmitter.RecipientCount),
					new XElement("RpcLatency", mailItemSubmitter.RpcLatency),
					new XElement("StackTrace", content3),
					LatencyFormatter.GetDiagnosticInfo(mailItemSubmitter.LatencyTracker)
				});
				root.Add(xelement);
			});
			return root;
		}

		// Token: 0x04000025 RID: 37
		private SynchronizedDictionary<Thread, MailItemSubmitter> map;
	}
}
