using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCore.Exceptions
{
	// Token: 0x020001E9 RID: 489
	internal static class Strings
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x00036C70 File Offset: 0x00034E70
		static Strings()
		{
			Strings.stringIDs.Add(3474105973U, "ResolveCallerStage");
			Strings.stringIDs.Add(4197870756U, "NoDialPlanFound");
			Strings.stringIDs.Add(170519939U, "DialPlanNotFound_RetireTime");
			Strings.stringIDs.Add(537191075U, "SpeechServiceNotRunning");
			Strings.stringIDs.Add(1647771799U, "WatsoningDueToWorkerProcessNotTerminating");
			Strings.stringIDs.Add(172656460U, "MediaEdgeResourceAllocationFailed");
			Strings.stringIDs.Add(1109759235U, "MediaEdgeAuthenticationServiceDiscoveryFailed");
			Strings.stringIDs.Add(2357624513U, "PartnerGatewayNotFoundError");
			Strings.stringIDs.Add(1237921669U, "FailedQueueingWorkItemException");
			Strings.stringIDs.Add(2767459964U, "NonFunctionalAsrAA");
			Strings.stringIDs.Add(567544794U, "MobileRecoDispatcherStopping");
			Strings.stringIDs.Add(2663630261U, "SearchFolderVerificationStage");
			Strings.stringIDs.Add(2606993156U, "WatsoningDueToTimeout");
			Strings.stringIDs.Add(3371889563U, "WorkItemNeedsToBeRequeued");
			Strings.stringIDs.Add(3952240266U, "PingNoResponse");
			Strings.stringIDs.Add(3373645747U, "DialPlanObjectInvalid");
			Strings.stringIDs.Add(485458581U, "UMWorkerProcessNotAvailableError");
			Strings.stringIDs.Add(898152416U, "HeavyBlockingOperationException");
			Strings.stringIDs.Add(452693103U, "TCPOnly");
			Strings.stringIDs.Add(1961615598U, "MediaEdgeAuthenticationServiceCredentialsAcquisitionFailed");
			Strings.stringIDs.Add(1039614757U, "SipEndpointStartFailure");
			Strings.stringIDs.Add(1272750593U, "TwoExpressions");
			Strings.stringIDs.Add(2320007644U, "TransferTargetPhone");
			Strings.stringIDs.Add(189751189U, "MobileRecoDispatcherNotInitialized");
			Strings.stringIDs.Add(4029710960U, "BusinessLocationDefaultMenuName");
			Strings.stringIDs.Add(4262408002U, "NoValidResultsException");
			Strings.stringIDs.Add(320703422U, "NoSpeechDetectedException");
			Strings.stringIDs.Add(3953874673U, "UMServerDisabled");
			Strings.stringIDs.Add(3024498600U, "DisabledAA");
			Strings.stringIDs.Add(3421894090U, "ConfigurationStage");
			Strings.stringIDs.Add(1403090333U, "IPv6Only");
			Strings.stringIDs.Add(1395597532U, "ExpressionUnaryOp");
			Strings.stringIDs.Add(3380825748U, "SIPAccessServiceNotSet");
			Strings.stringIDs.Add(752583827U, "SIPSessionBorderControllerNotSet");
			Strings.stringIDs.Add(2059145634U, "InvalidSyntax");
			Strings.stringIDs.Add(339680743U, "TLSOnly");
			Strings.stringIDs.Add(1513784523U, "MediaEdgeFipsEncryptionNegotiationFailure");
			Strings.stringIDs.Add(3959337510U, "InvalidRequest");
			Strings.stringIDs.Add(697886779U, "NonFunctionalDtmfAA");
			Strings.stringIDs.Add(446476468U, "InvalidDefaultMailboxAA");
			Strings.stringIDs.Add(1862730821U, "MediaEdgeCredentialsRejected");
			Strings.stringIDs.Add(1630784093U, "AVAuthenticationServiceNotSet");
			Strings.stringIDs.Add(2954555563U, "IllegalVoipProvider");
			Strings.stringIDs.Add(1691255536U, "OperatorBinaryOp");
			Strings.stringIDs.Add(3923867977U, "PipelineCleanupGeneratedWatson");
			Strings.stringIDs.Add(95483037U, "MediaEdgeChannelEstablishmentUnknown");
			Strings.stringIDs.Add(3197238929U, "MediaEdgeResourceAllocationUnknown");
			Strings.stringIDs.Add(3039625362U, "MediaEdgeDnsResolutionFailure");
			Strings.stringIDs.Add(3718706677U, "ExpressionLeftParen");
			Strings.stringIDs.Add(2450261225U, "OutboundCallCancelled");
			Strings.stringIDs.Add(1928440680U, "UnknownNode");
			Strings.stringIDs.Add(3612559634U, "TransferTargetHost");
			Strings.stringIDs.Add(255737285U, "CacheRefreshInitialization");
			Strings.stringIDs.Add(3342577741U, "Blind");
			Strings.stringIDs.Add(2537385312U, "OperatorRightParen");
			Strings.stringIDs.Add(1382974549U, "NotificationEventFormatException");
			Strings.stringIDs.Add(861740637U, "SmtpSubmissionFailed");
			Strings.stringIDs.Add(3990320669U, "WatsoningDueToRecycling");
			Strings.stringIDs.Add(681134424U, "PipelineInitialization");
			Strings.stringIDs.Add(2918939034U, "TCPnTLS");
			Strings.stringIDs.Add(352018919U, "IPv4Only");
			Strings.stringIDs.Add(3801336377U, "MediaEdgeConnectionFailure");
			Strings.stringIDs.Add(2520682044U, "Supervised");
			Strings.stringIDs.Add(890268669U, "SourceStringInvalid");
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000371AC File Offset: 0x000353AC
		public static LocalizedString LegacyMailboxesNotSupported(string organizationId, string callId)
		{
			return new LocalizedString("LegacyMailboxesNotSupported", Strings.ResourceManager, new object[]
			{
				organizationId,
				callId
			});
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000371D8 File Offset: 0x000353D8
		public static LocalizedString InvalidFileInVoiceMailSubmissionFolder(string file, string error)
		{
			return new LocalizedString("InvalidFileInVoiceMailSubmissionFolder", Strings.ResourceManager, new object[]
			{
				file,
				error
			});
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00037204 File Offset: 0x00035404
		public static LocalizedString ResolveCallerStage
		{
			get
			{
				return new LocalizedString("ResolveCallerStage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0003721B File Offset: 0x0003541B
		public static LocalizedString NoDialPlanFound
		{
			get
			{
				return new LocalizedString("NoDialPlanFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00037234 File Offset: 0x00035434
		public static LocalizedString PingSummaryLine(string peer, int responseCode, string responseText, string diagnostics)
		{
			return new LocalizedString("PingSummaryLine", Strings.ResourceManager, new object[]
			{
				peer,
				responseCode,
				responseText,
				diagnostics
			});
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0003726D File Offset: 0x0003546D
		public static LocalizedString DialPlanNotFound_RetireTime
		{
			get
			{
				return new LocalizedString("DialPlanNotFound_RetireTime", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00037284 File Offset: 0x00035484
		public static LocalizedString FsmModuleNotFound(string module, string file)
		{
			return new LocalizedString("FsmModuleNotFound", Strings.ResourceManager, new object[]
			{
				module,
				file
			});
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000372B0 File Offset: 0x000354B0
		public static LocalizedString UnknownFirstActivityId(string manager)
		{
			return new LocalizedString("UnknownFirstActivityId", Strings.ResourceManager, new object[]
			{
				manager
			});
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000372D8 File Offset: 0x000354D8
		public static LocalizedString InvalidObjectGuidException(string smtpAddress)
		{
			return new LocalizedString("InvalidObjectGuidException", Strings.ResourceManager, new object[]
			{
				smtpAddress
			});
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00037300 File Offset: 0x00035500
		public static LocalizedString InvalidPromptResourceId(string statementId)
		{
			return new LocalizedString("InvalidPromptResourceId", Strings.ResourceManager, new object[]
			{
				statementId
			});
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00037328 File Offset: 0x00035528
		public static LocalizedString InvalidRecoEventDeclaration(string path, string rule)
		{
			return new LocalizedString("InvalidRecoEventDeclaration", Strings.ResourceManager, new object[]
			{
				path,
				rule
			});
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00037354 File Offset: 0x00035554
		public static LocalizedString UnableToInitializeResource(string reason)
		{
			return new LocalizedString("UnableToInitializeResource", Strings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0003737C File Offset: 0x0003557C
		public static LocalizedString SpeechServiceNotRunning
		{
			get
			{
				return new LocalizedString("SpeechServiceNotRunning", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00037394 File Offset: 0x00035594
		public static LocalizedString ExpressionSyntaxException(string error)
		{
			return new LocalizedString("ExpressionSyntaxException", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x000373BC File Offset: 0x000355BC
		public static LocalizedString WatsoningDueToWorkerProcessNotTerminating
		{
			get
			{
				return new LocalizedString("WatsoningDueToWorkerProcessNotTerminating", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000373D4 File Offset: 0x000355D4
		public static LocalizedString InvalidVariable(string varName)
		{
			return new LocalizedString("InvalidVariable", Strings.ResourceManager, new object[]
			{
				varName
			});
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000373FC File Offset: 0x000355FC
		public static LocalizedString InvalidCondition(string conditionName)
		{
			return new LocalizedString("InvalidCondition", Strings.ResourceManager, new object[]
			{
				conditionName
			});
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x00037424 File Offset: 0x00035624
		public static LocalizedString MediaEdgeResourceAllocationFailed
		{
			get
			{
				return new LocalizedString("MediaEdgeResourceAllocationFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0003743B File Offset: 0x0003563B
		public static LocalizedString MediaEdgeAuthenticationServiceDiscoveryFailed
		{
			get
			{
				return new LocalizedString("MediaEdgeAuthenticationServiceDiscoveryFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00037452 File Offset: 0x00035652
		public static LocalizedString PartnerGatewayNotFoundError
		{
			get
			{
				return new LocalizedString("PartnerGatewayNotFoundError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003746C File Offset: 0x0003566C
		public static LocalizedString PipelineFull(string user)
		{
			return new LocalizedString("PipelineFull", Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00037494 File Offset: 0x00035694
		public static LocalizedString InvalidTCPPort(string port)
		{
			return new LocalizedString("InvalidTCPPort", Strings.ResourceManager, new object[]
			{
				port
			});
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000374BC File Offset: 0x000356BC
		public static LocalizedString DelayedPingResponse(double pingTime)
		{
			return new LocalizedString("DelayedPingResponse", Strings.ResourceManager, new object[]
			{
				pingTime
			});
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x000374E9 File Offset: 0x000356E9
		public static LocalizedString FailedQueueingWorkItemException
		{
			get
			{
				return new LocalizedString("FailedQueueingWorkItemException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00037500 File Offset: 0x00035700
		public static LocalizedString UnableToFindCertificate(string thumbprint, string server)
		{
			return new LocalizedString("UnableToFindCertificate", Strings.ResourceManager, new object[]
			{
				thumbprint,
				server
			});
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003752C File Offset: 0x0003572C
		public static LocalizedString SpeechGrammarFetchErrorException(string grammar)
		{
			return new LocalizedString("SpeechGrammarFetchErrorException", Strings.ResourceManager, new object[]
			{
				grammar
			});
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00037554 File Offset: 0x00035754
		public static LocalizedString CallFromInvalidGateway(string gatewayAddress)
		{
			return new LocalizedString("CallFromInvalidGateway", Strings.ResourceManager, new object[]
			{
				gatewayAddress
			});
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0003757C File Offset: 0x0003577C
		public static LocalizedString UndeclaredRecoEventName(string path, string rule, string name)
		{
			return new LocalizedString("UndeclaredRecoEventName", Strings.ResourceManager, new object[]
			{
				path,
				rule,
				name
			});
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000375AC File Offset: 0x000357AC
		public static LocalizedString FreeDiskSpaceLimitExceeded(long available, long limit)
		{
			return new LocalizedString("FreeDiskSpaceLimitExceeded", Strings.ResourceManager, new object[]
			{
				available,
				limit
			});
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000375E4 File Offset: 0x000357E4
		public static LocalizedString FileNotFound(string path)
		{
			return new LocalizedString("FileNotFound", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0003760C File Offset: 0x0003580C
		public static LocalizedString PromptParameterCondition(string statementId)
		{
			return new LocalizedString("PromptParameterCondition", Strings.ResourceManager, new object[]
			{
				statementId
			});
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00037634 File Offset: 0x00035834
		public static LocalizedString UnKnownManager(string manager)
		{
			return new LocalizedString("UnKnownManager", Strings.ResourceManager, new object[]
			{
				manager
			});
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0003765C File Offset: 0x0003585C
		public static LocalizedString NonFunctionalAsrAA
		{
			get
			{
				return new LocalizedString("NonFunctionalAsrAA", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00037674 File Offset: 0x00035874
		public static LocalizedString MinDtmfNotZeroWithNoKey(string id)
		{
			return new LocalizedString("MinDtmfNotZeroWithNoKey", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0003769C File Offset: 0x0003589C
		public static LocalizedString MobileRecoDispatcherStopping
		{
			get
			{
				return new LocalizedString("MobileRecoDispatcherStopping", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x000376B3 File Offset: 0x000358B3
		public static LocalizedString SearchFolderVerificationStage
		{
			get
			{
				return new LocalizedString("SearchFolderVerificationStage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000376CC File Offset: 0x000358CC
		public static LocalizedString CacheRefreshADDeleteNotification(string name)
		{
			return new LocalizedString("CacheRefreshADDeleteNotification", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x000376F4 File Offset: 0x000358F4
		public static LocalizedString WatsoningDueToTimeout
		{
			get
			{
				return new LocalizedString("WatsoningDueToTimeout", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003770C File Offset: 0x0003590C
		public static LocalizedString DuplicateGrammarRule(string path, string rule)
		{
			return new LocalizedString("DuplicateGrammarRule", Strings.ResourceManager, new object[]
			{
				path,
				rule
			});
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00037738 File Offset: 0x00035938
		public static LocalizedString SpeechGrammarFetchTimeoutException(string grammar)
		{
			return new LocalizedString("SpeechGrammarFetchTimeoutException", Strings.ResourceManager, new object[]
			{
				grammar
			});
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00037760 File Offset: 0x00035960
		public static LocalizedString OCFeatureInvalidItemId(string itemId)
		{
			return new LocalizedString("OCFeatureInvalidItemId", Strings.ResourceManager, new object[]
			{
				itemId
			});
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00037788 File Offset: 0x00035988
		public static LocalizedString WorkItemNeedsToBeRequeued
		{
			get
			{
				return new LocalizedString("WorkItemNeedsToBeRequeued", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0003779F File Offset: 0x0003599F
		public static LocalizedString PingNoResponse
		{
			get
			{
				return new LocalizedString("PingNoResponse", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x000377B6 File Offset: 0x000359B6
		public static LocalizedString DialPlanObjectInvalid
		{
			get
			{
				return new LocalizedString("DialPlanObjectInvalid", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x000377D0 File Offset: 0x000359D0
		public static LocalizedString MobileRecoRPCShutdownException(Guid id)
		{
			return new LocalizedString("MobileRecoRPCShutdownException", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00037800 File Offset: 0x00035A00
		public static LocalizedString MaxCallsLimitReached(int numCalls)
		{
			return new LocalizedString("MaxCallsLimitReached", Strings.ResourceManager, new object[]
			{
				numCalls
			});
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00037830 File Offset: 0x00035A30
		public static LocalizedString InvalidPerfCounterException(string counterName)
		{
			return new LocalizedString("InvalidPerfCounterException", Strings.ResourceManager, new object[]
			{
				counterName
			});
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00037858 File Offset: 0x00035A58
		public static LocalizedString UMWorkerProcessNotAvailableError
		{
			get
			{
				return new LocalizedString("UMWorkerProcessNotAvailableError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00037870 File Offset: 0x00035A70
		public static LocalizedString ErrorChangingCertificates(string service, string server)
		{
			return new LocalizedString("ErrorChangingCertificates", Strings.ResourceManager, new object[]
			{
				service,
				server
			});
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0003789C File Offset: 0x00035A9C
		public static LocalizedString HeavyBlockingOperationException
		{
			get
			{
				return new LocalizedString("HeavyBlockingOperationException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x000378B4 File Offset: 0x00035AB4
		public static LocalizedString MailboxUnavailableException(string messageType, string database, string exceptionMessage)
		{
			return new LocalizedString("MailboxUnavailableException", Strings.ResourceManager, new object[]
			{
				messageType,
				database,
				exceptionMessage
			});
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000378E4 File Offset: 0x00035AE4
		public static LocalizedString MissingMainPrompts(string id)
		{
			return new LocalizedString("MissingMainPrompts", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0003790C File Offset: 0x00035B0C
		public static LocalizedString OCFeatureInvalidLocalResourcePath(string resourcePath)
		{
			return new LocalizedString("OCFeatureInvalidLocalResourcePath", Strings.ResourceManager, new object[]
			{
				resourcePath
			});
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00037934 File Offset: 0x00035B34
		public static LocalizedString TCPOnly
		{
			get
			{
				return new LocalizedString("TCPOnly", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003794C File Offset: 0x00035B4C
		public static LocalizedString CallFromUnknownTcpGateway(string gatewayAddress)
		{
			return new LocalizedString("CallFromUnknownTcpGateway", Strings.ResourceManager, new object[]
			{
				gatewayAddress
			});
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00037974 File Offset: 0x00035B74
		public static LocalizedString NoValidLegacyServer(string callId, string user)
		{
			return new LocalizedString("NoValidLegacyServer", Strings.ResourceManager, new object[]
			{
				callId,
				user
			});
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000379A0 File Offset: 0x00035BA0
		public static LocalizedString FaxRequestActivityWithoutFaxRequestAccepted(string id)
		{
			return new LocalizedString("FaxRequestActivityWithoutFaxRequestAccepted", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x000379C8 File Offset: 0x00035BC8
		public static LocalizedString ObjectPromptsNotConsistent(string identity)
		{
			return new LocalizedString("ObjectPromptsNotConsistent", Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000379F0 File Offset: 0x00035BF0
		public static LocalizedString CallFromUnknownTlsGateway(string remoteEndpoint, string certThumbPrint, string certFqdns)
		{
			return new LocalizedString("CallFromUnknownTlsGateway", Strings.ResourceManager, new object[]
			{
				remoteEndpoint,
				certThumbPrint,
				certFqdns
			});
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00037A20 File Offset: 0x00035C20
		public static LocalizedString MediaEdgeAuthenticationServiceCredentialsAcquisitionFailed
		{
			get
			{
				return new LocalizedString("MediaEdgeAuthenticationServiceCredentialsAcquisitionFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00037A38 File Offset: 0x00035C38
		public static LocalizedString UnknownGrammarRule(string path, string rule)
		{
			return new LocalizedString("UnknownGrammarRule", Strings.ResourceManager, new object[]
			{
				path,
				rule
			});
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00037A64 File Offset: 0x00035C64
		public static LocalizedString SipEndpointStartFailure
		{
			get
			{
				return new LocalizedString("SipEndpointStartFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00037A7B File Offset: 0x00035C7B
		public static LocalizedString TwoExpressions
		{
			get
			{
				return new LocalizedString("TwoExpressions", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00037A94 File Offset: 0x00035C94
		public static LocalizedString ErrorLookingUpActiveMailboxServer(string user, string callId)
		{
			return new LocalizedString("ErrorLookingUpActiveMailboxServer", Strings.ResourceManager, new object[]
			{
				user,
				callId
			});
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00037AC0 File Offset: 0x00035CC0
		public static LocalizedString TransferTargetPhone
		{
			get
			{
				return new LocalizedString("TransferTargetPhone", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00037AD8 File Offset: 0x00035CD8
		public static LocalizedString InvalidAction(string action)
		{
			return new LocalizedString("InvalidAction", Strings.ResourceManager, new object[]
			{
				action
			});
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00037B00 File Offset: 0x00035D00
		public static LocalizedString UnknownTransitionId(string id, string source)
		{
			return new LocalizedString("UnknownTransitionId", Strings.ResourceManager, new object[]
			{
				id,
				source
			});
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00037B2C File Offset: 0x00035D2C
		public static LocalizedString UnexpectedToken(string token)
		{
			return new LocalizedString("UnexpectedToken", Strings.ResourceManager, new object[]
			{
				token
			});
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00037B54 File Offset: 0x00035D54
		public static LocalizedString HeaderFileArgumentInvalid(string argName)
		{
			return new LocalizedString("HeaderFileArgumentInvalid", Strings.ResourceManager, new object[]
			{
				argName
			});
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00037B7C File Offset: 0x00035D7C
		public static LocalizedString DuplicateCondition(string eventId)
		{
			return new LocalizedString("DuplicateCondition", Strings.ResourceManager, new object[]
			{
				eventId
			});
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00037BA4 File Offset: 0x00035DA4
		public static LocalizedString MobileRecoDispatcherNotInitialized
		{
			get
			{
				return new LocalizedString("MobileRecoDispatcherNotInitialized", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00037BBC File Offset: 0x00035DBC
		public static LocalizedString InvalidDiversionReceived(string diversion)
		{
			return new LocalizedString("InvalidDiversionReceived", Strings.ResourceManager, new object[]
			{
				diversion
			});
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00037BE4 File Offset: 0x00035DE4
		public static LocalizedString BusinessLocationDefaultMenuName
		{
			get
			{
				return new LocalizedString("BusinessLocationDefaultMenuName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00037BFB File Offset: 0x00035DFB
		public static LocalizedString NoValidResultsException
		{
			get
			{
				return new LocalizedString("NoValidResultsException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00037C12 File Offset: 0x00035E12
		public static LocalizedString NoSpeechDetectedException
		{
			get
			{
				return new LocalizedString("NoSpeechDetectedException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00037C2C File Offset: 0x00035E2C
		public static LocalizedString MissingRequiredTransition(string id, string transition)
		{
			return new LocalizedString("MissingRequiredTransition", Strings.ResourceManager, new object[]
			{
				id,
				transition
			});
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00037C58 File Offset: 0x00035E58
		public static LocalizedString FreeDiskSpaceLimitWarning(long available, long limit, long warning)
		{
			return new LocalizedString("FreeDiskSpaceLimitWarning", Strings.ResourceManager, new object[]
			{
				available,
				limit,
				warning
			});
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00037C98 File Offset: 0x00035E98
		public static LocalizedString MinDtmfZeroWithoutNoKey(string id)
		{
			return new LocalizedString("MinDtmfZeroWithoutNoKey", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00037CC0 File Offset: 0x00035EC0
		public static LocalizedString UMServerDisabled
		{
			get
			{
				return new LocalizedString("UMServerDisabled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00037CD8 File Offset: 0x00035ED8
		public static LocalizedString MinNumericGreaterThanMax(string id)
		{
			return new LocalizedString("MinNumericGreaterThanMax", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00037D00 File Offset: 0x00035F00
		public static LocalizedString InputTimeoutLessThanInterdigit(string id)
		{
			return new LocalizedString("InputTimeoutLessThanInterdigit", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00037D28 File Offset: 0x00035F28
		public static LocalizedString DuplicateRecoRequestId(Guid id)
		{
			return new LocalizedString("DuplicateRecoRequestId", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00037D55 File Offset: 0x00035F55
		public static LocalizedString DisabledAA
		{
			get
			{
				return new LocalizedString("DisabledAA", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x00037D6C File Offset: 0x00035F6C
		public static LocalizedString ConfigurationStage
		{
			get
			{
				return new LocalizedString("ConfigurationStage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00037D84 File Offset: 0x00035F84
		public static LocalizedString ServerNotAssociatedWithDialPlan(string dialplan)
		{
			return new LocalizedString("ServerNotAssociatedWithDialPlan", Strings.ResourceManager, new object[]
			{
				dialplan
			});
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x00037DAC File Offset: 0x00035FAC
		public static LocalizedString IPv6Only
		{
			get
			{
				return new LocalizedString("IPv6Only", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00037DC4 File Offset: 0x00035FC4
		public static LocalizedString ReachMaxProcessedTimes(string argName)
		{
			return new LocalizedString("ReachMaxProcessedTimes", Strings.ResourceManager, new object[]
			{
				argName
			});
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x00037DEC File Offset: 0x00035FEC
		public static LocalizedString ExpressionUnaryOp
		{
			get
			{
				return new LocalizedString("ExpressionUnaryOp", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00037E04 File Offset: 0x00036004
		public static LocalizedString MinDtmfGreaterThanMax(string id)
		{
			return new LocalizedString("MinDtmfGreaterThanMax", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00037E2C File Offset: 0x0003602C
		public static LocalizedString SIPAccessServiceNotSet
		{
			get
			{
				return new LocalizedString("SIPAccessServiceNotSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00037E43 File Offset: 0x00036043
		public static LocalizedString SIPSessionBorderControllerNotSet
		{
			get
			{
				return new LocalizedString("SIPSessionBorderControllerNotSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00037E5C File Offset: 0x0003605C
		public static LocalizedString CallFromInvalidHuntGroup(string huntGroup, string gatewayAddress)
		{
			return new LocalizedString("CallFromInvalidHuntGroup", Strings.ResourceManager, new object[]
			{
				huntGroup,
				gatewayAddress
			});
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00037E88 File Offset: 0x00036088
		public static LocalizedString OCFeatureDataValidation(LocalizedString info)
		{
			return new LocalizedString("OCFeatureDataValidation", Strings.ResourceManager, new object[]
			{
				info
			});
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00037EB8 File Offset: 0x000360B8
		public static LocalizedString InvalidNestedPrompt(string promptName, string promptTYpe)
		{
			return new LocalizedString("InvalidNestedPrompt", Strings.ResourceManager, new object[]
			{
				promptName,
				promptTYpe
			});
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x00037EE4 File Offset: 0x000360E4
		public static LocalizedString InvalidSyntax
		{
			get
			{
				return new LocalizedString("InvalidSyntax", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x00037EFB File Offset: 0x000360FB
		public static LocalizedString TLSOnly
		{
			get
			{
				return new LocalizedString("TLSOnly", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00037F12 File Offset: 0x00036112
		public static LocalizedString MediaEdgeFipsEncryptionNegotiationFailure
		{
			get
			{
				return new LocalizedString("MediaEdgeFipsEncryptionNegotiationFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x00037F29 File Offset: 0x00036129
		public static LocalizedString InvalidRequest
		{
			get
			{
				return new LocalizedString("InvalidRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00037F40 File Offset: 0x00036140
		public static LocalizedString InvalidSIPHeader(string request, string header, string value)
		{
			return new LocalizedString("InvalidSIPHeader", Strings.ResourceManager, new object[]
			{
				request,
				header,
				value
			});
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00037F70 File Offset: 0x00036170
		public static LocalizedString DuplicateTransition(string id, string eventId)
		{
			return new LocalizedString("DuplicateTransition", Strings.ResourceManager, new object[]
			{
				id,
				eventId
			});
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00037F9C File Offset: 0x0003619C
		public static LocalizedString NoGrammarCapableMailbox(string organizationId, string callId)
		{
			return new LocalizedString("NoGrammarCapableMailbox", Strings.ResourceManager, new object[]
			{
				organizationId,
				callId
			});
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00037FC8 File Offset: 0x000361C8
		public static LocalizedString InvalidGrammarResourceId(string statementId)
		{
			return new LocalizedString("InvalidGrammarResourceId", Strings.ResourceManager, new object[]
			{
				statementId
			});
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00037FF0 File Offset: 0x000361F0
		public static LocalizedString NonFunctionalDtmfAA
		{
			get
			{
				return new LocalizedString("NonFunctionalDtmfAA", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00038007 File Offset: 0x00036207
		public static LocalizedString InvalidDefaultMailboxAA
		{
			get
			{
				return new LocalizedString("InvalidDefaultMailboxAA", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00038020 File Offset: 0x00036220
		public static LocalizedString UnexpectedSwitchValueException(string enumValue)
		{
			return new LocalizedString("UnexpectedSwitchValueException", Strings.ResourceManager, new object[]
			{
				enumValue
			});
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00038048 File Offset: 0x00036248
		public static LocalizedString MissingRecoEventDeclaration(string path, string ruleName)
		{
			return new LocalizedString("MissingRecoEventDeclaration", Strings.ResourceManager, new object[]
			{
				path,
				ruleName
			});
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00038074 File Offset: 0x00036274
		public static LocalizedString StateMachineHalted(string id)
		{
			return new LocalizedString("StateMachineHalted", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0003809C File Offset: 0x0003629C
		public static LocalizedString SpeechGrammarLoadException(string grammar)
		{
			return new LocalizedString("SpeechGrammarLoadException", Strings.ResourceManager, new object[]
			{
				grammar
			});
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000380C4 File Offset: 0x000362C4
		public static LocalizedString InvalidParseState(string id, string node, string state)
		{
			return new LocalizedString("InvalidParseState", Strings.ResourceManager, new object[]
			{
				id,
				node,
				state
			});
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x000380F4 File Offset: 0x000362F4
		public static LocalizedString KillWorkItemInvalidGuid(string filename)
		{
			return new LocalizedString("KillWorkItemInvalidGuid", Strings.ResourceManager, new object[]
			{
				filename
			});
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0003811C File Offset: 0x0003631C
		public static LocalizedString MediaEdgeCredentialsRejected
		{
			get
			{
				return new LocalizedString("MediaEdgeCredentialsRejected", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00038134 File Offset: 0x00036334
		public static LocalizedString Ports(int port1, int port2)
		{
			return new LocalizedString("Ports", Strings.ResourceManager, new object[]
			{
				port1,
				port2
			});
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0003816C File Offset: 0x0003636C
		public static LocalizedString EDiscoveryMailboxFull(string name, string exception)
		{
			return new LocalizedString("EDiscoveryMailboxFull", Strings.ResourceManager, new object[]
			{
				name,
				exception
			});
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00038198 File Offset: 0x00036398
		public static LocalizedString AVAuthenticationServiceNotSet
		{
			get
			{
				return new LocalizedString("AVAuthenticationServiceNotSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x000381B0 File Offset: 0x000363B0
		public static LocalizedString DiagnosticCallFromRemoteHost(string gatewayAddress)
		{
			return new LocalizedString("DiagnosticCallFromRemoteHost", Strings.ResourceManager, new object[]
			{
				gatewayAddress
			});
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x000381D8 File Offset: 0x000363D8
		public static LocalizedString IllegalVoipProvider
		{
			get
			{
				return new LocalizedString("IllegalVoipProvider", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x000381EF File Offset: 0x000363EF
		public static LocalizedString OperatorBinaryOp
		{
			get
			{
				return new LocalizedString("OperatorBinaryOp", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00038208 File Offset: 0x00036408
		public static LocalizedString UMServerNotFoundinAD(string serverFqdn)
		{
			return new LocalizedString("UMServerNotFoundinAD", Strings.ResourceManager, new object[]
			{
				serverFqdn
			});
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00038230 File Offset: 0x00036430
		public static LocalizedString ExpressionException(string error)
		{
			return new LocalizedString("ExpressionException", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00038258 File Offset: 0x00036458
		public static LocalizedString PersonalContactsSpeechGrammarTimeoutException(string user)
		{
			return new LocalizedString("PersonalContactsSpeechGrammarTimeoutException", Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00038280 File Offset: 0x00036480
		public static LocalizedString UnexpectedSymbol(string symbol)
		{
			return new LocalizedString("UnexpectedSymbol", Strings.ResourceManager, new object[]
			{
				symbol
			});
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000382A8 File Offset: 0x000364A8
		public static LocalizedString KillWorkItemHeaderFileNotExist(string headerfile)
		{
			return new LocalizedString("KillWorkItemHeaderFileNotExist", Strings.ResourceManager, new object[]
			{
				headerfile
			});
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x000382D0 File Offset: 0x000364D0
		public static LocalizedString GrammarFileNotFoundException(string grammarFile)
		{
			return new LocalizedString("GrammarFileNotFoundException", Strings.ResourceManager, new object[]
			{
				grammarFile
			});
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x000382F8 File Offset: 0x000364F8
		public static LocalizedString ExpiredCertificate(string thumbprint, string server)
		{
			return new LocalizedString("ExpiredCertificate", Strings.ResourceManager, new object[]
			{
				thumbprint,
				server
			});
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00038324 File Offset: 0x00036524
		public static LocalizedString DuplicateScopedId(string id)
		{
			return new LocalizedString("DuplicateScopedId", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0003834C File Offset: 0x0003654C
		public static LocalizedString PersonalContactsSpeechGrammarErrorException(string user)
		{
			return new LocalizedString("PersonalContactsSpeechGrammarErrorException", Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x00038374 File Offset: 0x00036574
		public static LocalizedString PipelineCleanupGeneratedWatson
		{
			get
			{
				return new LocalizedString("PipelineCleanupGeneratedWatson", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0003838B File Offset: 0x0003658B
		public static LocalizedString MediaEdgeChannelEstablishmentUnknown
		{
			get
			{
				return new LocalizedString("MediaEdgeChannelEstablishmentUnknown", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x000383A2 File Offset: 0x000365A2
		public static LocalizedString MediaEdgeResourceAllocationUnknown
		{
			get
			{
				return new LocalizedString("MediaEdgeResourceAllocationUnknown", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000383BC File Offset: 0x000365BC
		public static LocalizedString InvalidEvent(string tevent)
		{
			return new LocalizedString("InvalidEvent", Strings.ResourceManager, new object[]
			{
				tevent
			});
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x000383E4 File Offset: 0x000365E4
		public static LocalizedString MediaEdgeDnsResolutionFailure
		{
			get
			{
				return new LocalizedString("MediaEdgeDnsResolutionFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000383FC File Offset: 0x000365FC
		public static LocalizedString MaxGreetingLengthExceededException(int greetingLength)
		{
			return new LocalizedString("MaxGreetingLengthExceededException", Strings.ResourceManager, new object[]
			{
				greetingLength
			});
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00038429 File Offset: 0x00036629
		public static LocalizedString ExpressionLeftParen
		{
			get
			{
				return new LocalizedString("ExpressionLeftParen", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00038440 File Offset: 0x00036640
		public static LocalizedString ConfigurationException(string msg)
		{
			return new LocalizedString("ConfigurationException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00038468 File Offset: 0x00036668
		public static LocalizedString UMServiceBaseException(string exceptionText)
		{
			return new LocalizedString("UMServiceBaseException", Strings.ResourceManager, new object[]
			{
				exceptionText
			});
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00038490 File Offset: 0x00036690
		public static LocalizedString UnableToRemovePermissions(string service, int errorCode)
		{
			return new LocalizedString("UnableToRemovePermissions", Strings.ResourceManager, new object[]
			{
				service,
				errorCode
			});
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000384C4 File Offset: 0x000366C4
		public static LocalizedString UnableToStopListening(string service)
		{
			return new LocalizedString("UnableToStopListening", Strings.ResourceManager, new object[]
			{
				service
			});
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x000384EC File Offset: 0x000366EC
		public static LocalizedString MaxCallsLimitReachedWarning(int currentCalls, int maxCalls)
		{
			return new LocalizedString("MaxCallsLimitReachedWarning", Strings.ResourceManager, new object[]
			{
				currentCalls,
				maxCalls
			});
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00038524 File Offset: 0x00036724
		public static LocalizedString OCFeatureCAMustHaveDiversion(string feature)
		{
			return new LocalizedString("OCFeatureCAMustHaveDiversion", Strings.ResourceManager, new object[]
			{
				feature
			});
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0003854C File Offset: 0x0003674C
		public static LocalizedString InvalidResultTypeException(string resultType)
		{
			return new LocalizedString("InvalidResultTypeException", Strings.ResourceManager, new object[]
			{
				resultType
			});
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x00038574 File Offset: 0x00036774
		public static LocalizedString OutboundCallCancelled
		{
			get
			{
				return new LocalizedString("OutboundCallCancelled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0003858C File Offset: 0x0003678C
		public static LocalizedString RecognizerNotInstalled(string engineType, string language)
		{
			return new LocalizedString("RecognizerNotInstalled", Strings.ResourceManager, new object[]
			{
				engineType,
				language
			});
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000385B8 File Offset: 0x000367B8
		public static LocalizedString ToHeaderDoesNotContainTenantGuid(string callId, string toUri)
		{
			return new LocalizedString("ToHeaderDoesNotContainTenantGuid", Strings.ResourceManager, new object[]
			{
				callId,
				toUri
			});
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x000385E4 File Offset: 0x000367E4
		public static LocalizedString InvalidQualifiedName(string name)
		{
			return new LocalizedString("InvalidQualifiedName", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0003860C File Offset: 0x0003680C
		public static LocalizedString UnknownNode
		{
			get
			{
				return new LocalizedString("UnknownNode", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00038624 File Offset: 0x00036824
		public static LocalizedString UnableToCreateCallerPropertiesException(string typeA)
		{
			return new LocalizedString("UnableToCreateCallerPropertiesException", Strings.ResourceManager, new object[]
			{
				typeA
			});
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0003864C File Offset: 0x0003684C
		public static LocalizedString RecordMissingTransitions(string id)
		{
			return new LocalizedString("RecordMissingTransitions", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00038674 File Offset: 0x00036874
		public static LocalizedString RuleNotPublic(string path, string rule)
		{
			return new LocalizedString("RuleNotPublic", Strings.ResourceManager, new object[]
			{
				path,
				rule
			});
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000386A0 File Offset: 0x000368A0
		public static LocalizedString NotificationEventSignalingException(string msg)
		{
			return new LocalizedString("NotificationEventSignalingException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000386C8 File Offset: 0x000368C8
		public static LocalizedString MissingResourcePrompt(string statementId, int lcid)
		{
			return new LocalizedString("MissingResourcePrompt", Strings.ResourceManager, new object[]
			{
				statementId,
				lcid
			});
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x000386F9 File Offset: 0x000368F9
		public static LocalizedString TransferTargetHost
		{
			get
			{
				return new LocalizedString("TransferTargetHost", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x00038710 File Offset: 0x00036910
		public static LocalizedString CacheRefreshInitialization
		{
			get
			{
				return new LocalizedString("CacheRefreshInitialization", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00038728 File Offset: 0x00036928
		public static LocalizedString InvalidActivityManager(string name)
		{
			return new LocalizedString("InvalidActivityManager", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x00038750 File Offset: 0x00036950
		public static LocalizedString Blind
		{
			get
			{
				return new LocalizedString("Blind", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00038768 File Offset: 0x00036968
		public static LocalizedString MaxMobileRecoRequestsReached(int current, int max)
		{
			return new LocalizedString("MaxMobileRecoRequestsReached", Strings.ResourceManager, new object[]
			{
				current,
				max
			});
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000387A0 File Offset: 0x000369A0
		public static LocalizedString GlobalGatewayWithNoMatch(string gatewayAddress, string pilotnumber)
		{
			return new LocalizedString("GlobalGatewayWithNoMatch", Strings.ResourceManager, new object[]
			{
				gatewayAddress,
				pilotnumber
			});
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x000387CC File Offset: 0x000369CC
		public static LocalizedString OperatorRightParen
		{
			get
			{
				return new LocalizedString("OperatorRightParen", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000387E4 File Offset: 0x000369E4
		public static LocalizedString OCFeatureSACannotHaveDiversion(string feature)
		{
			return new LocalizedString("OCFeatureSACannotHaveDiversion", Strings.ResourceManager, new object[]
			{
				feature
			});
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0003880C File Offset: 0x00036A0C
		public static LocalizedString CacheRefreshADUpdateNotification(string name)
		{
			return new LocalizedString("CacheRefreshADUpdateNotification", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00038834 File Offset: 0x00036A34
		public static LocalizedString NotificationEventFormatException
		{
			get
			{
				return new LocalizedString("NotificationEventFormatException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0003884C File Offset: 0x00036A4C
		public static LocalizedString MissingSuffixRule(string ruleName)
		{
			return new LocalizedString("MissingSuffixRule", Strings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00038874 File Offset: 0x00036A74
		public static LocalizedString EmptyRecoRequestId(Guid id)
		{
			return new LocalizedString("EmptyRecoRequestId", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000388A1 File Offset: 0x00036AA1
		public static LocalizedString SmtpSubmissionFailed
		{
			get
			{
				return new LocalizedString("SmtpSubmissionFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000388B8 File Offset: 0x00036AB8
		public static LocalizedString InvalidPromptType(string name)
		{
			return new LocalizedString("InvalidPromptType", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000388E0 File Offset: 0x00036AE0
		public static LocalizedString ConfigFileInitializationError(string file)
		{
			return new LocalizedString("ConfigFileInitializationError", Strings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00038908 File Offset: 0x00036B08
		public static LocalizedString CallCouldNotBeHandled(string callId, string remoteEndpoint)
		{
			return new LocalizedString("CallCouldNotBeHandled", Strings.ResourceManager, new object[]
			{
				callId,
				remoteEndpoint
			});
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00038934 File Offset: 0x00036B34
		public static LocalizedString InvalidRecoRequestId(Guid id)
		{
			return new LocalizedString("InvalidRecoRequestId", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00038961 File Offset: 0x00036B61
		public static LocalizedString WatsoningDueToRecycling
		{
			get
			{
				return new LocalizedString("WatsoningDueToRecycling", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00038978 File Offset: 0x00036B78
		public static LocalizedString PipelineInitialization
		{
			get
			{
				return new LocalizedString("PipelineInitialization", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00038990 File Offset: 0x00036B90
		public static LocalizedString GrammarFetcherException(string msg)
		{
			return new LocalizedString("GrammarFetcherException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x000389B8 File Offset: 0x00036BB8
		public static LocalizedString TCPnTLS
		{
			get
			{
				return new LocalizedString("TCPnTLS", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x000389D0 File Offset: 0x00036BD0
		public static LocalizedString UserNotFoundException(Guid id)
		{
			return new LocalizedString("UserNotFoundException", Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x000389FD File Offset: 0x00036BFD
		public static LocalizedString IPv4Only
		{
			get
			{
				return new LocalizedString("IPv4Only", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00038A14 File Offset: 0x00036C14
		public static LocalizedString InvalidTLSPort(string port)
		{
			return new LocalizedString("InvalidTLSPort", Strings.ResourceManager, new object[]
			{
				port
			});
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00038A3C File Offset: 0x00036C3C
		public static LocalizedString InvalidAudioStreamException(string msg)
		{
			return new LocalizedString("InvalidAudioStreamException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00038A64 File Offset: 0x00036C64
		public static LocalizedString MediaEdgeConnectionFailure
		{
			get
			{
				return new LocalizedString("MediaEdgeConnectionFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00038A7B File Offset: 0x00036C7B
		public static LocalizedString Supervised
		{
			get
			{
				return new LocalizedString("Supervised", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00038A94 File Offset: 0x00036C94
		public static LocalizedString PipelineFullWithCDRMessages(string name, string dbName)
		{
			return new LocalizedString("PipelineFullWithCDRMessages", Strings.ResourceManager, new object[]
			{
				name,
				dbName
			});
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00038AC0 File Offset: 0x00036CC0
		public static LocalizedString SourceStringInvalid
		{
			get
			{
				return new LocalizedString("SourceStringInvalid", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00038AD8 File Offset: 0x00036CD8
		public static LocalizedString InvalidOperator(string op)
		{
			return new LocalizedString("InvalidOperator", Strings.ResourceManager, new object[]
			{
				op
			});
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00038B00 File Offset: 0x00036D00
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040007B8 RID: 1976
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(64);

		// Token: 0x040007B9 RID: 1977
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.UM.UMCore.Exceptions.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x020001EA RID: 490
		public enum IDs : uint
		{
			// Token: 0x040007BB RID: 1979
			ResolveCallerStage = 3474105973U,
			// Token: 0x040007BC RID: 1980
			NoDialPlanFound = 4197870756U,
			// Token: 0x040007BD RID: 1981
			DialPlanNotFound_RetireTime = 170519939U,
			// Token: 0x040007BE RID: 1982
			SpeechServiceNotRunning = 537191075U,
			// Token: 0x040007BF RID: 1983
			WatsoningDueToWorkerProcessNotTerminating = 1647771799U,
			// Token: 0x040007C0 RID: 1984
			MediaEdgeResourceAllocationFailed = 172656460U,
			// Token: 0x040007C1 RID: 1985
			MediaEdgeAuthenticationServiceDiscoveryFailed = 1109759235U,
			// Token: 0x040007C2 RID: 1986
			PartnerGatewayNotFoundError = 2357624513U,
			// Token: 0x040007C3 RID: 1987
			FailedQueueingWorkItemException = 1237921669U,
			// Token: 0x040007C4 RID: 1988
			NonFunctionalAsrAA = 2767459964U,
			// Token: 0x040007C5 RID: 1989
			MobileRecoDispatcherStopping = 567544794U,
			// Token: 0x040007C6 RID: 1990
			SearchFolderVerificationStage = 2663630261U,
			// Token: 0x040007C7 RID: 1991
			WatsoningDueToTimeout = 2606993156U,
			// Token: 0x040007C8 RID: 1992
			WorkItemNeedsToBeRequeued = 3371889563U,
			// Token: 0x040007C9 RID: 1993
			PingNoResponse = 3952240266U,
			// Token: 0x040007CA RID: 1994
			DialPlanObjectInvalid = 3373645747U,
			// Token: 0x040007CB RID: 1995
			UMWorkerProcessNotAvailableError = 485458581U,
			// Token: 0x040007CC RID: 1996
			HeavyBlockingOperationException = 898152416U,
			// Token: 0x040007CD RID: 1997
			TCPOnly = 452693103U,
			// Token: 0x040007CE RID: 1998
			MediaEdgeAuthenticationServiceCredentialsAcquisitionFailed = 1961615598U,
			// Token: 0x040007CF RID: 1999
			SipEndpointStartFailure = 1039614757U,
			// Token: 0x040007D0 RID: 2000
			TwoExpressions = 1272750593U,
			// Token: 0x040007D1 RID: 2001
			TransferTargetPhone = 2320007644U,
			// Token: 0x040007D2 RID: 2002
			MobileRecoDispatcherNotInitialized = 189751189U,
			// Token: 0x040007D3 RID: 2003
			BusinessLocationDefaultMenuName = 4029710960U,
			// Token: 0x040007D4 RID: 2004
			NoValidResultsException = 4262408002U,
			// Token: 0x040007D5 RID: 2005
			NoSpeechDetectedException = 320703422U,
			// Token: 0x040007D6 RID: 2006
			UMServerDisabled = 3953874673U,
			// Token: 0x040007D7 RID: 2007
			DisabledAA = 3024498600U,
			// Token: 0x040007D8 RID: 2008
			ConfigurationStage = 3421894090U,
			// Token: 0x040007D9 RID: 2009
			IPv6Only = 1403090333U,
			// Token: 0x040007DA RID: 2010
			ExpressionUnaryOp = 1395597532U,
			// Token: 0x040007DB RID: 2011
			SIPAccessServiceNotSet = 3380825748U,
			// Token: 0x040007DC RID: 2012
			SIPSessionBorderControllerNotSet = 752583827U,
			// Token: 0x040007DD RID: 2013
			InvalidSyntax = 2059145634U,
			// Token: 0x040007DE RID: 2014
			TLSOnly = 339680743U,
			// Token: 0x040007DF RID: 2015
			MediaEdgeFipsEncryptionNegotiationFailure = 1513784523U,
			// Token: 0x040007E0 RID: 2016
			InvalidRequest = 3959337510U,
			// Token: 0x040007E1 RID: 2017
			NonFunctionalDtmfAA = 697886779U,
			// Token: 0x040007E2 RID: 2018
			InvalidDefaultMailboxAA = 446476468U,
			// Token: 0x040007E3 RID: 2019
			MediaEdgeCredentialsRejected = 1862730821U,
			// Token: 0x040007E4 RID: 2020
			AVAuthenticationServiceNotSet = 1630784093U,
			// Token: 0x040007E5 RID: 2021
			IllegalVoipProvider = 2954555563U,
			// Token: 0x040007E6 RID: 2022
			OperatorBinaryOp = 1691255536U,
			// Token: 0x040007E7 RID: 2023
			PipelineCleanupGeneratedWatson = 3923867977U,
			// Token: 0x040007E8 RID: 2024
			MediaEdgeChannelEstablishmentUnknown = 95483037U,
			// Token: 0x040007E9 RID: 2025
			MediaEdgeResourceAllocationUnknown = 3197238929U,
			// Token: 0x040007EA RID: 2026
			MediaEdgeDnsResolutionFailure = 3039625362U,
			// Token: 0x040007EB RID: 2027
			ExpressionLeftParen = 3718706677U,
			// Token: 0x040007EC RID: 2028
			OutboundCallCancelled = 2450261225U,
			// Token: 0x040007ED RID: 2029
			UnknownNode = 1928440680U,
			// Token: 0x040007EE RID: 2030
			TransferTargetHost = 3612559634U,
			// Token: 0x040007EF RID: 2031
			CacheRefreshInitialization = 255737285U,
			// Token: 0x040007F0 RID: 2032
			Blind = 3342577741U,
			// Token: 0x040007F1 RID: 2033
			OperatorRightParen = 2537385312U,
			// Token: 0x040007F2 RID: 2034
			NotificationEventFormatException = 1382974549U,
			// Token: 0x040007F3 RID: 2035
			SmtpSubmissionFailed = 861740637U,
			// Token: 0x040007F4 RID: 2036
			WatsoningDueToRecycling = 3990320669U,
			// Token: 0x040007F5 RID: 2037
			PipelineInitialization = 681134424U,
			// Token: 0x040007F6 RID: 2038
			TCPnTLS = 2918939034U,
			// Token: 0x040007F7 RID: 2039
			IPv4Only = 352018919U,
			// Token: 0x040007F8 RID: 2040
			MediaEdgeConnectionFailure = 3801336377U,
			// Token: 0x040007F9 RID: 2041
			Supervised = 2520682044U,
			// Token: 0x040007FA RID: 2042
			SourceStringInvalid = 890268669U
		}

		// Token: 0x020001EB RID: 491
		private enum ParamIDs
		{
			// Token: 0x040007FC RID: 2044
			LegacyMailboxesNotSupported,
			// Token: 0x040007FD RID: 2045
			InvalidFileInVoiceMailSubmissionFolder,
			// Token: 0x040007FE RID: 2046
			PingSummaryLine,
			// Token: 0x040007FF RID: 2047
			FsmModuleNotFound,
			// Token: 0x04000800 RID: 2048
			UnknownFirstActivityId,
			// Token: 0x04000801 RID: 2049
			InvalidObjectGuidException,
			// Token: 0x04000802 RID: 2050
			InvalidPromptResourceId,
			// Token: 0x04000803 RID: 2051
			InvalidRecoEventDeclaration,
			// Token: 0x04000804 RID: 2052
			UnableToInitializeResource,
			// Token: 0x04000805 RID: 2053
			ExpressionSyntaxException,
			// Token: 0x04000806 RID: 2054
			InvalidVariable,
			// Token: 0x04000807 RID: 2055
			InvalidCondition,
			// Token: 0x04000808 RID: 2056
			PipelineFull,
			// Token: 0x04000809 RID: 2057
			InvalidTCPPort,
			// Token: 0x0400080A RID: 2058
			DelayedPingResponse,
			// Token: 0x0400080B RID: 2059
			UnableToFindCertificate,
			// Token: 0x0400080C RID: 2060
			SpeechGrammarFetchErrorException,
			// Token: 0x0400080D RID: 2061
			CallFromInvalidGateway,
			// Token: 0x0400080E RID: 2062
			UndeclaredRecoEventName,
			// Token: 0x0400080F RID: 2063
			FreeDiskSpaceLimitExceeded,
			// Token: 0x04000810 RID: 2064
			FileNotFound,
			// Token: 0x04000811 RID: 2065
			PromptParameterCondition,
			// Token: 0x04000812 RID: 2066
			UnKnownManager,
			// Token: 0x04000813 RID: 2067
			MinDtmfNotZeroWithNoKey,
			// Token: 0x04000814 RID: 2068
			CacheRefreshADDeleteNotification,
			// Token: 0x04000815 RID: 2069
			DuplicateGrammarRule,
			// Token: 0x04000816 RID: 2070
			SpeechGrammarFetchTimeoutException,
			// Token: 0x04000817 RID: 2071
			OCFeatureInvalidItemId,
			// Token: 0x04000818 RID: 2072
			MobileRecoRPCShutdownException,
			// Token: 0x04000819 RID: 2073
			MaxCallsLimitReached,
			// Token: 0x0400081A RID: 2074
			InvalidPerfCounterException,
			// Token: 0x0400081B RID: 2075
			ErrorChangingCertificates,
			// Token: 0x0400081C RID: 2076
			MailboxUnavailableException,
			// Token: 0x0400081D RID: 2077
			MissingMainPrompts,
			// Token: 0x0400081E RID: 2078
			OCFeatureInvalidLocalResourcePath,
			// Token: 0x0400081F RID: 2079
			CallFromUnknownTcpGateway,
			// Token: 0x04000820 RID: 2080
			NoValidLegacyServer,
			// Token: 0x04000821 RID: 2081
			FaxRequestActivityWithoutFaxRequestAccepted,
			// Token: 0x04000822 RID: 2082
			ObjectPromptsNotConsistent,
			// Token: 0x04000823 RID: 2083
			CallFromUnknownTlsGateway,
			// Token: 0x04000824 RID: 2084
			UnknownGrammarRule,
			// Token: 0x04000825 RID: 2085
			ErrorLookingUpActiveMailboxServer,
			// Token: 0x04000826 RID: 2086
			InvalidAction,
			// Token: 0x04000827 RID: 2087
			UnknownTransitionId,
			// Token: 0x04000828 RID: 2088
			UnexpectedToken,
			// Token: 0x04000829 RID: 2089
			HeaderFileArgumentInvalid,
			// Token: 0x0400082A RID: 2090
			DuplicateCondition,
			// Token: 0x0400082B RID: 2091
			InvalidDiversionReceived,
			// Token: 0x0400082C RID: 2092
			MissingRequiredTransition,
			// Token: 0x0400082D RID: 2093
			FreeDiskSpaceLimitWarning,
			// Token: 0x0400082E RID: 2094
			MinDtmfZeroWithoutNoKey,
			// Token: 0x0400082F RID: 2095
			MinNumericGreaterThanMax,
			// Token: 0x04000830 RID: 2096
			InputTimeoutLessThanInterdigit,
			// Token: 0x04000831 RID: 2097
			DuplicateRecoRequestId,
			// Token: 0x04000832 RID: 2098
			ServerNotAssociatedWithDialPlan,
			// Token: 0x04000833 RID: 2099
			ReachMaxProcessedTimes,
			// Token: 0x04000834 RID: 2100
			MinDtmfGreaterThanMax,
			// Token: 0x04000835 RID: 2101
			CallFromInvalidHuntGroup,
			// Token: 0x04000836 RID: 2102
			OCFeatureDataValidation,
			// Token: 0x04000837 RID: 2103
			InvalidNestedPrompt,
			// Token: 0x04000838 RID: 2104
			InvalidSIPHeader,
			// Token: 0x04000839 RID: 2105
			DuplicateTransition,
			// Token: 0x0400083A RID: 2106
			NoGrammarCapableMailbox,
			// Token: 0x0400083B RID: 2107
			InvalidGrammarResourceId,
			// Token: 0x0400083C RID: 2108
			UnexpectedSwitchValueException,
			// Token: 0x0400083D RID: 2109
			MissingRecoEventDeclaration,
			// Token: 0x0400083E RID: 2110
			StateMachineHalted,
			// Token: 0x0400083F RID: 2111
			SpeechGrammarLoadException,
			// Token: 0x04000840 RID: 2112
			InvalidParseState,
			// Token: 0x04000841 RID: 2113
			KillWorkItemInvalidGuid,
			// Token: 0x04000842 RID: 2114
			Ports,
			// Token: 0x04000843 RID: 2115
			EDiscoveryMailboxFull,
			// Token: 0x04000844 RID: 2116
			DiagnosticCallFromRemoteHost,
			// Token: 0x04000845 RID: 2117
			UMServerNotFoundinAD,
			// Token: 0x04000846 RID: 2118
			ExpressionException,
			// Token: 0x04000847 RID: 2119
			PersonalContactsSpeechGrammarTimeoutException,
			// Token: 0x04000848 RID: 2120
			UnexpectedSymbol,
			// Token: 0x04000849 RID: 2121
			KillWorkItemHeaderFileNotExist,
			// Token: 0x0400084A RID: 2122
			GrammarFileNotFoundException,
			// Token: 0x0400084B RID: 2123
			ExpiredCertificate,
			// Token: 0x0400084C RID: 2124
			DuplicateScopedId,
			// Token: 0x0400084D RID: 2125
			PersonalContactsSpeechGrammarErrorException,
			// Token: 0x0400084E RID: 2126
			InvalidEvent,
			// Token: 0x0400084F RID: 2127
			MaxGreetingLengthExceededException,
			// Token: 0x04000850 RID: 2128
			ConfigurationException,
			// Token: 0x04000851 RID: 2129
			UMServiceBaseException,
			// Token: 0x04000852 RID: 2130
			UnableToRemovePermissions,
			// Token: 0x04000853 RID: 2131
			UnableToStopListening,
			// Token: 0x04000854 RID: 2132
			MaxCallsLimitReachedWarning,
			// Token: 0x04000855 RID: 2133
			OCFeatureCAMustHaveDiversion,
			// Token: 0x04000856 RID: 2134
			InvalidResultTypeException,
			// Token: 0x04000857 RID: 2135
			RecognizerNotInstalled,
			// Token: 0x04000858 RID: 2136
			ToHeaderDoesNotContainTenantGuid,
			// Token: 0x04000859 RID: 2137
			InvalidQualifiedName,
			// Token: 0x0400085A RID: 2138
			UnableToCreateCallerPropertiesException,
			// Token: 0x0400085B RID: 2139
			RecordMissingTransitions,
			// Token: 0x0400085C RID: 2140
			RuleNotPublic,
			// Token: 0x0400085D RID: 2141
			NotificationEventSignalingException,
			// Token: 0x0400085E RID: 2142
			MissingResourcePrompt,
			// Token: 0x0400085F RID: 2143
			InvalidActivityManager,
			// Token: 0x04000860 RID: 2144
			MaxMobileRecoRequestsReached,
			// Token: 0x04000861 RID: 2145
			GlobalGatewayWithNoMatch,
			// Token: 0x04000862 RID: 2146
			OCFeatureSACannotHaveDiversion,
			// Token: 0x04000863 RID: 2147
			CacheRefreshADUpdateNotification,
			// Token: 0x04000864 RID: 2148
			MissingSuffixRule,
			// Token: 0x04000865 RID: 2149
			EmptyRecoRequestId,
			// Token: 0x04000866 RID: 2150
			InvalidPromptType,
			// Token: 0x04000867 RID: 2151
			ConfigFileInitializationError,
			// Token: 0x04000868 RID: 2152
			CallCouldNotBeHandled,
			// Token: 0x04000869 RID: 2153
			InvalidRecoRequestId,
			// Token: 0x0400086A RID: 2154
			GrammarFetcherException,
			// Token: 0x0400086B RID: 2155
			UserNotFoundException,
			// Token: 0x0400086C RID: 2156
			InvalidTLSPort,
			// Token: 0x0400086D RID: 2157
			InvalidAudioStreamException,
			// Token: 0x0400086E RID: 2158
			PipelineFullWithCDRMessages,
			// Token: 0x0400086F RID: 2159
			InvalidOperator
		}
	}
}
