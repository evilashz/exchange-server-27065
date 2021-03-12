using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000AE2 RID: 2786
	internal class ServicePrincipalName
	{
		// Token: 0x06003BCD RID: 15309 RVA: 0x00099C89 File Offset: 0x00097E89
		public static int RegisterServiceClass(string serviceClass)
		{
			return ServicePrincipalName.DsServerRegisterSpn(NativeMethods.SpnWriteOperation.Add, serviceClass, null);
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x00099C93 File Offset: 0x00097E93
		public static int UnregisterServiceClass(string serviceClass)
		{
			return ServicePrincipalName.DsServerRegisterSpn(NativeMethods.SpnWriteOperation.Delete, serviceClass, null);
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x00099CA0 File Offset: 0x00097EA0
		public static int DsServerRegisterSpn(NativeMethods.SpnWriteOperation operation, string serviceClass, string userObjectDN)
		{
			if (string.IsNullOrEmpty(serviceClass))
			{
				throw new ArgumentNullException("serviceClass");
			}
			ExTraceGlobals.DirectoryServicesTracer.TraceDebug<NativeMethods.SpnWriteOperation, string, string>(0L, "DsServerRegisterSpn({0}, {1}, {2})", operation, serviceClass, userObjectDN ?? "(null)");
			int num = NativeMethods.DsServerRegisterSpn(operation, serviceClass, userObjectDN);
			if (num == 0)
			{
				return num;
			}
			string[] spns = null;
			num = ServicePrincipalName.GetFormattedSpns(serviceClass, out spns);
			if (num != 0)
			{
				ExTraceGlobals.DirectoryServicesTracer.TraceError<int>(0L, "Failed in GetFormattedSpns with status {0}", num);
				return num;
			}
			if (string.IsNullOrEmpty(userObjectDN))
			{
				num = ServicePrincipalName.GetComputerObjectDN(out userObjectDN);
				if (num != 0)
				{
					ExTraceGlobals.DirectoryServicesTracer.TraceError<int>(0L, "Failed in GetComputerObjectDN with status {0}", num);
					return num;
				}
			}
			string text = ServicePrincipalName.GetCurrentADDomainFromUserIdentity();
			if (string.IsNullOrEmpty(text))
			{
				text = ComputerInformation.DnsDomainName;
				ExTraceGlobals.DirectoryServicesTracer.TraceDebug(0L, "Could not locate current AD domain from user identity, using registry info");
			}
			ExTraceGlobals.DirectoryServicesTracer.TraceDebug<string>(0L, "Current AD domain is {0}", text);
			if (ServicePrincipalName.InternalDsServerRegisterSpn(operation, spns, text, userObjectDN) == 0)
			{
				return 0;
			}
			WindowsImpersonationContext windowsImpersonationContext = null;
			int result;
			try
			{
				try
				{
					using (AuthenticationContext authenticationContext = new AuthenticationContext())
					{
						SecurityStatus securityStatus = authenticationContext.LogonAsMachineAccount();
						if (securityStatus != SecurityStatus.OK)
						{
							result = (int)securityStatus;
						}
						else
						{
							windowsImpersonationContext = authenticationContext.Identity.Impersonate();
							result = ServicePrincipalName.InternalDsServerRegisterSpn(operation, spns, text, userObjectDN);
						}
					}
				}
				finally
				{
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
						windowsImpersonationContext.Dispose();
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x00099DFC File Offset: 0x00097FFC
		public override string ToString()
		{
			return this.spn;
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x00099E04 File Offset: 0x00098004
		public int FormatSpn(NativeMethods.SpnNameType spnNameType, string serviceClass)
		{
			uint num = 0U;
			this.spn = null;
			SafeSpnArrayHandle safeSpnArrayHandle;
			int num2 = NativeMethods.DsGetSpn(spnNameType, serviceClass, null, 0, 0, null, null, out num, out safeSpnArrayHandle);
			if (num2 != 0)
			{
				return num2;
			}
			if (safeSpnArrayHandle != null && !safeSpnArrayHandle.IsInvalid)
			{
				using (safeSpnArrayHandle)
				{
					if (num2 == 0 && num == 1U)
					{
						string[] spnStrings = safeSpnArrayHandle.GetSpnStrings(num);
						this.spn = spnStrings[0];
					}
					else
					{
						safeSpnArrayHandle.SetCount(num);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.spn))
			{
				return 0;
			}
			return 13;
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x00099E90 File Offset: 0x00098090
		public static int GetFormattedSpns(string serviceClass, out string[] spns)
		{
			spns = new string[2];
			int num = 0;
			ServicePrincipalName servicePrincipalName = new ServicePrincipalName();
			int num2 = servicePrincipalName.FormatSpn(NativeMethods.SpnNameType.DnsHost, serviceClass);
			if (num2 != 0)
			{
				ExTraceGlobals.DirectoryServicesTracer.TraceError<string, int>(0L, "Spn.FormatSpn DnsHost failed for {0} because of {1}", serviceClass, num2);
				return num2;
			}
			spns[num++] = servicePrincipalName.ToString();
			num2 = servicePrincipalName.FormatSpn(NativeMethods.SpnNameType.NetbiosHost, serviceClass);
			if (num2 == 0)
			{
				spns[num++] = servicePrincipalName.ToString();
				return num2;
			}
			ExTraceGlobals.DirectoryServicesTracer.TraceError<string, int>(0L, "Spn.FormatSpn NetbiosHost failed for {0} because of {1}", serviceClass, num2);
			return num2;
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x00099F14 File Offset: 0x00098114
		private static int InternalDsServerRegisterSpn(NativeMethods.SpnWriteOperation operation, string[] spns, string domain, string userObjectDN)
		{
			if (operation != NativeMethods.SpnWriteOperation.Add && operation != NativeMethods.SpnWriteOperation.Delete)
			{
				throw new NotSupportedException();
			}
			SafeDomainControllerInfoHandle safeDomainControllerInfoHandle;
			uint num = NativeMethods.DsGetDcName(null, domain, null, NativeMethods.DsGetDCNameFlags.WritableRequired, out safeDomainControllerInfoHandle);
			NativeMethods.DomainControllerInformation domainControllerInfo;
			using (safeDomainControllerInfoHandle)
			{
				if (num != 0U)
				{
					ExTraceGlobals.DirectoryServicesTracer.TraceError<uint>(0L, "DsGetDcName failed {0}", num);
					return (int)num;
				}
				domainControllerInfo = safeDomainControllerInfoHandle.GetDomainControllerInfo();
			}
			SafeDsHandle safeDsHandle;
			num = NativeMethods.DsBind(domainControllerInfo.DomainControllerName, null, out safeDsHandle);
			if (num != 0U)
			{
				ExTraceGlobals.DirectoryServicesTracer.TraceError<string, uint>(0L, "DsBind failed to connect to server {0}, the error code: {1} ", domainControllerInfo.DomainControllerName, num);
				return (int)num;
			}
			int result;
			using (safeDsHandle)
			{
				num = NativeMethods.DsWriteAccountSpn(safeDsHandle, operation, userObjectDN, (uint)spns.Length, spns);
				if (num != 0U)
				{
					ExTraceGlobals.DirectoryServicesTracer.TraceError<uint>(0L, "DsWriteAccountSpn failed {0}", num);
				}
				result = (int)num;
			}
			return result;
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x00099FF4 File Offset: 0x000981F4
		private static int GetComputerObjectDN(out string userObjectDN)
		{
			int num = 0;
			userObjectDN = null;
			StringBuilder stringBuilder = new StringBuilder(512);
			int capacity = stringBuilder.Capacity;
			if (!NativeMethods.GetComputerObjectName(NativeMethods.ExtendedNameFormat.FullyQualifiedDN, stringBuilder, ref capacity))
			{
				num = Marshal.GetLastWin32Error();
				if (num == 122)
				{
					stringBuilder.EnsureCapacity(capacity);
					if (!NativeMethods.GetComputerObjectName(NativeMethods.ExtendedNameFormat.FullyQualifiedDN, stringBuilder, ref capacity))
					{
						num = Marshal.GetLastWin32Error();
					}
					else
					{
						num = 0;
					}
				}
				if (num != 0)
				{
					ExTraceGlobals.DirectoryServicesTracer.TraceError<int>(0L, "GetComputerObjectName failed {0}", num);
					return num;
				}
			}
			userObjectDN = stringBuilder.ToString();
			return num;
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x0009A06C File Offset: 0x0009826C
		private static string GetCurrentADDomainFromUserIdentity()
		{
			string text = null;
			WindowsImpersonationContext windowsImpersonationContext = null;
			string result;
			try
			{
				try
				{
					for (int i = 1; i <= 2; i++)
					{
						if (i == 2)
						{
							using (AuthenticationContext authenticationContext = new AuthenticationContext())
							{
								SecurityStatus securityStatus = authenticationContext.LogonAsMachineAccount();
								if (securityStatus != SecurityStatus.OK)
								{
									ExTraceGlobals.DirectoryServicesTracer.TraceError<SecurityStatus>(0L, "Failed to impersonate machine account with status {0}", securityStatus);
									break;
								}
								if (windowsImpersonationContext != null)
								{
									windowsImpersonationContext.Dispose();
									windowsImpersonationContext = null;
								}
								windowsImpersonationContext = authenticationContext.Identity.Impersonate();
							}
						}
						using (WindowsIdentity current = WindowsIdentity.GetCurrent())
						{
							try
							{
								if (!current.User.IsWellKnown(WellKnownSidType.LocalSystemSid) && !current.User.IsWellKnown(WellKnownSidType.LocalServiceSid) && !current.User.IsWellKnown(WellKnownSidType.NetworkServiceSid))
								{
									ExTraceGlobals.DirectoryServicesTracer.TraceDebug<string>(0L, "Current identity: {0}", current.Name);
									int num = current.Name.IndexOfAny(ServicePrincipalName.domainDelimiters);
									if (num != -1)
									{
										if (current.Name[num] == '\\')
										{
											text = current.Name.Substring(0, num);
										}
										else
										{
											text = current.Name.Substring(num + 1);
										}
									}
									return text;
								}
							}
							catch (SystemException arg)
							{
								ExTraceGlobals.DirectoryServicesTracer.TraceError<SystemException>(0L, "Could not determine username, exception {0}", arg);
								break;
							}
						}
					}
					result = text;
				}
				finally
				{
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
						windowsImpersonationContext.Dispose();
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x040034AD RID: 13485
		private const int ErrorInvalidData = 13;

		// Token: 0x040034AE RID: 13486
		private static readonly char[] domainDelimiters = new char[]
		{
			'\\',
			'@'
		};

		// Token: 0x040034AF RID: 13487
		private string spn;
	}
}
