using System;
using System.Net;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000006 RID: 6
	internal sealed class ConnectionInfo : WatsonHelper.IProvideWatsonReportData
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000034AC File Offset: 0x000016AC
		public ConnectionInfo(Activity activity, SecurityIdentifier securityIndentifier, string userDn, ConnectionFlags connectionFlags, LocaleInfo localeInfo, MapiVersion clientVersion, IPAddress clientIpAddress, IPAddress serverIpAddress, string protocolSequence, bool isEncrypted, OrganizationId organizationId, string rpcServerTarget, bool isAnonymous, DispatchOptions dispatchOptions)
		{
			this.Activity = activity;
			this.securityIndentifier = securityIndentifier;
			this.UserDn = userDn;
			this.ConnectionFlags = connectionFlags;
			this.LocaleInfo = localeInfo;
			this.ClientVersion = clientVersion;
			this.ClientIpAddress = clientIpAddress;
			this.ServerIpAddress = serverIpAddress;
			this.ProtocolSequence = protocolSequence;
			this.IsEncrypted = isEncrypted;
			this.OrganizationId = organizationId;
			this.RpcServerTarget = rpcServerTarget;
			this.IsAnonymous = isAnonymous;
			this.DispatchOptions = (dispatchOptions ?? new DispatchOptions());
			activity.RegisterWatsonReportDataProvider(WatsonReportActionType.Connection, this);
			ExTraceGlobals.ConnectRpcTracer.TraceDebug(0, Activity.TraceId, "Connecting {0} acting as '{1}' from {2}/{3}", new object[]
			{
				this.securityIndentifier,
				this.UserDn,
				this.ProtocolSequence,
				this.ClientIpAddress
			});
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003580 File Offset: 0x00001780
		string WatsonHelper.IProvideWatsonReportData.GetWatsonReportString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.AppendFormat("UserDn: {0}\r\n", this.UserDn);
			stringBuilder.AppendFormat("ClientVersion: v", new object[0]);
			stringBuilder.Append(this.ClientVersion);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("ClientIpAddress: {0}\r\n", this.ClientIpAddress);
			stringBuilder.AppendFormat("Protocol: {0}\r\n", this.ProtocolSequence);
			stringBuilder.AppendFormat("UserSid: {0}\r\n", this.securityIndentifier);
			stringBuilder.Append(this.LocaleInfo);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("RpcServerTarget: {0}\r\n", this.RpcServerTarget);
			stringBuilder.AppendFormat("IsEncrypted: {0}\r\n", this.IsEncrypted);
			stringBuilder.AppendFormat("IsAnonymous: {0}\r\n", this.IsAnonymous);
			return stringBuilder.ToString();
		}

		// Token: 0x04000027 RID: 39
		private readonly SecurityIdentifier securityIndentifier;

		// Token: 0x04000028 RID: 40
		internal readonly Activity Activity;

		// Token: 0x04000029 RID: 41
		internal readonly string UserDn;

		// Token: 0x0400002A RID: 42
		internal readonly ConnectionFlags ConnectionFlags;

		// Token: 0x0400002B RID: 43
		internal readonly LocaleInfo LocaleInfo;

		// Token: 0x0400002C RID: 44
		internal readonly MapiVersion ClientVersion;

		// Token: 0x0400002D RID: 45
		internal readonly IPAddress ClientIpAddress;

		// Token: 0x0400002E RID: 46
		internal readonly IPAddress ServerIpAddress;

		// Token: 0x0400002F RID: 47
		internal readonly string ProtocolSequence;

		// Token: 0x04000030 RID: 48
		internal readonly bool IsEncrypted;

		// Token: 0x04000031 RID: 49
		internal readonly OrganizationId OrganizationId;

		// Token: 0x04000032 RID: 50
		internal readonly string RpcServerTarget;

		// Token: 0x04000033 RID: 51
		internal readonly bool IsAnonymous;

		// Token: 0x04000034 RID: 52
		internal readonly DispatchOptions DispatchOptions;
	}
}
