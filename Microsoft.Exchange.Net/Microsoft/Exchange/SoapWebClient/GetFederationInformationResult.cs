using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006CA RID: 1738
	[Serializable]
	public sealed class GetFederationInformationResult : ISerializable
	{
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002071 RID: 8305 RVA: 0x00040229 File Offset: 0x0003E429
		// (set) Token: 0x06002072 RID: 8306 RVA: 0x00040231 File Offset: 0x0003E431
		public AutodiscoverResult Type { get; internal set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x0004023A File Offset: 0x0003E43A
		// (set) Token: 0x06002074 RID: 8308 RVA: 0x00040242 File Offset: 0x0003E442
		public LocalizedException Exception { get; internal set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x0004024B File Offset: 0x0003E44B
		// (set) Token: 0x06002076 RID: 8310 RVA: 0x00040253 File Offset: 0x0003E453
		public GetFederationInformationResult Alternate { get; internal set; }

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x0004025C File Offset: 0x0003E45C
		// (set) Token: 0x06002078 RID: 8312 RVA: 0x00040264 File Offset: 0x0003E464
		public Uri Url { get; internal set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x0004026D File Offset: 0x0003E46D
		// (set) Token: 0x0600207A RID: 8314 RVA: 0x00040275 File Offset: 0x0003E475
		public string ApplicationUri { get; internal set; }

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x0004027E File Offset: 0x0003E47E
		// (set) Token: 0x0600207C RID: 8316 RVA: 0x00040286 File Offset: 0x0003E486
		public string[] TokenIssuerUris { get; internal set; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x0004028F File Offset: 0x0003E48F
		// (set) Token: 0x0600207E RID: 8318 RVA: 0x00040297 File Offset: 0x0003E497
		public StringList Domains { get; internal set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x000402A0 File Offset: 0x0003E4A0
		// (set) Token: 0x06002080 RID: 8320 RVA: 0x000402A8 File Offset: 0x0003E4A8
		public Uri RedirectUrl { get; internal set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x000402B1 File Offset: 0x0003E4B1
		// (set) Token: 0x06002082 RID: 8322 RVA: 0x000402B9 File Offset: 0x0003E4B9
		public StringList SslCertificateHostnames { get; internal set; }

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x000402C2 File Offset: 0x0003E4C2
		// (set) Token: 0x06002084 RID: 8324 RVA: 0x000402CA File Offset: 0x0003E4CA
		public string Details { get; internal set; }

		// Token: 0x06002085 RID: 8325 RVA: 0x000402D4 File Offset: 0x0003E4D4
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo serializationInfo, StreamingContext context)
		{
			serializationInfo.AddValue("Type", this.Type);
			serializationInfo.AddValue("Exception", this.Exception);
			serializationInfo.AddValue("Alternate", this.Alternate);
			serializationInfo.AddValue("Url", this.Url);
			serializationInfo.AddValue("ApplicationUri", this.ApplicationUri);
			serializationInfo.AddValue("TokenIssuerUris", this.TokenIssuerUris);
			serializationInfo.AddValue("Domains", this.Domains);
			serializationInfo.AddValue("RedirectUrl", this.RedirectUrl);
			serializationInfo.AddValue("SslCertificateHostnames", this.SslCertificateHostnames);
			serializationInfo.AddValue("Details", this.Details);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00040390 File Offset: 0x0003E590
		public GetFederationInformationResult(SerializationInfo serializationInfo, StreamingContext context)
		{
			this.Type = GetFederationInformationResult.GetSerializedValue<AutodiscoverResult>(serializationInfo, "Type");
			this.Exception = GetFederationInformationResult.GetSerializedValue<LocalizedException>(serializationInfo, "Exception");
			this.Alternate = GetFederationInformationResult.GetSerializedValue<GetFederationInformationResult>(serializationInfo, "Alternate");
			this.Url = GetFederationInformationResult.GetSerializedValue<Uri>(serializationInfo, "Url");
			this.ApplicationUri = GetFederationInformationResult.GetSerializedValue<string>(serializationInfo, "ApplicationUri");
			this.TokenIssuerUris = GetFederationInformationResult.GetSerializedValue<string[]>(serializationInfo, "TokenIssuerUris");
			this.Domains = GetFederationInformationResult.GetSerializedValue<StringList>(serializationInfo, "Domains");
			this.RedirectUrl = GetFederationInformationResult.GetSerializedValue<Uri>(serializationInfo, "RedirectUrl");
			this.SslCertificateHostnames = GetFederationInformationResult.GetSerializedValue<StringList>(serializationInfo, "SslCertificateHostnames");
			this.Details = GetFederationInformationResult.GetSerializedValue<string>(serializationInfo, "Details");
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00040450 File Offset: 0x0003E650
		private static T GetSerializedValue<T>(SerializationInfo serializationInfo, string name)
		{
			object value = serializationInfo.GetValue(name, typeof(T));
			if (value == null)
			{
				return default(T);
			}
			return (T)((object)value);
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00040482 File Offset: 0x0003E682
		internal GetFederationInformationResult()
		{
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0004048C File Offset: 0x0003E68C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(400);
			stringBuilder.Append("Type=" + this.Type.ToString() + ";");
			if (this.Url != null)
			{
				stringBuilder.Append("Url=" + this.Url + ";");
			}
			if (this.ApplicationUri != null)
			{
				stringBuilder.Append("ApplicationUri=" + this.ApplicationUri + ";");
			}
			if (this.TokenIssuerUris != null)
			{
				stringBuilder.Append("TokenIssuerUris=" + string.Join(",", this.TokenIssuerUris) + ";");
			}
			if (this.Domains != null)
			{
				stringBuilder.Append("Domains=" + this.Domains.ToString() + ";");
			}
			if (this.Exception != null)
			{
				stringBuilder.Append("Exception=" + this.Exception.Message + ";");
			}
			if (this.Alternate != null)
			{
				stringBuilder.Append("Alternate=(" + this.Alternate.ToString() + ");");
			}
			if (this.Details != null)
			{
				stringBuilder.Append("Details=(" + this.Details + ");");
			}
			return stringBuilder.ToString();
		}
	}
}
