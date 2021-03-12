using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200009D RID: 157
	public sealed class StartupNotification : IPersistence
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00020126 File Offset: 0x0001E326
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0002012E File Offset: 0x0001E32E
		public LocalDataAccessMetaData LocalDataAccessMetaData { get; private set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00020137 File Offset: 0x0001E337
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x0002013F File Offset: 0x0001E33F
		public string NotificationId { get; internal set; }

		// Token: 0x060007AF RID: 1967 RVA: 0x00020148 File Offset: 0x0001E348
		public void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData)
		{
			this.LocalDataAccessMetaData = metaData;
			this.SetProperties(propertyBag);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00020158 File Offset: 0x0001E358
		public void SetProperties(Dictionary<string, string> propertyBag)
		{
			string inputStr = null;
			if (propertyBag.TryGetValue("NotificationId", out inputStr))
			{
				this.NotificationId = CrimsonHelper.NullDecode(inputStr);
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00020182 File Offset: 0x0001E382
		public void Write(Action<IPersistence> preWriteHandler = null)
		{
			if (preWriteHandler != null)
			{
				preWriteHandler(this);
			}
			StartupNotification.InsertStartupNotification(this.NotificationId);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0002019C File Offset: 0x0001E39C
		public static void SetStartupNotificationDefinition(WorkDefinition definition, string notificationId, int maxStartWaitInSeconds)
		{
			if (definition == null)
			{
				throw new ArgumentNullException("definition");
			}
			if (string.IsNullOrWhiteSpace(notificationId))
			{
				throw new ArgumentException("notificationId cannot be null or empty.");
			}
			if (maxStartWaitInSeconds <= 0)
			{
				throw new ArgumentOutOfRangeException("maxStartWaitInSeconds", maxStartWaitInSeconds, "Must be greater than 0.");
			}
			definition.Attributes["StartupNotificationId"] = notificationId;
			definition.Attributes["StartupNotificationMaxStartWaitInSeconds"] = maxStartWaitInSeconds.ToString();
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0002020C File Offset: 0x0001E40C
		public static void InsertStartupNotification(string notificationId)
		{
			ManagedAvailabilityCrimsonEvents.StartupNotification.Log<string>(notificationId);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0002021C File Offset: 0x0001E41C
		public static DateTime GetSystemBootTime(bool isRestrictPrecisionToSeconds = true)
		{
			long tickCount = NativeMethods.GetTickCount64();
			DateTime result = DateTime.UtcNow - TimeSpan.FromMilliseconds((double)tickCount);
			if (isRestrictPrecisionToSeconds)
			{
				result = new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, result.Kind);
			}
			return result;
		}
	}
}
