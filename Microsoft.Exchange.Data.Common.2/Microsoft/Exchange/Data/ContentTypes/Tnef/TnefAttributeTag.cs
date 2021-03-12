using System;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000E5 RID: 229
	public enum TnefAttributeTag
	{
		// Token: 0x040007DD RID: 2013
		Null,
		// Token: 0x040007DE RID: 2014
		From = 32768,
		// Token: 0x040007DF RID: 2015
		Subject = 98308,
		// Token: 0x040007E0 RID: 2016
		DateSent = 229381,
		// Token: 0x040007E1 RID: 2017
		DateReceived,
		// Token: 0x040007E2 RID: 2018
		MessageStatus = 425991,
		// Token: 0x040007E3 RID: 2019
		MessageClass = 491528,
		// Token: 0x040007E4 RID: 2020
		MessageId = 98313,
		// Token: 0x040007E5 RID: 2021
		ParentId,
		// Token: 0x040007E6 RID: 2022
		ConversationId,
		// Token: 0x040007E7 RID: 2023
		Body = 163852,
		// Token: 0x040007E8 RID: 2024
		Priority = 294925,
		// Token: 0x040007E9 RID: 2025
		AttachData = 425999,
		// Token: 0x040007EA RID: 2026
		AttachTitle = 98320,
		// Token: 0x040007EB RID: 2027
		AttachMetaFile = 426001,
		// Token: 0x040007EC RID: 2028
		AttachCreateDate = 229394,
		// Token: 0x040007ED RID: 2029
		AttachModifyDate,
		// Token: 0x040007EE RID: 2030
		DateModified = 229408,
		// Token: 0x040007EF RID: 2031
		AttachTransportFilename = 430081,
		// Token: 0x040007F0 RID: 2032
		AttachRenderData,
		// Token: 0x040007F1 RID: 2033
		MapiProperties,
		// Token: 0x040007F2 RID: 2034
		RecipientTable,
		// Token: 0x040007F3 RID: 2035
		Attachment,
		// Token: 0x040007F4 RID: 2036
		TnefVersion = 561158,
		// Token: 0x040007F5 RID: 2037
		OemCodepage = 430087,
		// Token: 0x040007F6 RID: 2038
		OriginalMessageClass = 458758,
		// Token: 0x040007F7 RID: 2039
		Owner = 393216,
		// Token: 0x040007F8 RID: 2040
		SentFor,
		// Token: 0x040007F9 RID: 2041
		Delegate,
		// Token: 0x040007FA RID: 2042
		DateStart = 196614,
		// Token: 0x040007FB RID: 2043
		DateEnd,
		// Token: 0x040007FC RID: 2044
		AidOwner = 327688,
		// Token: 0x040007FD RID: 2045
		RequestResponse = 262153
	}
}
