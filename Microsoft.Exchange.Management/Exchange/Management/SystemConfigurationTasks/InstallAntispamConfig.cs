using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A9A RID: 2714
	public abstract class InstallAntispamConfig<TDataObject> : NewFixedNameSystemConfigurationObjectTask<TDataObject> where TDataObject : MessageHygieneAgentConfig, new()
	{
		// Token: 0x17001D1D RID: 7453
		// (get) Token: 0x06006040 RID: 24640
		protected abstract string CanonicalName { get; }

		// Token: 0x06006041 RID: 24641 RVA: 0x0019153C File Offset: 0x0018F73C
		protected override IConfigurable PrepareDataObject()
		{
			TDataObject tdataObject = (TDataObject)((object)base.PrepareDataObject());
			tdataObject.SetId((IConfigurationSession)base.DataSession, this.CanonicalName);
			return tdataObject;
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x0019157C File Offset: 0x0018F77C
		protected override void InternalProcessRecord()
		{
			TDataObject[] array = this.ConfigurationSession.Find<TDataObject>(base.RootOrgContainerId, QueryScope.SubTree, null, null, 2);
			if (array == null || array.Length == 0)
			{
				base.InternalProcessRecord();
			}
		}
	}
}
