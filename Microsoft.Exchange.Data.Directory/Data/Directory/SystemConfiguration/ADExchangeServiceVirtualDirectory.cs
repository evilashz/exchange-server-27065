using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000324 RID: 804
	[Serializable]
	public abstract class ADExchangeServiceVirtualDirectory : ExchangeVirtualDirectory
	{
		// Token: 0x0600254C RID: 9548 RVA: 0x0009E918 File Offset: 0x0009CB18
		protected static void AddOrRemoveAuthenticationMethod(List<AuthenticationMethod> authenticationMethods, bool? authenticationMethodFlag, params AuthenticationMethod[] applicableAuthenticationMethods)
		{
			if (authenticationMethodFlag != null)
			{
				if (authenticationMethodFlag.Value)
				{
					foreach (AuthenticationMethod item in applicableAuthenticationMethods)
					{
						if (!authenticationMethods.Contains(item))
						{
							authenticationMethods.Add(item);
						}
					}
					return;
				}
				foreach (AuthenticationMethod item2 in applicableAuthenticationMethods)
				{
					authenticationMethods.Remove(item2);
				}
			}
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x0009E980 File Offset: 0x0009CB80
		private void UpdateInternalAndExternalAuthenticationMethods()
		{
			List<AuthenticationMethod> list = new List<AuthenticationMethod>();
			if (base.InternalAuthenticationMethods != null)
			{
				list.AddRange(base.InternalAuthenticationMethods);
			}
			ADExchangeServiceVirtualDirectory.AddOrRemoveAuthenticationMethod(list, this.LiveIdNegotiateAuthentication, new AuthenticationMethod[]
			{
				AuthenticationMethod.LiveIdNegotiate
			});
			ADExchangeServiceVirtualDirectory.AddOrRemoveAuthenticationMethod(list, this.AdfsAuthentication, new AuthenticationMethod[]
			{
				AuthenticationMethod.Adfs
			});
			ADExchangeServiceVirtualDirectory.AddOrRemoveAuthenticationMethod(list, this.WSSecurityAuthentication, new AuthenticationMethod[]
			{
				AuthenticationMethod.WSSecurity
			});
			ADExchangeServiceVirtualDirectory.AddOrRemoveAuthenticationMethod(list, this.LiveIdBasicAuthentication, new AuthenticationMethod[]
			{
				AuthenticationMethod.LiveIdBasic
			});
			List<AuthenticationMethod> authenticationMethods = list;
			bool? authenticationMethodFlag = this.BasicAuthentication;
			AuthenticationMethod[] applicableAuthenticationMethods = new AuthenticationMethod[1];
			ADExchangeServiceVirtualDirectory.AddOrRemoveAuthenticationMethod(authenticationMethods, authenticationMethodFlag, applicableAuthenticationMethods);
			ADExchangeServiceVirtualDirectory.AddOrRemoveAuthenticationMethod(list, this.DigestAuthentication, new AuthenticationMethod[]
			{
				AuthenticationMethod.Digest
			});
			ADExchangeServiceVirtualDirectory.AddOrRemoveAuthenticationMethod(list, this.WindowsAuthentication, new AuthenticationMethod[]
			{
				AuthenticationMethod.Ntlm,
				AuthenticationMethod.WindowsIntegrated
			});
			ADExchangeServiceVirtualDirectory.AddOrRemoveAuthenticationMethod(list, this.OAuthAuthentication, new AuthenticationMethod[]
			{
				AuthenticationMethod.OAuth
			});
			MultiValuedProperty<AuthenticationMethod> multiValuedProperty = new MultiValuedProperty<AuthenticationMethod>(list);
			base.InternalAuthenticationMethods = multiValuedProperty;
			base.ExternalAuthenticationMethods = multiValuedProperty;
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x0009EA91 File Offset: 0x0009CC91
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x0009EAA4 File Offset: 0x0009CCA4
		// (set) Token: 0x06002550 RID: 9552 RVA: 0x0009EAAC File Offset: 0x0009CCAC
		public new string Name
		{
			get
			{
				return base.Name;
			}
			internal set
			{
				base.Name = value;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x0009EAB5 File Offset: 0x0009CCB5
		public new MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
		{
			get
			{
				return base.InternalAuthenticationMethods;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x0009EABD File Offset: 0x0009CCBD
		public new MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
		{
			get
			{
				return base.ExternalAuthenticationMethods;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x0009EAC5 File Offset: 0x0009CCC5
		// (set) Token: 0x06002554 RID: 9556 RVA: 0x0009EACD File Offset: 0x0009CCCD
		public bool? LiveIdNegotiateAuthentication
		{
			get
			{
				return this.liveIdNegotiateAuthentication;
			}
			set
			{
				this.liveIdNegotiateAuthentication = value;
				this.UpdateInternalAndExternalAuthenticationMethods();
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x0009EADC File Offset: 0x0009CCDC
		// (set) Token: 0x06002556 RID: 9558 RVA: 0x0009EAE4 File Offset: 0x0009CCE4
		public bool? WSSecurityAuthentication
		{
			get
			{
				return this.wsSecurityAuthentication;
			}
			set
			{
				this.wsSecurityAuthentication = value;
				this.UpdateInternalAndExternalAuthenticationMethods();
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x0009EAF3 File Offset: 0x0009CCF3
		// (set) Token: 0x06002558 RID: 9560 RVA: 0x0009EAFB File Offset: 0x0009CCFB
		public bool? LiveIdBasicAuthentication
		{
			get
			{
				return this.liveIdBasicAuthentication;
			}
			set
			{
				this.liveIdBasicAuthentication = value;
				this.UpdateInternalAndExternalAuthenticationMethods();
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x0009EB0A File Offset: 0x0009CD0A
		// (set) Token: 0x0600255A RID: 9562 RVA: 0x0009EB12 File Offset: 0x0009CD12
		public bool? BasicAuthentication
		{
			get
			{
				return this.basicAuthentication;
			}
			set
			{
				this.basicAuthentication = value;
				this.UpdateInternalAndExternalAuthenticationMethods();
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x0009EB21 File Offset: 0x0009CD21
		// (set) Token: 0x0600255C RID: 9564 RVA: 0x0009EB29 File Offset: 0x0009CD29
		public bool? DigestAuthentication
		{
			get
			{
				return this.digestAuthentication;
			}
			set
			{
				this.digestAuthentication = value;
				this.UpdateInternalAndExternalAuthenticationMethods();
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x0009EB38 File Offset: 0x0009CD38
		// (set) Token: 0x0600255E RID: 9566 RVA: 0x0009EB40 File Offset: 0x0009CD40
		public bool? WindowsAuthentication
		{
			get
			{
				return this.windowsAuthentication;
			}
			set
			{
				this.windowsAuthentication = value;
				this.UpdateInternalAndExternalAuthenticationMethods();
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600255F RID: 9567 RVA: 0x0009EB4F File Offset: 0x0009CD4F
		// (set) Token: 0x06002560 RID: 9568 RVA: 0x0009EB57 File Offset: 0x0009CD57
		public bool? OAuthAuthentication
		{
			get
			{
				return this.oAuthAuthentication;
			}
			set
			{
				this.oAuthAuthentication = value;
				this.UpdateInternalAndExternalAuthenticationMethods();
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x0009EB66 File Offset: 0x0009CD66
		// (set) Token: 0x06002562 RID: 9570 RVA: 0x0009EB6E File Offset: 0x0009CD6E
		public bool? AdfsAuthentication
		{
			get
			{
				return this.adfsAuthentication;
			}
			set
			{
				this.adfsAuthentication = value;
				this.UpdateInternalAndExternalAuthenticationMethods();
			}
		}

		// Token: 0x040016E8 RID: 5864
		private bool? basicAuthentication = null;

		// Token: 0x040016E9 RID: 5865
		private bool? digestAuthentication = null;

		// Token: 0x040016EA RID: 5866
		private bool? windowsAuthentication = null;

		// Token: 0x040016EB RID: 5867
		private bool? liveIdBasicAuthentication = null;

		// Token: 0x040016EC RID: 5868
		private bool? wsSecurityAuthentication = null;

		// Token: 0x040016ED RID: 5869
		private bool? liveIdNegotiateAuthentication = null;

		// Token: 0x040016EE RID: 5870
		private bool? oAuthAuthentication = null;

		// Token: 0x040016EF RID: 5871
		private bool? adfsAuthentication = null;
	}
}
