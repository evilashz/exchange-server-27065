using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Constants
	{
		// Token: 0x04000068 RID: 104
		internal const string MapiHttpProtocolSequence = "MapiHttp";

		// Token: 0x04000069 RID: 105
		internal const string HttpVerbPost = "POST";

		// Token: 0x0400006A RID: 106
		internal const string HttpVerbGet = "GET";

		// Token: 0x0400006B RID: 107
		internal const string ContentTypeApplicationMapiHttp = "application/mapi-http";

		// Token: 0x0400006C RID: 108
		internal const string ContentTypeLegacyApplicationOctetStream = "application/octet-stream";

		// Token: 0x0400006D RID: 109
		internal const string ContentTypeTextHtml = "text/html";

		// Token: 0x0400006E RID: 110
		internal const string AcceptApplicationMapiHttp = "application/mapi-http";

		// Token: 0x0400006F RID: 111
		internal const string AcceptLegacyApplicationOctetStream = "application/octet-stream";

		// Token: 0x04000070 RID: 112
		internal const string AcceptApplicationWildcard = "application/*";

		// Token: 0x04000071 RID: 113
		internal const string AcceptWildcard = "*/*";

		// Token: 0x04000072 RID: 114
		internal const string RequestTypeUnknown = "Unknown";

		// Token: 0x04000073 RID: 115
		internal const string RequestTypePing = "PING";

		// Token: 0x04000074 RID: 116
		internal const string HeaderClientInfo = "X-ClientInfo";

		// Token: 0x04000075 RID: 117
		internal const string HeaderRequestType = "X-RequestType";

		// Token: 0x04000076 RID: 118
		internal const string HeaderRequestId = "X-RequestId";

		// Token: 0x04000077 RID: 119
		internal const string HeaderResponseCode = "X-ResponseCode";

		// Token: 0x04000078 RID: 120
		internal const string HeaderServiceCode = "X-ServiceCode";

		// Token: 0x04000079 RID: 121
		internal const string HeaderExpirationInfo = "X-ExpirationInfo";

		// Token: 0x0400007A RID: 122
		internal const string HeaderFailureLID = "X-FailureLID";

		// Token: 0x0400007B RID: 123
		internal const string HeaderFailureDescription = "X-FailureDescription";

		// Token: 0x0400007C RID: 124
		internal const string HeaderDelayRequest = "X-DelayRequest";

		// Token: 0x0400007D RID: 125
		internal const string HeaderDelayResponse = "X-DelayResponse";

		// Token: 0x0400007E RID: 126
		internal const string HeaderPendingPeriod = "X-PendingPeriod";

		// Token: 0x0400007F RID: 127
		internal const string HeaderSourceCafeServer = "X-SourceCafeServer";

		// Token: 0x04000080 RID: 128
		internal const string HeaderClientApplication = "X-ClientApplication";

		// Token: 0x04000081 RID: 129
		internal const string HeaderServerApplication = "X-ServerApplication";

		// Token: 0x04000082 RID: 130
		internal const string HeaderContentType = "Content-Type";

		// Token: 0x04000083 RID: 131
		internal const string HeaderStartTime = "X-StartTime";

		// Token: 0x04000084 RID: 132
		internal const string HeaderElapsedTime = "X-ElapsedTime";

		// Token: 0x04000085 RID: 133
		internal const string HeaderTunnelExpirationTime = "X-TunnelExpirationTime";

		// Token: 0x04000086 RID: 134
		internal const string CookieContextHandle = "MapiContext";

		// Token: 0x04000087 RID: 135
		internal const string CookieSequence = "MapiSequence";

		// Token: 0x04000088 RID: 136
		internal const string RequestTypeEmsmdbConnect = "Connect";

		// Token: 0x04000089 RID: 137
		internal const string RequestTypeEmsmdbDisconnect = "Disconnect";

		// Token: 0x0400008A RID: 138
		internal const string RequestTypeEmsmdbExecute = "Execute";

		// Token: 0x0400008B RID: 139
		internal const string RequestTypeEmsmdbNotificationWait = "NotificationWait";

		// Token: 0x0400008C RID: 140
		internal const string RequestTypeEmsmdbDummy = "Dummy";

		// Token: 0x0400008D RID: 141
		internal const string RequestTypeLegacyEmsmdbConnect = "EcDoConnectEx";

		// Token: 0x0400008E RID: 142
		internal const string RequestTypeLegacyEmsmdbDisconnect = "EcDoDisconnect";

		// Token: 0x0400008F RID: 143
		internal const string RequestTypeLegacyEmsmdbExecute = "EcDoRpcExt2";

		// Token: 0x04000090 RID: 144
		internal const string RequestTypeLegacyEmsmdbNotificationWait = "EcDoAsyncWaitEx";

		// Token: 0x04000091 RID: 145
		internal const string RequestTypeLegacyEmsmdbDummy = "EcDoDummy";

		// Token: 0x04000092 RID: 146
		internal const string RequestTypeNspiBind = "Bind";

		// Token: 0x04000093 RID: 147
		internal const string RequestTypeNspiUnbind = "Unbind";

		// Token: 0x04000094 RID: 148
		internal const string RequestTypeNspiUpdateStat = "UpdateStat";

		// Token: 0x04000095 RID: 149
		internal const string RequestTypeNspiQueryRows = "QueryRows";

		// Token: 0x04000096 RID: 150
		internal const string RequestTypeNspiQueryColumns = "QueryColumns";

		// Token: 0x04000097 RID: 151
		internal const string RequestTypeNspiSeekEntries = "SeekEntries";

		// Token: 0x04000098 RID: 152
		internal const string RequestTypeNspiResortRestriction = "ResortRestriction";

		// Token: 0x04000099 RID: 153
		internal const string RequestTypeNspiDNToEph = "DNToMId";

		// Token: 0x0400009A RID: 154
		internal const string RequestTypeNspiCompareMIds = "CompareMIds";

		// Token: 0x0400009B RID: 155
		internal const string RequestTypeNspiCompareDNTs = "CompareDNTs";

		// Token: 0x0400009C RID: 156
		internal const string RequestTypeNspiCompareMinIds = "CompareMinIds";

		// Token: 0x0400009D RID: 157
		internal const string RequestTypeNspiGetSpecialTable = "GetSpecialTable";

		// Token: 0x0400009E RID: 158
		internal const string RequestTypeNspiGetTemplateInfo = "GetTemplateInfo";

		// Token: 0x0400009F RID: 159
		internal const string RequestTypeNspiModLinkAtt = "ModLinkAtt";

		// Token: 0x040000A0 RID: 160
		internal const string RequestTypeNspiResolveNames = "ResolveNames";

		// Token: 0x040000A1 RID: 161
		internal const string RequestTypeNspiGetMatches = "GetMatches";

		// Token: 0x040000A2 RID: 162
		internal const string RequestTypeNspiGetPropList = "GetPropList";

		// Token: 0x040000A3 RID: 163
		internal const string RequestTypeNspiGetProps = "GetProps";

		// Token: 0x040000A4 RID: 164
		internal const string RequestTypeNspiModProps = "ModProps";

		// Token: 0x040000A5 RID: 165
		internal const string RequestTypeRfriGetMailboxUrl = "GetMailboxUrl";

		// Token: 0x040000A6 RID: 166
		internal const string RequestTypeRfriGetAddressBookUrl = "GetAddressBookUrl";

		// Token: 0x040000A7 RID: 167
		internal const string RequestTypeLegacyRfriGetNspiUrl = "GetNspiUrl";

		// Token: 0x040000A8 RID: 168
		internal const string MetaTagProcessing = "PROCESSING";

		// Token: 0x040000A9 RID: 169
		internal const string MetaTagPending = "PENDING";

		// Token: 0x040000AA RID: 170
		internal const string MetaTagDone = "DONE";

		// Token: 0x040000AB RID: 171
		internal const string ClientUserAgent = "MapiHttpClient";

		// Token: 0x040000AC RID: 172
		internal const string ServerApplication = "Exchange";

		// Token: 0x040000AD RID: 173
		internal const string ClientApplication = "MapiHttpClient";

		// Token: 0x040000AE RID: 174
		internal const string ProtocolSequencePrefix = "MapiHttp:";

		// Token: 0x040000AF RID: 175
		internal const int HttpStatusCodeRoutingError = 555;

		// Token: 0x040000B0 RID: 176
		internal const int ExtendedBufferHeaderSize = 8;

		// Token: 0x040000B1 RID: 177
		internal const int AuxInSize = 4104;

		// Token: 0x040000B2 RID: 178
		internal const int AuxOutSize = 4104;

		// Token: 0x040000B3 RID: 179
		internal const int MaxRequestSize = 268288;

		// Token: 0x040000B4 RID: 180
		internal const int MaxResponseSize = 1054720;

		// Token: 0x040000B5 RID: 181
		internal const int ReadResponseFragmentSize = 32768;

		// Token: 0x040000B6 RID: 182
		internal const int CompletedAsyncOperationTrackingCount = 16;

		// Token: 0x040000B7 RID: 183
		internal const int FailedAsyncOperationTrackingCount = 16;

		// Token: 0x040000B8 RID: 184
		internal const int ServicePointConnectionLimit = 65000;

		// Token: 0x040000B9 RID: 185
		internal const string TrustEntireForwardedForConfigurationKey = "TrustEntireForwardedFor";

		// Token: 0x040000BA RID: 186
		internal const string UseBufferedReadStreamConfigurationKey = "UseBufferedReadStream";

		// Token: 0x040000BB RID: 187
		internal const string ClientTunnelExpirationTimeConfigurationKey = "ClientTunnelExpirationTime";

		// Token: 0x040000BC RID: 188
		internal static readonly TimeSpan ClientTunnelExpirationTimeDefault = TimeSpan.FromMinutes(30.0);

		// Token: 0x040000BD RID: 189
		internal static readonly TimeSpan ClientTunnelExpirationTimeMin = TimeSpan.Zero;

		// Token: 0x040000BE RID: 190
		internal static readonly TimeSpan ClientTunnelExpirationTimeMax = TimeSpan.FromDays(1.0);

		// Token: 0x040000BF RID: 191
		internal static readonly TimeSpan MaxDelayRequest = TimeSpan.FromMinutes(5.0);

		// Token: 0x040000C0 RID: 192
		internal static readonly TimeSpan MaxDelayResponse = TimeSpan.FromMinutes(5.0);

		// Token: 0x040000C1 RID: 193
		internal static readonly TimeSpan DefaultPendingPeriod = TimeSpan.FromSeconds(15.0);

		// Token: 0x040000C2 RID: 194
		internal static readonly TimeSpan HttpRequestTimeout = TimeSpan.FromSeconds(6.0 * Constants.DefaultPendingPeriod.TotalSeconds);

		// Token: 0x040000C3 RID: 195
		internal static readonly TimeSpan MinPendingPeriod = TimeSpan.FromSeconds(5.0);

		// Token: 0x040000C4 RID: 196
		internal static readonly TimeSpan SessionContextIdleTimeout = TimeSpan.FromMinutes(15.0);

		// Token: 0x040000C5 RID: 197
		internal static readonly TimeSpan SessionContextRefreshInterval = TimeSpan.FromMilliseconds(Constants.SessionContextIdleTimeout.TotalMilliseconds / 2.0);

		// Token: 0x040000C6 RID: 198
		internal static readonly TimeSpan UserContextIdleTimeout = TimeSpan.FromMinutes(15.0);

		// Token: 0x040000C7 RID: 199
		internal static readonly TimeSpan MinExpirationInfo = TimeSpan.FromSeconds(5.0);

		// Token: 0x040000C8 RID: 200
		internal static readonly TimeSpan MaxExpirationInfo = TimeSpan.FromHours(2.0);

		// Token: 0x040000C9 RID: 201
		internal static readonly byte[] ProcessingMarker = Encoding.UTF8.GetBytes(string.Format("{0}\r\n", "PROCESSING"));

		// Token: 0x040000CA RID: 202
		internal static readonly byte[] PendingMarker = Encoding.UTF8.GetBytes(string.Format("{0}\r\n", "PENDING"));

		// Token: 0x040000CB RID: 203
		internal static readonly byte[] DoneMarker = Encoding.UTF8.GetBytes(string.Format("{0}\r\n\r\n", "DONE"));

		// Token: 0x040000CC RID: 204
		internal static readonly byte[] DoneMarkerNoEmptyLine = Encoding.UTF8.GetBytes(string.Format("{0}\r\n", "DONE"));

		// Token: 0x040000CD RID: 205
		internal static readonly int DefaultHttpPort = new Uri("http://www.contoso.com/").Port;

		// Token: 0x040000CE RID: 206
		internal static readonly int DefaultHttpsPort = new Uri("https://www.contoso.com/").Port;

		// Token: 0x040000CF RID: 207
		internal static readonly int BackEndHttpsPort = 444;
	}
}
