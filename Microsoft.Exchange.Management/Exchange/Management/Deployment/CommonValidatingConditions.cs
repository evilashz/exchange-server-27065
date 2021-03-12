using System;
using System.ComponentModel;
using System.ServiceProcess;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200017F RID: 383
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CommonValidatingConditions
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00040AE8 File Offset: 0x0003ECE8
		public static ValidatingCondition StoreServiceExistsAndIsRunning
		{
			get
			{
				return new ValidatingCondition(new ValidationDelegate(CommonValidatingConditions.StoreServiceExistsAndIsRunningCheck), Strings.VerifyStoreServiceExists, false);
			}
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00040B04 File Offset: 0x0003ED04
		private static bool StoreServiceExistsAndIsRunningCheck()
		{
			bool flag = false;
			using (ServiceController serviceController = new ServiceController("MSExchangeIS"))
			{
				try
				{
					ServiceControllerStatus status = serviceController.Status;
					flag = true;
				}
				catch (InvalidOperationException ex)
				{
					Win32Exception ex2 = ex.InnerException as Win32Exception;
					if (ex2 == null || 1060 != ex2.NativeErrorCode)
					{
						throw;
					}
					flag = false;
				}
			}
			if (!flag)
			{
				throw new LocalizedException(Strings.ExceptionServiceDoesNotExist("MSExchangeIS"));
			}
			bool result;
			using (ServiceController serviceController2 = new ServiceController("MSExchangeIS"))
			{
				if (serviceController2.Status != ServiceControllerStatus.Running)
				{
					throw new LocalizedException(Strings.ExceptionServiceIsNotRunning("MSExchangeIS"));
				}
				result = true;
			}
			return result;
		}
	}
}
