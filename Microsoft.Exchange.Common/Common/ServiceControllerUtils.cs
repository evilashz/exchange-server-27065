using System;
using System.ComponentModel;
using System.ServiceProcess;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000058 RID: 88
	internal static class ServiceControllerUtils
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00008970 File Offset: 0x00006B70
		internal static bool IsInstalled(string serviceName)
		{
			bool result;
			using (ServiceController serviceController = new ServiceController(serviceName))
			{
				result = ServiceControllerUtils.IsInstalled(serviceController);
			}
			return result;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000089A8 File Offset: 0x00006BA8
		internal static bool IsInstalled(ServiceController serviceController)
		{
			if (serviceController == null)
			{
				throw new ArgumentNullException("serviceController");
			}
			bool result = false;
			try
			{
				ServiceControllerStatus status = serviceController.Status;
				result = true;
			}
			catch (InvalidOperationException ex)
			{
				Win32Exception ex2 = ex.InnerException as Win32Exception;
				if (ex2 != null && (1060 == ex2.NativeErrorCode || 1072 == ex2.NativeErrorCode))
				{
					result = false;
				}
				else
				{
					if (ex2 == null || 1058 != ex2.NativeErrorCode)
					{
						throw;
					}
					result = true;
				}
			}
			return result;
		}
	}
}
