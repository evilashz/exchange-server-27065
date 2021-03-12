using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging
{
	// Token: 0x02000339 RID: 825
	public static class ExTraceGlobals
	{
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x0004FDA9 File Offset: 0x0004DFA9
		public static Trace ServiceStartTracer
		{
			get
			{
				if (ExTraceGlobals.serviceStartTracer == null)
				{
					ExTraceGlobals.serviceStartTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.serviceStartTracer;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x0004FDC7 File Offset: 0x0004DFC7
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x0004FDE5 File Offset: 0x0004DFE5
		public static Trace ServiceStopTracer
		{
			get
			{
				if (ExTraceGlobals.serviceStopTracer == null)
				{
					ExTraceGlobals.serviceStopTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.serviceStopTracer;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0004FE03 File Offset: 0x0004E003
		public static Trace VoipPlatformTracer
		{
			get
			{
				if (ExTraceGlobals.voipPlatformTracer == null)
				{
					ExTraceGlobals.voipPlatformTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.voipPlatformTracer;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x0004FE21 File Offset: 0x0004E021
		public static Trace CallSessionTracer
		{
			get
			{
				if (ExTraceGlobals.callSessionTracer == null)
				{
					ExTraceGlobals.callSessionTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.callSessionTracer;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0004FE3F File Offset: 0x0004E03F
		public static Trace StackIfTracer
		{
			get
			{
				if (ExTraceGlobals.stackIfTracer == null)
				{
					ExTraceGlobals.stackIfTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.stackIfTracer;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0004FE5D File Offset: 0x0004E05D
		public static Trace StateMachineTracer
		{
			get
			{
				if (ExTraceGlobals.stateMachineTracer == null)
				{
					ExTraceGlobals.stateMachineTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.stateMachineTracer;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0004FE7B File Offset: 0x0004E07B
		public static Trace GreetingsTracer
		{
			get
			{
				if (ExTraceGlobals.greetingsTracer == null)
				{
					ExTraceGlobals.greetingsTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.greetingsTracer;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0004FE99 File Offset: 0x0004E099
		public static Trace AuthenticationTracer
		{
			get
			{
				if (ExTraceGlobals.authenticationTracer == null)
				{
					ExTraceGlobals.authenticationTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.authenticationTracer;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0004FEB7 File Offset: 0x0004E0B7
		public static Trace VoiceMailTracer
		{
			get
			{
				if (ExTraceGlobals.voiceMailTracer == null)
				{
					ExTraceGlobals.voiceMailTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.voiceMailTracer;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0004FED6 File Offset: 0x0004E0D6
		public static Trace CalendarTracer
		{
			get
			{
				if (ExTraceGlobals.calendarTracer == null)
				{
					ExTraceGlobals.calendarTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.calendarTracer;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0004FEF5 File Offset: 0x0004E0F5
		public static Trace EmailTracer
		{
			get
			{
				if (ExTraceGlobals.emailTracer == null)
				{
					ExTraceGlobals.emailTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.emailTracer;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0004FF14 File Offset: 0x0004E114
		public static Trace XsoTracer
		{
			get
			{
				if (ExTraceGlobals.xsoTracer == null)
				{
					ExTraceGlobals.xsoTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.xsoTracer;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0004FF33 File Offset: 0x0004E133
		public static Trace FaxTracer
		{
			get
			{
				if (ExTraceGlobals.faxTracer == null)
				{
					ExTraceGlobals.faxTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.faxTracer;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0004FF52 File Offset: 0x0004E152
		public static Trace AutoAttendantTracer
		{
			get
			{
				if (ExTraceGlobals.autoAttendantTracer == null)
				{
					ExTraceGlobals.autoAttendantTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.autoAttendantTracer;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0004FF71 File Offset: 0x0004E171
		public static Trace DirectorySearchTracer
		{
			get
			{
				if (ExTraceGlobals.directorySearchTracer == null)
				{
					ExTraceGlobals.directorySearchTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.directorySearchTracer;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0004FF90 File Offset: 0x0004E190
		public static Trace UtilTracer
		{
			get
			{
				if (ExTraceGlobals.utilTracer == null)
				{
					ExTraceGlobals.utilTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.utilTracer;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x0004FFAF File Offset: 0x0004E1AF
		public static Trace ClientAccessTracer
		{
			get
			{
				if (ExTraceGlobals.clientAccessTracer == null)
				{
					ExTraceGlobals.clientAccessTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.clientAccessTracer;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0004FFCE File Offset: 0x0004E1CE
		public static Trace DiagnosticTracer
		{
			get
			{
				if (ExTraceGlobals.diagnosticTracer == null)
				{
					ExTraceGlobals.diagnosticTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.diagnosticTracer;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0004FFED File Offset: 0x0004E1ED
		public static Trace OutdialingTracer
		{
			get
			{
				if (ExTraceGlobals.outdialingTracer == null)
				{
					ExTraceGlobals.outdialingTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.outdialingTracer;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0005000C File Offset: 0x0004E20C
		public static Trace SpeechAutoAttendantTracer
		{
			get
			{
				if (ExTraceGlobals.speechAutoAttendantTracer == null)
				{
					ExTraceGlobals.speechAutoAttendantTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.speechAutoAttendantTracer;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0005002B File Offset: 0x0004E22B
		public static Trace AsrContactsTracer
		{
			get
			{
				if (ExTraceGlobals.asrContactsTracer == null)
				{
					ExTraceGlobals.asrContactsTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.asrContactsTracer;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0005004A File Offset: 0x0004E24A
		public static Trace AsrSearchTracer
		{
			get
			{
				if (ExTraceGlobals.asrSearchTracer == null)
				{
					ExTraceGlobals.asrSearchTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.asrSearchTracer;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00050069 File Offset: 0x0004E269
		public static Trace PromptProvisioningTracer
		{
			get
			{
				if (ExTraceGlobals.promptProvisioningTracer == null)
				{
					ExTraceGlobals.promptProvisioningTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.promptProvisioningTracer;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00050088 File Offset: 0x0004E288
		public static Trace PFDUMCallAcceptanceTracer
		{
			get
			{
				if (ExTraceGlobals.pFDUMCallAcceptanceTracer == null)
				{
					ExTraceGlobals.pFDUMCallAcceptanceTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.pFDUMCallAcceptanceTracer;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x000500A7 File Offset: 0x0004E2A7
		public static Trace UMCertificateTracer
		{
			get
			{
				if (ExTraceGlobals.uMCertificateTracer == null)
				{
					ExTraceGlobals.uMCertificateTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.uMCertificateTracer;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x000500C6 File Offset: 0x0004E2C6
		public static Trace OCSNotifEventsTracer
		{
			get
			{
				if (ExTraceGlobals.oCSNotifEventsTracer == null)
				{
					ExTraceGlobals.oCSNotifEventsTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.oCSNotifEventsTracer;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x000500E5 File Offset: 0x0004E2E5
		public static Trace PersonalAutoAttendantTracer
		{
			get
			{
				if (ExTraceGlobals.personalAutoAttendantTracer == null)
				{
					ExTraceGlobals.personalAutoAttendantTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.personalAutoAttendantTracer;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00050104 File Offset: 0x0004E304
		public static Trace FindMeTracer
		{
			get
			{
				if (ExTraceGlobals.findMeTracer == null)
				{
					ExTraceGlobals.findMeTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.findMeTracer;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00050123 File Offset: 0x0004E323
		public static Trace MWITracer
		{
			get
			{
				if (ExTraceGlobals.mWITracer == null)
				{
					ExTraceGlobals.mWITracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.mWITracer;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00050142 File Offset: 0x0004E342
		public static Trace UMPartnerMessageTracer
		{
			get
			{
				if (ExTraceGlobals.uMPartnerMessageTracer == null)
				{
					ExTraceGlobals.uMPartnerMessageTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.uMPartnerMessageTracer;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00050161 File Offset: 0x0004E361
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00050180 File Offset: 0x0004E380
		public static Trace SipPeerManagerTracer
		{
			get
			{
				if (ExTraceGlobals.sipPeerManagerTracer == null)
				{
					ExTraceGlobals.sipPeerManagerTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.sipPeerManagerTracer;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0005019F File Offset: 0x0004E39F
		public static Trace RpcTracer
		{
			get
			{
				if (ExTraceGlobals.rpcTracer == null)
				{
					ExTraceGlobals.rpcTracer = new Trace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.rpcTracer;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x000501BE File Offset: 0x0004E3BE
		public static Trace UCMATracer
		{
			get
			{
				if (ExTraceGlobals.uCMATracer == null)
				{
					ExTraceGlobals.uCMATracer = new Trace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.uCMATracer;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x000501DD File Offset: 0x0004E3DD
		public static Trace UMReportsTracer
		{
			get
			{
				if (ExTraceGlobals.uMReportsTracer == null)
				{
					ExTraceGlobals.uMReportsTracer = new Trace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.uMReportsTracer;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x000501FC File Offset: 0x0004E3FC
		public static Trace MobileSpeechRecoTracer
		{
			get
			{
				if (ExTraceGlobals.mobileSpeechRecoTracer == null)
				{
					ExTraceGlobals.mobileSpeechRecoTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.mobileSpeechRecoTracer;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0005021B File Offset: 0x0004E41B
		public static Trace UMGrammarGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.uMGrammarGeneratorTracer == null)
				{
					ExTraceGlobals.uMGrammarGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 37);
				}
				return ExTraceGlobals.uMGrammarGeneratorTracer;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0005023A File Offset: 0x0004E43A
		public static Trace UMCallRouterTracer
		{
			get
			{
				if (ExTraceGlobals.uMCallRouterTracer == null)
				{
					ExTraceGlobals.uMCallRouterTracer = new Trace(ExTraceGlobals.componentGuid, 38);
				}
				return ExTraceGlobals.uMCallRouterTracer;
			}
		}

		// Token: 0x04001768 RID: 5992
		private static Guid componentGuid = new Guid("321b4079-df13-45c3-bbc9-2c610013dff4");

		// Token: 0x04001769 RID: 5993
		private static Trace serviceStartTracer = null;

		// Token: 0x0400176A RID: 5994
		private static Trace serviceTracer = null;

		// Token: 0x0400176B RID: 5995
		private static Trace serviceStopTracer = null;

		// Token: 0x0400176C RID: 5996
		private static Trace voipPlatformTracer = null;

		// Token: 0x0400176D RID: 5997
		private static Trace callSessionTracer = null;

		// Token: 0x0400176E RID: 5998
		private static Trace stackIfTracer = null;

		// Token: 0x0400176F RID: 5999
		private static Trace stateMachineTracer = null;

		// Token: 0x04001770 RID: 6000
		private static Trace greetingsTracer = null;

		// Token: 0x04001771 RID: 6001
		private static Trace authenticationTracer = null;

		// Token: 0x04001772 RID: 6002
		private static Trace voiceMailTracer = null;

		// Token: 0x04001773 RID: 6003
		private static Trace calendarTracer = null;

		// Token: 0x04001774 RID: 6004
		private static Trace emailTracer = null;

		// Token: 0x04001775 RID: 6005
		private static Trace xsoTracer = null;

		// Token: 0x04001776 RID: 6006
		private static Trace faxTracer = null;

		// Token: 0x04001777 RID: 6007
		private static Trace autoAttendantTracer = null;

		// Token: 0x04001778 RID: 6008
		private static Trace directorySearchTracer = null;

		// Token: 0x04001779 RID: 6009
		private static Trace utilTracer = null;

		// Token: 0x0400177A RID: 6010
		private static Trace clientAccessTracer = null;

		// Token: 0x0400177B RID: 6011
		private static Trace diagnosticTracer = null;

		// Token: 0x0400177C RID: 6012
		private static Trace outdialingTracer = null;

		// Token: 0x0400177D RID: 6013
		private static Trace speechAutoAttendantTracer = null;

		// Token: 0x0400177E RID: 6014
		private static Trace asrContactsTracer = null;

		// Token: 0x0400177F RID: 6015
		private static Trace asrSearchTracer = null;

		// Token: 0x04001780 RID: 6016
		private static Trace promptProvisioningTracer = null;

		// Token: 0x04001781 RID: 6017
		private static Trace pFDUMCallAcceptanceTracer = null;

		// Token: 0x04001782 RID: 6018
		private static Trace uMCertificateTracer = null;

		// Token: 0x04001783 RID: 6019
		private static Trace oCSNotifEventsTracer = null;

		// Token: 0x04001784 RID: 6020
		private static Trace personalAutoAttendantTracer = null;

		// Token: 0x04001785 RID: 6021
		private static Trace findMeTracer = null;

		// Token: 0x04001786 RID: 6022
		private static Trace mWITracer = null;

		// Token: 0x04001787 RID: 6023
		private static Trace uMPartnerMessageTracer = null;

		// Token: 0x04001788 RID: 6024
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001789 RID: 6025
		private static Trace sipPeerManagerTracer = null;

		// Token: 0x0400178A RID: 6026
		private static Trace rpcTracer = null;

		// Token: 0x0400178B RID: 6027
		private static Trace uCMATracer = null;

		// Token: 0x0400178C RID: 6028
		private static Trace uMReportsTracer = null;

		// Token: 0x0400178D RID: 6029
		private static Trace mobileSpeechRecoTracer = null;

		// Token: 0x0400178E RID: 6030
		private static Trace uMGrammarGeneratorTracer = null;

		// Token: 0x0400178F RID: 6031
		private static Trace uMCallRouterTracer = null;
	}
}
