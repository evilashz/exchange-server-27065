using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02001210 RID: 4624
	internal static class QueueViewerStrings
	{
		// Token: 0x0600BA35 RID: 47669 RVA: 0x002A7590 File Offset: 0x002A5790
		static QueueViewerStrings()
		{
			QueueViewerStrings.stringIDs.Add(317860356U, "UnfreezeMessageTask");
			QueueViewerStrings.stringIDs.Add(2910097809U, "FreezeMessageTask");
			QueueViewerStrings.stringIDs.Add(1959773104U, "Status");
			QueueViewerStrings.stringIDs.Add(1421152990U, "Type");
			QueueViewerStrings.stringIDs.Add(3089884583U, "DateReceived");
			QueueViewerStrings.stringIDs.Add(1316090539U, "MissingName");
			QueueViewerStrings.stringIDs.Add(1572946888U, "UnfreezeQueueTask");
			QueueViewerStrings.stringIDs.Add(3695048262U, "InvalidServerData");
			QueueViewerStrings.stringIDs.Add(973091570U, "InvalidIdentityString");
			QueueViewerStrings.stringIDs.Add(3845883191U, "FreezeQueueTask");
			QueueViewerStrings.stringIDs.Add(1481046174U, "SourceConnector");
			QueueViewerStrings.stringIDs.Add(3815822302U, "InvalidServerVersion");
			QueueViewerStrings.stringIDs.Add(2112399141U, "SuccessMessageRedirectMessageRequestCompleted");
			QueueViewerStrings.stringIDs.Add(2470050549U, "TextMatchingNotSupported");
			QueueViewerStrings.stringIDs.Add(3457520377U, "Size");
			QueueViewerStrings.stringIDs.Add(1800498867U, "MissingSender");
			QueueViewerStrings.stringIDs.Add(287061521U, "Id");
			QueueViewerStrings.stringIDs.Add(4170207812U, "SetMessageResubmitMustBeTrue");
			QueueViewerStrings.stringIDs.Add(2064039061U, "MessageNextRetryTime");
			QueueViewerStrings.stringIDs.Add(337370324U, "InvalidFieldName");
			QueueViewerStrings.stringIDs.Add(3710722188U, "MessageCount");
			QueueViewerStrings.stringIDs.Add(3894225067U, "RetryCount");
			QueueViewerStrings.stringIDs.Add(1638755231U, "FilterTypeNotSupported");
			QueueViewerStrings.stringIDs.Add(3688482442U, "SourceIP");
			QueueViewerStrings.stringIDs.Add(793883224U, "RedirectMessageTask");
			QueueViewerStrings.stringIDs.Add(4178538216U, "QueueResubmitInProgress");
			QueueViewerStrings.stringIDs.Add(1686262562U, "ComparisonNotSupported");
			QueueViewerStrings.stringIDs.Add(3457614570U, "RetryQueueTask");
			QueueViewerStrings.stringIDs.Add(3644361014U, "ExpirationTime");
			QueueViewerStrings.stringIDs.Add(501948002U, "InvalidServerCollection");
			QueueViewerStrings.stringIDs.Add(4294095275U, "RedirectMessageInProgress");
			QueueViewerStrings.stringIDs.Add(2999867432U, "RemoveMessageTask");
			QueueViewerStrings.stringIDs.Add(3834431484U, "MessageLastRetryTime");
			QueueViewerStrings.stringIDs.Add(2455477494U, "InvalidIdentityForEquality");
			QueueViewerStrings.stringIDs.Add(3857609986U, "Priority");
			QueueViewerStrings.stringIDs.Add(1726666628U, "SetMessageTask");
			QueueViewerStrings.stringIDs.Add(1732412034U, "Subject");
			QueueViewerStrings.stringIDs.Add(822915892U, "OldestMessage");
			QueueViewerStrings.stringIDs.Add(3853989365U, "TooManyResults");
			QueueViewerStrings.stringIDs.Add(2816985275U, "AmbiguousParameterSet");
			QueueViewerStrings.stringIDs.Add(1967184194U, "NextRetryTime");
			QueueViewerStrings.stringIDs.Add(523788226U, "InvalidClientData");
			QueueViewerStrings.stringIDs.Add(1850287295U, "LastRetryTime");
			QueueViewerStrings.stringIDs.Add(51203499U, "Sender");
			QueueViewerStrings.stringIDs.Add(3552881163U, "MessageGrid");
		}

		// Token: 0x17003A6D RID: 14957
		// (get) Token: 0x0600BA36 RID: 47670 RVA: 0x002A7950 File Offset: 0x002A5B50
		public static LocalizedString UnfreezeMessageTask
		{
			get
			{
				return new LocalizedString("UnfreezeMessageTask", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A6E RID: 14958
		// (get) Token: 0x0600BA37 RID: 47671 RVA: 0x002A796E File Offset: 0x002A5B6E
		public static LocalizedString FreezeMessageTask
		{
			get
			{
				return new LocalizedString("FreezeMessageTask", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A6F RID: 14959
		// (get) Token: 0x0600BA38 RID: 47672 RVA: 0x002A798C File Offset: 0x002A5B8C
		public static LocalizedString Status
		{
			get
			{
				return new LocalizedString("Status", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA39 RID: 47673 RVA: 0x002A79AC File Offset: 0x002A5BAC
		public static LocalizedString ConfirmationMessageSetMessageFilter(string Filter)
		{
			return new LocalizedString("ConfirmationMessageSetMessageFilter", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				Filter
			});
		}

		// Token: 0x17003A70 RID: 14960
		// (get) Token: 0x0600BA3A RID: 47674 RVA: 0x002A79DB File Offset: 0x002A5BDB
		public static LocalizedString Type
		{
			get
			{
				return new LocalizedString("Type", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A71 RID: 14961
		// (get) Token: 0x0600BA3B RID: 47675 RVA: 0x002A79F9 File Offset: 0x002A5BF9
		public static LocalizedString DateReceived
		{
			get
			{
				return new LocalizedString("DateReceived", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA3C RID: 47676 RVA: 0x002A7A18 File Offset: 0x002A5C18
		public static LocalizedString ConfirmationMessageRedirectMessage(string target)
		{
			return new LocalizedString("ConfirmationMessageRedirectMessage", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				target
			});
		}

		// Token: 0x17003A72 RID: 14962
		// (get) Token: 0x0600BA3D RID: 47677 RVA: 0x002A7A47 File Offset: 0x002A5C47
		public static LocalizedString MissingName
		{
			get
			{
				return new LocalizedString("MissingName", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A73 RID: 14963
		// (get) Token: 0x0600BA3E RID: 47678 RVA: 0x002A7A65 File Offset: 0x002A5C65
		public static LocalizedString UnfreezeQueueTask
		{
			get
			{
				return new LocalizedString("UnfreezeQueueTask", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A74 RID: 14964
		// (get) Token: 0x0600BA3F RID: 47679 RVA: 0x002A7A83 File Offset: 0x002A5C83
		public static LocalizedString InvalidServerData
		{
			get
			{
				return new LocalizedString("InvalidServerData", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA40 RID: 47680 RVA: 0x002A7AA4 File Offset: 0x002A5CA4
		public static LocalizedString ConfirmationMessageSetMessageIdentity(string Identity)
		{
			return new LocalizedString("ConfirmationMessageSetMessageIdentity", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x17003A75 RID: 14965
		// (get) Token: 0x0600BA41 RID: 47681 RVA: 0x002A7AD3 File Offset: 0x002A5CD3
		public static LocalizedString InvalidIdentityString
		{
			get
			{
				return new LocalizedString("InvalidIdentityString", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA42 RID: 47682 RVA: 0x002A7AF4 File Offset: 0x002A5CF4
		public static LocalizedString IncompleteIdentity(string identity)
		{
			return new LocalizedString("IncompleteIdentity", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17003A76 RID: 14966
		// (get) Token: 0x0600BA43 RID: 47683 RVA: 0x002A7B23 File Offset: 0x002A5D23
		public static LocalizedString FreezeQueueTask
		{
			get
			{
				return new LocalizedString("FreezeQueueTask", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A77 RID: 14967
		// (get) Token: 0x0600BA44 RID: 47684 RVA: 0x002A7B41 File Offset: 0x002A5D41
		public static LocalizedString SourceConnector
		{
			get
			{
				return new LocalizedString("SourceConnector", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A78 RID: 14968
		// (get) Token: 0x0600BA45 RID: 47685 RVA: 0x002A7B5F File Offset: 0x002A5D5F
		public static LocalizedString InvalidServerVersion
		{
			get
			{
				return new LocalizedString("InvalidServerVersion", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA46 RID: 47686 RVA: 0x002A7B80 File Offset: 0x002A5D80
		public static LocalizedString ConfirmationMessageSuspendQueueIdentity(string Identity)
		{
			return new LocalizedString("ConfirmationMessageSuspendQueueIdentity", "Ex90C1C2", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x17003A79 RID: 14969
		// (get) Token: 0x0600BA47 RID: 47687 RVA: 0x002A7BAF File Offset: 0x002A5DAF
		public static LocalizedString SuccessMessageRedirectMessageRequestCompleted
		{
			get
			{
				return new LocalizedString("SuccessMessageRedirectMessageRequestCompleted", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A7A RID: 14970
		// (get) Token: 0x0600BA48 RID: 47688 RVA: 0x002A7BCD File Offset: 0x002A5DCD
		public static LocalizedString TextMatchingNotSupported
		{
			get
			{
				return new LocalizedString("TextMatchingNotSupported", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA49 RID: 47689 RVA: 0x002A7BEC File Offset: 0x002A5DEC
		public static LocalizedString ObjectNotFound(string identity)
		{
			return new LocalizedString("ObjectNotFound", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17003A7B RID: 14971
		// (get) Token: 0x0600BA4A RID: 47690 RVA: 0x002A7C1B File Offset: 0x002A5E1B
		public static LocalizedString Size
		{
			get
			{
				return new LocalizedString("Size", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A7C RID: 14972
		// (get) Token: 0x0600BA4B RID: 47691 RVA: 0x002A7C39 File Offset: 0x002A5E39
		public static LocalizedString MissingSender
		{
			get
			{
				return new LocalizedString("MissingSender", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA4C RID: 47692 RVA: 0x002A7C58 File Offset: 0x002A5E58
		public static LocalizedString ConfirmationMessageRetryQueueFilter(string Filter)
		{
			return new LocalizedString("ConfirmationMessageRetryQueueFilter", "ExB8D442", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Filter
			});
		}

		// Token: 0x17003A7D RID: 14973
		// (get) Token: 0x0600BA4D RID: 47693 RVA: 0x002A7C87 File Offset: 0x002A5E87
		public static LocalizedString Id
		{
			get
			{
				return new LocalizedString("Id", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A7E RID: 14974
		// (get) Token: 0x0600BA4E RID: 47694 RVA: 0x002A7CA5 File Offset: 0x002A5EA5
		public static LocalizedString SetMessageResubmitMustBeTrue
		{
			get
			{
				return new LocalizedString("SetMessageResubmitMustBeTrue", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A7F RID: 14975
		// (get) Token: 0x0600BA4F RID: 47695 RVA: 0x002A7CC3 File Offset: 0x002A5EC3
		public static LocalizedString MessageNextRetryTime
		{
			get
			{
				return new LocalizedString("MessageNextRetryTime", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A80 RID: 14976
		// (get) Token: 0x0600BA50 RID: 47696 RVA: 0x002A7CE1 File Offset: 0x002A5EE1
		public static LocalizedString InvalidFieldName
		{
			get
			{
				return new LocalizedString("InvalidFieldName", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA51 RID: 47697 RVA: 0x002A7D00 File Offset: 0x002A5F00
		public static LocalizedString InvalidDomainFormat(string domain)
		{
			return new LocalizedString("InvalidDomainFormat", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x0600BA52 RID: 47698 RVA: 0x002A7D30 File Offset: 0x002A5F30
		public static LocalizedString ConfirmationMessageResumeQueueIdentity(string Identity)
		{
			return new LocalizedString("ConfirmationMessageResumeQueueIdentity", "Ex64B180", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x17003A81 RID: 14977
		// (get) Token: 0x0600BA53 RID: 47699 RVA: 0x002A7D5F File Offset: 0x002A5F5F
		public static LocalizedString MessageCount
		{
			get
			{
				return new LocalizedString("MessageCount", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA54 RID: 47700 RVA: 0x002A7D80 File Offset: 0x002A5F80
		public static LocalizedString ConfirmationMessageResumeMessageFilter(string Filter)
		{
			return new LocalizedString("ConfirmationMessageResumeMessageFilter", "Ex969910", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Filter
			});
		}

		// Token: 0x0600BA55 RID: 47701 RVA: 0x002A7DB0 File Offset: 0x002A5FB0
		public static LocalizedString InvalidProviderName(string provider)
		{
			return new LocalizedString("InvalidProviderName", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				provider
			});
		}

		// Token: 0x0600BA56 RID: 47702 RVA: 0x002A7DE0 File Offset: 0x002A5FE0
		public static LocalizedString ConfirmationMessageRetryQueueIdentity(string Identity)
		{
			return new LocalizedString("ConfirmationMessageRetryQueueIdentity", "Ex8AC9D5", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x17003A82 RID: 14978
		// (get) Token: 0x0600BA57 RID: 47703 RVA: 0x002A7E0F File Offset: 0x002A600F
		public static LocalizedString RetryCount
		{
			get
			{
				return new LocalizedString("RetryCount", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A83 RID: 14979
		// (get) Token: 0x0600BA58 RID: 47704 RVA: 0x002A7E2D File Offset: 0x002A602D
		public static LocalizedString FilterTypeNotSupported
		{
			get
			{
				return new LocalizedString("FilterTypeNotSupported", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A84 RID: 14980
		// (get) Token: 0x0600BA59 RID: 47705 RVA: 0x002A7E4B File Offset: 0x002A604B
		public static LocalizedString SourceIP
		{
			get
			{
				return new LocalizedString("SourceIP", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A85 RID: 14981
		// (get) Token: 0x0600BA5A RID: 47706 RVA: 0x002A7E69 File Offset: 0x002A6069
		public static LocalizedString RedirectMessageTask
		{
			get
			{
				return new LocalizedString("RedirectMessageTask", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA5B RID: 47707 RVA: 0x002A7E88 File Offset: 0x002A6088
		public static LocalizedString ConfirmationMessageRemoveMessageIdentity(string Identity)
		{
			return new LocalizedString("ConfirmationMessageRemoveMessageIdentity", "ExE29E2E", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600BA5C RID: 47708 RVA: 0x002A7EB8 File Offset: 0x002A60B8
		public static LocalizedString GenericError(string message)
		{
			return new LocalizedString("GenericError", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17003A86 RID: 14982
		// (get) Token: 0x0600BA5D RID: 47709 RVA: 0x002A7EE7 File Offset: 0x002A60E7
		public static LocalizedString QueueResubmitInProgress
		{
			get
			{
				return new LocalizedString("QueueResubmitInProgress", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA5E RID: 47710 RVA: 0x002A7F08 File Offset: 0x002A6108
		public static LocalizedString RpcNotRegistered(string computername)
		{
			return new LocalizedString("RpcNotRegistered", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				computername
			});
		}

		// Token: 0x0600BA5F RID: 47711 RVA: 0x002A7F38 File Offset: 0x002A6138
		public static LocalizedString ConfirmationMessageSuspendQueueFilter(string Filter)
		{
			return new LocalizedString("ConfirmationMessageSuspendQueueFilter", "Ex671206", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Filter
			});
		}

		// Token: 0x0600BA60 RID: 47712 RVA: 0x002A7F68 File Offset: 0x002A6168
		public static LocalizedString ConfirmationMessageSuspendMessageFilter(string Filter)
		{
			return new LocalizedString("ConfirmationMessageSuspendMessageFilter", "ExD0E030", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Filter
			});
		}

		// Token: 0x0600BA61 RID: 47713 RVA: 0x002A7F98 File Offset: 0x002A6198
		public static LocalizedString ConfirmationMessageResumeQueueFilter(string Filter)
		{
			return new LocalizedString("ConfirmationMessageResumeQueueFilter", "Ex2DCBD0", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Filter
			});
		}

		// Token: 0x17003A87 RID: 14983
		// (get) Token: 0x0600BA62 RID: 47714 RVA: 0x002A7FC7 File Offset: 0x002A61C7
		public static LocalizedString ComparisonNotSupported
		{
			get
			{
				return new LocalizedString("ComparisonNotSupported", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A88 RID: 14984
		// (get) Token: 0x0600BA63 RID: 47715 RVA: 0x002A7FE5 File Offset: 0x002A61E5
		public static LocalizedString RetryQueueTask
		{
			get
			{
				return new LocalizedString("RetryQueueTask", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA64 RID: 47716 RVA: 0x002A8004 File Offset: 0x002A6204
		public static LocalizedString NonAbsolutePath(string path)
		{
			return new LocalizedString("NonAbsolutePath", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17003A89 RID: 14985
		// (get) Token: 0x0600BA65 RID: 47717 RVA: 0x002A8033 File Offset: 0x002A6233
		public static LocalizedString ExpirationTime
		{
			get
			{
				return new LocalizedString("ExpirationTime", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA66 RID: 47718 RVA: 0x002A8054 File Offset: 0x002A6254
		public static LocalizedString InvalidOperation(string identity)
		{
			return new LocalizedString("InvalidOperation", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17003A8A RID: 14986
		// (get) Token: 0x0600BA67 RID: 47719 RVA: 0x002A8083 File Offset: 0x002A6283
		public static LocalizedString InvalidServerCollection
		{
			get
			{
				return new LocalizedString("InvalidServerCollection", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A8B RID: 14987
		// (get) Token: 0x0600BA68 RID: 47720 RVA: 0x002A80A1 File Offset: 0x002A62A1
		public static LocalizedString RedirectMessageInProgress
		{
			get
			{
				return new LocalizedString("RedirectMessageInProgress", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA69 RID: 47721 RVA: 0x002A80C0 File Offset: 0x002A62C0
		public static LocalizedString ConfirmationMessageExportMessage(string Identity)
		{
			return new LocalizedString("ConfirmationMessageExportMessage", "ExC199BE", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600BA6A RID: 47722 RVA: 0x002A80F0 File Offset: 0x002A62F0
		public static LocalizedString GenericRpcError(string message, string computername)
		{
			return new LocalizedString("GenericRpcError", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				message,
				computername
			});
		}

		// Token: 0x0600BA6B RID: 47723 RVA: 0x002A8124 File Offset: 0x002A6324
		public static LocalizedString MessageNotSuspended(string identity)
		{
			return new LocalizedString("MessageNotSuspended", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17003A8C RID: 14988
		// (get) Token: 0x0600BA6C RID: 47724 RVA: 0x002A8153 File Offset: 0x002A6353
		public static LocalizedString RemoveMessageTask
		{
			get
			{
				return new LocalizedString("RemoveMessageTask", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A8D RID: 14989
		// (get) Token: 0x0600BA6D RID: 47725 RVA: 0x002A8171 File Offset: 0x002A6371
		public static LocalizedString MessageLastRetryTime
		{
			get
			{
				return new LocalizedString("MessageLastRetryTime", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A8E RID: 14990
		// (get) Token: 0x0600BA6E RID: 47726 RVA: 0x002A818F File Offset: 0x002A638F
		public static LocalizedString InvalidIdentityForEquality
		{
			get
			{
				return new LocalizedString("InvalidIdentityForEquality", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A8F RID: 14991
		// (get) Token: 0x0600BA6F RID: 47727 RVA: 0x002A81AD File Offset: 0x002A63AD
		public static LocalizedString Priority
		{
			get
			{
				return new LocalizedString("Priority", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA70 RID: 47728 RVA: 0x002A81CC File Offset: 0x002A63CC
		public static LocalizedString ConfirmationMessageResumeMessageIdentity(string Identity)
		{
			return new LocalizedString("ConfirmationMessageResumeMessageIdentity", "ExF21D72", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600BA71 RID: 47729 RVA: 0x002A81FC File Offset: 0x002A63FC
		public static LocalizedString ConfirmationMessageRemoveMessageFilter(string Filter)
		{
			return new LocalizedString("ConfirmationMessageRemoveMessageFilter", "Ex77FC78", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Filter
			});
		}

		// Token: 0x17003A90 RID: 14992
		// (get) Token: 0x0600BA72 RID: 47730 RVA: 0x002A822B File Offset: 0x002A642B
		public static LocalizedString SetMessageTask
		{
			get
			{
				return new LocalizedString("SetMessageTask", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA73 RID: 47731 RVA: 0x002A824C File Offset: 0x002A644C
		public static LocalizedString SetMessageOutboundPoolOutsideRange(int port, int min, int max)
		{
			return new LocalizedString("SetMessageOutboundPoolOutsideRange", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				port,
				min,
				max
			});
		}

		// Token: 0x0600BA74 RID: 47732 RVA: 0x002A8294 File Offset: 0x002A6494
		public static LocalizedString RpcUnavailable(string computername)
		{
			return new LocalizedString("RpcUnavailable", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				computername
			});
		}

		// Token: 0x17003A91 RID: 14993
		// (get) Token: 0x0600BA75 RID: 47733 RVA: 0x002A82C3 File Offset: 0x002A64C3
		public static LocalizedString Subject
		{
			get
			{
				return new LocalizedString("Subject", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A92 RID: 14994
		// (get) Token: 0x0600BA76 RID: 47734 RVA: 0x002A82E1 File Offset: 0x002A64E1
		public static LocalizedString OldestMessage
		{
			get
			{
				return new LocalizedString("OldestMessage", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A93 RID: 14995
		// (get) Token: 0x0600BA77 RID: 47735 RVA: 0x002A82FF File Offset: 0x002A64FF
		public static LocalizedString TooManyResults
		{
			get
			{
				return new LocalizedString("TooManyResults", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A94 RID: 14996
		// (get) Token: 0x0600BA78 RID: 47736 RVA: 0x002A831D File Offset: 0x002A651D
		public static LocalizedString AmbiguousParameterSet
		{
			get
			{
				return new LocalizedString("AmbiguousParameterSet", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A95 RID: 14997
		// (get) Token: 0x0600BA79 RID: 47737 RVA: 0x002A833B File Offset: 0x002A653B
		public static LocalizedString NextRetryTime
		{
			get
			{
				return new LocalizedString("NextRetryTime", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA7A RID: 47738 RVA: 0x002A835C File Offset: 0x002A655C
		public static LocalizedString NotTransportHubServer(string fqdn)
		{
			return new LocalizedString("NotTransportHubServer", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x17003A96 RID: 14998
		// (get) Token: 0x0600BA7B RID: 47739 RVA: 0x002A838B File Offset: 0x002A658B
		public static LocalizedString InvalidClientData
		{
			get
			{
				return new LocalizedString("InvalidClientData", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA7C RID: 47740 RVA: 0x002A83AC File Offset: 0x002A65AC
		public static LocalizedString UnknownServer(string fqdn)
		{
			return new LocalizedString("UnknownServer", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x0600BA7D RID: 47741 RVA: 0x002A83DC File Offset: 0x002A65DC
		public static LocalizedString ConfirmationMessageSuspendMessageIdentity(string Identity)
		{
			return new LocalizedString("ConfirmationMessageSuspendMessageIdentity", "Ex68F928", false, true, QueueViewerStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x17003A97 RID: 14999
		// (get) Token: 0x0600BA7E RID: 47742 RVA: 0x002A840B File Offset: 0x002A660B
		public static LocalizedString LastRetryTime
		{
			get
			{
				return new LocalizedString("LastRetryTime", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA7F RID: 47743 RVA: 0x002A842C File Offset: 0x002A662C
		public static LocalizedString MultipleIdentityMatch(string identity)
		{
			return new LocalizedString("MultipleIdentityMatch", "", false, false, QueueViewerStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17003A98 RID: 15000
		// (get) Token: 0x0600BA80 RID: 47744 RVA: 0x002A845B File Offset: 0x002A665B
		public static LocalizedString Sender
		{
			get
			{
				return new LocalizedString("Sender", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A99 RID: 15001
		// (get) Token: 0x0600BA81 RID: 47745 RVA: 0x002A8479 File Offset: 0x002A6679
		public static LocalizedString MessageGrid
		{
			get
			{
				return new LocalizedString("MessageGrid", "", false, false, QueueViewerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA82 RID: 47746 RVA: 0x002A8497 File Offset: 0x002A6697
		public static LocalizedString GetLocalizedString(QueueViewerStrings.IDs key)
		{
			return new LocalizedString(QueueViewerStrings.stringIDs[(uint)key], QueueViewerStrings.ResourceManager, new object[0]);
		}

		// Token: 0x040064B3 RID: 25779
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(45);

		// Token: 0x040064B4 RID: 25780
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.QueueViewerStrings", typeof(QueueViewerStrings).GetTypeInfo().Assembly);

		// Token: 0x02001211 RID: 4625
		public enum IDs : uint
		{
			// Token: 0x040064B6 RID: 25782
			UnfreezeMessageTask = 317860356U,
			// Token: 0x040064B7 RID: 25783
			FreezeMessageTask = 2910097809U,
			// Token: 0x040064B8 RID: 25784
			Status = 1959773104U,
			// Token: 0x040064B9 RID: 25785
			Type = 1421152990U,
			// Token: 0x040064BA RID: 25786
			DateReceived = 3089884583U,
			// Token: 0x040064BB RID: 25787
			MissingName = 1316090539U,
			// Token: 0x040064BC RID: 25788
			UnfreezeQueueTask = 1572946888U,
			// Token: 0x040064BD RID: 25789
			InvalidServerData = 3695048262U,
			// Token: 0x040064BE RID: 25790
			InvalidIdentityString = 973091570U,
			// Token: 0x040064BF RID: 25791
			FreezeQueueTask = 3845883191U,
			// Token: 0x040064C0 RID: 25792
			SourceConnector = 1481046174U,
			// Token: 0x040064C1 RID: 25793
			InvalidServerVersion = 3815822302U,
			// Token: 0x040064C2 RID: 25794
			SuccessMessageRedirectMessageRequestCompleted = 2112399141U,
			// Token: 0x040064C3 RID: 25795
			TextMatchingNotSupported = 2470050549U,
			// Token: 0x040064C4 RID: 25796
			Size = 3457520377U,
			// Token: 0x040064C5 RID: 25797
			MissingSender = 1800498867U,
			// Token: 0x040064C6 RID: 25798
			Id = 287061521U,
			// Token: 0x040064C7 RID: 25799
			SetMessageResubmitMustBeTrue = 4170207812U,
			// Token: 0x040064C8 RID: 25800
			MessageNextRetryTime = 2064039061U,
			// Token: 0x040064C9 RID: 25801
			InvalidFieldName = 337370324U,
			// Token: 0x040064CA RID: 25802
			MessageCount = 3710722188U,
			// Token: 0x040064CB RID: 25803
			RetryCount = 3894225067U,
			// Token: 0x040064CC RID: 25804
			FilterTypeNotSupported = 1638755231U,
			// Token: 0x040064CD RID: 25805
			SourceIP = 3688482442U,
			// Token: 0x040064CE RID: 25806
			RedirectMessageTask = 793883224U,
			// Token: 0x040064CF RID: 25807
			QueueResubmitInProgress = 4178538216U,
			// Token: 0x040064D0 RID: 25808
			ComparisonNotSupported = 1686262562U,
			// Token: 0x040064D1 RID: 25809
			RetryQueueTask = 3457614570U,
			// Token: 0x040064D2 RID: 25810
			ExpirationTime = 3644361014U,
			// Token: 0x040064D3 RID: 25811
			InvalidServerCollection = 501948002U,
			// Token: 0x040064D4 RID: 25812
			RedirectMessageInProgress = 4294095275U,
			// Token: 0x040064D5 RID: 25813
			RemoveMessageTask = 2999867432U,
			// Token: 0x040064D6 RID: 25814
			MessageLastRetryTime = 3834431484U,
			// Token: 0x040064D7 RID: 25815
			InvalidIdentityForEquality = 2455477494U,
			// Token: 0x040064D8 RID: 25816
			Priority = 3857609986U,
			// Token: 0x040064D9 RID: 25817
			SetMessageTask = 1726666628U,
			// Token: 0x040064DA RID: 25818
			Subject = 1732412034U,
			// Token: 0x040064DB RID: 25819
			OldestMessage = 822915892U,
			// Token: 0x040064DC RID: 25820
			TooManyResults = 3853989365U,
			// Token: 0x040064DD RID: 25821
			AmbiguousParameterSet = 2816985275U,
			// Token: 0x040064DE RID: 25822
			NextRetryTime = 1967184194U,
			// Token: 0x040064DF RID: 25823
			InvalidClientData = 523788226U,
			// Token: 0x040064E0 RID: 25824
			LastRetryTime = 1850287295U,
			// Token: 0x040064E1 RID: 25825
			Sender = 51203499U,
			// Token: 0x040064E2 RID: 25826
			MessageGrid = 3552881163U
		}

		// Token: 0x02001212 RID: 4626
		private enum ParamIDs
		{
			// Token: 0x040064E4 RID: 25828
			ConfirmationMessageSetMessageFilter,
			// Token: 0x040064E5 RID: 25829
			ConfirmationMessageRedirectMessage,
			// Token: 0x040064E6 RID: 25830
			ConfirmationMessageSetMessageIdentity,
			// Token: 0x040064E7 RID: 25831
			IncompleteIdentity,
			// Token: 0x040064E8 RID: 25832
			ConfirmationMessageSuspendQueueIdentity,
			// Token: 0x040064E9 RID: 25833
			ObjectNotFound,
			// Token: 0x040064EA RID: 25834
			ConfirmationMessageRetryQueueFilter,
			// Token: 0x040064EB RID: 25835
			InvalidDomainFormat,
			// Token: 0x040064EC RID: 25836
			ConfirmationMessageResumeQueueIdentity,
			// Token: 0x040064ED RID: 25837
			ConfirmationMessageResumeMessageFilter,
			// Token: 0x040064EE RID: 25838
			InvalidProviderName,
			// Token: 0x040064EF RID: 25839
			ConfirmationMessageRetryQueueIdentity,
			// Token: 0x040064F0 RID: 25840
			ConfirmationMessageRemoveMessageIdentity,
			// Token: 0x040064F1 RID: 25841
			GenericError,
			// Token: 0x040064F2 RID: 25842
			RpcNotRegistered,
			// Token: 0x040064F3 RID: 25843
			ConfirmationMessageSuspendQueueFilter,
			// Token: 0x040064F4 RID: 25844
			ConfirmationMessageSuspendMessageFilter,
			// Token: 0x040064F5 RID: 25845
			ConfirmationMessageResumeQueueFilter,
			// Token: 0x040064F6 RID: 25846
			NonAbsolutePath,
			// Token: 0x040064F7 RID: 25847
			InvalidOperation,
			// Token: 0x040064F8 RID: 25848
			ConfirmationMessageExportMessage,
			// Token: 0x040064F9 RID: 25849
			GenericRpcError,
			// Token: 0x040064FA RID: 25850
			MessageNotSuspended,
			// Token: 0x040064FB RID: 25851
			ConfirmationMessageResumeMessageIdentity,
			// Token: 0x040064FC RID: 25852
			ConfirmationMessageRemoveMessageFilter,
			// Token: 0x040064FD RID: 25853
			SetMessageOutboundPoolOutsideRange,
			// Token: 0x040064FE RID: 25854
			RpcUnavailable,
			// Token: 0x040064FF RID: 25855
			NotTransportHubServer,
			// Token: 0x04006500 RID: 25856
			UnknownServer,
			// Token: 0x04006501 RID: 25857
			ConfirmationMessageSuspendMessageIdentity,
			// Token: 0x04006502 RID: 25858
			MultipleIdentityMatch
		}
	}
}
