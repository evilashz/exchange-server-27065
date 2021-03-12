using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A93 RID: 2707
	internal class UndeliverableItems
	{
		// Token: 0x06006028 RID: 24616 RVA: 0x00191219 File Offset: 0x0018F419
		public UndeliverableItems(string undeliverableFolderPath, string replayFolderPath, Task.TaskWarningLoggingDelegate warningWriter, Task.TaskErrorLoggingDelegate errorWriter)
		{
			this.undeliverableFolderPath = undeliverableFolderPath;
			this.replayFolderPath = replayFolderPath;
			this.warningWriter = warningWriter;
			this.errorWriter = errorWriter;
		}

		// Token: 0x17001D12 RID: 7442
		// (get) Token: 0x06006029 RID: 24617 RVA: 0x0019123E File Offset: 0x0018F43E
		public string UndeliverableFolderPath
		{
			get
			{
				return this.undeliverableFolderPath;
			}
		}

		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x0600602A RID: 24618 RVA: 0x00191246 File Offset: 0x0018F446
		public string ReplayFolderPath
		{
			get
			{
				return this.replayFolderPath;
			}
		}

		// Token: 0x0600602B RID: 24619 RVA: 0x00191250 File Offset: 0x0018F450
		public List<MalwareFilterRecoveryItem> GetAllItems()
		{
			List<MalwareFilterRecoveryItem> list = new List<MalwareFilterRecoveryItem>();
			string[] files = Directory.GetFiles(this.undeliverableFolderPath, "*.frf");
			foreach (string fileName in files)
			{
				FileInfo fileInfo = new FileInfo(fileName);
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.FullName);
				list.Add(new MalwareFilterRecoveryItem(fileNameWithoutExtension, fileInfo.LastWriteTimeUtc));
			}
			return list;
		}

		// Token: 0x0600602C RID: 24620 RVA: 0x001912B8 File Offset: 0x0018F4B8
		public MalwareFilterRecoveryItem FindItem(string identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentException("FindItem does not accept null or empty string as a parameter.");
			}
			string text = Path.Combine(this.undeliverableFolderPath, identity) + ".frf";
			if (File.Exists(text))
			{
				FileInfo fileInfo = new FileInfo(text);
				return new MalwareFilterRecoveryItem(identity, fileInfo.LastWriteTimeUtc);
			}
			this.errorWriter(new ManagementObjectNotFoundException(Strings.ErrorRecoveryItemNotFoundByIdentity(identity)), ErrorCategory.ObjectNotFound, identity);
			return null;
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x00191328 File Offset: 0x0018F528
		public void RemoveItem(string identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentException("RemoveItem does not accept null or empty string as a parameter.");
			}
			string path = Path.Combine(this.undeliverableFolderPath, identity) + ".frf";
			if (!File.Exists(path))
			{
				this.errorWriter(new ManagementObjectNotFoundException(Strings.ErrorRecoveryItemNotFoundByIdentity(identity)), ErrorCategory.ObjectNotFound, identity);
				return;
			}
			File.Delete(path);
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x00191388 File Offset: 0x0018F588
		public void ReplayItem(string identity, bool remove)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			string path = Path.Combine(this.undeliverableFolderPath, identity) + ".frf";
			if (!File.Exists(path))
			{
				this.errorWriter(new ManagementObjectNotFoundException(Strings.ErrorRecoveryItemNotFoundByIdentity(identity)), ErrorCategory.ObjectNotFound, identity);
				return;
			}
			byte[] array = File.ReadAllBytes(path);
			array = UndeliverableItems.Decode(array);
			path = Path.Combine(this.replayFolderPath, identity) + ".frf";
			path = Path.ChangeExtension(path, ".eml");
			if (File.Exists(path))
			{
				this.warningWriter(Strings.WarningMessageExistsInReplayQueue(identity));
				return;
			}
			try
			{
				using (Stream stream = File.Open(path, FileMode.CreateNew))
				{
					stream.Write(array, 0, array.Length);
				}
				if (remove)
				{
					this.RemoveItem(identity);
				}
			}
			catch (Exception exception)
			{
				this.errorWriter(exception, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x00191484 File Offset: 0x0018F684
		private static byte[] Decode(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			byte[] array = new byte[buffer.Length];
			for (int i = 0; i < buffer.Length; i++)
			{
				byte[] array2 = array;
				int num = i;
				int num2 = i;
				array2[num] = (buffer[num2] ^= UndeliverableItems.obfuscateValue);
			}
			return array;
		}

		// Token: 0x04003535 RID: 13621
		private readonly string undeliverableFolderPath;

		// Token: 0x04003536 RID: 13622
		private readonly string replayFolderPath;

		// Token: 0x04003537 RID: 13623
		private Task.TaskWarningLoggingDelegate warningWriter;

		// Token: 0x04003538 RID: 13624
		private Task.TaskErrorLoggingDelegate errorWriter;

		// Token: 0x04003539 RID: 13625
		private static byte obfuscateValue = byte.MaxValue;
	}
}
