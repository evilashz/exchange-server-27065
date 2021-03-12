using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContextPropertySchema
	{
		// Token: 0x06000166 RID: 358 RVA: 0x000056EC File Offset: 0x000038EC
		static ContextPropertySchema()
		{
			ContextPropertySchema.AllProperties = (from property in (from field in typeof(ContextPropertySchema).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			where typeof(ContextProperty).IsAssignableFrom(field.FieldType)
			select field).Select(delegate(FieldInfo field)
			{
				ContextProperty contextProperty = (ContextProperty)field.GetValue(null);
				if (string.IsNullOrEmpty(contextProperty.Name))
				{
					contextProperty.Name = field.Name;
				}
				if (contextProperty.AllowedAccessMode != ContextProperty.AccessMode.None)
				{
					throw new InvalidOperationException("AccessMode can only be set on derived properties specific to tasks");
				}
				if (!field.IsPublic)
				{
					return null;
				}
				return contextProperty;
			})
			where property != null
			select property).ToList<ContextProperty>().AsReadOnly();
		}

		// Token: 0x0400008B RID: 139
		public static readonly ICollection<ContextProperty> AllProperties;

		// Token: 0x0400008C RID: 140
		public static readonly ContextProperty<ICredentials> Credentials = ContextProperty<ICredentials>.Declare(null);

		// Token: 0x0400008D RID: 141
		public static readonly ContextProperty<bool> IgnoreInvalidServerCertificateSubject = ContextProperty<bool>.Declare(true);

		// Token: 0x0400008E RID: 142
		public static readonly ContextProperty<TimeSpan> Timeout = ContextProperty<TimeSpan>.Declare(Constants.DefaultRpcTimeout);

		// Token: 0x0400008F RID: 143
		public static readonly ContextProperty<TimeSpan> Latency = ContextProperty<TimeSpan>.Declare();

		// Token: 0x04000090 RID: 144
		public static readonly ContextProperty<Exception> Exception = ContextProperty<System.Exception>.Declare();

		// Token: 0x04000091 RID: 145
		public static readonly ContextProperty<string> ErrorDetails = ContextProperty<string>.Declare(string.Empty);

		// Token: 0x04000092 RID: 146
		public static readonly ContextProperty<ExDateTime> TaskFinished = ContextProperty<ExDateTime>.Declare();

		// Token: 0x04000093 RID: 147
		public static readonly ContextProperty<ExDateTime> TaskStarted = ContextProperty<ExDateTime>.Declare();

		// Token: 0x04000094 RID: 148
		public static readonly ContextProperty<string> RpcServer = ContextProperty<string>.Declare();

		// Token: 0x04000095 RID: 149
		public static readonly ContextProperty<AuthenticationService> RpcAuthenticationType = ContextProperty<AuthenticationService>.Declare(AuthenticationService.Negotiate);

		// Token: 0x04000096 RID: 150
		public static readonly ContextProperty<string> WebProxyServer = ContextProperty<string>.Declare(null);

		// Token: 0x04000097 RID: 151
		public static readonly ContextProperty<string> ActualBinding = ContextProperty<string>.Declare();

		// Token: 0x04000098 RID: 152
		public static readonly ContextProperty<string> RpcProxyServer = ContextProperty<string>.Declare();

		// Token: 0x04000099 RID: 153
		public static readonly ContextProperty<RpcProxyPort> RpcProxyPort = ContextProperty<Microsoft.Exchange.Rpc.RpcProxyPort>.Declare(Microsoft.Exchange.Rpc.RpcProxyPort.Default);

		// Token: 0x0400009A RID: 154
		public static readonly ContextProperty<HttpAuthenticationScheme> RpcProxyAuthenticationType = ContextProperty<HttpAuthenticationScheme>.Declare();

		// Token: 0x0400009B RID: 155
		public static readonly ContextProperty<string> OutlookSessionCookieValue = ContextProperty<string>.Declare(() => Guid.NewGuid().ToString("B").ToUpperInvariant());

		// Token: 0x0400009C RID: 156
		public static readonly ContextProperty<OutlookEndpointSelection> RpcEndpoint = ContextProperty<OutlookEndpointSelection>.Declare(OutlookEndpointSelection.Consolidated);

		// Token: 0x0400009D RID: 157
		public static readonly ContextProperty<WebHeaderCollection> AdditionalHttpHeaders = ContextProperty<WebHeaderCollection>.Declare(new WebHeaderCollection());

		// Token: 0x0400009E RID: 158
		public static readonly ContextProperty<string> MapiHttpServer = ContextProperty<string>.Declare();

		// Token: 0x0400009F RID: 159
		public static readonly ContextProperty<string> MapiHttpPersonalizedServerName = ContextProperty<string>.Declare();

		// Token: 0x040000A0 RID: 160
		public static readonly ContextProperty<string[]> RequestedRpcProxyAuthenticationTypes = ContextProperty<string[]>.Declare();

		// Token: 0x040000A1 RID: 161
		public static readonly ContextProperty<CertificateValidationError> CertificateValidationErrors = ContextProperty<CertificateValidationError>.Declare();

		// Token: 0x040000A2 RID: 162
		public static readonly ContextProperty<string> RespondingWebProxyServer = ContextProperty<string>.Declare();

		// Token: 0x040000A3 RID: 163
		public static readonly ContextProperty<string> RespondingHttpServer = ContextProperty<string>.Declare();

		// Token: 0x040000A4 RID: 164
		public static readonly ContextProperty<string> RespondingRpcProxyServer = ContextProperty<string>.Declare();

		// Token: 0x040000A5 RID: 165
		public static readonly ContextProperty<HttpStatusCode> ResponseStatusCode = ContextProperty<HttpStatusCode>.Declare();

		// Token: 0x040000A6 RID: 166
		public static readonly ContextProperty<string> ResponseStatusCodeDescription = ContextProperty<string>.Declare();

		// Token: 0x040000A7 RID: 167
		public static readonly ContextProperty<string> RequestUrl = ContextProperty<string>.Declare();

		// Token: 0x040000A8 RID: 168
		public static readonly ContextProperty<WebHeaderCollection> ResponseHeaderCollection = ContextProperty<WebHeaderCollection>.Declare();

		// Token: 0x040000A9 RID: 169
		public static readonly ContextProperty<WebHeaderCollection> RequestHeaderCollection = ContextProperty<WebHeaderCollection>.Declare();

		// Token: 0x040000AA RID: 170
		public static readonly ContextProperty<string> DirectoryServer = ContextProperty<string>.Declare();

		// Token: 0x040000AB RID: 171
		public static readonly ContextProperty<string> RpcServerLegacyDN = ContextProperty<string>.Declare();

		// Token: 0x040000AC RID: 172
		public static readonly ContextProperty<string> HomeMdbLegacyDN = ContextProperty<string>.Declare();

		// Token: 0x040000AD RID: 173
		public static readonly ContextProperty<string> PrimarySmtpAddress = ContextProperty<string>.Declare();

		// Token: 0x040000AE RID: 174
		public static readonly ContextProperty<int[]> NspiMinimalIds = ContextProperty<int[]>.Declare();

		// Token: 0x040000AF RID: 175
		public static readonly ContextProperty<MapiVersion> RespondingRpcClientAccessServerVersion = ContextProperty<MapiVersion>.Declare();

		// Token: 0x040000B0 RID: 176
		public static readonly ContextProperty<bool> UseMonitoringContext = ContextProperty<bool>.Declare(true);

		// Token: 0x040000B1 RID: 177
		public static readonly ContextProperty<string> ActivityContext = ContextProperty<string>.Declare();

		// Token: 0x040000B2 RID: 178
		public static readonly ContextProperty<string> UserLegacyDN = ContextProperty<string>.Declare();

		// Token: 0x040000B3 RID: 179
		public static readonly ContextProperty<string> MailboxLegacyDN = ContextProperty<string>.Declare();

		// Token: 0x040000B4 RID: 180
		public static readonly ContextProperty<int> RetryCount = ContextProperty<int>.Declare();

		// Token: 0x040000B5 RID: 181
		public static readonly ContextProperty<TimeSpan> RetryInterval = ContextProperty<TimeSpan>.Declare();

		// Token: 0x040000B6 RID: 182
		public static readonly ContextProperty<TimeSpan> InitialLatency = ContextProperty<TimeSpan>.Declare();

		// Token: 0x040000B7 RID: 183
		public static readonly ContextProperty<Exception> InitialException = ContextProperty<System.Exception>.Declare();
	}
}
