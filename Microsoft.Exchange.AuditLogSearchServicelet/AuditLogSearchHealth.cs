using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x02000006 RID: 6
	public class AuditLogSearchHealth : HealthHandlerResult
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002764 File Offset: 0x00000964
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000027AC File Offset: 0x000009AC
		public Search[] Searches
		{
			get
			{
				Search[] result;
				lock (this.syncRoot)
				{
					result = this.searches.ToArray();
				}
				return result;
			}
			set
			{
				lock (this.syncRoot)
				{
					this.searches = new List<Search>(value ?? Array<Search>.Empty);
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000027FC File Offset: 0x000009FC
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002844 File Offset: 0x00000A44
		public string[] Tenants
		{
			get
			{
				string[] result;
				lock (this.syncRoot)
				{
					result = this.tenants.ToArray();
				}
				return result;
			}
			set
			{
				lock (this.syncRoot)
				{
					this.tenants = new List<string>(value ?? Array<string>.Empty);
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002894 File Offset: 0x00000A94
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000028DC File Offset: 0x00000ADC
		public string[] RetryTenants
		{
			get
			{
				string[] result;
				lock (this.syncRoot)
				{
					result = this.retryTenants.ToArray();
				}
				return result;
			}
			set
			{
				lock (this.syncRoot)
				{
					this.retryTenants = new List<string>(value ?? Array<string>.Empty);
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000292C File Offset: 0x00000B2C
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002974 File Offset: 0x00000B74
		public ExceptionDetails[] Exceptions
		{
			get
			{
				ExceptionDetails[] result;
				lock (this.syncRoot)
				{
					result = this.exceptions.ToArray();
				}
				return result;
			}
			set
			{
				lock (this.syncRoot)
				{
					this.exceptions.Clear();
					if (value != null && value.Length > 0)
					{
						for (int i = 0; i < value.Length; i++)
						{
							ExceptionDetails item = value[i];
							if (this.exceptions.Count >= 25)
							{
								break;
							}
							this.exceptions.Add(item);
						}
					}
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000029F8 File Offset: 0x00000BF8
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002A00 File Offset: 0x00000C00
		public DateTime ProcessStartTime { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002A09 File Offset: 0x00000C09
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002A11 File Offset: 0x00000C11
		public DateTime? ProcessEndTime { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002A1A File Offset: 0x00000C1A
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002A22 File Offset: 0x00000C22
		public DateTime? NextSearchTime { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002A2B File Offset: 0x00000C2B
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002A33 File Offset: 0x00000C33
		public int RetryIteration { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002A43 File Offset: 0x00000C43
		public SslPolicyInfo SslValidationInfo
		{
			get
			{
				return SslPolicyInfo.Instance;
			}
			set
			{
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002A48 File Offset: 0x00000C48
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002A72 File Offset: 0x00000C72
		public SslConfig SslConfig
		{
			get
			{
				return new SslConfig
				{
					AllowInternalUntrustedCerts = SslConfiguration.AllowInternalUntrustedCerts,
					AllowExternalUntrustedCerts = SslConfiguration.AllowExternalUntrustedCerts
				};
			}
			set
			{
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A74 File Offset: 0x00000C74
		internal void Clear()
		{
			lock (this.syncRoot)
			{
				this.searches.Clear();
				this.tenants.Clear();
				this.retryTenants.Clear();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AD0 File Offset: 0x00000CD0
		internal void ClearRetry()
		{
			lock (this.syncRoot)
			{
				this.retryTenants.Clear();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B18 File Offset: 0x00000D18
		internal void AddSearch(Search search)
		{
			if (search != null)
			{
				lock (this.syncRoot)
				{
					this.searches.Add(search);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B64 File Offset: 0x00000D64
		internal void AddTenant(ADUser tenant)
		{
			if (tenant != null)
			{
				lock (this.syncRoot)
				{
					this.tenants.Add(tenant.UserPrincipalName);
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002BB4 File Offset: 0x00000DB4
		internal void AddException(Exception e)
		{
			if (AuditLogSearchHealth.IsExceptionInterestingForDiagnostics(e))
			{
				ExceptionDetails exceptionDetails = ExceptionDetails.Create(e);
				if (exceptionDetails != null)
				{
					lock (this.syncRoot)
					{
						if (this.exceptions.Count == 25)
						{
							this.exceptions.RemoveAt(0);
						}
						this.exceptions.Add(exceptionDetails);
					}
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C28 File Offset: 0x00000E28
		internal void AddRetryTenant(ADUser tenant)
		{
			if (tenant != null)
			{
				lock (this.syncRoot)
				{
					this.retryTenants.Add(tenant.UserPrincipalName);
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C78 File Offset: 0x00000E78
		private static bool IsExceptionInterestingForDiagnostics(Exception exception)
		{
			return exception != null && !(exception is TenantAccessBlockedException) && !(exception is CannotResolveTenantNameException) && (exception.InnerException == null || !(exception.InnerException is CannotResolveTenantNameException));
		}

		// Token: 0x04000013 RID: 19
		internal const int MaxExceptions = 25;

		// Token: 0x04000014 RID: 20
		private readonly object syncRoot = new object();

		// Token: 0x04000015 RID: 21
		private List<Search> searches = new List<Search>();

		// Token: 0x04000016 RID: 22
		private List<string> tenants = new List<string>();

		// Token: 0x04000017 RID: 23
		private readonly List<ExceptionDetails> exceptions = new List<ExceptionDetails>(25);

		// Token: 0x04000018 RID: 24
		private List<string> retryTenants = new List<string>();
	}
}
