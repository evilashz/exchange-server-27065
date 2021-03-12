using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200001F RID: 31
	internal static class ExceptionHelper
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003CF7 File Offset: 0x00001EF7
		public static bool HandleLeakedException
		{
			get
			{
				return Components.TransportAppConfig.WorkerProcess.HandleLeakedException;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003D08 File Offset: 0x00001F08
		public static bool IsHandleableException(Exception e)
		{
			return ExceptionHelper.IsHandleablePermanentException(e) || ExceptionHelper.IsHandleableTransientException(e);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003D1C File Offset: 0x00001F1C
		public static bool IsHandleableTransientCtsException(Exception e)
		{
			if (ExceptionHelper.IsHandleablePermanentException(e))
			{
				return true;
			}
			IOException ex = e as IOException;
			return ex != null && (ExceptionHelper.IsDiskFullException(ex) || ExceptionHelper.IsUnspecificErrorException(ex));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003D4F File Offset: 0x00001F4F
		public static bool IsHandleablePermanentCtsException(Exception e)
		{
			return false;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003D52 File Offset: 0x00001F52
		public static bool IsHandleablePermanentException(Exception e)
		{
			return e is ExchangeDataException;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003D60 File Offset: 0x00001F60
		public static bool IsHandleableTransientException(Exception e)
		{
			if (e is DataSourceTransientException)
			{
				return true;
			}
			if (e is AddressBookTransientException)
			{
				return true;
			}
			if (e is ExchangeConfigurationException)
			{
				return true;
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Transport.ADExceptionHandling.Enabled)
			{
				if (e is DataSourceOperationException)
				{
					return true;
				}
				if (e is DataValidationException)
				{
					return true;
				}
			}
			IOException ex = e as IOException;
			return ex != null && (ExceptionHelper.IsDiskFullException(ex) || ExceptionHelper.IsUnspecificErrorException(ex));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003DDB File Offset: 0x00001FDB
		internal static bool IsDiskFullException(IOException exception)
		{
			return ExceptionHelper.IsSpecificIoException(exception, 2147942512U);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003DE8 File Offset: 0x00001FE8
		internal static bool IsUnspecificErrorException(IOException exception)
		{
			return ExceptionHelper.IsSpecificIoException(exception, 2147500037U);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003DF8 File Offset: 0x00001FF8
		internal static bool IsSpecificIoException(IOException exception, uint errorCode)
		{
			for (Exception ex = exception; ex != null; ex = ex.InnerException)
			{
				if (ex.GetType() == typeof(IOException))
				{
					uint hrforException = (uint)Marshal.GetHRForException(ex);
					if (hrforException == errorCode)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003E38 File Offset: 0x00002038
		internal static void StopServiceOnFatalError(string reason)
		{
			Components.StopService(reason, false, false, false);
		}

		// Token: 0x0400004E RID: 78
		private const uint Win32ErrorDiskFull = 2147942512U;

		// Token: 0x0400004F RID: 79
		private const uint Win32UnspecifiedError = 2147500037U;
	}
}
