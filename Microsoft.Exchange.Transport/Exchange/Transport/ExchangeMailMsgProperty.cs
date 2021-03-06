using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000441 RID: 1089
	internal enum ExchangeMailMsgProperty : uint
	{
		// Token: 0x04001937 RID: 6455
		IMMPID_EMP_INTERNET_MESSAGE_ID,
		// Token: 0x04001938 RID: 6456
		IMMPID_EMP_SUBJECT,
		// Token: 0x04001939 RID: 6457
		IMMPID_EMP_PRIORITY,
		// Token: 0x0400193A RID: 6458
		IMMPID_EMP_LATEST_DELIVERY_TIME,
		// Token: 0x0400193B RID: 6459
		IMMPID_EMP_INTERNET_SITE_ADDRESS,
		// Token: 0x0400193C RID: 6460
		IMMPID_EMP_SEND_RICH_INFO,
		// Token: 0x0400193D RID: 6461
		IMMPID_EMP_INTERNET_CHARSET,
		// Token: 0x0400193E RID: 6462
		IMMPID_EMP_INTERNET_WRAPPING_LENGTH,
		// Token: 0x0400193F RID: 6463
		IMMPID_EMP_INTERNET_ADDRESSING_OPTIONS,
		// Token: 0x04001940 RID: 6464
		IMMPID_EMP_SEND_INTERNET_ENCODING,
		// Token: 0x04001941 RID: 6465
		IMMPID_EMP_INTERNET_MESSAGE_OPTIONS,
		// Token: 0x04001942 RID: 6466
		IMMPID_EMP_MESSAGE_CLASS,
		// Token: 0x04001943 RID: 6467
		IMMPID_EMP_AUTO_FORWARDED,
		// Token: 0x04001944 RID: 6468
		IMMPID_EMP_RFC822_FROM_ADDRESS,
		// Token: 0x04001945 RID: 6469
		IMMPID_EMP_RFC822_TO_ADDRESS,
		// Token: 0x04001946 RID: 6470
		IMMPID_EMP_RFC822_CC_ADDRESS,
		// Token: 0x04001947 RID: 6471
		IMMPID_EMP_RFC822_BCC_ADDRESS,
		// Token: 0x04001948 RID: 6472
		IMMPID_EMP_TRACE_INFO,
		// Token: 0x04001949 RID: 6473
		IMMPID_EMP_INTERNAL_TRACE_INFO,
		// Token: 0x0400194A RID: 6474
		IMMPID_EMP_XEXCH50,
		// Token: 0x0400194B RID: 6475
		IMMPID_EMP_MTS_ID,
		// Token: 0x0400194C RID: 6476
		IMMPID_EMP_EXCHANGE_STOREDRV_MESSAGE,
		// Token: 0x0400194D RID: 6477
		IMMPID_EMP_MSGTRACKING_ORG_GUID,
		// Token: 0x0400194E RID: 6478
		IMMPID_EMP_JOURNAL_RECIPIENT_LIST,
		// Token: 0x0400194F RID: 6479
		IMMPID_EMP_MESSAGE_BIFINFO,
		// Token: 0x04001950 RID: 6480
		IMMPID_EMP_AUTO_RESPONSE_SUPPRESS,
		// Token: 0x04001951 RID: 6481
		IMMPID_EMP_SEND_RICH_INFO_NON_SMTP,
		// Token: 0x04001952 RID: 6482
		IMMPID_EMP_CONTENT_IDENTIFIER,
		// Token: 0x04001953 RID: 6483
		IMMPID_EMP_INBOUND_CHARSET,
		// Token: 0x04001954 RID: 6484
		IMMPID_EMP_INTERNET_ADDRESS_CONVERSION,
		// Token: 0x04001955 RID: 6485
		IMMPID_EMP_TRANS_MSG_BLOB,
		// Token: 0x04001956 RID: 6486
		IMMPID_EMP_SYSTEM_MESSAGE_CLASS,
		// Token: 0x04001957 RID: 6487
		IMMPID_EMP_SENDER_RECIPIENT_LIMIT,
		// Token: 0x04001958 RID: 6488
		IMMPID_EMP_AUTHENTICATED_ORIGINATOR,
		// Token: 0x04001959 RID: 6489
		IMMPID_EMP_DL_OWNER_DN,
		// Token: 0x0400195A RID: 6490
		IMMPID_EMP_P1_SENDER_DN,
		// Token: 0x0400195B RID: 6491
		IMMPID_EMP_ANTIVIRUS_VENDOR,
		// Token: 0x0400195C RID: 6492
		IMMPID_EMP_ANTIVIRUS_VERSION,
		// Token: 0x0400195D RID: 6493
		IMMPID_EMP_ANTIVIRUS_SCAN_STATUS,
		// Token: 0x0400195E RID: 6494
		IMMPID_EMP_ANTIVIRUS_SCAN_INFO,
		// Token: 0x0400195F RID: 6495
		IMMPID_EMP_ANTIVIRUS_CLEANED_MESSAGE,
		// Token: 0x04001960 RID: 6496
		IMMPID_EMP_XEXCH50_SERIALIZED_RECIPIENT_LIST,
		// Token: 0x04001961 RID: 6497
		IMMPID_EMP_XEXCH50_DEFERRED_RECIPIENT_BLOBS,
		// Token: 0x04001962 RID: 6498
		IMMPID_EMP_XEXCH50_EXTERNAL_ORG,
		// Token: 0x04001963 RID: 6499
		IMMPID_EMP_ORIGINAL_P1_RECIPIENT_LIST,
		// Token: 0x04001964 RID: 6500
		IMMPID_EMP_AUTHENTICATION_LEVEL,
		// Token: 0x04001965 RID: 6501
		IMMPID_EMP_FADDRESSES_REWRITTEN,
		// Token: 0x04001966 RID: 6502
		IMMPID_EMP_HIDDEN_RECIPIENTS,
		// Token: 0x04001967 RID: 6503
		IMMPID_EMP_RECIPIENT_REASSIGNMENT_PROHIBITED,
		// Token: 0x04001968 RID: 6504
		IMMPID_EMP_DMS_UNENCAP_SENDER,
		// Token: 0x04001969 RID: 6505
		IMMPID_EMP_EMBEDDED_MESSAGE_TYPE,
		// Token: 0x0400196A RID: 6506
		IMMPID_EMP_XORG_TRANS_MSG_BLOB,
		// Token: 0x0400196B RID: 6507
		IMMPID_EMP_EJ_ORIGINAL_SENDER_DISPLAY_NAME,
		// Token: 0x0400196C RID: 6508
		IMMPID_EMP_EJ_ORIGINAL_SENDER,
		// Token: 0x0400196D RID: 6509
		IMMPID_EMP_EJ_RECIPIENT_LIST,
		// Token: 0x0400196E RID: 6510
		IMMPID_EMP_SENDERID_RESOLUTION,
		// Token: 0x0400196F RID: 6511
		IMMPID_EMP_SENDERID_RESOLUTION_DONE,
		// Token: 0x04001970 RID: 6512
		IMMPID_EMP_EJ_RECIPIENT_P2_TYPE_LIST,
		// Token: 0x04001971 RID: 6513
		IMMPID_EMP_EJ_EXPANSION_HISTORY_LIST,
		// Token: 0x04001972 RID: 6514
		IMMPID_ERP_READ_RECEIPT_REQUESTED = 1000U,
		// Token: 0x04001973 RID: 6515
		IMMPID_ERP_NON_RECEIPT_NOTIFICATION_REQ,
		// Token: 0x04001974 RID: 6516
		IMMPID_ERP_REQUESTED_DELIVERY_METHOD,
		// Token: 0x04001975 RID: 6517
		IMMPID_ERP_PROOF_OF_DELIVERY_REQUESTED,
		// Token: 0x04001976 RID: 6518
		IMMPID_ERP_PHYSICAL_FORWARDING_ADDRESS_REQUESTED,
		// Token: 0x04001977 RID: 6519
		IMMPID_ERP_PHYSICAL_FORWARDING_PROHIBITED,
		// Token: 0x04001978 RID: 6520
		IMMPID_ERP_EXPLICIT_CONVERSION,
		// Token: 0x04001979 RID: 6521
		IMMPID_ERP_PHYSICAL_DELIVERY_REPORT_REQUEST,
		// Token: 0x0400197A RID: 6522
		IMMPID_ERP_PHYSICAL_DELIVERY_MODE,
		// Token: 0x0400197B RID: 6523
		IMMPID_ERP_REGISTERED_MAIL_TYPE,
		// Token: 0x0400197C RID: 6524
		IMMPID_ERP_PHYSICAL_DELIVERY_BUREAU_FAX_DELIVERY,
		// Token: 0x0400197D RID: 6525
		IMMPID_ERP_ORIGINATOR_REQUESTED_ALTERNATE_RECIPIENT,
		// Token: 0x0400197E RID: 6526
		IMMPID_ERP_AUTO_RESPONSE_SUPPRESS,
		// Token: 0x0400197F RID: 6527
		IMMPID_ERP_RECIPIENT_TYPE,
		// Token: 0x04001980 RID: 6528
		IMMPID_ERP_RECIPIENT_NUMBER,
		// Token: 0x04001981 RID: 6529
		IMMPID_ERP_ENTRY_ID,
		// Token: 0x04001982 RID: 6530
		IMMPID_ERP_ROW_ID,
		// Token: 0x04001983 RID: 6531
		IMMPID_ERP_PF_LTID,
		// Token: 0x04001984 RID: 6532
		IMMPID_ERP_FLOCALMAILBOX,
		// Token: 0x04001985 RID: 6533
		IMMPID_ERP_XEXCH50,
		// Token: 0x04001986 RID: 6534
		IMMPID_ERP_PF_REPLICA_MDB,
		// Token: 0x04001987 RID: 6535
		IMMPID_ERP_TRANS_RECIP_BLOB,
		// Token: 0x04001988 RID: 6536
		IMMPID_ERP_ALTERNATE_RECIPIENT_EXPANDED,
		// Token: 0x04001989 RID: 6537
		IMMPID_ERP_DMS_UNENCAP_RCPT,
		// Token: 0x0400198A RID: 6538
		IMMPID_ERP_XORG_TRANS_RECIP_BLOB,
		// Token: 0x0400198B RID: 6539
		EMMPROPS_COUNT = 2000U
	}
}
