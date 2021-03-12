using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AE8 RID: 2792
	public abstract class BaseOrganization : SetMultitenancySingletonSystemConfigurationObjectTask<ADOrganizationConfig>
	{
		// Token: 0x17001E11 RID: 7697
		// (get) Token: 0x06006324 RID: 25380 RVA: 0x0019E67D File Offset: 0x0019C87D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetOrganizationConfig;
			}
		}

		// Token: 0x17001E12 RID: 7698
		// (get) Token: 0x06006325 RID: 25381 RVA: 0x0019E684 File Offset: 0x0019C884
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E13 RID: 7699
		// (get) Token: 0x06006326 RID: 25382 RVA: 0x0019E687 File Offset: 0x0019C887
		// (set) Token: 0x06006327 RID: 25383 RVA: 0x0019E69E File Offset: 0x0019C89E
		[Parameter(Mandatory = false)]
		public int SCLJunkThreshold
		{
			get
			{
				return (int)base.Fields["SCLJunkThreshold"];
			}
			set
			{
				base.Fields["SCLJunkThreshold"] = value;
			}
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x0019E6B8 File Offset: 0x0019C8B8
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			Organization organization = (Organization)dataObject;
			if (base.Fields.IsModified("SCLJunkThreshold"))
			{
				this.uceContentFilter = this.FindUceContentFilter(organization.Identity);
				if (this.uceContentFilter != null)
				{
					organization.SCLJunkThreshold = this.uceContentFilter.SCLJunkThreshold;
					organization.ResetChangeTracking();
				}
				else
				{
					base.WriteVerbose(Strings.UceContentFilterObjectNotFound);
				}
			}
			base.StampChangesOn(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x0019E730 File Offset: 0x0019C930
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			Organization organization = (Organization)base.PrepareDataObject();
			if (base.Fields.IsModified("SCLJunkThreshold") && this.uceContentFilter != null)
			{
				organization.SCLJunkThreshold = this.SCLJunkThreshold;
				this.uceContentFilter.SCLJunkThreshold = this.SCLJunkThreshold;
			}
			TaskLogger.LogExit();
			return organization;
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x0019E78B File Offset: 0x0019C98B
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.Fields.IsModified("SCLJunkThreshold") && this.uceContentFilter != null)
			{
				base.DataSession.Save(this.uceContentFilter);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x0019E7C8 File Offset: 0x0019C9C8
		private UceContentFilter FindUceContentFilter(ObjectId rootId)
		{
			UceContentFilter result = null;
			IConfigurable[] array = base.DataSession.Find<UceContentFilter>(null, rootId, true, null);
			if (array != null && array.Length > 0)
			{
				result = (array[0] as UceContentFilter);
			}
			return result;
		}

		// Token: 0x040035E9 RID: 13801
		private const string SCLJunkThresholdParameterName = "SCLJunkThreshold";

		// Token: 0x040035EA RID: 13802
		private UceContentFilter uceContentFilter;
	}
}
