using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000080 RID: 128
	internal abstract class ResultIdGenerator<TResult> where TResult : WorkItemResult, IPersistence, new()
	{
		// Token: 0x060006DD RID: 1757 RVA: 0x0001CCDC File Offset: 0x0001AEDC
		static ResultIdGenerator()
		{
			List<SecurityIdentifier> list = new List<SecurityIdentifier>();
			list.Add(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null));
			list.Add(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null));
			list.Add(new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null));
			list.Add(new SecurityIdentifier(WellKnownSidType.LocalServiceSid, null));
			SecurityIdentifier securityIdentifier = null;
			try
			{
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					if (current.User != null)
					{
						securityIdentifier = current.User.AccountDomainSid;
					}
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError(WTFLog.Core, TracingContext.Default, ex.ToString(), null, ".cctor", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\ResultIdGenerator.cs", 90);
			}
			if (securityIdentifier != null)
			{
				list.Add(new SecurityIdentifier(WellKnownSidType.AccountAdministratorSid, securityIdentifier));
				list.Add(new SecurityIdentifier(WellKnownSidType.AccountEnterpriseAdminsSid, securityIdentifier));
			}
			ResultIdGenerator<TResult>.mutexSecurity = new MutexSecurity();
			foreach (SecurityIdentifier identity in list)
			{
				MutexAccessRule rule = new MutexAccessRule(identity, MutexRights.Modify | MutexRights.Synchronize, AccessControlType.Allow);
				ResultIdGenerator<TResult>.mutexSecurity.AddAccessRule(rule);
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001CE38 File Offset: 0x0001B038
		public ResultIdGenerator()
		{
			try
			{
				this.Counter.IncrementBy(0L);
			}
			catch (InvalidOperationException)
			{
				this.counterExists = false;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060006DF RID: 1759
		protected abstract ExPerformanceCounter Counter { get; }

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001CE7C File Offset: 0x0001B07C
		public int NextId()
		{
			if (!this.counterExists)
			{
				return 0;
			}
			if (this.Counter.RawValue == 0L)
			{
				bool flag;
				using (Mutex mutex = new Mutex(true, ResultIdGenerator<TResult>.mutexName, ref flag, ResultIdGenerator<TResult>.mutexSecurity))
				{
					if (!flag)
					{
						try
						{
							flag = mutex.WaitOne(1000);
						}
						catch (AbandonedMutexException)
						{
						}
					}
					try
					{
						if (this.Counter.RawValue == 0L)
						{
							using (CrimsonReader<TResult> crimsonReader = new CrimsonReader<TResult>())
							{
								TResult tresult = crimsonReader.ReadLast();
								if (tresult != null)
								{
									return (int)this.Counter.IncrementBy((long)(tresult.ResultId + 1000));
								}
							}
						}
					}
					finally
					{
						if (flag)
						{
							mutex.ReleaseMutex();
						}
					}
				}
			}
			return (int)this.Counter.Increment();
		}

		// Token: 0x0400045B RID: 1115
		private const int CounterJump = 1000;

		// Token: 0x0400045C RID: 1116
		private readonly bool counterExists = true;

		// Token: 0x0400045D RID: 1117
		private static string mutexName = typeof(TResult).Name + "{0AA95102-2F00-48A8-B1FC-8E1A42569BC8}";

		// Token: 0x0400045E RID: 1118
		private static MutexSecurity mutexSecurity;
	}
}
