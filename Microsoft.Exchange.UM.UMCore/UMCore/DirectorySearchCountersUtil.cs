using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000118 RID: 280
	internal class DirectorySearchCountersUtil : DirectoryAccessCountersUtil
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x0001FE9D File Offset: 0x0001E09D
		internal DirectorySearchCountersUtil(BaseUMCallSession vo) : base(vo)
		{
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001FEA8 File Offset: 0x0001E0A8
		protected override ExPerformanceCounter GetSingleCounter(DirectoryAccessCountersUtil.DirectoryAccessCounter perfcounter)
		{
			ExPerformanceCounter result;
			switch (perfcounter)
			{
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedByExtension:
				result = SubscriberAccessCounters.DirectoryAccessedByExtension;
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedByDialByName:
				result = SubscriberAccessCounters.DirectoryAccessedByDialByName;
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedSuccessfullyByDialByName:
				result = SubscriberAccessCounters.DirectoryAccessedSuccessfullyByDialByName;
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedBySpokenName:
				result = SubscriberAccessCounters.DirectoryAccessedBySpokenName;
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedSuccessfullyBySpokenName:
				result = SubscriberAccessCounters.DirectoryAccessedSuccessfullyBySpokenName;
				break;
			default:
				throw new InvalidPerfCounterException(perfcounter.ToString());
			}
			return result;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001FF10 File Offset: 0x0001E110
		protected override List<ExPerformanceCounter> GetCounters(DirectoryAccessCountersUtil.DirectoryAccessCounter counter)
		{
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			switch (counter)
			{
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedByExtension:
				list.Add(SubscriberAccessCounters.DirectoryAccessedByExtension);
				list.Add(SubscriberAccessCounters.DirectoryAccessed);
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedByDialByName:
				list.Add(SubscriberAccessCounters.DirectoryAccessedByDialByName);
				list.Add(SubscriberAccessCounters.DirectoryAccessed);
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedSuccessfullyByDialByName:
				list.Add(SubscriberAccessCounters.DirectoryAccessedSuccessfullyByDialByName);
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedBySpokenName:
				list.Add(SubscriberAccessCounters.DirectoryAccessedBySpokenName);
				list.Add(SubscriberAccessCounters.DirectoryAccessed);
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedSuccessfullyBySpokenName:
				list.Add(SubscriberAccessCounters.DirectoryAccessedSuccessfullyBySpokenName);
				break;
			default:
				throw new InvalidPerfCounterException(counter.ToString());
			}
			return list;
		}
	}
}
