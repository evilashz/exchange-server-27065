using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Monitoring;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200041F RID: 1055
	internal class MailboxAssistantsWatermarks : AssistantTroubleshooterBase
	{
		// Token: 0x060024AB RID: 9387 RVA: 0x00092510 File Offset: 0x00090710
		public MailboxAssistantsWatermarks(PropertyBag fields) : base(fields)
		{
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x00092528 File Offset: 0x00090728
		private TimeSpan WatermarkBehindWarningThrehold
		{
			get
			{
				if (this.watermarkBehindWarningThrehold == null)
				{
					if (this.fields["WatermarkBehindWarningThreholdInMinutes"] != null)
					{
						this.watermarkBehindWarningThrehold = new TimeSpan?(TimeSpan.FromMinutes((uint)this.fields["WatermarkBehindWarningThreholdInMinutes"]));
					}
					else
					{
						this.watermarkBehindWarningThrehold = new TimeSpan?(MailboxAssistantsWatermarks.DefaultWatermarkBehindWarningThrehold);
					}
				}
				return this.watermarkBehindWarningThrehold.Value;
			}
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x00092598 File Offset: 0x00090798
		public override MonitoringData InternalRunCheck()
		{
			MonitoringData monitoringData = new MonitoringData();
			if (base.ExchangeServer.AdminDisplayVersion.Major < MailboxAssistantsWatermarks.minExpectedServerVersion.Major || (base.ExchangeServer.AdminDisplayVersion.Major == MailboxAssistantsWatermarks.minExpectedServerVersion.Major && base.ExchangeServer.AdminDisplayVersion.Minor < MailboxAssistantsWatermarks.minExpectedServerVersion.Minor))
			{
				monitoringData.Events.Add(new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5101, EventTypeEnumeration.Warning, Strings.TSMinServerVersion(MailboxAssistantsWatermarks.minExpectedServerVersion.ToString())));
				return monitoringData;
			}
			using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=Management", base.ExchangeServer.Name, null, null, null))
			{
				List<MdbStatus> onlineMDBList = base.GetOnlineMDBList(exRpcAdmin);
				foreach (MdbStatus mdbStatus in onlineMDBList)
				{
					Guid empty = Guid.Empty;
					Watermark[] watermarksForMailbox = exRpcAdmin.GetWatermarksForMailbox(mdbStatus.MdbGuid, ref empty, Guid.Empty);
					MapiEventManager mapiEventManager = MapiEventManager.Create(exRpcAdmin, Guid.Empty, mdbStatus.MdbGuid);
					long eventCounter = mapiEventManager.ReadLastEvent().EventCounter;
					bool flag = false;
					foreach (Watermark watermark in watermarksForMailbox)
					{
						if (eventCounter - watermark.EventCounter > 50L)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						MailboxAssistantsWatermarks.WatermarkWithCreateTime[] array2 = MailboxAssistantsWatermarks.BuildWaterMarkWithCreateTimes(mapiEventManager, watermarksForMailbox);
						DateTime eventTime = MailboxAssistantsWatermarks.GetEventTime(mapiEventManager, eventCounter);
						List<MailboxAssistantsWatermarks.WatermarkWithCreateTime> list = new List<MailboxAssistantsWatermarks.WatermarkWithCreateTime>(watermarksForMailbox.Length);
						foreach (MailboxAssistantsWatermarks.WatermarkWithCreateTime watermarkWithCreateTime in array2)
						{
							if (eventTime - watermarkWithCreateTime.CreateTime > this.WatermarkBehindWarningThrehold)
							{
								list.Add(watermarkWithCreateTime);
							}
						}
						if (list.Count > 0)
						{
							monitoringData.Events.Add(new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5207, EventTypeEnumeration.Error, Strings.AIMDBWatermarksAreTooLow(base.ExchangeServer.Fqdn, mdbStatus.MdbName, ((int)this.WatermarkBehindWarningThrehold.TotalMinutes).ToString(), MailboxAssistantsWatermarks.BuildFormatedEventCounter(eventCounter, eventTime), MailboxAssistantsWatermarks.BuildFormatedWatermarks(list.ToArray()))));
						}
					}
				}
			}
			return monitoringData;
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x00092810 File Offset: 0x00090A10
		public override MonitoringData Resolve(MonitoringData monitoringData)
		{
			return monitoringData;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x00092814 File Offset: 0x00090A14
		private static MailboxAssistantsWatermarks.WatermarkWithCreateTime[] BuildWaterMarkWithCreateTimes(MapiEventManager eventManager, Watermark[] watermarks)
		{
			MailboxAssistantsWatermarks.WatermarkWithCreateTime[] array = new MailboxAssistantsWatermarks.WatermarkWithCreateTime[watermarks.Length];
			for (int i = 0; i < watermarks.Length; i++)
			{
				DateTime eventTime = MailboxAssistantsWatermarks.GetEventTime(eventManager, watermarks[i].EventCounter);
				array[i] = new MailboxAssistantsWatermarks.WatermarkWithCreateTime(watermarks[i].ConsumerGuid, watermarks[i].EventCounter, eventTime);
			}
			return array;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x00092864 File Offset: 0x00090A64
		private static DateTime GetEventTime(MapiEventManager eventManager, long eventCounter)
		{
			MapiEvent[] array = eventManager.ReadEvents(eventCounter, 1);
			if (array != null && array.Length != 0)
			{
				return array[0].CreateTime;
			}
			return DateTime.UtcNow;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x00092890 File Offset: 0x00090A90
		private static string BuildFormatedWatermarks(MailboxAssistantsWatermarks.WatermarkWithCreateTime[] watermarks)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < watermarks.Length; i++)
			{
				stringBuilder.Append(watermarks[i].ConsumerGuid);
				stringBuilder.Append(":");
				stringBuilder.Append(MailboxAssistantsWatermarks.BuildFormatedEventCounter(watermarks[i].EventCounter, watermarks[i].CreateTime));
				if (i != watermarks.Length - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x00092907 File Offset: 0x00090B07
		private static string BuildFormatedEventCounter(long eventCounter, DateTime eventTime)
		{
			return string.Format("{0}:{1}", eventCounter, eventTime);
		}

		// Token: 0x04001D07 RID: 7431
		public const string WatermarkBehindWarningThreholdInMinutesPropertyName = "WatermarkBehindWarningThreholdInMinutes";

		// Token: 0x04001D08 RID: 7432
		private TimeSpan? watermarkBehindWarningThrehold = null;

		// Token: 0x04001D09 RID: 7433
		private static TimeSpan DefaultWatermarkBehindWarningThrehold = TimeSpan.FromMinutes(60.0);

		// Token: 0x04001D0A RID: 7434
		private static ServerVersion minExpectedServerVersion = new ServerVersion(14, 1, 0, 0);

		// Token: 0x02000420 RID: 1056
		private class WatermarkWithCreateTime
		{
			// Token: 0x17000AC5 RID: 2757
			// (get) Token: 0x060024B4 RID: 9396 RVA: 0x00092943 File Offset: 0x00090B43
			// (set) Token: 0x060024B5 RID: 9397 RVA: 0x0009294B File Offset: 0x00090B4B
			public Guid ConsumerGuid { get; private set; }

			// Token: 0x17000AC6 RID: 2758
			// (get) Token: 0x060024B6 RID: 9398 RVA: 0x00092954 File Offset: 0x00090B54
			// (set) Token: 0x060024B7 RID: 9399 RVA: 0x0009295C File Offset: 0x00090B5C
			public long EventCounter { get; private set; }

			// Token: 0x17000AC7 RID: 2759
			// (get) Token: 0x060024B8 RID: 9400 RVA: 0x00092965 File Offset: 0x00090B65
			// (set) Token: 0x060024B9 RID: 9401 RVA: 0x0009296D File Offset: 0x00090B6D
			public DateTime CreateTime { get; private set; }

			// Token: 0x060024BA RID: 9402 RVA: 0x00092976 File Offset: 0x00090B76
			public WatermarkWithCreateTime(Guid consumerGuid, long eventCounter, DateTime createTime)
			{
				this.ConsumerGuid = consumerGuid;
				this.EventCounter = eventCounter;
				this.CreateTime = createTime;
			}
		}
	}
}
