using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000045 RID: 69
	internal class CompositeIdentityBuilder
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x0000D56C File Offset: 0x0000B76C
		private CompositeIdentityBuilder(HttpContext context)
		{
			this.isCompositeIdentityFlightEnabled = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.CompositeIdentity.Enabled;
			this.isMsaIdentityPrimary = CompositeIdentityBuilder.IsMsaIdentityRequestedToBePrimary(context);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		public static bool IsLiveIdAuthStillNeededForOwa(HttpContext context)
		{
			CompositeIdentityBuilder compositeIdentityBuilder;
			return CompositeIdentityBuilder.TryGetInstance(context, out compositeIdentityBuilder) && compositeIdentityBuilder.isCompositeIdentityFlightEnabled && compositeIdentityBuilder.orgIdIdentity == null;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		public static void AddIdentity(HttpContext context, GenericIdentity identity, AuthenticationAuthority authAuthority)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context", "The current HTTP context must not be null!");
			}
			if (identity == null)
			{
				throw new ArgumentNullException("identity", "You cannot add a null identity to the collection!");
			}
			CompositeIdentityBuilder instance = CompositeIdentityBuilder.GetInstance(context);
			if (instance.isCompositeIdentityFlightEnabled)
			{
				instance.AddIdentity(identity, authAuthority);
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000D668 File Offset: 0x0000B868
		public static IIdentity GetUserIdentity(HttpContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context", "The current HTTP context must not be null!");
			}
			IIdentity result = null;
			CompositeIdentity compositeIdentity;
			if (!CompositeIdentityBuilder.ExecuteIfIdentitiesPresent(context, delegate(CompositeIdentityBuilder builder)
			{
				if (builder.TryGetCompositeIdentity(out compositeIdentity))
				{
					result = compositeIdentity;
					return;
				}
				result = (builder.msaIdentity ?? builder.orgIdIdentity).Identity;
			}))
			{
				result = ((context.User != null) ? context.User.Identity : null);
			}
			return result;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		public static bool TryGetMsaNoAdUserIdentity(HttpContext context, out MSAIdentity msaIdentity)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context", "The current HTTP context must not be null!");
			}
			MSAIdentity result = null;
			if (!CompositeIdentityBuilder.ExecuteIfIdentitiesPresent(context, delegate(CompositeIdentityBuilder builder)
			{
				result = ((builder.msaIdentity != null) ? (builder.msaIdentity.Identity as MSAIdentity) : null);
			}))
			{
				result = ((context.User != null) ? (context.User.Identity as MSAIdentity) : null);
			}
			msaIdentity = result;
			return msaIdentity != null;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000D76C File Offset: 0x0000B96C
		public static bool TryHandleRehydratedIdentity(HttpContext context, IIdentity rehydratedIdentity)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context", "The current HTTP context must not be null!");
			}
			if (rehydratedIdentity == null)
			{
				throw new ArgumentNullException("rehydratedIdentity", "The rehydratedIdentity cannot be null!");
			}
			CompositeIdentityBuilder instance = CompositeIdentityBuilder.GetInstance(context);
			if (!instance.isCompositeIdentityFlightEnabled)
			{
				return true;
			}
			CompositeIdentity compositeIdentity = rehydratedIdentity as CompositeIdentity;
			if (compositeIdentity != null)
			{
				instance.SetIdentityCollection(compositeIdentity);
				return true;
			}
			MSAIdentity msaidentity = rehydratedIdentity as MSAIdentity;
			if (msaidentity != null)
			{
				instance.AddIdentity(msaidentity, AuthenticationAuthority.MSA);
				return true;
			}
			GenericIdentity genericIdentity = rehydratedIdentity as GenericIdentity;
			if (genericIdentity != null)
			{
				instance.AddIdentity(genericIdentity, AuthenticationAuthority.ORGID);
				return true;
			}
			return false;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		private static bool TryGetInstance(HttpContext context, out CompositeIdentityBuilder instance)
		{
			instance = (context.Items["CompositeIdentityAuthenticationHelper"] as CompositeIdentityBuilder);
			return instance != null;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000D810 File Offset: 0x0000BA10
		private static CompositeIdentityBuilder GetInstance(HttpContext context)
		{
			CompositeIdentityBuilder compositeIdentityBuilder;
			if (!CompositeIdentityBuilder.TryGetInstance(context, out compositeIdentityBuilder))
			{
				compositeIdentityBuilder = new CompositeIdentityBuilder(context);
				context.Items["CompositeIdentityAuthenticationHelper"] = compositeIdentityBuilder;
			}
			return compositeIdentityBuilder;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000D840 File Offset: 0x0000BA40
		private static bool ExecuteIfIdentitiesPresent(HttpContext context, Action<CompositeIdentityBuilder> actionToExecute)
		{
			CompositeIdentityBuilder compositeIdentityBuilder;
			if (CompositeIdentityBuilder.TryGetInstance(context, out compositeIdentityBuilder) && compositeIdentityBuilder.isCompositeIdentityFlightEnabled && compositeIdentityBuilder.IdentitiesCount > 0)
			{
				actionToExecute(compositeIdentityBuilder);
				return true;
			}
			return false;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000D874 File Offset: 0x0000BA74
		private static string GetPrimaryIdentityHeaderValue(HttpContext context)
		{
			string text = context.Request.Headers[WellKnownHeader.PrimaryIdentity];
			if (string.IsNullOrWhiteSpace(text))
			{
				text = context.Request.Headers["X-PrimaryIdentityId"];
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000D8C0 File Offset: 0x0000BAC0
		private static bool IsMsaIdentityRequestedToBePrimary(HttpContext context)
		{
			string primaryIdentityHeaderValue = CompositeIdentityBuilder.GetPrimaryIdentityHeaderValue(context);
			return string.Equals(primaryIdentityHeaderValue, "MSA", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		private int IdentitiesCount
		{
			get
			{
				return Convert.ToInt32(this.msaIdentity != null) + Convert.ToInt32(this.orgIdIdentity != null);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000D908 File Offset: 0x0000BB08
		private bool TryGetCompositeIdentity(out CompositeIdentity compositeIdentity)
		{
			compositeIdentity = null;
			if (this.isMsaIdentityPrimary && this.msaIdentity == null)
			{
				throw new MissingIdentityException(CompositeIdentityBuilder.UnifiedMailboxMarker, "The primary identity (MSA) is missing.");
			}
			if (this.IdentitiesCount > 1)
			{
				if (this.isMsaIdentityPrimary)
				{
					compositeIdentity = new CompositeIdentity(this.msaIdentity, new IdentityRef[]
					{
						this.orgIdIdentity
					});
				}
				else
				{
					if (this.orgIdIdentity == null)
					{
						throw new MissingIdentityException(CompositeIdentityBuilder.OrgIdMailboxMarker, "The primary identity (ORGID) is missing.");
					}
					compositeIdentity = new CompositeIdentity(this.orgIdIdentity, (this.msaIdentity == null) ? null : new IdentityRef[]
					{
						this.msaIdentity
					});
				}
				return true;
			}
			return false;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000D9B0 File Offset: 0x0000BBB0
		private void AddIdentity(GenericIdentity identity, AuthenticationAuthority authAuthority)
		{
			IdentityRef identityRef = new IdentityRef(identity, authAuthority, authAuthority != AuthenticationAuthority.MSA);
			if (authAuthority == AuthenticationAuthority.MSA)
			{
				this.msaIdentity = identityRef;
				return;
			}
			this.orgIdIdentity = identityRef;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		private void SetIdentityCollection(CompositeIdentity ci)
		{
			foreach (IdentityRef identityRef in ci)
			{
				if (identityRef.Authority == AuthenticationAuthority.MSA)
				{
					this.msaIdentity = identityRef;
				}
				else
				{
					this.orgIdIdentity = identityRef;
				}
			}
		}

		// Token: 0x040001CC RID: 460
		private const string BuilderInstanceItemName = "CompositeIdentityAuthenticationHelper";

		// Token: 0x040001CD RID: 461
		public static readonly Guid UnifiedMailboxMarker = new Guid("6b08da0b-2146-48b1-9b7d-712991891aef");

		// Token: 0x040001CE RID: 462
		public static readonly Guid OrgIdMailboxMarker = new Guid("000f9a33-4461-46f3-bf99-c183947612a5");

		// Token: 0x040001CF RID: 463
		private readonly bool isMsaIdentityPrimary;

		// Token: 0x040001D0 RID: 464
		private readonly bool isCompositeIdentityFlightEnabled;

		// Token: 0x040001D1 RID: 465
		private IdentityRef msaIdentity;

		// Token: 0x040001D2 RID: 466
		private IdentityRef orgIdIdentity;
	}
}
