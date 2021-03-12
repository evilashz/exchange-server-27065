using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000075 RID: 117
	internal sealed class MessageSnapshotWriter : SnapshotWriter
	{
		// Token: 0x06000283 RID: 643 RVA: 0x00006949 File Offset: 0x00004B49
		public MessageSnapshotWriter(SnapshotWriterState state)
		{
			this.state = state;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00006958 File Offset: 0x00004B58
		public static string Rfc2047Encode(string value)
		{
			EncodingOptions encodingOptions = new EncodingOptions(Charset.UTF8);
			return MimeCommon.EncodeValue(value, encodingOptions, ValueEncodingStyle.Normal).ToString();
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00006988 File Offset: 0x00004B88
		private static DirectorySecurity Security
		{
			get
			{
				if (MessageSnapshotWriter.security == null)
				{
					DirectorySecurity directorySecurity = new DirectorySecurity();
					directorySecurity.SetAccessRuleProtection(false, false);
					using (WindowsIdentity current = WindowsIdentity.GetCurrent())
					{
						directorySecurity.SetOwner(current.User);
					}
					for (int i = 0; i < MessageSnapshotWriter.DirectoryAccessRules.Length; i++)
					{
						directorySecurity.AddAccessRule(MessageSnapshotWriter.DirectoryAccessRules[i]);
					}
					Interlocked.CompareExchange<DirectorySecurity>(ref MessageSnapshotWriter.security, directorySecurity, null);
				}
				return MessageSnapshotWriter.security;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00006A0C File Offset: 0x00004C0C
		public override void WriteOriginalData(int agentId, string id, string topic, string address, MailItem mailItem)
		{
			if (this.state.IsOriginalWritten || (this.state.OriginalWriterAgentId != null && agentId != this.state.OriginalWriterAgentId))
			{
				return;
			}
			FileMode fileMode = FileMode.Append;
			if (this.state.OriginalWriterAgentId == null)
			{
				this.state.OriginalWriterAgentId = new int?(agentId);
				fileMode = FileMode.Create;
			}
			string pipelineTracingPath = mailItem.PipelineTracingPath;
			if (string.IsNullOrEmpty(pipelineTracingPath))
			{
				return;
			}
			string snapshotFolder = this.GetSnapshotFolder(pipelineTracingPath);
			string originalSnapshotFileName = this.GetOriginalSnapshotFileName(snapshotFolder);
			try
			{
				this.EnsureSnapshotFolderCreated(snapshotFolder);
				using (FileStream fileStream = new FileStream(originalSnapshotFileName, fileMode, FileAccess.Write, FileShare.None))
				{
					if (topic == "OnRcptCommand")
					{
						if (fileMode == FileMode.Create)
						{
							MessageSnapshotWriter.WriteBeginningHeadersToFile(fileStream, id, topic, null);
							MessageSnapshotWriter.WriteSenderHeaderToFile(fileStream, mailItem.FromAddress.ToString());
						}
						MessageSnapshotWriter.WriteReceiverHeaderToFile(fileStream, address);
					}
					else if (topic == "OnEndOfHeaders")
					{
						MessageSnapshotWriter.WriteEndHeadersToFile(fileStream);
						this.WriteMessageHeadersToFile(fileStream, mailItem);
					}
					else if (topic == "OnEndOfData")
					{
						this.AppendBodyToFile(fileStream, mailItem);
						this.state.IsOriginalWritten = true;
					}
					else if (topic == "OnSubmittedMessage")
					{
						this.WriteMessageToFile(fileStream, id, topic, null, mailItem);
						this.state.IsOriginalWritten = true;
					}
				}
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00006BC0 File Offset: 0x00004DC0
		public override void WritePreProcessedData(int agentId, string prefix, string id, string topic, MailItem mailItem)
		{
			if (topic != this.state.LastPreProcessedSnapshotTopic)
			{
				this.state.LastPreProcessedSnapshotTopic = topic;
				this.WriteProcessedData(prefix, id, topic, string.Empty, mailItem);
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public override void WriteProcessedData(string prefix, string id, string topic, string agent, MailItem mailItem)
		{
			try
			{
				if (!mailItem.HasBeenDeferred)
				{
					string pipelineTracingPath = mailItem.PipelineTracingPath;
					if (!string.IsNullOrEmpty(pipelineTracingPath))
					{
						string snapshotFolder = this.GetSnapshotFolder(pipelineTracingPath);
						string nextSnapshotFilePath = this.GetNextSnapshotFilePath(snapshotFolder, topic, prefix);
						this.EnsureSnapshotFolderCreated(snapshotFolder);
						using (FileStream fileStream = new FileStream(nextSnapshotFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
						{
							this.WriteMessageToFile(fileStream, id, topic, agent, mailItem);
						}
					}
				}
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00006C9C File Offset: 0x00004E9C
		private static void WriteBeginningHeadersToFile(FileStream file, string id, string topic, string agent)
		{
			MessageSnapshotWriter.WriteBeginningHeadersToFile(file, id, topic, agent, null);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00006CA8 File Offset: 0x00004EA8
		private static void WriteBeginningHeadersToFile(FileStream file, string id, string topic, string agent, string messageState)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("X-CreatedBy: MessageSnapshot-Begin injected headers\r\n");
			stringBuilder.Append("X-MessageSnapshot-UTC-Time: ");
			stringBuilder.Append(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture));
			stringBuilder.Append("\r\n");
			if (topic == "OnSubmittedMessage" || topic == "OnRoutedMessage" || topic == "OnResolvedMessage" || topic == "OnCategorizedMessage")
			{
				stringBuilder.Append("X-MessageSnapshot-Record-Id: ");
			}
			else
			{
				stringBuilder.Append("X-MessageSnapshot-Protocol-Id: ");
			}
			stringBuilder.Append(id);
			stringBuilder.Append("\r\n");
			if (agent == null)
			{
				stringBuilder.Append("X-MessageSnapshot-Source: Original\r\n");
			}
			else
			{
				string format = (agent.Length == 0) ? "X-MessageSnapshot-Source: {0}" : "X-MessageSnapshot-Source: {0},{1}";
				string value = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					topic,
					agent
				});
				stringBuilder.Append(MessageSnapshotWriter.Rfc2047Encode(value));
				stringBuilder.Append("\r\n");
			}
			if (!string.IsNullOrEmpty(messageState))
			{
				stringBuilder.AppendFormat("X-MessageSnapshot-MessageStatus: {0}\r\n", messageState);
			}
			byte[] bytes = CTSGlobals.AsciiEncoding.GetBytes(stringBuilder.ToString());
			file.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00006DF8 File Offset: 0x00004FF8
		private static void WriteEndHeadersToFile(FileStream file)
		{
			byte[] bytes = CTSGlobals.AsciiEncoding.GetBytes("X-EndOfInjectedXHeaders: MessageSnapshot-End injected headers\r\n");
			file.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00006E20 File Offset: 0x00005020
		private static void WriteSenderHeaderToFile(FileStream file, string address)
		{
			MessageSnapshotWriter.WriteHeaderToFile(file, "X-Sender", address);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00006E2E File Offset: 0x0000502E
		private static void WriteReceiverHeaderToFile(FileStream file, string address)
		{
			MessageSnapshotWriter.WriteHeaderToFile(file, "X-Receiver", address);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00006E3C File Offset: 0x0000503C
		private static void WriteHeaderToFile(FileStream file, string header, string content)
		{
			byte[] bytes = CTSGlobals.AsciiEncoding.GetBytes(MessageSnapshotWriter.Rfc2047Encode(header + ": " + content));
			file.Write(bytes, 0, bytes.Length);
			file.WriteByte(13);
			file.WriteByte(10);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00006E80 File Offset: 0x00005080
		private static void WriteInjectedHeadersToFile(FileStream file, string id, string topic, string agent, string sender, IEnumerable<string> receivers)
		{
			MessageSnapshotWriter.WriteInjectedHeadersToFile(file, id, topic, agent, sender, receivers, null);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00006E90 File Offset: 0x00005090
		private static void WriteInjectedHeadersToFile(FileStream file, string id, string topic, string agent, string sender, IEnumerable<string> receivers, string messageState)
		{
			MessageSnapshotWriter.WriteBeginningHeadersToFile(file, id, topic, agent, messageState);
			if (sender != null)
			{
				MessageSnapshotWriter.WriteSenderHeaderToFile(file, sender);
			}
			if (receivers != null)
			{
				foreach (string address in receivers)
				{
					MessageSnapshotWriter.WriteReceiverHeaderToFile(file, address);
				}
			}
			MessageSnapshotWriter.WriteEndHeadersToFile(file);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00006EFC File Offset: 0x000050FC
		private string GetSnapshotFolder(string pipelineTracingPath)
		{
			return Path.Combine(Path.Combine(pipelineTracingPath, "MessageSnapshots"), this.state.Id);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00006F26 File Offset: 0x00005126
		private string GetOriginalSnapshotFileName(string snapshotFolder)
		{
			return Path.Combine(snapshotFolder, "Original.eml");
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00006F34 File Offset: 0x00005134
		private string GetNextSnapshotFilePath(string snapshotFolder, string topic, string prefix)
		{
			if (topic == "OnSubmittedMessage" && this.state.Sequence < 16777216)
			{
				this.state.Sequence = 16777216;
			}
			int num = ++this.state.Sequence & 16777215;
			string path = string.Format(CultureInfo.InvariantCulture, "{0}{1:D4}.eml", new object[]
			{
				prefix,
				num
			});
			return Path.Combine(snapshotFolder, path);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00006FBC File Offset: 0x000051BC
		private void WriteMessageToFile(FileStream file, string id, string topic, string agent, MailItem mailItem)
		{
			if (mailItem.HasBeenDeferred || mailItem.HasBeenDeleted)
			{
				MessageSnapshotWriter.WriteInjectedHeadersToFile(file, id, topic, agent, null, null, mailItem.HasBeenDeferred ? "Deferred" : "Deleted");
				return;
			}
			string[] array = new string[mailItem.Recipients.Count];
			int num = 0;
			foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
			{
				array[num++] = envelopeRecipient.Address.ToString();
			}
			MessageSnapshotWriter.WriteInjectedHeadersToFile(file, id, topic, agent, mailItem.FromAddress.ToString(), array);
			mailItem.MimeDocument.RootPart.WriteTo(file);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000070A4 File Offset: 0x000052A4
		private void WriteMessageHeadersToFile(FileStream file, MailItem mailItem)
		{
			mailItem.MimeDocument.RootPart.Headers.WriteTo(file);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000070BD File Offset: 0x000052BD
		private void AppendBodyToFile(FileStream file, MailItem mailItem)
		{
			file.WriteByte(13);
			file.WriteByte(10);
			mailItem.MimeDocument.RootPart.WriteTo(file, null, MessageSnapshotWriter.RootHeaderFilter);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000070E8 File Offset: 0x000052E8
		private void EnsureSnapshotFolderCreated(string snapshotFolder)
		{
			if (this.state.IsFolderCreated)
			{
				return;
			}
			try
			{
				Directory.CreateDirectory(Path.Combine(snapshotFolder, ".."), MessageSnapshotWriter.Security);
				Directory.CreateDirectory(snapshotFolder);
				this.state.IsFolderCreated = true;
			}
			catch (IOException)
			{
				this.state.IsFolderCreated = true;
			}
		}

		// Token: 0x040001CD RID: 461
		private const string SnapshotBaseFolderRelative = "MessageSnapshots";

		// Token: 0x040001CE RID: 462
		private const string OriginalSnapshotFileName = "Original.eml";

		// Token: 0x040001CF RID: 463
		private const string ProcessedSnapshotFileNameFormat = "{0}{1:D4}.eml";

		// Token: 0x040001D0 RID: 464
		private static readonly MessageSnapshotWriter.SuppressRootHeadersFilter RootHeaderFilter = new MessageSnapshotWriter.SuppressRootHeadersFilter();

		// Token: 0x040001D1 RID: 465
		private static readonly FileSystemAccessRule[] DirectoryAccessRules = new FileSystemAccessRule[]
		{
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow),
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow),
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null), FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.WriteExtendedAttributes | FileSystemRights.ExecuteFile | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.ReadAttributes | FileSystemRights.WriteAttributes | FileSystemRights.Delete | FileSystemRights.ReadPermissions | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow)
		};

		// Token: 0x040001D2 RID: 466
		private static DirectorySecurity security;

		// Token: 0x040001D3 RID: 467
		private SnapshotWriterState state;

		// Token: 0x02000076 RID: 118
		private sealed class SuppressRootHeadersFilter : MimeOutputFilter
		{
			// Token: 0x06000299 RID: 665 RVA: 0x000071BC File Offset: 0x000053BC
			public override bool FilterHeaderList(HeaderList headerList, Stream stream)
			{
				return headerList.Parent == null || headerList.Parent.Parent == null;
			}
		}
	}
}
