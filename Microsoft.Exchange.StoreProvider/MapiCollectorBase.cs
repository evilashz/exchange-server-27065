using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200007B RID: 123
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiCollectorBase : MapiUnk
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000E588 File Offset: 0x0000C788
		internal MapiCollectorBase(IExImportContentsChanges iExchangeImportContentsChanges, MapiStore mapiStore) : base(iExchangeImportContentsChanges, null, mapiStore)
		{
			this.iExchangeImportContentsChanges = iExchangeImportContentsChanges;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000E59A File Offset: 0x0000C79A
		protected override void MapiInternalDispose()
		{
			this.iExchangeImportContentsChanges = null;
			base.MapiInternalDispose();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E5A9 File Offset: 0x0000C7A9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiCollectorBase>(this);
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000E5B1 File Offset: 0x0000C7B1
		internal IntPtr ExchangeCollector
		{
			get
			{
				base.CheckDisposed();
				return ((SafeExImportContentsChangesHandle)this.iExchangeImportContentsChanges).DangerousGetHandle();
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		public unsafe void ImportPerUserReadStateChange(params ReadState[] readStates)
		{
			base.CheckDisposed();
			if (readStates == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("readStates");
			}
			if (readStates.Length <= 0)
			{
				throw MapiExceptionHelper.ArgumentOutOfRangeException("readStates", "entryIds must contain at least 1 element");
			}
			base.LockStore();
			try
			{
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < readStates.Length; i++)
				{
					num2 += readStates[i].GetBytesToMarshal();
				}
				try
				{
					fixed (byte* ptr = new byte[num2])
					{
						_ReadState* ptr2 = (_ReadState*)ptr;
						byte* ptr3 = ptr + (IntPtr)readStates.Length * (IntPtr)(_ReadState.SizeOf + 7 & -8);
						for (int j = 0; j < readStates.Length; j++)
						{
							readStates[j].MarshalToNative(ptr2, ref ptr3);
							ptr2++;
						}
						num = this.iExchangeImportContentsChanges.ImportPerUserReadStateChange(readStates.Length, (_ReadState*)ptr);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				if (num != 0)
				{
					base.ThrowIfErrorOrWarning("Unable to import read state changes.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		public unsafe void ImportMessageDeletion(ImportDeletionFlags importDeletionFlags, params PropValue[] sourceKeys)
		{
			base.CheckDisposed();
			if (sourceKeys == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("sourceKeys");
			}
			if (sourceKeys.Length <= 0)
			{
				throw MapiExceptionHelper.ArgumentOutOfRangeException("sourceKeys", "sourceKeys must contain at least 1 element");
			}
			base.LockStore();
			try
			{
				int num = 0;
				SBinary[] array = new SBinary[sourceKeys.Length];
				for (int i = 0; i < sourceKeys.Length; i++)
				{
					array[i] = new SBinary(sourceKeys[i].GetBytes());
				}
				int bytesToMarshal = SBinaryArray.GetBytesToMarshal(array);
				try
				{
					fixed (byte* ptr = new byte[bytesToMarshal])
					{
						SBinaryArray.MarshalToNative(ptr, array);
						num = this.iExchangeImportContentsChanges.ImportMessageDeletion((int)importDeletionFlags, (_SBinaryArray*)ptr);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				if (num != 0)
				{
					base.ThrowIfErrorOrWarning("Unable to import message deletion.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		public void ImportMessageDeletion(params PropValue[] sourceKeys)
		{
			this.ImportMessageDeletion(ImportDeletionFlags.None, sourceKeys);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		protected int InternalImportMessageMove(PropValue sourceFolderKey, PropValue sourceMessageKey, PropValue changeListKey, PropValue destMessageKey, PropValue changeNumberKey)
		{
			return this.iExchangeImportContentsChanges.ImportMessageMove(sourceFolderKey.GetBytes().Length, sourceFolderKey.GetBytes(), sourceMessageKey.GetBytes().Length, sourceMessageKey.GetBytes(), changeListKey.GetBytes().Length, changeListKey.GetBytes(), destMessageKey.GetBytes().Length, destMessageKey.GetBytes(), changeNumberKey.GetBytes().Length, changeNumberKey.GetBytes());
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000E83C File Offset: 0x0000CA3C
		protected unsafe int InternalImportMessageChange(PropValue[] propValues, ImportMessageChangeFlags importMessageChangeFlags, out MapiMessage mapiMessage)
		{
			mapiMessage = null;
			if (propValues == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("propValues");
			}
			if (propValues.Length <= 0)
			{
				throw MapiExceptionHelper.ArgumentOutOfRangeException("propValues", "values must contain at least 1 element");
			}
			bool flag = false;
			int result;
			try
			{
				int num = 0;
				int num2 = 0;
				SafeExMapiMessageHandle safeExMapiMessageHandle = null;
				for (int i = 0; i < propValues.Length; i++)
				{
					num2 += propValues[i].GetBytesToMarshal();
				}
				try
				{
					fixed (byte* ptr = new byte[num2])
					{
						PropValue.MarshalToNative(propValues, ptr);
						try
						{
							num = this.iExchangeImportContentsChanges.ImportMessageChange(propValues.Length, (SPropValue*)ptr, (int)importMessageChangeFlags, out safeExMapiMessageHandle);
							if (safeExMapiMessageHandle != null && !safeExMapiMessageHandle.IsInvalid)
							{
								mapiMessage = new MapiMessage(safeExMapiMessageHandle, null, base.MapiStore);
								safeExMapiMessageHandle = null;
							}
						}
						finally
						{
							safeExMapiMessageHandle.DisposeIfValid();
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
				flag = true;
				result = num;
			}
			finally
			{
				if (!flag && mapiMessage != null)
				{
					mapiMessage.Dispose();
					mapiMessage = null;
				}
			}
			return result;
		}

		// Token: 0x040004CF RID: 1231
		private IExImportContentsChanges iExchangeImportContentsChanges;
	}
}
