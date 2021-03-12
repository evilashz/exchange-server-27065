using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x02000606 RID: 1542
	public class GuestTenantDetail
	{
		// Token: 0x06001B9A RID: 7066 RVA: 0x00032124 File Offset: 0x00030324
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static GuestTenantDetail CreateGuestTenantDetail(Collection<string> domains)
		{
			GuestTenantDetail guestTenantDetail = new GuestTenantDetail();
			if (domains == null)
			{
				throw new ArgumentNullException("domains");
			}
			guestTenantDetail.domains = domains;
			return guestTenantDetail;
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x0003214D File Offset: 0x0003034D
		// (set) Token: 0x06001B9C RID: 7068 RVA: 0x00032155 File Offset: 0x00030355
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string tenantId
		{
			get
			{
				return this._tenantId;
			}
			set
			{
				this._tenantId = value;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0003215E File Offset: 0x0003035E
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x00032166 File Offset: 0x00030366
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string country
		{
			get
			{
				return this._country;
			}
			set
			{
				this._country = value;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0003216F File Offset: 0x0003036F
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x00032177 File Offset: 0x00030377
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string countryCode
		{
			get
			{
				return this._countryCode;
			}
			set
			{
				this._countryCode = value;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x00032180 File Offset: 0x00030380
		// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x00032188 File Offset: 0x00030388
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string displayName
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x00032191 File Offset: 0x00030391
		// (set) Token: 0x06001BA4 RID: 7076 RVA: 0x00032199 File Offset: 0x00030399
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> domains
		{
			get
			{
				return this._domains;
			}
			set
			{
				this._domains = value;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x000321A2 File Offset: 0x000303A2
		// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x000321AA File Offset: 0x000303AA
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? isHomeTenant
		{
			get
			{
				return this._isHomeTenant;
			}
			set
			{
				this._isHomeTenant = value;
			}
		}

		// Token: 0x04001C90 RID: 7312
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _tenantId;

		// Token: 0x04001C91 RID: 7313
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _country;

		// Token: 0x04001C92 RID: 7314
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _countryCode;

		// Token: 0x04001C93 RID: 7315
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001C94 RID: 7316
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _domains = new Collection<string>();

		// Token: 0x04001C95 RID: 7317
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _isHomeTenant;
	}
}
