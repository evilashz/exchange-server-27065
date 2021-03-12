using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001E0 RID: 480
	internal interface INspiAsyncDispatch
	{
		// Token: 0x06000A16 RID: 2582
		ICancelableAsyncResult BeginBind(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, NspiBindFlags flags, NspiState state, Guid? guid, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A17 RID: 2583
		NspiStatus EndBind(ICancelableAsyncResult asyncResult, out Guid? guid, out IntPtr contextHandle);

		// Token: 0x06000A18 RID: 2584
		ICancelableAsyncResult BeginUnbind(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiUnbindFlags flags, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A19 RID: 2585
		NspiStatus EndUnbind(ICancelableAsyncResult asyncResult, out IntPtr contextHandle);

		// Token: 0x06000A1A RID: 2586
		ICancelableAsyncResult BeginUpdateStat(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiUpdateStatFlags flags, NspiState state, [MarshalAs(UnmanagedType.U1)] bool deltaRequested, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A1B RID: 2587
		NspiStatus EndUpdateStat(ICancelableAsyncResult asyncResult, out NspiState state, out int? delta);

		// Token: 0x06000A1C RID: 2588
		ICancelableAsyncResult BeginQueryRows(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiQueryRowsFlags flags, NspiState state, int[] mids, int count, PropertyTag[] propertyTags, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A1D RID: 2589
		NspiStatus EndQueryRows(ICancelableAsyncResult asyncResult, out NspiState state, out PropertyValue[][] rowset);

		// Token: 0x06000A1E RID: 2590
		ICancelableAsyncResult BeginSeekEntries(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiSeekEntriesFlags flags, NspiState state, PropertyValue? target, int[] restriction, PropertyTag[] propertyTags, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A1F RID: 2591
		NspiStatus EndSeekEntries(ICancelableAsyncResult asyncResult, out NspiState state, out PropertyValue[][] rowset);

		// Token: 0x06000A20 RID: 2592
		ICancelableAsyncResult BeginGetMatches(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetMatchesFlags flags, NspiState state, int[] mids, int interfaceOptions, Restriction restriction, IntPtr propName, int maxRows, PropertyTag[] propertyTags, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A21 RID: 2593
		NspiStatus EndGetMatches(ICancelableAsyncResult asyncResult, out NspiState state, out int[] mids, out PropertyValue[][] rowset);

		// Token: 0x06000A22 RID: 2594
		ICancelableAsyncResult BeginResortRestriction(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiResortRestrictionFlags flags, NspiState state, int[] mids, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A23 RID: 2595
		NspiStatus EndResortRestriction(ICancelableAsyncResult asyncResult, out NspiState state, out int[] mids);

		// Token: 0x06000A24 RID: 2596
		ICancelableAsyncResult BeginDNToEph(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiDNToEphFlags flags, string[] DNs, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A25 RID: 2597
		NspiStatus EndDNToEph(ICancelableAsyncResult asyncResult, out int[] mids);

		// Token: 0x06000A26 RID: 2598
		ICancelableAsyncResult BeginGetPropList(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetPropListFlags flags, int mid, int codePage, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A27 RID: 2599
		NspiStatus EndGetPropList(ICancelableAsyncResult asyncResult, out PropertyTag[] propertyTags);

		// Token: 0x06000A28 RID: 2600
		ICancelableAsyncResult BeginGetProps(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetPropsFlags flags, NspiState state, PropertyTag[] propertyTags, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A29 RID: 2601
		NspiStatus EndGetProps(ICancelableAsyncResult asyncResult, out int codePage, out PropertyValue[] row);

		// Token: 0x06000A2A RID: 2602
		ICancelableAsyncResult BeginCompareDNTs(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiCompareDNTsFlags flags, NspiState state, int mid1, int mid2, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A2B RID: 2603
		NspiStatus EndCompareDNTs(ICancelableAsyncResult asyncResult, out int result);

		// Token: 0x06000A2C RID: 2604
		ICancelableAsyncResult BeginModProps(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiModPropsFlags flags, NspiState state, PropertyTag[] propertyTags, PropertyValue[] row, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A2D RID: 2605
		NspiStatus EndModProps(ICancelableAsyncResult asyncResult);

		// Token: 0x06000A2E RID: 2606
		ICancelableAsyncResult BeginGetHierarchyInfo(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetHierarchyInfoFlags flags, NspiState state, int version, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A2F RID: 2607
		NspiStatus EndGetHierarchyInfo(ICancelableAsyncResult asyncResult, out int codePage, out int returnedVersion, out PropertyValue[][] rowset);

		// Token: 0x06000A30 RID: 2608
		ICancelableAsyncResult BeginGetTemplateInfo(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetTemplateInfoFlags flags, int type, string dn, int codePage, int locale, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A31 RID: 2609
		NspiStatus EndGetTemplateInfo(ICancelableAsyncResult asyncResult, out int codePage, out PropertyValue[] row);

		// Token: 0x06000A32 RID: 2610
		ICancelableAsyncResult BeginModLinkAtt(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiModLinkAttFlags flags, PropertyTag propertyTag, int mid, byte[][] entryIds, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A33 RID: 2611
		NspiStatus EndModLinkAtt(ICancelableAsyncResult asyncResult);

		// Token: 0x06000A34 RID: 2612
		ICancelableAsyncResult BeginDeleteEntries(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiDeleteEntriesFlags flags, int mid, byte[][] entryIds, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A35 RID: 2613
		NspiStatus EndDeleteEntries(ICancelableAsyncResult asyncResult);

		// Token: 0x06000A36 RID: 2614
		ICancelableAsyncResult BeginQueryColumns(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiQueryColumnsFlags flags, NspiQueryColumnsMapiFlags mapiFlags, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A37 RID: 2615
		NspiStatus EndQueryColumns(ICancelableAsyncResult asyncResult, out PropertyTag[] columns);

		// Token: 0x06000A38 RID: 2616
		ICancelableAsyncResult BeginGetNamesFromIDs(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetNamesFromIDsFlags flags, Guid? guid, PropertyTag[] propertyTags, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A39 RID: 2617
		NspiStatus EndGetNamesFromIDs(ICancelableAsyncResult asyncResult, out PropertyTag[] propertyTags, out SafeRpcMemoryHandle namesHandle);

		// Token: 0x06000A3A RID: 2618
		ICancelableAsyncResult BeginGetIDsFromNames(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiGetIDsFromNamesFlags flags, int mapiFlags, int nameCount, IntPtr names, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A3B RID: 2619
		NspiStatus EndGetIDsFromNames(ICancelableAsyncResult asyncResult, out PropertyTag[] propertyTags);

		// Token: 0x06000A3C RID: 2620
		ICancelableAsyncResult BeginResolveNames(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiResolveNamesFlags flags, NspiState state, PropertyTag[] propertyTags, byte[][] names, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A3D RID: 2621
		NspiStatus EndResolveNames(ICancelableAsyncResult asyncResult, out int codePage, out int[] mids, out PropertyValue[][] rowset);

		// Token: 0x06000A3E RID: 2622
		ICancelableAsyncResult BeginResolveNamesW(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, NspiResolveNamesFlags flags, NspiState state, PropertyTag[] propertyTags, string[] names, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A3F RID: 2623
		NspiStatus EndResolveNamesW(ICancelableAsyncResult asyncResult, out int codePage, out int[] mids, out PropertyValue[][] rowset);

		// Token: 0x06000A40 RID: 2624
		void ContextHandleRundown(IntPtr contextHandle);
	}
}
