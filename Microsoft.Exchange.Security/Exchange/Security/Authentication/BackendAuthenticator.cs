using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication.FederatedAuthService;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200002A RID: 42
	internal abstract class BackendAuthenticator
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000104 RID: 260
		protected abstract string[] RequiredFields { get; }

		// Token: 0x06000105 RID: 261 RVA: 0x0000945B File Offset: 0x0000765B
		public static void GetAuthIdentifier(CommonAccessToken token, ref BackendAuthenticator authenticator, out string authIdentifier)
		{
			authIdentifier = null;
			if (authenticator == null)
			{
				authenticator = BackendAuthenticator.GetAuthenticator(token);
			}
			authenticator.InternalGetAuthIdentifier(token, out authIdentifier);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00009478 File Offset: 0x00007678
		public static void Rehydrate(CommonAccessToken token, ref BackendAuthenticator authenticator, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
		{
			IAccountValidationContext accountValidationContext = null;
			BackendAuthenticator.Rehydrate(token, ref authenticator, wantAuthIdentifier, out authIdentifier, out principal, ref accountValidationContext);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00009494 File Offset: 0x00007694
		public static void Rehydrate(CommonAccessToken token, ref BackendAuthenticator authenticator, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
		{
			authIdentifier = null;
			principal = null;
			if (authenticator == null)
			{
				authenticator = BackendAuthenticator.GetAuthenticator(token);
			}
			if (authenticator is BackendAuthenticator.LiveIdBasicAuthenticator)
			{
				authenticator.InternalRehydrate(token, wantAuthIdentifier, out authIdentifier, out principal, ref accountValidationContext);
				return;
			}
			authenticator.InternalRehydrate(token, wantAuthIdentifier, out authIdentifier, out principal);
		}

		// Token: 0x06000108 RID: 264
		protected abstract void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier);

		// Token: 0x06000109 RID: 265
		protected abstract void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal);

		// Token: 0x0600010A RID: 266
		protected abstract void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext);

		// Token: 0x0600010B RID: 267 RVA: 0x000094CD File Offset: 0x000076CD
		protected virtual BackendAuthenticator InternalGetAuthenticator(CommonAccessToken token)
		{
			return this;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000094D0 File Offset: 0x000076D0
		protected virtual void ValidateToken(CommonAccessToken token)
		{
			if (this.RequiredFields != null)
			{
				foreach (string key in this.RequiredFields)
				{
					if (!token.ExtensionData.ContainsKey(key))
					{
						throw new BackendRehydrationException(SecurityStrings.MissingExtensionDataKey(key));
					}
				}
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00009518 File Offset: 0x00007718
		protected static string GetNonEmptyValue(CommonAccessToken token, string key)
		{
			string text;
			if (!token.ExtensionData.TryGetValue(key, out text) || string.IsNullOrEmpty(text))
			{
				throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey(key));
			}
			return text;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000954C File Offset: 0x0000774C
		protected static OrganizationId ExtractOrganizationId(CommonAccessToken token)
		{
			OrganizationId result;
			try
			{
				string nonEmptyValue = BackendAuthenticator.GetNonEmptyValue(token, "OrganizationIdBase64");
				result = CommonAccessTokenAccessor.DeserializeOrganizationId(nonEmptyValue);
			}
			catch (ArgumentException innerException)
			{
				throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("OrganizationIdBase64"), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("OrganizationIdBase64"), innerException2);
			}
			return result;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000095B0 File Offset: 0x000077B0
		protected static IEnumerable<string> ExtractGroupMembershipSids(CommonAccessToken token)
		{
			IEnumerable<string> result = new List<string>();
			try
			{
				string nonEmptyValue = BackendAuthenticator.GetNonEmptyValue(token, "GroupMembershipSids");
				result = CommonAccessTokenAccessor.DeserializeGroupMembershipSids(nonEmptyValue);
			}
			catch (BackendRehydrationException)
			{
				if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaServer.ShouldSkipAdfsGroupReadOnFrontend.Enabled)
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00009610 File Offset: 0x00007810
		protected static bool ExtractIsPublicSession(CommonAccessToken token)
		{
			bool result;
			try
			{
				string nonEmptyValue = BackendAuthenticator.GetNonEmptyValue(token, "IsPublicSession");
				result = bool.Parse(nonEmptyValue);
			}
			catch (ArgumentException innerException)
			{
				throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("OrganizationIdBase64"), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("OrganizationIdBase64"), innerException2);
			}
			return result;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00009674 File Offset: 0x00007874
		protected static bool TryGetAuthIdentifierFromUserSid(CommonAccessToken token, out string authIdentifier)
		{
			authIdentifier = null;
			string text;
			if (token.ExtensionData.TryGetValue("UserSid", out text) && !string.IsNullOrEmpty(text))
			{
				authIdentifier = text.ToUpper();
				return true;
			}
			return false;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000096AB File Offset: 0x000078AB
		protected static bool TryGetAuthIdentifierFromUserSid(SecurityIdentifier securityIdentifier, out string authIdentifier)
		{
			authIdentifier = null;
			if (securityIdentifier != null)
			{
				authIdentifier = securityIdentifier.Value;
				return true;
			}
			return false;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000096C4 File Offset: 0x000078C4
		protected static bool TryGetAuthIdentifierFromUserSid(string userSid, out string authIdentifier)
		{
			authIdentifier = null;
			if (!string.IsNullOrEmpty(userSid))
			{
				authIdentifier = userSid.ToUpper();
				return true;
			}
			return false;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000096DC File Offset: 0x000078DC
		private static BackendAuthenticator GetAuthenticator(CommonAccessToken token)
		{
			BackendAuthenticator backendAuthenticator = null;
			AccessTokenType key;
			if (Enum.TryParse<AccessTokenType>(token.TokenType, true, out key) && BackendAuthenticator.Authenticators.TryGetValue(key, out backendAuthenticator))
			{
				backendAuthenticator = backendAuthenticator.InternalGetAuthenticator(token);
				backendAuthenticator.ValidateToken(token);
				return backendAuthenticator;
			}
			throw new BackendRehydrationException(SecurityStrings.AccessTokenTypeNotSupported(token.TokenType));
		}

		// Token: 0x04000185 RID: 389
		protected const int MinAuthIdentifierCachePartitions = 1;

		// Token: 0x04000186 RID: 390
		protected const int MinAuthIdentifierCacheBuckets = 2;

		// Token: 0x04000187 RID: 391
		protected const int MinAuthIdentifierCacheLifetime = 60;

		// Token: 0x04000188 RID: 392
		protected const int MaxAuthIdentifierCachePartitions = 1024;

		// Token: 0x04000189 RID: 393
		protected const int MaxAuthIdentifierCacheBuckets = 100;

		// Token: 0x0400018A RID: 394
		protected const int MaxAuthIdentifierCacheLifetime = 86400;

		// Token: 0x0400018B RID: 395
		protected const int DefaultAuthIdentifierCachePartitions = 32;

		// Token: 0x0400018C RID: 396
		protected const int DefaultAuthIdentifierCacheBuckets = 5;

		// Token: 0x0400018D RID: 397
		protected const int DefaultAuthIdentifierCacheLifetime = 900;

		// Token: 0x0400018E RID: 398
		protected const bool DefaultAuthIdentifierCacheEnabled = true;

		// Token: 0x0400018F RID: 399
		protected static string[] EmptyStringArray = new string[0];

		// Token: 0x04000190 RID: 400
		private static readonly Dictionary<AccessTokenType, BackendAuthenticator> Authenticators = new Dictionary<AccessTokenType, BackendAuthenticator>
		{
			{
				AccessTokenType.Windows,
				new BackendAuthenticator.WindowsAuthenticator()
			},
			{
				AccessTokenType.LiveId,
				new BackendAuthenticator.LiveIdAuthenticator()
			},
			{
				AccessTokenType.LiveIdBasic,
				new BackendAuthenticator.LiveIdBasicAuthenticator()
			},
			{
				AccessTokenType.LiveIdNego2,
				new BackendAuthenticator.LiveIdNego2Authenticator()
			},
			{
				AccessTokenType.OAuth,
				new BackendAuthenticator.OAuthAuthenticator()
			},
			{
				AccessTokenType.Adfs,
				new BackendAuthenticator.AdfsAuthenticator()
			},
			{
				AccessTokenType.CertificateSid,
				new BackendAuthenticator.CertificateSidAuthenticator()
			},
			{
				AccessTokenType.RemotePowerShellDelegated,
				new BackendAuthenticator.RemotePowerShellDelegatedAuthenticator()
			},
			{
				AccessTokenType.Anonymous,
				new BackendAuthenticator.AnonymousAuthenticator()
			},
			{
				AccessTokenType.CompositeIdentity,
				new CompositeIdentityAuthenticator()
			}
		};

		// Token: 0x04000191 RID: 401
		private static BoolAppSettingsEntry RehydrateSidOAuthIdentity = new BoolAppSettingsEntry("OAuthAuthenticator.RehydrateSidOAuthIdentity", false, ExTraceGlobals.BackendRehydrationTracer);

		// Token: 0x0200002B RID: 43
		private sealed class AdfsAuthenticator : BackendAuthenticator
		{
			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000117 RID: 279 RVA: 0x000097E6 File Offset: 0x000079E6
			protected override string[] RequiredFields
			{
				get
				{
					return BackendAuthenticator.AdfsAuthenticator.requiredFields;
				}
			}

			// Token: 0x06000118 RID: 280 RVA: 0x000097ED File Offset: 0x000079ED
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				if (!BackendAuthenticator.TryGetAuthIdentifierFromUserSid(token, out authIdentifier))
				{
					authIdentifier = null;
				}
			}

			// Token: 0x06000119 RID: 281 RVA: 0x000097FB File Offset: 0x000079FB
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600011A RID: 282 RVA: 0x00009804 File Offset: 0x00007A04
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				authIdentifier = null;
				principal = null;
				string nonEmptyValue = BackendAuthenticator.GetNonEmptyValue(token, "UserPrincipalName");
				string nonEmptyValue2 = BackendAuthenticator.GetNonEmptyValue(token, "UserSid");
				OrganizationId organizationId = BackendAuthenticator.ExtractOrganizationId(token);
				IEnumerable<string> groupSidIds = BackendAuthenticator.ExtractGroupMembershipSids(token);
				bool isPublicSession = BackendAuthenticator.ExtractIsPublicSession(token);
				AdfsIdentity identity = new AdfsIdentity(nonEmptyValue, nonEmptyValue2, organizationId, organizationId.PartitionId.ToString(), groupSidIds, isPublicSession);
				if (wantAuthIdentifier && !BackendAuthenticator.TryGetAuthIdentifierFromUserSid(token, out authIdentifier))
				{
					authIdentifier = null;
				}
				principal = new GenericPrincipal(identity, null);
			}

			// Token: 0x04000192 RID: 402
			private static string[] requiredFields = new string[]
			{
				"UserSid",
				"UserPrincipalName",
				"OrganizationIdBase64",
				"GroupMembershipSids"
			};
		}

		// Token: 0x0200002C RID: 44
		private sealed class AnonymousAuthenticator : BackendAuthenticator
		{
			// Token: 0x1700002E RID: 46
			// (get) Token: 0x0600011D RID: 285 RVA: 0x000098BA File Offset: 0x00007ABA
			protected override string[] RequiredFields
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600011E RID: 286 RVA: 0x000098BD File Offset: 0x00007ABD
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				authIdentifier = null;
			}

			// Token: 0x0600011F RID: 287 RVA: 0x000098C2 File Offset: 0x00007AC2
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000120 RID: 288 RVA: 0x000098C9 File Offset: 0x00007AC9
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				authIdentifier = null;
				principal = null;
			}
		}

		// Token: 0x0200002D RID: 45
		private sealed class CertificateSidAuthenticator : BackendAuthenticator
		{
			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000122 RID: 290 RVA: 0x000098DA File Offset: 0x00007ADA
			protected override string[] RequiredFields
			{
				get
				{
					return BackendAuthenticator.CertificateSidAuthenticator.requiredFields;
				}
			}

			// Token: 0x06000123 RID: 291 RVA: 0x000098E1 File Offset: 0x00007AE1
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				authIdentifier = null;
			}

			// Token: 0x06000124 RID: 292 RVA: 0x000098E6 File Offset: 0x00007AE6
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000125 RID: 293 RVA: 0x000098F0 File Offset: 0x00007AF0
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				authIdentifier = null;
				principal = null;
				string text = token.ExtensionData["UserSid"];
				if (token.ExtensionData.ContainsKey("CertificateSubject"))
				{
					string text2;
					string text3;
					if (BackendAuthenticator.CertificateSidAuthenticator.TryToLookUpUserSidByCertificateSubject(token.ExtensionData["CertificateSubject"], out text2, out text3))
					{
						ExTraceGlobals.BackendRehydrationTracer.TraceDebug<string, string>(0L, "[CertificateSidAuthenticator.InternalRehydrate] Replace user sid from {0} to {1}.", text, text2);
						text = text2;
						token.ExtensionData["UserSid"] = text;
					}
					token.ExtensionData.Remove("CertificateSubject");
					HttpContext.Current.Request.Headers["X-CommonAccessToken"] = token.Serialize();
					if (text3 != null)
					{
						HttpContext.Current.Items["AuthenticatedUser"] = text3;
					}
				}
				GenericIdentity identity = new GenericIdentity(text);
				principal = new GenericPrincipal(identity, BackendAuthenticator.EmptyStringArray);
			}

			// Token: 0x06000126 RID: 294 RVA: 0x000099C8 File Offset: 0x00007BC8
			private static bool TryToLookUpUserSidByCertificateSubject(string certificateSubject, out string userSidByCertSubject, out string userName)
			{
				userSidByCertSubject = null;
				userName = null;
				ExTraceGlobals.BackendRehydrationTracer.TraceDebug<string>(0L, "[CertificateSidAuthenticator::TryToLookUpUserSidByCertificateSubject] Certificate Subject = {0}.", certificateSubject);
				X509Identifier identifier;
				if (!X509Identifier.TryParse(certificateSubject, out identifier))
				{
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug(0L, "[CertificateSidAuthenticator::TryToLookUpUserSidByCertificateSubject] Invalid certificate subject.");
					return false;
				}
				ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 148, "TryToLookUpUserSidByCertificateSubject", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\BackendAuthenticator\\CertificateSidAuthenticator.cs");
				ADRawEntry[] array;
				try
				{
					array = tenantOrRootOrgRecipientSession.FindByCertificate(identifier, new PropertyDefinition[]
					{
						ADMailboxRecipientSchema.Sid,
						ADObjectSchema.Name
					});
				}
				catch (Exception arg)
				{
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug<Exception>(0L, "[CertificateSidAuthenticator::TryToLookUpUserSidByCertificateSubject] Error encountered: {0}", arg);
					return false;
				}
				if (array.Length != 1)
				{
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug<int>(0L, "[CertificateSidAuthenticator::TryToLookUpUserSidByCertificateSubject] No/Multiple user matching the certificate. users.Length = {0}", array.Length);
					return false;
				}
				ADRawEntry adrawEntry = array[0];
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)adrawEntry[ADMailboxRecipientSchema.Sid];
				if (securityIdentifier == null)
				{
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug(0L, "[CertificateSidAuthenticator::TryToLookUpUserSidByCertificateSubject] Sid doesn't exist in the user object.");
					return false;
				}
				userSidByCertSubject = securityIdentifier.ToString();
				userName = ((adrawEntry[ADObjectSchema.Name] == null) ? null : adrawEntry[ADObjectSchema.Name].ToString());
				return true;
			}

			// Token: 0x04000193 RID: 403
			private static string[] requiredFields = new string[]
			{
				"UserSid"
			};
		}

		// Token: 0x0200002E RID: 46
		private sealed class RemotePowerShellDelegatedAuthenticator : BackendAuthenticator
		{
			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000129 RID: 297 RVA: 0x00009B2E File Offset: 0x00007D2E
			protected override string[] RequiredFields
			{
				get
				{
					return BackendAuthenticator.RemotePowerShellDelegatedAuthenticator.requiredFields;
				}
			}

			// Token: 0x0600012A RID: 298 RVA: 0x00009B35 File Offset: 0x00007D35
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				authIdentifier = null;
			}

			// Token: 0x0600012B RID: 299 RVA: 0x00009B3A File Offset: 0x00007D3A
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600012C RID: 300 RVA: 0x00009B44 File Offset: 0x00007D44
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				authIdentifier = null;
				principal = null;
				GenericIdentity identity = new GenericIdentity(token.ExtensionData["DelegatedData"], DelegatedPrincipal.DelegatedAuthenticationType.ToString());
				principal = new GenericPrincipal(identity, BackendAuthenticator.EmptyStringArray);
			}

			// Token: 0x04000194 RID: 404
			private static string[] requiredFields = new string[]
			{
				"DelegatedData"
			};
		}

		// Token: 0x0200002F RID: 47
		private sealed class WindowsAuthenticator : BackendAuthenticator
		{
			// Token: 0x17000031 RID: 49
			// (get) Token: 0x0600012F RID: 303 RVA: 0x00009BB2 File Offset: 0x00007DB2
			protected override string[] RequiredFields
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06000130 RID: 304 RVA: 0x00009BB5 File Offset: 0x00007DB5
			protected override void ValidateToken(CommonAccessToken token)
			{
				if (token.WindowsAccessToken == null)
				{
					throw new BackendRehydrationException(SecurityStrings.MissingWindowsAccessToken);
				}
				base.ValidateToken(token);
			}

			// Token: 0x06000131 RID: 305 RVA: 0x00009BD1 File Offset: 0x00007DD1
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				if (!BackendAuthenticator.TryGetAuthIdentifierFromUserSid(token.WindowsAccessToken.UserSid, out authIdentifier))
				{
					authIdentifier = null;
				}
			}

			// Token: 0x06000132 RID: 306 RVA: 0x00009BE9 File Offset: 0x00007DE9
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000133 RID: 307 RVA: 0x00009BF0 File Offset: 0x00007DF0
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				authIdentifier = null;
				if (wantAuthIdentifier && !BackendAuthenticator.TryGetAuthIdentifierFromUserSid(token.WindowsAccessToken.UserSid, out authIdentifier))
				{
					authIdentifier = null;
				}
				WindowsTokenIdentity identity = new WindowsTokenIdentity(token.WindowsAccessToken);
				principal = new GenericPrincipal(identity, null);
			}
		}

		// Token: 0x02000030 RID: 48
		private sealed class LiveIdAuthenticator : BackendAuthenticator
		{
			// Token: 0x17000032 RID: 50
			// (get) Token: 0x06000135 RID: 309 RVA: 0x00009C37 File Offset: 0x00007E37
			protected override string[] RequiredFields
			{
				get
				{
					return BackendAuthenticator.LiveIdAuthenticator.requiredFields;
				}
			}

			// Token: 0x06000136 RID: 310 RVA: 0x00009C3E File Offset: 0x00007E3E
			protected override BackendAuthenticator InternalGetAuthenticator(CommonAccessToken token)
			{
				if (!token.ExtensionData.ContainsKey("OrganizationIdBase64"))
				{
					return BackendAuthenticator.LiveIdAuthenticator.legacyLiveIdAuthenticator.InternalGetAuthenticator(token);
				}
				return base.InternalGetAuthenticator(token);
			}

			// Token: 0x06000137 RID: 311 RVA: 0x00009C65 File Offset: 0x00007E65
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				if (!BackendAuthenticator.TryGetAuthIdentifierFromUserSid(token, out authIdentifier))
				{
					authIdentifier = null;
				}
			}

			// Token: 0x06000138 RID: 312 RVA: 0x00009C73 File Offset: 0x00007E73
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000139 RID: 313 RVA: 0x00009C7C File Offset: 0x00007E7C
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				authIdentifier = null;
				principal = null;
				string nonEmptyValue = BackendAuthenticator.GetNonEmptyValue(token, "UserPrincipalName");
				string nonEmptyValue2 = BackendAuthenticator.GetNonEmptyValue(token, "UserSid");
				string nonEmptyValue3 = BackendAuthenticator.GetNonEmptyValue(token, "MemberName");
				OrganizationId organizationId = BackendAuthenticator.ExtractOrganizationId(token);
				LiveIdLoginAttributes loginAttributes = this.ExtractLoginAttributes(token);
				LiveIDIdentity liveIDIdentity = new LiveIDIdentity(nonEmptyValue, nonEmptyValue2, nonEmptyValue3, organizationId.PartitionId.ToString(), loginAttributes, null);
				liveIDIdentity.UserOrganizationId = organizationId;
				liveIDIdentity.HasAcceptedAccruals = true;
				string value;
				if (token.ExtensionData.TryGetValue("LiveIdHasAcceptedAccruals", out value) && !string.IsNullOrEmpty(value))
				{
					try
					{
						liveIDIdentity.HasAcceptedAccruals = bool.Parse(value);
					}
					catch (FormatException innerException)
					{
						throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("LiveIdHasAcceptedAccruals"), innerException);
					}
				}
				if (wantAuthIdentifier && !BackendAuthenticator.TryGetAuthIdentifierFromUserSid(token, out authIdentifier))
				{
					authIdentifier = null;
				}
				principal = new GenericPrincipal(liveIDIdentity, null);
			}

			// Token: 0x0600013A RID: 314 RVA: 0x00009D58 File Offset: 0x00007F58
			private LiveIdLoginAttributes ExtractLoginAttributes(CommonAccessToken token)
			{
				uint num = 0U;
				if (token.ExtensionData.ContainsKey("LoginAttributes"))
				{
					num = Convert.ToUInt32(token.ExtensionData["LoginAttributes"]);
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug<uint>((long)this.GetHashCode(), "[LiveIdAuthenticator::ExtractLoginAttributes] Found loginAttributes in the common access token. Value = {0}", num);
				}
				else
				{
					ExTraceGlobals.BackendRehydrationTracer.TraceError((long)this.GetHashCode(), "[LiveIdAuthenticator::ExtractLoginAttributes] loginAttributes NOT Found in the common access token. Defaulting to 0");
				}
				return new LiveIdLoginAttributes(num);
			}

			// Token: 0x04000195 RID: 405
			private static BackendAuthenticator.LegacyLiveIdAuthenticator legacyLiveIdAuthenticator = new BackendAuthenticator.LegacyLiveIdAuthenticator();

			// Token: 0x04000196 RID: 406
			private static string[] requiredFields = new string[]
			{
				"UserSid",
				"UserPrincipalName",
				"MemberName",
				"OrganizationIdBase64"
			};
		}

		// Token: 0x02000031 RID: 49
		private sealed class LegacyLiveIdAuthenticator : BackendAuthenticator
		{
			// Token: 0x17000033 RID: 51
			// (get) Token: 0x0600013D RID: 317 RVA: 0x00009E10 File Offset: 0x00008010
			protected override string[] RequiredFields
			{
				get
				{
					return BackendAuthenticator.LegacyLiveIdAuthenticator.requiredFields;
				}
			}

			// Token: 0x0600013E RID: 318 RVA: 0x00009E17 File Offset: 0x00008017
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				if (!BackendAuthenticator.TryGetAuthIdentifierFromUserSid(token, out authIdentifier))
				{
					authIdentifier = null;
				}
			}

			// Token: 0x0600013F RID: 319 RVA: 0x00009E25 File Offset: 0x00008025
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000140 RID: 320 RVA: 0x00009E2C File Offset: 0x0000802C
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				authIdentifier = null;
				principal = null;
				string text = token.ExtensionData["UserPrincipalName"];
				string text2 = token.ExtensionData["UserSid"];
				string text3 = token.ExtensionData["MemberName"];
				string text4 = token.ExtensionData["OrganizationName"];
				string partitionId = null;
				if (token.ExtensionData.ContainsKey("Partition"))
				{
					partitionId = token.ExtensionData["Partition"];
				}
				if (string.IsNullOrEmpty(text))
				{
					throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("UserPrincipalName"));
				}
				if (string.IsNullOrEmpty(text2))
				{
					throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("UserSid"));
				}
				if (string.IsNullOrEmpty(text3))
				{
					throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("MemberName"));
				}
				if (string.IsNullOrEmpty(text4))
				{
					throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("OrganizationName"));
				}
				LiveIDIdentity liveIDIdentity = new LiveIDIdentity(text, text2, text3, partitionId, new LiveIdLoginAttributes(0U), null);
				try
				{
					ADSessionSettings adsessionSettings = ADSessionSettings.FromTenantCUName(text4);
					liveIDIdentity.UserOrganizationId = adsessionSettings.CurrentOrganizationId;
				}
				catch (CannotResolveTenantNameException)
				{
					throw new BackendRehydrationException(SecurityStrings.CannotResolveOrganization(text4));
				}
				liveIDIdentity.HasAcceptedAccruals = bool.Parse(token.ExtensionData["LiveIdHasAcceptedAccruals"]);
				OrganizationProperties userOrganizationProperties;
				OrganizationPropertyCache.TryGetOrganizationProperties(liveIDIdentity.UserOrganizationId, out userOrganizationProperties);
				liveIDIdentity.UserOrganizationProperties = userOrganizationProperties;
				if (wantAuthIdentifier && !BackendAuthenticator.TryGetAuthIdentifierFromUserSid(token, out authIdentifier))
				{
					authIdentifier = null;
				}
				principal = new GenericPrincipal(liveIDIdentity, null);
			}

			// Token: 0x04000197 RID: 407
			private static string[] requiredFields = new string[]
			{
				"OrganizationName",
				"UserPrincipalName",
				"UserSid",
				"LiveIdHasAcceptedAccruals"
			};
		}

		// Token: 0x02000032 RID: 50
		private sealed class LiveIdBasicAuthenticator : BackendAuthenticator
		{
			// Token: 0x17000034 RID: 52
			// (get) Token: 0x06000143 RID: 323 RVA: 0x00009FE6 File Offset: 0x000081E6
			protected override string[] RequiredFields
			{
				get
				{
					return BackendAuthenticator.LiveIdBasicAuthenticator.requiredFields;
				}
			}

			// Token: 0x06000144 RID: 324 RVA: 0x00009FED File Offset: 0x000081ED
			protected override BackendAuthenticator InternalGetAuthenticator(CommonAccessToken token)
			{
				if (token.Version >= 2)
				{
					return this;
				}
				if (!token.ExtensionData.ContainsKey("OrganizationIdBase64"))
				{
					return BackendAuthenticator.LiveIdBasicAuthenticator.legacyLiveIdBasicAuthenticator.InternalGetAuthenticator(token);
				}
				return base.InternalGetAuthenticator(token);
			}

			// Token: 0x06000145 RID: 325 RVA: 0x0000A01F File Offset: 0x0000821F
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				if (!BackendAuthenticator.TryGetAuthIdentifierFromUserSid(token, out authIdentifier) && BackendAuthenticator.LiveIdBasicAuthenticator.AuthIdentifierCacheEnabled.Value)
				{
					authIdentifier = this.authIdentifierCache.Lookup(BackendAuthenticator.LiveIdBasicAuthenticator.BuildCacheKey(token));
				}
			}

			// Token: 0x06000146 RID: 326 RVA: 0x0000A049 File Offset: 0x00008249
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000147 RID: 327 RVA: 0x0000A090 File Offset: 0x00008290
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				authIdentifier = null;
				principal = null;
				List<string> list = null;
				bool flag = false;
				string nonEmptyValue = BackendAuthenticator.GetNonEmptyValue(token, "MemberName");
				string nonEmptyValue2 = BackendAuthenticator.GetNonEmptyValue(token, "Puid");
				string text = null;
				if ((!token.ExtensionData.TryGetValue("OrganizationContext", out text) || string.IsNullOrEmpty(text)) && HttpContext.Current != null && HttpContext.Current.Request != null)
				{
					text = HttpContext.Current.Request.Headers[WellKnownHeader.OrganizationContext];
				}
				AccountValidationContextByPUID accountValidationContextByPUID = null;
				if (ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("AccountValidationEnabled"))
				{
					if (accountValidationContext == null || !(accountValidationContext is AccountValidationContextByPUID))
					{
						accountValidationContextByPUID = this.GetAccountValidationContext(nonEmptyValue2, token);
					}
					else
					{
						accountValidationContextByPUID = (AccountValidationContextByPUID)accountValidationContext;
					}
				}
				ADRawEntry adrawEntry = null;
				bool flag2 = false;
				int passwordConfidenceInDays = 5;
				ITenantRecipientSession recipientSession = null;
				if (!BackendAuthenticator.LiveIdBasicAuthenticator.SkipPasswordConfidenceCheck.Value)
				{
					string value;
					token.ExtensionData.TryGetValue("CheckPasswordConfidence", out value);
					if (!string.IsNullOrEmpty(value))
					{
						bool.TryParse(value, out flag2);
					}
					string text2 = null;
					token.ExtensionData.TryGetValue("PasswordConfidenceInDays", out text2);
					if (!string.IsNullOrEmpty(text2))
					{
						int.TryParse(text2, out passwordConfidenceInDays);
					}
				}
				if (!BackendAuthenticator.LiveIdBasicAuthenticator.RehydrateLiveIdIdentity.Value)
				{
					string text3 = null;
					if (!token.ExtensionData.TryGetValue("ImplicitUpn", out text3) || string.IsNullOrEmpty(text3))
					{
						PropertyDefinition[] properties = (!flag2) ? BackendAuthenticator.LiveIdBasicAuthenticator.propertiesToGet : BackendAuthenticator.LiveIdBasicAuthenticator.propertiesToGetOfflineOrgId;
						adrawEntry = DirectoryHelper.GetADRawEntry(nonEmptyValue2, text, nonEmptyValue, properties, out recipientSession);
						if (adrawEntry == null)
						{
							throw new BackendRehydrationException(SecurityStrings.CannotLookupUserEx(nonEmptyValue2, nonEmptyValue), new UnauthorizedAccessException());
						}
						OrganizationId organizationId = (OrganizationId)adrawEntry[ADObjectSchema.OrganizationId];
						if (accountValidationContextByPUID != null)
						{
							accountValidationContextByPUID.SetOrgId(organizationId);
						}
						string arg = (string)adrawEntry[ADMailboxRecipientSchema.SamAccountName];
						text3 = string.Format("{0}@{1}", arg, organizationId.PartitionId.ForestFQDN);
					}
					try
					{
						WindowsIdentity windowsIdentity = new WindowsIdentity(text3);
						if (wantAuthIdentifier)
						{
							if (!BackendAuthenticator.TryGetAuthIdentifierFromUserSid(windowsIdentity.User, out authIdentifier))
							{
								authIdentifier = null;
							}
							else
							{
								flag = true;
							}
						}
						principal = new WindowsPrincipal(windowsIdentity);
						goto IL_469;
					}
					catch (SecurityException innerException)
					{
						throw new BackendRehydrationException(SecurityStrings.FailedToLogon(text3), innerException);
					}
					catch (UnauthorizedAccessException innerException2)
					{
						throw new BackendRehydrationException(SecurityStrings.FailedToLogon(text3), innerException2);
					}
				}
				string userSid = null;
				string userPrincipal;
				OrganizationId organizationId2;
				if (token.Version < 2)
				{
					userPrincipal = BackendAuthenticator.GetNonEmptyValue(token, "UserPrincipalName");
					userSid = BackendAuthenticator.GetNonEmptyValue(token, "UserSid");
					organizationId2 = BackendAuthenticator.ExtractOrganizationId(token);
				}
				else
				{
					string a;
					bool flag3 = (token.ExtensionData.TryGetValue("UserType", out a) && string.Equals(a, UserType.OutlookCom.ToString())) || ConsumerIdentityHelper.IsConsumerMailbox(nonEmptyValue);
					bool flag4 = false;
					if (HttpContext.Current != null && HttpContext.Current.Request != null)
					{
						string value2 = HttpContext.Current.Request.Headers[WellKnownHeader.MissingDirectoryUserObjectHint];
						if (!string.IsNullOrEmpty(value2))
						{
							bool.TryParse(value2, out flag4);
						}
					}
					if (!flag4)
					{
						PropertyDefinition[] properties2 = (!flag2) ? BackendAuthenticator.LiveIdBasicAuthenticator.propertiesToGet : BackendAuthenticator.LiveIdBasicAuthenticator.propertiesToGetOfflineOrgId;
						adrawEntry = DirectoryHelper.GetADRawEntry(nonEmptyValue2, flag3 ? TemplateTenantConfiguration.DefaultDomain : text, nonEmptyValue, properties2, BackendAuthenticator.LiveIdBasicAuthenticator.RehydrateMSAIdentity.Value, out recipientSession);
					}
					if (adrawEntry == null)
					{
						if (!flag4)
						{
							throw new BackendRehydrationException(SecurityStrings.CannotLookupUserEx(nonEmptyValue2, nonEmptyValue), new UnauthorizedAccessException());
						}
						MSAIdentity identity = new MSAIdentity(nonEmptyValue2, nonEmptyValue);
						authIdentifier = null;
						principal = new GenericPrincipal(identity, null);
						return;
					}
					else
					{
						userPrincipal = (string)adrawEntry[ADUserSchema.UserPrincipalName];
						userSid = ((SecurityIdentifier)adrawEntry[ADMailboxRecipientSchema.Sid]).ToString();
						organizationId2 = (OrganizationId)adrawEntry[ADObjectSchema.OrganizationId];
						if (accountValidationContextByPUID != null)
						{
							accountValidationContextByPUID.SetOrgId(organizationId2);
						}
						if (BackendAuthenticator.LiveIdBasicAuthenticator.IsRemotePowerShell.Value)
						{
							BackendAuthenticator.LiveIdBasicAuthenticator.UpdateTokenForMissingProperties(token, adrawEntry);
						}
						if (BackendAuthenticator.LiveIdBasicAuthenticator.PrepopulateGroupSids.Value)
						{
							list = DirectoryHelper.GetTokenSids(adrawEntry, nonEmptyValue2, flag3 ? TemplateTenantConfiguration.DefaultDomain : text, nonEmptyValue, BackendAuthenticator.LiveIdBasicAuthenticator.RehydrateMSAIdentity.Value);
						}
					}
				}
				LiveIdLoginAttributes loginAttributes = this.ExtractLoginAttributes(token);
				LiveIDIdentity liveIDIdentity = new LiveIDIdentity(userPrincipal, userSid, nonEmptyValue, organizationId2.PartitionId.ToString(), loginAttributes, nonEmptyValue2);
				if (list != null)
				{
					liveIDIdentity.PrepopulateGroupSidIds(list);
				}
				liveIDIdentity.UserOrganizationId = organizationId2;
				liveIDIdentity.HasAcceptedAccruals = true;
				string value3;
				if (token.ExtensionData.TryGetValue("LiveIdHasAcceptedAccruals", out value3) && !string.IsNullOrEmpty(value3))
				{
					try
					{
						liveIDIdentity.HasAcceptedAccruals = bool.Parse(value3);
					}
					catch (FormatException innerException3)
					{
						throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("LiveIdHasAcceptedAccruals"), innerException3);
					}
				}
				if (wantAuthIdentifier)
				{
					if (!BackendAuthenticator.TryGetAuthIdentifierFromUserSid(userSid, out authIdentifier))
					{
						authIdentifier = null;
					}
					else
					{
						flag = true;
					}
				}
				principal = new GenericPrincipal(liveIDIdentity, null);
				IL_469:
				string value4 = null;
				token.ExtensionData.TryGetValue("SyncHRD", out value4);
				bool flag5 = false;
				bool.TryParse(value4, out flag5);
				string passwordToSync = null;
				token.ExtensionData.TryGetValue("PasswordToSync", out passwordToSync);
				if ((flag5 || !string.IsNullOrEmpty(passwordToSync)) && !flag2)
				{
					LiveIdBasicAuthentication authentication = new LiveIdBasicAuthentication();
					byte[] bytes = Encoding.Default.GetBytes(nonEmptyValue);
					byte[] passwordBytes = Encoding.Default.GetBytes(passwordToSync);
					authentication.SyncUPN = BackendAuthenticator.LiveIdBasicAuthenticator.SyncUPN.Value;
					authentication.BeginSyncADPassword(nonEmptyValue2, bytes, passwordBytes, null, delegate(IAsyncResult ar)
					{
						Array.Clear(passwordBytes, 0, passwordBytes.Length);
						passwordToSync = null;
						authentication.EndSyncADPassword(ar);
					}, null, Guid.Empty, flag5);
				}
				else if (flag2)
				{
					if (adrawEntry == null)
					{
						adrawEntry = DirectoryHelper.GetADRawEntry(nonEmptyValue2, text, nonEmptyValue, BackendAuthenticator.LiveIdBasicAuthenticator.propertiesToGetOfflineOrgId, out recipientSession);
					}
					ADObjectId adobjectId = (ADObjectId)adrawEntry[ADMailboxRecipientSchema.Database];
					if (adrawEntry[ADMailboxRecipientSchema.ExchangeGuid] == null || adobjectId == null || Guid.Empty.Equals((Guid)adrawEntry[ADMailboxRecipientSchema.ExchangeGuid]))
					{
						throw new BackendRehydrationException(SecurityStrings.LowPasswordConfidence(nonEmptyValue), new LowPasswordConfidenceException(SecurityStrings.LowPasswordConfidence(nonEmptyValue)));
					}
					bool flag6 = false;
					try
					{
						flag6 = OfflineOrgIdAuth.CheckPasswordConfidence(nonEmptyValue2, adrawEntry.Id, passwordConfidenceInDays, recipientSession);
					}
					catch (Exception e)
					{
						throw new BackendRehydrationException(SecurityStrings.LowPasswordConfidence(nonEmptyValue), new LowPasswordConfidenceException(SecurityStrings.LowPasswordConfidenceWithException(nonEmptyValue, e)));
					}
					if (!flag6)
					{
						throw new BackendRehydrationException(SecurityStrings.LowPasswordConfidence(nonEmptyValue), new LowPasswordConfidenceException(SecurityStrings.LowPasswordConfidence(nonEmptyValue)));
					}
				}
				if (BackendAuthenticator.LiveIdBasicAuthenticator.VerifyUserHasNoMailbox.Value)
				{
					if (adrawEntry == null)
					{
						adrawEntry = DirectoryHelper.GetADRawEntry(nonEmptyValue2, text, nonEmptyValue, BackendAuthenticator.LiveIdBasicAuthenticator.propertiesToGet);
					}
					if (adrawEntry != null && HttpContext.Current != null && HttpContext.Current.Response != null)
					{
						ADObjectId adobjectId2 = (ADObjectId)adrawEntry[ADMailboxRecipientSchema.Database];
						ADObjectId adobjectId3 = (ADObjectId)adrawEntry[ADUserSchema.ArchiveDatabase];
						if ((adobjectId2 == null || adobjectId2.ObjectGuid == Guid.Empty) && (adobjectId3 == null || adobjectId3.ObjectGuid == Guid.Empty))
						{
							OrganizationId organizationId3 = (OrganizationId)adrawEntry[ADObjectSchema.OrganizationId];
							if (accountValidationContextByPUID != null && accountValidationContextByPUID.OrgId == null)
							{
								accountValidationContextByPUID.SetOrgId(organizationId3);
							}
							string externalDirectoryOrganizationId = this.GetExternalDirectoryOrganizationId(organizationId3);
							if (GlsDirectorySession.GetTenantOverrideStatus(externalDirectoryOrganizationId).HasFlag(GlsOverrideFlag.GlsRecordMismatch))
							{
								string value5 = string.Format("LiveIdBasic-InternalRehydrate-UserHasNoMailbox: puid={0}, organizationContext={1}, memberName={2}", nonEmptyValue2, text, nonEmptyValue);
								HttpContext.Current.Response.Headers[WellKnownHeader.BEServerRoutingError] = value5;
								throw new BackendRehydrationException(SecurityStrings.FailedToLogon(nonEmptyValue), new UnauthorizedAccessException());
							}
						}
					}
				}
				if (accountValidationContextByPUID != null)
				{
					accountValidationContext = accountValidationContextByPUID;
				}
				if (wantAuthIdentifier && flag && !string.IsNullOrEmpty(authIdentifier) && BackendAuthenticator.LiveIdBasicAuthenticator.AuthIdentifierCacheEnabled.Value)
				{
					this.authIdentifierCache.Add(BackendAuthenticator.LiveIdBasicAuthenticator.BuildCacheKey(token), authIdentifier);
				}
			}

			// Token: 0x06000148 RID: 328 RVA: 0x0000A84C File Offset: 0x00008A4C
			private AccountValidationContextByPUID GetAccountValidationContext(string puid, CommonAccessToken token)
			{
				ExDateTime utcNow = ExDateTime.UtcNow;
				if (token.ExtensionData.ContainsKey("CreateTime"))
				{
					string text = token.ExtensionData["CreateTime"];
					if (!string.IsNullOrEmpty(text))
					{
						ExDateTime.TryParse(text, out utcNow);
					}
				}
				string appName;
				if (token.ExtensionData.ContainsKey("AppName"))
				{
					appName = token.ExtensionData["AppName"];
				}
				else
				{
					appName = string.Empty;
				}
				return new AccountValidationContextByPUID(puid, utcNow, appName);
			}

			// Token: 0x06000149 RID: 329 RVA: 0x0000A8C8 File Offset: 0x00008AC8
			private string GetExternalDirectoryOrganizationId(OrganizationId organizationId)
			{
				if (organizationId == null)
				{
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug((long)this.GetHashCode(), "GetExternalDirectoryOrganizationId - organizationId == null");
					return null;
				}
				if (organizationId.ConfigurationUnit == null)
				{
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug((long)this.GetHashCode(), "GetExternalDirectoryOrganizationId - organizationId.ConfigurationUnit == null");
					return null;
				}
				ITenantConfigurationSession tenantConfigurationSession = null;
				try
				{
					tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 724, "GetExternalDirectoryOrganizationId", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\BackendAuthenticator\\LiveIdBasicAuthenticator.cs");
				}
				catch (Exception arg)
				{
					ExTraceGlobals.BackendRehydrationTracer.TraceWarning<OrganizationId, Exception>((long)this.GetHashCode(), "GetExternalDirectoryOrganizationId - Caught Exception when trying to CreateTenantConfigurationSession for organizationId '{0}'.  Exception details: {1}.", organizationId, arg);
				}
				if (tenantConfigurationSession == null)
				{
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "GetExternalDirectoryOrganizationId - Cannot CreateTenantConfigurationSession for organizationId '{0}'.", organizationId);
					return null;
				}
				ExchangeConfigurationUnit exchangeConfigurationUnit = tenantConfigurationSession.Read<ExchangeConfigurationUnit>(organizationId.ConfigurationUnit);
				if (exchangeConfigurationUnit == null)
				{
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "GetExternalDirectoryOrganizationId - Cannot find tenantOrg for organizationId '{0}'.", organizationId);
					return null;
				}
				ExTraceGlobals.BackendRehydrationTracer.TraceDebug<string, OrganizationId>((long)this.GetHashCode(), "GetExternalDirectoryOrganizationId - Found tenantOrg ExternalDirectoryOrganizationId '{0}' for organizationId '{1}'.", exchangeConfigurationUnit.ExternalDirectoryOrganizationId, organizationId);
				return exchangeConfigurationUnit.ExternalDirectoryOrganizationId;
			}

			// Token: 0x0600014A RID: 330 RVA: 0x0000A9D0 File Offset: 0x00008BD0
			private static string BuildCacheKey(CommonAccessToken token)
			{
				string nonEmptyValue = BackendAuthenticator.GetNonEmptyValue(token, "Puid");
				return nonEmptyValue.ToUpper();
			}

			// Token: 0x0600014B RID: 331 RVA: 0x0000A9F0 File Offset: 0x00008BF0
			private static void UpdateTokenForMissingProperties(CommonAccessToken token, ADRawEntry rawEntry)
			{
				token.ExtensionData["UserSid"] = ((SecurityIdentifier)rawEntry[ADMailboxRecipientSchema.Sid]).ToString();
				token.ExtensionData["UserPrincipalName"] = (string)rawEntry[ADUserSchema.UserPrincipalName];
				OrganizationId organizationId = (OrganizationId)rawEntry[ADObjectSchema.OrganizationId];
				if (organizationId != null)
				{
					if (organizationId.PartitionId != null)
					{
						string arg = (string)rawEntry[ADMailboxRecipientSchema.SamAccountName];
						token.ExtensionData["ImplicitUpn"] = string.Format("{0}@{1}", arg, organizationId.PartitionId.ForestFQDN);
						token.ExtensionData["Partition"] = organizationId.PartitionId.ToString();
					}
					token.ExtensionData["OrganizationIdBase64"] = CommonAccessTokenAccessor.SerializeOrganizationId(organizationId);
				}
				HttpContext.Current.Request.Headers["X-CommonAccessToken"] = token.Serialize();
				HttpContext.Current.Items["Item-CommonAccessToken"] = token;
			}

			// Token: 0x0600014C RID: 332 RVA: 0x0000AB08 File Offset: 0x00008D08
			private LiveIdLoginAttributes ExtractLoginAttributes(CommonAccessToken token)
			{
				uint num = 0U;
				if (token.ExtensionData.ContainsKey("LoginAttributes"))
				{
					num = Convert.ToUInt32(token.ExtensionData["LoginAttributes"]);
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug<uint>((long)this.GetHashCode(), "[LiveIdAuthenticator::ExtractLoginAttributes] Found loginAttributes in the common access token. Value = {0}", num);
				}
				return new LiveIdLoginAttributes(num);
			}

			// Token: 0x04000198 RID: 408
			private static BoolAppSettingsEntry RehydrateLiveIdIdentity = new BoolAppSettingsEntry("LiveIdBasicAuthenticator.RehydrateLiveIdIdentity", false, ExTraceGlobals.BackendRehydrationTracer);

			// Token: 0x04000199 RID: 409
			private static BoolAppSettingsEntry SyncUPN = new BoolAppSettingsEntry("LiveIdBasicAuthenticator.SyncUPN", false, ExTraceGlobals.BackendRehydrationTracer);

			// Token: 0x0400019A RID: 410
			public static BoolAppSettingsEntry RehydrateMSAIdentity = new BoolAppSettingsEntry("LiveIdBasicAuthenticator.RehydrateMSAIdentity", false, ExTraceGlobals.BackendRehydrationTracer);

			// Token: 0x0400019B RID: 411
			public static BoolAppSettingsEntry SkipPasswordConfidenceCheck = new BoolAppSettingsEntry("LiveIdBasicAuthenticator.SkipPasswordConfidenceCheck", false, ExTraceGlobals.BackendRehydrationTracer);

			// Token: 0x0400019C RID: 412
			public static BoolAppSettingsEntry VerifyUserHasNoMailbox = new BoolAppSettingsEntry("LiveIdBasicAuthenticator.VerifyUserHasNoMailbox", false, ExTraceGlobals.BackendRehydrationTracer);

			// Token: 0x0400019D RID: 413
			public static BoolAppSettingsEntry PrepopulateGroupSids = new BoolAppSettingsEntry("LiveIdBasicAuthenticator.PrepopulateGroupSids", false, ExTraceGlobals.BackendRehydrationTracer);

			// Token: 0x0400019E RID: 414
			private static BackendAuthenticator.LegacyLiveIdBasicAuthenticator legacyLiveIdBasicAuthenticator = new BackendAuthenticator.LegacyLiveIdBasicAuthenticator();

			// Token: 0x0400019F RID: 415
			private static string[] requiredFields = new string[]
			{
				"Puid",
				"MemberName"
			};

			// Token: 0x040001A0 RID: 416
			private static BoolAppSettingsEntry AuthIdentifierCacheEnabled = new BoolAppSettingsEntry("LiveIdBasicAuthenticator.AuthIdentifierCacheEnabled", true, ExTraceGlobals.BackendRehydrationTracer);

			// Token: 0x040001A1 RID: 417
			private static IntAppSettingsEntry AuthIdentifierCachePartitions = new IntAppSettingsEntry("LiveIdBasicAuthenticator.AuthIdentifierCachePartitions", 32, ExTraceGlobals.BackendRehydrationTracer);

			// Token: 0x040001A2 RID: 418
			private static IntAppSettingsEntry AuthIdentifierCacheBuckets = new IntAppSettingsEntry("LiveIdBasicAuthenticator.AuthIdentifierCacheBuckets", 5, ExTraceGlobals.BackendRehydrationTracer);

			// Token: 0x040001A3 RID: 419
			private static IntAppSettingsEntry AuthIdentifierCacheLifetime = new IntAppSettingsEntry("LiveIdBasicAuthenticator.AuthIdentifierCacheLifetime", 900, ExTraceGlobals.BackendRehydrationTracer);

			// Token: 0x040001A4 RID: 420
			private readonly AuthIdentifierCache authIdentifierCache = new AuthIdentifierCache(Math.Min(Math.Max(BackendAuthenticator.LiveIdBasicAuthenticator.AuthIdentifierCachePartitions.Value, 1), 1024), Math.Min(Math.Max(BackendAuthenticator.LiveIdBasicAuthenticator.AuthIdentifierCacheBuckets.Value, 2), 100), TimeSpan.FromSeconds((double)Math.Min(Math.Max(BackendAuthenticator.LiveIdBasicAuthenticator.AuthIdentifierCacheLifetime.Value, 60), 86400)));

			// Token: 0x040001A5 RID: 421
			private static readonly Lazy<bool> IsRemotePowerShell = new Lazy<bool>(delegate()
			{
				if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.Url == null)
				{
					return false;
				}
				bool result;
				try
				{
					Uri url = HttpContext.Current.Request.Url;
					if (url.AbsolutePath == null)
					{
						result = false;
					}
					else
					{
						result = url.AbsolutePath.StartsWith("/powershell", StringComparison.OrdinalIgnoreCase);
					}
				}
				catch (InvalidOperationException)
				{
					result = false;
				}
				return result;
			}, true);

			// Token: 0x040001A6 RID: 422
			private static PropertyDefinition[] propertiesToGet = new PropertyDefinition[]
			{
				ADUserSchema.UserPrincipalName,
				ADObjectSchema.OrganizationId,
				ADMailboxRecipientSchema.SamAccountName,
				ADMailboxRecipientSchema.Sid,
				ADMailboxRecipientSchema.Database,
				ADUserSchema.ArchiveDatabase,
				ADUserSchema.ArchiveGuid
			};

			// Token: 0x040001A7 RID: 423
			private static PropertyDefinition[] propertiesToGetOfflineOrgId = new PropertyDefinition[]
			{
				ADUserSchema.UserPrincipalName,
				ADObjectSchema.OrganizationId,
				ADMailboxRecipientSchema.SamAccountName,
				ADMailboxRecipientSchema.Sid,
				ADMailboxRecipientSchema.ExchangeGuid,
				ADMailboxRecipientSchema.Database,
				IADMailStorageSchema.DatabaseName,
				ADUserSchema.ArchiveDatabase,
				ADUserSchema.ArchiveGuid
			};
		}

		// Token: 0x02000033 RID: 51
		private sealed class LegacyLiveIdBasicAuthenticator : BackendAuthenticator
		{
			// Token: 0x17000035 RID: 53
			// (get) Token: 0x06000150 RID: 336 RVA: 0x0000AE1C File Offset: 0x0000901C
			protected override string[] RequiredFields
			{
				get
				{
					return BackendAuthenticator.LegacyLiveIdBasicAuthenticator.requiredFields;
				}
			}

			// Token: 0x06000151 RID: 337 RVA: 0x0000AE23 File Offset: 0x00009023
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				if (!BackendAuthenticator.TryGetAuthIdentifierFromUserSid(token, out authIdentifier))
				{
					authIdentifier = null;
				}
			}

			// Token: 0x06000152 RID: 338 RVA: 0x0000AE31 File Offset: 0x00009031
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000153 RID: 339 RVA: 0x0000AE38 File Offset: 0x00009038
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				authIdentifier = null;
				principal = null;
				PartitionId partitionId;
				if (!PartitionId.TryParse(token.ExtensionData["Partition"], out partitionId))
				{
					throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("Partition"));
				}
				string sddlForm = token.ExtensionData["UserSid"];
				ADRawEntry adrawEntry = BackendAuthenticator.LegacyLiveIdBasicAuthenticator.FindAdUser(partitionId, new SecurityIdentifier(sddlForm));
				string arg = adrawEntry[ADMailboxRecipientSchema.SamAccountName].ToString();
				string text = string.Format("{0}@{1}", arg, partitionId.ForestFQDN);
				try
				{
					WindowsIdentity ntIdentity = new WindowsIdentity(text);
					if (wantAuthIdentifier && !BackendAuthenticator.TryGetAuthIdentifierFromUserSid(token, out authIdentifier))
					{
						authIdentifier = null;
					}
					principal = new WindowsPrincipal(ntIdentity);
				}
				catch (SecurityException innerException)
				{
					throw new BackendRehydrationException(SecurityStrings.FailedToLogon(text), innerException);
				}
				catch (UnauthorizedAccessException innerException2)
				{
					throw new BackendRehydrationException(SecurityStrings.FailedToLogon(text), innerException2);
				}
			}

			// Token: 0x06000154 RID: 340 RVA: 0x0000AF1C File Offset: 0x0000911C
			private static ADRawEntry FindAdUser(PartitionId partitionId, SecurityIdentifier userSid)
			{
				PropertyDefinition[] properties = new PropertyDefinition[]
				{
					ADMailboxRecipientSchema.SamAccountName
				};
				Exception ex = null;
				try
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
					ITenantRecipientSession tenantRecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 950, "FindAdUser", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\BackendAuthenticator\\LiveIdBasicAuthenticator.cs");
					return tenantRecipientSession.FindADRawEntryBySid(userSid, properties);
				}
				catch (NonUniqueRecipientException ex2)
				{
					ex = ex2;
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug<NonUniqueRecipientException>(0L, "[LiveIdBasicAuthenticator::FindAdUser] Error encountered: {0}", ex2);
				}
				catch (ADTransientException ex3)
				{
					ex = ex3;
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug<ADTransientException>(0L, "[LiveIdBasicAuthenticator::FindAdUser] Error encountered: {0}", ex3);
				}
				catch (DataValidationException ex4)
				{
					ex = ex4;
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug<DataValidationException>(0L, "[LiveIdBasicAuthenticator::FindAdUser] Error encountered: {0}", ex4);
				}
				catch (DataSourceOperationException ex5)
				{
					ex = ex5;
					ExTraceGlobals.BackendRehydrationTracer.TraceDebug<DataSourceOperationException>(0L, "[LiveIdBasicAuthenticator::FindAdUser] Error encountered: {0}", ex5);
				}
				if (ex != null)
				{
					throw new BackendRehydrationException(SecurityStrings.CannotLookupUser(partitionId.ToString(), userSid.ToString(), ex.Message));
				}
				return null;
			}

			// Token: 0x040001A9 RID: 425
			private static string[] requiredFields = new string[]
			{
				"Partition",
				"UserSid",
				"MemberName"
			};
		}

		// Token: 0x02000034 RID: 52
		private sealed class LiveIdNego2Authenticator : BackendAuthenticator
		{
			// Token: 0x17000036 RID: 54
			// (get) Token: 0x06000157 RID: 343 RVA: 0x0000B06E File Offset: 0x0000926E
			protected override string[] RequiredFields
			{
				get
				{
					return BackendAuthenticator.LiveIdNego2Authenticator.requiredFields;
				}
			}

			// Token: 0x06000158 RID: 344 RVA: 0x0000B075 File Offset: 0x00009275
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				authIdentifier = null;
			}

			// Token: 0x06000159 RID: 345 RVA: 0x0000B07A File Offset: 0x0000927A
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600015A RID: 346 RVA: 0x0000B084 File Offset: 0x00009284
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				authIdentifier = null;
				string name = token.ExtensionData["UserSid"];
				GenericIdentity identity = new GenericIdentity(name);
				principal = new GenericPrincipal(identity, BackendAuthenticator.EmptyStringArray);
			}

			// Token: 0x040001AA RID: 426
			private static string[] requiredFields = new string[]
			{
				"UserSid"
			};
		}

		// Token: 0x02000035 RID: 53
		private sealed class OAuthAuthenticator : BackendAuthenticator
		{
			// Token: 0x17000037 RID: 55
			// (get) Token: 0x0600015D RID: 349 RVA: 0x0000B0E6 File Offset: 0x000092E6
			protected override string[] RequiredFields
			{
				get
				{
					return BackendAuthenticator.OAuthAuthenticator.requiredFields;
				}
			}

			// Token: 0x0600015E RID: 350 RVA: 0x0000B0ED File Offset: 0x000092ED
			protected override BackendAuthenticator InternalGetAuthenticator(CommonAccessToken token)
			{
				if (token.Version == 2)
				{
					return this;
				}
				if (token.ExtensionData.ContainsKey("OAuthData"))
				{
					return BackendAuthenticator.OAuthAuthenticator.legacyAuthenticator.InternalGetAuthenticator(token);
				}
				return base.InternalGetAuthenticator(token);
			}

			// Token: 0x0600015F RID: 351 RVA: 0x0000B120 File Offset: 0x00009320
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				OrganizationId organizationId = BackendAuthenticator.ExtractOrganizationId(token);
				OAuthActAsUser actAsUser = BackendAuthenticator.OAuthAuthenticator.ExtractActAsUser(organizationId, token);
				authIdentifier = BackendAuthenticator.OAuthAuthenticator.ComputeAuthIdentifier(actAsUser);
			}

			// Token: 0x06000160 RID: 352 RVA: 0x0000B144 File Offset: 0x00009344
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000161 RID: 353 RVA: 0x0000B14C File Offset: 0x0000934C
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				authIdentifier = null;
				principal = null;
				if (FaultInjection.TraceTest<bool>((FaultInjection.LIDs)4085656893U))
				{
					throw new InvalidOAuthTokenException(OAuthErrors.TestOnlyExceptionDuringRehydration, null, null);
				}
				OrganizationId organizationId = BackendAuthenticator.ExtractOrganizationId(token);
				OAuthApplication application = BackendAuthenticator.OAuthAuthenticator.ExtractOAuthApplication(token);
				OAuthActAsUser actAsUser = BackendAuthenticator.OAuthAuthenticator.ExtractActAsUser(organizationId, token);
				if (wantAuthIdentifier)
				{
					authIdentifier = BackendAuthenticator.OAuthAuthenticator.ComputeAuthIdentifier(actAsUser);
				}
				OAuthIdentity oauthIdentity = OAuthIdentity.Create(organizationId, application, actAsUser);
				try
				{
					IIdentity identity = oauthIdentity.ConvertIdentityIfNeed();
					principal = new GenericPrincipal(identity, null);
				}
				catch (InvalidOAuthLinkedAccountException ex)
				{
					throw new BackendRehydrationException(new LocalizedString(ex.Message), ex);
				}
			}

			// Token: 0x06000162 RID: 354 RVA: 0x0000B1E0 File Offset: 0x000093E0
			private static OAuthApplication ExtractOAuthApplication(CommonAccessToken token)
			{
				string nonEmptyValue = BackendAuthenticator.GetNonEmptyValue(token, "AppType");
				OAuthApplicationType oauthApplicationType;
				if (!Enum.TryParse<OAuthApplicationType>(nonEmptyValue, out oauthApplicationType))
				{
					throw new BackendRehydrationException(SecurityStrings.InvalidExtensionDataKey("AppType"));
				}
				OAuthApplication oauthApplication = null;
				switch (oauthApplicationType)
				{
				case OAuthApplicationType.S2SApp:
				{
					string nonEmptyValue2 = BackendAuthenticator.GetNonEmptyValue(token, "AppDn");
					string nonEmptyValue3 = BackendAuthenticator.GetNonEmptyValue(token, "AppId");
					string realm = null;
					token.ExtensionData.TryGetValue("AppRealm", out realm);
					OAuthIdentitySerializer.PartnerApplicationCacheKey key = new OAuthIdentitySerializer.PartnerApplicationCacheKey(nonEmptyValue2, nonEmptyValue3, realm);
					PartnerApplication partnerApplication = OAuthIdentitySerializer.PartnerApplicationCache.Instance.Get(key);
					oauthApplication = new OAuthApplication(partnerApplication);
					string b;
					if (token.ExtensionData.TryGetValue("IsFromSameOrgExchange", out b))
					{
						oauthApplication.IsFromSameOrgExchange = new bool?(string.Equals(bool.TrueString, b, StringComparison.OrdinalIgnoreCase));
					}
					break;
				}
				case OAuthApplicationType.CallbackApp:
				{
					string nonEmptyValue4 = BackendAuthenticator.GetNonEmptyValue(token, "CallbackAppId");
					string scope = null;
					token.ExtensionData.TryGetValue("Scope", out scope);
					oauthApplication = new OAuthApplication(new OfficeExtensionInfo(nonEmptyValue4, scope));
					break;
				}
				case OAuthApplicationType.V1App:
				{
					string nonEmptyValue5 = BackendAuthenticator.GetNonEmptyValue(token, "V1AppId");
					string scp = null;
					string rol = null;
					token.ExtensionData.TryGetValue("Scope", out scp);
					token.ExtensionData.TryGetValue("Role", out rol);
					oauthApplication = new OAuthApplication(new V1ProfileAppInfo(nonEmptyValue5, scp, rol));
					break;
				}
				case OAuthApplicationType.V1ExchangeSelfIssuedApp:
				{
					string nonEmptyValue6 = BackendAuthenticator.GetNonEmptyValue(token, "V1AppId");
					string scp2 = null;
					string rol2 = null;
					token.ExtensionData.TryGetValue("Scope", out scp2);
					token.ExtensionData.TryGetValue("Role", out rol2);
					string nonEmptyValue7 = BackendAuthenticator.GetNonEmptyValue(token, "AppDn");
					string nonEmptyValue8 = BackendAuthenticator.GetNonEmptyValue(token, "AppId");
					string realm2 = null;
					token.ExtensionData.TryGetValue("AppRealm", out realm2);
					OAuthIdentitySerializer.PartnerApplicationCacheKey key2 = new OAuthIdentitySerializer.PartnerApplicationCacheKey(nonEmptyValue7, nonEmptyValue8, realm2);
					PartnerApplication partnerApplication2 = OAuthIdentitySerializer.PartnerApplicationCache.Instance.Get(key2);
					oauthApplication = new OAuthApplication(new V1ProfileAppInfo(nonEmptyValue6, scp2, rol2), partnerApplication2);
					break;
				}
				}
				return oauthApplication;
			}

			// Token: 0x06000163 RID: 355 RVA: 0x0000B3D8 File Offset: 0x000095D8
			public static OAuthActAsUser ExtractActAsUser(OrganizationId organizationId, CommonAccessToken token)
			{
				if (string.Equals(BackendAuthenticator.GetNonEmptyValue(token, "AppOnly"), bool.TrueString, StringComparison.OrdinalIgnoreCase))
				{
					return null;
				}
				Dictionary<string, string> rawAttributes = null;
				string value;
				if (token.ExtensionData.TryGetValue("RawUserInfo", out value))
				{
					rawAttributes = value.DeserializeFromJson<Dictionary<string, string>>();
				}
				Dictionary<string, string> verifiedAttributes = null;
				string value2;
				if (token.ExtensionData.TryGetValue("VerifiedUserInfo", out value2))
				{
					verifiedAttributes = value2.DeserializeFromJson<Dictionary<string, string>>();
				}
				return OAuthActAsUser.InternalCreateFromAttributes(organizationId, false, rawAttributes, verifiedAttributes);
			}

			// Token: 0x06000164 RID: 356 RVA: 0x0000B448 File Offset: 0x00009648
			internal static string ComputeAuthIdentifier(OAuthActAsUser actAsUser)
			{
				string result = null;
				if (actAsUser != null && actAsUser.Sid != null && !BackendAuthenticator.TryGetAuthIdentifierFromUserSid(actAsUser.GetMasterAccountSidIfAvailable(), out result))
				{
					result = null;
				}
				return result;
			}

			// Token: 0x040001AB RID: 427
			private static BackendAuthenticator.LegacyOAuthAuthenticator legacyAuthenticator = new BackendAuthenticator.LegacyOAuthAuthenticator();

			// Token: 0x040001AC RID: 428
			private static string[] requiredFields = new string[]
			{
				"OrganizationIdBase64",
				"AppType",
				"AppOnly"
			};
		}

		// Token: 0x02000036 RID: 54
		private sealed class LegacyOAuthAuthenticator : BackendAuthenticator
		{
			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000167 RID: 359 RVA: 0x0000B4C0 File Offset: 0x000096C0
			protected override string[] RequiredFields
			{
				get
				{
					return BackendAuthenticator.LegacyOAuthAuthenticator.requiredFields;
				}
			}

			// Token: 0x06000168 RID: 360 RVA: 0x0000B4C8 File Offset: 0x000096C8
			protected override void InternalGetAuthIdentifier(CommonAccessToken token, out string authIdentifier)
			{
				OAuthIdentity oauthIdentity = OAuthIdentitySerializer.ConvertFromCommonAccessToken(token);
				authIdentifier = BackendAuthenticator.OAuthAuthenticator.ComputeAuthIdentifier(oauthIdentity.ActAsUser);
			}

			// Token: 0x06000169 RID: 361 RVA: 0x0000B4E9 File Offset: 0x000096E9
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal, ref IAccountValidationContext accountValidationContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600016A RID: 362 RVA: 0x0000B4F0 File Offset: 0x000096F0
			protected override void InternalRehydrate(CommonAccessToken token, bool wantAuthIdentifier, out string authIdentifier, out IPrincipal principal)
			{
				authIdentifier = null;
				principal = null;
				try
				{
					OAuthIdentity oauthIdentity = OAuthIdentitySerializer.ConvertFromCommonAccessToken(token);
					if (wantAuthIdentifier)
					{
						authIdentifier = BackendAuthenticator.OAuthAuthenticator.ComputeAuthIdentifier(oauthIdentity.ActAsUser);
					}
					IIdentity identity = oauthIdentity;
					if (BackendAuthenticator.RehydrateSidOAuthIdentity.Value)
					{
						ExTraceGlobals.BackendRehydrationTracer.TraceDebug(0L, "[OAuthAuthenticator::InternalRehydrate] Convert OAuthIdentity to SidOAuthIdentity.");
						identity = SidOAuthIdentity.Create(oauthIdentity);
					}
					else if (oauthIdentity.OAuthApplication.V1ProfileApp != null)
					{
						if (!string.Equals(oauthIdentity.OAuthApplication.V1ProfileApp.Scope, Constants.ClaimValues.UserImpersonation, StringComparison.OrdinalIgnoreCase))
						{
							throw new BackendRehydrationException(new LocalizedString("Invalid value in scp claim-type."));
						}
						identity = new SidBasedIdentity(oauthIdentity.ActAsUser.UserPrincipalName, oauthIdentity.ActAsUser.GetMasterAccountSidIfAvailable().Value, oauthIdentity.ActAsUser.UserPrincipalName, "OAuth", oauthIdentity.OrganizationId.PartitionId.ToString())
						{
							UserOrganizationId = oauthIdentity.OrganizationId
						};
					}
					principal = new GenericPrincipal(identity, null);
				}
				catch (OAuthIdentityDeserializationException ex)
				{
					throw new BackendRehydrationException(new LocalizedString(ex.Message), ex);
				}
				catch (InvalidOAuthLinkedAccountException ex2)
				{
					throw new BackendRehydrationException(new LocalizedString(ex2.Message), ex2);
				}
			}

			// Token: 0x040001AD RID: 429
			private static string[] requiredFields = new string[]
			{
				"OAuthData"
			};
		}
	}
}
