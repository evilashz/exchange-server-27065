using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000426 RID: 1062
	[Cmdlet("Get", "MailboxAuditBypassAssociation", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxAuditBypassAssociation : GetRecipientBase<MailboxAuditBypassAssociationIdParameter, ADRecipient>
	{
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x00093AC2 File Offset: 0x00091CC2
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x00093AC5 File Offset: 0x00091CC5
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MailboxAuditBypassAssociationSchema>();
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x00093ACC File Offset: 0x00091CCC
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00093AD0 File Offset: 0x00091CD0
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			ADRecipient dataObject2 = (ADRecipient)dataObject;
			return MailboxAuditBypassAssociation.FromDataObject(dataObject2);
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x00093AEA File Offset: 0x00091CEA
		internal new OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x00093AED File Offset: 0x00091CED
		internal new string Anr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x00093AF0 File Offset: 0x00091CF0
		internal new SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return new SwitchParameter(false);
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x00093AF8 File Offset: 0x00091CF8
		internal new PSCredential Credential
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x00093AFB File Offset: 0x00091CFB
		internal new string Filter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x00093AFE File Offset: 0x00091CFE
		internal new string SortBy
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x00093B01 File Offset: 0x00091D01
		internal new SwitchParameter ReadFromDomainController
		{
			get
			{
				return new SwitchParameter(false);
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x00093B09 File Offset: 0x00091D09
		// (set) Token: 0x06002503 RID: 9475 RVA: 0x00093B10 File Offset: 0x00091D10
		internal new AccountPartitionIdParameter AccountPartition
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
