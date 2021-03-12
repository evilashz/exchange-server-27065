using System;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200001C RID: 28
	internal interface IAirSyncRequest
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001F6 RID: 502
		string LogonUserName { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001F7 RID: 503
		string Url { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001F8 RID: 504
		string PathAndQuery { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001F9 RID: 505
		Stream InputStream { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001FA RID: 506
		bool IsEmpty { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001FB RID: 507
		int ContentLength { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001FC RID: 508
		string ContentType { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001FD RID: 509
		Encoding ContentEncoding { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001FE RID: 510
		bool IsSecureConnection { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001FF RID: 511
		string UserHostName { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000200 RID: 512
		string HostHeaderInfo { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000201 RID: 513
		WindowsIdentity LogonUserIdentity { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000202 RID: 514
		uint? PolicyKey { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000203 RID: 515
		string DescriptionForEventLog { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000204 RID: 516
		CommandType CommandType { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000205 RID: 517
		int Version { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000206 RID: 518
		string VersionString { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000207 RID: 519
		bool HasOutlookExtensions { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000208 RID: 520
		DeviceIdentity DeviceIdentity { get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000209 RID: 521
		string User { get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600020A RID: 522
		string UserAgent { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600020B RID: 523
		// (set) Token: 0x0600020C RID: 524
		XmlDocument XmlDocument { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600020D RID: 525
		XmlElement CommandXml { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600020E RID: 526
		CultureInfo Culture { get; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600020F RID: 527
		bool WasDCProxied { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000210 RID: 528
		string DCProxyHeader { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000211 RID: 529
		bool WasProxied { get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000212 RID: 530
		bool WasFromCafe { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000213 RID: 531
		bool WasBasicAuthProxied { get; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000214 RID: 532
		string ProxyHeader { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000215 RID: 533
		string VDirSettingsHeader { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000216 RID: 534
		bool AcceptMultiPartResponse { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000217 RID: 535
		string XMSWLHeader { get; }

		// Token: 0x06000218 RID: 536
		HttpRequest GetRawHttpRequest();

		// Token: 0x06000219 RID: 537
		XmlDocument LoadRequestDocument();

		// Token: 0x0600021A RID: 538
		string GetLegacyUrlParameter(string name);

		// Token: 0x0600021B RID: 539
		string GetHeadersAsString();

		// Token: 0x0600021C RID: 540
		void ParseAndValidateHeaders();

		// Token: 0x0600021D RID: 541
		void TraceHeaders();

		// Token: 0x0600021E RID: 542
		void PrepareToHang();

		// Token: 0x0600021F RID: 543
		bool SupportsExtension(OutlookExtension extension);
	}
}
