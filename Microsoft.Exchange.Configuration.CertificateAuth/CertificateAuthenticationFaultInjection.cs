using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.CertificateAuthentication;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Configuration.CertificateAuthentication
{
	// Token: 0x02000004 RID: 4
	public class CertificateAuthenticationFaultInjection
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002C94 File Offset: 0x00000E94
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (CertificateAuthenticationFaultInjection.faultInjectionTracer == null)
				{
					lock (CertificateAuthenticationFaultInjection.lockObject)
					{
						if (CertificateAuthenticationFaultInjection.faultInjectionTracer == null)
						{
							FaultInjectionTrace faultInjectionTrace = ExTraceGlobals.FaultInjectionTracer;
							faultInjectionTrace.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(CertificateAuthenticationFaultInjection.Callback));
							CertificateAuthenticationFaultInjection.faultInjectionTracer = faultInjectionTrace;
						}
					}
				}
				return CertificateAuthenticationFaultInjection.faultInjectionTracer;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002D00 File Offset: 0x00000F00
		public static Exception Callback(string exceptionType)
		{
			Exception result = null;
			if (exceptionType != null)
			{
				if (typeof(ADTransientException).FullName.Equals(exceptionType))
				{
					return new ADTransientException(new LocalizedString("fault injection!"));
				}
				if (typeof(ApplicationException).FullName.Equals(exceptionType))
				{
					return new ApplicationException(new LocalizedString("fault injection!"));
				}
			}
			return result;
		}

		// Token: 0x04000013 RID: 19
		private static object lockObject = new object();

		// Token: 0x04000014 RID: 20
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
