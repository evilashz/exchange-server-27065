using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002B7 RID: 695
	public enum ColumnId
	{
		// Token: 0x04001339 RID: 4921
		MailIcon,
		// Token: 0x0400133A RID: 4922
		From,
		// Token: 0x0400133B RID: 4923
		To,
		// Token: 0x0400133C RID: 4924
		Subject,
		// Token: 0x0400133D RID: 4925
		Department,
		// Token: 0x0400133E RID: 4926
		HasAttachment,
		// Token: 0x0400133F RID: 4927
		Importance,
		// Token: 0x04001340 RID: 4928
		DeliveryTime,
		// Token: 0x04001341 RID: 4929
		SentTime,
		// Token: 0x04001342 RID: 4930
		Size,
		// Token: 0x04001343 RID: 4931
		ContactIcon,
		// Token: 0x04001344 RID: 4932
		FileAs,
		// Token: 0x04001345 RID: 4933
		Title,
		// Token: 0x04001346 RID: 4934
		CompanyName,
		// Token: 0x04001347 RID: 4935
		PhoneNumbers,
		// Token: 0x04001348 RID: 4936
		BusinessPhone,
		// Token: 0x04001349 RID: 4937
		BusinessFax,
		// Token: 0x0400134A RID: 4938
		MobilePhone,
		// Token: 0x0400134B RID: 4939
		HomePhone,
		// Token: 0x0400134C RID: 4940
		EmailAddresses,
		// Token: 0x0400134D RID: 4941
		Email1,
		// Token: 0x0400134E RID: 4942
		Email2,
		// Token: 0x0400134F RID: 4943
		Email3,
		// Token: 0x04001350 RID: 4944
		GivenName,
		// Token: 0x04001351 RID: 4945
		Surname,
		// Token: 0x04001352 RID: 4946
		Categories,
		// Token: 0x04001353 RID: 4947
		ContactCategories,
		// Token: 0x04001354 RID: 4948
		SharepointDocumentIcon,
		// Token: 0x04001355 RID: 4949
		SharepointDocumentDisplayName,
		// Token: 0x04001356 RID: 4950
		SharepointDocumentLastModified,
		// Token: 0x04001357 RID: 4951
		SharepointDocumentModifiedBy,
		// Token: 0x04001358 RID: 4952
		SharepointDocumentCheckedOutTo,
		// Token: 0x04001359 RID: 4953
		SharepointDocumentFileSize,
		// Token: 0x0400135A RID: 4954
		UncDocumentIcon,
		// Token: 0x0400135B RID: 4955
		UncDocumentLibraryIcon,
		// Token: 0x0400135C RID: 4956
		UncDocumentDisplayName,
		// Token: 0x0400135D RID: 4957
		UncDocumentLastModified,
		// Token: 0x0400135E RID: 4958
		UncDocumentFileSize,
		// Token: 0x0400135F RID: 4959
		SharepointDocumentLibraryIcon,
		// Token: 0x04001360 RID: 4960
		SharepointDocumentLibraryDisplayName,
		// Token: 0x04001361 RID: 4961
		SharepointDocumentLibraryDescription,
		// Token: 0x04001362 RID: 4962
		SharepointDocumentLibraryItemCount,
		// Token: 0x04001363 RID: 4963
		SharepointDocumentLibraryLastModified,
		// Token: 0x04001364 RID: 4964
		CheckBox,
		// Token: 0x04001365 RID: 4965
		CheckBoxContact,
		// Token: 0x04001366 RID: 4966
		ADIcon,
		// Token: 0x04001367 RID: 4967
		AliasAD,
		// Token: 0x04001368 RID: 4968
		BusinessFaxAD,
		// Token: 0x04001369 RID: 4969
		BusinessPhoneAD,
		// Token: 0x0400136A RID: 4970
		CheckBoxAD,
		// Token: 0x0400136B RID: 4971
		CompanyAD,
		// Token: 0x0400136C RID: 4972
		DepartmentAD,
		// Token: 0x0400136D RID: 4973
		DisplayNameAD,
		// Token: 0x0400136E RID: 4974
		EmailAddressAD,
		// Token: 0x0400136F RID: 4975
		HomePhoneAD,
		// Token: 0x04001370 RID: 4976
		MobilePhoneAD,
		// Token: 0x04001371 RID: 4977
		OfficeAD,
		// Token: 0x04001372 RID: 4978
		PhoneAD,
		// Token: 0x04001373 RID: 4979
		TitleAD,
		// Token: 0x04001374 RID: 4980
		YomiCompanyName,
		// Token: 0x04001375 RID: 4981
		YomiCompanyAD,
		// Token: 0x04001376 RID: 4982
		YomiFirstName,
		// Token: 0x04001377 RID: 4983
		YomiFullName,
		// Token: 0x04001378 RID: 4984
		YomiLastName,
		// Token: 0x04001379 RID: 4985
		YomiDisplayNameAD,
		// Token: 0x0400137A RID: 4986
		YomiDepartmentAD,
		// Token: 0x0400137B RID: 4987
		ResourceCapacityAD,
		// Token: 0x0400137C RID: 4988
		FlagDueDate,
		// Token: 0x0400137D RID: 4989
		FlagStartDate,
		// Token: 0x0400137E RID: 4990
		ContactFlagDueDate,
		// Token: 0x0400137F RID: 4991
		ContactFlagStartDate,
		// Token: 0x04001380 RID: 4992
		TaskFlag,
		// Token: 0x04001381 RID: 4993
		TaskIcon,
		// Token: 0x04001382 RID: 4994
		MarkCompleteCheckbox,
		// Token: 0x04001383 RID: 4995
		DueDate,
		// Token: 0x04001384 RID: 4996
		MemberDisplayName,
		// Token: 0x04001385 RID: 4997
		MemberEmail,
		// Token: 0x04001386 RID: 4998
		MemberIcon,
		// Token: 0x04001387 RID: 4999
		DeletedOnTime,
		// Token: 0x04001388 RID: 5000
		DumpsterReceivedTime,
		// Token: 0x04001389 RID: 5001
		ObjectDisplayName,
		// Token: 0x0400138A RID: 5002
		ObjectIcon,
		// Token: 0x0400138B RID: 5003
		ConversationLastDeliveryTime,
		// Token: 0x0400138C RID: 5004
		ConversationIcon,
		// Token: 0x0400138D RID: 5005
		ConversationSubject,
		// Token: 0x0400138E RID: 5006
		ConversationUnreadCount,
		// Token: 0x0400138F RID: 5007
		ConversationHasAttachment,
		// Token: 0x04001390 RID: 5008
		ConversationSenderList,
		// Token: 0x04001391 RID: 5009
		ConversationImportance,
		// Token: 0x04001392 RID: 5010
		ConversationCategories,
		// Token: 0x04001393 RID: 5011
		ConversationSize,
		// Token: 0x04001394 RID: 5012
		ConversationFlagStatus,
		// Token: 0x04001395 RID: 5013
		ConversationFlagDueDate,
		// Token: 0x04001396 RID: 5014
		IMAddress,
		// Token: 0x04001397 RID: 5015
		ConversationToList,
		// Token: 0x04001398 RID: 5016
		Count
	}
}
