using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.FfoReporting.Data
{
	// Token: 0x02000400 RID: 1024
	internal static class DataProcessorDriver
	{
		// Token: 0x06002414 RID: 9236 RVA: 0x000902C0 File Offset: 0x0008E4C0
		internal static IReadOnlyList<TOutputObject> Process<TOutputObject>(IEnumerable inputs, IDataProcessor processor)
		{
			return DataProcessorDriver.Process<TOutputObject>(inputs, new IDataProcessor[]
			{
				processor
			});
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000902E0 File Offset: 0x0008E4E0
		internal static IReadOnlyList<TOutputObject> Process<TOutputObject>(IEnumerable inputs, IEnumerable<IDataProcessor> groupOfProcessors)
		{
			List<TOutputObject> list = new List<TOutputObject>();
			if (inputs != null)
			{
				IEnumerable<IDataProcessor> enumerable = (groupOfProcessors != null) ? groupOfProcessors : new List<IDataProcessor>();
				foreach (object obj in inputs)
				{
					object obj2 = obj;
					foreach (IDataProcessor dataProcessor in enumerable)
					{
						obj2 = dataProcessor.Process(obj2);
					}
					list.Add((TOutputObject)((object)obj2));
				}
			}
			return list;
		}
	}
}
