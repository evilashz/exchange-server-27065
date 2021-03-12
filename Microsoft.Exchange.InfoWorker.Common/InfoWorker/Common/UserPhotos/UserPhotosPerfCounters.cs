using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.UserPhotos
{
	// Token: 0x0200031D RID: 797
	internal static class UserPhotosPerfCounters
	{
		// Token: 0x0600176C RID: 5996 RVA: 0x0006E8D4 File Offset: 0x0006CAD4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (UserPhotosPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in UserPhotosPerfCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04000FA2 RID: 4002
		public const string CategoryName = "MSExchange UserPhotos";

		// Token: 0x04000FA3 RID: 4003
		public static readonly ExPerformanceCounter UserPhotosCurrentRequests = new ExPerformanceCounter("MSExchange UserPhotos", "UserPhotos Current Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000FA4 RID: 4004
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			UserPhotosPerfCounters.UserPhotosCurrentRequests
		};
	}
}
