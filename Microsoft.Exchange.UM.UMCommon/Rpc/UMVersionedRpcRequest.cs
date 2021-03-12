using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public abstract class UMVersionedRpcRequest
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000DDD3 File Offset: 0x0000BFD3
		public UMVersionedRpcRequest()
		{
			this.version = this.CurrentVersion;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000DDE7 File Offset: 0x0000BFE7
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000DDEF File Offset: 0x0000BFEF
		public Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000DDF8 File Offset: 0x0000BFF8
		internal virtual Version CurrentVersion
		{
			get
			{
				return UMVersionedRpcRequest.Version10;
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000DE00 File Offset: 0x0000C000
		internal static UMRpcResponse ExecuteRequest(UMVersionedRpcRequest request)
		{
			UMRpcResponse result = null;
			try
			{
				if (request.Version == null || request.Version.Major != request.CurrentVersion.Major || request.Version.Minor > request.CurrentVersion.Minor)
				{
					throw new UMRPCIncompabibleVersionException();
				}
				result = request.Execute();
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, 0, "{0}.ExecuteRequest: {1}", new object[]
				{
					request,
					ex
				});
				request.LogErrorEvent(ex);
				UMRPCComponentBase.HandleException(ex);
				throw new UMRpcException(ex);
			}
			return result;
		}

		// Token: 0x060003D6 RID: 982
		internal abstract UMRpcResponse Execute();

		// Token: 0x060003D7 RID: 983
		internal abstract string GetFriendlyName();

		// Token: 0x060003D8 RID: 984
		protected abstract void LogErrorEvent(Exception ex);

		// Token: 0x060003D9 RID: 985 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type:{0}({1}) RequestVersion:{2} CurrentVersion:{3} HashCode:{4}", new object[]
			{
				base.GetType().Name,
				this.GetFriendlyName(),
				this.Version,
				this.CurrentVersion,
				this.GetHashCode()
			});
		}

		// Token: 0x040002B2 RID: 690
		public static readonly Version Version10 = new Version(1, 0);

		// Token: 0x040002B3 RID: 691
		private Version version;
	}
}
