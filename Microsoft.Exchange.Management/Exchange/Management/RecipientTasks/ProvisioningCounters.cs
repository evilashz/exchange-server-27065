using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000DCB RID: 3531
	internal static class ProvisioningCounters
	{
		// Token: 0x06008679 RID: 34425 RVA: 0x0022507C File Offset: 0x0022327C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ProvisioningCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ProvisioningCounters.AllCounters)
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

		// Token: 0x0400415C RID: 16732
		public const string CategoryName = "MSExchange Provisioning";

		// Token: 0x0400415D RID: 16733
		public static readonly ExPerformanceCounter NumberOfNewMailboxCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of New-Mailbox Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400415E RID: 16734
		public static readonly ExPerformanceCounter NumberOfNewMailuserCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of New-MailUser Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400415F RID: 16735
		public static readonly ExPerformanceCounter NumberOfNewRemoteMailboxCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of New-RemoteMailbox Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004160 RID: 16736
		public static readonly ExPerformanceCounter NumberOfNewSyncMailboxCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of New-SyncMailbox Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004161 RID: 16737
		public static readonly ExPerformanceCounter NumberOfNewSyncMailuserCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of New-SyncMailUser Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004162 RID: 16738
		public static readonly ExPerformanceCounter NumberOfUndoSoftDeletedMailboxCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of Undo-SoftDeletedMailbox Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004163 RID: 16739
		public static readonly ExPerformanceCounter NumberOfUndoSyncSoftDeletedMailboxCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of Undo-SyncSoftDeletedMailbox Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004164 RID: 16740
		public static readonly ExPerformanceCounter NumberOfUndoSyncSoftDeletedMailuserCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of Undo-SyncSoftDeletedMailuser Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004165 RID: 16741
		public static readonly ExPerformanceCounter NumberOfSuccessfulNewMailboxCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of Successful New-Mailbox Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004166 RID: 16742
		public static readonly ExPerformanceCounter NumberOfSuccessfulNewMailuserCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of Successful New-MailUser Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004167 RID: 16743
		public static readonly ExPerformanceCounter NumberOfSuccessfulNewRemoteMailboxCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of Successful New-RemoteMailbox Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004168 RID: 16744
		public static readonly ExPerformanceCounter NumberOfSuccessfulNewSyncMailboxCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of Successful New-SyncMailbox Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004169 RID: 16745
		public static readonly ExPerformanceCounter NumberOfSuccessfulNewSyncMailuserCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of successful New-SyncMailUser Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400416A RID: 16746
		public static readonly ExPerformanceCounter NumberOfSuccessfulUndoSoftDeletedMailboxCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of successful Undo-SoftDeletedMailbox Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400416B RID: 16747
		public static readonly ExPerformanceCounter NumberOfSuccessfulUndoSyncSoftDeletedMailboxCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of successful Undo-SyncSoftDeletedMailbox Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400416C RID: 16748
		public static readonly ExPerformanceCounter NumberOfSuccessfulUndoSyncSoftDeletedMailuserCalls = new ExPerformanceCounter("MSExchange Provisioning", "Number of successful Undo-SyncSoftDeletedMailuser Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400416D RID: 16749
		public static readonly ExPerformanceCounter AverageNewMailboxResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Average New-Mailbox Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400416E RID: 16750
		public static readonly ExPerformanceCounter AverageNewMailboxResponseTimeBase = new ExPerformanceCounter("MSExchange Provisioning", "Average New-Mailbox Response TimeBase", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400416F RID: 16751
		public static readonly ExPerformanceCounter AverageNewMailboxResponseTimeWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-Mailbox Response Time with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004170 RID: 16752
		public static readonly ExPerformanceCounter AverageNewMailboxResponseTimeBaseWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-Mailbox Response TimeBase with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004171 RID: 16753
		public static readonly ExPerformanceCounter AverageNewMailboxResponseTimeWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-Mailbox Response Time Without Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004172 RID: 16754
		public static readonly ExPerformanceCounter AverageNewMailboxResponseTimeBaseWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-Mailbox Response TimeBase Without Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004173 RID: 16755
		public static readonly ExPerformanceCounter AverageNewMailuserResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Average New-MailUser Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004174 RID: 16756
		public static readonly ExPerformanceCounter AverageNewMailuserResponseTimeBase = new ExPerformanceCounter("MSExchange Provisioning", "Average New-MailUser Response TimeBase", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004175 RID: 16757
		public static readonly ExPerformanceCounter AverageNewMailuserResponseTimeWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-MailUser Response Time with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004176 RID: 16758
		public static readonly ExPerformanceCounter AverageNewMailuserResponseTimeBaseWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-MailUser Response TimeBase with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004177 RID: 16759
		public static readonly ExPerformanceCounter AverageNewMailuserResponseTimeWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-MailUser Response Time Without Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004178 RID: 16760
		public static readonly ExPerformanceCounter AverageNewMailuserResponseTimeBaseWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-MailUser Response TimeBase Without Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004179 RID: 16761
		public static readonly ExPerformanceCounter AverageNewRemoteMailboxResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Average New-RemoteMailbox Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400417A RID: 16762
		public static readonly ExPerformanceCounter AverageNewRemoteMailboxResponseTimeBase = new ExPerformanceCounter("MSExchange Provisioning", "Average New-RemoteMailbox Response TimeBase", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400417B RID: 16763
		public static readonly ExPerformanceCounter AverageNewRemoteMailboxResponseTimeWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-RemoteMailbox Response Time with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400417C RID: 16764
		public static readonly ExPerformanceCounter AverageNewRemoteMailboxResponseTimeBaseWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-RemoteMailbox Response TimeBase with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400417D RID: 16765
		public static readonly ExPerformanceCounter AverageNewRemoteMailboxResponseTimeWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-RemoteMailbox Response Time Without Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400417E RID: 16766
		public static readonly ExPerformanceCounter AverageNewRemoteMailboxResponseTimeBaseWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-RemoteMailbox Response TimeBase Without Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400417F RID: 16767
		public static readonly ExPerformanceCounter AverageNewSyncMailboxResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailbox Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004180 RID: 16768
		public static readonly ExPerformanceCounter AverageNewSyncMailboxResponseTimeBase = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailbox Response TimeBase", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004181 RID: 16769
		public static readonly ExPerformanceCounter AverageNewSyncMailboxResponseTimeWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailbox Response Time with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004182 RID: 16770
		public static readonly ExPerformanceCounter AverageNewSyncMailboxResponseTimeBaseWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailbox Response TimeBase with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004183 RID: 16771
		public static readonly ExPerformanceCounter AverageNewSyncMailboxResponseTimeWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailbox Response Time Without Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004184 RID: 16772
		public static readonly ExPerformanceCounter AverageNewSyncMailboxResponseTimeBaseWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailbox Response TimeBase Without Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004185 RID: 16773
		public static readonly ExPerformanceCounter AverageNewSyncMailuserResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailUser Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004186 RID: 16774
		public static readonly ExPerformanceCounter AverageNewSyncMailuserResponseTimeBase = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailUser Response TimeBase", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004187 RID: 16775
		public static readonly ExPerformanceCounter AverageNewSyncMailuserResponseTimeWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailUser Response Time with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004188 RID: 16776
		public static readonly ExPerformanceCounter AverageNewSyncMailuserResponseTimeBaseWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailUser Response TimeBase with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004189 RID: 16777
		public static readonly ExPerformanceCounter AverageNewSyncMailuserResponseTimeWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailUser Response Time Without Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400418A RID: 16778
		public static readonly ExPerformanceCounter AverageNewSyncMailuserResponseTimeBaseWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average New-SyncMailUser Response TimeBase Without Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400418B RID: 16779
		public static readonly ExPerformanceCounter AverageUndoSoftDeletedMailboxResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SoftDeletedMailbox Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400418C RID: 16780
		public static readonly ExPerformanceCounter AverageUndoSoftDeletedMailboxResponseTimeBase = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SoftDeletedMailbox Response TimeBase", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400418D RID: 16781
		public static readonly ExPerformanceCounter AverageUndoSoftDeletedMailboxResponseTimeWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SoftDeletedMailbox Response Time with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400418E RID: 16782
		public static readonly ExPerformanceCounter AverageUndoSoftDeletedMailboxResponseTimeBaseWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SoftDeletedMailbox Response TimeBase with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400418F RID: 16783
		public static readonly ExPerformanceCounter AverageUndoSoftDeletedMailboxResponseTimeWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SoftDeletedMailbox Response Time Without Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004190 RID: 16784
		public static readonly ExPerformanceCounter AverageUndoSoftDeletedMailboxResponseTimeBaseWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SoftDeletedMailbox Response TimeBase Without Active Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004191 RID: 16785
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailboxResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailbox Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004192 RID: 16786
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailboxResponseTimeBase = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailbox Response TimeBase", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004193 RID: 16787
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailboxResponseTimeWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailbox Response Time with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004194 RID: 16788
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailboxResponseTimeBaseWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailbox Response TimeBase with Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004195 RID: 16789
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailboxResponseTimeWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailbox Response Time Without Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004196 RID: 16790
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailboxResponseTimeBaseWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailbox Response TimeBase Without Active Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004197 RID: 16791
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailuserResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailuser Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004198 RID: 16792
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailuserResponseTimeBase = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailuser Response TimeBase", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004199 RID: 16793
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailuserResponseTimeWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailuser Response Time with Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400419A RID: 16794
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailuserResponseTimeBaseWithCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailuser Response TimeBase with Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400419B RID: 16795
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailuserResponseTimeWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailuser Response Time Without Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400419C RID: 16796
		public static readonly ExPerformanceCounter AverageUndoSyncSoftDeletedMailuserResponseTimeBaseWithoutCache = new ExPerformanceCounter("MSExchange Provisioning", "Average Undo-SyncSoftDeletedMailuser Response TimeBase Without Active Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400419D RID: 16797
		public static readonly ExPerformanceCounter TotalNewMailboxResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Total New-Mailbox Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400419E RID: 16798
		public static readonly ExPerformanceCounter TotalNewMailuserResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Total New-MailUser Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400419F RID: 16799
		public static readonly ExPerformanceCounter TotalNewRemoteMailboxResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Total New-RemoteMailbox Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041A0 RID: 16800
		public static readonly ExPerformanceCounter TotalNewSyncMailboxResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Total New-SyncMailbox Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041A1 RID: 16801
		public static readonly ExPerformanceCounter TotalNewSyncMailuserResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Total New-SyncMailUser Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041A2 RID: 16802
		public static readonly ExPerformanceCounter TotalUndoSoftDeletedMailboxResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Total Undo-SoftDeletedMailbox Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041A3 RID: 16803
		public static readonly ExPerformanceCounter TotalUndoSyncSoftDeletedMailboxResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Total Undo-SyncSoftDeletedMailbox Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041A4 RID: 16804
		public static readonly ExPerformanceCounter TotalUndoSyncSoftDeletedMailuserResponseTime = new ExPerformanceCounter("MSExchange Provisioning", "Total Undo-SyncSoftDeletedMailuser Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041A5 RID: 16805
		public static readonly ExPerformanceCounter NewMailboxCacheActivePercentage = new ExPerformanceCounter("MSExchange Provisioning", "Percentage of New-Mailbox Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041A6 RID: 16806
		public static readonly ExPerformanceCounter NewMailboxCacheActivePercentageBase = new ExPerformanceCounter("MSExchange Provisioning", "Percentage Base of New-Mailbox Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041A7 RID: 16807
		public static readonly ExPerformanceCounter NewMailuserCacheActivePercentage = new ExPerformanceCounter("MSExchange Provisioning", "Percentage of New-MailUser Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041A8 RID: 16808
		public static readonly ExPerformanceCounter NewMailuserCacheActivePercentageBase = new ExPerformanceCounter("MSExchange Provisioning", "Percentage Base of New-MailUser Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041A9 RID: 16809
		public static readonly ExPerformanceCounter NewRemoteMailboxCacheActivePercentage = new ExPerformanceCounter("MSExchange Provisioning", "Percentage of New-RemoteMailbox Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041AA RID: 16810
		public static readonly ExPerformanceCounter NewRemoteMailboxCacheActivePercentageBase = new ExPerformanceCounter("MSExchange Provisioning", "Percentage Base of New-RemoteMailbox Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041AB RID: 16811
		public static readonly ExPerformanceCounter NewSyncMailboxCacheActivePercentage = new ExPerformanceCounter("MSExchange Provisioning", "Percentage of New-SyncMailbox calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041AC RID: 16812
		public static readonly ExPerformanceCounter NewSyncMailboxCacheActivePercentageBase = new ExPerformanceCounter("MSExchange Provisioning", "Percentage Base of New-SyncMailbox Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041AD RID: 16813
		public static readonly ExPerformanceCounter NewSyncMailuserCacheActivePercentage = new ExPerformanceCounter("MSExchange Provisioning", "Percentage of New-SyncMailUser Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041AE RID: 16814
		public static readonly ExPerformanceCounter NewSyncMailuserCacheActivePercentageBase = new ExPerformanceCounter("MSExchange Provisioning", "Percentage Base of New-SyncMailUser Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041AF RID: 16815
		public static readonly ExPerformanceCounter UndoSoftDeletedMailboxCacheActivePercentage = new ExPerformanceCounter("MSExchange Provisioning", "Percentage of Undo-SoftDeletedMailbox Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041B0 RID: 16816
		public static readonly ExPerformanceCounter UndoSoftDeletedMailboxCacheActivePercentageBase = new ExPerformanceCounter("MSExchange Provisioning", "Percentage Base of Undo-SoftDeletedMailbox Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041B1 RID: 16817
		public static readonly ExPerformanceCounter UndoSyncSoftDeletedMailboxCacheActivePercentage = new ExPerformanceCounter("MSExchange Provisioning", "Percentage of Undo-SyncSoftDeletedMailbox Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041B2 RID: 16818
		public static readonly ExPerformanceCounter UndoSyncSoftDeletedMailboxCacheActivePercentageBase = new ExPerformanceCounter("MSExchange Provisioning", "Percentage Base of Undo-SyncSoftDeletedMailbox Calls with Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041B3 RID: 16819
		public static readonly ExPerformanceCounter UndoSyncSoftDeletedMailuserCacheActivePercentage = new ExPerformanceCounter("MSExchange Provisioning", "Percentage of Undo-SyncSoftDeletedMailuser Calls with Active Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041B4 RID: 16820
		public static readonly ExPerformanceCounter UndoSyncSoftDeletedMailuserCacheActivePercentageBase = new ExPerformanceCounter("MSExchange Provisioning", "Percentage Base of Undo-SyncSoftDeletedMailuser Calls with Provisioning Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040041B5 RID: 16821
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ProvisioningCounters.NumberOfNewMailboxCalls,
			ProvisioningCounters.NumberOfNewMailuserCalls,
			ProvisioningCounters.NumberOfNewRemoteMailboxCalls,
			ProvisioningCounters.NumberOfNewSyncMailboxCalls,
			ProvisioningCounters.NumberOfNewSyncMailuserCalls,
			ProvisioningCounters.NumberOfUndoSoftDeletedMailboxCalls,
			ProvisioningCounters.NumberOfUndoSyncSoftDeletedMailboxCalls,
			ProvisioningCounters.NumberOfUndoSyncSoftDeletedMailuserCalls,
			ProvisioningCounters.NumberOfSuccessfulNewMailboxCalls,
			ProvisioningCounters.NumberOfSuccessfulNewMailuserCalls,
			ProvisioningCounters.NumberOfSuccessfulNewRemoteMailboxCalls,
			ProvisioningCounters.NumberOfSuccessfulNewSyncMailboxCalls,
			ProvisioningCounters.NumberOfSuccessfulNewSyncMailuserCalls,
			ProvisioningCounters.NumberOfSuccessfulUndoSoftDeletedMailboxCalls,
			ProvisioningCounters.NumberOfSuccessfulUndoSyncSoftDeletedMailboxCalls,
			ProvisioningCounters.NumberOfSuccessfulUndoSyncSoftDeletedMailuserCalls,
			ProvisioningCounters.AverageNewMailboxResponseTime,
			ProvisioningCounters.AverageNewMailboxResponseTimeBase,
			ProvisioningCounters.AverageNewMailboxResponseTimeWithCache,
			ProvisioningCounters.AverageNewMailboxResponseTimeBaseWithCache,
			ProvisioningCounters.AverageNewMailboxResponseTimeWithoutCache,
			ProvisioningCounters.AverageNewMailboxResponseTimeBaseWithoutCache,
			ProvisioningCounters.AverageNewMailuserResponseTime,
			ProvisioningCounters.AverageNewMailuserResponseTimeBase,
			ProvisioningCounters.AverageNewMailuserResponseTimeWithCache,
			ProvisioningCounters.AverageNewMailuserResponseTimeBaseWithCache,
			ProvisioningCounters.AverageNewMailuserResponseTimeWithoutCache,
			ProvisioningCounters.AverageNewMailuserResponseTimeBaseWithoutCache,
			ProvisioningCounters.AverageNewRemoteMailboxResponseTime,
			ProvisioningCounters.AverageNewRemoteMailboxResponseTimeBase,
			ProvisioningCounters.AverageNewRemoteMailboxResponseTimeWithCache,
			ProvisioningCounters.AverageNewRemoteMailboxResponseTimeBaseWithCache,
			ProvisioningCounters.AverageNewRemoteMailboxResponseTimeWithoutCache,
			ProvisioningCounters.AverageNewRemoteMailboxResponseTimeBaseWithoutCache,
			ProvisioningCounters.AverageNewSyncMailboxResponseTime,
			ProvisioningCounters.AverageNewSyncMailboxResponseTimeBase,
			ProvisioningCounters.AverageNewSyncMailboxResponseTimeWithCache,
			ProvisioningCounters.AverageNewSyncMailboxResponseTimeBaseWithCache,
			ProvisioningCounters.AverageNewSyncMailboxResponseTimeWithoutCache,
			ProvisioningCounters.AverageNewSyncMailboxResponseTimeBaseWithoutCache,
			ProvisioningCounters.AverageNewSyncMailuserResponseTime,
			ProvisioningCounters.AverageNewSyncMailuserResponseTimeBase,
			ProvisioningCounters.AverageNewSyncMailuserResponseTimeWithCache,
			ProvisioningCounters.AverageNewSyncMailuserResponseTimeBaseWithCache,
			ProvisioningCounters.AverageNewSyncMailuserResponseTimeWithoutCache,
			ProvisioningCounters.AverageNewSyncMailuserResponseTimeBaseWithoutCache,
			ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTime,
			ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTimeBase,
			ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTimeWithCache,
			ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTimeBaseWithCache,
			ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTimeWithoutCache,
			ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTimeBaseWithoutCache,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTime,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTimeBase,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTimeWithCache,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTimeBaseWithCache,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTimeWithoutCache,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTimeBaseWithoutCache,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTime,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTimeBase,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTimeWithCache,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTimeBaseWithCache,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTimeWithoutCache,
			ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTimeBaseWithoutCache,
			ProvisioningCounters.TotalNewMailboxResponseTime,
			ProvisioningCounters.TotalNewMailuserResponseTime,
			ProvisioningCounters.TotalNewRemoteMailboxResponseTime,
			ProvisioningCounters.TotalNewSyncMailboxResponseTime,
			ProvisioningCounters.TotalNewSyncMailuserResponseTime,
			ProvisioningCounters.TotalUndoSoftDeletedMailboxResponseTime,
			ProvisioningCounters.TotalUndoSyncSoftDeletedMailboxResponseTime,
			ProvisioningCounters.TotalUndoSyncSoftDeletedMailuserResponseTime,
			ProvisioningCounters.NewMailboxCacheActivePercentage,
			ProvisioningCounters.NewMailboxCacheActivePercentageBase,
			ProvisioningCounters.NewMailuserCacheActivePercentage,
			ProvisioningCounters.NewMailuserCacheActivePercentageBase,
			ProvisioningCounters.NewRemoteMailboxCacheActivePercentage,
			ProvisioningCounters.NewRemoteMailboxCacheActivePercentageBase,
			ProvisioningCounters.NewSyncMailboxCacheActivePercentage,
			ProvisioningCounters.NewSyncMailboxCacheActivePercentageBase,
			ProvisioningCounters.NewSyncMailuserCacheActivePercentage,
			ProvisioningCounters.NewSyncMailuserCacheActivePercentageBase,
			ProvisioningCounters.UndoSoftDeletedMailboxCacheActivePercentage,
			ProvisioningCounters.UndoSoftDeletedMailboxCacheActivePercentageBase,
			ProvisioningCounters.UndoSyncSoftDeletedMailboxCacheActivePercentage,
			ProvisioningCounters.UndoSyncSoftDeletedMailboxCacheActivePercentageBase,
			ProvisioningCounters.UndoSyncSoftDeletedMailuserCacheActivePercentage,
			ProvisioningCounters.UndoSyncSoftDeletedMailuserCacheActivePercentageBase
		};
	}
}
