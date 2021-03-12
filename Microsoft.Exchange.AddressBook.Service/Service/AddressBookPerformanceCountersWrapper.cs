using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200003C RID: 60
	internal static class AddressBookPerformanceCountersWrapper
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000115DF File Offset: 0x0000F7DF
		public static IAddressBookPerformanceCounters AddressBookPerformanceCounters
		{
			get
			{
				return AddressBookPerformanceCountersWrapper.addressBookPerformanceCounters;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000115E6 File Offset: 0x0000F7E6
		public static void Initialize(IAddressBookPerformanceCounters addressBookPerformanceCounters)
		{
			AddressBookPerformanceCountersWrapper.addressBookPerformanceCounters = addressBookPerformanceCounters;
			AddressBookPerformanceCountersWrapper.InitializeCounters(AddressBookPerformanceCountersWrapper.addressBookPerformanceCounters);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000115F8 File Offset: 0x0000F7F8
		private static void InitializeCounters(object performanceCounters)
		{
			Type typeFromHandle = typeof(IExPerformanceCounter);
			foreach (PropertyInfo propertyInfo in performanceCounters.GetType().GetProperties())
			{
				if (typeFromHandle.IsAssignableFrom(propertyInfo.PropertyType))
				{
					IExPerformanceCounter exPerformanceCounter = propertyInfo.GetValue(performanceCounters, null) as IExPerformanceCounter;
					if (exPerformanceCounter != null)
					{
						exPerformanceCounter.RawValue = 0L;
					}
				}
			}
		}

		// Token: 0x04000180 RID: 384
		private static IAddressBookPerformanceCounters addressBookPerformanceCounters = new NullAddressBookPerformanceCounters();
	}
}
