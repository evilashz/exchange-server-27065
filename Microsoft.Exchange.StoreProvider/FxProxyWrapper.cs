using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200004D RID: 77
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FxProxyWrapper : IExchangeFastTransferEx, IMAPIProp
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x0000A56B File Offset: 0x0000876B
		internal FxProxyWrapper(IMapiFxCollector fxCollector)
		{
			this.fxCollector = fxCollector;
			this.lastException = null;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A584 File Offset: 0x00008784
		public static MapiProp CreateFxProxyWrapper(IMapiFxProxy iFxProxy, MapiStore mapiStore, out FxProxyWrapper wrapper)
		{
			IMapiFxCollector mapiFxCollector = new FxCollectorSerializer(iFxProxy);
			Guid objectType = mapiFxCollector.GetObjectType();
			if (objectType == InterfaceIds.IMAPIFolderGuid)
			{
				wrapper = new FxProxyFolderWrapper(mapiFxCollector);
				return new MapiFolder(null, (IMAPIFolder)wrapper, mapiStore);
			}
			if (objectType == InterfaceIds.IMsgStoreGuid)
			{
				wrapper = new FxProxyStoreWrapper(mapiFxCollector);
				return new MapiStore(null, (IMsgStore)wrapper, mapiStore.ExRpcConnection, mapiStore.ApplicationId);
			}
			if (objectType == InterfaceIds.IMessageGuid)
			{
				wrapper = new FxProxyMessageWrapper(mapiFxCollector);
				return new MapiMessage(null, (IMessage)wrapper, mapiStore);
			}
			if (objectType == InterfaceIds.IAttachGuid)
			{
				wrapper = new FxProxyAttachWrapper(mapiFxCollector);
				return new MapiAttach(null, (IAttach)wrapper, mapiStore);
			}
			wrapper = null;
			throw MapiExceptionHelper.ArgumentException("iFxProxy", string.Format("Unsupported iFxProxy objectType {0}", objectType));
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000A657 File Offset: 0x00008857
		public Exception LastException
		{
			get
			{
				return this.lastException;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A660 File Offset: 0x00008860
		public int Config(int ulFlags, int ulTransferMethod)
		{
			if (this.lastException != null)
			{
				return -2147467259;
			}
			int result = 0;
			try
			{
				this.fxCollector.Config(ulFlags, ulTransferMethod);
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A6B0 File Offset: 0x000088B0
		public int TransferBuffer(int cbData, byte[] data, out int cbProcessed)
		{
			int result = 0;
			cbProcessed = 0;
			if (this.lastException != null)
			{
				return -2147467259;
			}
			try
			{
				this.fxCollector.TransferBuffer(data);
				cbProcessed = cbData;
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A704 File Offset: 0x00008904
		public bool IsInterfaceOk(int ulTransferMethod, ref Guid refiid, IntPtr lpPropTagArray, int ulFlags)
		{
			if (this.lastException != null)
			{
				return false;
			}
			bool result;
			try
			{
				this.fxCollector.IsInterfaceOk(ulTransferMethod, refiid, ulFlags);
				result = true;
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = false;
			}
			return result;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A750 File Offset: 0x00008950
		public int GetObjectType(out Guid objType)
		{
			int result = 0;
			objType = Guid.Empty;
			if (this.lastException != null)
			{
				return -2147467259;
			}
			try
			{
				objType = this.fxCollector.GetObjectType();
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A7AC File Offset: 0x000089AC
		public unsafe int GetServerVersion(int cbBufSize, byte* buffer, out int cbBuffer)
		{
			int result = 0;
			cbBuffer = 0;
			if (this.lastException != null)
			{
				return -2147467259;
			}
			try
			{
				byte[] serverVersion = this.fxCollector.GetServerVersion();
				cbBuffer = serverVersion.Length;
				if (cbBuffer > cbBufSize)
				{
					return -2147024774;
				}
				Marshal.Copy(serverVersion, 0, (IntPtr)((void*)buffer), cbBuffer);
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A820 File Offset: 0x00008A20
		public unsafe int TellPartnerVersion(int cbBuffer, byte* buffer)
		{
			if (this.lastException != null)
			{
				return -2147467259;
			}
			int result = 0;
			try
			{
				byte[] array = new byte[cbBuffer];
				Marshal.Copy((IntPtr)((void*)buffer), array, 0, cbBuffer);
				this.fxCollector.TellPartnerVersion(array);
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A884 File Offset: 0x00008A84
		public int GetLastLowLevelError(out int lowLevelError)
		{
			lowLevelError = -2147467259;
			return 0;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A890 File Offset: 0x00008A90
		public bool IsPrivateLogon()
		{
			if (this.lastException != null)
			{
				return false;
			}
			bool result = false;
			try
			{
				result = this.fxCollector.IsPrivateLogon();
			}
			catch (Exception ex)
			{
				this.lastException = ex;
			}
			return result;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A8D4 File Offset: 0x00008AD4
		public int StartMdbEventsImport()
		{
			if (this.lastException != null)
			{
				return -2147467259;
			}
			int result = 0;
			try
			{
				this.fxCollector.StartMdbEventsImport();
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A920 File Offset: 0x00008B20
		public int FinishMdbEventsImport(bool success)
		{
			if (this.lastException != null)
			{
				return -2147467259;
			}
			int result = 0;
			try
			{
				this.fxCollector.FinishMdbEventsImport(success);
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A96C File Offset: 0x00008B6C
		public int AddMdbEvents(int cbRequest, byte[] pbRequest)
		{
			if (this.lastException != null)
			{
				return -2147467259;
			}
			int result = 0;
			try
			{
				this.fxCollector.AddMdbEvents(pbRequest);
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		public int SetWatermarks(int cWMs, MDBEVENTWMRAW[] WMs)
		{
			if (this.lastException != null)
			{
				return -2147467259;
			}
			int result = 0;
			try
			{
				this.fxCollector.SetWatermarks(WMs);
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000AA04 File Offset: 0x00008C04
		public int SetReceiveFolder(int cbEntryId, byte[] entryId, string messageClass)
		{
			if (this.lastException != null)
			{
				return -2147467259;
			}
			int result = 0;
			try
			{
				this.fxCollector.SetReceiveFolder(entryId, messageClass);
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000AA54 File Offset: 0x00008C54
		public unsafe int SetPerUser(ref MapiLtidNative ltid, Guid* pguidReplica, int lib, byte[] pb, int cb, bool fLast)
		{
			if (this.lastException != null)
			{
				return -2147467259;
			}
			int result = 0;
			try
			{
				Guid pguidReplica2 = (pguidReplica == null) ? Guid.Empty : (*pguidReplica);
				this.fxCollector.SetPerUser(ltid, pguidReplica2, lib, pb, fLast);
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		public unsafe int SetProps(int cValues, SPropValue* lpPropArray)
		{
			if (this.lastException != null)
			{
				return -2147467259;
			}
			int result = 0;
			try
			{
				PropValue[] array = new PropValue[cValues];
				for (int i = 0; i < cValues; i++)
				{
					array[i] = new PropValue(lpPropArray + i);
				}
				this.fxCollector.SetProps(array);
			}
			catch (Exception ex)
			{
				this.lastException = ex;
				result = -2147467259;
			}
			return result;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000AB3C File Offset: 0x00008D3C
		public unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError)
		{
			lpMapiError = (IntPtr)((UIntPtr)0);
			return -2147221246;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000AB47 File Offset: 0x00008D47
		public int SaveChanges(int ulFlags)
		{
			return -2147221246;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000AB4E File Offset: 0x00008D4E
		public int GetProps(PropTag[] lpPropTagArray, int ulFlags, out int lpcValues, out SafeExLinkedMemoryHandle lppPropArray)
		{
			lpcValues = 0;
			lppPropArray = null;
			return -2147221246;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000AB5C File Offset: 0x00008D5C
		public int GetPropList(int ulFlags, out SafeExLinkedMemoryHandle propList)
		{
			propList = null;
			return -2147221246;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000AB66 File Offset: 0x00008D66
		public int OpenProperty(int propTag, Guid lpInterface, int interfaceOptions, int ulFlags, out object obj)
		{
			if (propTag == 1714356237 && lpInterface == InterfaceIds.IExchangeFastTransferEx)
			{
				obj = this;
				return 0;
			}
			obj = null;
			return -2147467262;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000AB8C File Offset: 0x00008D8C
		public unsafe int SetProps(int cValues, SPropValue* lpPropArray, out SafeExLinkedMemoryHandle lppProblems)
		{
			lppProblems = null;
			return -2147221246;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000AB96 File Offset: 0x00008D96
		public int DeleteProps(PropTag[] lpPropTagArray, out SafeExLinkedMemoryHandle lppProblems)
		{
			lppProblems = null;
			return -2147221246;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public int CopyTo(int ciidExclude, Guid[] rgiidExclude, PropTag[] lpExcludeProps, IntPtr ulUiParam, IntPtr lpProgress, Guid lpInterface, IMAPIProp lpDestObj, int ulFlags, out SafeExLinkedMemoryHandle lppProblems)
		{
			lppProblems = null;
			return -2147221246;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000ABAB File Offset: 0x00008DAB
		public int CopyProps(PropTag[] lpIncludeProps, IntPtr ulUIParam, IntPtr lpProgress, Guid lpInterface, IMAPIProp lpDestObj, int ulFlags, out SafeExLinkedMemoryHandle lppProblems)
		{
			lppProblems = null;
			return -2147221246;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000ABB6 File Offset: 0x00008DB6
		public unsafe int GetNamesFromIDs(int** lppPropTagArray, Guid* lpGuid, int ulFlags, ref int cPropNames, out SafeExLinkedMemoryHandle lppNames)
		{
			lppNames = null;
			return -2147221246;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000ABC1 File Offset: 0x00008DC1
		public unsafe int GetIDsFromNames(int cPropNames, SNameId** lppPropNames, int ulFlags, out SafeExLinkedMemoryHandle lpPropIDs)
		{
			lpPropIDs = null;
			return -2147221246;
		}

		// Token: 0x0400044D RID: 1101
		private IMapiFxCollector fxCollector;

		// Token: 0x0400044E RID: 1102
		private Exception lastException;
	}
}
