using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003E0 RID: 992
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MailboxSearches : DataSourceService, IMailboxSearches, IDataSourceService<MailboxSearchFilter, MailboxSearchRow, MailboxSearch, SetMailboxSearchParameters, NewMailboxSearchParameters>, IDataSourceService<MailboxSearchFilter, MailboxSearchRow, MailboxSearch, SetMailboxSearchParameters, NewMailboxSearchParameters, BaseWebServiceParameters>, IEditListService<MailboxSearchFilter, MailboxSearchRow, MailboxSearch, NewMailboxSearchParameters, BaseWebServiceParameters>, IGetListService<MailboxSearchFilter, MailboxSearchRow>, INewObjectService<MailboxSearchRow, NewMailboxSearchParameters>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<MailboxSearch, SetMailboxSearchParameters, MailboxSearchRow>, IGetObjectService<MailboxSearch>, IGetObjectForListService<MailboxSearchRow>
	{
		// Token: 0x06003326 RID: 13094 RVA: 0x0009E7BD File Offset: 0x0009C9BD
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxSearch?Identity")]
		public PowerShellResults<MailboxSearchRow> GetList(MailboxSearchFilter filter, SortOptions sort)
		{
			return base.GetList<MailboxSearchRow, MailboxSearchFilter>("Get-MailboxSearch", filter, sort, "LastModifiedUTCDateTime");
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x0009E7D1 File Offset: 0x0009C9D1
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxSearch?Identity")]
		public PowerShellResults<MailboxSearch> GetObject(Identity identity)
		{
			return base.GetObject<MailboxSearch>("Get-MailboxSearch", identity);
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x0009E7DF File Offset: 0x0009C9DF
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxSearch?Identity")]
		public PowerShellResults<MailboxSearchRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<MailboxSearchRow>("Get-MailboxSearch", identity);
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x0009E7ED File Offset: 0x0009C9ED
		[PrincipalPermission(SecurityAction.Demand, Role = "New-MailboxSearch?StartDate&EndDate&SourceMailboxes&Language")]
		public PowerShellResults<MailboxSearchRow> NewObject(NewMailboxSearchParameters properties)
		{
			return base.NewObject<MailboxSearchRow, NewMailboxSearchParameters>("New-MailboxSearch", properties);
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x0009E7FB File Offset: 0x0009C9FB
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-MailboxSearch?Identity")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-MailboxSearch", identities, parameters);
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x0009E80A File Offset: 0x0009CA0A
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxSearch?Identity+Set-MailboxSearch?Identity")]
		public PowerShellResults<MailboxSearchRow> SetObject(Identity identity, SetMailboxSearchParameters properties)
		{
			return base.SetObject<MailboxSearch, SetMailboxSearchParameters, MailboxSearchRow>("Set-MailboxSearch", identity, properties);
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x0009E81C File Offset: 0x0009CA1C
		[PrincipalPermission(SecurityAction.Demand, Role = "Start-MailboxSearch?Identity")]
		public PowerShellResults<MailboxSearchRow> StartSearch(Identity[] identities, StartMailboxSearchParameters parameters)
		{
			List<Identity> list = new List<Identity>();
			if (parameters != null && parameters.Resume)
			{
				list.AddRange(identities);
			}
			else
			{
				foreach (Identity identity in identities)
				{
					PowerShellResults<MailboxSearch> @object = base.GetObject<MailboxSearch>("Get-MailboxSearch", identity);
					if (@object.Succeeded && @object.HasValue)
					{
						if (!@object.Output[0].IsEstimateOnly)
						{
							PSCommand pscommand = new PSCommand().AddCommand("Set-MailboxSearch");
							pscommand.AddParameter("Identity", identity);
							pscommand.AddParameter("EstimateOnly", true);
							pscommand.AddParameter("ExcludeDuplicateMessages", false);
							pscommand.AddParameter("LogLevel", LoggingLevel.Suppress);
							pscommand.AddParameter("Force", true);
							PowerShellResults powerShellResults = base.Invoke(pscommand);
							if (!powerShellResults.Succeeded)
							{
								break;
							}
						}
						list.Add(identity);
					}
				}
			}
			return base.InvokeAndGetObject<MailboxSearchRow>(new PSCommand().AddCommand("Start-MailboxSearch"), list.ToArray(), parameters);
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x0009E93D File Offset: 0x0009CB3D
		[PrincipalPermission(SecurityAction.Demand, Role = "Stop-MailboxSearch?Identity")]
		public PowerShellResults<MailboxSearchRow> StopSearch(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.InvokeAndGetObject<MailboxSearchRow>(new PSCommand().AddCommand("Stop-MailboxSearch"), identities, parameters);
		}

		// Token: 0x040024A2 RID: 9378
		internal const string GetCmdlet = "Get-MailboxSearch";

		// Token: 0x040024A3 RID: 9379
		internal const string NewCmdlet = "New-MailboxSearch";

		// Token: 0x040024A4 RID: 9380
		internal const string RemoveCmdlet = "Remove-MailboxSearch";

		// Token: 0x040024A5 RID: 9381
		internal const string SetCmdlet = "Set-MailboxSearch";

		// Token: 0x040024A6 RID: 9382
		internal const string StartCmdlet = "Start-MailboxSearch";

		// Token: 0x040024A7 RID: 9383
		internal const string StopCmdlet = "Stop-MailboxSearch";

		// Token: 0x040024A8 RID: 9384
		private const string GetListRole = "Get-MailboxSearch";

		// Token: 0x040024A9 RID: 9385
		private const string GetObjectRole = "Get-MailboxSearch?Identity";

		// Token: 0x040024AA RID: 9386
		private const string NewObjectRole = "New-MailboxSearch?StartDate&EndDate&SourceMailboxes&Language";

		// Token: 0x040024AB RID: 9387
		private const string RemoveObjectRole = "Remove-MailboxSearch?Identity";

		// Token: 0x040024AC RID: 9388
		private const string SetObjectRole = "Get-MailboxSearch?Identity+Set-MailboxSearch?Identity";

		// Token: 0x040024AD RID: 9389
		private const string StartSearchRole = "Start-MailboxSearch?Identity";

		// Token: 0x040024AE RID: 9390
		private const string StopSearchRole = "Stop-MailboxSearch?Identity";
	}
}
