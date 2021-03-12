using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B5A RID: 2906
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExternalRMSServerInfo
	{
		// Token: 0x06006946 RID: 26950 RVA: 0x001C4343 File Offset: 0x001C2543
		public ExternalRMSServerInfo(Uri keyUri) : this(keyUri, null, null, null, null, DateTime.MaxValue)
		{
		}

		// Token: 0x06006947 RID: 26951 RVA: 0x001C4358 File Offset: 0x001C2558
		public ExternalRMSServerInfo(Uri keyUri, Uri certificationWsUri, Uri certificationWsTargetUri, Uri serverLicensingWsUri, Uri serverLicensingWsTargetUri, DateTime expiryTime)
		{
			if (null == keyUri)
			{
				throw new ArgumentNullException("keyUri");
			}
			this.KeyUri = keyUri;
			this.CertificationWSPipeline = certificationWsUri;
			this.CertificationWSTargetUri = certificationWsTargetUri;
			this.ServerLicensingWSPipeline = serverLicensingWsUri;
			this.ServerLicensingWSTargetUri = serverLicensingWsTargetUri;
			this.ExpiryTime = expiryTime;
		}

		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x06006948 RID: 26952 RVA: 0x001C43AC File Offset: 0x001C25AC
		// (set) Token: 0x06006949 RID: 26953 RVA: 0x001C43B4 File Offset: 0x001C25B4
		public Uri KeyUri
		{
			get
			{
				return this.keyUri;
			}
			private set
			{
				this.keyUri = value;
			}
		}

		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x0600694A RID: 26954 RVA: 0x001C43BD File Offset: 0x001C25BD
		// (set) Token: 0x0600694B RID: 26955 RVA: 0x001C43C5 File Offset: 0x001C25C5
		public Uri ServerLicensingWSTargetUri
		{
			get
			{
				return this.serverLicensingWsTargetUri;
			}
			set
			{
				this.serverLicensingWsTargetUri = value;
			}
		}

		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x0600694C RID: 26956 RVA: 0x001C43CE File Offset: 0x001C25CE
		// (set) Token: 0x0600694D RID: 26957 RVA: 0x001C43D6 File Offset: 0x001C25D6
		public Uri CertificationWSTargetUri
		{
			get
			{
				return this.certificationWsTargetUri;
			}
			set
			{
				this.certificationWsTargetUri = value;
			}
		}

		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x0600694E RID: 26958 RVA: 0x001C43DF File Offset: 0x001C25DF
		// (set) Token: 0x0600694F RID: 26959 RVA: 0x001C43E7 File Offset: 0x001C25E7
		public Uri ServerLicensingWSPipeline
		{
			get
			{
				return this.serverLicensingWsUri;
			}
			set
			{
				this.serverLicensingWsUri = value;
			}
		}

		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x06006950 RID: 26960 RVA: 0x001C43F0 File Offset: 0x001C25F0
		// (set) Token: 0x06006951 RID: 26961 RVA: 0x001C43F8 File Offset: 0x001C25F8
		public Uri CertificationWSPipeline
		{
			get
			{
				return this.certificationWsUri;
			}
			set
			{
				this.certificationWsUri = value;
			}
		}

		// Token: 0x17001CCB RID: 7371
		// (get) Token: 0x06006952 RID: 26962 RVA: 0x001C4401 File Offset: 0x001C2601
		// (set) Token: 0x06006953 RID: 26963 RVA: 0x001C4409 File Offset: 0x001C2609
		public DateTime ExpiryTime
		{
			get
			{
				return this.expiryTime;
			}
			private set
			{
				this.expiryTime = value;
			}
		}

		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x06006954 RID: 26964 RVA: 0x001C4412 File Offset: 0x001C2612
		public bool IsNegativeEntry
		{
			get
			{
				return this.ExpiryTime != DateTime.MaxValue;
			}
		}

		// Token: 0x06006955 RID: 26965 RVA: 0x001C4424 File Offset: 0x001C2624
		public static bool TryParse(string[] values, out ExternalRMSServerInfo info)
		{
			info = null;
			if (values == null || values.Length != ExternalRMSServerInfo.ColumnNames.Length)
			{
				ExternalRMSServerInfo.Tracer.TraceError(0L, "External Rms Server Info failed to parse values.");
				return false;
			}
			Uri uri;
			if (!Uri.TryCreate(values[0], UriKind.Absolute, out uri))
			{
				ExternalRMSServerInfo.Tracer.TraceError<string>(0L, "External Rms Server Info failed to parse Key Uri ({0}).", values[0]);
				return false;
			}
			long num;
			if (long.TryParse(values[5], out num) && num >= DateTime.MinValue.Ticks && num <= DateTime.MaxValue.Ticks)
			{
				DateTime d = new DateTime(num);
				bool flag = d != DateTime.MaxValue;
				Uri uri2;
				if (!Uri.TryCreate(values[1], UriKind.Absolute, out uri2) && !flag)
				{
					ExternalRMSServerInfo.Tracer.TraceError<string>(0L, "External Rms Server Info failed to parse CertificationWsUrl ({0}).", values[1]);
				}
				Uri uri3;
				if (!Uri.TryCreate(values[2], UriKind.Absolute, out uri3) && !flag)
				{
					ExternalRMSServerInfo.Tracer.TraceError<string>(0L, "External Rms Server Info failed to parse CertificationWsTargetUri ({0}).", values[2]);
				}
				Uri uri4;
				if (!Uri.TryCreate(values[3], UriKind.Absolute, out uri4) && !flag)
				{
					ExternalRMSServerInfo.Tracer.TraceError<string>(0L, "External Rms Server Info failed to parse ServerLicensingWsUrl ({0}).", values[3]);
				}
				Uri uri5;
				if (!Uri.TryCreate(values[4], UriKind.Absolute, out uri5) && !flag)
				{
					ExternalRMSServerInfo.Tracer.TraceError<string>(0L, "External Rms Server Info failed to parse ServerLicensingWsTargetUri ({0}).", values[4]);
				}
				info = new ExternalRMSServerInfo(uri, uri2, uri3, uri4, uri5, d);
				return true;
			}
			ExternalRMSServerInfo.Tracer.TraceError<string>(0L, "External Rms Server Info failed to parse Expiry Time ({0}).", values[5]);
			return false;
		}

		// Token: 0x06006956 RID: 26966 RVA: 0x001C4578 File Offset: 0x001C2778
		public void MarkAsNegative()
		{
			if (this.IsNegativeEntry)
			{
				return;
			}
			this.ExpiryTime = DateTime.UtcNow.Add(RmsClientManager.AppSettings.NegativeServerInfoCacheExpirationInterval);
		}

		// Token: 0x06006957 RID: 26967 RVA: 0x001C45AC File Offset: 0x001C27AC
		public string[] ToStringArray()
		{
			return new string[]
			{
				this.KeyUri.ToString(),
				(this.CertificationWSPipeline == null) ? null : this.CertificationWSPipeline.ToString(),
				(this.CertificationWSTargetUri == null) ? null : this.CertificationWSTargetUri.ToString(),
				(this.ServerLicensingWSPipeline == null) ? null : this.ServerLicensingWSPipeline.ToString(),
				(this.ServerLicensingWSTargetUri == null) ? null : this.ServerLicensingWSTargetUri.ToString(),
				this.ExpiryTime.Ticks.ToString()
			};
		}

		// Token: 0x04003BF7 RID: 15351
		public static readonly string[] ColumnNames = new string[]
		{
			"keyUrl",
			"certificationWsUrl",
			"certificationWsTargetUri",
			"serverLicensingWsUrl",
			"serverLicensingWsTargetUri",
			"expiryTime"
		};

		// Token: 0x04003BF8 RID: 15352
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;

		// Token: 0x04003BF9 RID: 15353
		private Uri keyUri;

		// Token: 0x04003BFA RID: 15354
		private Uri serverLicensingWsUri;

		// Token: 0x04003BFB RID: 15355
		private Uri serverLicensingWsTargetUri;

		// Token: 0x04003BFC RID: 15356
		private Uri certificationWsUri;

		// Token: 0x04003BFD RID: 15357
		private Uri certificationWsTargetUri;

		// Token: 0x04003BFE RID: 15358
		private DateTime expiryTime;
	}
}
