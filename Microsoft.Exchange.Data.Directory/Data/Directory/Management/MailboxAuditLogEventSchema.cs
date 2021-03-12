using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200072D RID: 1837
	internal class MailboxAuditLogEventSchema : MailboxAuditLogRecordSchema
	{
		// Token: 0x04003B48 RID: 15176
		public static readonly SimpleProviderPropertyDefinition Operation = new SimpleProviderPropertyDefinition("Operation", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B49 RID: 15177
		public static readonly SimpleProviderPropertyDefinition OperationResult = new SimpleProviderPropertyDefinition("OperationResult", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B4A RID: 15178
		public static readonly SimpleProviderPropertyDefinition LogonType = new SimpleProviderPropertyDefinition("LogonType", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B4B RID: 15179
		public static readonly SimpleProviderPropertyDefinition ExternalAccess = new SimpleProviderPropertyDefinition("ExternalAccess", ExchangeObjectVersion.Exchange2010, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B4C RID: 15180
		public static readonly SimpleProviderPropertyDefinition FolderId = new SimpleProviderPropertyDefinition("FolderId", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B4D RID: 15181
		public static readonly SimpleProviderPropertyDefinition FolderPathName = new SimpleProviderPropertyDefinition("FolderPathName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B4E RID: 15182
		public static readonly SimpleProviderPropertyDefinition DestFolderId = new SimpleProviderPropertyDefinition("DestFolderId", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B4F RID: 15183
		public static readonly SimpleProviderPropertyDefinition DestFolderPathName = new SimpleProviderPropertyDefinition("DestFolderPathName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B50 RID: 15184
		public static readonly SimpleProviderPropertyDefinition ClientIPAddress = new SimpleProviderPropertyDefinition("CientIPAddress", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B51 RID: 15185
		public static readonly SimpleProviderPropertyDefinition ClientMachineName = new SimpleProviderPropertyDefinition("CientMachineName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B52 RID: 15186
		public static readonly SimpleProviderPropertyDefinition ClientProcessName = new SimpleProviderPropertyDefinition("CientProcessName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B53 RID: 15187
		public static readonly SimpleProviderPropertyDefinition ClientVersion = new SimpleProviderPropertyDefinition("CientVersion", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B54 RID: 15188
		public static readonly SimpleProviderPropertyDefinition ClientInfoString = new SimpleProviderPropertyDefinition("ClientInfoString", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B55 RID: 15189
		public static readonly SimpleProviderPropertyDefinition InternalLogonType = new SimpleProviderPropertyDefinition("InternalLogonType", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B56 RID: 15190
		public static readonly SimpleProviderPropertyDefinition CrossMailboxOperation = new SimpleProviderPropertyDefinition("CrossMailboxOperation", ExchangeObjectVersion.Exchange2010, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B57 RID: 15191
		public static readonly SimpleProviderPropertyDefinition LogonUserDisplayName = new SimpleProviderPropertyDefinition("LogonUserDisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B58 RID: 15192
		public static readonly SimpleProviderPropertyDefinition LogonUserSid = new SimpleProviderPropertyDefinition("LogonUserSid", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B59 RID: 15193
		public static readonly SimpleProviderPropertyDefinition DestMailboxGuid = new SimpleProviderPropertyDefinition("DestMailboxGuid", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B5A RID: 15194
		public static readonly SimpleProviderPropertyDefinition MailboxOwnerUPN = new SimpleProviderPropertyDefinition("MailboxOwnerUPN", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B5B RID: 15195
		public static readonly SimpleProviderPropertyDefinition MailboxOwnerSid = new SimpleProviderPropertyDefinition("MailboxOwnerSid", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B5C RID: 15196
		public static readonly SimpleProviderPropertyDefinition DestMailboxOwnerUPN = new SimpleProviderPropertyDefinition("DestMailboxOwnerUPN", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B5D RID: 15197
		public static readonly SimpleProviderPropertyDefinition DestMailboxOwnerSid = new SimpleProviderPropertyDefinition("DestMailboxOwnerSid", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B5E RID: 15198
		public static readonly SimpleProviderPropertyDefinition SourceItems = new SimpleProviderPropertyDefinition("SourceItems", ExchangeObjectVersion.Exchange2010, typeof(MailboxAuditLogSourceItem), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B5F RID: 15199
		public static readonly SimpleProviderPropertyDefinition SourceFolders = new SimpleProviderPropertyDefinition("SourceFolders", ExchangeObjectVersion.Exchange2010, typeof(MailboxAuditLogSourceFolder), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B60 RID: 15200
		public static readonly SimpleProviderPropertyDefinition ItemId = new SimpleProviderPropertyDefinition("ItemId", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B61 RID: 15201
		public static readonly SimpleProviderPropertyDefinition ItemSubject = new SimpleProviderPropertyDefinition("ItemSubject", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B62 RID: 15202
		public static readonly SimpleProviderPropertyDefinition DirtyProperties = new SimpleProviderPropertyDefinition("DirtyProperties", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B63 RID: 15203
		public static readonly SimpleProviderPropertyDefinition OriginatingServer = new SimpleProviderPropertyDefinition("OriginatingServer", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
