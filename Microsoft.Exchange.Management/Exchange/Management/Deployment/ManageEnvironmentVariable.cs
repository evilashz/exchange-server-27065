using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000200 RID: 512
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public abstract class ManageEnvironmentVariable : Task
	{
		// Token: 0x06001175 RID: 4469 RVA: 0x0004CD66 File Offset: 0x0004AF66
		public ManageEnvironmentVariable()
		{
			this.Target = EnvironmentVariableTarget.Process;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0004CD75 File Offset: 0x0004AF75
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x0004CD8C File Offset: 0x0004AF8C
		[Parameter(Mandatory = true)]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0004CD9F File Offset: 0x0004AF9F
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x0004CDB6 File Offset: 0x0004AFB6
		[Parameter(Mandatory = false)]
		public EnvironmentVariableTarget Target
		{
			get
			{
				return (EnvironmentVariableTarget)base.Fields["Target"];
			}
			set
			{
				base.Fields["Target"] = value;
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0004CDD0 File Offset: 0x0004AFD0
		protected void SetVariable(string name, string value, EnvironmentVariableTarget target)
		{
			try
			{
				Environment.SetEnvironmentVariable(name, value, target);
			}
			catch (ArgumentNullException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (ArgumentException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
			}
			catch (NotSupportedException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
			}
			catch (SecurityException exception4)
			{
				base.WriteError(exception4, ErrorCategory.SecurityError, null);
			}
		}
	}
}
