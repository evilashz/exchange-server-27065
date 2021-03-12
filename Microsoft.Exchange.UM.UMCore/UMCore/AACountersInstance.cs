using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000304 RID: 772
	internal sealed class AACountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600177D RID: 6013 RVA: 0x00063AD4 File Offset: 0x00061CD4
		internal AACountersInstance(string instanceName, AACountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeUMAutoAttendant")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalCalls = new ExPerformanceCounter(base.CategoryName, "Total Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalCalls);
				this.BusinessHoursCalls = new ExPerformanceCounter(base.CategoryName, "Business Hours Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BusinessHoursCalls);
				this.OutOfHoursCalls = new ExPerformanceCounter(base.CategoryName, "Out of Hours Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutOfHoursCalls);
				this.DisconnectedWithoutInput = new ExPerformanceCounter(base.CategoryName, "Disconnected Without Input", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DisconnectedWithoutInput);
				this.TransferredCount = new ExPerformanceCounter(base.CategoryName, "Transferred Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TransferredCount);
				this.DirectoryAccessed = new ExPerformanceCounter(base.CategoryName, "Directory Accessed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessed);
				this.DirectoryAccessedByExtension = new ExPerformanceCounter(base.CategoryName, "Directory Accessed by Extension", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessedByExtension);
				this.DirectoryAccessedByDialByName = new ExPerformanceCounter(base.CategoryName, "Directory Accessed by Dial by Name", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessedByDialByName);
				this.DirectoryAccessedSuccessfullyByDialByName = new ExPerformanceCounter(base.CategoryName, "Directory Accessed Successfully by Dial by Name", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessedSuccessfullyByDialByName);
				this.DirectoryAccessedBySpokenName = new ExPerformanceCounter(base.CategoryName, "Directory Accessed by Spoken Name", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessedBySpokenName);
				this.DirectoryAccessedSuccessfullyBySpokenName = new ExPerformanceCounter(base.CategoryName, "Directory Accessed Successfully by Spoken Name", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessedSuccessfullyBySpokenName);
				this.OperatorTransfers = new ExPerformanceCounter(base.CategoryName, "Operator Transfers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OperatorTransfers);
				this.CustomMenuOptions = new ExPerformanceCounter(base.CategoryName, "Custom Menu Options", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CustomMenuOptions);
				this.MenuOption1 = new ExPerformanceCounter(base.CategoryName, "Menu Option 1 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption1);
				this.MenuOption2 = new ExPerformanceCounter(base.CategoryName, "Menu Option 2 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption2);
				this.MenuOption3 = new ExPerformanceCounter(base.CategoryName, "Menu Option 3 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption3);
				this.MenuOption4 = new ExPerformanceCounter(base.CategoryName, "Menu Option 4 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption4);
				this.MenuOption5 = new ExPerformanceCounter(base.CategoryName, "Menu Option 5 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption5);
				this.MenuOption6 = new ExPerformanceCounter(base.CategoryName, "Menu Option 6 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption6);
				this.MenuOption7 = new ExPerformanceCounter(base.CategoryName, "Menu Option 7 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption7);
				this.MenuOption8 = new ExPerformanceCounter(base.CategoryName, "Menu Option 8 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption8);
				this.MenuOption9 = new ExPerformanceCounter(base.CategoryName, "Menu Option 9 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption9);
				this.MenuOptionTimeout = new ExPerformanceCounter(base.CategoryName, "Menu Option Timed Out", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOptionTimeout);
				this.AverageCallTime = new ExPerformanceCounter(base.CategoryName, "Average Call Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageCallTime);
				this.AverageRecentCallTime = new ExPerformanceCounter(base.CategoryName, "Average Recent Call Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageRecentCallTime);
				this.CallsDisconnectedOnIrrecoverableExternalError = new ExPerformanceCounter(base.CategoryName, "Calls Disconnected by UM on Irrecoverable External Error", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CallsDisconnectedOnIrrecoverableExternalError);
				this.SpeechCalls = new ExPerformanceCounter(base.CategoryName, "Calls with Speech Input", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SpeechCalls);
				this.AmbiguousNameTransfers = new ExPerformanceCounter(base.CategoryName, "Ambiguous Name Transfers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AmbiguousNameTransfers);
				this.DisallowedTransfers = new ExPerformanceCounter(base.CategoryName, "Disallowed Transfers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DisallowedTransfers);
				this.NameSpoken = new ExPerformanceCounter(base.CategoryName, "Calls with Spoken Name", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NameSpoken);
				this.TransfersToSendMessage = new ExPerformanceCounter(base.CategoryName, "Calls with Sent Message", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TransfersToSendMessage);
				this.TransfersToDtmfFallbackAutoAttendant = new ExPerformanceCounter(base.CategoryName, "Calls with DTMF fallback", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TransfersToDtmfFallbackAutoAttendant);
				this.SentToAutoAttendant = new ExPerformanceCounter(base.CategoryName, "Sent to Auto Attendant", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SentToAutoAttendant);
				this.OperatorTransfersInitiatedByUser = new ExPerformanceCounter(base.CategoryName, "Operator Transfers Requested by User", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OperatorTransfersInitiatedByUser);
				this.PercentageSuccessfulCalls = new ExPerformanceCounter(base.CategoryName, "% Successful Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageSuccessfulCalls);
				this.PercentageSuccessfulCalls_Base = new ExPerformanceCounter(base.CategoryName, "Base counter for % Successful Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageSuccessfulCalls_Base);
				this.OperatorTransfersInitiatedByUserFromOpeningMenu = new ExPerformanceCounter(base.CategoryName, "Operator Transfers Requested by User from Opening Menu", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OperatorTransfersInitiatedByUserFromOpeningMenu);
				long num = this.TotalCalls.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00064194 File Offset: 0x00062394
		internal AACountersInstance(string instanceName) : base(instanceName, "MSExchangeUMAutoAttendant")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalCalls = new ExPerformanceCounter(base.CategoryName, "Total Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalCalls);
				this.BusinessHoursCalls = new ExPerformanceCounter(base.CategoryName, "Business Hours Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BusinessHoursCalls);
				this.OutOfHoursCalls = new ExPerformanceCounter(base.CategoryName, "Out of Hours Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutOfHoursCalls);
				this.DisconnectedWithoutInput = new ExPerformanceCounter(base.CategoryName, "Disconnected Without Input", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DisconnectedWithoutInput);
				this.TransferredCount = new ExPerformanceCounter(base.CategoryName, "Transferred Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TransferredCount);
				this.DirectoryAccessed = new ExPerformanceCounter(base.CategoryName, "Directory Accessed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessed);
				this.DirectoryAccessedByExtension = new ExPerformanceCounter(base.CategoryName, "Directory Accessed by Extension", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessedByExtension);
				this.DirectoryAccessedByDialByName = new ExPerformanceCounter(base.CategoryName, "Directory Accessed by Dial by Name", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessedByDialByName);
				this.DirectoryAccessedSuccessfullyByDialByName = new ExPerformanceCounter(base.CategoryName, "Directory Accessed Successfully by Dial by Name", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessedSuccessfullyByDialByName);
				this.DirectoryAccessedBySpokenName = new ExPerformanceCounter(base.CategoryName, "Directory Accessed by Spoken Name", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessedBySpokenName);
				this.DirectoryAccessedSuccessfullyBySpokenName = new ExPerformanceCounter(base.CategoryName, "Directory Accessed Successfully by Spoken Name", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryAccessedSuccessfullyBySpokenName);
				this.OperatorTransfers = new ExPerformanceCounter(base.CategoryName, "Operator Transfers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OperatorTransfers);
				this.CustomMenuOptions = new ExPerformanceCounter(base.CategoryName, "Custom Menu Options", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CustomMenuOptions);
				this.MenuOption1 = new ExPerformanceCounter(base.CategoryName, "Menu Option 1 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption1);
				this.MenuOption2 = new ExPerformanceCounter(base.CategoryName, "Menu Option 2 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption2);
				this.MenuOption3 = new ExPerformanceCounter(base.CategoryName, "Menu Option 3 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption3);
				this.MenuOption4 = new ExPerformanceCounter(base.CategoryName, "Menu Option 4 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption4);
				this.MenuOption5 = new ExPerformanceCounter(base.CategoryName, "Menu Option 5 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption5);
				this.MenuOption6 = new ExPerformanceCounter(base.CategoryName, "Menu Option 6 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption6);
				this.MenuOption7 = new ExPerformanceCounter(base.CategoryName, "Menu Option 7 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption7);
				this.MenuOption8 = new ExPerformanceCounter(base.CategoryName, "Menu Option 8 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption8);
				this.MenuOption9 = new ExPerformanceCounter(base.CategoryName, "Menu Option 9 Used", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOption9);
				this.MenuOptionTimeout = new ExPerformanceCounter(base.CategoryName, "Menu Option Timed Out", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MenuOptionTimeout);
				this.AverageCallTime = new ExPerformanceCounter(base.CategoryName, "Average Call Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageCallTime);
				this.AverageRecentCallTime = new ExPerformanceCounter(base.CategoryName, "Average Recent Call Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageRecentCallTime);
				this.CallsDisconnectedOnIrrecoverableExternalError = new ExPerformanceCounter(base.CategoryName, "Calls Disconnected by UM on Irrecoverable External Error", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CallsDisconnectedOnIrrecoverableExternalError);
				this.SpeechCalls = new ExPerformanceCounter(base.CategoryName, "Calls with Speech Input", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SpeechCalls);
				this.AmbiguousNameTransfers = new ExPerformanceCounter(base.CategoryName, "Ambiguous Name Transfers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AmbiguousNameTransfers);
				this.DisallowedTransfers = new ExPerformanceCounter(base.CategoryName, "Disallowed Transfers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DisallowedTransfers);
				this.NameSpoken = new ExPerformanceCounter(base.CategoryName, "Calls with Spoken Name", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NameSpoken);
				this.TransfersToSendMessage = new ExPerformanceCounter(base.CategoryName, "Calls with Sent Message", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TransfersToSendMessage);
				this.TransfersToDtmfFallbackAutoAttendant = new ExPerformanceCounter(base.CategoryName, "Calls with DTMF fallback", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TransfersToDtmfFallbackAutoAttendant);
				this.SentToAutoAttendant = new ExPerformanceCounter(base.CategoryName, "Sent to Auto Attendant", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SentToAutoAttendant);
				this.OperatorTransfersInitiatedByUser = new ExPerformanceCounter(base.CategoryName, "Operator Transfers Requested by User", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OperatorTransfersInitiatedByUser);
				this.PercentageSuccessfulCalls = new ExPerformanceCounter(base.CategoryName, "% Successful Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageSuccessfulCalls);
				this.PercentageSuccessfulCalls_Base = new ExPerformanceCounter(base.CategoryName, "Base counter for % Successful Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageSuccessfulCalls_Base);
				this.OperatorTransfersInitiatedByUserFromOpeningMenu = new ExPerformanceCounter(base.CategoryName, "Operator Transfers Requested by User from Opening Menu", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OperatorTransfersInitiatedByUserFromOpeningMenu);
				long num = this.TotalCalls.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00064854 File Offset: 0x00062A54
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04000DB8 RID: 3512
		public readonly ExPerformanceCounter TotalCalls;

		// Token: 0x04000DB9 RID: 3513
		public readonly ExPerformanceCounter BusinessHoursCalls;

		// Token: 0x04000DBA RID: 3514
		public readonly ExPerformanceCounter OutOfHoursCalls;

		// Token: 0x04000DBB RID: 3515
		public readonly ExPerformanceCounter DisconnectedWithoutInput;

		// Token: 0x04000DBC RID: 3516
		public readonly ExPerformanceCounter TransferredCount;

		// Token: 0x04000DBD RID: 3517
		public readonly ExPerformanceCounter DirectoryAccessed;

		// Token: 0x04000DBE RID: 3518
		public readonly ExPerformanceCounter DirectoryAccessedByExtension;

		// Token: 0x04000DBF RID: 3519
		public readonly ExPerformanceCounter DirectoryAccessedByDialByName;

		// Token: 0x04000DC0 RID: 3520
		public readonly ExPerformanceCounter DirectoryAccessedSuccessfullyByDialByName;

		// Token: 0x04000DC1 RID: 3521
		public readonly ExPerformanceCounter DirectoryAccessedBySpokenName;

		// Token: 0x04000DC2 RID: 3522
		public readonly ExPerformanceCounter DirectoryAccessedSuccessfullyBySpokenName;

		// Token: 0x04000DC3 RID: 3523
		public readonly ExPerformanceCounter OperatorTransfers;

		// Token: 0x04000DC4 RID: 3524
		public readonly ExPerformanceCounter CustomMenuOptions;

		// Token: 0x04000DC5 RID: 3525
		public readonly ExPerformanceCounter MenuOption1;

		// Token: 0x04000DC6 RID: 3526
		public readonly ExPerformanceCounter MenuOption2;

		// Token: 0x04000DC7 RID: 3527
		public readonly ExPerformanceCounter MenuOption3;

		// Token: 0x04000DC8 RID: 3528
		public readonly ExPerformanceCounter MenuOption4;

		// Token: 0x04000DC9 RID: 3529
		public readonly ExPerformanceCounter MenuOption5;

		// Token: 0x04000DCA RID: 3530
		public readonly ExPerformanceCounter MenuOption6;

		// Token: 0x04000DCB RID: 3531
		public readonly ExPerformanceCounter MenuOption7;

		// Token: 0x04000DCC RID: 3532
		public readonly ExPerformanceCounter MenuOption8;

		// Token: 0x04000DCD RID: 3533
		public readonly ExPerformanceCounter MenuOption9;

		// Token: 0x04000DCE RID: 3534
		public readonly ExPerformanceCounter MenuOptionTimeout;

		// Token: 0x04000DCF RID: 3535
		public readonly ExPerformanceCounter AverageCallTime;

		// Token: 0x04000DD0 RID: 3536
		public readonly ExPerformanceCounter AverageRecentCallTime;

		// Token: 0x04000DD1 RID: 3537
		public readonly ExPerformanceCounter CallsDisconnectedOnIrrecoverableExternalError;

		// Token: 0x04000DD2 RID: 3538
		public readonly ExPerformanceCounter SpeechCalls;

		// Token: 0x04000DD3 RID: 3539
		public readonly ExPerformanceCounter AmbiguousNameTransfers;

		// Token: 0x04000DD4 RID: 3540
		public readonly ExPerformanceCounter DisallowedTransfers;

		// Token: 0x04000DD5 RID: 3541
		public readonly ExPerformanceCounter NameSpoken;

		// Token: 0x04000DD6 RID: 3542
		public readonly ExPerformanceCounter TransfersToSendMessage;

		// Token: 0x04000DD7 RID: 3543
		public readonly ExPerformanceCounter TransfersToDtmfFallbackAutoAttendant;

		// Token: 0x04000DD8 RID: 3544
		public readonly ExPerformanceCounter SentToAutoAttendant;

		// Token: 0x04000DD9 RID: 3545
		public readonly ExPerformanceCounter OperatorTransfersInitiatedByUser;

		// Token: 0x04000DDA RID: 3546
		public readonly ExPerformanceCounter PercentageSuccessfulCalls;

		// Token: 0x04000DDB RID: 3547
		public readonly ExPerformanceCounter PercentageSuccessfulCalls_Base;

		// Token: 0x04000DDC RID: 3548
		public readonly ExPerformanceCounter OperatorTransfersInitiatedByUserFromOpeningMenu;
	}
}
