using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000239 RID: 569
	internal class StreamLogItem : IDisposable
	{
		// Token: 0x0600107A RID: 4218 RVA: 0x0004AEF8 File Offset: 0x000490F8
		internal StreamLogItem(Referenced<MailboxSession> mailboxSession, StoreId messageId, StoreId folderId, string subject, string attachmentName)
		{
			if (mailboxSession == null || mailboxSession.Value == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (messageId == null)
			{
				this.messageItem = MessageItem.Create(mailboxSession, folderId);
				this.messageItem.IsDraft = false;
			}
			else
			{
				this.messageItem = MessageItem.Bind(mailboxSession, messageId, null);
			}
			this.messageItem.Subject = subject;
			this.messageItem.ClassName = "IPM.Note.Microsoft.Exchange.Search.Log";
			this.attachmentName = attachmentName;
			this.mailboxSession = mailboxSession.Reacquire();
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x0004AFA1 File Offset: 0x000491A1
		internal MessageItem MessageItem
		{
			get
			{
				return this.messageItem;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x0004AFA9 File Offset: 0x000491A9
		private string AttachmentName
		{
			get
			{
				return this.attachmentName;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0004AFB1 File Offset: 0x000491B1
		private StreamAttachment Attachment
		{
			get
			{
				return this.attachment;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0004AFB9 File Offset: 0x000491B9
		private StreamWriter AttachmentStream
		{
			get
			{
				return this.attachmentStream;
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0004AFC4 File Offset: 0x000491C4
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (this.Attachment != null)
				{
					this.CloseOpenedStream();
					this.Save(false);
				}
				if (this.MessageItem != null)
				{
					this.MessageItem.Dispose();
					this.messageItem = null;
				}
				this.mailboxSession.Dispose();
				this.mailboxSession = null;
				this.disposed = true;
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0004B048 File Offset: 0x00049248
		internal void WriteLogs(List<StreamLogItem.LogItem> logList)
		{
			lock (this.mailboxSession.Value)
			{
				foreach (StreamLogItem.LogItem logItem in logList)
				{
					StreamLogItem.LogItemAttachment subAttachment = null;
					this.subAttachments.TryGetValue(logItem.WorkerId, out subAttachment);
					if (subAttachment == null)
					{
						subAttachment = this.InitializeSubAttachment(logItem.WorkerId);
						this.subAttachments.Add(logItem.WorkerId, subAttachment);
					}
					logItem.Logs.ForEach(delegate(LocalizedString x)
					{
						subAttachment.AttachmentStream.WriteLine(x.ToString());
					});
				}
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0004B12C File Offset: 0x0004932C
		internal void Save(bool reload)
		{
			lock (this.mailboxSession.Value)
			{
				try
				{
					ConflictResolutionResult conflictResolutionResult = this.MessageItem.Save(SaveMode.NoConflictResolutionForceSave);
					if (conflictResolutionResult.SaveStatus != SaveResult.Success)
					{
						StreamLogItem.Tracer.TraceError<SaveResult>((long)this.GetHashCode(), "Log item is saved with status {0}", conflictResolutionResult.SaveStatus);
					}
				}
				catch (MessageSubmissionExceededException)
				{
					StreamLogItem.Tracer.TraceError((long)this.GetHashCode(), "Log item is too large");
				}
				if (reload)
				{
					this.MessageItem.Load();
				}
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0004B1D4 File Offset: 0x000493D4
		internal void CloseOpenedStream()
		{
			if (this.Attachment != null)
			{
				lock (this.mailboxSession.Value)
				{
					this.RemoveAllSubAttachments(false);
					if (this.AttachmentStream != null)
					{
						this.AttachmentStream.Dispose();
						this.attachmentStream = null;
					}
					if (this.logPackage != null)
					{
						this.logPackage.Close();
						this.logPackage = null;
					}
					this.Attachment.Save();
					this.Attachment.Dispose();
					this.attachment = null;
				}
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0004B274 File Offset: 0x00049474
		internal void ConsolidateLog(int workerId, bool merge)
		{
			lock (this.mailboxSession.Value)
			{
				if (this.subAttachments.Count != 0)
				{
					if (this.Attachment == null)
					{
						this.InitializeMainAttachment();
					}
					StreamLogItem.LogItemAttachment logItemAttachment = null;
					this.subAttachments.TryGetValue(workerId, out logItemAttachment);
					if (logItemAttachment != null)
					{
						this.RemoveSubAttachment(logItemAttachment, merge);
					}
				}
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0004B2EC File Offset: 0x000494EC
		private string GetAttachmentFileName(MessageItem messageItem)
		{
			string str;
			if (!this.AttachmentName.IsNullOrEmpty())
			{
				str = this.AttachmentName;
			}
			else if (!messageItem.Subject.IsNullOrEmpty())
			{
				str = messageItem.Subject;
			}
			else
			{
				str = "Search Results";
			}
			return str + ".csv";
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0004B338 File Offset: 0x00049538
		private void InitializeMainAttachment()
		{
			this.attachment = (StreamAttachment)this.MessageItem.AttachmentCollection.Create(AttachmentType.Stream);
			string attachmentFileName = this.GetAttachmentFileName(this.MessageItem);
			this.Attachment.FileName = attachmentFileName + ".zip";
			this.Attachment[AttachmentSchema.DisplayName] = this.Attachment.FileName;
			Stream contentStream = this.Attachment.GetContentStream();
			this.logPackage = Package.Open(contentStream, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			string uriString = "/" + Uri.EscapeDataString(attachmentFileName);
			PackagePart packagePart = this.logPackage.CreatePart(new Uri(uriString, UriKind.Relative), "application/zip", CompressionOption.Maximum);
			this.attachmentStream = new StreamWriter(packagePart.GetStream(FileMode.Create, FileAccess.Write));
			this.AttachmentStream.WriteLine(Strings.SearchLogHeader);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0004B40C File Offset: 0x0004960C
		private StreamLogItem.LogItemAttachment InitializeSubAttachment(int workerId)
		{
			string text = workerId.ToString();
			StreamLogItem.LogItemAttachment logItemAttachment = new StreamLogItem.LogItemAttachment();
			logItemAttachment.Id = workerId;
			logItemAttachment.Name = text;
			logItemAttachment.Attachment = (StreamAttachment)this.MessageItem.AttachmentCollection.Create(AttachmentType.Stream);
			logItemAttachment.Attachment.FileName = text + ".csv";
			logItemAttachment.Attachment[AttachmentSchema.DisplayName] = text;
			logItemAttachment.AttachmentStream = new StreamWriter(new GZipStream(logItemAttachment.Attachment.GetContentStream(), CompressionMode.Compress));
			return logItemAttachment;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0004B498 File Offset: 0x00049698
		private void RemoveAllSubAttachments(bool merge)
		{
			List<StreamLogItem.LogItemAttachment> list = new List<StreamLogItem.LogItemAttachment>();
			list.AddRange(this.subAttachments.Values);
			foreach (StreamLogItem.LogItemAttachment subAttachment in list)
			{
				this.RemoveSubAttachment(subAttachment, merge);
			}
			this.subAttachments.Clear();
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0004B50C File Offset: 0x0004970C
		private void RemoveSubAttachment(StreamLogItem.LogItemAttachment subAttachment, bool merge)
		{
			Stream baseStream = subAttachment.AttachmentStream.BaseStream;
			subAttachment.AttachmentStream.Flush();
			subAttachment.AttachmentStream.Dispose();
			baseStream.Dispose();
			subAttachment.Attachment.Save();
			subAttachment.Attachment.Load();
			if (merge)
			{
				using (StreamAttachment streamAttachment = (StreamAttachment)this.MessageItem.AttachmentCollection.Open(subAttachment.Attachment.Id))
				{
					using (GZipStream gzipStream = new GZipStream(streamAttachment.GetContentStream(), CompressionMode.Decompress))
					{
						using (StreamReader streamReader = new StreamReader(gzipStream))
						{
							string value;
							while ((value = streamReader.ReadLine()) != null)
							{
								this.AttachmentStream.WriteLine(value);
							}
						}
					}
				}
			}
			this.MessageItem.AttachmentCollection.Remove(subAttachment.Attachment.Id);
			this.MessageItem.Save(SaveMode.NoConflictResolutionForceSave);
			this.MessageItem.Load();
			int id = subAttachment.Id;
			subAttachment.AttachmentStream.Dispose();
			subAttachment.AttachmentStream = null;
			subAttachment.Attachment.Dispose();
			subAttachment.Attachment = null;
			subAttachment = null;
			this.subAttachments.Remove(id);
		}

		// Token: 0x04000B25 RID: 2853
		protected static readonly Trace Tracer = ExTraceGlobals.SearchTracer;

		// Token: 0x04000B26 RID: 2854
		private bool disposed;

		// Token: 0x04000B27 RID: 2855
		private string attachmentName;

		// Token: 0x04000B28 RID: 2856
		private Referenced<MailboxSession> mailboxSession;

		// Token: 0x04000B29 RID: 2857
		private MessageItem messageItem;

		// Token: 0x04000B2A RID: 2858
		private StreamAttachment attachment;

		// Token: 0x04000B2B RID: 2859
		private StreamWriter attachmentStream;

		// Token: 0x04000B2C RID: 2860
		private Dictionary<int, StreamLogItem.LogItemAttachment> subAttachments = new Dictionary<int, StreamLogItem.LogItemAttachment>();

		// Token: 0x04000B2D RID: 2861
		private Package logPackage;

		// Token: 0x0200023A RID: 570
		internal class LogItem
		{
			// Token: 0x0600108A RID: 4234 RVA: 0x0004B678 File Offset: 0x00049878
			internal LogItem(int workerId, IEnumerable<LocalizedString> logs)
			{
				this.WorkerId = workerId;
				this.Logs = logs;
			}

			// Token: 0x1700045B RID: 1115
			// (get) Token: 0x0600108B RID: 4235 RVA: 0x0004B68E File Offset: 0x0004988E
			// (set) Token: 0x0600108C RID: 4236 RVA: 0x0004B696 File Offset: 0x00049896
			internal int WorkerId { get; private set; }

			// Token: 0x1700045C RID: 1116
			// (get) Token: 0x0600108D RID: 4237 RVA: 0x0004B69F File Offset: 0x0004989F
			// (set) Token: 0x0600108E RID: 4238 RVA: 0x0004B6A7 File Offset: 0x000498A7
			internal IEnumerable<LocalizedString> Logs { get; private set; }
		}

		// Token: 0x0200023B RID: 571
		private class LogItemAttachment
		{
			// Token: 0x1700045D RID: 1117
			// (get) Token: 0x0600108F RID: 4239 RVA: 0x0004B6B0 File Offset: 0x000498B0
			// (set) Token: 0x06001090 RID: 4240 RVA: 0x0004B6B8 File Offset: 0x000498B8
			internal int Id { get; set; }

			// Token: 0x1700045E RID: 1118
			// (get) Token: 0x06001091 RID: 4241 RVA: 0x0004B6C1 File Offset: 0x000498C1
			// (set) Token: 0x06001092 RID: 4242 RVA: 0x0004B6C9 File Offset: 0x000498C9
			internal string Name { get; set; }

			// Token: 0x1700045F RID: 1119
			// (get) Token: 0x06001093 RID: 4243 RVA: 0x0004B6D2 File Offset: 0x000498D2
			// (set) Token: 0x06001094 RID: 4244 RVA: 0x0004B6DA File Offset: 0x000498DA
			internal StreamAttachment Attachment { get; set; }

			// Token: 0x17000460 RID: 1120
			// (get) Token: 0x06001095 RID: 4245 RVA: 0x0004B6E3 File Offset: 0x000498E3
			// (set) Token: 0x06001096 RID: 4246 RVA: 0x0004B6EB File Offset: 0x000498EB
			internal StreamWriter AttachmentStream { get; set; }
		}
	}
}
