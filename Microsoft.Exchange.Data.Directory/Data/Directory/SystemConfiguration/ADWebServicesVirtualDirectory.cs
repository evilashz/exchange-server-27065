using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003A2 RID: 930
	[Serializable]
	public sealed class ADWebServicesVirtualDirectory : ADExchangeServiceVirtualDirectory
	{
		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x000B2F36 File Offset: 0x000B1136
		// (set) Token: 0x06002AE6 RID: 10982 RVA: 0x000B2F3E File Offset: 0x000B113E
		public bool? CertificateAuthentication { get; internal set; }

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000B2F47 File Offset: 0x000B1147
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADWebServicesVirtualDirectory.schema;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x000B2F4E File Offset: 0x000B114E
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADWebServicesVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x000B2F55 File Offset: 0x000B1155
		// (set) Token: 0x06002AEA RID: 10986 RVA: 0x000B2F67 File Offset: 0x000B1167
		public Uri InternalNLBBypassUrl
		{
			get
			{
				return (Uri)this[ADWebServicesVirtualDirectorySchema.InternalNLBBypassUrl];
			}
			internal set
			{
				this[ADWebServicesVirtualDirectorySchema.InternalNLBBypassUrl] = value;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x000B2F75 File Offset: 0x000B1175
		// (set) Token: 0x06002AEC RID: 10988 RVA: 0x000B2F87 File Offset: 0x000B1187
		public GzipLevel GzipLevel
		{
			get
			{
				return (GzipLevel)this[ADWebServicesVirtualDirectorySchema.ADGzipLevel];
			}
			internal set
			{
				this[ADWebServicesVirtualDirectorySchema.ADGzipLevel] = value;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x000B2F9A File Offset: 0x000B119A
		// (set) Token: 0x06002AEE RID: 10990 RVA: 0x000B2FAC File Offset: 0x000B11AC
		public bool MRSProxyEnabled
		{
			get
			{
				return (bool)this[ADWebServicesVirtualDirectorySchema.MRSProxyEnabled];
			}
			internal set
			{
				this[ADWebServicesVirtualDirectorySchema.MRSProxyEnabled] = value;
			}
		}

		// Token: 0x040019C4 RID: 6596
		private static readonly ADWebServicesVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADWebServicesVirtualDirectorySchema>();

		// Token: 0x040019C5 RID: 6597
		public static readonly string MostDerivedClass = "msExchWebServicesVirtualDirectory";
	}
}
