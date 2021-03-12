using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000643 RID: 1603
	[Serializable]
	public class OrganizationProperties
	{
		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x06004B70 RID: 19312 RVA: 0x00116380 File Offset: 0x00114580
		public bool SkipToUAndParentalControlCheck
		{
			get
			{
				return this.skipToUAndParentalControlCheck;
			}
		}

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x06004B71 RID: 19313 RVA: 0x00116388 File Offset: 0x00114588
		// (set) Token: 0x06004B72 RID: 19314 RVA: 0x00116390 File Offset: 0x00114590
		public bool IsTenantAccessBlocked
		{
			get
			{
				return this.isTenantAccessBlocked;
			}
			internal set
			{
				this.isTenantAccessBlocked = value;
			}
		}

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x06004B73 RID: 19315 RVA: 0x00116399 File Offset: 0x00114599
		// (set) Token: 0x06004B74 RID: 19316 RVA: 0x001163A1 File Offset: 0x001145A1
		public bool HideAdminAccessWarning
		{
			get
			{
				return this.hideAdminAccessWarning;
			}
			internal set
			{
				this.hideAdminAccessWarning = value;
			}
		}

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x06004B75 RID: 19317 RVA: 0x001163AA File Offset: 0x001145AA
		// (set) Token: 0x06004B76 RID: 19318 RVA: 0x001163B2 File Offset: 0x001145B2
		public bool ShowAdminAccessWarning
		{
			get
			{
				return this.showAdminAccessWarning;
			}
			internal set
			{
				this.showAdminAccessWarning = value;
			}
		}

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x06004B77 RID: 19319 RVA: 0x001163BB File Offset: 0x001145BB
		// (set) Token: 0x06004B78 RID: 19320 RVA: 0x001163C3 File Offset: 0x001145C3
		public bool ActivityBasedAuthenticationTimeoutEnabled
		{
			get
			{
				return this.activityBasedAuthenticationTimeoutEnabled;
			}
			internal set
			{
				this.activityBasedAuthenticationTimeoutEnabled = value;
			}
		}

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x06004B79 RID: 19321 RVA: 0x001163CC File Offset: 0x001145CC
		// (set) Token: 0x06004B7A RID: 19322 RVA: 0x001163D4 File Offset: 0x001145D4
		public bool ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled
		{
			get
			{
				return this.activityBasedAuthenticationTimeoutWithSingleSignOnEnabled;
			}
			internal set
			{
				this.activityBasedAuthenticationTimeoutWithSingleSignOnEnabled = value;
			}
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x06004B7B RID: 19323 RVA: 0x001163DD File Offset: 0x001145DD
		// (set) Token: 0x06004B7C RID: 19324 RVA: 0x001163E5 File Offset: 0x001145E5
		public EnhancedTimeSpan ActivityBasedAuthenticationTimeoutInterval
		{
			get
			{
				return this.activityBasedAuthenticationTimeoutInterval;
			}
			internal set
			{
				this.activityBasedAuthenticationTimeoutInterval = value;
			}
		}

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x06004B7D RID: 19325 RVA: 0x001163EE File Offset: 0x001145EE
		public string ServicePlan
		{
			get
			{
				return this.servicePlan;
			}
		}

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x06004B7E RID: 19326 RVA: 0x001163F6 File Offset: 0x001145F6
		// (set) Token: 0x06004B7F RID: 19327 RVA: 0x001163FE File Offset: 0x001145FE
		public bool IsLicensingEnforced
		{
			get
			{
				return this.isLicensingEnforced;
			}
			internal set
			{
				this.isLicensingEnforced = value;
			}
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x00116407 File Offset: 0x00114607
		internal OrganizationProperties(bool skipToUAndParentalControlCheck, string servicePlan)
		{
			this.skipToUAndParentalControlCheck = skipToUAndParentalControlCheck;
			this.servicePlan = servicePlan;
			this.propertyBag = new Dictionary<Type, object>();
			this.readerWriterLock = new ReaderWriterLock();
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x00116434 File Offset: 0x00114634
		public void SetValue<T>(T value)
		{
			try
			{
				this.readerWriterLock.AcquireWriterLock(-1);
				this.propertyBag[typeof(T)] = value;
			}
			finally
			{
				try
				{
					this.readerWriterLock.ReleaseWriterLock();
				}
				catch (ApplicationException)
				{
				}
			}
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x00116498 File Offset: 0x00114698
		public bool TryGetValue<T>(out T value)
		{
			bool result;
			try
			{
				this.readerWriterLock.AcquireReaderLock(-1);
				object obj;
				bool flag = this.propertyBag.TryGetValue(typeof(T), out obj);
				value = (flag ? ((T)((object)obj)) : default(T));
				result = flag;
			}
			finally
			{
				try
				{
					this.readerWriterLock.ReleaseReaderLock();
				}
				catch (ApplicationException)
				{
				}
			}
			return result;
		}

		// Token: 0x040033D4 RID: 13268
		private bool skipToUAndParentalControlCheck;

		// Token: 0x040033D5 RID: 13269
		private bool showAdminAccessWarning;

		// Token: 0x040033D6 RID: 13270
		private bool hideAdminAccessWarning;

		// Token: 0x040033D7 RID: 13271
		private bool activityBasedAuthenticationTimeoutEnabled;

		// Token: 0x040033D8 RID: 13272
		private bool activityBasedAuthenticationTimeoutWithSingleSignOnEnabled;

		// Token: 0x040033D9 RID: 13273
		private EnhancedTimeSpan activityBasedAuthenticationTimeoutInterval;

		// Token: 0x040033DA RID: 13274
		private string servicePlan;

		// Token: 0x040033DB RID: 13275
		private bool isLicensingEnforced;

		// Token: 0x040033DC RID: 13276
		private bool isTenantAccessBlocked;

		// Token: 0x040033DD RID: 13277
		private Dictionary<Type, object> propertyBag;

		// Token: 0x040033DE RID: 13278
		[NonSerialized]
		private ReaderWriterLock readerWriterLock;
	}
}
