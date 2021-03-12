using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Security.X509CertAuth
{
	// Token: 0x0200009B RID: 155
	internal class X509CertUserCache : LazyLookupTimeoutCache<X509Identifier, X509CertUserCache.ResolvedX509CertUser>
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x0002A35F File Offset: 0x0002855F
		private X509CertUserCache() : base(1, X509CertUserCache.cacheSize.Value, false, X509CertUserCache.cacheTimeToLive.Value)
		{
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0002A380 File Offset: 0x00028580
		public static X509CertUserCache Singleton
		{
			get
			{
				if (X509CertUserCache.singleton == null)
				{
					lock (X509CertUserCache.lockObj)
					{
						if (X509CertUserCache.singleton == null)
						{
							X509CertUserCache.singleton = new X509CertUserCache();
						}
					}
				}
				return X509CertUserCache.singleton;
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0002A3D8 File Offset: 0x000285D8
		protected override X509CertUserCache.ResolvedX509CertUser CreateOnCacheMiss(X509Identifier key, ref bool shouldAdd)
		{
			shouldAdd = true;
			string subject = key.Subject;
			string anchorMailbox = null;
			string text = null;
			if (HttpContext.Current != null && HttpContext.Current.Request != null)
			{
				anchorMailbox = (text = HttpContext.Current.Request.Headers[WellKnownHeader.AnchorMailbox]);
			}
			if (string.IsNullOrEmpty(text))
			{
				int num = subject.IndexOf("=");
				if (num != -1)
				{
					int num2 = subject.IndexOf(",", num);
					if (num2 != -1)
					{
						text = subject.Substring(num + 1, num2 - num - 1).Trim();
					}
					else
					{
						text = subject.Substring(num + 1).Trim();
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				return X509CertUserCache.ResolvedX509CertUser.CannotFindUserAddress(subject, anchorMailbox);
			}
			if (!SmtpAddress.IsValidSmtpAddress(text))
			{
				return X509CertUserCache.ResolvedX509CertUser.InvalidUserAddress(text);
			}
			string domain = new SmtpAddress(text).Domain;
			ADSessionSettings adsessionSettings = null;
			ADTransientException ex = null;
			for (int i = 0; i < X509CertUserCache.lookupRetryMax.Value; i++)
			{
				try
				{
					adsessionSettings = ADSessionSettings.FromTenantAcceptedDomain(domain);
				}
				catch (CannotResolveTenantNameException)
				{
					return X509CertUserCache.ResolvedX509CertUser.CannotResolveTenantName(domain);
				}
				catch (ADTransientException ex2)
				{
					ex = ex2;
				}
			}
			if (adsessionSettings == null)
			{
				shouldAdd = false;
				return X509CertUserCache.ResolvedX509CertUser.ADTransientException(ex);
			}
			for (int j = 0; j < X509CertUserCache.lookupRetryMax.Value; j++)
			{
				try
				{
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, adsessionSettings, 163, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\X509CertAuth\\X509CertUserCache.cs");
					ADUser aduser = (ADUser)tenantOrRootOrgRecipientSession.FindByCertificate(key);
					ExTraceGlobals.X509CertAuthTracer.TraceDebug<ADUser, X509Identifier>(0L, "[X509CertCache::CreateOnCacheMiss] FindByCertificate returns '{0}' for X509Identifier {1}", aduser, key);
					return new X509CertUserCache.ResolvedX509CertUser
					{
						OrganizationId = aduser.OrganizationId,
						Sid = aduser.Sid,
						UserPrincipalName = aduser.UserPrincipalName,
						ImplicitUserPrincipalName = string.Format("{0}@{1}", aduser.SamAccountName, aduser.Id.GetPartitionId().ForestFQDN)
					};
				}
				catch (NonUniqueRecipientException arg)
				{
					ExTraceGlobals.X509CertAuthTracer.TraceError<NonUniqueRecipientException>(0L, "[X509CertCache::CreateOnCacheMiss] FindByCertificate throws NonUniqueRecipientException {0}", arg);
					return X509CertUserCache.ResolvedX509CertUser.NonUniqueRecipient(key);
				}
				catch (ADTransientException ex3)
				{
					ExTraceGlobals.X509CertAuthTracer.TraceDebug<int, ADTransientException>(0L, "[X509CertCache::CreateOnCacheMiss] FindByCertificate will retry for {0} times, for ADTransientException {1}", j, ex3);
					ex = ex3;
				}
			}
			ExTraceGlobals.X509CertAuthTracer.TraceWarning<X509Identifier>(0L, "[X509CertCache::CreateOnCacheMiss] FindByCertificate returns null for X509Identifier {0}", key);
			shouldAdd = false;
			return X509CertUserCache.ResolvedX509CertUser.ADTransientException(ex);
		}

		// Token: 0x04000573 RID: 1395
		private static TimeSpanAppSettingsEntry cacheTimeToLive = new TimeSpanAppSettingsEntry("X509CertCacheTimeToLive", TimeSpanUnit.Seconds, TimeSpan.FromMinutes(15.0), ExTraceGlobals.X509CertAuthTracer);

		// Token: 0x04000574 RID: 1396
		private static IntAppSettingsEntry cacheSize = new IntAppSettingsEntry("X509CertCacheMaxItems", 500, ExTraceGlobals.X509CertAuthTracer);

		// Token: 0x04000575 RID: 1397
		private static IntAppSettingsEntry lookupRetryMax = new IntAppSettingsEntry("X509CertCacheLookupRetryMax", 3, ExTraceGlobals.X509CertAuthTracer);

		// Token: 0x04000576 RID: 1398
		private static readonly object lockObj = new object();

		// Token: 0x04000577 RID: 1399
		private static X509CertUserCache singleton = null;

		// Token: 0x0200009C RID: 156
		internal class ResolvedX509CertUser
		{
			// Token: 0x1700010C RID: 268
			// (get) Token: 0x0600052E RID: 1326 RVA: 0x0002A6B2 File Offset: 0x000288B2
			// (set) Token: 0x0600052F RID: 1327 RVA: 0x0002A6BA File Offset: 0x000288BA
			public OrganizationId OrganizationId { get; set; }

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000530 RID: 1328 RVA: 0x0002A6C3 File Offset: 0x000288C3
			// (set) Token: 0x06000531 RID: 1329 RVA: 0x0002A6CB File Offset: 0x000288CB
			public string ImplicitUserPrincipalName { get; set; }

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000532 RID: 1330 RVA: 0x0002A6D4 File Offset: 0x000288D4
			// (set) Token: 0x06000533 RID: 1331 RVA: 0x0002A6DC File Offset: 0x000288DC
			public string UserPrincipalName { get; set; }

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x06000534 RID: 1332 RVA: 0x0002A6E5 File Offset: 0x000288E5
			// (set) Token: 0x06000535 RID: 1333 RVA: 0x0002A6ED File Offset: 0x000288ED
			public SecurityIdentifier Sid { get; set; }

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000536 RID: 1334 RVA: 0x0002A6F6 File Offset: 0x000288F6
			// (set) Token: 0x06000537 RID: 1335 RVA: 0x0002A6FE File Offset: 0x000288FE
			public string ErrorString { get; set; }

			// Token: 0x06000538 RID: 1336 RVA: 0x0002A708 File Offset: 0x00028908
			public static X509CertUserCache.ResolvedX509CertUser CannotFindUserAddress(string subject, string anchorMailbox)
			{
				return X509CertUserCache.ResolvedX509CertUser.CreateErrorObject("Cannot find user email address in cert subject '{0}' or anchor mailbox header '{1}'", new object[]
				{
					subject,
					anchorMailbox
				});
			}

			// Token: 0x06000539 RID: 1337 RVA: 0x0002A730 File Offset: 0x00028930
			public static X509CertUserCache.ResolvedX509CertUser InvalidUserAddress(string userAddress)
			{
				return X509CertUserCache.ResolvedX509CertUser.CreateErrorObject("'{0}' is not valid email address", new object[]
				{
					userAddress
				});
			}

			// Token: 0x0600053A RID: 1338 RVA: 0x0002A754 File Offset: 0x00028954
			public static X509CertUserCache.ResolvedX509CertUser CannotResolveTenantName(string domain)
			{
				return X509CertUserCache.ResolvedX509CertUser.CreateErrorObject("Cannot resolve tenant name '{0}'", new object[]
				{
					domain
				});
			}

			// Token: 0x0600053B RID: 1339 RVA: 0x0002A778 File Offset: 0x00028978
			public static X509CertUserCache.ResolvedX509CertUser NonUniqueRecipient(X509Identifier identifier)
			{
				return X509CertUserCache.ResolvedX509CertUser.CreateErrorObject("FindByCertificate hit non-unique recipient for subject '{0}', issuer '{1}'", new object[]
				{
					identifier.Subject,
					identifier.Issuer
				});
			}

			// Token: 0x0600053C RID: 1340 RVA: 0x0002A7AC File Offset: 0x000289AC
			public static X509CertUserCache.ResolvedX509CertUser ADTransientException(Exception ex)
			{
				return X509CertUserCache.ResolvedX509CertUser.CreateErrorObject("Hitting transient exception with '{0}'", new object[]
				{
					ex
				});
			}

			// Token: 0x0600053D RID: 1341 RVA: 0x0002A7D0 File Offset: 0x000289D0
			private static X509CertUserCache.ResolvedX509CertUser CreateErrorObject(string format, params object[] args)
			{
				return new X509CertUserCache.ResolvedX509CertUser
				{
					ErrorString = string.Format(format, args)
				};
			}
		}
	}
}
