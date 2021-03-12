using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007ED RID: 2029
	[Cmdlet("New", "AuthRedirect", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class NewAuthRedirect : NewSystemConfigurationObjectTask<AuthRedirect>
	{
		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x060046E4 RID: 18148 RVA: 0x001233A2 File Offset: 0x001215A2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewAuthRedirect(this.Name);
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x060046E5 RID: 18149 RVA: 0x001233AF File Offset: 0x001215AF
		// (set) Token: 0x060046E6 RID: 18150 RVA: 0x001233B7 File Offset: 0x001215B7
		public new string Name
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

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x060046E7 RID: 18151 RVA: 0x001233C0 File Offset: 0x001215C0
		// (set) Token: 0x060046E8 RID: 18152 RVA: 0x001233CD File Offset: 0x001215CD
		[Parameter(Mandatory = true)]
		public AuthScheme AuthScheme
		{
			get
			{
				return this.DataObject.AuthScheme;
			}
			set
			{
				this.DataObject.AuthScheme = value;
			}
		}

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x060046E9 RID: 18153 RVA: 0x001233DB File Offset: 0x001215DB
		// (set) Token: 0x060046EA RID: 18154 RVA: 0x001233E8 File Offset: 0x001215E8
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string TargetUrl
		{
			get
			{
				return this.DataObject.TargetUrl;
			}
			set
			{
				this.DataObject.TargetUrl = value;
			}
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x001233F8 File Offset: 0x001215F8
		protected override IConfigurable PrepareDataObject()
		{
			this.Name = string.Format("AuthRedirect-{0}-{1}", this.AuthScheme.ToString(), AuthRedirect.AuthRedirectKeywords);
			AuthRedirect authRedirect = (AuthRedirect)base.PrepareDataObject();
			ADObjectId childId = this.ConfigurationSession.GetOrgContainerId().GetChildId(ServiceEndpointContainer.DefaultName);
			ADObjectId childId2 = childId.GetChildId(this.Name);
			authRedirect.SetId(childId2);
			return authRedirect;
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x00123464 File Offset: 0x00121664
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.AuthScheme == AuthScheme.Unknown)
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorInvalidAuthScheme(this.AuthScheme.ToString())), ErrorCategory.InvalidArgument, null);
			}
			AuthRedirect[] array = this.ConfigurationSession.Find<AuthRedirect>(this.ConfigurationSession.GetOrgContainerId().GetChildId(ServiceEndpointContainer.DefaultName), QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, AuthRedirectSchema.AuthScheme, this.AuthScheme), null, 1);
			if (array.Length > 0)
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorAuthSchemeExists(array[0].Id.ToString(), this.AuthScheme.ToString())), ErrorCategory.InvalidArgument, null);
			}
		}
	}
}
