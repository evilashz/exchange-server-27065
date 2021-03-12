using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiClient
{
	// Token: 0x020002EF RID: 751
	internal class NspiAsyncRpcClient : RpcClientBase, INspiAsyncDispatch
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x00033BF4 File Offset: 0x00032FF4
		public NspiAsyncRpcClient(RpcBindingInfo bindingInfo) : base(bindingInfo.UseKerberosSpn("exchangeAB", null))
		{
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x000355F0 File Offset: 0x000349F0
		public virtual ICancelableAsyncResult BeginBind(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, NspiBindFlags flags, NspiState state, Guid? guid, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_Bind clientAsyncCallState_Bind = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				IntPtr bindingHandle = (IntPtr)base.BindingHandle;
				clientAsyncCallState_Bind = new ClientAsyncCallState_Bind(asyncCallback, asyncState, bindingHandle, flags, state, guid);
				clientAsyncCallState_Bind.Begin();
				flag = true;
				result = clientAsyncCallState_Bind;
			}
			finally
			{
				if (!flag && clientAsyncCallState_Bind != null)
				{
					((IDisposable)clientAsyncCallState_Bind).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00035280 File Offset: 0x00034680
		public virtual NspiStatus EndBind(ICancelableAsyncResult asyncResult, out Guid? guid, out IntPtr contextHandle)
		{
			NspiStatus result;
			using (ClientAsyncCallState_Bind clientAsyncCallState_Bind = (ClientAsyncCallState_Bind)asyncResult)
			{
				result = clientAsyncCallState_Bind.End(out guid, out contextHandle);
			}
			return result;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00035658 File Offset: 0x00034A58
		public virtual ICancelableAsyncResult BeginUnbind(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiUnbindFlags flags, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_Unbind clientAsyncCallState_Unbind = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				clientAsyncCallState_Unbind = new ClientAsyncCallState_Unbind(asyncCallback, asyncState, contextHandle, flags);
				clientAsyncCallState_Unbind.Begin();
				flag = true;
				result = clientAsyncCallState_Unbind;
			}
			finally
			{
				if (!flag && clientAsyncCallState_Unbind != null)
				{
					((IDisposable)clientAsyncCallState_Unbind).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000352C8 File Offset: 0x000346C8
		public virtual NspiStatus EndUnbind(ICancelableAsyncResult asyncResult, out IntPtr contextHandle)
		{
			NspiStatus result;
			using (ClientAsyncCallState_Unbind clientAsyncCallState_Unbind = (ClientAsyncCallState_Unbind)asyncResult)
			{
				result = clientAsyncCallState_Unbind.End(out contextHandle);
			}
			return result;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00034410 File Offset: 0x00033810
		public virtual ICancelableAsyncResult BeginUpdateStat(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiUpdateStatFlags flags, NspiState state, [MarshalAs(UnmanagedType.U1)] bool deltaRequested, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginUpdateStat not implemented");
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0003442C File Offset: 0x0003382C
		public virtual NspiStatus EndUpdateStat(ICancelableAsyncResult asyncResult, out NspiState state, out int? delta)
		{
			throw new NotImplementedException("EndUpdateStat not implemented");
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x000356B0 File Offset: 0x00034AB0
		public virtual ICancelableAsyncResult BeginQueryRows(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiQueryRowsFlags flags, NspiState state, int[] mids, int count, PropertyTag[] propTags, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_QueryRows clientAsyncCallState_QueryRows = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				clientAsyncCallState_QueryRows = new ClientAsyncCallState_QueryRows(asyncCallback, asyncState, contextHandle, flags, state, mids, count, propTags);
				clientAsyncCallState_QueryRows.Begin();
				flag = true;
				result = clientAsyncCallState_QueryRows;
			}
			finally
			{
				if (!flag && clientAsyncCallState_QueryRows != null)
				{
					((IDisposable)clientAsyncCallState_QueryRows).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00035310 File Offset: 0x00034710
		public virtual NspiStatus EndQueryRows(ICancelableAsyncResult asyncResult, out NspiState state, out PropertyValue[][] rowset)
		{
			NspiStatus result;
			using (ClientAsyncCallState_QueryRows clientAsyncCallState_QueryRows = (ClientAsyncCallState_QueryRows)asyncResult)
			{
				result = clientAsyncCallState_QueryRows.End(out state, out rowset);
			}
			return result;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00034448 File Offset: 0x00033848
		public virtual ICancelableAsyncResult BeginSeekEntries(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiSeekEntriesFlags flags, NspiState state, PropertyValue? target, int[] restriction, PropertyTag[] propTags, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginSeekEntries not implemented");
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00034464 File Offset: 0x00033864
		public virtual NspiStatus EndSeekEntries(ICancelableAsyncResult asyncResult, out NspiState state, out PropertyValue[][] rowset)
		{
			throw new NotImplementedException("EndSeekEntries not implemented");
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00035710 File Offset: 0x00034B10
		public virtual ICancelableAsyncResult BeginGetMatches(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetMatchesFlags flags, NspiState state, int[] mids, int interfaceOptions, Restriction restriction, IntPtr propName, int maxRows, PropertyTag[] propTags, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_GetMatches clientAsyncCallState_GetMatches = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				clientAsyncCallState_GetMatches = new ClientAsyncCallState_GetMatches(asyncCallback, asyncState, contextHandle, flags, state, mids, interfaceOptions, restriction, propName, maxRows, propTags);
				clientAsyncCallState_GetMatches.Begin();
				flag = true;
				result = clientAsyncCallState_GetMatches;
			}
			finally
			{
				if (!flag && clientAsyncCallState_GetMatches != null)
				{
					((IDisposable)clientAsyncCallState_GetMatches).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00035358 File Offset: 0x00034758
		public virtual NspiStatus EndGetMatches(ICancelableAsyncResult asyncResult, out NspiState state, out int[] mids, out PropertyValue[][] rowset)
		{
			NspiStatus result;
			using (ClientAsyncCallState_GetMatches clientAsyncCallState_GetMatches = (ClientAsyncCallState_GetMatches)asyncResult)
			{
				result = clientAsyncCallState_GetMatches.End(out state, out mids, out rowset);
			}
			return result;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00034480 File Offset: 0x00033880
		public virtual ICancelableAsyncResult BeginResortRestriction(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiResortRestrictionFlags flags, NspiState state, int[] mids, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginResortRestriction not implemented");
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0003449C File Offset: 0x0003389C
		public virtual NspiStatus EndResortRestriction(ICancelableAsyncResult asyncResult, out NspiState state, out int[] mids)
		{
			throw new NotImplementedException("EndResortRestriction not implemented");
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00035774 File Offset: 0x00034B74
		public virtual ICancelableAsyncResult BeginDNToEph(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiDNToEphFlags flags, string[] DNs, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_DNToEph clientAsyncCallState_DNToEph = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				clientAsyncCallState_DNToEph = new ClientAsyncCallState_DNToEph(asyncCallback, asyncState, contextHandle, flags, DNs);
				clientAsyncCallState_DNToEph.Begin();
				flag = true;
				result = clientAsyncCallState_DNToEph;
			}
			finally
			{
				if (!flag && clientAsyncCallState_DNToEph != null)
				{
					((IDisposable)clientAsyncCallState_DNToEph).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x000353A4 File Offset: 0x000347A4
		public virtual NspiStatus EndDNToEph(ICancelableAsyncResult asyncResult, out int[] mids)
		{
			NspiStatus result;
			using (ClientAsyncCallState_DNToEph clientAsyncCallState_DNToEph = (ClientAsyncCallState_DNToEph)asyncResult)
			{
				result = clientAsyncCallState_DNToEph.End(out mids);
			}
			return result;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000344B8 File Offset: 0x000338B8
		public virtual ICancelableAsyncResult BeginGetPropList(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetPropListFlags flags, int mid, int codePage, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginGetPropList not implemented");
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000344D4 File Offset: 0x000338D4
		public virtual NspiStatus EndGetPropList(ICancelableAsyncResult asyncResult, out PropertyTag[] propTags)
		{
			throw new NotImplementedException("EndGetPropList not implemented");
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000357CC File Offset: 0x00034BCC
		public virtual ICancelableAsyncResult BeginGetProps(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetPropsFlags flags, NspiState state, PropertyTag[] propTags, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_GetProps clientAsyncCallState_GetProps = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				clientAsyncCallState_GetProps = new ClientAsyncCallState_GetProps(asyncCallback, asyncState, contextHandle, flags, state, propTags);
				clientAsyncCallState_GetProps.Begin();
				flag = true;
				result = clientAsyncCallState_GetProps;
			}
			finally
			{
				if (!flag && clientAsyncCallState_GetProps != null)
				{
					((IDisposable)clientAsyncCallState_GetProps).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x000353EC File Offset: 0x000347EC
		public virtual NspiStatus EndGetProps(ICancelableAsyncResult asyncResult, out int codePage, out PropertyValue[] row)
		{
			NspiStatus result;
			using (ClientAsyncCallState_GetProps clientAsyncCallState_GetProps = (ClientAsyncCallState_GetProps)asyncResult)
			{
				result = clientAsyncCallState_GetProps.End(out codePage, row);
			}
			return result;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x000344F0 File Offset: 0x000338F0
		public virtual ICancelableAsyncResult BeginCompareDNTs(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiCompareDNTsFlags flags, NspiState state, int mid1, int mid2, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginCompareDNTs not implemented");
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0003450C File Offset: 0x0003390C
		public virtual NspiStatus EndCompareDNTs(ICancelableAsyncResult asyncResult, out int result)
		{
			throw new NotImplementedException("EndCompareDNTs not implemented");
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00034528 File Offset: 0x00033928
		public virtual ICancelableAsyncResult BeginModProps(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiModPropsFlags flags, NspiState state, PropertyTag[] propTags, PropertyValue[] row, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginModProps not implemented");
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00034544 File Offset: 0x00033944
		public virtual NspiStatus EndModProps(ICancelableAsyncResult asyncResult)
		{
			throw new NotImplementedException("EndModProps not implemented");
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00035828 File Offset: 0x00034C28
		public virtual ICancelableAsyncResult BeginGetHierarchyInfo(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetHierarchyInfoFlags flags, NspiState state, int version, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_GetHierarchyInfo clientAsyncCallState_GetHierarchyInfo = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				clientAsyncCallState_GetHierarchyInfo = new ClientAsyncCallState_GetHierarchyInfo(asyncCallback, asyncState, contextHandle, flags, state, version);
				clientAsyncCallState_GetHierarchyInfo.Begin();
				flag = true;
				result = clientAsyncCallState_GetHierarchyInfo;
			}
			finally
			{
				if (!flag && clientAsyncCallState_GetHierarchyInfo != null)
				{
					((IDisposable)clientAsyncCallState_GetHierarchyInfo).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00035438 File Offset: 0x00034838
		public virtual NspiStatus EndGetHierarchyInfo(ICancelableAsyncResult asyncResult, out int codePage, out int returnedVersion, out PropertyValue[][] rowset)
		{
			NspiStatus result;
			using (ClientAsyncCallState_GetHierarchyInfo clientAsyncCallState_GetHierarchyInfo = (ClientAsyncCallState_GetHierarchyInfo)asyncResult)
			{
				result = clientAsyncCallState_GetHierarchyInfo.End(out codePage, out returnedVersion, out rowset);
			}
			return result;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00034560 File Offset: 0x00033960
		public virtual ICancelableAsyncResult BeginGetTemplateInfo(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetTemplateInfoFlags flags, int type, string dn, int codePage, int locale, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginGetTemplateInfo not implemented");
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0003457C File Offset: 0x0003397C
		public virtual NspiStatus EndGetTemplateInfo(ICancelableAsyncResult asyncResult, out int codePage, out PropertyValue[] row)
		{
			throw new NotImplementedException("EndGetTemplateInfo not implemented");
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00034598 File Offset: 0x00033998
		public virtual ICancelableAsyncResult BeginModLinkAtt(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiModLinkAttFlags flags, PropertyTag propTag, int mid, byte[][] entryIds, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginModLinkAtt not implemented");
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000345B4 File Offset: 0x000339B4
		public virtual NspiStatus EndModLinkAtt(ICancelableAsyncResult asyncResult)
		{
			throw new NotImplementedException("EndModLinkAtt not implemented");
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000345D0 File Offset: 0x000339D0
		public virtual ICancelableAsyncResult BeginDeleteEntries(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiDeleteEntriesFlags flags, int mid, byte[][] entryIds, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginDeleteEntries not implemented");
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000345EC File Offset: 0x000339EC
		public virtual NspiStatus EndDeleteEntries(ICancelableAsyncResult asyncResult)
		{
			throw new NotImplementedException("EndDeleteEntries not implemented");
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00034608 File Offset: 0x00033A08
		public virtual ICancelableAsyncResult BeginQueryColumns(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiQueryColumnsFlags flags, NspiQueryColumnsMapiFlags mapiFlags, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginQueryColumns not implemented");
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00034624 File Offset: 0x00033A24
		public virtual NspiStatus EndQueryColumns(ICancelableAsyncResult asyncResult, out PropertyTag[] columns)
		{
			throw new NotImplementedException("EndQueryColumns not implemented");
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00034640 File Offset: 0x00033A40
		public virtual ICancelableAsyncResult BeginGetNamesFromIDs(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetNamesFromIDsFlags flags, Guid? guid, PropertyTag[] propTags, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginGetNamesFromIDs not implemented");
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003465C File Offset: 0x00033A5C
		public virtual NspiStatus EndGetNamesFromIDs(ICancelableAsyncResult asyncResult, out PropertyTag[] propTags, out SafeRpcMemoryHandle namesHandle)
		{
			throw new NotImplementedException("EndGetNamesFromIDs not implemented");
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00034678 File Offset: 0x00033A78
		public virtual ICancelableAsyncResult BeginGetIDsFromNames(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetIDsFromNamesFlags flags, int mapiFlags, int nameCount, IntPtr names, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginGetIDsFromNames not implemented");
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00034694 File Offset: 0x00033A94
		public virtual NspiStatus EndGetIDsFromNames(ICancelableAsyncResult asyncResult, out PropertyTag[] propTags)
		{
			throw new NotImplementedException("EndGetIDsFromNames not implemented");
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x000346B0 File Offset: 0x00033AB0
		public virtual ICancelableAsyncResult BeginResolveNames(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiResolveNamesFlags flags, NspiState state, PropertyTag[] propTags, byte[][] names, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginResolveNames not implemented");
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x000346CC File Offset: 0x00033ACC
		public virtual NspiStatus EndResolveNames(ICancelableAsyncResult asyncResult, out int codePage, out int[] mids, out PropertyValue[][] rowset)
		{
			throw new NotImplementedException("EndResolveNames not implemented");
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000346E8 File Offset: 0x00033AE8
		public virtual ICancelableAsyncResult BeginResolveNamesW(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiResolveNamesFlags flags, NspiState state, PropertyTag[] propTags, string[] names, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException("BeginResolveNamesW not implemented");
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00034704 File Offset: 0x00033B04
		public virtual NspiStatus EndResolveNamesW(ICancelableAsyncResult asyncResult, out int codePage, out int[] mids, out PropertyValue[][] rowset)
		{
			throw new NotImplementedException("EndResolveNamesW not implemented");
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00034720 File Offset: 0x00033B20
		public virtual void ContextHandleRundown(IntPtr contextHandle)
		{
			throw new NotImplementedException("ContextHandleRundown not implemented");
		}
	}
}
