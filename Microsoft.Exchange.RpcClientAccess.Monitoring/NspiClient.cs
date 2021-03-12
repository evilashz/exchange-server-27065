using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.NspiClient;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NspiClient : BaseRpcClient<NspiAsyncRpcClient>, INspiClient, IRpcClient, IDisposable
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x0000344F File Offset: 0x0000164F
		public NspiClient(RpcBindingInfo bindingInfo) : base(new NspiAsyncRpcClient(bindingInfo))
		{
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000345D File Offset: 0x0000165D
		private bool NeedToUnbind
		{
			get
			{
				return this.nspiContextHandle != IntPtr.Zero;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000346F File Offset: 0x0000166F
		public IAsyncResult BeginBind(TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			return new NspiClient.BindRpcCallContext(base.RpcClient, timeout, asyncCallback, asyncState).Begin();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003484 File Offset: 0x00001684
		public NspiCallResult EndBind(IAsyncResult asyncResult)
		{
			return ((NspiClient.BindRpcCallContext)asyncResult).End(asyncResult, out this.nspiContextHandle, out this.serverGuid);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000034AC File Offset: 0x000016AC
		public IAsyncResult BeginUnbind(TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.NeedToUnbind)
			{
				return new NspiClient.UnbindRpcCallContext(base.RpcClient, this.nspiContextHandle, timeout, asyncCallback, asyncState).Begin();
			}
			EasyAsyncResult easyAsyncResult = new EasyAsyncResult(asyncCallback, asyncState);
			easyAsyncResult.InvokeCallback();
			return easyAsyncResult;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000034EC File Offset: 0x000016EC
		public NspiCallResult EndUnbind(IAsyncResult asyncResult)
		{
			if (this.NeedToUnbind)
			{
				IntPtr intPtr;
				NspiCallResult result = ((NspiClient.UnbindRpcCallContext)asyncResult).End(asyncResult, out intPtr);
				this.nspiContextHandle = intPtr;
				return result;
			}
			return NspiCallResult.CreateSuccessfulResult();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000351E File Offset: 0x0000171E
		public IAsyncResult BeginGetHierarchyInfo(TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			return new NspiClient.GetHierarchyInfoRpcCallContext(base.RpcClient, this.nspiContextHandle, timeout, asyncCallback, asyncState).Begin();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000353C File Offset: 0x0000173C
		public NspiCallResult EndGetHierarchyInfo(IAsyncResult asyncResult, out int version)
		{
			return ((NspiClient.GetHierarchyInfoRpcCallContext)asyncResult).End(asyncResult, out version);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003558 File Offset: 0x00001758
		public IAsyncResult BeginGetMatches(string primarySmtpAddress, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			return new NspiClient.GetMatchesRpcCallContext(base.RpcClient, this.nspiContextHandle, primarySmtpAddress, timeout, asyncCallback, asyncState).Begin();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003578 File Offset: 0x00001778
		public NspiCallResult EndGetMatches(IAsyncResult asyncResult, out int[] minimalIds)
		{
			return ((NspiClient.GetMatchesRpcCallContext)asyncResult).End(asyncResult, out minimalIds);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003594 File Offset: 0x00001794
		public IAsyncResult BeginQueryRows(int[] minimalIds, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			return new NspiClient.QueryRowsRpcCallContext(base.RpcClient, this.nspiContextHandle, minimalIds, timeout, asyncCallback, asyncState).Begin();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000035B4 File Offset: 0x000017B4
		public NspiCallResult EndQueryRows(IAsyncResult asyncResult, out string homeMDB, out string userLegacyDN)
		{
			return ((NspiClient.QueryRowsRpcCallContext)asyncResult).End(asyncResult, out homeMDB, out userLegacyDN);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000035D1 File Offset: 0x000017D1
		public IAsyncResult BeginDNToEph(string serverLegacyDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			return new NspiClient.DNToEphRpcCallContext(base.RpcClient, this.nspiContextHandle, serverLegacyDn, timeout, asyncCallback, asyncState).Begin();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000035EE File Offset: 0x000017EE
		public NspiCallResult EndDNToEph(IAsyncResult asyncResult, out int[] minimalIds)
		{
			return ((NspiClient.DNToEphRpcCallContext)asyncResult).End(asyncResult, out minimalIds);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000035FD File Offset: 0x000017FD
		public IAsyncResult BeginGetNetworkAddresses(int[] minimalIds, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			return new NspiClient.GetNetworkAddressesRpcCallContext(base.RpcClient, this.nspiContextHandle, minimalIds, timeout, asyncCallback, asyncState).Begin();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000361C File Offset: 0x0000181C
		public NspiCallResult EndGetNetworkAddresses(IAsyncResult asyncResult, out string[] networkAddresses)
		{
			return ((NspiClient.GetNetworkAddressesRpcCallContext)asyncResult).End(asyncResult, out networkAddresses);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003638 File Offset: 0x00001838
		protected sealed override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiClient>(this);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003640 File Offset: 0x00001840
		protected sealed override void InternalDispose()
		{
			if (this.NeedToUnbind)
			{
				this.EndUnbind(this.BeginUnbind(Constants.DefaultRpcTimeout, null, null));
			}
			base.InternalDispose();
		}

		// Token: 0x04000036 RID: 54
		private static readonly int DefaultLcid = CultureInfo.CurrentUICulture.LCID;

		// Token: 0x04000037 RID: 55
		private static readonly int DefaultANSICodePage = CultureInfo.CurrentUICulture.TextInfo.ANSICodePage;

		// Token: 0x04000038 RID: 56
		private IntPtr nspiContextHandle;

		// Token: 0x04000039 RID: 57
		private Guid serverGuid;

		// Token: 0x0200001C RID: 28
		private abstract class NspiBaseRpcCallContext : RpcCallContext<NspiCallResult>
		{
			// Token: 0x060000CA RID: 202 RVA: 0x0000368C File Offset: 0x0000188C
			protected NspiBaseRpcCallContext(NspiAsyncRpcClient rpcClient, IntPtr contextHandle, NspiState nspiState, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(timeout, asyncCallback, asyncState)
			{
				Util.ThrowOnNullArgument(rpcClient, "rpcClient");
				this.rpcClient = rpcClient;
				this.contextHandle = contextHandle;
				this.nspiState = nspiState;
				if (this.nspiState == null)
				{
					this.nspiState = new NspiState();
					this.nspiState.SortLocale = NspiClient.DefaultLcid;
					this.nspiState.TemplateLocale = NspiClient.DefaultLcid;
					this.nspiState.CodePage = NspiClient.DefaultANSICodePage;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000CB RID: 203 RVA: 0x00003713 File Offset: 0x00001913
			protected NspiAsyncRpcClient NspiAsyncRpcClient
			{
				get
				{
					return this.rpcClient;
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000CC RID: 204 RVA: 0x0000371B File Offset: 0x0000191B
			// (set) Token: 0x060000CD RID: 205 RVA: 0x00003723 File Offset: 0x00001923
			protected NspiState NspiState
			{
				get
				{
					return this.nspiState;
				}
				set
				{
					this.nspiState = value;
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000CE RID: 206 RVA: 0x0000372C File Offset: 0x0000192C
			// (set) Token: 0x060000CF RID: 207 RVA: 0x00003734 File Offset: 0x00001934
			protected IntPtr ContextHandle
			{
				get
				{
					return this.contextHandle;
				}
				set
				{
					this.contextHandle = value;
				}
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x00003740 File Offset: 0x00001940
			protected override NspiCallResult ConvertExceptionToResult(Exception exception)
			{
				RpcException ex = exception as RpcException;
				if (ex != null)
				{
					return this.OnRpcException(ex);
				}
				NspiDataException ex2 = exception as NspiDataException;
				if (ex2 != null)
				{
					return this.OnNspiDataException(ex2);
				}
				return null;
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x00003772 File Offset: 0x00001972
			protected override NspiCallResult OnRpcException(RpcException rpcException)
			{
				return new NspiCallResult(rpcException);
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x0000377A File Offset: 0x0000197A
			protected NspiCallResult OnNspiDataException(NspiDataException nspiException)
			{
				return new NspiCallResult(nspiException);
			}

			// Token: 0x0400003A RID: 58
			private readonly NspiAsyncRpcClient rpcClient;

			// Token: 0x0400003B RID: 59
			private NspiState nspiState;

			// Token: 0x0400003C RID: 60
			private IntPtr contextHandle = IntPtr.Zero;
		}

		// Token: 0x0200001D RID: 29
		private sealed class BindRpcCallContext : NspiClient.NspiBaseRpcCallContext
		{
			// Token: 0x060000D3 RID: 211 RVA: 0x00003782 File Offset: 0x00001982
			public BindRpcCallContext(NspiAsyncRpcClient rpcClient, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(rpcClient, IntPtr.Zero, null, timeout, asyncCallback, asyncState)
			{
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00003798 File Offset: 0x00001998
			public NspiCallResult End(IAsyncResult asyncResult, out IntPtr contextHandle, out Guid serverGuid)
			{
				NspiCallResult result = base.GetResult();
				contextHandle = base.ContextHandle;
				serverGuid = this.serverGuid;
				return result;
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x000037C8 File Offset: 0x000019C8
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				ICancelableAsyncResult result = base.NspiAsyncRpcClient.BeginBind(null, null, NspiBindFlags.None, base.NspiState, new Guid?(this.serverGuid), asyncCallback, asyncState);
				base.StartRpcTimer();
				return result;
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x00003800 File Offset: 0x00001A00
			protected override NspiCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				NspiCallResult result;
				try
				{
					Guid? guid;
					IntPtr contextHandle;
					NspiStatus nspiStatus = base.NspiAsyncRpcClient.EndBind(asyncResult, out guid, out contextHandle);
					if (nspiStatus == NspiStatus.Success)
					{
						base.ContextHandle = contextHandle;
						if (guid != null)
						{
							this.serverGuid = guid.Value;
						}
					}
					result = new NspiCallResult(nspiStatus);
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
				}
				return result;
			}

			// Token: 0x0400003D RID: 61
			private Guid serverGuid;
		}

		// Token: 0x0200001E RID: 30
		private sealed class UnbindRpcCallContext : NspiClient.NspiBaseRpcCallContext
		{
			// Token: 0x060000D7 RID: 215 RVA: 0x00003860 File Offset: 0x00001A60
			public UnbindRpcCallContext(NspiAsyncRpcClient rpcClient, IntPtr contextHandle, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(rpcClient, contextHandle, null, timeout, asyncCallback, asyncState)
			{
				Util.ThrowOnIntPtrZero(contextHandle, "contextHandle");
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x0000387C File Offset: 0x00001A7C
			public NspiCallResult End(IAsyncResult asyncResult, out IntPtr contextHandle)
			{
				NspiCallResult result = base.GetResult();
				contextHandle = base.ContextHandle;
				return result;
			}

			// Token: 0x060000D9 RID: 217 RVA: 0x000038A0 File Offset: 0x00001AA0
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				ICancelableAsyncResult result = base.NspiAsyncRpcClient.BeginUnbind(null, base.ContextHandle, NspiUnbindFlags.None, asyncCallback, asyncState);
				base.StartRpcTimer();
				return result;
			}

			// Token: 0x060000DA RID: 218 RVA: 0x000038CC File Offset: 0x00001ACC
			protected override NspiCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				NspiCallResult result;
				try
				{
					IntPtr contextHandle;
					NspiStatus nspiStatus = base.NspiAsyncRpcClient.EndUnbind(asyncResult, out contextHandle);
					base.ContextHandle = contextHandle;
					if (nspiStatus == NspiStatus.UnbindSuccess)
					{
						result = new NspiCallResult(NspiStatus.Success);
					}
					else
					{
						result = new NspiCallResult(NspiStatus.GeneralFailure);
					}
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
				}
				return result;
			}
		}

		// Token: 0x0200001F RID: 31
		private sealed class GetHierarchyInfoRpcCallContext : NspiClient.NspiBaseRpcCallContext
		{
			// Token: 0x060000DB RID: 219 RVA: 0x00003924 File Offset: 0x00001B24
			public GetHierarchyInfoRpcCallContext(NspiAsyncRpcClient rpcClient, IntPtr contextHandle, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(rpcClient, contextHandle, null, timeout, asyncCallback, asyncState)
			{
				Util.ThrowOnIntPtrZero(contextHandle, "contextHandle");
			}

			// Token: 0x060000DC RID: 220 RVA: 0x00003940 File Offset: 0x00001B40
			public NspiCallResult End(IAsyncResult asyncResult, out int version)
			{
				NspiCallResult result = base.GetResult();
				version = this.version;
				return result;
			}

			// Token: 0x060000DD RID: 221 RVA: 0x00003960 File Offset: 0x00001B60
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				ICancelableAsyncResult result = base.NspiAsyncRpcClient.BeginGetHierarchyInfo(null, base.ContextHandle, NspiGetHierarchyInfoFlags.Unicode, base.NspiState, 0, asyncCallback, asyncState);
				base.StartRpcTimer();
				return result;
			}

			// Token: 0x060000DE RID: 222 RVA: 0x00003994 File Offset: 0x00001B94
			protected override NspiCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				NspiCallResult result;
				try
				{
					PropertyValue[][] array = null;
					int num;
					int num2;
					NspiStatus nspiStatus = base.NspiAsyncRpcClient.EndGetHierarchyInfo(asyncResult, out num, out num2, out array);
					this.version = num2;
					result = new NspiCallResult(nspiStatus);
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
				}
				return result;
			}

			// Token: 0x0400003E RID: 62
			private int version;
		}

		// Token: 0x02000020 RID: 32
		private sealed class GetMatchesRpcCallContext : NspiClient.NspiBaseRpcCallContext
		{
			// Token: 0x060000DF RID: 223 RVA: 0x000039E0 File Offset: 0x00001BE0
			public GetMatchesRpcCallContext(NspiAsyncRpcClient rpcClient, IntPtr contextHandle, string primarySmtpAddress, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(rpcClient, contextHandle, null, timeout, asyncCallback, asyncState)
			{
				Util.ThrowOnIntPtrZero(contextHandle, "contextHandle");
				Util.ThrowOnNullOrEmptyArgument(primarySmtpAddress, "primarySmtpAddress");
				this.primarySmtpAddress = primarySmtpAddress;
			}

			// Token: 0x060000E0 RID: 224 RVA: 0x00003A10 File Offset: 0x00001C10
			public NspiCallResult End(IAsyncResult asyncResult, out int[] minimalIds)
			{
				NspiCallResult result = base.GetResult();
				minimalIds = this.minimalIds;
				return result;
			}

			// Token: 0x060000E1 RID: 225 RVA: 0x00003A30 File Offset: 0x00001C30
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				Restriction restriction = new PropertyRestriction(RelationOperator.Equals, PropertyTag.Anr, new PropertyValue?(new PropertyValue(PropertyTag.Anr, this.primarySmtpAddress)));
				ICancelableAsyncResult result = base.NspiAsyncRpcClient.BeginGetMatches(null, base.ContextHandle, NspiGetMatchesFlags.None, base.NspiState, null, 0, restriction, IntPtr.Zero, 100, NspiClient.GetMatchesRpcCallContext.MatchesPropertyTags, asyncCallback, asyncState);
				base.StartRpcTimer();
				return result;
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x00003A90 File Offset: 0x00001C90
			protected override NspiCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				NspiCallResult result;
				try
				{
					NspiState nspiState = null;
					PropertyValue[][] array = null;
					NspiStatus nspiStatus = base.NspiAsyncRpcClient.EndGetMatches(asyncResult, out nspiState, out this.minimalIds, out array);
					base.NspiState = nspiState;
					result = new NspiCallResult(nspiStatus);
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
				}
				return result;
			}

			// Token: 0x0400003F RID: 63
			private static readonly PropertyTag[] MatchesPropertyTags = new PropertyTag[]
			{
				new PropertyTag(805371934U),
				new PropertyTag(974585887U),
				new PropertyTag(973602847U),
				new PropertyTag(974716958U),
				new PropertyTag(972947487U),
				new PropertyTag(974520351U),
				new PropertyTag(973078559U),
				new PropertyTag(805437470U),
				new PropertyTag(268370178U),
				new PropertyTag(268304387U),
				new PropertyTag(956301315U),
				new PropertyTag(956628995U),
				new PropertyTag(267780354U),
				new PropertyTag(805503006U)
			};

			// Token: 0x04000040 RID: 64
			private readonly string primarySmtpAddress;

			// Token: 0x04000041 RID: 65
			private int[] minimalIds;
		}

		// Token: 0x02000021 RID: 33
		private sealed class QueryRowsRpcCallContext : NspiClient.NspiBaseRpcCallContext
		{
			// Token: 0x060000E4 RID: 228 RVA: 0x00003C34 File Offset: 0x00001E34
			public QueryRowsRpcCallContext(NspiAsyncRpcClient rpcClient, IntPtr contextHandle, int[] minimalIds, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(rpcClient, contextHandle, null, timeout, asyncCallback, asyncState)
			{
				Util.ThrowOnIntPtrZero(contextHandle, "contextHandle");
				Util.ThrowOnNullArgument(minimalIds, "minimalIds");
				this.minimalIds = minimalIds;
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x00003C64 File Offset: 0x00001E64
			public NspiCallResult End(IAsyncResult asyncResult, out string homeMDB, out string userLegacyDN)
			{
				NspiCallResult result = base.GetResult();
				homeMDB = this.homeMDB;
				userLegacyDN = this.userLegacyDN;
				return result;
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x00003C8C File Offset: 0x00001E8C
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				ICancelableAsyncResult result = base.NspiAsyncRpcClient.BeginQueryRows(null, base.ContextHandle, NspiQueryRowsFlags.None, base.NspiState, this.minimalIds, 100, NspiClient.QueryRowsRpcCallContext.QueryRowsPropTags, asyncCallback, asyncState);
				base.StartRpcTimer();
				return result;
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x00003CCC File Offset: 0x00001ECC
			protected override NspiCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				NspiCallResult result;
				try
				{
					NspiState nspiState = null;
					PropertyValue[][] array = null;
					NspiStatus nspiStatus = base.NspiAsyncRpcClient.EndQueryRows(asyncResult, out nspiState, out array);
					base.NspiState = nspiState;
					if (nspiStatus == NspiStatus.Success)
					{
						if (array == null)
						{
							return new NspiCallResult(new NspiDataException("QueryHomeMDB::QueryRows", "Rows is null"));
						}
						if (array.Length != 1)
						{
							return new NspiCallResult(new NspiDataException("QueryHomeMDB::QueryRows", string.Format("ExpectedRowCount = {0}, ActualRowCount = {1}", 1, array.Length)));
						}
						if (array[0].Length != NspiClient.QueryRowsRpcCallContext.QueryRowsPropTags.Length)
						{
							return new NspiCallResult(new NspiDataException("QueryHomeMDB::QueryRows", string.Format("ExpectedPropertyCount = {0}, ActualPropertyCount = {1}", NspiClient.QueryRowsRpcCallContext.QueryRowsPropTags.Length, array[0].Length)));
						}
						this.homeMDB = (string)array[0][3].Value;
						this.userLegacyDN = (string)array[0][1].Value;
					}
					result = new NspiCallResult(nspiStatus);
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
				}
				return result;
			}

			// Token: 0x04000042 RID: 66
			private const int IndexEmailAddressAnsi = 1;

			// Token: 0x04000043 RID: 67
			private const int IndexHomeMdb = 3;

			// Token: 0x04000044 RID: 68
			private static readonly PropertyTag[] QueryRowsPropTags = new PropertyTag[]
			{
				new PropertyTag(805371935U),
				new PropertyTag(805503006U),
				new PropertyTag(956301315U),
				new PropertyTag(2147876894U),
				new PropertyTag(237043970U),
				new PropertyTag(1712525342U),
				new PropertyTag(2148470814U),
				new PropertyTag(956628995U)
			};

			// Token: 0x04000045 RID: 69
			private readonly int[] minimalIds;

			// Token: 0x04000046 RID: 70
			private string homeMDB;

			// Token: 0x04000047 RID: 71
			private string userLegacyDN;
		}

		// Token: 0x02000022 RID: 34
		private class DNToEphRpcCallContext : NspiClient.NspiBaseRpcCallContext
		{
			// Token: 0x060000E9 RID: 233 RVA: 0x00003EAC File Offset: 0x000020AC
			public DNToEphRpcCallContext(NspiAsyncRpcClient rpcClient, IntPtr contextHandle, string serverLegacyDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
			{
				int[] array = new int[1];
				this.minimalIds = array;
				base..ctor(rpcClient, contextHandle, null, timeout, asyncCallback, asyncState);
				Util.ThrowOnIntPtrZero(contextHandle, "contextHandle");
				Util.ThrowOnNullOrEmptyArgument(serverLegacyDn, "serverLegacyDn");
				this.serverLegDn = serverLegacyDn;
			}

			// Token: 0x060000EA RID: 234 RVA: 0x00003EF4 File Offset: 0x000020F4
			public NspiCallResult End(IAsyncResult asyncResult, out int[] minimalIds)
			{
				NspiCallResult result = base.GetResult();
				minimalIds = this.minimalIds;
				return result;
			}

			// Token: 0x060000EB RID: 235 RVA: 0x00003F14 File Offset: 0x00002114
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				ICancelableAsyncResult result = base.NspiAsyncRpcClient.BeginDNToEph(null, base.ContextHandle, NspiDNToEphFlags.None, new string[]
				{
					this.serverLegDn
				}, asyncCallback, asyncState);
				base.StartRpcTimer();
				return result;
			}

			// Token: 0x060000EC RID: 236 RVA: 0x00003F50 File Offset: 0x00002150
			protected override NspiCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				NspiCallResult result;
				try
				{
					NspiStatus nspiStatus = base.NspiAsyncRpcClient.EndDNToEph(asyncResult, out this.minimalIds);
					result = new NspiCallResult(nspiStatus);
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
				}
				return result;
			}

			// Token: 0x04000048 RID: 72
			private readonly string serverLegDn;

			// Token: 0x04000049 RID: 73
			private int[] minimalIds;
		}

		// Token: 0x02000023 RID: 35
		private sealed class GetNetworkAddressesRpcCallContext : NspiClient.NspiBaseRpcCallContext
		{
			// Token: 0x060000ED RID: 237 RVA: 0x00003F94 File Offset: 0x00002194
			public GetNetworkAddressesRpcCallContext(NspiAsyncRpcClient rpcClient, IntPtr contextHandle, int[] minimalIds, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(rpcClient, contextHandle, null, timeout, asyncCallback, asyncState)
			{
				Util.ThrowOnIntPtrZero(contextHandle, "contextHandle");
				base.NspiState.CurrentRecord = minimalIds[0];
			}

			// Token: 0x060000EE RID: 238 RVA: 0x00003FCC File Offset: 0x000021CC
			public NspiCallResult End(IAsyncResult asyncResult, out string[] networkAddresses)
			{
				NspiCallResult result = base.GetResult();
				networkAddresses = this.networkAddresses;
				return result;
			}

			// Token: 0x060000EF RID: 239 RVA: 0x00003FEC File Offset: 0x000021EC
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				ICancelableAsyncResult result = base.NspiAsyncRpcClient.BeginGetProps(null, base.ContextHandle, NspiGetPropsFlags.None, base.NspiState, NspiClient.GetNetworkAddressesRpcCallContext.EmsABNetworkAddresses, asyncCallback, asyncState);
				base.StartRpcTimer();
				return result;
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x00004024 File Offset: 0x00002224
			protected override NspiCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				NspiCallResult result;
				try
				{
					int num = 0;
					PropertyValue[] array = null;
					NspiStatus nspiStatus = base.NspiAsyncRpcClient.EndGetProps(asyncResult, out num, out array);
					if (nspiStatus == NspiStatus.Success)
					{
						if (array == null || array.Length != 1)
						{
							throw new NspiDataException("GetNetworkAddresses::GetProps", string.Format("Properties = {0}, Count = {1}", (array != null) ? array.ToString() : "null", (array != null) ? array.Length : -1));
						}
						this.networkAddresses = (string[])array[0].Value;
					}
					result = new NspiCallResult(nspiStatus);
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
				}
				return result;
			}

			// Token: 0x0400004A RID: 74
			public static readonly PropertyTag EmsABNetworkAddress = new PropertyTag(2171605022U);

			// Token: 0x0400004B RID: 75
			public static readonly PropertyTag[] EmsABNetworkAddresses = new PropertyTag[]
			{
				NspiClient.GetNetworkAddressesRpcCallContext.EmsABNetworkAddress
			};

			// Token: 0x0400004C RID: 76
			private string[] networkAddresses = new string[0];
		}
	}
}
