using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001D2 RID: 466
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiManifestCollector : DisposeTrackableBase, IExchangeImportContentsChanges
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001A564 File Offset: 0x00018764
		public MapiManifestCollector(MapiFolder sourceFolder, IMapiManifestCallback manifestCallback)
		{
			this.manifestCallback = manifestCallback;
			this.iUnknown = Marshal.GetIUnknownForObject(this);
			this.sourceFolder = sourceFolder;
			byte[] bytes = this.sourceFolder.GetProp(PropTag.EntryId).GetBytes();
			this.sourceFID = this.sourceFolder.MapiStore.GetFidFromEntryId(bytes);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001A5C1 File Offset: 0x000187C1
		internal IntPtr ExchangeCollector
		{
			get
			{
				return this.iUnknown;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001A5C9 File Offset: 0x000187C9
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.iUnknown != IntPtr.Zero)
			{
				Marshal.Release(this.iUnknown);
				this.iUnknown = IntPtr.Zero;
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001A5F7 File Offset: 0x000187F7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiManifestCollector>(this);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001A5FF File Offset: 0x000187FF
		unsafe int IExchangeImportContentsChanges.GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError)
		{
			lpMapiError = (IntPtr)((UIntPtr)0);
			return 0;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001A606 File Offset: 0x00018806
		int IExchangeImportContentsChanges.Config(IStream pIStream, int ulFlags)
		{
			return 0;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001A609 File Offset: 0x00018809
		int IExchangeImportContentsChanges.UpdateState(IStream pIStream)
		{
			return 0;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001A60C File Offset: 0x0001880C
		unsafe int IExchangeImportContentsChanges.ImportMessageChange(int cpvalChanges, SPropValue* ppvalChanges, int ulFlags, out IMessage message)
		{
			ManifestChangeType changeType = ((ulFlags & 2048) != 0) ? ManifestChangeType.Add : ManifestChangeType.Change;
			byte[] array = null;
			byte[] array2 = null;
			byte[] changeKey = null;
			byte[] changeList = null;
			DateTime lastModificationTime = DateTime.MinValue;
			bool associated = false;
			List<PropValue> list = new List<PropValue>();
			int i = 0;
			while (i < cpvalChanges)
			{
				PropValue item = new PropValue(ppvalChanges + i);
				PropTag propTag = item.PropTag;
				if (propTag <= PropTag.SourceKey)
				{
					if (propTag != PropTag.EntryId)
					{
						if (propTag != PropTag.LastModificationTime)
						{
							if (propTag != PropTag.SourceKey)
							{
								goto IL_D2;
							}
							array2 = item.GetBytes();
						}
						else
						{
							lastModificationTime = item.GetDateTime();
						}
					}
					else
					{
						array = item.GetBytes();
					}
				}
				else if (propTag != PropTag.ChangeKey)
				{
					if (propTag != PropTag.PredecessorChangeList)
					{
						if (propTag != PropTag.Associated)
						{
							goto IL_D2;
						}
						associated = item.GetBoolean();
					}
					else
					{
						changeList = item.GetBytes();
					}
				}
				else
				{
					changeKey = item.GetBytes();
				}
				IL_DB:
				i++;
				continue;
				IL_D2:
				list.Add(item);
				goto IL_DB;
			}
			if (array == null && array2 != null)
			{
				array = this.EntryIdFromSourceKey(array2);
			}
			if (array != null)
			{
				this.manifestCallback.Change(array, array2, changeKey, changeList, lastModificationTime, changeType, associated, (list.Count > 0) ? list.ToArray() : null);
			}
			message = null;
			return -2147219455;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001A748 File Offset: 0x00018948
		unsafe int IExchangeImportContentsChanges.ImportMessageDeletion(int ulFlags, _SBinaryArray* lpSrcEntryList)
		{
			if (lpSrcEntryList == null || lpSrcEntryList->cValues == 0)
			{
				return 0;
			}
			_SBinary* ptr = lpSrcEntryList->lpbin;
			int i = 0;
			while (i < lpSrcEntryList->cValues)
			{
				byte[] array = new byte[ptr->cb];
				Marshal.Copy((IntPtr)((void*)ptr->lpb), array, 0, ptr->cb);
				byte[] array2 = this.EntryIdFromSourceKey(array);
				if (array2 != null)
				{
					this.manifestCallback.Delete(array2, (ulFlags & 4) == 0, false);
				}
				i++;
				ptr++;
			}
			return 0;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001A7D0 File Offset: 0x000189D0
		unsafe int IExchangeImportContentsChanges.ImportPerUserReadStateChange(int cElements, _ReadState* lpReadState)
		{
			int i = 0;
			while (i < cElements)
			{
				byte[] array = new byte[lpReadState->cbSourceKey];
				Marshal.Copy((IntPtr)((void*)lpReadState->pbSourceKey), array, 0, lpReadState->cbSourceKey);
				ExchangeManifestCallbackReadFlags ulFlags = (ExchangeManifestCallbackReadFlags)lpReadState->ulFlags;
				bool read = (ulFlags & ExchangeManifestCallbackReadFlags.Read) == ExchangeManifestCallbackReadFlags.Read;
				byte[] array2 = this.EntryIdFromSourceKey(array);
				if (array2 != null)
				{
					this.manifestCallback.ReadUnread(array2, read);
				}
				i++;
				lpReadState++;
			}
			return 0;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001A842 File Offset: 0x00018A42
		int IExchangeImportContentsChanges.ImportMessageMove(int cbSourceKeySrcFolder, byte[] pbSourceKeySrcFolder, int cbSourceKeySrcMessage, byte[] pbSourceKeySrcMessage, int cbPCLMessage, byte[] pbPCLMessage, int cbSourceKeyDestMessage, byte[] pbSourceKeyDestMessage, int cbChangeNumDestMessage, byte[] pbChangeNumDestMessage)
		{
			return 0;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001A848 File Offset: 0x00018A48
		private byte[] EntryIdFromSourceKey(byte[] sourceKey)
		{
			if (sourceKey == null)
			{
				return null;
			}
			long mid = 0L;
			try
			{
				mid = this.sourceFolder.MapiStore.IdFromGlobalId(sourceKey);
			}
			catch (ArgumentException)
			{
				return null;
			}
			return this.sourceFolder.MapiStore.CreateEntryId(this.sourceFID, mid);
		}

		// Token: 0x0400063C RID: 1596
		private IMapiManifestCallback manifestCallback;

		// Token: 0x0400063D RID: 1597
		private IntPtr iUnknown;

		// Token: 0x0400063E RID: 1598
		private MapiFolder sourceFolder;

		// Token: 0x0400063F RID: 1599
		private long sourceFID;
	}
}
