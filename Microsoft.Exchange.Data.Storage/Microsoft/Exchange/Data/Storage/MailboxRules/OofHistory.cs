using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BE7 RID: 3047
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OofHistory : DisposeTrackableBase
	{
		// Token: 0x17001D61 RID: 7521
		// (get) Token: 0x06006C24 RID: 27684 RVA: 0x001D00C4 File Offset: 0x001CE2C4
		// (set) Token: 0x06006C25 RID: 27685 RVA: 0x001D00CC File Offset: 0x001CE2CC
		public bool Testing
		{
			get
			{
				return this.testing;
			}
			set
			{
				this.testing = value;
			}
		}

		// Token: 0x06006C26 RID: 27686 RVA: 0x001D00D5 File Offset: 0x001CE2D5
		public OofHistory(byte[] senderAddress, byte[] globalRuleId, IRuleEvaluationContext context)
		{
			this.senderAddress = senderAddress;
			this.globalRuleId = globalRuleId;
			this.context = context;
		}

		// Token: 0x06006C27 RID: 27687 RVA: 0x001D00F2 File Offset: 0x001CE2F2
		public bool TryInitialize()
		{
			if (!this.TryOpenHistoryFolder())
			{
				return false;
			}
			this.OpenHistoryStream();
			return true;
		}

		// Token: 0x06006C28 RID: 27688 RVA: 0x001D0105 File Offset: 0x001CE305
		public void TestInitialize(Stream stream)
		{
			this.oofHistoryStream = stream;
			this.reader = new OofHistoryReader();
			this.reader.Initialize(this.oofHistoryStream);
		}

		// Token: 0x06006C29 RID: 27689 RVA: 0x001D012C File Offset: 0x001CE32C
		public bool ShouldSendOofReply()
		{
			if (this.isNew)
			{
				return true;
			}
			try
			{
				this.reader = new OofHistoryReader();
				this.reader.Initialize(this.oofHistoryStream);
				while (this.reader.HasMoreEntries)
				{
					this.reader.ReadEntry();
					if (this.MatchEntryProperties())
					{
						return false;
					}
				}
			}
			catch (OofHistoryCorruptionException ex)
			{
				this.Trace<string>("OOF history data corruption detected: {0}", ex.Message);
				if (this.context != null && !this.testing)
				{
					this.context.LogEvent(this.context.OofHistoryCorruption, ex.GetType().ToString(), new object[]
					{
						((MailboxSession)this.context.StoreSession).MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
					});
				}
				this.CloseHistoryStream();
				this.oofHistoryFolder.DeleteProps(new PropTag[]
				{
					PropTag.OofHistory
				});
				this.OpenHistoryStream();
				this.reader = new OofHistoryReader();
				this.reader.Initialize(this.oofHistoryStream);
			}
			return true;
		}

		// Token: 0x06006C2A RID: 27690 RVA: 0x001D0264 File Offset: 0x001CE464
		public void AppendEntry()
		{
			int num = (this.isNew || this.reader == null) ? 0 : this.reader.EntryCount;
			if (num >= 10000)
			{
				this.Trace<int>("History data reached the limit of {0} entries, append aborted.", 10000);
				return;
			}
			int num2 = this.senderAddress.Length;
			if (num2 > 1000)
			{
				this.Trace<int, ushort>("Sender address size {0} is over the limit of {1} bytes, append aborted.", num2, 1000);
				return;
			}
			int num3 = this.globalRuleId.Length;
			if (num3 > 1000)
			{
				this.Trace<int, ushort>("Global rule id size is over the limit of {0} bytes, append aborted.", num3, 1000);
				return;
			}
			OofHistoryWriter oofHistoryWriter = new OofHistoryWriter();
			oofHistoryWriter.Initialize(this.oofHistoryStream);
			oofHistoryWriter.AppendEntry(num + 1, this.senderAddress, this.globalRuleId);
			this.isNew = false;
		}

		// Token: 0x06006C2B RID: 27691 RVA: 0x001D0320 File Offset: 0x001CE520
		public void DumpHistory(TextWriter writer)
		{
			this.reader = new OofHistoryReader();
			this.reader.Initialize(this.oofHistoryStream);
			while (this.reader.HasMoreEntries)
			{
				this.reader.ReadEntry();
				IList<byte> currentEntryRuleIdBytes = this.reader.CurrentEntryRuleIdBytes;
				byte[] array = new byte[currentEntryRuleIdBytes.Count];
				currentEntryRuleIdBytes.CopyTo(array, 0);
				string arg = BitConverter.ToString(array);
				IList<byte> currentEntryAddressBytes = this.reader.CurrentEntryAddressBytes;
				byte[] array2 = new byte[currentEntryAddressBytes.Count];
				currentEntryAddressBytes.CopyTo(array2, 0);
				string @string = Encoding.ASCII.GetString(array2);
				writer.WriteLine("{0} - {1}", arg, @string);
			}
		}

		// Token: 0x06006C2C RID: 27692 RVA: 0x001D03C8 File Offset: 0x001CE5C8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OofHistory>(this);
		}

		// Token: 0x06006C2D RID: 27693 RVA: 0x001D03D0 File Offset: 0x001CE5D0
		protected override void InternalDispose(bool disposing)
		{
			if (this.oofHistoryStream != null)
			{
				this.CloseHistoryStream();
			}
			if (this.oofHistoryFolder != null)
			{
				this.oofHistoryFolder.Dispose();
				this.oofHistoryFolder = null;
			}
		}

		// Token: 0x06006C2E RID: 27694 RVA: 0x001D03FC File Offset: 0x001CE5FC
		private static bool MatchEntryProperty(IList<byte> expectedPropertyData, IList<byte> actualPropertyData)
		{
			if (expectedPropertyData.Count != actualPropertyData.Count)
			{
				return false;
			}
			ushort num = 0;
			while ((int)num < actualPropertyData.Count)
			{
				if (expectedPropertyData[(int)num] != actualPropertyData[(int)num])
				{
					return false;
				}
				num += 1;
			}
			return true;
		}

		// Token: 0x06006C2F RID: 27695 RVA: 0x001D043E File Offset: 0x001CE63E
		private bool MatchEntryProperties()
		{
			return OofHistory.MatchEntryProperty(this.senderAddress, this.reader.CurrentEntryAddressBytes) && OofHistory.MatchEntryProperty(this.globalRuleId, this.reader.CurrentEntryRuleIdBytes);
		}

		// Token: 0x06006C30 RID: 27696 RVA: 0x001D0478 File Offset: 0x001CE678
		private bool TryOpenHistoryFolder()
		{
			bool result;
			try
			{
				MapiStore mapiStore = this.context.StoreSession.Mailbox.MapiStore;
				using (MapiFolder nonIpmSubtreeFolder = mapiStore.GetNonIpmSubtreeFolder())
				{
					this.oofHistoryFolder = nonIpmSubtreeFolder.OpenSubFolderByName("Freebusy Data");
				}
				this.Trace("OOF history folder opened.");
				result = true;
			}
			catch (MapiExceptionNotFound mapiExceptionNotFound)
			{
				this.context.TraceError<string>("Unable to open the OOF history folder {0}, OOF history operation skipped.", "Freebusy Data");
				this.context.LogEvent(this.context.OofHistoryFolderMissing, mapiExceptionNotFound.GetType().ToString(), new object[]
				{
					((MailboxSession)this.context.StoreSession).MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
				});
				result = false;
			}
			return result;
		}

		// Token: 0x06006C31 RID: 27697 RVA: 0x001D0564 File Offset: 0x001CE764
		private void OpenHistoryStream()
		{
			if (this.oofHistoryFolder == null)
			{
				throw new InvalidOperationException("OOF history folder must be opened first.");
			}
			try
			{
				OpenPropertyFlags flags = OpenPropertyFlags.Modify | OpenPropertyFlags.DeferredErrors;
				this.oofHistoryStream = this.oofHistoryFolder.OpenStream(PropTag.OofHistory, flags);
				this.Trace("OOF history property opened.");
				this.LockStreamWithRetry();
			}
			catch (MapiExceptionNotFound)
			{
				this.Trace("OOF history property does not exist, creating new OOF history stream.");
				if (this.oofHistoryStream != null)
				{
					this.oofHistoryStream.Dispose();
					this.oofHistoryStream = null;
				}
				OpenPropertyFlags flags2 = OpenPropertyFlags.Create | OpenPropertyFlags.Modify | OpenPropertyFlags.DeferredErrors;
				this.oofHistoryStream = this.oofHistoryFolder.OpenStream(PropTag.OofHistory, flags2);
				this.LockStreamWithRetry();
				this.PrepareNewOofHistoryStream();
			}
		}

		// Token: 0x06006C32 RID: 27698 RVA: 0x001D0610 File Offset: 0x001CE810
		private void CloseHistoryStream()
		{
			try
			{
				if (this.streamLocked)
				{
					MapiStream mapiStream = this.oofHistoryStream as MapiStream;
					mapiStream.UnlockRegion(0L, 1L, 1);
				}
			}
			finally
			{
				this.oofHistoryStream.Dispose();
				this.oofHistoryStream = null;
			}
		}

		// Token: 0x06006C33 RID: 27699 RVA: 0x001D0664 File Offset: 0x001CE864
		private void PrepareNewOofHistoryStream()
		{
			this.Trace("Resetting / Initializing OOF history stream.");
			OofHistoryWriter.Reset(this.oofHistoryStream);
			this.oofHistoryStream.Position = 0L;
			this.isNew = true;
		}

		// Token: 0x06006C34 RID: 27700 RVA: 0x001D0690 File Offset: 0x001CE890
		private void LockStreamWithRetry()
		{
			MapiStream mapiStream = this.oofHistoryStream as MapiStream;
			if (mapiStream == null)
			{
				return;
			}
			for (int i = 0; i < 6; i++)
			{
				try
				{
					mapiStream.LockRegion(0L, 1L, 1);
					this.streamLocked = true;
					this.Trace("Oof history stream locked.");
					break;
				}
				catch (MapiExceptionLockViolation argument)
				{
					if (i == 5)
					{
						throw;
					}
					this.context.TraceError<int, MapiExceptionLockViolation>("Failed to lock on attempt {0}. Exception encountered is {1}.", i, argument);
					Thread.Sleep(100);
				}
			}
		}

		// Token: 0x06006C35 RID: 27701 RVA: 0x001D070C File Offset: 0x001CE90C
		private void Trace(string message)
		{
			if (this.context != null)
			{
				this.context.TraceDebug(message);
			}
		}

		// Token: 0x06006C36 RID: 27702 RVA: 0x001D0722 File Offset: 0x001CE922
		private void Trace<T>(string format, T argument)
		{
			if (this.context != null)
			{
				this.context.TraceDebug<T>(format, argument);
			}
		}

		// Token: 0x06006C37 RID: 27703 RVA: 0x001D0739 File Offset: 0x001CE939
		private void Trace<T1, T2>(string format, T1 argument1, T2 argument2)
		{
			if (this.context != null)
			{
				this.context.TraceDebug<T1, T2>(format, argument1, argument2);
			}
		}

		// Token: 0x04003DD9 RID: 15833
		public const ushort MaxPropertyValueLength = 1000;

		// Token: 0x04003DDA RID: 15834
		internal const int HistoryEntryLimit = 10000;

		// Token: 0x04003DDB RID: 15835
		internal const int LockRetrySleepMilliSeconds = 100;

		// Token: 0x04003DDC RID: 15836
		internal const string OofHistoryFolderName = "Freebusy Data";

		// Token: 0x04003DDD RID: 15837
		private const int LockRetryLimit = 6;

		// Token: 0x04003DDE RID: 15838
		private IRuleEvaluationContext context;

		// Token: 0x04003DDF RID: 15839
		private byte[] senderAddress;

		// Token: 0x04003DE0 RID: 15840
		private byte[] globalRuleId;

		// Token: 0x04003DE1 RID: 15841
		private bool isNew;

		// Token: 0x04003DE2 RID: 15842
		private OofHistoryReader reader;

		// Token: 0x04003DE3 RID: 15843
		private MapiFolder oofHistoryFolder;

		// Token: 0x04003DE4 RID: 15844
		private Stream oofHistoryStream;

		// Token: 0x04003DE5 RID: 15845
		private bool streamLocked;

		// Token: 0x04003DE6 RID: 15846
		private bool testing;

		// Token: 0x02000BE8 RID: 3048
		public enum PropId : byte
		{
			// Token: 0x04003DE8 RID: 15848
			SenderAddress = 1,
			// Token: 0x04003DE9 RID: 15849
			GlobalRuleId
		}
	}
}
