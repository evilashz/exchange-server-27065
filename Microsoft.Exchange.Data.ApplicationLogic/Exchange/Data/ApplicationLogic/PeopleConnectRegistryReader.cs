using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000181 RID: 385
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PeopleConnectRegistryReader
	{
		// Token: 0x06000F1B RID: 3867 RVA: 0x0003D118 File Offset: 0x0003B318
		private PeopleConnectRegistryReader()
		{
			this.DogfoodInEnterprise = false;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0003D128 File Offset: 0x0003B328
		public static PeopleConnectRegistryReader Read()
		{
			PeopleConnectRegistryReader result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PeopleConnect"))
				{
					if (registryKey == null)
					{
						result = new PeopleConnectRegistryReader();
					}
					else
					{
						result = new PeopleConnectRegistryReader
						{
							DogfoodInEnterprise = Convert.ToBoolean((int)registryKey.GetValue("DogfoodInEnterprise", 0))
						};
					}
				}
			}
			catch (SecurityException e)
			{
				result = PeopleConnectRegistryReader.TraceErrorAndReturnEmptyConfiguration(e);
			}
			catch (IOException e2)
			{
				result = PeopleConnectRegistryReader.TraceErrorAndReturnEmptyConfiguration(e2);
			}
			catch (UnauthorizedAccessException e3)
			{
				result = PeopleConnectRegistryReader.TraceErrorAndReturnEmptyConfiguration(e3);
			}
			return result;
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0003D1E0 File Offset: 0x0003B3E0
		// (set) Token: 0x06000F1E RID: 3870 RVA: 0x0003D1E8 File Offset: 0x0003B3E8
		public bool DogfoodInEnterprise { get; private set; }

		// Token: 0x06000F1F RID: 3871 RVA: 0x0003D1F1 File Offset: 0x0003B3F1
		private static PeopleConnectRegistryReader TraceErrorAndReturnEmptyConfiguration(Exception e)
		{
			PeopleConnectRegistryReader.Tracer.TraceError<Exception>(0L, "PeopleConnectRegistryReader.Read: caught exception {0}", e);
			return new PeopleConnectRegistryReader();
		}

		// Token: 0x040007F7 RID: 2039
		private const string DogfoodInEnterpriseValueName = "DogfoodInEnterprise";

		// Token: 0x040007F8 RID: 2040
		private const string PeopleConnectKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PeopleConnect";

		// Token: 0x040007F9 RID: 2041
		private static readonly Trace Tracer = ExTraceGlobals.PeopleConnectConfigurationTracer;
	}
}
