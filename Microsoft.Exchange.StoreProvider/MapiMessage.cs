using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001D9 RID: 473
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiMessage : MapiProp
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x0001AD68 File Offset: 0x00018F68
		internal MapiMessage(IExMapiMessage iMessage, IMessage externalIMessage, MapiStore mapiStore) : base(iMessage, externalIMessage, mapiStore, MapiMessage.IMessageGuids)
		{
			if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(14546, 17, (long)this.GetHashCode(), "MapiMessage.MapiMessage: this={0}", TraceUtils.MakeHash(this));
			}
			this.iMessage = iMessage;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001ADA6 File Offset: 0x00018FA6
		protected override void MapiInternalDispose()
		{
			if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(9426, 17, (long)this.GetHashCode(), "MapiMessage.InternalDispose: this={0}", TraceUtils.MakeHash(this));
			}
			this.iMessage = null;
			base.MapiInternalDispose();
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001ADDC File Offset: 0x00018FDC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiMessage>(this);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001ADE4 File Offset: 0x00018FE4
		public MapiTable GetRecipientTable()
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			MapiTable mapiTable = null;
			bool flag = false;
			MapiTable result;
			try
			{
				IExMapiTable exMapiTable = null;
				try
				{
					int recipientTable = this.iMessage.GetRecipientTable(int.MinValue, out exMapiTable);
					if (recipientTable != 0)
					{
						base.ThrowIfError("Unable to get recipient table.", recipientTable);
					}
					mapiTable = new MapiTable(exMapiTable, base.MapiStore);
					exMapiTable = null;
				}
				finally
				{
					exMapiTable.DisposeIfValid();
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(13522, 17, (long)this.GetHashCode(), "MapiMessage.GetRecipientTable results: mapiTable={0}", TraceUtils.MakeHash(mapiTable));
				}
				flag = true;
				result = mapiTable;
			}
			finally
			{
				if (!flag && mapiTable != null)
				{
					mapiTable.Dispose();
				}
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001AEA4 File Offset: 0x000190A4
		public void ModifyRecipients(ModifyRecipientsFlags modFlags, params AdrEntry[] adrEntries)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string>(11474, 17, (long)this.GetHashCode(), "MapiMessage.ModifyRecipients params: modFlags={0}, adrEntries={1}", modFlags.ToString(), TraceUtils.DumpArray(adrEntries));
				}
				int num = this.iMessage.ModifyRecipients((int)modFlags, adrEntries);
				if (num != 0)
				{
					base.ThrowIfError("Unable to modify recipients.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001AF2C File Offset: 0x0001912C
		public void SubmitMessage(SubmitMessageFlags submitMessageFlags)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(15570, 17, (long)this.GetHashCode(), "MapiMessage.SubmitMessage params: submitMessageFlags={0}", submitMessageFlags.ToString());
				}
				int num = this.iMessage.SubmitMessage((int)submitMessageFlags);
				if (num != 0)
				{
					base.ThrowIfError("Unable to submit message.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001AFAC File Offset: 0x000191AC
		public void SubmitMessageEx(SubmitMessageExFlags submitMessageExFlags)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				int num = this.iMessage.SubmitMessageEx((int)submitMessageExFlags);
				if (num != 0)
				{
					base.ThrowIfError("Unable to submit message.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001B000 File Offset: 0x00019200
		public void TransportSendMessage(out PropValue[] propsToReturn)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace(15570, 17, (long)this.GetHashCode(), "MapiMessage.TransportSendMessage.");
				}
				int num = this.iMessage.TransportSendMessage(out propsToReturn);
				if (num != 0)
				{
					base.ThrowIfError("Unable to transport send message.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001B078 File Offset: 0x00019278
		public void SetReadFlag(SetReadFlags readFlags)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(9042, 17, (long)this.GetHashCode(), "MapiMessage.SetReadFlag params: readFlags={0}", readFlags.ToString());
				}
				int num = this.iMessage.SetReadFlag((int)readFlags);
				if (num != 0)
				{
					base.ThrowIfError("Unable to set read flag.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001B0F8 File Offset: 0x000192F8
		public MapiTable GetAttachmentTable()
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			MapiTable mapiTable = null;
			bool flag = false;
			MapiTable result;
			try
			{
				IExMapiTable exMapiTable = null;
				try
				{
					int attachmentTable = this.iMessage.GetAttachmentTable(8, out exMapiTable);
					if (attachmentTable != 0)
					{
						base.ThrowIfError("Unable to get attachment table.", attachmentTable);
					}
					mapiTable = new MapiTable(exMapiTable, base.MapiStore);
					exMapiTable = null;
				}
				finally
				{
					exMapiTable.DisposeIfValid();
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(13138, 17, (long)this.GetHashCode(), "MapiMessage.GetAttachmentTable results: mapiTable={0}", TraceUtils.MakeHash(mapiTable));
				}
				flag = true;
				result = mapiTable;
			}
			finally
			{
				if (!flag && mapiTable != null)
				{
					mapiTable.Dispose();
				}
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001B1B4 File Offset: 0x000193B4
		public MapiAttach OpenAttach(int attachNumber)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			MapiAttach result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(11090, 17, (long)this.GetHashCode(), "MapiMessage.OpenAttach params: attachNumber={0}", attachNumber.ToString());
				}
				MapiAttach mapiAttach = null;
				IExMapiAttach exMapiAttach = null;
				try
				{
					int num = this.iMessage.OpenAttach(attachNumber, Guid.Empty, 24, out exMapiAttach);
					if (num != 0)
					{
						base.ThrowIfError("Unable to open attachment.", num);
					}
					mapiAttach = new MapiAttach(exMapiAttach, null, base.MapiStore);
					exMapiAttach = null;
				}
				finally
				{
					exMapiAttach.DisposeIfValid();
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(15186, 17, (long)this.GetHashCode(), "MapiMessage.OpenAttach results: mapiAttach={0}", TraceUtils.MakeHash(mapiAttach));
				}
				result = mapiAttach;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001B28C File Offset: 0x0001948C
		public MapiAttach CreateAttach(out int attachNumber)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			MapiAttach result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace(10066, 17, (long)this.GetHashCode(), "MapiMessage.CreateAttach");
				}
				MapiAttach mapiAttach = null;
				IExMapiAttach exMapiAttach = null;
				try
				{
					int num = this.iMessage.CreateAttach(Guid.Empty, 8, out attachNumber, out exMapiAttach);
					if (num != 0)
					{
						base.ThrowIfError("Unable to create attachment.", num);
					}
					mapiAttach = new MapiAttach(exMapiAttach, null, base.MapiStore);
					exMapiAttach = null;
				}
				finally
				{
					exMapiAttach.DisposeIfValid();
				}
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(14162, 17, (long)this.GetHashCode(), "MapiMessage.CreateAttach results: mapiAttach={0}", TraceUtils.MakeHash(mapiAttach));
				}
				result = mapiAttach;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001B35C File Offset: 0x0001955C
		public void DeleteAttach(int attachNumber)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(12114, 17, (long)this.GetHashCode(), "MapiMessage.DeleteAttach params: attachNumber={0}", attachNumber.ToString());
				}
				int num = this.iMessage.DeleteAttach(attachNumber, IntPtr.Zero, IntPtr.Zero, 0);
				if (num != 0)
				{
					base.ThrowIfError("Unable to delete attachment.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001B3E4 File Offset: 0x000195E4
		public void SaveMessageToFile(string filePath, bool unicode)
		{
			this.SaveMessageToFile(filePath, unicode ? SaveMessageFlags.Unicode : SaveMessageFlags.None);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001B3F8 File Offset: 0x000195F8
		public void SaveMessageToFile(string filePath, SaveMessageFlags flags)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string>(12626, 17, (long)this.GetHashCode(), "MapiMessage.SaveMessageToFile params: filePath={0}, flags={1}", TraceUtils.MakeString(filePath), flags.ToString());
				}
				IExRpcManageInterface exRpcManageInterface = null;
				object obj = null;
				try
				{
					NativeMethods.StorageMode mode = NativeMethods.StorageMode.Write | NativeMethods.StorageMode.ShareExclusive | NativeMethods.StorageMode.Create;
					int num = NativeMethods.StgCreateStorageEx(filePath, mode, NativeMethods.StorageFormat.DocumentFile, 0, IntPtr.Zero, 0, InterfaceIds.IStorageGuid, out obj);
					if (num != 0)
					{
						throw MapiExceptionHelper.FileOpenFailureException("Couldn't open file: " + filePath, num);
					}
					exRpcManageInterface = ExRpcModule.GetExRpcManage();
					IStorage iStorage = obj as IStorage;
					num = exRpcManageInterface.ToIStg(iStorage, ((SafeExMapiMessageHandle)this.iMessage).DangerousGetHandle(), (int)flags);
					if (num != 0)
					{
						MapiExceptionHelper.ThrowIfError("Unable to save message to file.", num, exRpcManageInterface, null);
					}
				}
				finally
				{
					if (obj != null)
					{
						Marshal.ReleaseComObject(obj);
					}
					exRpcManageInterface.DisposeIfValid();
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001B4F0 File Offset: 0x000196F0
		public void SaveMessageToStream(Stream ioStream, bool unicode)
		{
			this.SaveMessageToStream(ioStream, unicode ? SaveMessageFlags.Unicode : SaveMessageFlags.None);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001B504 File Offset: 0x00019704
		public void SaveMessageToStream(Stream ioStream, SaveMessageFlags flags)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			if (ioStream == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("ioStream");
			}
			base.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(17))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string>(10578, 17, (long)this.GetHashCode(), "MapiMessage.SaveMessageToStream params: ioStream={0}, flags={1}", TraceUtils.MakeHash(ioStream), flags.ToString());
				}
				IExRpcManageInterface exRpcManageInterface = null;
				try
				{
					MapiIStream iStream = new MapiIStream(ioStream);
					exRpcManageInterface = ExRpcModule.GetExRpcManage();
					int num = exRpcManageInterface.ToIStream(iStream, ((SafeExMapiMessageHandle)this.iMessage).DangerousGetHandle(), (int)flags);
					if (num != 0)
					{
						MapiExceptionHelper.ThrowIfError("Unable to save message to stream.", num, exRpcManageInterface, null);
					}
				}
				finally
				{
					exRpcManageInterface.DisposeIfValid();
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001B5CC File Offset: 0x000197CC
		public void Deliver(RecipientType recipientType)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				int num = this.iMessage.Deliver((int)recipientType);
				if (num != 0)
				{
					base.ThrowIfError("Unable to deliver message.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001B620 File Offset: 0x00019820
		public void DoneWithMessage()
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				int num = this.iMessage.DoneWithMessage();
				if (num != 0)
				{
					base.ThrowIfError("Unable to finish message submission.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001B674 File Offset: 0x00019874
		public void DuplicateDeliveryCheck(string internetMessageId, DateTime? clientSubmitTime)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			base.LockStore();
			try
			{
				long submitTimeAsLong = 0L;
				if (clientSubmitTime != null)
				{
					submitTimeAsLong = PropValue.DateTimeAsLong(clientSubmitTime.Value);
				}
				int num = this.iMessage.DuplicateDeliveryCheck(internetMessageId, submitTimeAsLong);
				if (num != 0)
				{
					base.ThrowIfError("Duplicate delivery check failed.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x04000660 RID: 1632
		private IExMapiMessage iMessage;

		// Token: 0x04000661 RID: 1633
		private static Guid[] IMessageGuids = new Guid[]
		{
			InterfaceIds.IMessageGuid
		};
	}
}
