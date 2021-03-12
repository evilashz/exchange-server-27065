using System;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.FfoQuarantine
{
	// Token: 0x0200002A RID: 42
	[Cmdlet("Get", "QuarantineMessageHeader")]
	[OutputType(new Type[]
	{
		typeof(QuarantineMessageHeader)
	})]
	public sealed class GetQuarantineMessageHeader : Task
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000576A File Offset: 0x0000396A
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00005772 File Offset: 0x00003972
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public string Identity { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000577B File Offset: 0x0000397B
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00005783 File Offset: 0x00003983
		[Parameter(ValueFromPipelineByPropertyName = true)]
		public OrganizationIdParameter Organization { get; set; }

		// Token: 0x0600011D RID: 285 RVA: 0x0000578C File Offset: 0x0000398C
		protected override void InternalProcessRecord()
		{
			SystemProbe.Trace(GetQuarantineMessageHeader.ComponentName, SystemProbe.Status.Pass, "Entering InternalProcessRecord", new object[0]);
			try
			{
				Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.ManagementHelper");
				Type type = assembly.GetType("Microsoft.Exchange.Hygiene.ManagementHelper.FfoQuarantine.GetQuarantineMessageHeaderHelper");
				MethodInfo method = type.GetMethod("InternalProcessRecordHelper", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(GetQuarantineMessageHeader)
				}, null);
				method.Invoke(null, new object[]
				{
					this
				});
			}
			catch (TargetInvocationException ex)
			{
				SystemProbe.Trace(GetQuarantineMessageHeader.ComponentName, SystemProbe.Status.Fail, "TargetInvocationException in InternalProcessRecord: {0}", new object[]
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
				SystemProbe.Trace(GetQuarantineMessageHeader.ComponentName, SystemProbe.Status.Fail, "Unhandled Exception in InternalProcessRecord: {0}", new object[]
				{
					ex2.ToString()
				});
				throw;
			}
			SystemProbe.Trace(GetQuarantineMessageHeader.ComponentName, SystemProbe.Status.Pass, "Exiting InternalProcessRecord", new object[0]);
		}

		// Token: 0x04000065 RID: 101
		private static readonly string ComponentName = "GetQuarantineMessageHeader";
	}
}
