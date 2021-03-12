using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000013 RID: 19
	public sealed class MapiAttachment : MapiPropBagBase
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004BAA File Offset: 0x00002DAA
		public MapiAttachment() : base(MapiObjectType.Attachment)
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004BB3 File Offset: 0x00002DB3
		public ExchangeId Atid
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.atid.ConvertNullToZero();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004BC7 File Offset: 0x00002DC7
		public bool CanUseSharedMailboxLockForSave
		{
			get
			{
				return this.storeAttachment != null && this.storeAttachment.Mailbox.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql && this.attachmentTableExists;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004BF6 File Offset: 0x00002DF6
		public override bool CanUseSharedMailboxLockForCopy
		{
			get
			{
				return this.storeAttachment != null && this.storeAttachment.Mailbox.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004C22 File Offset: 0x00002E22
		protected override PropertyBag StorePropertyBag
		{
			get
			{
				return this.StoreAttachment;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004C2A File Offset: 0x00002E2A
		internal Attachment StoreAttachment
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.storeAttachment;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004C3C File Offset: 0x00002E3C
		internal ErrorCode Configure(MapiContext context, MapiMessage parentObject, AttachmentConfigureFlags configFlags, int attachNum)
		{
			base.ParentObject = parentObject;
			base.Logon = parentObject.Logon;
			this.CheckRights(context, MapiAttachment.AttachmentRightsFromAttachmentConfigureFlags(configFlags), false, AccessCheckOperation.AttachmentOpen, (LID)46951U);
			this.storeAttachment = parentObject.StoreMessage.OpenAttachment(context, attachNum);
			if (this.storeAttachment == null)
			{
				return ErrorCode.CreateNotFound((LID)53272U);
			}
			this.attachmentTableExists = true;
			this.atid = this.storeAttachment.GetId(context);
			base.IsValid = true;
			return ErrorCode.NoError;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004CC8 File Offset: 0x00002EC8
		internal ErrorCode ConfigureNew(MapiContext context, MapiMessage parentObject)
		{
			base.ParentObject = parentObject;
			base.Logon = parentObject.Logon;
			base.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, AccessCheckOperation.AttachmentCreate, (LID)63335U);
			this.storeAttachment = parentObject.StoreMessage.CreateAttachment(context);
			this.attachmentTableExists = this.storeAttachment.CheckTableExists(context);
			this.atid = ExchangeId.Null;
			base.IsValid = true;
			return ErrorCode.NoError;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004D38 File Offset: 0x00002F38
		public bool IsEmbeddedMessage(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return this.storeAttachment.IsEmbeddedMessage(context);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004D50 File Offset: 0x00002F50
		public ErrorCode OpenEmbeddedMessage(MapiContext context, MessageConfigureFlags flags, CodePage codePage, out MapiMessage message)
		{
			message = null;
			if (this.embeddedMessage != null)
			{
				return ErrorCode.CreateCallFailed((LID)40984U);
			}
			this.CheckRights(context, MapiMessage.MessageRightsFromMessageConfigureFlags(flags), false, AccessCheckOperation.AttachmentOpenEmbeddedMessage, (LID)38759U);
			ErrorCode errorCode = ErrorCode.NoError;
			MapiMessage mapiMessage = null;
			try
			{
				mapiMessage = new MapiMessage(MapiObjectType.EmbeddedMessage);
				if (flags == MessageConfigureFlags.CreateNewMessage)
				{
					this.NukeContent(context);
					base.InternalSetOnePropShouldNotFail(context, PropTag.Attachment.AttachMethod, 5);
					errorCode = mapiMessage.ConfigureNewEmbeddedMessage(context, this, codePage);
				}
				else
				{
					errorCode = mapiMessage.ConfigureEmbeddedMessage(context, this, flags, codePage);
				}
				if (errorCode == ErrorCode.NoError)
				{
					message = (this.embeddedMessage = mapiMessage);
					mapiMessage = null;
				}
			}
			finally
			{
				if (mapiMessage != null)
				{
					mapiMessage.Dispose();
				}
			}
			return errorCode;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004E10 File Offset: 0x00003010
		private static FolderSecurity.ExchangeSecurityDescriptorFolderRights AttachmentRightsFromAttachmentConfigureFlags(AttachmentConfigureFlags flags)
		{
			if ((flags & AttachmentConfigureFlags.RequestWrite) == AttachmentConfigureFlags.RequestWrite)
			{
				return MapiMessage.WriteRights;
			}
			if ((flags & AttachmentConfigureFlags.RequestReadOnly) == AttachmentConfigureFlags.RequestReadOnly)
			{
				return MapiMessage.ReadOnlyRights;
			}
			return MapiMessage.ReadOnlyRights;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004E30 File Offset: 0x00003030
		private void NukeContent(MapiContext context)
		{
			base.InternalDeleteOnePropShouldNotFail(context, PropTag.Attachment.Content);
			using (EmbeddedMessage embeddedMessage = this.storeAttachment.OpenEmbeddedMessage(context))
			{
				if (embeddedMessage != null)
				{
					embeddedMessage.Delete(context);
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004E7C File Offset: 0x0000307C
		private void PreSaveChangesWork(MapiContext context)
		{
			if (this.StoreAttachment.IsDead)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "SaveChanges on a dead attachment!  Throwing ExExceptionInvalidObject!");
				throw new ExExceptionInvalidObject((LID)47416U, "Object is Dead");
			}
			if (this.streamSizeInvalid)
			{
				throw new ExExceptionMaxSubmissionExceeded((LID)44956U, "Exceeded the size limitation");
			}
			base.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, AccessCheckOperation.AttachmentSaveChanges, (LID)55143U);
			base.CommitDirtyStreams(context);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004EF5 File Offset: 0x000030F5
		public IChunked PrepareToSaveChanges(MapiContext context)
		{
			if (!ConfigurationSchema.AttachmentMessageSaveChunking.Value)
			{
				return null;
			}
			this.PreSaveChangesWork(context);
			if (this.StoreAttachment.GetLargeDirtyStreamsSize() < ConfigurationSchema.AttachmentMessageSaveChunkingMinSize.Value)
			{
				return null;
			}
			return this.StoreAttachment.PrepareToSaveChanges(context);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004F34 File Offset: 0x00003134
		public void SaveChanges(MapiContext context)
		{
			this.PreSaveChangesWork(context);
			object onePropValue = base.GetOnePropValue(context, PropTag.Attachment.AttachMethod);
			if (this.IsEmbeddedMessage(context))
			{
				if (onePropValue == null || (int)onePropValue != 5)
				{
					base.InternalSetOnePropShouldNotFail(context, PropTag.Attachment.AttachMethod, 5);
				}
			}
			else if (onePropValue == null || (int)onePropValue == 5)
			{
				base.InternalSetOnePropShouldNotFail(context, PropTag.Attachment.AttachMethod, 1);
			}
			if (!this.StoreAttachment.SaveChanges(context))
			{
				throw new ExExceptionObjectChanged((LID)58940U, "attachment was changed from under us");
			}
			this.atid = this.storeAttachment.GetId(context);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004FD1 File Offset: 0x000031D1
		internal ErrorCode Delete(MapiContext context)
		{
			this.StoreAttachment.Delete(context);
			return ErrorCode.NoError;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004FE4 File Offset: 0x000031E4
		public override bool IsStreamSizeInvalid(MapiContext context, long size)
		{
			this.streamSizeInvalid |= base.ParentObject.IsStreamSizeInvalid(context, size);
			return this.streamSizeInvalid;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005008 File Offset: 0x00003208
		protected override ErrorCode InternalSetOneProp(MapiContext context, StorePropTag propTag, object objValue)
		{
			if (propTag == PropTag.Attachment.AttachMethod)
			{
				int? num = objValue as int?;
				if (num != null && num.Value == 3)
				{
					((MapiMessage)base.ParentObject).SetNeedAttachResolve(context, true);
				}
			}
			return base.InternalSetOneProp(context, propTag, objValue);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000505C File Offset: 0x0000325C
		protected override List<Property> InternalGetAllProperties(MapiContext context, GetPropListFlags flags, bool loadValues, Predicate<StorePropTag> propertyFilter)
		{
			List<Property> list = base.InternalGetAllProperties(context, flags, loadValues, propertyFilter);
			int? num = base.GetOnePropValue(context, PropTag.Attachment.AttachMethod) as int?;
			if (num != null)
			{
				if (num.Value == 6)
				{
					Properties.RemoveFrom(list, PropTag.Attachment.Content);
				}
				else if (num.Value == 5)
				{
					list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Attachment.ContentObj) : new Property(PropTag.Attachment.ContentObj, null));
				}
				else
				{
					Properties.RemoveFrom(list, PropTag.Attachment.ContentObj);
				}
			}
			else
			{
				Properties.RemoveFrom(list, PropTag.Attachment.ContentObj);
				Properties.RemoveFrom(list, PropTag.Attachment.Content);
			}
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Attachment.AttachNum) : new Property(PropTag.Attachment.AttachNum, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Attachment.AccessLevel) : new Property(PropTag.Attachment.AccessLevel, null));
			return list;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005144 File Offset: 0x00003344
		protected override bool TryGetPropertyImp(MapiContext context, ushort propId, out StorePropTag actualPropTag, out object propValue)
		{
			if (propId == 3617)
			{
				propValue = this.GetAttachmentNumber();
				actualPropTag = PropTag.Attachment.AttachNum;
				return true;
			}
			if (propId != 4087)
			{
				return base.TryGetPropertyImp(context, propId, out actualPropTag, out propValue);
			}
			propValue = this.GetAccessLevel();
			actualPropTag = PropTag.Attachment.AccessLevel;
			return true;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000051A8 File Offset: 0x000033A8
		protected override object GetPropertyValueImp(MapiContext context, StorePropTag propTag)
		{
			uint propTag2 = propTag.PropTag;
			object result;
			if (propTag2 != 237043715U)
			{
				if (propTag2 != 267845635U)
				{
					result = base.GetPropertyValueImp(context, propTag);
				}
				else
				{
					result = this.GetAccessLevel();
				}
			}
			else
			{
				result = this.GetAttachmentNumber();
			}
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000051F8 File Offset: 0x000033F8
		protected override void CopyToInternal(MapiContext context, MapiPropBagBase destination, IList<StorePropTag> propTagsExclude, CopyToFlags flags, ref List<MapiPropertyProblem> propProblems)
		{
			base.ThrowIfNotValid(null);
			if (destination == null)
			{
				throw base.CreateCopyToNotSupportedException((LID)39224U, destination);
			}
			if (object.ReferenceEquals(this, destination))
			{
				throw new ExExceptionAccessDenied((LID)55608U, "Cannot copy attachment onto itself");
			}
			if (this.IsEmbeddedMessage(context))
			{
				if (propTagsExclude != null && propTagsExclude.Count != 0)
				{
					for (int i = 0; i < propTagsExclude.Count; i++)
					{
						if (propTagsExclude[i].PropId == PropTag.Attachment.Content.PropId)
						{
							flags &= ~(CopyToFlags.CopyEmbeddedMessage | CopyToFlags.CopyFirstLevelEmbeddedMessage);
						}
					}
				}
				if (((CopyToFlags.CopyEmbeddedMessage | CopyToFlags.CopyFirstLevelEmbeddedMessage) & flags) != CopyToFlags.None)
				{
					this.CopyToCopyEmbeddedMessageToDestination(context, (MapiAttachment)destination, flags);
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000052A8 File Offset: 0x000034A8
		private void CopyToCopyEmbeddedMessageToDestination(MapiContext context, MapiAttachment destination, CopyToFlags flags)
		{
			if ((CopyToFlags.DoNotReplaceProperties & flags) != CopyToFlags.None && destination.IsEmbeddedMessage(context))
			{
				return;
			}
			destination.NukeContent(context);
			MapiMessage mapiMessage;
			if (this.embeddedMessage == null)
			{
				this.OpenEmbeddedMessage(context, MessageConfigureFlags.None, CodePage.Unicode, out mapiMessage);
			}
			else
			{
				mapiMessage = this.embeddedMessage;
			}
			MapiMessage mapiMessage2;
			destination.OpenEmbeddedMessage(context, MessageConfigureFlags.CreateNewMessage, CodePage.Unicode, out mapiMessage2);
			List<MapiPropertyProblem> list = null;
			bool flag = (flags & CopyToFlags.CopyFirstLevelEmbeddedMessage) == CopyToFlags.None;
			mapiMessage.CopyTo(context, mapiMessage2, null, flag ? (CopyToFlags.CopyRecipients | CopyToFlags.CopyAttachments) : CopyToFlags.CopyRecipients, ref list);
			ExchangeId exchangeId;
			mapiMessage2.SaveChanges(context, MapiSaveMessageChangesFlags.None, out exchangeId);
			if ((CopyToFlags.MoveProperties & flags) != CopyToFlags.None)
			{
				this.NukeContent(context);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005334 File Offset: 0x00003534
		public override void CopyTo(MapiContext context, MapiPropBagBase destination, IList<StorePropTag> propTagsExclude, CopyToFlags flags, ref List<MapiPropertyProblem> propProblems)
		{
			if (this.Atid.IsNullOrZero)
			{
				return;
			}
			base.CopyTo(context, destination, propTagsExclude, flags, ref propProblems);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005360 File Offset: 0x00003560
		public override void CopyProps(MapiContext context, MapiPropBagBase destination, IList<StorePropTag> propTags, bool replaceIfExists, ref List<MapiPropertyProblem> propProblems)
		{
			base.ThrowIfNotValid(null);
			if (!(destination is MapiAttachment))
			{
				throw base.CreateCopyPropsNotSupportedException((LID)45368U, destination);
			}
			if (object.ReferenceEquals(this, destination))
			{
				throw new ExExceptionAccessDenied((LID)49464U, "Cannot copy attachment onto itself");
			}
			propProblems = null;
			if (this.Atid.IsNullOrZero)
			{
				return;
			}
			List<StorePropTag> list = new List<StorePropTag>(propTags);
			bool flag = false;
			if (this.IsEmbeddedMessage(context))
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].PropId == PropTag.Attachment.Content.PropId)
					{
						flag = true;
						list.RemoveAt(i);
						i--;
					}
				}
			}
			this.CopyToRemoveNoAccessProps(context, destination, list);
			if (MapiPropBagBase.copyToTestHook.Value != null)
			{
				MapiPropBagBase.copyToTestHook.Value(list);
				return;
			}
			if (!replaceIfExists)
			{
				MapiPropBagBase.CopyToRemovePreexistingDestinationProperties(context, destination, list);
			}
			if (list.Count != 0)
			{
				base.CopyToCopyPropertiesToDestination(context, list, destination, ref propProblems);
			}
			if (flag)
			{
				this.CopyToCopyEmbeddedMessageToDestination(context, (MapiAttachment)destination, replaceIfExists ? CopyToFlags.None : CopyToFlags.DoNotReplaceProperties);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005471 File Offset: 0x00003671
		public int GetAttachmentNumber()
		{
			base.ThrowIfNotValid(null);
			return this.storeAttachment.AttachmentNumber;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005485 File Offset: 0x00003685
		public int GetAccessLevel()
		{
			base.ThrowIfNotValid(null);
			return 0;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000548F File Offset: 0x0000368F
		internal void EmbeddedMessageClosed(MapiMessage message)
		{
			this.embeddedMessage = null;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005498 File Offset: 0x00003698
		internal override void CheckRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, bool allRights, AccessCheckOperation operation, LID lid)
		{
			base.ParentObject.CheckRights(context, requestedRights, allRights, operation, lid);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000054AC File Offset: 0x000036AC
		internal override void CheckPropertyRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, AccessCheckOperation operation, LID lid)
		{
			base.ParentObject.CheckPropertyRights(context, requestedRights, operation, lid);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000054BE File Offset: 0x000036BE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiAttachment>(this);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000054C6 File Offset: 0x000036C6
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.embeddedMessage != null)
				{
					this.embeddedMessage.Dispose();
					this.embeddedMessage = null;
				}
				if (this.storeAttachment != null)
				{
					this.storeAttachment.Dispose();
					this.storeAttachment = null;
				}
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x04000061 RID: 97
		private Attachment storeAttachment;

		// Token: 0x04000062 RID: 98
		private MapiMessage embeddedMessage;

		// Token: 0x04000063 RID: 99
		private ExchangeId atid;

		// Token: 0x04000064 RID: 100
		private bool streamSizeInvalid;

		// Token: 0x04000065 RID: 101
		private bool attachmentTableExists;
	}
}
