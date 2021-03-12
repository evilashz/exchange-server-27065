using System;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000229 RID: 553
	[Cmdlet("Remove", "SetupFile")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class RemoveSetupFile : Task
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0005271F File Offset: 0x0005091F
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x00052736 File Offset: 0x00050936
		[Parameter(Mandatory = true)]
		public LongPath FilePath
		{
			get
			{
				return (LongPath)base.Fields["FilePath"];
			}
			set
			{
				base.Fields["FilePath"] = value;
			}
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0005274C File Offset: 0x0005094C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			TaskLogger.Log(Strings.RemovingFile(this.FilePath.PathName));
			try
			{
				File.Delete(this.FilePath.PathName);
			}
			catch (SecurityException exception)
			{
				base.WriteError(exception, ErrorCategory.SecurityError, null);
			}
			catch (UnauthorizedAccessException exception2)
			{
				base.WriteError(exception2, ErrorCategory.PermissionDenied, null);
			}
			catch (IOException exception3)
			{
				base.WriteError(exception3, ErrorCategory.ResourceUnavailable, null);
			}
			catch (Win32Exception exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}
	}
}
