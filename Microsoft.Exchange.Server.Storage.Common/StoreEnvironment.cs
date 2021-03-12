using System;
using System.Net.Sockets;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000088 RID: 136
	public class StoreEnvironment
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x000143DD File Offset: 0x000125DD
		private static StoreEnvironment.Values Instance
		{
			get
			{
				if (StoreEnvironment.values == null)
				{
					StoreEnvironment.values = new StoreEnvironment.Values();
				}
				return StoreEnvironment.values;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x000143F5 File Offset: 0x000125F5
		public static bool IsDatacenterEnvironment
		{
			get
			{
				return StoreEnvironment.Instance.IsDatacenterEnvironment;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00014401 File Offset: 0x00012601
		public static bool IsDedicatedEnvironment
		{
			get
			{
				return StoreEnvironment.Instance.IsDedicatedEnvironment;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001440D File Offset: 0x0001260D
		public static bool IsDogfoodEnvironment
		{
			get
			{
				return StoreEnvironment.Instance.IsDogfoodEnvironment;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00014419 File Offset: 0x00012619
		public static bool IsPerformanceEnvironment
		{
			get
			{
				return StoreEnvironment.Instance.IsPerformanceEnvironment;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00014425 File Offset: 0x00012625
		public static bool IsSdfEnvironment
		{
			get
			{
				return StoreEnvironment.Instance.IsSdfEnvironment;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00014431 File Offset: 0x00012631
		public static bool IsTestEnvironment
		{
			get
			{
				return StoreEnvironment.Instance.IsTestEnvironment;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001443D File Offset: 0x0001263D
		public static string MachineName
		{
			get
			{
				return StoreEnvironment.Instance.MachineName;
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00014449 File Offset: 0x00012649
		internal static void Reset()
		{
			StoreEnvironment.values = null;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00014454 File Offset: 0x00012654
		private static bool GetIsDatacenterDedicated()
		{
			bool result = false;
			try
			{
				result = Datacenter.IsDatacenterDedicated(true);
			}
			catch (CannotDetermineExchangeModeException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_CannotDetectEnvironment, new object[]
				{
					"Datacenter",
					ex
				});
			}
			return result;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000144AC File Offset: 0x000126AC
		private static bool GetExEnvironmentSetting(Func<bool> getter)
		{
			bool result = false;
			try
			{
				result = getter();
			}
			catch (SocketException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_CannotDetectEnvironment, new object[]
				{
					"ExEnvironment",
					ex
				});
			}
			return result;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00014504 File Offset: 0x00012704
		private static bool GetIsPerformanceEnvironment()
		{
			int value = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Exchange_Test\\v15", "PerformanceTestEnvironment", 0);
			return value != 0;
		}

		// Token: 0x0400067E RID: 1662
		private static StoreEnvironment.Values values;

		// Token: 0x02000089 RID: 137
		private class Values
		{
			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001453D File Offset: 0x0001273D
			public bool IsDatacenterEnvironment
			{
				get
				{
					if (this.isDatacenterEnvironment == null)
					{
						this.isDatacenterEnvironment = new bool?(Datacenter.IsRunningInExchangeDatacenter(false));
					}
					return this.isDatacenterEnvironment.Value;
				}
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06000750 RID: 1872 RVA: 0x00014568 File Offset: 0x00012768
			public bool IsDedicatedEnvironment
			{
				get
				{
					if (this.isDedicatedEnvironment == null)
					{
						this.isDedicatedEnvironment = new bool?(StoreEnvironment.GetIsDatacenterDedicated());
					}
					return this.isDedicatedEnvironment.Value;
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001459C File Offset: 0x0001279C
			public bool IsDogfoodEnvironment
			{
				get
				{
					if (this.isDogfoodEnvironment == null)
					{
						this.isDogfoodEnvironment = new bool?(StoreEnvironment.GetExEnvironmentSetting(() => ExEnvironment.IsDogfoodDomain));
					}
					return this.isDogfoodEnvironment.Value;
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000752 RID: 1874 RVA: 0x000145EE File Offset: 0x000127EE
			public bool IsPerformanceEnvironment
			{
				get
				{
					if (this.isPerformanceTest == null)
					{
						this.isPerformanceTest = new bool?(StoreEnvironment.GetIsPerformanceEnvironment());
					}
					return this.isPerformanceTest.Value;
				}
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x06000753 RID: 1875 RVA: 0x00014620 File Offset: 0x00012820
			public bool IsSdfEnvironment
			{
				get
				{
					if (this.isSdfEnvironment == null)
					{
						this.isSdfEnvironment = new bool?(StoreEnvironment.GetExEnvironmentSetting(() => ExEnvironment.IsSdfDomain));
					}
					return this.isSdfEnvironment.Value;
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x06000754 RID: 1876 RVA: 0x00014684 File Offset: 0x00012884
			public bool IsTestEnvironment
			{
				get
				{
					if (this.isTestEnvironment == null)
					{
						this.isTestEnvironment = new bool?(StoreEnvironment.GetExEnvironmentSetting(() => ExEnvironment.IsTestDomain || ExEnvironment.IsTest));
					}
					return this.isTestEnvironment.Value;
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x06000755 RID: 1877 RVA: 0x000146D6 File Offset: 0x000128D6
			public string MachineName
			{
				get
				{
					if (this.machineName == null)
					{
						this.machineName = StoreEnvironment.Values.GetMachineName();
					}
					return this.machineName;
				}
			}

			// Token: 0x06000756 RID: 1878 RVA: 0x000146F1 File Offset: 0x000128F1
			public static string GetMachineName()
			{
				return Environment.MachineName;
			}

			// Token: 0x0400067F RID: 1663
			private bool? isDatacenterEnvironment;

			// Token: 0x04000680 RID: 1664
			private bool? isDedicatedEnvironment;

			// Token: 0x04000681 RID: 1665
			private bool? isDogfoodEnvironment;

			// Token: 0x04000682 RID: 1666
			private bool? isPerformanceTest;

			// Token: 0x04000683 RID: 1667
			private bool? isSdfEnvironment;

			// Token: 0x04000684 RID: 1668
			private bool? isTestEnvironment;

			// Token: 0x04000685 RID: 1669
			private string machineName;
		}
	}
}
