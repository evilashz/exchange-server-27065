using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Dsn
{
	// Token: 0x02000093 RID: 147
	[Cmdlet("New", "SystemMessage", DefaultParameterSetName = "Dsn", SupportsShouldProcess = true)]
	public sealed class NewSystemMessage : NewFixedNameSystemConfigurationObjectTask<SystemMessage>
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001465A File Offset: 0x0001285A
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x00014671 File Offset: 0x00012871
		[Parameter(Mandatory = true, ParameterSetName = "Dsn")]
		public EnhancedStatusCode DsnCode
		{
			get
			{
				return (EnhancedStatusCode)base.Fields["DsnCode"];
			}
			set
			{
				base.Fields["DsnCode"] = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00014684 File Offset: 0x00012884
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0001469B File Offset: 0x0001289B
		[Parameter(Mandatory = true, ParameterSetName = "Quota")]
		public QuotaMessageType QuotaMessageType
		{
			get
			{
				return (QuotaMessageType)base.Fields["QuotaMessageType"];
			}
			set
			{
				base.Fields["QuotaMessageType"] = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x000146B3 File Offset: 0x000128B3
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x000146C0 File Offset: 0x000128C0
		[Parameter(Mandatory = true, ParameterSetName = "Quota")]
		[Parameter(Mandatory = true, ParameterSetName = "Dsn")]
		public string Text
		{
			get
			{
				return this.DataObject.Text;
			}
			set
			{
				this.DataObject.Text = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x000146CE File Offset: 0x000128CE
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x000146E5 File Offset: 0x000128E5
		[Parameter(Mandatory = true, ParameterSetName = "Dsn")]
		public bool Internal
		{
			get
			{
				return (bool)base.Fields["Internal"];
			}
			set
			{
				base.Fields["Internal"] = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x000146FD File Offset: 0x000128FD
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x00014714 File Offset: 0x00012914
		[Parameter(Mandatory = true, ParameterSetName = "Dsn")]
		[Parameter(Mandatory = true, ParameterSetName = "Quota")]
		public CultureInfo Language
		{
			get
			{
				return (CultureInfo)base.Fields["Language"];
			}
			set
			{
				base.Fields["Language"] = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00014728 File Offset: 0x00012928
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.DsnCode != null)
				{
					return Strings.ConfirmationMessageNewSystemMessageDsn(this.DsnCode.ToString(), this.Text, this.Internal.ToString(), this.Language.ToString());
				}
				return Strings.ConfirmationMessageNewSystemMessageQuota(this.QuotaMessageType.ToString(), this.Text, this.Language.ToString());
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001479C File Offset: 0x0001299C
		protected override ObjectId RootId
		{
			get
			{
				ADObjectId currentOrgContainerId = base.CurrentOrgContainerId;
				return SystemMessage.GetDsnCustomizationContainer(currentOrgContainerId);
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000147B8 File Offset: 0x000129B8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			ADObjectId adobjectId = this.RootId as ADObjectId;
			ADObjectId childId = adobjectId.GetChildId(this.Language.LCID.ToString(NumberFormatInfo.InvariantInfo));
			ADObjectId childId3;
			if (this.DsnCode != null)
			{
				ADObjectId childId2 = childId.GetChildId(this.Internal ? "internal" : "external");
				childId3 = childId2.GetChildId(this.DsnCode.Value);
			}
			else
			{
				childId3 = childId.GetChildId(this.QuotaMessageType.ToString());
			}
			this.DataObject.SetId(childId3);
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x040001C0 RID: 448
		private const string DsnParameterSetName = "Dsn";

		// Token: 0x040001C1 RID: 449
		private const string QuotaParameterSetName = "Quota";
	}
}
