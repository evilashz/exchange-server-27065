using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000015 RID: 21
	[Flags]
	public enum RopFlags
	{
		// Token: 0x04000089 RID: 137
		Private = 1,
		// Token: 0x0400008A RID: 138
		Undercover = 2,
		// Token: 0x0400008B RID: 139
		Ghosted = 4,
		// Token: 0x0400008C RID: 140
		SplProcess = 8,
		// Token: 0x0400008D RID: 141
		Mapi0 = 16,
		// Token: 0x0400008E RID: 142
		MbxGuids = 32,
		// Token: 0x0400008F RID: 143
		Extended = 64,
		// Token: 0x04000090 RID: 144
		NoRulesInterface = 128,
		// Token: 0x04000091 RID: 145
		ReadOnly = 0,
		// Token: 0x04000092 RID: 146
		ReadWrite = 1,
		// Token: 0x04000093 RID: 147
		Create = 2,
		// Token: 0x04000094 RID: 148
		BestAccess = 3,
		// Token: 0x04000095 RID: 149
		OpenSoftDeleted = 4,
		// Token: 0x04000096 RID: 150
		NoBlock = 8,
		// Token: 0x04000097 RID: 151
		Append = 4,
		// Token: 0x04000098 RID: 152
		Wait = 0,
		// Token: 0x04000099 RID: 153
		NoWait = 1,
		// Token: 0x0400009A RID: 154
		Advance = 0,
		// Token: 0x0400009B RID: 155
		NoAdvance = 1,
		// Token: 0x0400009C RID: 156
		SendMax = 2,
		// Token: 0x0400009D RID: 157
		Close = 0,
		// Token: 0x0400009E RID: 158
		KeepOpenReadOnly = 1,
		// Token: 0x0400009F RID: 159
		KeepOpenReadWrite = 2,
		// Token: 0x040000A0 RID: 160
		ForceSave = 4,
		// Token: 0x040000A1 RID: 161
		DelayedCall = 8,
		// Token: 0x040000A2 RID: 162
		ICS = 512,
		// Token: 0x040000A3 RID: 163
		ExtendedReg = 4,
		// Token: 0x040000A4 RID: 164
		ReplaceAll = 1,
		// Token: 0x040000A5 RID: 165
		Categorize = 1,
		// Token: 0x040000A6 RID: 166
		Associated = 2,
		// Token: 0x040000A7 RID: 167
		Depth = 4,
		// Token: 0x040000A8 RID: 168
		DeferredErrors = 8,
		// Token: 0x040000A9 RID: 169
		NoNotifs = 16,
		// Token: 0x040000AA RID: 170
		SoftDeletes = 32,
		// Token: 0x040000AB RID: 171
		MapiUnicode = 64,
		// Token: 0x040000AC RID: 172
		IgnoreMe = 128,
		// Token: 0x040000AD RID: 173
		MarkRead = 1,
		// Token: 0x040000AE RID: 174
		SendRN = 2,
		// Token: 0x040000AF RID: 175
		Preprocess = 1,
		// Token: 0x040000B0 RID: 176
		SystemSubmit = 16,
		// Token: 0x040000B1 RID: 177
		NeedsSpooler = 8192,
		// Token: 0x040000B2 RID: 178
		IgnoreSendAsRight = 131072,
		// Token: 0x040000B3 RID: 179
		GenByRule = 16384,
		// Token: 0x040000B4 RID: 180
		DelegateSend = 32768,
		// Token: 0x040000B5 RID: 181
		delMessages = 1,
		// Token: 0x040000B6 RID: 182
		delFolders = 4,
		// Token: 0x040000B7 RID: 183
		delAssociated = 8,
		// Token: 0x040000B8 RID: 184
		delHardDelete = 16,
		// Token: 0x040000B9 RID: 185
		delAssoc = 1,
		// Token: 0x040000BA RID: 186
		delForce = 2,
		// Token: 0x040000BB RID: 187
		clientAssociatedFlag = 64,
		// Token: 0x040000BC RID: 188
		contentAggregation = 1
	}
}
