using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000D4 RID: 212
	internal enum MailboxLogDataName
	{
		// Token: 0x0400076A RID: 1898
		AdditionalData,
		// Token: 0x0400076B RID: 1899
		RequestHeader,
		// Token: 0x0400076C RID: 1900
		RequestBody,
		// Token: 0x0400076D RID: 1901
		RequestTime,
		// Token: 0x0400076E RID: 1902
		ResponseHeader,
		// Token: 0x0400076F RID: 1903
		ResponseBody,
		// Token: 0x04000770 RID: 1904
		ResponseTime,
		// Token: 0x04000771 RID: 1905
		Command_WorkerThread_Exception,
		// Token: 0x04000772 RID: 1906
		SyncCommand_ConvertRequestsAndApply_Add_Exception,
		// Token: 0x04000773 RID: 1907
		SyncCommand_ConvertRequestsAndApply_Change_RejectClientChange_Exception,
		// Token: 0x04000774 RID: 1908
		SyncCommand_ConvertRequestsAndApply_Delete_Exception,
		// Token: 0x04000775 RID: 1909
		SyncCommand_GenerateResponsesXmlNode_Fetch_Exception,
		// Token: 0x04000776 RID: 1910
		SyncCommand_GenerateResponsesXmlNode_AddChange_ConvertServerToClientObject_Exception,
		// Token: 0x04000777 RID: 1911
		SyncCommand_GenerateResponsesXmlNode_AddChange_Exception,
		// Token: 0x04000778 RID: 1912
		SyncCommand_OnExecute_Exception,
		// Token: 0x04000779 RID: 1913
		MailboxSyncCommand_HasSchemaPropertyChanged_Exception,
		// Token: 0x0400077A RID: 1914
		MeetingResponseCommand_OnExecute_Exception,
		// Token: 0x0400077B RID: 1915
		GetItemEstimateCommand_OnExecute_Exception,
		// Token: 0x0400077C RID: 1916
		PingCommand_Consume_Exception,
		// Token: 0x0400077D RID: 1917
		PingCommand__ItemChangesSinceLastSync_Exception,
		// Token: 0x0400077E RID: 1918
		SyncCommand_ConvertRequestsAndApply_Change_AcceptClientChange_Exception,
		// Token: 0x0400077F RID: 1919
		ItemOperationsCommand_Execute_Fetch_Exception,
		// Token: 0x04000780 RID: 1920
		SearchCommand_Execute_Exception,
		// Token: 0x04000781 RID: 1921
		Identifier,
		// Token: 0x04000782 RID: 1922
		LogicalRequest,
		// Token: 0x04000783 RID: 1923
		WasPending,
		// Token: 0x04000784 RID: 1924
		ServerName,
		// Token: 0x04000785 RID: 1925
		AssemblyVersion,
		// Token: 0x04000786 RID: 1926
		ValidateCertCommand_ProcessCommand_Per_Cert_Exception,
		// Token: 0x04000787 RID: 1927
		ValidateCertCommand_ProcessCommand_Exception,
		// Token: 0x04000788 RID: 1928
		AccessState,
		// Token: 0x04000789 RID: 1929
		AccessStateReason,
		// Token: 0x0400078A RID: 1930
		DeviceAccessControlRule,
		// Token: 0x0400078B RID: 1931
		SyncCollection_VerifySyncKey_Exception,
		// Token: 0x0400078C RID: 1932
		IRM_FailureCode,
		// Token: 0x0400078D RID: 1933
		IRM_Exception,
		// Token: 0x0400078E RID: 1934
		CalendarSync_Exception
	}
}
