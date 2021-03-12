using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006F6 RID: 1782
	[Serializable]
	public abstract class DistributionGroupBase : MailEnabledRecipient
	{
		// Token: 0x060053D0 RID: 21456 RVA: 0x001312CB File Offset: 0x0012F4CB
		protected DistributionGroupBase()
		{
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x001312D3 File Offset: 0x0012F4D3
		protected DistributionGroupBase(ADObject dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001BBC RID: 7100
		// (get) Token: 0x060053D2 RID: 21458 RVA: 0x001312DC File Offset: 0x0012F4DC
		// (set) Token: 0x060053D3 RID: 21459 RVA: 0x001312EE File Offset: 0x0012F4EE
		public string ExpansionServer
		{
			get
			{
				return (string)this[DistributionGroupBaseSchema.ExpansionServer];
			}
			set
			{
				this[DistributionGroupBaseSchema.ExpansionServer] = value;
			}
		}

		// Token: 0x17001BBD RID: 7101
		// (get) Token: 0x060053D4 RID: 21460 RVA: 0x001312FC File Offset: 0x0012F4FC
		// (set) Token: 0x060053D5 RID: 21461 RVA: 0x0013130E File Offset: 0x0012F50E
		[Parameter(Mandatory = false)]
		public bool ReportToManagerEnabled
		{
			get
			{
				return (bool)this[DistributionGroupBaseSchema.ReportToManagerEnabled];
			}
			set
			{
				this[DistributionGroupBaseSchema.ReportToManagerEnabled] = value;
			}
		}

		// Token: 0x17001BBE RID: 7102
		// (get) Token: 0x060053D6 RID: 21462 RVA: 0x00131321 File Offset: 0x0012F521
		// (set) Token: 0x060053D7 RID: 21463 RVA: 0x00131333 File Offset: 0x0012F533
		[Parameter(Mandatory = false)]
		public bool ReportToOriginatorEnabled
		{
			get
			{
				return (bool)this[DistributionGroupBaseSchema.ReportToOriginatorEnabled];
			}
			set
			{
				this[DistributionGroupBaseSchema.ReportToOriginatorEnabled] = value;
			}
		}

		// Token: 0x17001BBF RID: 7103
		// (get) Token: 0x060053D8 RID: 21464 RVA: 0x00131346 File Offset: 0x0012F546
		// (set) Token: 0x060053D9 RID: 21465 RVA: 0x00131358 File Offset: 0x0012F558
		[Parameter(Mandatory = false)]
		public bool SendOofMessageToOriginatorEnabled
		{
			get
			{
				return (bool)this[DistributionGroupBaseSchema.SendOofMessageToOriginatorEnabled];
			}
			set
			{
				this[DistributionGroupBaseSchema.SendOofMessageToOriginatorEnabled] = value;
			}
		}
	}
}
