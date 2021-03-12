using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000038 RID: 56
	internal class AutoAttendantCountersUtil : DirectoryAccessCountersUtil
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000B01C File Offset: 0x0000921C
		internal AutoAttendantCountersUtil(BaseUMCallSession vo) : base(vo)
		{
			if (CommonConstants.UseDataCenterLogging)
			{
				this.instanceName = "DataCenterAutoAttendantPerfCounterName";
				return;
			}
			this.instanceName = base.Session.CurrentCallContext.AutoAttendantInfo.Name;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B054 File Offset: 0x00009254
		internal void ComputeSuccessRate()
		{
			long rawValue = this.GetInstance().TotalCalls.RawValue;
			long value = this.GetInstance().TransfersToSendMessage.RawValue + this.GetInstance().DisconnectedWithoutInput.RawValue + this.GetInstance().OperatorTransfersInitiatedByUserFromOpeningMenu.RawValue + this.GetInstance().SentToAutoAttendant.RawValue + this.GetInstance().TransferredCount.RawValue + this.GetInstance().AmbiguousNameTransfers.RawValue + this.GetInstance().DisallowedTransfers.RawValue;
			if (rawValue > 0L)
			{
				base.Session.SetCounter(this.GetInstance().PercentageSuccessfulCalls, value);
				base.Session.SetCounter(this.GetInstance().PercentageSuccessfulCalls_Base, rawValue);
				return;
			}
			base.Session.SetCounter(this.GetInstance().PercentageSuccessfulCalls, 0L);
			base.Session.SetCounter(this.GetInstance().PercentageSuccessfulCalls_Base, 1L);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B14D File Offset: 0x0000934D
		internal AACountersInstance GetInstance()
		{
			return AACounters.GetInstance(this.instanceName);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B15C File Offset: 0x0000935C
		internal void IncrementCallTypeCounters(bool isBusinessHourCall)
		{
			base.Session.IncrementCounter(this.GetInstance().TotalCalls);
			if (isBusinessHourCall)
			{
				base.Session.IncrementCounter(this.GetInstance().BusinessHoursCalls);
				return;
			}
			base.Session.IncrementCounter(this.GetInstance().OutOfHoursCalls);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B1B0 File Offset: 0x000093B0
		internal void IncrementTransferCounters(TransferExtension ext)
		{
			switch (ext)
			{
			case TransferExtension.Operator:
				base.Session.IncrementCounter(this.GetInstance().OperatorTransfers);
				return;
			case TransferExtension.CustomMenuExtension:
				base.Session.IncrementCounter(this.GetInstance().TransferredCount);
				return;
			case TransferExtension.UserExtension:
				base.Session.IncrementCounter(this.GetInstance().TransferredCount);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B218 File Offset: 0x00009418
		internal void IncrementTransferCounter()
		{
			base.Session.IncrementCounter(this.GetInstance().TransferredCount);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B230 File Offset: 0x00009430
		internal void IncrementTransfersToSendMessageCounter()
		{
			base.Session.IncrementCounter(this.GetInstance().TransfersToSendMessage);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B248 File Offset: 0x00009448
		internal void IncrementNameSpokenCounter()
		{
			base.Session.IncrementCounter(this.GetInstance().NameSpoken);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B260 File Offset: 0x00009460
		internal void IncrementTransfersToDtmfFallbackAutoAttendantCounter()
		{
			base.Session.IncrementCounter(this.GetInstance().TransfersToDtmfFallbackAutoAttendant);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000B278 File Offset: 0x00009478
		internal void IncrementUserInitiatedTransferToOperatorCounter()
		{
			base.Session.IncrementCounter(this.GetInstance().OperatorTransfersInitiatedByUser);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B290 File Offset: 0x00009490
		internal void IncrementUserInitiatedTransferToOperatorFromMainMenuCounter()
		{
			base.Session.IncrementCounter(this.GetInstance().OperatorTransfersInitiatedByUserFromOpeningMenu);
			base.Session.IncrementCounter(this.GetInstance().OperatorTransfersInitiatedByUser);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B2BE File Offset: 0x000094BE
		internal void IncrementTransferToKeyMappingAutoAttendantCounter()
		{
			base.Session.IncrementCounter(this.GetInstance().SentToAutoAttendant);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B2D8 File Offset: 0x000094D8
		internal void IncrementCustomMenuCounters(CustomMenuKey option)
		{
			switch (option)
			{
			case CustomMenuKey.One:
				base.Session.IncrementCounter(this.GetInstance().MenuOption1);
				break;
			case CustomMenuKey.Two:
				base.Session.IncrementCounter(this.GetInstance().MenuOption2);
				break;
			case CustomMenuKey.Three:
				base.Session.IncrementCounter(this.GetInstance().MenuOption3);
				break;
			case CustomMenuKey.Four:
				base.Session.IncrementCounter(this.GetInstance().MenuOption4);
				break;
			case CustomMenuKey.Five:
				base.Session.IncrementCounter(this.GetInstance().MenuOption5);
				break;
			case CustomMenuKey.Six:
				base.Session.IncrementCounter(this.GetInstance().MenuOption6);
				break;
			case CustomMenuKey.Seven:
				base.Session.IncrementCounter(this.GetInstance().MenuOption7);
				break;
			case CustomMenuKey.Eight:
				base.Session.IncrementCounter(this.GetInstance().MenuOption8);
				break;
			case CustomMenuKey.Nine:
				base.Session.IncrementCounter(this.GetInstance().MenuOption9);
				break;
			case CustomMenuKey.Timeout:
				base.Session.IncrementCounter(this.GetInstance().MenuOptionTimeout);
				break;
			}
			base.Session.IncrementCounter(this.GetInstance().CustomMenuOptions);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B430 File Offset: 0x00009630
		internal void IncrementSpeechCallsCounter()
		{
			base.Session.IncrementCounter(this.GetInstance().SpeechCalls);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B448 File Offset: 0x00009648
		internal void IncrementANRTransfersToOperatorCounter()
		{
			base.Session.IncrementCounter(this.GetInstance().AmbiguousNameTransfers);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B460 File Offset: 0x00009660
		internal void IncrementDisallowedTransferCalls()
		{
			base.Session.IncrementCounter(this.GetInstance().DisallowedTransfers);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B478 File Offset: 0x00009678
		internal void UpdateAverageTimeCounters(ExDateTime startTime)
		{
			TimeSpan timeSpan = ExDateTime.UtcNow - startTime;
			base.Session.SetCounter(this.GetInstance().AverageRecentCallTime, AutoAttendantCountersUtil.recentCallTimeAverage.Update((long)timeSpan.Seconds));
			base.Session.SetCounter(this.GetInstance().AverageCallTime, AutoAttendantCountersUtil.callTimeAverage.Update((long)timeSpan.Seconds));
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B4E4 File Offset: 0x000096E4
		protected override ExPerformanceCounter GetSingleCounter(DirectoryAccessCountersUtil.DirectoryAccessCounter perfcounter)
		{
			ExPerformanceCounter result;
			switch (perfcounter)
			{
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccess:
				result = this.GetInstance().DirectoryAccessed;
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedByExtension:
				result = this.GetInstance().DirectoryAccessedByExtension;
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedByDialByName:
				result = this.GetInstance().DirectoryAccessedByDialByName;
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedSuccessfullyByDialByName:
				result = this.GetInstance().DirectoryAccessedSuccessfullyByDialByName;
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedBySpokenName:
				result = this.GetInstance().DirectoryAccessedBySpokenName;
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedSuccessfullyBySpokenName:
				result = this.GetInstance().DirectoryAccessedSuccessfullyBySpokenName;
				break;
			default:
				throw new InvalidPerfCounterException(perfcounter.ToString());
			}
			return result;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B57C File Offset: 0x0000977C
		protected override List<ExPerformanceCounter> GetCounters(DirectoryAccessCountersUtil.DirectoryAccessCounter counter)
		{
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			switch (counter)
			{
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccess:
				list.Add(this.GetInstance().DirectoryAccessed);
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedByExtension:
				list.Add(this.GetInstance().DirectoryAccessedByExtension);
				list.Add(this.GetInstance().DirectoryAccessed);
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedByDialByName:
				list.Add(this.GetInstance().DirectoryAccessedByDialByName);
				list.Add(this.GetInstance().DirectoryAccessed);
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedSuccessfullyByDialByName:
				list.Add(this.GetInstance().DirectoryAccessedSuccessfullyByDialByName);
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedBySpokenName:
				list.Add(this.GetInstance().DirectoryAccessedBySpokenName);
				list.Add(this.GetInstance().DirectoryAccessed);
				break;
			case DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedSuccessfullyBySpokenName:
				list.Add(this.GetInstance().DirectoryAccessedSuccessfullyBySpokenName);
				break;
			default:
				throw new InvalidPerfCounterException(counter.ToString());
			}
			return list;
		}

		// Token: 0x040000C9 RID: 201
		private static MovingAverage recentCallTimeAverage = new MovingAverage(50);

		// Token: 0x040000CA RID: 202
		private static Average callTimeAverage = new Average();

		// Token: 0x040000CB RID: 203
		private string instanceName;
	}
}
