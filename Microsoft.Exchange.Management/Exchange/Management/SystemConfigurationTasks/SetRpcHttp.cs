using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C50 RID: 3152
	[Cmdlet("Set", "OutlookAnywhere", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetRpcHttp : SetExchangeVirtualDirectory<ADRpcHttpVirtualDirectory>
	{
		// Token: 0x170024EF RID: 9455
		// (get) Token: 0x06007795 RID: 30613 RVA: 0x001E75D0 File Offset: 0x001E57D0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRpcHttp(this.Identity.ToString());
			}
		}

		// Token: 0x170024F0 RID: 9456
		// (get) Token: 0x06007796 RID: 30614 RVA: 0x001E75E2 File Offset: 0x001E57E2
		// (set) Token: 0x06007797 RID: 30615 RVA: 0x001E75EA File Offset: 0x001E57EA
		private new Uri InternalUrl
		{
			get
			{
				return base.InternalUrl;
			}
			set
			{
				base.InternalUrl = value;
			}
		}

		// Token: 0x170024F1 RID: 9457
		// (get) Token: 0x06007798 RID: 30616 RVA: 0x001E75F3 File Offset: 0x001E57F3
		// (set) Token: 0x06007799 RID: 30617 RVA: 0x001E75FB File Offset: 0x001E57FB
		private new Uri ExternalUrl
		{
			get
			{
				return base.ExternalUrl;
			}
			set
			{
				base.ExternalUrl = value;
			}
		}

		// Token: 0x170024F2 RID: 9458
		// (get) Token: 0x0600779A RID: 30618 RVA: 0x001E7604 File Offset: 0x001E5804
		// (set) Token: 0x0600779B RID: 30619 RVA: 0x001E760C File Offset: 0x001E580C
		private new MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
		{
			get
			{
				return base.InternalAuthenticationMethods;
			}
			set
			{
				base.InternalAuthenticationMethods = value;
			}
		}

		// Token: 0x170024F3 RID: 9459
		// (get) Token: 0x0600779C RID: 30620 RVA: 0x001E7615 File Offset: 0x001E5815
		// (set) Token: 0x0600779D RID: 30621 RVA: 0x001E761D File Offset: 0x001E581D
		private new MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
		{
			get
			{
				return base.ExternalAuthenticationMethods;
			}
			set
			{
				base.ExternalAuthenticationMethods = value;
			}
		}

		// Token: 0x170024F4 RID: 9460
		// (get) Token: 0x0600779E RID: 30622 RVA: 0x001E7626 File Offset: 0x001E5826
		// (set) Token: 0x0600779F RID: 30623 RVA: 0x001E763D File Offset: 0x001E583D
		[Parameter]
		public bool SSLOffloading
		{
			get
			{
				return (bool)base.Fields["SSLOffloading"];
			}
			set
			{
				base.Fields["SSLOffloading"] = value;
			}
		}

		// Token: 0x170024F5 RID: 9461
		// (get) Token: 0x060077A0 RID: 30624 RVA: 0x001E7655 File Offset: 0x001E5855
		// (set) Token: 0x060077A1 RID: 30625 RVA: 0x001E766C File Offset: 0x001E586C
		[Parameter]
		public bool ExternalClientsRequireSsl
		{
			get
			{
				return (bool)base.Fields["ExternalClientsRequireSsl"];
			}
			set
			{
				base.Fields["ExternalClientsRequireSsl"] = value;
			}
		}

		// Token: 0x170024F6 RID: 9462
		// (get) Token: 0x060077A2 RID: 30626 RVA: 0x001E7684 File Offset: 0x001E5884
		// (set) Token: 0x060077A3 RID: 30627 RVA: 0x001E769B File Offset: 0x001E589B
		[Parameter]
		public bool InternalClientsRequireSsl
		{
			get
			{
				return (bool)base.Fields["InternalClientsRequireSsl"];
			}
			set
			{
				base.Fields["InternalClientsRequireSsl"] = value;
			}
		}

		// Token: 0x170024F7 RID: 9463
		// (get) Token: 0x060077A4 RID: 30628 RVA: 0x001E76B3 File Offset: 0x001E58B3
		// (set) Token: 0x060077A5 RID: 30629 RVA: 0x001E76CA File Offset: 0x001E58CA
		[Parameter]
		public string ExternalHostname
		{
			get
			{
				return (string)base.Fields["ExternalHostname"];
			}
			set
			{
				base.Fields["ExternalHostname"] = value;
			}
		}

		// Token: 0x170024F8 RID: 9464
		// (get) Token: 0x060077A6 RID: 30630 RVA: 0x001E76DD File Offset: 0x001E58DD
		// (set) Token: 0x060077A7 RID: 30631 RVA: 0x001E76F4 File Offset: 0x001E58F4
		[Parameter]
		public string InternalHostname
		{
			get
			{
				return (string)base.Fields["InternalHostname"];
			}
			set
			{
				base.Fields["InternalHostname"] = value;
			}
		}

		// Token: 0x170024F9 RID: 9465
		// (get) Token: 0x060077A8 RID: 30632 RVA: 0x001E7707 File Offset: 0x001E5907
		// (set) Token: 0x060077A9 RID: 30633 RVA: 0x001E771E File Offset: 0x001E591E
		[Parameter]
		public AuthenticationMethod DefaultAuthenticationMethod
		{
			get
			{
				return (AuthenticationMethod)base.Fields["DefaultAuthenticationMethod"];
			}
			set
			{
				base.Fields["DefaultAuthenticationMethod"] = value;
			}
		}

		// Token: 0x170024FA RID: 9466
		// (get) Token: 0x060077AA RID: 30634 RVA: 0x001E7736 File Offset: 0x001E5936
		// (set) Token: 0x060077AB RID: 30635 RVA: 0x001E774D File Offset: 0x001E594D
		[Parameter]
		public AuthenticationMethod ExternalClientAuthenticationMethod
		{
			get
			{
				return (AuthenticationMethod)base.Fields["ExternalClientAuthenticationMethod"];
			}
			set
			{
				base.Fields["ExternalClientAuthenticationMethod"] = value;
			}
		}

		// Token: 0x170024FB RID: 9467
		// (get) Token: 0x060077AC RID: 30636 RVA: 0x001E7765 File Offset: 0x001E5965
		// (set) Token: 0x060077AD RID: 30637 RVA: 0x001E777C File Offset: 0x001E597C
		[Parameter]
		public AuthenticationMethod InternalClientAuthenticationMethod
		{
			get
			{
				return (AuthenticationMethod)base.Fields["InternalClientAuthenticationMethod"];
			}
			set
			{
				base.Fields["InternalClientAuthenticationMethod"] = value;
			}
		}

		// Token: 0x170024FC RID: 9468
		// (get) Token: 0x060077AE RID: 30638 RVA: 0x001E7794 File Offset: 0x001E5994
		// (set) Token: 0x060077AF RID: 30639 RVA: 0x001E77AB File Offset: 0x001E59AB
		[Parameter]
		public Uri XropUrl
		{
			get
			{
				return (Uri)base.Fields["XropUrl"];
			}
			set
			{
				base.Fields["XropUrl"] = value;
			}
		}

		// Token: 0x170024FD RID: 9469
		// (get) Token: 0x060077B0 RID: 30640 RVA: 0x001E77BE File Offset: 0x001E59BE
		// (set) Token: 0x060077B1 RID: 30641 RVA: 0x001E77D5 File Offset: 0x001E59D5
		[Parameter]
		public MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
		{
			get
			{
				return (MultiValuedProperty<AuthenticationMethod>)base.Fields["IISAuthenticationMethods"];
			}
			set
			{
				base.Fields["IISAuthenticationMethods"] = value;
			}
		}

		// Token: 0x060077B2 RID: 30642 RVA: 0x001E77E8 File Offset: 0x001E59E8
		protected override IConfigurable PrepareDataObject()
		{
			ADRpcHttpVirtualDirectory adrpcHttpVirtualDirectory = (ADRpcHttpVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (base.Fields.Contains("ExternalHostname"))
			{
				if (!string.IsNullOrEmpty(this.ExternalHostname))
				{
					if (!base.Fields.Contains("ExternalClientsRequireSsl"))
					{
						base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyExternalClientsRequireSslParameter), ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
					}
					if (!base.Fields.Contains("DefaultAuthenticationMethod") && !base.Fields.Contains("ExternalClientAuthenticationMethod"))
					{
						base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyExternalClientAuthenticationParameter), ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
					}
					try
					{
						adrpcHttpVirtualDirectory.ExternalHostname = new Hostname(this.ExternalHostname);
						goto IL_D4;
					}
					catch (ArgumentException exception)
					{
						base.WriteError(exception, ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
						goto IL_D4;
					}
				}
				adrpcHttpVirtualDirectory.ExternalHostname = null;
			}
			IL_D4:
			if (base.Fields.Contains("InternalHostname"))
			{
				if (!string.IsNullOrEmpty(this.InternalHostname))
				{
					if (!base.Fields.Contains("InternalClientsRequireSsl"))
					{
						base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyInternalClientsRequireSslParameter), ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
					}
					try
					{
						adrpcHttpVirtualDirectory.InternalHostname = new Hostname(this.InternalHostname);
						goto IL_14C;
					}
					catch (ArgumentException exception2)
					{
						base.WriteError(exception2, ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
						goto IL_14C;
					}
				}
				adrpcHttpVirtualDirectory.InternalHostname = null;
			}
			IL_14C:
			if (base.Fields["ExternalClientsRequireSsl"] != null)
			{
				adrpcHttpVirtualDirectory.ExternalClientsRequireSsl = this.ExternalClientsRequireSsl;
			}
			if (base.Fields["InternalClientsRequireSsl"] != null)
			{
				adrpcHttpVirtualDirectory.InternalClientsRequireSsl = this.InternalClientsRequireSsl;
			}
			if (base.Fields.Contains("SSLOffloading"))
			{
				adrpcHttpVirtualDirectory.SSLOffloading = this.SSLOffloading;
			}
			if (base.Fields.Contains("XropUrl"))
			{
				adrpcHttpVirtualDirectory.XropUrl = this.XropUrl;
			}
			if (base.Fields.Contains("DefaultAuthenticationMethod"))
			{
				if (base.Fields.Contains("ExternalClientAuthenticationMethod") || base.Fields.Contains("InternalClientAuthenticationMethod") || base.Fields.Contains("IISAuthenticationMethods"))
				{
					base.WriteError(new ArgumentException(Strings.ErrorRpcHttpInvalidSwitchCombo), ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
				}
				if (this.DefaultAuthenticationMethod == AuthenticationMethod.Negotiate)
				{
					this.WriteWarning(Strings.WarnRpcHttpNegotiateCoexistence);
				}
				adrpcHttpVirtualDirectory.ExternalClientAuthenticationMethod = this.DefaultAuthenticationMethod;
				adrpcHttpVirtualDirectory.InternalClientAuthenticationMethod = this.DefaultAuthenticationMethod;
				adrpcHttpVirtualDirectory.IISAuthenticationMethods = new MultiValuedProperty<AuthenticationMethod>();
				adrpcHttpVirtualDirectory.IISAuthenticationMethods.Add(this.DefaultAuthenticationMethod);
			}
			else
			{
				if ((base.Fields.Contains("ExternalClientAuthenticationMethod") && this.ExternalClientAuthenticationMethod == AuthenticationMethod.Negotiate) || (base.Fields.Contains("InternalClientAuthenticationMethod") && this.InternalClientAuthenticationMethod == AuthenticationMethod.Negotiate))
				{
					this.WriteWarning(Strings.WarnRpcHttpNegotiateCoexistence);
				}
				if (base.Fields.Contains("ExternalClientAuthenticationMethod"))
				{
					adrpcHttpVirtualDirectory.ExternalClientAuthenticationMethod = this.ExternalClientAuthenticationMethod;
				}
				if (base.Fields.Contains("InternalClientAuthenticationMethod"))
				{
					adrpcHttpVirtualDirectory.InternalClientAuthenticationMethod = this.InternalClientAuthenticationMethod;
				}
				if (base.Fields.Contains("IISAuthenticationMethods"))
				{
					adrpcHttpVirtualDirectory.IISAuthenticationMethods = this.IISAuthenticationMethods;
				}
			}
			bool flag = adrpcHttpVirtualDirectory.ExternalHostname != null && !string.IsNullOrEmpty(adrpcHttpVirtualDirectory.ExternalHostname.ToString());
			bool flag2 = adrpcHttpVirtualDirectory.InternalHostname != null && !string.IsNullOrEmpty(adrpcHttpVirtualDirectory.InternalHostname.ToString());
			if (!flag && !flag2)
			{
				base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyEitherInternalOrExternalHostName), ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
			}
			if (flag && adrpcHttpVirtualDirectory.ExternalClientAuthenticationMethod == AuthenticationMethod.Misconfigured)
			{
				base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyExternalClientAuthenticationMethodOrDefaultAuthenticationMethod), ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
			}
			if (flag && adrpcHttpVirtualDirectory.ExternalClientAuthenticationMethod == AuthenticationMethod.Basic && !adrpcHttpVirtualDirectory.ExternalClientsRequireSsl)
			{
				base.WriteError(new ArgumentException(Strings.ErrorRpcHttpBasicAuthOverHttpDisallowed), ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
			}
			if (flag2 && adrpcHttpVirtualDirectory.InternalClientAuthenticationMethod == AuthenticationMethod.Misconfigured)
			{
				base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyInternalClientAuthenticationMethodOrDefaultAuthenticationMethod), ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
			}
			if (flag2 && adrpcHttpVirtualDirectory.InternalClientAuthenticationMethod == AuthenticationMethod.Basic && !adrpcHttpVirtualDirectory.InternalClientsRequireSsl)
			{
				base.WriteError(new ArgumentException(Strings.ErrorRpcHttpBasicAuthOverHttpDisallowed), ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
			}
			if (!adrpcHttpVirtualDirectory.SSLOffloading && ((flag && !adrpcHttpVirtualDirectory.ExternalClientsRequireSsl) || (flag2 && !adrpcHttpVirtualDirectory.InternalClientsRequireSsl)))
			{
				base.WriteError(new ArgumentException(Strings.ErrorRpcHttpSSLOffloadingDisabled), ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
			}
			if (adrpcHttpVirtualDirectory.IISAuthenticationMethods == null)
			{
				base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyEitherIISAuthenticationMethodsOrDefaultAuthenticationMethod), ErrorCategory.InvalidArgument, adrpcHttpVirtualDirectory.Identity);
			}
			return adrpcHttpVirtualDirectory;
		}

		// Token: 0x04003BDF RID: 15327
		private const string SSLOffloadingKey = "SSLOffloading";

		// Token: 0x04003BE0 RID: 15328
		private const string ExternalHostnameKey = "ExternalHostname";

		// Token: 0x04003BE1 RID: 15329
		private const string InternalHostnameKey = "InternalHostname";

		// Token: 0x04003BE2 RID: 15330
		private const string ExternalClientAuthenticationMethodKey = "ExternalClientAuthenticationMethod";

		// Token: 0x04003BE3 RID: 15331
		private const string InternalClientAuthenticationMethodKey = "InternalClientAuthenticationMethod";

		// Token: 0x04003BE4 RID: 15332
		private const string ExternalClientsRequireSslKey = "ExternalClientsRequireSsl";

		// Token: 0x04003BE5 RID: 15333
		private const string InternalClientsRequireSslKey = "InternalClientsRequireSsl";

		// Token: 0x04003BE6 RID: 15334
		private const string IISAuthenticationMethodsKey = "IISAuthenticationMethods";

		// Token: 0x04003BE7 RID: 15335
		private const string DefaultAuthenticationMethodKey = "DefaultAuthenticationMethod";

		// Token: 0x04003BE8 RID: 15336
		private const string XropUrlKey = "XropUrl";
	}
}
