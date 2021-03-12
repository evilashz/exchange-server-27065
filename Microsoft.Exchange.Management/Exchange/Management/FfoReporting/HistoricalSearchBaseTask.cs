using System;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.FfoReporting.Providers;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003E8 RID: 1000
	public abstract class HistoricalSearchBaseTask : Task
	{
		// Token: 0x06002349 RID: 9033 RVA: 0x0008F1C0 File Offset: 0x0008D3C0
		protected HistoricalSearchBaseTask(string componentName, string assemblyType)
		{
			this.componentName = componentName;
			this.assemblyType = assemblyType;
			this.configDataProvider = new Lazy<IConfigDataProvider>(() => ServiceLocator.Current.GetService<IAuthenticationProvider>().CreateConfigSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId));
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x0008F215 File Offset: 0x0008D415
		// (set) Token: 0x0600234B RID: 9035 RVA: 0x0008F22C File Offset: 0x0008D42C
		[Parameter(Mandatory = false, Position = 0)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x0008F23F File Offset: 0x0008D43F
		internal IConfigDataProvider ConfigSession
		{
			get
			{
				return this.configDataProvider.Value;
			}
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0008F24C File Offset: 0x0008D44C
		protected sealed override void InternalProcessRecord()
		{
			SystemProbe.Trace(this.componentName, SystemProbe.Status.Pass, "Entering InternalProcessRecord", new object[0]);
			try
			{
				base.InternalProcessRecord();
				Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.ManagementHelper");
				Type type = assembly.GetType(this.assemblyType);
				MethodInfo method = type.GetMethod("InternalProcessRecordHelper", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					base.GetType()
				}, null);
				method.Invoke(null, new object[]
				{
					this
				});
			}
			catch (TargetInvocationException ex)
			{
				SystemProbe.Trace(this.componentName, SystemProbe.Status.Fail, "TargetInvocationException in InternalProcessRecord: {0}", new object[]
				{
					ex.ToString()
				});
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				throw;
			}
			catch (Exception ex2)
			{
				SystemProbe.Trace(this.componentName, SystemProbe.Status.Fail, "Unhandled Exception in InternalProcessRecord: {0}", new object[]
				{
					ex2.ToString()
				});
				throw;
			}
			SystemProbe.Trace(this.componentName, SystemProbe.Status.Pass, "Exiting InternalProcessRecord", new object[0]);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x0008F360 File Offset: 0x0008D560
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.Organization == null)
			{
				if (base.CurrentOrganizationId.OrganizationalUnit == null)
				{
					throw new ArgumentException(Strings.InvalidOrganization);
				}
				this.Organization = new OrganizationIdParameter(base.CurrentOrganizationId.OrganizationalUnit.Name);
			}
			ServiceLocator.Current.GetService<IAuthenticationProvider>().ResolveOrganizationId(this.Organization, this);
		}

		// Token: 0x04001C2E RID: 7214
		private const string helperAssemblyName = "Microsoft.Exchange.Hygiene.ManagementHelper";

		// Token: 0x04001C2F RID: 7215
		private readonly string componentName = string.Empty;

		// Token: 0x04001C30 RID: 7216
		private readonly string assemblyType = string.Empty;

		// Token: 0x04001C31 RID: 7217
		private Lazy<IConfigDataProvider> configDataProvider;
	}
}
