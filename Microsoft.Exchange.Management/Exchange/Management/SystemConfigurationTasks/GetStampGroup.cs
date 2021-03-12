using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008A9 RID: 2217
	[Cmdlet("Get", "StampGroup")]
	public sealed class GetStampGroup : GetSystemConfigurationObjectTask<StampGroupIdParameter, StampGroup>
	{
		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x06004E39 RID: 20025 RVA: 0x00144700 File Offset: 0x00142900
		// (set) Token: 0x06004E3A RID: 20026 RVA: 0x00144726 File Offset: 0x00142926
		[Parameter]
		public SwitchParameter Status
		{
			get
			{
				return (SwitchParameter)(base.Fields["Status"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Status"] = value;
			}
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x0014473E File Offset: 0x0014293E
		protected override bool IsKnownException(Exception e)
		{
			return DagTaskHelper.IsKnownException(this, e) || base.IsKnownException(e);
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x00144752 File Offset: 0x00142952
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 72, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Cluster\\GetStampGroup.cs");
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x0014477C File Offset: 0x0014297C
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			StampGroup dataObject2 = (StampGroup)dataObject;
			base.WriteResult(dataObject2);
			TaskLogger.LogExit();
		}

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x06004E3E RID: 20030 RVA: 0x001447B6 File Offset: 0x001429B6
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
