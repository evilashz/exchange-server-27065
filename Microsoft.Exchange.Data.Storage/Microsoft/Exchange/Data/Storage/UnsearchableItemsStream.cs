using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnsearchableItemsStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x000171C4 File Offset: 0x000153C4
		internal static void SetTestDataSource(UnsearchableItemsStream.TestDataSource method)
		{
			UnsearchableItemsStream.testDataSource = method;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000171CC File Offset: 0x000153CC
		internal UnsearchableItemsStream(MailboxSession session)
		{
			this.session = session;
			this.disposeTracker = this.GetDisposeTracker();
			this.isDisposed = false;
			string serverFqdn = session.MailboxOwner.MailboxInfo.Location.ServerFqdn;
			this.serverGuid = session.MailboxOwner.MailboxInfo.Location.ServerGuid;
			if (UnsearchableItemsStream.testDataSource == null)
			{
				bool flag = false;
				try
				{
					if (session != null)
					{
						session.BeginMapiCall();
						session.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					this.exRpcAdmin = ExRpcAdmin.Create("Client=CI", serverFqdn, null, null, null);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExFailedToGetUnsearchableItems, ex, session, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("UnsearchableItemsStream::Constructor. Failed to create ExRpcAdmin.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExFailedToGetUnsearchableItems, ex2, session, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("UnsearchableItemsStream::Constructor. Failed to create ExRpcAdmin.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (session != null)
						{
							session.EndMapiCall();
							if (flag)
							{
								session.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			this.lastMaxDocId = 0U;
			this.GetDataBlock();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001735C File Offset: 0x0001555C
		private void GetDataBlock()
		{
			PropValue[][] array = null;
			if (UnsearchableItemsStream.testDataSource != null)
			{
				array = UnsearchableItemsStream.testDataSource(this.lastMaxDocId);
			}
			else
			{
				StoreSession storeSession = this.session;
				bool flag = false;
				try
				{
					if (storeSession != null)
					{
						storeSession.BeginMapiCall();
						storeSession.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					this.exRpcAdmin.CiEnumerateFailedItemsByMailbox(this.session.MailboxOwner.MailboxInfo.GetDatabaseGuid(), this.serverGuid, this.session.MailboxOwner.MailboxInfo.MailboxGuid, this.lastMaxDocId, out array);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExFailedToGetUnsearchableItems, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("UnsearchableItemsStream::GetDataBlock. Failed to read data from CiEnumerateFailedItemsByMailbox.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExFailedToGetUnsearchableItems, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("UnsearchableItemsStream::GetDataBlock. Failed to read data from CiEnumerateFailedItemsByMailbox.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (storeSession != null)
						{
							storeSession.EndMapiCall();
							if (flag)
							{
								storeSession.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			if (array == null)
			{
				this.eof = true;
				return;
			}
			int num = array[0][2].GetBytes().Length;
			int num2 = array.Length * (num + 4);
			int i = 0;
			Stream stream = this.dataStream;
			if (stream != null)
			{
				i = (int)(stream.Length - stream.Position);
				if (i > 0)
				{
					num2 += i;
				}
			}
			MemoryStream memoryStream = new MemoryStream(num2);
			if (stream != null)
			{
				while (i > 0)
				{
					byte[] buffer = new byte[i];
					int num3 = stream.Read(buffer, 0, i);
					memoryStream.Write(buffer, 0, num3);
					i -= num3;
				}
				stream.Dispose();
				this.dataStream = null;
			}
			foreach (PropValue[] array3 in array)
			{
				uint @int = (uint)array3[0].GetInt();
				this.lastMaxDocId = @int;
				byte[] bytes = array3[2].GetBytes();
				memoryStream.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
				memoryStream.Write(bytes, 0, bytes.Length);
			}
			long position = memoryStream.Position;
			memoryStream.Flush();
			memoryStream.Position = 0L;
			memoryStream.SetLength(position);
			this.dataStream = memoryStream;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00017604 File Offset: 0x00015804
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00017626 File Offset: 0x00015826
		protected override void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.InternalDispose(disposing);
				this.isDisposed = true;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001764B File Offset: 0x0001584B
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.dataStream != null)
				{
					this.dataStream.Dispose();
					this.dataStream = null;
				}
				Util.DisposeIfPresent(this.exRpcAdmin);
				Util.DisposeIfPresent(this.disposeTracker);
			}
			base.Dispose(disposing);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00017687 File Offset: 0x00015887
		internal bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0001768F File Offset: 0x0001588F
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<UnsearchableItemsStream>(this);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00017697 File Offset: 0x00015897
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000176AC File Offset: 0x000158AC
		public override bool CanRead
		{
			get
			{
				this.CheckDisposed("CanRead");
				return true;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000176BA File Offset: 0x000158BA
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed("CanSeek");
				return false;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000176C8 File Offset: 0x000158C8
		public override bool CanWrite
		{
			get
			{
				this.CheckDisposed("CanWrite");
				return false;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000176D8 File Offset: 0x000158D8
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("Read");
			if (this.dataStream == null)
			{
				return 0;
			}
			while ((long)count > this.dataStream.Length - this.dataStream.Position && !this.eof)
			{
				this.GetDataBlock();
			}
			return this.dataStream.Read(buffer, offset, count);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00017731 File Offset: 0x00015931
		public override void Flush()
		{
			this.CheckDisposed("Flush");
			throw new NotSupportedException(string.Format("{0} Not supported for UnsearchableItemsStream", "Flush"));
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00017752 File Offset: 0x00015952
		public override long Length
		{
			get
			{
				this.CheckDisposed("Length");
				throw new NotSupportedException(string.Format("{0} Not supported for UnsearchableItemsStream", "Length"));
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00017773 File Offset: 0x00015973
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00017794 File Offset: 0x00015994
		public override long Position
		{
			get
			{
				this.CheckDisposed("Position");
				throw new NotSupportedException(string.Format("{0} Not supported for UnsearchableItemsStream", "get_Position"));
			}
			set
			{
				this.CheckDisposed("Position");
				throw new NotSupportedException(string.Format("{0} Not supported for UnsearchableItemsStream", "set_Position"));
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000177B5 File Offset: 0x000159B5
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed("Seek");
			throw new NotSupportedException(string.Format("{0} Not supported for UnsearchableItemsStream", "Seek"));
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000177D6 File Offset: 0x000159D6
		public override void SetLength(long length)
		{
			this.CheckDisposed("SetLength");
			throw new NotSupportedException(string.Format("{0} Not supported for UnsearchableItemsStream", "SetLength"));
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000177F7 File Offset: 0x000159F7
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("Write");
			throw new NotSupportedException(string.Format("{0} Not supported for UnsearchableItemsStream", "Write"));
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00017818 File Offset: 0x00015A18
		public static byte[] GetEntryId(BinaryReader reader)
		{
			int count = reader.ReadInt32();
			byte[] result;
			try
			{
				result = reader.ReadBytes(count);
			}
			catch (EndOfStreamException innerException)
			{
				throw new CorruptDataException(ServerStrings.ExFailedToGetUnsearchableItems, innerException);
			}
			return result;
		}

		// Token: 0x040000D5 RID: 213
		private const string ErrorString = "{0} Not supported for UnsearchableItemsStream";

		// Token: 0x040000D6 RID: 214
		private bool isDisposed;

		// Token: 0x040000D7 RID: 215
		private DisposeTracker disposeTracker;

		// Token: 0x040000D8 RID: 216
		private readonly MailboxSession session;

		// Token: 0x040000D9 RID: 217
		private readonly Guid serverGuid;

		// Token: 0x040000DA RID: 218
		private readonly ExRpcAdmin exRpcAdmin;

		// Token: 0x040000DB RID: 219
		private MemoryStream dataStream;

		// Token: 0x040000DC RID: 220
		private bool eof;

		// Token: 0x040000DD RID: 221
		private uint lastMaxDocId;

		// Token: 0x040000DE RID: 222
		private static UnsearchableItemsStream.TestDataSource testDataSource;

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x06000309 RID: 777
		internal delegate PropValue[][] TestDataSource(uint lastDocId);
	}
}
