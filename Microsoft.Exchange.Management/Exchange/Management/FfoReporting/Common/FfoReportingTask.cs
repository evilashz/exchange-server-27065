using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Providers;
using Microsoft.Exchange.Management.ReportingTask;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x0200038E RID: 910
	public abstract class FfoReportingTask<TOutputObject> : Task where TOutputObject : new()
	{
		// Token: 0x06001FB3 RID: 8115 RVA: 0x00087CF4 File Offset: 0x00085EF4
		public FfoReportingTask()
		{
			this.outputObjectTypeName = typeof(TOutputObject).Name.ToString();
			this.Diagnostics = new Diagnostics(this.ComponentName);
			this.configDataProvider = new Lazy<IConfigDataProvider>(() => ServiceLocator.Current.GetService<IAuthenticationProvider>().CreateConfigSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId));
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x00087D50 File Offset: 0x00085F50
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x00087D67 File Offset: 0x00085F67
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

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x00087D7A File Offset: 0x00085F7A
		// (set) Token: 0x06001FB7 RID: 8119 RVA: 0x00087D82 File Offset: 0x00085F82
		[Parameter(Mandatory = false)]
		public Expression Expression { get; set; }

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x00087D8B File Offset: 0x00085F8B
		// (set) Token: 0x06001FB9 RID: 8121 RVA: 0x00087D93 File Offset: 0x00085F93
		[Parameter(Mandatory = false)]
		public string ProbeTag
		{
			get
			{
				return this.probeTag;
			}
			set
			{
				this.Diagnostics.ActivityId = value;
				this.probeTag = value;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x00087DA8 File Offset: 0x00085FA8
		// (set) Token: 0x06001FBB RID: 8123 RVA: 0x00087DB0 File Offset: 0x00085FB0
		internal Diagnostics Diagnostics { get; private set; }

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x00087DB9 File Offset: 0x00085FB9
		internal IConfigDataProvider ConfigSession
		{
			get
			{
				return this.configDataProvider.Value;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001FBD RID: 8125
		public abstract string ComponentName { get; }

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001FBE RID: 8126
		public abstract string MonitorEventName { get; }

		// Token: 0x06001FBF RID: 8127 RVA: 0x00087DC8 File Offset: 0x00085FC8
		protected override void InternalValidate()
		{
			base.InternalValidate();
			try
			{
				FaultInjection.FaultInjectionTracer.TraceTest(3355847997U);
				this.Authenticate();
				this.ValidateParameters();
			}
			catch (InvalidExpressionException ex)
			{
				this.Diagnostics.TraceError(string.Format("ValidationError-{0}", ex.Message));
				base.WriteError(ex, ErrorCategory.InvalidArgument, null);
			}
			catch (Exception exception)
			{
				this.Diagnostics.TraceException(string.Format("{0} validation failed", this.outputObjectTypeName), exception);
				base.WriteError(new FfoReportingException(), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00087E68 File Offset: 0x00086068
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
				FaultInjection.FaultInjectionTracer.TraceTest(3070635325U);
				IReadOnlyList<TOutputObject> readOnlyList = this.AggregateOutput();
				this.Diagnostics.Checkpoint("AggregateOutput");
				try
				{
					this.Diagnostics.StartTimer("WriteObject");
					foreach (!0 ! in readOnlyList)
					{
						object sendToPipeline = !;
						base.WriteObject(sendToPipeline);
					}
				}
				finally
				{
					this.Diagnostics.StopTimer("WriteObject");
				}
				if (readOnlyList.Count == 0)
				{
					this.PostProcessRecordValidation();
				}
				this.Diagnostics.SetHealthGreen(this.MonitorEventName, string.Format("{0} completed", this.outputObjectTypeName));
			}
			catch (InvalidExpressionException exception)
			{
				this.Diagnostics.SetHealthGreen(this.MonitorEventName, string.Format("{0} completed with validation error", this.outputObjectTypeName));
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (Exception ex)
			{
				this.Diagnostics.SetHealthRed(this.MonitorEventName, string.Format("{0} failed: {1}", this.outputObjectTypeName, ex.ToString()), ex);
				base.WriteError(new FfoReportingException(), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06001FC1 RID: 8129
		protected abstract IReadOnlyList<TOutputObject> AggregateOutput();

		// Token: 0x06001FC2 RID: 8130 RVA: 0x00087FC4 File Offset: 0x000861C4
		protected virtual void OnAuthenticateOrganization()
		{
			ServiceLocator.Current.GetService<IAuthenticationProvider>().ResolveOrganizationId(this.Organization, this);
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00087FDC File Offset: 0x000861DC
		protected void Authenticate()
		{
			try
			{
				this.Diagnostics.StartTimer("Authentication");
				if (this.Organization == null)
				{
					if (base.CurrentOrganizationId.OrganizationalUnit == null)
					{
						throw new InvalidExpressionException(Strings.InvalidOrganization);
					}
					this.Organization = new OrganizationIdParameter(base.CurrentOrganizationId.OrganizationalUnit.Name);
				}
				this.OnAuthenticateOrganization();
			}
			catch (ManagementObjectNotFoundException innerException)
			{
				throw new InvalidExpressionException(Strings.InvalidOrganization, innerException);
			}
			finally
			{
				this.Diagnostics.StopTimer("Authentication");
			}
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00088078 File Offset: 0x00086278
		protected virtual void CustomInternalValidate()
		{
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x00088084 File Offset: 0x00086284
		private void PostProcessRecordValidation()
		{
			this.Diagnostics.Checkpoint("PostProcessRecordValidation");
			Schema.Utilities.ValidateParameters(this, () => this.ConfigSession, new HashSet<CmdletValidator.ValidatorTypes>
			{
				CmdletValidator.ValidatorTypes.Postprocessing,
				CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession
			});
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x000880D4 File Offset: 0x000862D4
		private void ValidateParameters()
		{
			try
			{
				this.Diagnostics.StartTimer("Validation");
				FfoExpressionVisitor<TOutputObject>.Parse(this.Expression, this);
				Schema.Utilities.ValidateParameters(this, () => this.ConfigSession, new HashSet<CmdletValidator.ValidatorTypes>
				{
					CmdletValidator.ValidatorTypes.Preprocessing
				});
				this.CustomInternalValidate();
			}
			finally
			{
				this.Diagnostics.StopTimer("Validation");
			}
		}

		// Token: 0x04001994 RID: 6548
		private readonly string outputObjectTypeName;

		// Token: 0x04001995 RID: 6549
		private string probeTag;

		// Token: 0x04001996 RID: 6550
		private Lazy<IConfigDataProvider> configDataProvider;
	}
}
