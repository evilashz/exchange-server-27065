using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Nspi
{
	// Token: 0x020001D2 RID: 466
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiVirtualListView
	{
		// Token: 0x060012E9 RID: 4841 RVA: 0x0005B008 File Offset: 0x00059208
		internal NspiVirtualListView(IConfigurationSession session, int codePage, ADObjectId addressListId, PropertyDefinition[] properties)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (addressListId == null)
			{
				throw new ArgumentNullException("addressListId");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			this.session = session;
			this.codePage = codePage;
			this.addressListId = addressListId;
			this.properties = properties;
			this.currentRow = -1;
			this.estimatedRowCount = -1;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0005B071 File Offset: 0x00059271
		public int CurrentRow
		{
			get
			{
				return this.currentRow;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0005B079 File Offset: 0x00059279
		public int EstimatedRowCount
		{
			get
			{
				if (this.estimatedRowCount == -1)
				{
					this.estimatedRowCount = NspiVirtualListView.GetEstimatedRowCount(this.session, this.addressListId.ObjectGuid);
				}
				return this.estimatedRowCount;
			}
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0005B0A8 File Offset: 0x000592A8
		public ADRawEntry[] GetPropertyBags(int offset, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return Array<ADRawEntry>.Empty;
			}
			Encoding encoding = Encoding.GetEncoding(this.codePage);
			NspiPropertyMap nspiPropertyMap = NspiPropertyMap.Create(this.properties, encoding);
			PropRowSet propRowSet = null;
			try
			{
				using (NspiRpcClientConnection nspiRpcClientConnection = this.session.GetNspiRpcClientConnection())
				{
					int addressListEphemeralId = NspiVirtualListView.GetAddressListEphemeralId(nspiRpcClientConnection, this.addressListId.ObjectGuid);
					if (addressListEphemeralId == 0)
					{
						return null;
					}
					NspiState nspiState = new NspiState
					{
						SortIndex = SortIndex.DisplayName,
						ContainerId = addressListEphemeralId,
						CurrentRecord = 0,
						Delta = offset,
						CodePage = this.codePage,
						TemplateLocale = this.session.Lcid,
						SortLocale = this.session.Lcid
					};
					using (SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle(nspiState.GetBytesToMarshal()))
					{
						SafeRpcMemoryHandle safeRpcMemoryHandle2 = null;
						try
						{
							IntPtr intPtr = safeRpcMemoryHandle.DangerousGetHandle();
							nspiState.MarshalToNative(intPtr);
							int num = nspiRpcClientConnection.RpcClient.UpdateStat(intPtr);
							if (num != 0)
							{
								throw new NspiFailureException(num);
							}
							num = nspiRpcClientConnection.RpcClient.QueryRows(0, intPtr, null, count, nspiPropertyMap.NspiProperties, out safeRpcMemoryHandle2);
							if (num != 0)
							{
								throw new NspiFailureException(num);
							}
							nspiState.MarshalFromNative(intPtr);
							this.currentRow = nspiState.Position;
							this.estimatedRowCount = nspiState.TotalRecords;
							if (safeRpcMemoryHandle2 != null)
							{
								propRowSet = new PropRowSet(safeRpcMemoryHandle2, true);
							}
						}
						finally
						{
							if (safeRpcMemoryHandle2 != null)
							{
								safeRpcMemoryHandle2.Dispose();
							}
						}
					}
				}
			}
			catch (RpcException ex)
			{
				throw new DataSourceOperationException(DirectoryStrings.NspiRpcError(ex.Message), ex);
			}
			if (propRowSet == null)
			{
				return null;
			}
			return nspiPropertyMap.Convert(propRowSet);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0005B2B8 File Offset: 0x000594B8
		internal static int GetEstimatedRowCount(IConfigurationSession session, Guid addressListObjectGuid)
		{
			int result;
			try
			{
				using (NspiRpcClientConnection nspiRpcClientConnection = session.GetNspiRpcClientConnection())
				{
					int addressListEphemeralId = NspiVirtualListView.GetAddressListEphemeralId(nspiRpcClientConnection, addressListObjectGuid);
					if (addressListEphemeralId == 0)
					{
						result = -1;
					}
					else
					{
						NspiState nspiState = new NspiState
						{
							ContainerId = addressListEphemeralId,
							CodePage = 1252,
							TemplateLocale = 1033,
							SortLocale = 1033
						};
						using (SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle(nspiState.GetBytesToMarshal()))
						{
							IntPtr intPtr = safeRpcMemoryHandle.DangerousGetHandle();
							nspiState.MarshalToNative(intPtr);
							int num = nspiRpcClientConnection.RpcClient.UpdateStat(intPtr);
							if (num != 0)
							{
								throw new NspiFailureException(num);
							}
							nspiState.MarshalFromNative(intPtr);
						}
						result = nspiState.TotalRecords;
					}
				}
			}
			catch (RpcException ex)
			{
				throw new DataSourceOperationException(DirectoryStrings.NspiRpcError(ex.Message), ex);
			}
			return result;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0005B3B4 File Offset: 0x000595B4
		private static int GetAddressListEphemeralId(NspiRpcClientConnection nspiRpcClientConnection, Guid addressListObjectGuid)
		{
			string text = LegacyDN.FormatLegacyDnFromGuid(Guid.Empty, addressListObjectGuid);
			int[] array;
			int num = nspiRpcClientConnection.RpcClient.DNToEph(new string[]
			{
				text
			}, out array);
			if (num != 0)
			{
				throw new NspiFailureException(num);
			}
			return array[0];
		}

		// Token: 0x04000AD3 RID: 2771
		private readonly IConfigurationSession session;

		// Token: 0x04000AD4 RID: 2772
		private readonly int codePage;

		// Token: 0x04000AD5 RID: 2773
		private readonly ADObjectId addressListId;

		// Token: 0x04000AD6 RID: 2774
		private readonly PropertyDefinition[] properties;

		// Token: 0x04000AD7 RID: 2775
		private int currentRow;

		// Token: 0x04000AD8 RID: 2776
		private int estimatedRowCount;
	}
}
