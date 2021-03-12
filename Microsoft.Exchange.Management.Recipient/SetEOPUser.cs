using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000040 RID: 64
	[Cmdlet("Set", "EOPUser", DefaultParameterSetName = "Identity")]
	public sealed class SetEOPUser : EOPTask
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000DC60 File Offset: 0x0000BE60
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000DC68 File Offset: 0x0000BE68
		[Parameter(Mandatory = false)]
		public UserIdParameter Identity { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000DC71 File Offset: 0x0000BE71
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000DC79 File Offset: 0x0000BE79
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000DC82 File Offset: 0x0000BE82
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000DC8A File Offset: 0x0000BE8A
		[Parameter(Mandatory = false)]
		public string City { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000DC93 File Offset: 0x0000BE93
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000DC9B File Offset: 0x0000BE9B
		[Parameter(Mandatory = false)]
		public string Company { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		[Parameter(Mandatory = false)]
		public CountryInfo CountryOrRegion { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000DCB5 File Offset: 0x0000BEB5
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000DCBD File Offset: 0x0000BEBD
		[Parameter(Mandatory = false)]
		public string Department { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000DCC6 File Offset: 0x0000BEC6
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000DCCE File Offset: 0x0000BECE
		[Parameter(Mandatory = false)]
		public string DisplayName { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000DCD7 File Offset: 0x0000BED7
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000DCDF File Offset: 0x0000BEDF
		[Parameter(Mandatory = false)]
		public string Fax { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		[Parameter(Mandatory = false)]
		public string FirstName { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000DCF9 File Offset: 0x0000BEF9
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000DD01 File Offset: 0x0000BF01
		[Parameter(Mandatory = false)]
		public string HomePhone { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000DD0A File Offset: 0x0000BF0A
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000DD12 File Offset: 0x0000BF12
		[Parameter(Mandatory = false)]
		public string Initials { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000DD1B File Offset: 0x0000BF1B
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000DD23 File Offset: 0x0000BF23
		[Parameter(Mandatory = false)]
		public string LastName { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000DD34 File Offset: 0x0000BF34
		[Parameter(Mandatory = false)]
		public string MobilePhone { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000DD3D File Offset: 0x0000BF3D
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000DD45 File Offset: 0x0000BF45
		[Parameter(Mandatory = false)]
		public string Notes { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000DD4E File Offset: 0x0000BF4E
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000DD56 File Offset: 0x0000BF56
		[Parameter(Mandatory = false)]
		public string Office { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000DD5F File Offset: 0x0000BF5F
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000DD67 File Offset: 0x0000BF67
		[Parameter(Mandatory = false)]
		public string Phone { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000DD70 File Offset: 0x0000BF70
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000DD78 File Offset: 0x0000BF78
		[Parameter(Mandatory = false)]
		public string PostalCode { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000DD81 File Offset: 0x0000BF81
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000DD89 File Offset: 0x0000BF89
		[Parameter(Mandatory = false)]
		public string StateOrProvince { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000DD92 File Offset: 0x0000BF92
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000DD9A File Offset: 0x0000BF9A
		[Parameter(Mandatory = false)]
		public string StreetAddress { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000DDA3 File Offset: 0x0000BFA3
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000DDAB File Offset: 0x0000BFAB
		[Parameter(Mandatory = false)]
		public string Title { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000DDB4 File Offset: 0x0000BFB4
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		[Parameter(Mandatory = false)]
		public string WebPage { get; set; }

		// Token: 0x0600031D RID: 797 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		protected override void InternalProcessRecord()
		{
			try
			{
				SetUserCmdlet setUserCmdlet = new SetUserCmdlet();
				ADObjectId executingUserId;
				base.ExchangeRunspaceConfig.TryGetExecutingUserId(out executingUserId);
				setUserCmdlet.Authenticator = Authenticator.Create(base.CurrentOrganizationId, executingUserId);
				setUserCmdlet.HostServerName = EOPRecipient.GetPsWsHostServerName();
				if (string.IsNullOrEmpty(this.ExternalDirectoryObjectId) && this.Identity == null)
				{
					base.ThrowTaskError(new ArgumentException(CoreStrings.MissingIdentityParameter.ToString()));
				}
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.Identity, string.IsNullOrEmpty(this.ExternalDirectoryObjectId) ? this.Identity.ToString() : this.ExternalDirectoryObjectId);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.DisplayName, this.DisplayName);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.FirstName, this.FirstName);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.LastName, this.LastName);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.Initials, this.Initials);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.City, this.City);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.Office, this.Office);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.StateOrProvince, this.StateOrProvince);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.PostalCode, this.PostalCode);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.CountryOrRegion, this.CountryOrRegion);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.Phone, this.Phone);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.Notes, this.Notes);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.Title, this.Title);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.Department, this.Department);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.Company, this.Company);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.StreetAddress, this.StreetAddress);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.MobilePhone, this.MobilePhone);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.Fax, this.Fax);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.HomePhone, this.HomePhone);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.WebPage, this.WebPage);
				EOPRecipient.SetProperty(setUserCmdlet, Parameters.Organization, base.Organization);
				setUserCmdlet.Run();
				EOPRecipient.CheckForError(this, setUserCmdlet);
			}
			catch (Exception e)
			{
				base.ThrowAndLogTaskError(e);
			}
		}
	}
}
