using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.PowerShell.RbacHostingTools.Asp.Net;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000068 RID: 104
	public abstract class DataSourceService
	{
		// Token: 0x06001A81 RID: 6785 RVA: 0x00054655 File Offset: 0x00052855
		protected DataSourceService() : this(DataSourceService.UserRunspaces)
		{
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00054662 File Offset: 0x00052862
		protected DataSourceService(RunspaceMediator runspaceMediator)
		{
			this.runspaceMediator = runspaceMediator;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00054674 File Offset: 0x00052874
		protected PowerShellResults<L> GetList<L, F>(string getCmdlet, F filter, SortOptions sort, string defaultSortProperty) where F : WebServiceParameters, new()
		{
			PSCommand psCommand = new PSCommand().AddCommand(getCmdlet);
			return this.GetList<L, F>(psCommand, filter, sort, defaultSortProperty);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00054698 File Offset: 0x00052898
		protected PowerShellResults<L> GetList<L, F>(PSCommand psCommand, F filter, SortOptions sort, string defaultSortProperty) where F : WebServiceParameters, new()
		{
			if (string.IsNullOrEmpty(defaultSortProperty))
			{
				throw new FaultException(new ArgumentNullException("defaultSortProperty").Message);
			}
			if (sort == null)
			{
				sort = new SortOptions();
				sort.Direction = SortDirection.Ascending;
				sort.PropertyName = defaultSortProperty;
			}
			return this.GetList<L, F>(psCommand, filter, sort);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000546E8 File Offset: 0x000528E8
		protected PowerShellResults<L> GetList<L, F>(string getCmdlet, F filter, SortOptions sort) where F : WebServiceParameters, new()
		{
			PSCommand psCommand = new PSCommand().AddCommand(getCmdlet);
			return this.GetList<L, F>(psCommand, filter, sort);
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0005470C File Offset: 0x0005290C
		protected PowerShellResults<L> GetList<L, F>(PSCommand psCommand, F filter, SortOptions sort) where F : WebServiceParameters, new()
		{
			EcpPerfCounters.WebServiceGetList.Increment();
			Func<L[], L[]> func = (sort != null) ? sort.GetSortFunction<L>() : null;
			F f;
			if ((f = filter) == null)
			{
				f = Activator.CreateInstance<F>();
			}
			filter = f;
			PowerShellResults<L> powerShellResults = this.CoreInvoke<L>(psCommand, null, null, filter);
			if (func != null)
			{
				powerShellResults.Output = func(powerShellResults.Output);
			}
			ResultSizeFilter resultSizeFilter = filter as ResultSizeFilter;
			if (resultSizeFilter != null && powerShellResults.HasWarnings)
			{
				for (int i = 0; i < powerShellResults.Warnings.Length; i++)
				{
					if (powerShellResults.Warnings[i] == Strings.WarningMoreResultsAvailable || powerShellResults.Warnings[i] == Strings.WarningDefaultResultSizeReached(resultSizeFilter.ResultSize.ToString()))
					{
						powerShellResults.Warnings[i] = ClientStrings.ListViewMoreResultsWarning;
					}
				}
			}
			return powerShellResults;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000547E0 File Offset: 0x000529E0
		protected PowerShellResults<O> GetObject<O>(string getCmdlet, Identity identity)
		{
			return this.GetObject<O>(new PSCommand().AddCommand(getCmdlet), identity);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x000547F4 File Offset: 0x000529F4
		protected PowerShellResults<L> GetObjectForList<L>(string getCmdlet, Identity identity)
		{
			return this.GetObject<L>(getCmdlet, identity);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x000547FE File Offset: 0x000529FE
		protected PowerShellResults<L> GetObjectForList<L>(PSCommand psCommand, Identity identity)
		{
			return this.GetObject<L>(psCommand, identity);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00054808 File Offset: 0x00052A08
		protected PowerShellResults<O> GetObject<O>(PSCommand psCommand, Identity identity)
		{
			EcpPerfCounters.WebServiceGetObject.Increment();
			identity.FaultIfNull();
			PowerShellResults<O> powerShellResults = this.CoreInvoke<O>(psCommand, identity.ToPipelineInput(), identity, null);
			if (powerShellResults.Output.Length > 1)
			{
				throw new SecurityException(Strings.ErrorManagementObjectAmbiguous(identity.DisplayName));
			}
			return powerShellResults;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00054858 File Offset: 0x00052A58
		protected PowerShellResults<O> GetObject<O>(string getCmdlet)
		{
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand(getCmdlet);
			return this.GetObject<O>(pscommand);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x0005487C File Offset: 0x00052A7C
		protected PowerShellResults<O> GetObject<O>(PSCommand psCommand)
		{
			EcpPerfCounters.WebServiceGetObject.Increment();
			return psCommand.Invoke(this.runspaceMediator, null, null);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000548A4 File Offset: 0x00052AA4
		protected PowerShellResults Invoke(PSCommand psCommand)
		{
			return this.Invoke(psCommand, Identity.FromExecutingUserId(), null);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000548D0 File Offset: 0x00052AD0
		protected PowerShellResults InvokeAsync(PSCommand psCommand, Action<PowerShellResults> onCompleted)
		{
			return AsyncServiceManager.InvokeAsync(() => this.Invoke(psCommand), onCompleted, RbacPrincipal.Current.UniqueName, AsyncTaskType.Default, psCommand.ToTraceString());
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00054919 File Offset: 0x00052B19
		[PrincipalPermission(SecurityAction.Demand, Role = "*")]
		public PowerShellResults<JsonDictionary<object>> GetProgress(string progressId)
		{
			return AsyncServiceManager.GetProgress(progressId);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00054921 File Offset: 0x00052B21
		protected PowerShellResults Invoke(PSCommand psCommand, Identity translationIdentity, WebServiceParameters parameters)
		{
			if (null == translationIdentity)
			{
				throw new ArgumentNullException("translationIdentity");
			}
			return this.CoreInvoke(psCommand, null, translationIdentity, parameters);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00054944 File Offset: 0x00052B44
		protected PowerShellResults Invoke(PSCommand psCommand, Identity[] identities, WebServiceParameters parameters)
		{
			identities.FaultIfNullOrEmpty();
			Identity translationIdentity = (identities.Length == 1) ? identities[0] : null;
			return this.CoreInvoke(psCommand, identities.ToIdParameters(), translationIdentity, parameters);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000549A0 File Offset: 0x00052BA0
		protected PowerShellResults<O> InvokeAndGetObject<O>(PSCommand psCommand, Identity[] identities, WebServiceParameters parameters) where O : BaseRow
		{
			PowerShellResults<O> powerShellResults = new PowerShellResults<O>();
			powerShellResults.MergeErrors(this.Invoke(psCommand, identities, parameters));
			if (powerShellResults.Succeeded && identities.Length <= 5)
			{
				IGetObjectForListService<O> getObjectForListService = this as IGetObjectForListService<O>;
				Func<Identity, PowerShellResults<O>> func;
				if (getObjectForListService != null)
				{
					func = ((Identity x) => getObjectForListService.GetObjectForList(x));
				}
				else
				{
					IGetObjectService<O> getObjectService = this as IGetObjectService<O>;
					if (getObjectService == null)
					{
						throw new Exception("Either IGetObjectForListService or IGetObjectService must be implemented for single row refresh.");
					}
					func = ((Identity x) => getObjectService.GetObject(x));
				}
				PowerShellResults<O> powerShellResults2 = new PowerShellResults<O>();
				try
				{
					for (int i = 0; i < identities.Length; i++)
					{
						powerShellResults2.MergeAll(func(identities[i]));
						if (powerShellResults2.Failed)
						{
							break;
						}
					}
				}
				catch (SecurityException)
				{
					if (powerShellResults2.HasValue)
					{
						throw;
					}
				}
				if (powerShellResults2.SucceededWithValue)
				{
					powerShellResults.MergeAll(powerShellResults2);
				}
			}
			return powerShellResults;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00054ABC File Offset: 0x00052CBC
		protected PowerShellResults<O> Invoke<O>(PSCommand psCommand)
		{
			return this.CoreInvoke<O>(psCommand, null, null, null);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00054AC8 File Offset: 0x00052CC8
		protected PowerShellResults<L> NewObject<L, C>(string newCmdlet, C properties) where C : WebServiceParameters
		{
			EcpPerfCounters.WebServiceNewObject.Increment();
			properties.FaultIfNull();
			return this.CoreInvoke<L>(new PSCommand().AddCommand(newCmdlet), null, null, properties);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00054AFC File Offset: 0x00052CFC
		protected PowerShellResults RemoveObjects(string removeCmdlet, Identity identity, Identity[] identities, string parameterNameForIdentities, WebServiceParameters parameters)
		{
			EcpPerfCounters.WebServiceRemoveObject.Increment();
			PowerShellResults powerShellResults = new PowerShellResults();
			foreach (Identity identity2 in identities)
			{
				PSCommand psCommand = new PSCommand().AddCommand(removeCmdlet).AddParameter("Identity", identity).AddParameter(parameterNameForIdentities, identity2.RawIdentity);
				powerShellResults.MergeErrors(this.Invoke(psCommand, identity, parameters));
			}
			return powerShellResults;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00054B69 File Offset: 0x00052D69
		protected PowerShellResults RemoveObjects(string removeCmdlet, Identity[] identities, WebServiceParameters parameters)
		{
			return this.RemoveObjects(new PSCommand().AddCommand(removeCmdlet), identities, parameters);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00054B7E File Offset: 0x00052D7E
		protected PowerShellResults RemoveObjects(PSCommand psCommand, Identity[] identities, WebServiceParameters parameters)
		{
			EcpPerfCounters.WebServiceRemoveObject.Increment();
			return this.Invoke(psCommand, identities, parameters);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00054B94 File Offset: 0x00052D94
		protected PowerShellResults<O> SetObject<O, U>(string setCmdlet, Identity identity, U properties) where U : SetObjectProperties
		{
			return this.SetObject<O, U, O>(setCmdlet, identity, properties, identity);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x00054BA0 File Offset: 0x00052DA0
		protected PowerShellResults<L> SetObject<O, U, L>(string setCmdlet, Identity identity, U properties) where O : L where U : SetObjectProperties
		{
			return this.SetObject<O, U, L>(setCmdlet, identity, properties, identity);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x00054BAC File Offset: 0x00052DAC
		protected PowerShellResults<L> SetObject<O, U, L>(string setCmdlet, Identity identity, U properties, Identity identityForGetCmdlet) where O : L where U : SetObjectProperties
		{
			EcpPerfCounters.WebServiceSetObject.Increment();
			identity.FaultIfNull();
			properties.FaultIfNull();
			PowerShellResults<L> powerShellResults = new PowerShellResults<L>();
			properties.IgnoreNullOrEmpty = false;
			if (properties.Any<KeyValuePair<string, object>>())
			{
				powerShellResults = this.CoreInvoke<L>(new PSCommand().AddCommand(setCmdlet), identity.ToPipelineInput(), identity, properties);
			}
			if (powerShellResults.Succeeded && null != identityForGetCmdlet)
			{
				PowerShellResults<L> powerShellResults2 = null;
				if (properties.ReturnObjectType == ReturnObjectTypes.Full && this is IGetObjectService<O>)
				{
					IGetObjectService<O> getObjectService = this as IGetObjectService<O>;
					PowerShellResults<O> @object = getObjectService.GetObject(identityForGetCmdlet);
					powerShellResults2 = new PowerShellResults<L>();
					powerShellResults2.MergeOutput(@object.Output.Cast<L>().ToArray<L>());
					powerShellResults2.MergeErrors<O>(@object);
				}
				else if (properties.ReturnObjectType == ReturnObjectTypes.PartialForList && this is IGetObjectForListService<L>)
				{
					IGetObjectForListService<L> getObjectForListService = this as IGetObjectForListService<L>;
					powerShellResults2 = getObjectForListService.GetObjectForList(identityForGetCmdlet);
				}
				if (powerShellResults2 != null)
				{
					powerShellResults.MergeAll(powerShellResults2);
				}
			}
			return powerShellResults;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00054CB3 File Offset: 0x00052EB3
		private PowerShellResults CoreInvoke(PSCommand psCommand, IEnumerable pipelineInput, Identity translationIdentity, WebServiceParameters parameters)
		{
			return new PowerShellResults(this.CoreInvoke<PSObject>(psCommand, pipelineInput, translationIdentity, parameters));
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00054CC8 File Offset: 0x00052EC8
		private PowerShellResults<T> CoreInvoke<T>(PSCommand psCommand, IEnumerable pipelineInput, Identity translationIdentity, WebServiceParameters parameters)
		{
			PowerShellResults<T> powerShellResults = psCommand.Invoke(this.runspaceMediator, pipelineInput, parameters);
			powerShellResults.TranslationIdentity = translationIdentity;
			return powerShellResults;
		}

		// Token: 0x04001B18 RID: 6936
		private const int SingleRowRefreshThreshold = 5;

		// Token: 0x04001B19 RID: 6937
		public static RunspaceMediator UserRunspaces = new RunspaceMediator(new EcpRunspaceFactory(new RbacInitialSessionStateFactory(), EcpHost.Factory), new EcpRunspaceCache());

		// Token: 0x04001B1A RID: 6938
		private RunspaceMediator runspaceMediator;
	}
}
