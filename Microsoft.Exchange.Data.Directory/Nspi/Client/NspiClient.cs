using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Nspi.Client
{
	// Token: 0x020001CC RID: 460
	internal class NspiClient : IDisposeTrackable, IDisposable
	{
		// Token: 0x060012A8 RID: 4776 RVA: 0x00059B51 File Offset: 0x00057D51
		public NspiClient(string host) : this(host, null)
		{
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00059B5C File Offset: 0x00057D5C
		public NspiClient(string host, NetworkCredential nc)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.disposeTracker = this.GetDisposeTracker();
				this.client = new NspiRpcClient(host, "ncacn_ip_tcp", nc);
				this.stat = new NspiState();
				this.stat.SortLocale = 1033;
				this.stat.TemplateLocale = 1033;
				this.stat.CodePage = 1252;
				this.statHandle = new SafeRpcMemoryHandle(this.stat.GetBytesToMarshal());
				disposeGuard.Success();
			}
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00059C0C File Offset: 0x00057E0C
		public NspiClient(string machinename, string proxyserver, NetworkCredential nc) : this(machinename, proxyserver, nc, "ncacn_http:6004")
		{
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00059C1C File Offset: 0x00057E1C
		public NspiClient(string machinename, string proxyserver, NetworkCredential nc, string protocolSequence) : this(machinename, proxyserver, nc, protocolSequence, HTTPAuthentication.Basic, AuthenticationService.Negotiate, machinename)
		{
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00059C2D File Offset: 0x00057E2D
		public NspiClient(string machinename, string proxyserver, NetworkCredential nc, string protocolSequence, HTTPAuthentication httpAuth, AuthenticationService authService) : this(machinename, proxyserver, nc, protocolSequence, httpAuth, authService, machinename)
		{
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00059C40 File Offset: 0x00057E40
		public NspiClient(string machinename, string proxyserver, NetworkCredential nc, string protocolSequence, HTTPAuthentication httpAuth, AuthenticationService authService, string instanceName) : this(machinename, proxyserver, nc, protocolSequence, httpAuth, authService, instanceName, string.Empty, true)
		{
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00059C64 File Offset: 0x00057E64
		public NspiClient(string machinename, string proxyserver, NetworkCredential nc, string protocolSequence, HTTPAuthentication httpAuth, AuthenticationService authService, string instanceName, string certificateSubjectName, bool useEncryption = true)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.disposeTracker = this.GetDisposeTracker();
				this.client = new NspiRpcClient(machinename, proxyserver, protocolSequence, nc, (HttpAuthenticationScheme)httpAuth, authService, instanceName, true, certificateSubjectName, useEncryption);
				this.stat = new NspiState();
				this.stat.SortLocale = 1033;
				this.stat.TemplateLocale = 1033;
				this.stat.CodePage = 1252;
				this.statHandle = new SafeRpcMemoryHandle(this.stat.GetBytesToMarshal());
				disposeGuard.Success();
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00059D20 File Offset: 0x00057F20
		internal NspiState Stat
		{
			get
			{
				return this.stat;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00059D28 File Offset: 0x00057F28
		internal Guid ServerGuid
		{
			get
			{
				return this.serverGuid;
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00059D30 File Offset: 0x00057F30
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiClient>(this);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00059D38 File Offset: 0x00057F38
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00059D4D File Offset: 0x00057F4D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00059D5C File Offset: 0x00057F5C
		public NspiStatus Bind(NspiBindFlags flags)
		{
			NspiStatus result;
			using (SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle(16))
			{
				this.MarshalStatToNative();
				NspiStatus nspiStatus = this.client.Bind(flags, this.statHandle.DangerousGetHandle(), safeRpcMemoryHandle.DangerousGetHandle());
				this.MarshalNativeToStat();
				byte[] array = new byte[16];
				Marshal.Copy(safeRpcMemoryHandle.DangerousGetHandle(), array, 0, array.Length);
				this.serverGuid = new Guid(array);
				this.needToUnbind = (nspiStatus == NspiStatus.Success);
				result = nspiStatus;
			}
			return result;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00059DE8 File Offset: 0x00057FE8
		public int Unbind()
		{
			this.needToUnbind = false;
			return this.client.Unbind();
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00059DFC File Offset: 0x00057FFC
		public NspiStatus GetHierarchyInfo(NspiGetHierarchyInfoFlags flags, ref uint version, out PropRowSet rowset)
		{
			this.MarshalStatToNative();
			SafeRpcMemoryHandle rowsetHandle;
			NspiStatus hierarchyInfo = this.client.GetHierarchyInfo(flags, this.statHandle.DangerousGetHandle(), ref version, out rowsetHandle);
			this.MarshalNativeToStat();
			rowset = NspiClient.GetRowSetAndDisposeHandle(rowsetHandle);
			return hierarchyInfo;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00059E3C File Offset: 0x0005803C
		public NspiStatus GetMatches(Restriction restriction, object propName, int maxRows, IList<PropTag> propTags, out int[] mids, out PropRowSet rowset)
		{
			NspiStatus result;
			using (SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle())
			{
				if (restriction != null)
				{
					safeRpcMemoryHandle.Allocate(restriction.GetBytesToMarshalNspi());
					restriction.MarshalToNativeNspi(safeRpcMemoryHandle);
				}
				int[] intArrayFromPropTagArray = NspiClient.GetIntArrayFromPropTagArray(propTags);
				this.MarshalStatToNative();
				SafeRpcMemoryHandle rowsetHandle;
				NspiStatus matches = this.client.GetMatches(this.statHandle.DangerousGetHandle(), safeRpcMemoryHandle.DangerousGetHandle(), IntPtr.Zero, maxRows, intArrayFromPropTagArray, out mids, out rowsetHandle);
				this.MarshalNativeToStat();
				rowset = NspiClient.GetRowSetAndDisposeHandle(rowsetHandle);
				result = matches;
			}
			return result;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00059ECC File Offset: 0x000580CC
		public NspiStatus QueryRows(NspiQueryRowsFlags flags, int[] mids, int count, IList<PropTag> propTags, out PropRowSet rowset)
		{
			int[] intArrayFromPropTagArray = NspiClient.GetIntArrayFromPropTagArray(propTags);
			this.MarshalStatToNative();
			SafeRpcMemoryHandle rowsetHandle;
			NspiStatus result = this.client.QueryRows(flags, this.statHandle.DangerousGetHandle(), mids, count, intArrayFromPropTagArray, out rowsetHandle);
			this.MarshalNativeToStat();
			rowset = NspiClient.GetRowSetAndDisposeHandle(rowsetHandle);
			return result;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00059F14 File Offset: 0x00058114
		public NspiStatus DnToEph(string[] distinguishedNames, out int[] mids)
		{
			return this.client.DNToEph(distinguishedNames, out mids);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00059F24 File Offset: 0x00058124
		public NspiStatus ResolveNames(string[] names, IList<PropTag> propTags, out ResolveResult[] results, out PropRowSet rowset)
		{
			int[] intArrayFromPropTagArray = NspiClient.GetIntArrayFromPropTagArray(propTags);
			this.MarshalStatToNative();
			int[] array;
			SafeRpcMemoryHandle rowsetHandle;
			NspiStatus result = this.client.ResolveNames(this.statHandle.DangerousGetHandle(), intArrayFromPropTagArray, names, out array, out rowsetHandle);
			this.MarshalNativeToStat();
			rowset = NspiClient.GetRowSetAndDisposeHandle(rowsetHandle);
			if (array != null)
			{
				results = new ResolveResult[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					results[i] = (ResolveResult)array[i];
				}
			}
			else
			{
				results = null;
			}
			return result;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00059F98 File Offset: 0x00058198
		public NspiStatus ResolveNames(byte[][] names, IList<PropTag> propTags, out ResolveResult[] results, out PropRowSet rowset)
		{
			int[] intArrayFromPropTagArray = NspiClient.GetIntArrayFromPropTagArray(propTags);
			this.MarshalStatToNative();
			int[] array;
			SafeRpcMemoryHandle rowsetHandle;
			NspiStatus result = this.client.ResolveNames(this.statHandle.DangerousGetHandle(), intArrayFromPropTagArray, names, out array, out rowsetHandle);
			this.MarshalNativeToStat();
			rowset = NspiClient.GetRowSetAndDisposeHandle(rowsetHandle);
			if (array != null)
			{
				results = new ResolveResult[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					results[i] = (ResolveResult)array[i];
				}
			}
			else
			{
				results = null;
			}
			return result;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0005A00C File Offset: 0x0005820C
		public NspiStatus GetProps(NspiGetPropsFlags flags, IList<PropTag> propTags, out PropRow row)
		{
			int[] intArrayFromPropTagArray = NspiClient.GetIntArrayFromPropTagArray(propTags);
			this.MarshalStatToNative();
			SafeRpcMemoryHandle rowHandle;
			NspiStatus props = this.client.GetProps(flags, this.statHandle.DangerousGetHandle(), intArrayFromPropTagArray, out rowHandle);
			this.MarshalNativeToStat();
			row = NspiClient.GetRowAndDisposeHandle(rowHandle);
			return props;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0005A050 File Offset: 0x00058250
		public NspiStatus GetTemplateInfo(NspiGetTemplateInfoFlags flags, int type, string dn, int codePage, int localeId, out PropRow row)
		{
			SafeRpcMemoryHandle rowHandle;
			NspiStatus templateInfo = this.client.GetTemplateInfo(flags, type, dn, codePage, localeId, out rowHandle);
			row = NspiClient.GetRowAndDisposeHandle(rowHandle);
			return templateInfo;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0005A07C File Offset: 0x0005827C
		public NspiStatus UpdateStat(out int delta)
		{
			this.MarshalStatToNative();
			NspiStatus result = this.client.UpdateStat(this.statHandle.DangerousGetHandle(), out delta);
			this.MarshalNativeToStat();
			return result;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0005A0B0 File Offset: 0x000582B0
		public NspiStatus UpdateStat()
		{
			this.MarshalStatToNative();
			NspiStatus result = this.client.UpdateStat(this.statHandle.DangerousGetHandle());
			this.MarshalNativeToStat();
			return result;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0005A0E4 File Offset: 0x000582E4
		public NspiStatus ResortRestriction(int[] mids, out int[] returnedMids)
		{
			this.MarshalStatToNative();
			NspiStatus result = this.client.ResortRestriction(this.statHandle.DangerousGetHandle(), mids, out returnedMids);
			this.MarshalNativeToStat();
			return result;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0005A118 File Offset: 0x00058318
		public NspiStatus GetPropList(NspiGetPropListFlags flags, int mid, int codePage, out PropTag[] props)
		{
			this.MarshalStatToNative();
			int[] propTagsAsInts;
			NspiStatus propList = this.client.GetPropList(flags, mid, codePage, out propTagsAsInts);
			this.MarshalNativeToStat();
			props = NspiClient.GetPropTagArrayFromIntArray(propTagsAsInts);
			return propList;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0005A14C File Offset: 0x0005834C
		public NspiStatus CompareMids(int mid1, int mid2, out int result)
		{
			this.MarshalStatToNative();
			NspiStatus result2 = this.client.CompareMids(this.statHandle.DangerousGetHandle(), mid1, mid2, out result);
			this.MarshalNativeToStat();
			return result2;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0005A180 File Offset: 0x00058380
		public NspiStatus ModProps(IList<PropTag> propTags, PropRow row)
		{
			int[] intArrayFromPropTagArray = NspiClient.GetIntArrayFromPropTagArray(propTags);
			NspiStatus result;
			using (SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle(row.GetBytesToMarshal()))
			{
				SafeRpcMemoryHandle safeRpcMemoryHandle2 = NspiMarshal.MarshalPropValueCollection(row.Properties);
				row.MarshalledPropertiesHandle = safeRpcMemoryHandle2;
				safeRpcMemoryHandle.AddAssociatedHandle(safeRpcMemoryHandle2);
				row.MarshalToNative(safeRpcMemoryHandle);
				this.MarshalStatToNative();
				NspiStatus nspiStatus = this.client.ModProps(this.statHandle.DangerousGetHandle(), intArrayFromPropTagArray, safeRpcMemoryHandle.DangerousGetHandle());
				this.MarshalNativeToStat();
				result = nspiStatus;
			}
			return result;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0005A20C File Offset: 0x0005840C
		public NspiStatus QueryColumns(NspiQueryColumnsFlags flags, out PropTag[] propTags)
		{
			int[] propTagsAsInts;
			NspiStatus result = this.client.QueryColumns((int)flags, out propTagsAsInts);
			propTags = NspiClient.GetPropTagArrayFromIntArray(propTagsAsInts);
			return result;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0005A231 File Offset: 0x00058431
		public NspiStatus ModLinkAtt(NspiModLinkAttFlags flags, PropTag propTag, int mid, byte[][] entryIDs)
		{
			return this.client.ModLinkAtt(flags, (int)propTag, mid, entryIDs);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0005A244 File Offset: 0x00058444
		public NspiStatus SeekEntries(PropValue propValue, int[] mids, IList<PropTag> propTags, out PropRowSet rowset)
		{
			int[] intArrayFromPropTagArray = NspiClient.GetIntArrayFromPropTagArray(propTags);
			IList<PropValue> list = new List<PropValue>();
			list.Add(propValue);
			NspiStatus result;
			using (SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle(propValue.GetBytesToMarshal()))
			{
				PropValue.MarshalToNative(list, safeRpcMemoryHandle);
				this.MarshalStatToNative();
				SafeRpcMemoryHandle rowsetHandle;
				NspiStatus nspiStatus = this.client.SeekEntries(this.statHandle.DangerousGetHandle(), safeRpcMemoryHandle.DangerousGetHandle(), mids, intArrayFromPropTagArray, out rowsetHandle);
				this.MarshalNativeToStat();
				rowset = NspiClient.GetRowSetAndDisposeHandle(rowsetHandle);
				result = nspiStatus;
			}
			return result;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0005A2D4 File Offset: 0x000584D4
		public int Ping()
		{
			return this.client.Ping();
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0005A2E4 File Offset: 0x000584E4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.statHandle != null)
				{
					this.statHandle.Dispose();
					this.statHandle = null;
				}
				if (this.client != null)
				{
					if (this.needToUnbind)
					{
						try
						{
							this.client.Unbind();
						}
						catch (RpcException)
						{
						}
					}
					this.client.Dispose();
					this.client = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
			}
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0005A36C File Offset: 0x0005856C
		private static PropRow GetRowAndDisposeHandle(SafeRpcMemoryHandle rowHandle)
		{
			PropRow result;
			if (rowHandle != null)
			{
				result = new PropRow(rowHandle, true);
				rowHandle.Dispose();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0005A390 File Offset: 0x00058590
		private static PropRowSet GetRowSetAndDisposeHandle(SafeRpcMemoryHandle rowsetHandle)
		{
			PropRowSet result;
			if (rowsetHandle != null)
			{
				result = new PropRowSet(rowsetHandle, true);
				rowsetHandle.Dispose();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0005A3B4 File Offset: 0x000585B4
		private static int[] GetIntArrayFromPropTagArray(IList<PropTag> propTags)
		{
			int[] array = null;
			if (propTags != null)
			{
				array = new int[propTags.Count];
				for (int i = 0; i < propTags.Count; i++)
				{
					array[i] = (int)propTags[i];
				}
			}
			return array;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0005A3F0 File Offset: 0x000585F0
		private static PropTag[] GetPropTagArrayFromIntArray(int[] propTagsAsInts)
		{
			PropTag[] array = null;
			if (propTagsAsInts != null)
			{
				array = new PropTag[propTagsAsInts.Length];
				for (int i = 0; i < propTagsAsInts.Length; i++)
				{
					array[i] = (PropTag)propTagsAsInts[i];
				}
			}
			return array;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0005A420 File Offset: 0x00058620
		private void MarshalStatToNative()
		{
			this.stat.MarshalToNative(this.statHandle.DangerousGetHandle());
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0005A438 File Offset: 0x00058638
		private void MarshalNativeToStat()
		{
			this.stat.MarshalFromNative(this.statHandle.DangerousGetHandle());
		}

		// Token: 0x04000ABA RID: 2746
		private const int DefaultLcid = 1033;

		// Token: 0x04000ABB RID: 2747
		private const int DefaultANSICodePage = 1252;

		// Token: 0x04000ABC RID: 2748
		private NspiRpcClient client;

		// Token: 0x04000ABD RID: 2749
		private Guid serverGuid;

		// Token: 0x04000ABE RID: 2750
		private NspiState stat;

		// Token: 0x04000ABF RID: 2751
		private SafeRpcMemoryHandle statHandle;

		// Token: 0x04000AC0 RID: 2752
		private DisposeTracker disposeTracker;

		// Token: 0x04000AC1 RID: 2753
		private bool needToUnbind;
	}
}
