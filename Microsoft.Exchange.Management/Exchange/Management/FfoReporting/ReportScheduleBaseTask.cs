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
	// Token: 0x020003F1 RID: 1009
	public abstract class ReportScheduleBaseTask : Task
	{
		// Token: 0x060023A4 RID: 9124 RVA: 0x0008F8D8 File Offset: 0x0008DAD8
		protected ReportScheduleBaseTask(string componentName, string assemblyType)
		{
			this.componentName = componentName;
			this.assemblyType = assemblyType;
			this.configDataProvider = new Lazy<IConfigDataProvider>(() => ServiceLocator.Current.GetService<IAuthenticationProvider>().CreateConfigSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId));
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x0008F92D File Offset: 0x0008DB2D
		// (set) Token: 0x060023A6 RID: 9126 RVA: 0x0008F944 File Offset: 0x0008DB44
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

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x0008F957 File Offset: 0x0008DB57
		internal IConfigDataProvider ConfigSession
		{
			get
			{
				return this.configDataProvider.Value;
			}
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x0008F964 File Offset: 0x0008DB64
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

		// Token: 0x060023A9 RID: 9129 RVA: 0x0008FA78 File Offset: 0x0008DC78
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

		// Token: 0x04001C72 RID: 7282
		private const string helperAssemblyName = "Microsoft.Exchange.Hygiene.ManagementHelper";

		// Token: 0x04001C73 RID: 7283
		private readonly string componentName = string.Empty;

		// Token: 0x04001C74 RID: 7284
		private readonly string assemblyType = string.Empty;

		// Token: 0x04001C75 RID: 7285
		private Lazy<IConfigDataProvider> configDataProvider;
	}
}
