using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C03 RID: 3075
	[Cmdlet("Enable", "OutlookAnywhere", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class EnableRpcHttp : NewExchangeVirtualDirectory<ADRpcHttpVirtualDirectory>
	{
		// Token: 0x170023BA RID: 9146
		// (get) Token: 0x0600741B RID: 29723 RVA: 0x001D91CC File Offset: 0x001D73CC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				bool flag = this.ExternalHostname != null && !string.IsNullOrEmpty(this.ExternalHostname.ToString());
				bool flag2 = this.InternalHostname != null && !string.IsNullOrEmpty(this.InternalHostname.ToString());
				if (flag && flag2)
				{
					return Strings.ConfirmationMessageEnableRpcHttpExternalAndInternal(this.WebSiteName.ToString(), base.Server.ToString(), this.ExternalHostname.ToString(), this.ExternalClientAuthenticationMethod.ToString(), this.InternalHostname.ToString(), this.InternalClientAuthenticationMethod.ToString(), this.IISAuthenticationMethods.ToString());
				}
				if (flag2)
				{
					return Strings.ConfirmationMessageEnableRpcHttpInternalOnly(this.WebSiteName.ToString(), base.Server.ToString(), this.InternalHostname.ToString(), this.InternalClientAuthenticationMethod.ToString(), this.IISAuthenticationMethods.ToString());
				}
				return Strings.ConfirmationMessageEnableRpcHttpExternalOnly(this.WebSiteName.ToString(), base.Server.ToString(), this.ExternalHostname.ToString(), this.ExternalClientAuthenticationMethod.ToString(), this.IISAuthenticationMethods.ToString());
			}
		}

		// Token: 0x0600741C RID: 29724 RVA: 0x001D9300 File Offset: 0x001D7500
		public EnableRpcHttp()
		{
			this.Name = "Rpc";
			this.WebSiteName = null;
		}

		// Token: 0x170023BB RID: 9147
		// (get) Token: 0x0600741D RID: 29725 RVA: 0x001D9330 File Offset: 0x001D7530
		// (set) Token: 0x0600741E RID: 29726 RVA: 0x001D9338 File Offset: 0x001D7538
		private new string WebSiteName
		{
			get
			{
				return base.WebSiteName;
			}
			set
			{
				base.WebSiteName = value;
			}
		}

		// Token: 0x170023BC RID: 9148
		// (get) Token: 0x0600741F RID: 29727 RVA: 0x001D9341 File Offset: 0x001D7541
		// (set) Token: 0x06007420 RID: 29728 RVA: 0x001D9349 File Offset: 0x001D7549
		private new string Path
		{
			get
			{
				return base.Path;
			}
			set
			{
				base.Path = value;
			}
		}

		// Token: 0x170023BD RID: 9149
		// (get) Token: 0x06007421 RID: 29729 RVA: 0x001D9352 File Offset: 0x001D7552
		// (set) Token: 0x06007422 RID: 29730 RVA: 0x001D935A File Offset: 0x001D755A
		private new string AppPoolId
		{
			get
			{
				return base.AppPoolId;
			}
			set
			{
				base.AppPoolId = value;
			}
		}

		// Token: 0x170023BE RID: 9150
		// (get) Token: 0x06007423 RID: 29731 RVA: 0x001D9363 File Offset: 0x001D7563
		// (set) Token: 0x06007424 RID: 29732 RVA: 0x001D936B File Offset: 0x001D756B
		private new string ApplicationRoot
		{
			get
			{
				return base.ApplicationRoot;
			}
			set
			{
				base.ApplicationRoot = value;
			}
		}

		// Token: 0x170023BF RID: 9151
		// (get) Token: 0x06007425 RID: 29733 RVA: 0x001D9374 File Offset: 0x001D7574
		// (set) Token: 0x06007426 RID: 29734 RVA: 0x001D937C File Offset: 0x001D757C
		private new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x170023C0 RID: 9152
		// (get) Token: 0x06007427 RID: 29735 RVA: 0x001D9385 File Offset: 0x001D7585
		// (set) Token: 0x06007428 RID: 29736 RVA: 0x001D938D File Offset: 0x001D758D
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

		// Token: 0x170023C1 RID: 9153
		// (get) Token: 0x06007429 RID: 29737 RVA: 0x001D9396 File Offset: 0x001D7596
		// (set) Token: 0x0600742A RID: 29738 RVA: 0x001D939E File Offset: 0x001D759E
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

		// Token: 0x170023C2 RID: 9154
		// (get) Token: 0x0600742B RID: 29739 RVA: 0x001D93A7 File Offset: 0x001D75A7
		// (set) Token: 0x0600742C RID: 29740 RVA: 0x001D93AF File Offset: 0x001D75AF
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

		// Token: 0x170023C3 RID: 9155
		// (get) Token: 0x0600742D RID: 29741 RVA: 0x001D93B8 File Offset: 0x001D75B8
		// (set) Token: 0x0600742E RID: 29742 RVA: 0x001D93C0 File Offset: 0x001D75C0
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

		// Token: 0x0600742F RID: 29743 RVA: 0x001D93C9 File Offset: 0x001D75C9
		protected override bool FailOnVirtualDirectoryAlreadyExists()
		{
			return false;
		}

		// Token: 0x06007430 RID: 29744 RVA: 0x001D93CC File Offset: 0x001D75CC
		protected override bool InternalShouldCreateMetabaseObject()
		{
			return false;
		}

		// Token: 0x170023C4 RID: 9156
		// (get) Token: 0x06007431 RID: 29745 RVA: 0x001D93CF File Offset: 0x001D75CF
		// (set) Token: 0x06007432 RID: 29746 RVA: 0x001D93DC File Offset: 0x001D75DC
		[Parameter(Mandatory = true)]
		public bool SSLOffloading
		{
			get
			{
				return this.DataObject.SSLOffloading;
			}
			set
			{
				this.DataObject.SSLOffloading = value;
			}
		}

		// Token: 0x170023C5 RID: 9157
		// (get) Token: 0x06007433 RID: 29747 RVA: 0x001D93EA File Offset: 0x001D75EA
		// (set) Token: 0x06007434 RID: 29748 RVA: 0x001D93F7 File Offset: 0x001D75F7
		[Parameter]
		public Hostname ExternalHostname
		{
			get
			{
				return this.DataObject.ExternalHostname;
			}
			set
			{
				this.DataObject.ExternalHostname = value;
			}
		}

		// Token: 0x170023C6 RID: 9158
		// (get) Token: 0x06007435 RID: 29749 RVA: 0x001D9405 File Offset: 0x001D7605
		// (set) Token: 0x06007436 RID: 29750 RVA: 0x001D9412 File Offset: 0x001D7612
		[Parameter]
		public Hostname InternalHostname
		{
			get
			{
				return this.DataObject.InternalHostname;
			}
			set
			{
				this.DataObject.InternalHostname = value;
			}
		}

		// Token: 0x170023C7 RID: 9159
		// (get) Token: 0x06007438 RID: 29752 RVA: 0x001D9447 File Offset: 0x001D7647
		// (set) Token: 0x06007437 RID: 29751 RVA: 0x001D9420 File Offset: 0x001D7620
		[Parameter]
		public AuthenticationMethod DefaultAuthenticationMethod
		{
			get
			{
				return AuthenticationMethod.Misconfigured;
			}
			set
			{
				this.externalClientAuthenticationMethod = value;
				this.internalClientAuthenticationMethod = value;
				this.iisAuthenticationMethods = new MultiValuedProperty<AuthenticationMethod>();
				this.iisAuthenticationMethods.Add(value);
			}
		}

		// Token: 0x170023C8 RID: 9160
		// (get) Token: 0x06007439 RID: 29753 RVA: 0x001D944E File Offset: 0x001D764E
		// (set) Token: 0x0600743A RID: 29754 RVA: 0x001D9456 File Offset: 0x001D7656
		[Parameter]
		public AuthenticationMethod ExternalClientAuthenticationMethod
		{
			get
			{
				return this.externalClientAuthenticationMethod;
			}
			set
			{
				this.externalClientAuthenticationMethod = value;
			}
		}

		// Token: 0x170023C9 RID: 9161
		// (get) Token: 0x0600743B RID: 29755 RVA: 0x001D945F File Offset: 0x001D765F
		// (set) Token: 0x0600743C RID: 29756 RVA: 0x001D9467 File Offset: 0x001D7667
		[Parameter]
		public AuthenticationMethod InternalClientAuthenticationMethod
		{
			get
			{
				return this.internalClientAuthenticationMethod;
			}
			set
			{
				this.internalClientAuthenticationMethod = value;
			}
		}

		// Token: 0x170023CA RID: 9162
		// (get) Token: 0x0600743D RID: 29757 RVA: 0x001D9470 File Offset: 0x001D7670
		// (set) Token: 0x0600743E RID: 29758 RVA: 0x001D9478 File Offset: 0x001D7678
		[Parameter]
		public MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
		{
			get
			{
				return this.iisAuthenticationMethods;
			}
			set
			{
				this.iisAuthenticationMethods = value;
			}
		}

		// Token: 0x170023CB RID: 9163
		// (get) Token: 0x0600743F RID: 29759 RVA: 0x001D9481 File Offset: 0x001D7681
		// (set) Token: 0x06007440 RID: 29760 RVA: 0x001D948E File Offset: 0x001D768E
		[Parameter]
		public Uri XropUrl
		{
			get
			{
				return this.DataObject.XropUrl;
			}
			set
			{
				this.DataObject.XropUrl = value;
			}
		}

		// Token: 0x170023CC RID: 9164
		// (get) Token: 0x06007441 RID: 29761 RVA: 0x001D949C File Offset: 0x001D769C
		// (set) Token: 0x06007442 RID: 29762 RVA: 0x001D94C2 File Offset: 0x001D76C2
		[Parameter]
		public bool ExternalClientsRequireSsl
		{
			get
			{
				return this.externalClientsRequireSsl ?? false;
			}
			set
			{
				this.externalClientsRequireSsl = new bool?(value);
			}
		}

		// Token: 0x170023CD RID: 9165
		// (get) Token: 0x06007443 RID: 29763 RVA: 0x001D94D0 File Offset: 0x001D76D0
		// (set) Token: 0x06007444 RID: 29764 RVA: 0x001D94F6 File Offset: 0x001D76F6
		[Parameter]
		public bool InternalClientsRequireSsl
		{
			get
			{
				return this.internalClientsRequireSsl ?? false;
			}
			set
			{
				this.internalClientsRequireSsl = new bool?(value);
			}
		}

		// Token: 0x06007445 RID: 29765 RVA: 0x001D9504 File Offset: 0x001D7704
		private string StringFromList(List<Server> serverList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Server server in serverList)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(server.Fqdn);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007446 RID: 29766 RVA: 0x001D957C File Offset: 0x001D777C
		private bool IsInstalled()
		{
			ADRpcHttpVirtualDirectory[] array = (base.DataSession as IConfigurationSession).Find<ADRpcHttpVirtualDirectory>((ADObjectId)base.OwningServer.Identity, QueryScope.SubTree, null, null, 1);
			return array.Length > 0;
		}

		// Token: 0x06007447 RID: 29767 RVA: 0x001D95B4 File Offset: 0x001D77B4
		private void ValidateParameterValues()
		{
			bool flag = this.ExternalHostname != null && !string.IsNullOrEmpty(this.ExternalHostname.ToString());
			bool flag2 = this.InternalHostname != null && !string.IsNullOrEmpty(this.InternalHostname.ToString());
			if (!flag && !flag2)
			{
				base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyEitherInternalOrExternalHostName), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				return;
			}
			if (flag)
			{
				if (this.ExternalClientAuthenticationMethod == AuthenticationMethod.Misconfigured)
				{
					base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyExternalClientAuthenticationMethodOrDefaultAuthenticationMethod), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					return;
				}
				if (this.externalClientsRequireSsl == null)
				{
					base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyExternalClientsRequireSslParameter), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					return;
				}
				if (this.ExternalClientAuthenticationMethod == AuthenticationMethod.Basic && !this.ExternalClientsRequireSsl)
				{
					base.WriteError(new ArgumentException(Strings.ErrorRpcHttpBasicAuthOverHttpDisallowed), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					return;
				}
				this.DataObject.ExternalClientAuthenticationMethod = this.ExternalClientAuthenticationMethod;
				this.DataObject.ExternalClientsRequireSsl = this.ExternalClientsRequireSsl;
			}
			else
			{
				this.DataObject.ExternalClientAuthenticationMethod = AuthenticationMethod.Negotiate;
			}
			if (flag2)
			{
				if (this.InternalClientAuthenticationMethod == AuthenticationMethod.Misconfigured)
				{
					base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyInternalClientAuthenticationMethodOrDefaultAuthenticationMethod), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					return;
				}
				if (this.internalClientsRequireSsl == null)
				{
					base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyInternalClientsRequireSslParameter), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					return;
				}
				if (this.InternalClientAuthenticationMethod == AuthenticationMethod.Basic && !this.InternalClientsRequireSsl)
				{
					base.WriteError(new ArgumentException(Strings.ErrorRpcHttpBasicAuthOverHttpDisallowed), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					return;
				}
				this.DataObject.InternalClientAuthenticationMethod = this.InternalClientAuthenticationMethod;
				this.DataObject.InternalClientsRequireSsl = this.InternalClientsRequireSsl;
			}
			else
			{
				this.DataObject.InternalClientAuthenticationMethod = AuthenticationMethod.Negotiate;
			}
			if (this.IISAuthenticationMethods == null)
			{
				base.WriteError(new ArgumentException(Strings.ErrorRpcHttpMustSpecifyEitherIISAuthenticationMethodsOrDefaultAuthenticationMethod), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				return;
			}
			if (!this.SSLOffloading && ((flag && !this.ExternalClientsRequireSsl) || (flag2 && !this.InternalClientsRequireSsl)))
			{
				base.WriteError(new ArgumentException(Strings.ErrorRpcHttpSSLOffloadingDisabled), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			this.DataObject.IISAuthenticationMethods = this.iisAuthenticationMethods;
		}

		// Token: 0x06007448 RID: 29768 RVA: 0x001D9830 File Offset: 0x001D7A30
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.ValidateParameterValues();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			try
			{
				if (this.IsInstalled())
				{
					base.WriteError(new ArgumentException(Strings.ErrorRpcHttpAlreadyEnabled(base.OwningServer.Fqdn), string.Empty), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					return;
				}
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.LessThan, ServerSchema.VersionNumber, Microsoft.Exchange.Data.Directory.SystemConfiguration.Server.E2007MinVersion);
				IEnumerable<Server> enumerable = (base.DataSession as IConfigurationSession).FindPaged<Server>(null, QueryScope.SubTree, filter, null, 0);
				List<Server> list = new List<Server>();
				List<Server> list2 = new List<Server>();
				List<Server> list3 = new List<Server>();
				foreach (Server server in enumerable)
				{
					if (!server.IsExchange2003OrLater)
					{
						list.Add(server);
					}
					else if (!server.IsExchange2003Sp1OrLater)
					{
						list2.Add(server);
					}
					else if (!server.IsPreE12FrontEnd && !server.IsPreE12RPCHTTPEnabled)
					{
						list3.Add(server);
					}
				}
				if (list.Count > 0)
				{
					string servers = this.StringFromList(list);
					this.WriteWarning(Strings.RpcHttpE2kServers(servers));
				}
				if (list2.Count > 0)
				{
					string servers2 = this.StringFromList(list2);
					this.WriteWarning(Strings.RpcHttpE2k3Servers(servers2));
				}
				if (list3.Count > 0)
				{
					string servers3 = this.StringFromList(list3);
					this.WriteWarning(Strings.RpcHttpTiSp1FeatureDisabled(servers3));
				}
				if (list2.Count > 0 || list3.Count > 0)
				{
					string text = this.StringFromList(list2);
					string text2 = this.StringFromList(list3);
					if (text.Length > 0 && text2.Length > 0)
					{
						text += ", ";
					}
					text += text2;
					this.WriteWarning(Strings.RpcHttpOldOSServers(text));
				}
				this.WriteWarning(Strings.RpcHttpAvailability(this.DataObject.ServerName));
			}
			catch (ADTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, this.DataObject.Identity);
			}
			catch (DataValidationException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidData, this.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003AEF RID: 15087
		private const string VirtualDirectoryName = "Rpc";

		// Token: 0x04003AF0 RID: 15088
		private AuthenticationMethod externalClientAuthenticationMethod = AuthenticationMethod.Misconfigured;

		// Token: 0x04003AF1 RID: 15089
		private AuthenticationMethod internalClientAuthenticationMethod = AuthenticationMethod.Misconfigured;

		// Token: 0x04003AF2 RID: 15090
		private MultiValuedProperty<AuthenticationMethod> iisAuthenticationMethods;

		// Token: 0x04003AF3 RID: 15091
		private bool? externalClientsRequireSsl;

		// Token: 0x04003AF4 RID: 15092
		private bool? internalClientsRequireSsl;
	}
}
