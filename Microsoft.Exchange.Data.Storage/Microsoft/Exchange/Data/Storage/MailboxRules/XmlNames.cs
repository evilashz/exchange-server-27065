using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000C04 RID: 3076
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class XmlNames
	{
		// Token: 0x04003E63 RID: 15971
		public const string Mailbox = "Mailbox";

		// Token: 0x04003E64 RID: 15972
		public const string ReceiveFolders = "ReceiveFolders";

		// Token: 0x04003E65 RID: 15973
		public const string ReceiveFolder = "ReceiveFolder";

		// Token: 0x04003E66 RID: 15974
		public const string MessageClass = "MessageClass";

		// Token: 0x04003E67 RID: 15975
		public const string ExplicitClass = "ExplicitClass";

		// Token: 0x04003E68 RID: 15976
		public const string Folder = "Folder";

		// Token: 0x04003E69 RID: 15977
		public const string Rules = "Rules";

		// Token: 0x04003E6A RID: 15978
		public const string Owner = "Owner";

		// Token: 0x04003E6B RID: 15979
		public const string Error = "Error";

		// Token: 0x04003E6C RID: 15980
		public const string FolderName = "FolderName";

		// Token: 0x04003E6D RID: 15981
		public const string NumberOfRules = "NumberOfRules";

		// Token: 0x04003E6E RID: 15982
		public const string Rule = "Rule";

		// Token: 0x04003E6F RID: 15983
		public const string Index = "Index";

		// Token: 0x04003E70 RID: 15984
		public const string Name = "Name";

		// Token: 0x04003E71 RID: 15985
		public const string Provider = "Provider";

		// Token: 0x04003E72 RID: 15986
		public const string Id = "Id";

		// Token: 0x04003E73 RID: 15987
		public const string ExecutionSequence = "ExecutionSequence";

		// Token: 0x04003E74 RID: 15988
		public const string Level = "Level";

		// Token: 0x04003E75 RID: 15989
		public const string IsExtended = "IsExtended";

		// Token: 0x04003E76 RID: 15990
		public const string StateFlags = "StateFlags";

		// Token: 0x04003E77 RID: 15991
		public const string UserFlags = "UserFlags";

		// Token: 0x04003E78 RID: 15992
		public const string ProviderData = "ProviderData";

		// Token: 0x04003E79 RID: 15993
		public const string ExtraProperties = "ExtraProperties";

		// Token: 0x04003E7A RID: 15994
		public const string Restrictions = "Restrictions";

		// Token: 0x04003E7B RID: 15995
		public const string Restriction = "Restriction";

		// Token: 0x04003E7C RID: 15996
		public const string Count = "Count";

		// Token: 0x04003E7D RID: 15997
		public const string And = "And";

		// Token: 0x04003E7E RID: 15998
		public const string Or = "Or";

		// Token: 0x04003E7F RID: 15999
		public const string Not = "Not";

		// Token: 0x04003E80 RID: 16000
		public const string Sub = "Sub";

		// Token: 0x04003E81 RID: 16001
		public const string Comment = "Comment";

		// Token: 0x04003E82 RID: 16002
		public const string Recipients = "Recipients";

		// Token: 0x04003E83 RID: 16003
		public const string Type = "Type";

		// Token: 0x04003E84 RID: 16004
		public const string ContentFlags = "ContentFlags";

		// Token: 0x04003E85 RID: 16005
		public const string PropTag = "PropTag";

		// Token: 0x04003E86 RID: 16006
		public const string MultiValued = "MultiValued";

		// Token: 0x04003E87 RID: 16007
		public const string Operation = "Operation";

		// Token: 0x04003E88 RID: 16008
		public const string Mask = "Mask";

		// Token: 0x04003E89 RID: 16009
		public const string PropTagLeft = "PropTagLeft";

		// Token: 0x04003E8A RID: 16010
		public const string PropTagRight = "PropTagRight";

		// Token: 0x04003E8B RID: 16011
		public const string Size = "Size";

		// Token: 0x04003E8C RID: 16012
		public const string SubType = "SubType";

		// Token: 0x04003E8D RID: 16013
		public const string Actions = "Actions";

		// Token: 0x04003E8E RID: 16014
		public const string Action = "Action";

		// Token: 0x04003E8F RID: 16015
		public const string Defer = "Defer";

		// Token: 0x04003E90 RID: 16016
		public const string Bounce = "Bounce";

		// Token: 0x04003E91 RID: 16017
		public const string BounceCode = "BounceCode";

		// Token: 0x04003E92 RID: 16018
		public const string Forward = "Forward";

		// Token: 0x04003E93 RID: 16019
		public const string ForwardFlags = "ForwardFlags";

		// Token: 0x04003E94 RID: 16020
		public const string Delegate = "Delegate";

		// Token: 0x04003E95 RID: 16021
		public const string Recipient = "Recipient";

		// Token: 0x04003E96 RID: 16022
		public const string Reply = "Reply";

		// Token: 0x04003E97 RID: 16023
		public const string OofReply = "OofReply";

		// Token: 0x04003E98 RID: 16024
		public const string ReplySubject = "ReplySubject";

		// Token: 0x04003E99 RID: 16025
		public const string ReplyBody = "ReplyBody";

		// Token: 0x04003E9A RID: 16026
		public const string ReplyTemplateMessageId = "ReplyTemplateMessageId";

		// Token: 0x04003E9B RID: 16027
		public const string ReplyTemplateGuid = "ReplyTemplateGuid";

		// Token: 0x04003E9C RID: 16028
		public const string FolderEntry = "FolderEntry";

		// Token: 0x04003E9D RID: 16029
		public const string StoreEntry = "StoreEntry";

		// Token: 0x04003E9E RID: 16030
		public const string ActionFlags = "ActionFlags";

		// Token: 0x04003E9F RID: 16031
		public const string PropValue = "PropValue";

		// Token: 0x04003EA0 RID: 16032
		public const string Property = "Property";

		// Token: 0x04003EA1 RID: 16033
		public const string DataType = "DataType";

		// Token: 0x04003EA2 RID: 16034
		public const string Value = "Value";

		// Token: 0x04003EA3 RID: 16035
		public const string Values = "Values";

		// Token: 0x04003EA4 RID: 16036
		public const string Exception = "Exception";

		// Token: 0x04003EA5 RID: 16037
		public const string AttemptedAction = "AttemptedAction";

		// Token: 0x04003EA6 RID: 16038
		public const string ExceptionType = "ExceptionType";

		// Token: 0x04003EA7 RID: 16039
		public const string ExceptionMessage = "ExceptionMessage";

		// Token: 0x04003EA8 RID: 16040
		public const string StackTrace = "StackTrace";
	}
}
