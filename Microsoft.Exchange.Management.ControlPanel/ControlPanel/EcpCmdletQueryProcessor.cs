using System;
using System.Security;
using System.Web;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000379 RID: 889
	internal abstract class EcpCmdletQueryProcessor : RbacQuery.RbacQueryProcessor
	{
		// Token: 0x06003043 RID: 12355 RVA: 0x0009333C File Offset: 0x0009153C
		public sealed override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			bool? flag = null;
			if (HttpContext.Current.Items.Contains(base.GetType()))
			{
				flag = (bool?)HttpContext.Current.Items[base.GetType()];
			}
			else
			{
				try
				{
					flag = this.IsInRoleCmdlet(rbacConfiguration);
				}
				catch (SecurityException)
				{
					flag = null;
				}
				HttpContext.Current.Items[base.GetType()] = flag;
			}
			return flag;
		}

		// Token: 0x06003044 RID: 12356
		internal abstract bool? IsInRoleCmdlet(ExchangeRunspaceConfiguration rbacConfiguration);

		// Token: 0x06003045 RID: 12357 RVA: 0x000933C8 File Offset: 0x000915C8
		internal void LogCmdletError(PowerShellResults results, string roleName)
		{
			string text = string.Empty;
			if (results.ErrorRecords.Length > 0)
			{
				text = results.ErrorRecords[0].ToString();
			}
			EcpEventLogConstants.Tuple_UnableToDetectRbacRoleViaCmdlet.LogEvent(new object[]
			{
				EcpEventLogExtensions.GetUserNameToLog(),
				roleName,
				text
			});
		}

		// Token: 0x17001F2D RID: 7981
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x00093416 File Offset: 0x00091616
		public sealed override bool CanCache
		{
			get
			{
				return false;
			}
		}
	}
}
