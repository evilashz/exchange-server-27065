﻿using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Protocols.MAPI.ExtensionMethods;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200006B RID: 107
	public class MapiMessage : MapiPropBagBase
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x000147F4 File Offset: 0x000129F4
		private static Hookable<MapiMessage.GenerateReadReportDelegate> HookableGenerateReadReport
		{
			get
			{
				if (MapiMessage.hookableGenerateReadReport == null)
				{
					MapiMessage.hookableGenerateReadReport = Hookable<MapiMessage.GenerateReadReportDelegate>.Create(true, new MapiMessage.GenerateReadReportDelegate(MapiMessage.InternalGenerateReadReport));
				}
				return MapiMessage.hookableGenerateReadReport;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00014819 File Offset: 0x00012A19
		internal static IDisposable SetGenerateReadReportHook(MapiMessage.GenerateReadReportDelegate hook)
		{
			return MapiMessage.HookableGenerateReadReport.SetTestHook(hook);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00014826 File Offset: 0x00012A26
		internal static IDisposable SetCreateTargetMessageForOutlookHook(Func<ErrorCode> hook)
		{
			return MapiMessage.hookableCreateTargetMessageForOutlook.SetTestHook(hook);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00014834 File Offset: 0x00012A34
		internal new static ErrorCode CheckPropertyOperationAllowed(MapiContext context, MapiLogon logon, bool isEmbedded, MapiPropBagBase.PropOperation operation, StorePropTag propTag, object value)
		{
			if (operation == MapiPropBagBase.PropOperation.SetProps)
			{
				uint propTag2 = propTag.PropTag;
				if (propTag2 == 237174787U)
				{
					return ErrorCode.NoError;
				}
			}
			return MapiPropBagBase.CheckPropertyOperationAllowed(context, logon, isEmbedded, operation, propTag, value);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001486C File Offset: 0x00012A6C
		internal static ErrorCode InternalSetOneProp(MapiContext context, MapiLogon logon, Message storeMessage, bool isScrubbed, StorePropTag propTag, object objValue)
		{
			if (!logon.IsMoveUser)
			{
				ushort propId = propTag.PropId;
				if (propId <= 3619)
				{
					if (propId == 3591)
					{
						MessageFlags messageFlags;
						if (objValue == null || !(objValue is int))
						{
							messageFlags = MessageFlags.None;
						}
						else
						{
							messageFlags = (MessageFlags)((int)objValue);
						}
						MessageFlags messageFlags2 = MessageFlags.None;
						if (storeMessage.IsEmbedded)
						{
							messageFlags2 = (MessageFlags.Read | MessageFlags.Unmodified | MessageFlags.Unsent | MessageFlags.Resend | MessageFlags.NeedSpecialRecipientProcessing);
						}
						else if (storeMessage.IsNew || isScrubbed)
						{
							messageFlags2 = (MessageFlags.Read | MessageFlags.Unsent | MessageFlags.Resend | MessageFlags.ReadNotificationPending | MessageFlags.NonReadNotificationPending | MessageFlags.NeedSpecialRecipientProcessing);
						}
						MessageFlags flagsToSet = messageFlags & messageFlags2;
						MessageFlags flagsToClear = (messageFlags & messageFlags2) ^ messageFlags2;
						storeMessage.AdjustMessageFlags(context, flagsToSet, flagsToClear);
						DiagnosticContext.TraceLocation((LID)29756U);
						return ErrorCode.NoError;
					}
					if (propId != 3619)
					{
						goto IL_14A;
					}
				}
				else if (propId != 3631)
				{
					switch (propId)
					{
					case 25088:
					case 25089:
					{
						TopMessage topMessage = storeMessage as TopMessage;
						if (topMessage == null || topMessage.ParentFolder.IsIpmFolder(context))
						{
							return ErrorCode.CreateComputed((LID)55904U, propTag.PropTag);
						}
						goto IL_14A;
					}
					default:
						goto IL_14A;
					}
				}
				DiagnosticContext.TraceLocation((LID)45995U);
				return ErrorCode.NoError;
			}
			ushort propId2 = propTag.PropId;
			if (propId2 == 3591)
			{
				MessageFlags messageFlags3;
				if (objValue == null || !(objValue is int))
				{
					messageFlags3 = MessageFlags.None;
				}
				else
				{
					messageFlags3 = (MessageFlags)((int)objValue);
				}
				if ((messageFlags3 & MessageFlags.Submit) != MessageFlags.None)
				{
					messageFlags3 = ((messageFlags3 & ~MessageFlags.Submit) | MessageFlags.Unsent);
					objValue = messageFlags3;
				}
			}
			IL_14A:
			return MapiPropBagBase.InternalSetOneProp(context, logon, storeMessage, propTag, objValue);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000149CF File Offset: 0x00012BCF
		public MapiMessage() : this(MapiObjectType.Message)
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000149D8 File Offset: 0x00012BD8
		public MapiMessage(MapiObjectType mapiObjectType) : base(mapiObjectType)
		{
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000149EC File Offset: 0x00012BEC
		private static MapiMessage.GenerateReadReportDelegate GenerateReadReport
		{
			get
			{
				return MapiMessage.HookableGenerateReadReport.Value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000149F8 File Offset: 0x00012BF8
		public CodePage CodePage
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.codePage;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00014A08 File Offset: 0x00012C08
		// (set) Token: 0x060002CE RID: 718 RVA: 0x00014A38 File Offset: 0x00012C38
		public override bool IsReadOnly
		{
			get
			{
				MapiBase mapiBase = this;
				while (!(mapiBase.ParentObject is MapiLogon))
				{
					mapiBase = mapiBase.ParentObject;
				}
				return ((MapiMessage)mapiBase).readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00014A41 File Offset: 0x00012C41
		internal bool IsScrubbed
		{
			get
			{
				return this.scrubbed;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00014A49 File Offset: 0x00012C49
		internal ExchangeId Mid
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.mid.ConvertNullToZero();
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00014A5D File Offset: 0x00012C5D
		protected override PropertyBag StorePropertyBag
		{
			get
			{
				return this.StoreMessage;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00014A65 File Offset: 0x00012C65
		internal Message StoreMessage
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.storeMessage;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00014A74 File Offset: 0x00012C74
		public int AttachmentCount
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.storeMessage.AttachCount;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00014A88 File Offset: 0x00012C88
		public bool IsEmbedded
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.storeMessage.IsEmbedded;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00014A9C File Offset: 0x00012C9C
		public bool CanUseSharedMailboxLockForSave
		{
			get
			{
				return this.storeMessage != null && this.storeMessage.IsEmbedded && this.storeMessage.Mailbox.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql && this.attachmentTableExists;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00014AD8 File Offset: 0x00012CD8
		public override bool CanUseSharedMailboxLockForCopy
		{
			get
			{
				return this.storeMessage != null && this.storeMessage.Mailbox.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00014B04 File Offset: 0x00012D04
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00014B0C File Offset: 0x00012D0C
		public bool IsContentAggregation { get; set; }

		// Token: 0x060002D9 RID: 729 RVA: 0x00014B18 File Offset: 0x00012D18
		public static ErrorCode SendNotReadReportIfNeeded(MapiContext context, MapiLogon mapiLogon, Message storeMessage, bool messageExpired)
		{
			if (storeMessage.GetNeedsNotReadNotification(context))
			{
				Property oneProp = mapiLogon.GetOneProp(context, PropTag.Mailbox.InternetMdns);
				bool flag = oneProp.IsError || !(bool)oneProp.Value;
				bool flag2 = context.ClientType == ClientType.Migration || context.ClientType == ClientType.SimpleMigration || context.ClientType == ClientType.TransportSync || context.ClientType == ClientType.PublicFolderSystem;
				bool isPerUserReadUnreadTrackingEnabled = (storeMessage as TopMessage).ParentFolder.IsPerUserReadUnreadTrackingEnabled;
				if (flag && !flag2 && !mapiLogon.MailboxInfo.IsDiscoveryMailbox && !isPerUserReadUnreadTrackingEnabled)
				{
					ErrorCode first = MapiMessage.GenerateReadReport(context, mapiLogon, storeMessage, true, messageExpired);
					if (first != ErrorCode.NoError)
					{
						return first.Propagate((LID)29704U);
					}
				}
				storeMessage.SetReadReceiptSent(context, true);
				storeMessage.SaveChanges(context);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00014BF4 File Offset: 0x00012DF4
		internal static string GenerateReadReportMessageClass(string messageClass, bool notRead)
		{
			string str = "REPORT";
			if (messageClass.Length > 0)
			{
				str = str + "." + messageClass;
			}
			return str + (notRead ? ".IPNNRN" : ".IPNRN");
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00014C34 File Offset: 0x00012E34
		internal static bool IsReadReport(string messageClass)
		{
			return messageClass.StartsWith("REPORT.", StringComparison.OrdinalIgnoreCase) && (messageClass.EndsWith(".IPNRN", StringComparison.OrdinalIgnoreCase) || messageClass.EndsWith(".IPNNRN", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00014C64 File Offset: 0x00012E64
		internal static SaveMessageChangesFlags ToLogicalLayerSaveChangesFlags(MapiSaveMessageChangesFlags flags)
		{
			SaveMessageChangesFlags saveMessageChangesFlags = SaveMessageChangesFlags.None;
			if ((flags & MapiSaveMessageChangesFlags.IMAPIDChange) != MapiSaveMessageChangesFlags.None)
			{
				saveMessageChangesFlags |= SaveMessageChangesFlags.IMAPIDChange;
			}
			if ((flags & MapiSaveMessageChangesFlags.ForceSave) != MapiSaveMessageChangesFlags.None)
			{
				saveMessageChangesFlags |= SaveMessageChangesFlags.ForceSave;
			}
			if ((flags & MapiSaveMessageChangesFlags.SkipFolderQuotaCheck) != MapiSaveMessageChangesFlags.None)
			{
				saveMessageChangesFlags |= SaveMessageChangesFlags.SkipFolderQuotaCheck;
			}
			if ((flags & MapiSaveMessageChangesFlags.SkipMailboxQuotaCheck) != MapiSaveMessageChangesFlags.None)
			{
				saveMessageChangesFlags |= SaveMessageChangesFlags.SkipMailboxQuotaCheck;
			}
			if ((flags & MapiSaveMessageChangesFlags.NonFatalDuplicateKey) != MapiSaveMessageChangesFlags.None)
			{
				saveMessageChangesFlags |= SaveMessageChangesFlags.NonFatalDuplicateKey;
			}
			if ((flags & MapiSaveMessageChangesFlags.ForceCreatedEventForCopy) != MapiSaveMessageChangesFlags.None)
			{
				saveMessageChangesFlags |= SaveMessageChangesFlags.ForceCreatedEventForCopy;
			}
			return saveMessageChangesFlags;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00014CB4 File Offset: 0x00012EB4
		public ErrorCode ConfigureMessage(MapiContext context, MapiLogon mapiLogon, ExchangeId fid, ExchangeId mid, MessageConfigureFlags flags, CodePage codePage)
		{
			return this.ConfigureMessage(context, mapiLogon, fid, mid, flags, codePage, null);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00014CC8 File Offset: 0x00012EC8
		public ErrorCode ConfigureMessage(MapiContext context, MapiLogon mapiLogon, ExchangeId fid, ExchangeId mid, MessageConfigureFlags flags, CodePage codePage, MapiMessage sourceMessage)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			MapiFolder.GhostedFolderCheck(context, mapiLogon, fid, (LID)58521U);
			this.codePage = codePage;
			base.Logon = mapiLogon;
			base.IsValid = true;
			if ((flags & MessageConfigureFlags.CreateNewMessage) != MessageConfigureFlags.None)
			{
				this.fid = fid;
				this.mid = ExchangeId.Null;
				Folder folder = Folder.OpenFolder(context, base.Logon.StoreMailbox, fid);
				if (folder == null)
				{
					base.IsValid = false;
					return ErrorCode.CreateNotFound((LID)55320U);
				}
				if (folder is SearchFolder)
				{
					base.IsValid = false;
					return ErrorCode.CreateSearchFolder((LID)43032U);
				}
				if (ConfigurationSchema.CheckQuotaOnMessageCreate.Value && (flags & MessageConfigureFlags.SkipQuotaCheck) == MessageConfigureFlags.None && (flags & MessageConfigureFlags.IsAssociated) == MessageConfigureFlags.None && this.PerformShutoffQuotaCheck(context, false))
				{
					QuotaType quotaType = folder.IsDumpsterMarkedFolder(context) ? QuotaType.DumpsterShutoff : QuotaType.StorageShutoff;
					QuotaInfo quotaInfo;
					long num;
					errorCode = Quota.CheckForOverQuota(context, mapiLogon.StoreMailbox, quotaType, false, out quotaInfo, out num);
					if (errorCode != ErrorCode.NoError)
					{
						return errorCode.Propagate((LID)59420U);
					}
					if (base.Logon.MapiMailbox.IsPublicFolderMailbox)
					{
						errorCode = Quota.CheckForOverQuota(context, folder, QuotaType.StorageOverQuotaLimit, false, out quotaInfo, out num);
						if (errorCode != ErrorCode.NoError)
						{
							return errorCode.Propagate((LID)34844U);
						}
					}
				}
				this.isReportMessage = ((flags & MessageConfigureFlags.IsReportMessage) == MessageConfigureFlags.IsReportMessage);
				if (sourceMessage != null)
				{
					if (sourceMessage.StoreMessage.IsDirty)
					{
						base.IsValid = false;
						return ErrorCode.CreateInvalidParameter((LID)39509U);
					}
					Folder folder2 = Folder.OpenFolder(context, base.Logon.StoreMailbox, sourceMessage.GetFid());
					if (folder2 == null)
					{
						base.IsValid = false;
						return ErrorCode.CreateNotFound((LID)47701U);
					}
					this.storeMessage = TopMessage.CopyMessage(context, folder2, sourceMessage.Mid, folder);
					if (this.storeMessage == null)
					{
						base.IsValid = false;
						return ErrorCode.CreateNotFound((LID)64085U);
					}
					if (PropertyBagHelpers.TestPropertyFlags(context, this.storeMessage, PropTag.Message.MessageFlagsActual, 4, 4))
					{
						this.storeMessage.AdjustUncomputedMessageFlags(context, MessageFlags.Unsent, MessageFlags.Submit);
					}
				}
				else
				{
					this.storeMessage = TopMessage.CreateMessage(context, folder);
					this.storeMessage.SetProperty(context, PropTag.Message.InternetMessageId, LocalHost.GenerateInternetMessageId(base.Logon.LoggedOnUserAddressInfo.PrimarySmtpAddress, base.Logon.MailboxInfo.ExternalDirectoryOrganizationId));
					this.assignedInternetMessageId = true;
					((TopMessage)this.storeMessage).SetIsHidden(context, MessageConfigureFlags.None != (flags & MessageConfigureFlags.IsAssociated));
					if (!base.Logon.IsMoveDestination)
					{
						this.storeMessage.SetIsRead(context, true);
					}
					this.storeMessage.SetMessageClass(context, "IPM.Note");
				}
				if (!base.Logon.IsMoveDestination)
				{
					this.storeMessage.SetReadReceiptSent(context, true);
				}
				this.IsContentAggregation = (MessageConfigureFlags.None != (flags & MessageConfigureFlags.IsContentAggregation));
				if (context.PerfInstance != null)
				{
					context.PerfInstance.MapiMessagesCreatedRate.Increment();
				}
			}
			else
			{
				this.mid = mid;
				this.storeMessage = TopMessage.OpenMessage(context, base.Logon.StoreMailbox, fid, mid);
				if (this.storeMessage == null)
				{
					base.IsValid = false;
					return ErrorCode.CreateNotFound((LID)59416U);
				}
				this.fid = ((TopMessage)this.storeMessage).GetFolderId(context);
				if (context.PerfInstance != null)
				{
					context.PerfInstance.MapiMessagesOpenedRate.Increment();
				}
				this.InjectFailureIfNeeded(context, false);
			}
			Folder parentFolder = ((TopMessage)this.storeMessage).ParentFolder;
			this.accessCheckState = new AccessCheckState(context, parentFolder);
			base.ParentObject = base.Logon;
			if ((flags & MessageConfigureFlags.RequestReadOnly) != MessageConfigureFlags.None)
			{
				DiagnosticContext.TraceLocation((LID)51048U);
				this.IsReadOnly = true;
			}
			else if (PropertyBagHelpers.TestPropertyFlags(context, this.StoreMessage, PropTag.Message.MessageFlagsActual, 4, 4) && !base.Logon.SpoolerRights)
			{
				if ((flags & MessageConfigureFlags.RequestWrite) != MessageConfigureFlags.None)
				{
					return ErrorCode.CreateNoAccess((LID)41899U);
				}
				DiagnosticContext.TraceLocation((LID)58283U);
				this.IsReadOnly = true;
			}
			if ((flags & MessageConfigureFlags.CreateNewMessage) != MessageConfigureFlags.None)
			{
				MapiFolder.CheckFolderRights(context, fid.ToExchangeShortId(), parentFolder.IsSearchFolder(context), parentFolder.IsInternalAccess(context), this.accessCheckState, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody, true, AccessCheckOperation.MessageCreate, (LID)65383U);
				this.CheckRights(context, MapiMessage.MessageRightsFromMessageConfigureFlags(flags), false, AccessCheckOperation.MessageCreate, (LID)48999U);
			}
			else
			{
				FolderSecurity.ExchangeSecurityDescriptorFolderRights exchangeSecurityDescriptorFolderRights = MapiMessage.MessageRightsFromMessageConfigureFlags(flags);
				this.CheckRights(context, exchangeSecurityDescriptorFolderRights, false, AccessCheckOperation.MessageOpen, (LID)40807U);
				if ((exchangeSecurityDescriptorFolderRights & MapiMessage.ReadOnlyRights) != MapiMessage.ReadOnlyRights)
				{
					this.CheckRights(context, MapiMessage.ReadOnlyRights, false, AccessCheckOperation.MessageOpen, (LID)35424U);
				}
			}
			return errorCode;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00015174 File Offset: 0x00013374
		public IEnumerable<int> GetAttachmentNumbers()
		{
			base.ThrowIfNotValid(null);
			return this.storeMessage.GetAttachmentNumbers();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00015188 File Offset: 0x00013388
		protected override ErrorCode CheckPropertyOperationAllowed(MapiContext context, MapiPropBagBase.PropOperation operation, StorePropTag propTag, object value)
		{
			return MapiMessage.CheckPropertyOperationAllowed(context, base.Logon, this.IsEmbedded, operation, propTag, value);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000151A0 File Offset: 0x000133A0
		public override void SetProps(MapiContext context, Properties properties, ref List<MapiPropertyProblem> allProblems)
		{
			base.ThrowIfNotValid(null);
			if (base.Logon.IsMoveUser)
			{
				int i = 0;
				while (i < properties.Count)
				{
					if (properties[i].Tag == PropTag.Message.LTID)
					{
						if (!this.GetFid().IsValid && !this.IsEmbedded)
						{
							throw new StoreException((LID)53680U, ErrorCodeValue.NotSupported, "Setting long term id on this object is not supported.");
						}
						if (!this.IsEmbedded)
						{
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.storeMessage.IsNew, "LTID is supposed to be set only on a new message");
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!this.setPropsCalled, "LTID is supposed to be set only on a new unmodified message");
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.AttachmentCount == 0, "Message should be brand-new (1).");
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.people == null, "Message should be brand-new (2).");
							ExchangeId messageId = ExchangeId.CreateFrom22ByteArray(context, base.Logon.StoreMailbox.ReplidGuidMap, properties[i].Value as byte[]);
							using (MapiMessage mapiMessage = new MapiMessage())
							{
								MessageConfigureFlags flags = MessageConfigureFlags.RequestWrite;
								ErrorCode errorCode = mapiMessage.ConfigureMessage(context, base.Logon, this.GetFid(), messageId, flags, base.Logon.Session.CodePage);
								if (errorCode == ErrorCode.NoError)
								{
									mapiMessage.Scrub(context);
									this.DisposeStoreMessage(true);
									this.storeMessage = mapiMessage.ReleaseStoreMessage();
								}
								else
								{
									if (errorCode != ErrorCodeValue.NotFound)
									{
										throw new StoreException((LID)56192U, errorCode, "Unexpected error.");
									}
									this.SetMessageId(context, messageId);
								}
							}
						}
						Properties properties2 = new Properties(properties);
						properties2.Remove(PropTag.Message.LTID);
						properties = properties2;
						break;
					}
					else
					{
						i++;
					}
				}
			}
			if (properties.Count != 0)
			{
				this.setPropsCalled = true;
				base.SetProps(context, properties, ref allProblems);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00015394 File Offset: 0x00013594
		protected override List<Property> InternalGetAllProperties(MapiContext context, GetPropListFlags flags, bool loadValues, Predicate<StorePropTag> propertyFilter)
		{
			List<Property> list = base.InternalGetAllProperties(context, flags, loadValues, propertyFilter);
			if (!this.IsEmbedded)
			{
				list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.ParentDisplay) : new Property(PropTag.Message.ParentDisplay, null));
			}
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.AccessLevel) : new Property(PropTag.Message.AccessLevel, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.Access) : new Property(PropTag.Message.Access, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.DisplayTo) : new Property(PropTag.Message.DisplayTo, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.DisplayCc) : new Property(PropTag.Message.DisplayCc, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.DisplayBcc) : new Property(PropTag.Message.DisplayBcc, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.SentRepresentingName) : new Property(PropTag.Message.SentRepresentingName, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.SentRepresentingAddressType) : new Property(PropTag.Message.SentRepresentingAddressType, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.SentRepresentingEmailAddress) : new Property(PropTag.Message.SentRepresentingEmailAddress, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.SentRepresentingEntryId) : new Property(PropTag.Message.SentRepresentingEntryId, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.SenderName) : new Property(PropTag.Message.SenderName, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.SenderAddressType) : new Property(PropTag.Message.SenderAddressType, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.SenderEmailAddress) : new Property(PropTag.Message.SenderEmailAddress, null));
			list.Add(loadValues ? this.InternalGetOneProp(context, PropTag.Message.SenderEntryId) : new Property(PropTag.Message.SenderEntryId, null));
			return list;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00015594 File Offset: 0x00013794
		protected override ErrorCode InternalSetOneProp(MapiContext context, StorePropTag propTag, object objValue)
		{
			ushort propId = propTag.PropId;
			if (propId == 4149)
			{
				this.assignedInternetMessageId = false;
			}
			base.ThrowIfNotValid(null);
			return MapiMessage.InternalSetOneProp(context, base.Logon, this.storeMessage, this.IsScrubbed, propTag, objValue);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000155DC File Offset: 0x000137DC
		protected override ErrorCode InternalDeleteOneProp(MapiContext context, StorePropTag propTag)
		{
			ushort propId = propTag.PropId;
			if (propId == 4149)
			{
				this.assignedInternetMessageId = false;
			}
			return base.InternalDeleteOneProp(context, propTag);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00015608 File Offset: 0x00013808
		public void GetInformationForSetSender(MapiContext context, out byte[] sentRepresentingEntryId, out bool foreignSentRepresentingInfo, out string legacyDNOfAccountToBeCheckedForSendPermissions)
		{
			Trace submitMessageTracer = ExTraceGlobals.SubmitMessageTracer;
			if (this.IsEmbedded)
			{
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					submitMessageTracer.TraceDebug(0L, "Cannot submit embedded messages");
				}
				throw new ExExceptionNoSupport((LID)33488U, "Cannot submit embedded messages");
			}
			string sentRepresentingAddrType = base.GetOnePropValue(context, PropTag.Message.SentRepresentingAddressType) as string;
			string sentRepresentingEmailAddr = base.GetOnePropValue(context, PropTag.Message.SentRepresentingEmailAddress) as string;
			sentRepresentingEntryId = (base.GetOnePropValue(context, PropTag.Message.SentRepresentingEntryId) as byte[]);
			MapiMessage.GetInformationForSetSender(context, sentRepresentingAddrType, sentRepresentingEmailAddr, base.Logon.MailboxOwnerAddressInfo.LegacyExchangeDN, ref sentRepresentingEntryId, out foreignSentRepresentingInfo, out legacyDNOfAccountToBeCheckedForSendPermissions);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000156A4 File Offset: 0x000138A4
		public static void GetInformationForSetSender(MapiContext context, string sentRepresentingAddrType, string sentRepresentingEmailAddr, string ownerLegDn, ref byte[] sentRepresentingEntryId, out bool foreignSentRepresentingInfo, out string legacyDNOfAccountToBeCheckedForSendPermissions)
		{
			Trace submitMessageTracer = ExTraceGlobals.SubmitMessageTracer;
			bool flag = false;
			if (string.IsNullOrEmpty(sentRepresentingAddrType))
			{
				sentRepresentingAddrType = null;
			}
			if (string.IsNullOrEmpty(sentRepresentingEmailAddr))
			{
				sentRepresentingEmailAddr = null;
			}
			if (sentRepresentingEntryId != null && sentRepresentingEntryId.Length == 0)
			{
				sentRepresentingEntryId = null;
			}
			if (sentRepresentingEntryId != null || (sentRepresentingAddrType != null && sentRepresentingEmailAddr != null))
			{
				DiagnosticContext.TraceLocation((LID)37692U);
				flag = true;
			}
			if (!flag)
			{
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					submitMessageTracer.TraceDebug<string>(0L, "No SentRepresenting data at all. Using {0}", ownerLegDn);
				}
				sentRepresentingAddrType = "EX";
				sentRepresentingEmailAddr = ownerLegDn;
			}
			if (sentRepresentingEntryId == null)
			{
				DiagnosticContext.TraceLocation((LID)41788U);
				foreignSentRepresentingInfo = (sentRepresentingAddrType != "EX");
				if (foreignSentRepresentingInfo)
				{
					DiagnosticContext.TraceLocation((LID)58172U);
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug(0L, "we have a foreign address type");
					}
					legacyDNOfAccountToBeCheckedForSendPermissions = string.Empty;
				}
				else
				{
					if (string.IsNullOrEmpty(sentRepresentingEmailAddr))
					{
						throw new ExExceptionInvalidRecipients((LID)45932U, "Invalid SentRepresentingEntryID, representing email address is empty!");
					}
					legacyDNOfAccountToBeCheckedForSendPermissions = sentRepresentingEmailAddr;
					sentRepresentingEntryId = AddressBookEID.MakeAddressBookEntryID(legacyDNOfAccountToBeCheckedForSendPermissions, false);
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug<string>(0L, "retrieved the EntryID for {0}, to set it as SentRepresenting", legacyDNOfAccountToBeCheckedForSendPermissions);
					}
				}
			}
			else
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				if (AddressBookEID.IsAddressBookEntryId(context, sentRepresentingEntryId))
				{
					legacyDNOfAccountToBeCheckedForSendPermissions = AddressBookEID.SzAddrFromABEID(sentRepresentingEntryId);
					if (string.IsNullOrEmpty(legacyDNOfAccountToBeCheckedForSendPermissions))
					{
						throw new ExExceptionInvalidRecipients((LID)64296U, "Invalid SentRepresentingEntryID, legacyDN is empty!");
					}
					foreignSentRepresentingInfo = false;
				}
				else
				{
					MapiAPIFlags mapiAPIFlags;
					if (!AddressBookEID.IsOneOffEntryId(context, sentRepresentingEntryId, out mapiAPIFlags, ref text, ref text2, ref text3))
					{
						throw new ExExceptionInvalidRecipients((LID)48844U, "Invalid SentRepresentingEntryID, it's not a one-off or AddressBook entryID!");
					}
					DiagnosticContext.TraceLocation((LID)37948U);
					if (string.IsNullOrEmpty(text))
					{
						throw new ExExceptionInvalidRecipients((LID)49356U, "Invalid SentRepresentingEntryID, addresstype is empty!");
					}
					foreignSentRepresentingInfo = (text != "EX");
					if (foreignSentRepresentingInfo)
					{
						DiagnosticContext.TraceDwordAndString((LID)33852U, 0U, text ?? "<null>");
						legacyDNOfAccountToBeCheckedForSendPermissions = string.Empty;
					}
					else
					{
						legacyDNOfAccountToBeCheckedForSendPermissions = text2;
						if (string.IsNullOrEmpty(legacyDNOfAccountToBeCheckedForSendPermissions))
						{
							throw new ExExceptionInvalidRecipients((LID)34716U, "Invalid SentRepresentingEntryID, emailaddress is empty!");
						}
						sentRepresentingEntryId = AddressBookEID.MakeAddressBookEntryID(legacyDNOfAccountToBeCheckedForSendPermissions, false);
					}
				}
			}
			if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				submitMessageTracer.TraceDebug<string>(0L, "LegacyDN of the account to be checked for SendAs permissions is '{0}'", legacyDNOfAccountToBeCheckedForSendPermissions);
			}
			if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				submitMessageTracer.TraceDebug<string, bool, string>(0L, "GetInformationForSetSender: SentRepresentingEntryId={0}, foreignSentRepresentingInfo={1}, legacyDNOfAccountToBeCheckedForSendPermissions={2}", (sentRepresentingEntryId == null) ? "N/A" : BitConverter.ToString(sentRepresentingEntryId), foreignSentRepresentingInfo, (legacyDNOfAccountToBeCheckedForSendPermissions == null) ? "N/A" : legacyDNOfAccountToBeCheckedForSendPermissions);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00015928 File Offset: 0x00013B28
		public ErrorCode SubmitMessage(MapiContext context, RopFlags ropFlags, object sentRepresentingEntryId, AddressInfo addressInfoForAuthorization, SubmitMessageRightsCheckFlags submitMessageRightsCheckFlags)
		{
			base.ThrowIfNotValid(null);
			Trace submitMessageTracer = ExTraceGlobals.SubmitMessageTracer;
			if (this.IsEmbedded)
			{
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					submitMessageTracer.TraceDebug(0L, "Cannot submit embedded messages");
				}
				throw new ExExceptionNoSupport((LID)38983U, "Cannot submit embedded messages");
			}
			TopMessage topMessage = (TopMessage)this.storeMessage;
			if (this.GetAssociated(context))
			{
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					submitMessageTracer.TraceDebug(0L, "can't submit FAI messages");
				}
				throw new ExExceptionAccessDenied((LID)55367U, "Cannot submit FAI messages");
			}
			base.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, AccessCheckOperation.MessageSubmit, (LID)57191U);
			if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AddressInfo addressInfo = base.Logon.LoggedOnUserAddressInfo;
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					submitMessageTracer.TraceDebug(0L, "SubmitMessage:[LoggedOnUserAddressInfo] EntryId={0}, AddressType={1}, DisplayName={2}, EmailAddress={3}", new object[]
					{
						(addressInfo.UserEntryId() == null) ? "N/A" : BitConverter.ToString(addressInfo.UserEntryId()),
						(addressInfo.PrimaryEmailAddressType == null) ? "N/A" : addressInfo.PrimaryEmailAddressType,
						(addressInfo.DisplayName == null) ? "N/A" : addressInfo.DisplayName,
						(addressInfo.PrimaryEmailAddress == null) ? "N/A" : addressInfo.PrimaryEmailAddress
					});
				}
				addressInfo = base.Logon.MailboxOwnerAddressInfo;
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					submitMessageTracer.TraceDebug(0L, "SubmitMessage:[MailboxOwnerAddressInfo] EntryId={0}, AddressType={1}, DisplayName={2}, EmailAddress={3}", new object[]
					{
						(addressInfo.UserEntryId() == null) ? "N/A" : BitConverter.ToString(addressInfo.UserEntryId()),
						(addressInfo.PrimaryEmailAddressType == null) ? "N/A" : addressInfo.PrimaryEmailAddressType,
						(addressInfo.DisplayName == null) ? "N/A" : addressInfo.DisplayName,
						(addressInfo.PrimaryEmailAddress == null) ? "N/A" : addressInfo.PrimaryEmailAddress
					});
				}
			}
			ExchangeId @null = ExchangeId.Null;
			bool flag = (ropFlags & RopFlags.Private) != RopFlags.ReadOnly;
			bool flag2 = (ropFlags & RopFlags.NeedsSpooler) != RopFlags.ReadOnly;
			bool flag3 = (ropFlags & RopFlags.GenByRule) != RopFlags.ReadOnly;
			bool flag4 = (ropFlags & RopFlags.DelegateSend) != RopFlags.ReadOnly;
			bool flag5 = (ropFlags & RopFlags.Mapi0) != RopFlags.ReadOnly;
			bool ignoreSendAsRightsRequested = (ropFlags & RopFlags.IgnoreSendAsRight) != RopFlags.ReadOnly;
			bool flag6 = false;
			bool flag7 = false;
			ErrorCode errorCode = ErrorCode.NoError;
			if (!flag5 && !base.Logon.SystemRights && base.Logon.Session.MaxRecipients < this.GetRecipients().GetCount())
			{
				string text = string.Format("user recipient limit is {0} and number on message is {1}", base.Logon.Session.MaxRecipients, this.GetRecipients().GetCount());
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					submitMessageTracer.TraceDebug<string>(0L, "Can't submit message: too many recipients {0}", text);
				}
				throw new ExExceptionTooManyRecipients((LID)43079U, text);
			}
			if (flag5)
			{
				this.StoreMessage.SetProperty(context, PropTag.Message.DeleteAfterSubmit, true);
				this.StoreMessage.SetProperty(context, PropTag.Message.DeliveryReportRequested, false);
				this.StoreMessage.SetProperty(context, PropTag.Message.NonDeliveryReportRequested, false);
				this.StoreMessage.SetProperty(context, PropTag.Message.ContentIdentifier, "ExSysMessage");
			}
			if (topMessage.GetMessageClass(context) == null)
			{
				this.SetMessageClass(context, "IPM.Note");
			}
			if (string.IsNullOrEmpty(topMessage.GetInternetMessageId(context)) || (!this.assignedInternetMessageId && context.ClientType == ClientType.MoMT))
			{
				this.StoreMessage.SetProperty(context, PropTag.Message.InternetMessageId, LocalHost.GenerateInternetMessageId(base.Logon.LoggedOnUserAddressInfo.PrimarySmtpAddress, base.Logon.MailboxInfo.ExternalDirectoryOrganizationId));
				this.assignedInternetMessageId = true;
			}
			this.SetConversationTopicIfNecessary(context);
			if (!flag5)
			{
				if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.QuotaTracer.TraceDebug(29972L, "Quota enforcement on submit...");
				}
				Quota.Enforce((LID)52492U, context, base.Logon.StoreMailbox, QuotaType.StorageOverQuotaLimit, false);
			}
			DateTime utcNow = base.Logon.MapiMailbox.StoreMailbox.UtcNow;
			this.SetClientSubmitTime(context, new DateTime?(utcNow));
			errorCode = this.InternalSetOneProp(context, PropTag.Message.ContentFilterSCL, -1);
			if (errorCode != ErrorCode.NoError)
			{
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					submitMessageTracer.TraceDebug<ErrorCode>(0L, "Can't set SCL; non-critical failure for Submit; error code was {0}", errorCode);
				}
				DiagnosticContext.TraceLocation((LID)59463U);
				errorCode = ErrorCode.NoError;
			}
			string messageClass = this.GetMessageClass(context);
			bool flag8 = true;
			if (flag5 && !flag3)
			{
				foreach (string b in MapiMessage.noSetLcidSendClasses)
				{
					if (string.Equals(messageClass, b, StringComparison.OrdinalIgnoreCase))
					{
						flag8 = false;
						break;
					}
				}
				if (MapiMessage.IsReadReport(messageClass))
				{
					flag8 = false;
				}
			}
			if (flag8)
			{
				base.InternalSetOnePropShouldNotFail(context, PropTag.Message.MessageLocaleId, base.Logon.Session.LcidString);
			}
			this.AdjustMessageFlags(context, MessageFlags.Read | MessageFlags.Submit, MessageFlags.None);
			if (base.Logon.SpoolerRights || flag2 || flag)
			{
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("Message is added to spooler queue. Reason: ");
					if (base.Logon.SpoolerRights)
					{
						stringBuilder.Append("spooler submission.");
					}
					else if (flag2)
					{
						stringBuilder.Append("spooler processing requested.");
					}
					else
					{
						stringBuilder.Append("pre-processing requested.");
					}
					submitMessageTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				flag7 = true;
				MapiPersonCollection recipients = this.GetRecipients();
				for (int j = 0; j < recipients.GetCount(); j++)
				{
					MapiPerson mapiPerson = recipients[j];
					if (!mapiPerson.IsDeleted)
					{
						mapiPerson.SetResponsibility(false);
					}
				}
				this.SpecialRecipientProcessing(context);
			}
			else if (base.Logon.MapiTransportProvider)
			{
				flag6 = true;
			}
			else
			{
				bool flag9 = false;
				bool flag10 = false;
				if (base.Logon.NoSpooler)
				{
					MapiPersonCollection recipients2 = this.GetRecipients();
					for (int k = 0; k < recipients2.GetCount(); k++)
					{
						MapiPerson mapiPerson2 = recipients2[k];
						if (!mapiPerson2.IsDeleted)
						{
							mapiPerson2.SetResponsibility(true);
						}
					}
				}
				this.SetResponsibility(context, out flag9, out flag10);
				this.SpecialRecipientProcessing(context);
				if (flag9)
				{
					flag6 = true;
				}
				if (flag10)
				{
					submitMessageTracer.TraceDebug(0L, "Message is added to spooler queue. Reason: spooler recipients found.");
					flag7 = true;
				}
			}
			if (!flag3)
			{
				errorCode = this.InternalDeleteOneProp(context, PropTag.Message.RuleTriggerHistory);
				if (errorCode != ErrorCode.NoError)
				{
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug<ErrorCode>(0L, "Cannot delete ptagRuleTriggerHistory from message; non-critical error for Submit; error code was {0}", errorCode);
					}
					DiagnosticContext.TraceLocation((LID)51271U);
					errorCode = ErrorCode.NoError;
				}
				errorCode = this.InternalDeleteOneProp(context, PropTag.Message.MoveToStoreEid);
				if (errorCode != ErrorCode.NoError)
				{
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug<ErrorCode>(0L, "Cannot delete ptagMoveToStoreEid from message; non-critical error for Submit; error code was {0}", errorCode);
					}
					DiagnosticContext.TraceLocation((LID)45127U);
					errorCode = ErrorCode.NoError;
				}
				errorCode = this.InternalDeleteOneProp(context, PropTag.Message.MoveToFolderEid);
				if (errorCode != ErrorCode.NoError)
				{
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug<ErrorCode>(0L, "Cannot delete ptagMoveToFolderEid from message; non-critical error for Submit; error code was {0}", errorCode);
					}
					DiagnosticContext.TraceLocation((LID)61511U);
					errorCode = ErrorCode.NoError;
				}
			}
			base.InternalDeleteOnePropShouldNotFail(context, PropTag.Message.LocallyDelivered);
			object onePropValue = base.GetOnePropValue(context, PropTag.Message.HasDAMs);
			if (onePropValue != null && (bool)onePropValue)
			{
				errorCode = this.InternalSetOneProp(context, PropTag.Message.HasDAMs, false);
				if (errorCode != ErrorCode.NoError)
				{
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug<ErrorCode>(0L, "Cannot reset hasDAMs on message; non-critical error for SUBMIT; error code was {0}", errorCode);
					}
					DiagnosticContext.TraceLocation((LID)53319U);
					errorCode = ErrorCode.NoError;
				}
			}
			if (!base.Logon.MapiTransportProvider)
			{
				object onePropValue2 = base.GetOnePropValue(context, PropTag.Message.SentMailSvrEID);
				if (onePropValue2 != null)
				{
					bool flag11 = false;
					ExchangeId exchangeId = ExchangeId.Null;
					ExchangeId exchangeId2;
					ExchangeId exchangeId3;
					if (Helper.ParseSvrEid(base.Logon, (byte[])onePropValue2, false, out exchangeId2, out exchangeId3))
					{
						exchangeId = exchangeId2;
					}
					if (exchangeId.IsValid)
					{
						using (MapiFolder mapiFolder = MapiFolder.OpenFolder(context, base.Logon, exchangeId))
						{
							if (mapiFolder == null)
							{
								throw new ExExceptionNotFound((LID)44128U, "Cannot open sentItems folder for this message");
							}
							if (mapiFolder.IsSearchFolder())
							{
								if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									submitMessageTracer.TraceDebug(0L, "Cannot have search folder as sentItems folder");
								}
								throw new ExExceptionSearchFolder((LID)41031U, "Cannot have search folder as sentItems folder");
							}
							mapiFolder.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody, AccessCheckOperation.MessageSubmit, (LID)44903U);
						}
						flag11 = true;
					}
					if (!flag11)
					{
						submitMessageTracer.TraceDebug(0L, "SentMail entryid points to a foreign folder. Message is added to spooler queue.");
						flag7 = true;
					}
				}
			}
			SubmissionResponsibility submissionResponsibility = SubmissionResponsibility.None;
			if (!flag6)
			{
				submissionResponsibility |= SubmissionResponsibility.MdbDone;
				submissionResponsibility |= SubmissionResponsibility.LocalDeliveryDone;
			}
			if (!flag7)
			{
				submissionResponsibility |= SubmissionResponsibility.SpoolerDone;
			}
			if (!flag)
			{
				submissionResponsibility |= SubmissionResponsibility.PreProcessingDone;
			}
			base.InternalSetOnePropShouldNotFail(context, PropTag.Message.SubmitResponsibility, (int)submissionResponsibility);
			errorCode = this.ConvertSendOptionProps(context, utcNow);
			if (errorCode != ErrorCode.NoError)
			{
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					submitMessageTracer.TraceDebug(0L, "ConvertSendOptionProps has failed");
				}
				DiagnosticContext.TraceLocation((LID)47783U);
				goto IL_B90;
			}
			object onePropValue3 = base.GetOnePropValue(context, PropTag.Message.RequiresRefResolve);
			if (onePropValue3 != null)
			{
				if (!base.Logon.MapiTransportProvider)
				{
					if ((bool)onePropValue3)
					{
						this.SetNeedAttachResolve(context, false);
						if (flag6)
						{
							throw new ExExceptionRequiresRefResolve((LID)39160U, "Attachment needs to be resolved");
						}
					}
				}
				else
				{
					base.InternalDeleteOnePropShouldNotFail(context, PropTag.Message.RequiresRefResolve);
				}
			}
			if (!flag4)
			{
				errorCode = this.SetSender(context, flag5, ignoreSendAsRightsRequested, sentRepresentingEntryId, addressInfoForAuthorization, flag2, submitMessageRightsCheckFlags);
				if (errorCode != ErrorCode.NoError)
				{
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug(0L, "Cannot set sender");
					}
					DiagnosticContext.TraceLocation((LID)49223U);
					goto IL_B90;
				}
			}
			this.StoreMessage.SetProperty(context, PropTag.Message.SubmittedByAdmin, !base.Logon.IsUserLogon(context));
			MapiMessage mapiMessage = null;
			try
			{
				MapiSaveMessageChangesFlags mapiSaveMessageChangesFlags = MapiSaveMessageChangesFlags.Submit;
				if (flag5)
				{
					mapiSaveMessageChangesFlags |= MapiSaveMessageChangesFlags.SkipQuotaCheck;
				}
				this.SaveChangesInternal(context, mapiSaveMessageChangesFlags, out @null);
				byte[] array2 = (byte[])base.GetOnePropValue(context, PropTag.Message.TargetEntryId);
				if (!flag3 && !flag5 && array2 != null)
				{
					mapiMessage = this.CreateTargetMessageForOutlook(context, array2);
				}
				if (mapiMessage != null)
				{
					ExchangeId null2 = ExchangeId.Null;
					mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.InternetMessageId, this.GetInternetMessageId(context));
					errorCode = mapiMessage.SaveChanges(context, MapiSaveMessageChangesFlags.SkipMailboxQuotaCheck | MapiSaveMessageChangesFlags.SkipFolderQuotaCheck | MapiSaveMessageChangesFlags.ForceCreatedEventForCopy | MapiSaveMessageChangesFlags.SkipSizeCheck, out null2);
					if (errorCode != ErrorCode.NoError)
					{
						if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							submitMessageTracer.TraceDebug(39096L, "Unable to save the outlook target message, error code " + errorCode.ToString());
						}
						errorCode = ErrorCode.NoError;
					}
				}
			}
			finally
			{
				if (mapiMessage != null)
				{
					mapiMessage.Dispose();
					mapiMessage = null;
				}
			}
			object onePropValue4 = base.GetOnePropValue(context, PropTag.Message.DeferredSendTime);
			if (onePropValue4 != null)
			{
				DateTime dateTime = (DateTime)onePropValue4;
				MapiTimedEvents.RaiseDeferredSendEvent(context, dateTime, base.Logon.MapiMailbox.StoreMailbox.MailboxNumber, this.fid, this.Mid);
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					submitMessageTracer.TraceDebug(41144L, "Deferred submission queued for MailboxNumber:[{0}], FID:[{1:X}], MID:[{2:X}], Time:[{3}]", new object[]
					{
						base.Logon.MapiMailbox.StoreMailbox.MailboxNumber,
						this.fid,
						this.Mid,
						dateTime
					});
				}
			}
			else if (flag6 && !flag)
			{
				MailSubmittedNotificationEvent nev = NotificationEvents.CreateMailSubmittedEvent(context, topMessage);
				context.RiseNotificationEvent(nev);
			}
			if (context.PerfInstance != null)
			{
				context.PerfInstance.MessagesSubmittedRate.Increment();
			}
			return errorCode;
			IL_B90:
			if (errorCode != ErrorCode.NoError)
			{
				if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					submitMessageTracer.TraceDebug<ErrorCode>(0L, "SubmitMessage has failed with error code {0}", errorCode);
				}
				DiagnosticContext.TraceLocation((LID)48711U);
				return errorCode;
			}
			return errorCode;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00016518 File Offset: 0x00014718
		public ErrorCode AbortSubmit(MapiContext context)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!this.IsEmbedded, "Cannot abort submit of embedded messages");
			if (PropertyBagHelpers.TestPropertyFlags(context, this.StoreMessage, PropTag.Message.MessageFlagsActual, 4, 0))
			{
				return ErrorCode.CreateNotInQueue((LID)34840U);
			}
			bool isReadOnly = this.IsReadOnly;
			this.IsReadOnly = false;
			ErrorCode result;
			try
			{
				this.AdjustUncomputedMessageFlags(context, MessageFlags.Unsent, MessageFlags.Submit);
				if (PropertyBagHelpers.TestPropertyFlags(context, this.StoreMessage, PropTag.Message.MessageFlagsActual, 128, 128))
				{
					this.UndoSpecialRecipientProcessing(context);
				}
				ExchangeId exchangeId;
				result = this.SaveChanges(context, MapiSaveMessageChangesFlags.SkipMailboxQuotaCheck | MapiSaveMessageChangesFlags.SkipFolderQuotaCheck | MapiSaveMessageChangesFlags.SkipSizeCheck, out exchangeId);
			}
			finally
			{
				this.IsReadOnly = isReadOnly;
			}
			return result;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000165C4 File Offset: 0x000147C4
		public void SetMessageId(MapiContext context, ExchangeId messageId)
		{
			base.ThrowIfNotValid(null);
			if (!this.IsEmbedded)
			{
				this.storeMessage.SetProperty(context, PropTag.Message.MidBin, messageId.To26ByteArray());
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000165EE File Offset: 0x000147EE
		public void SetSourceKey(MapiContext context, byte[] sourceKey)
		{
			base.ThrowIfNotValid(null);
			if (!this.IsEmbedded)
			{
				this.storeMessage.SetProperty(context, PropTag.Message.InternalSourceKey, sourceKey);
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00016614 File Offset: 0x00014814
		internal ErrorCode ConfigureEmbeddedMessage(MapiContext context, MapiAttachment parentAttachment, MessageConfigureFlags flags, CodePage codePage)
		{
			parentAttachment.CheckRights(context, MapiMessage.MessageRightsFromMessageConfigureFlags(flags), false, AccessCheckOperation.MessageOpenEmbedded, (LID)61287U);
			this.codePage = codePage;
			base.Logon = parentAttachment.Logon;
			this.storeMessage = parentAttachment.StoreAttachment.OpenEmbeddedMessage(context);
			if (this.storeMessage == null)
			{
				return ErrorCode.CreateNotFound((LID)51224U);
			}
			this.attachmentTableExists = true;
			base.IsValid = true;
			base.ParentObject = parentAttachment;
			return ErrorCode.NoError;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00016694 File Offset: 0x00014894
		internal ErrorCode ConfigureNewEmbeddedMessage(MapiContext context, MapiAttachment parentAttachment, CodePage codePage)
		{
			parentAttachment.CheckRights(context, MapiMessage.MessageRightsFromMessageConfigureFlags(MessageConfigureFlags.RequestWrite), false, AccessCheckOperation.MessageCreateEmbedded, (LID)36711U);
			this.codePage = codePage;
			base.Logon = parentAttachment.Logon;
			this.storeMessage = parentAttachment.StoreAttachment.CreateEmbeddedMessage(context);
			this.storeMessage.SetIsRead(context, true);
			this.storeMessage.SetMessageClass(context, "IPM.Note");
			this.attachmentTableExists = this.storeMessage.CheckTableExists(context);
			base.IsValid = true;
			base.ParentObject = parentAttachment;
			return ErrorCode.NoError;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00016728 File Offset: 0x00014928
		internal StorePropTag GetNativeBodyPropertyTag(MapiContext context)
		{
			short? bodyType = this.StoreMessage.GetBodyType(context);
			if (bodyType == MapiMessage.rtfBodyFormat)
			{
				return PropTag.Message.RtfCompressed;
			}
			if (bodyType == MapiMessage.htmlBodyFormat)
			{
				return PropTag.Message.BodyHtml;
			}
			if (bodyType == MapiMessage.textBodyFormat)
			{
				return PropTag.Message.BodyUnicode;
			}
			return StorePropTag.Invalid;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000167E4 File Offset: 0x000149E4
		protected override bool TryGetPropertyImp(MapiContext context, ushort propId, out StorePropTag actualPropTag, out object propValue)
		{
			bool result;
			if (propId != 3604)
			{
				if (propId != 4084)
				{
					if (propId != 4087)
					{
						result = base.TryGetPropertyImp(context, propId, out actualPropTag, out propValue);
					}
					else
					{
						actualPropTag = PropTag.Message.AccessLevel;
						propValue = this.CalculateSecurityRelatedProperty(context, PropTag.Message.AccessLevel);
						result = (propValue != null);
					}
				}
				else
				{
					actualPropTag = PropTag.Message.Access;
					propValue = this.CalculateSecurityRelatedProperty(context, PropTag.Message.Access);
					result = (propValue != null);
				}
			}
			else
			{
				actualPropTag = PropTag.Message.SubmitFlags;
				propValue = this.GetSubmissionStatus(context);
				result = (propValue != null);
			}
			return result;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001688E File Offset: 0x00014A8E
		protected override object GetPropertyValueImp(MapiContext context, StorePropTag propTag)
		{
			if (MapiMessage.IsMessageSecurityRelatedProperty(propTag.PropTag))
			{
				return this.CalculateSecurityRelatedProperty(context, propTag);
			}
			if (propTag.PropTag == 236191747U)
			{
				return this.GetSubmissionStatus(context);
			}
			return base.GetPropertyValueImp(context, propTag);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000168CA File Offset: 0x00014ACA
		internal void SetMessageClass(MapiContext context, string value)
		{
			base.ThrowIfNotValid(null);
			this.storeMessage.SetProperty(context, PropTag.Message.MessageClass, value);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000168E6 File Offset: 0x00014AE6
		public ExchangeId GetFid()
		{
			base.ThrowIfNotValid(null);
			return this.fid;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000168F8 File Offset: 0x00014AF8
		public PropGroupChangeInfo GetPropGroupChangeInfo(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			TopMessage topMessage = this.storeMessage as TopMessage;
			if (topMessage == null)
			{
				return default(PropGroupChangeInfo);
			}
			return topMessage.GetPropGroupChangeInfo(context);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001692C File Offset: 0x00014B2C
		public string GetParentDisplay(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			TopMessage topMessage = this.storeMessage as TopMessage;
			if (topMessage == null)
			{
				return null;
			}
			return topMessage.ParentFolder.GetName(context);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001695D File Offset: 0x00014B5D
		public bool GetAssociated(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return (bool)this.storeMessage.GetPropertyValue(context, PropTag.Message.Associated);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001697C File Offset: 0x00014B7C
		internal void SetSubject(MapiContext context, string value)
		{
			base.ThrowIfNotValid(null);
			this.StoreMessage.SetProperty(context, PropTag.Message.Subject, value);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00016998 File Offset: 0x00014B98
		internal int GetMessageStatus(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return (int)this.StoreMessage.GetPropertyValue(context, PropTag.Message.MsgStatus);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000169B8 File Offset: 0x00014BB8
		internal int? GetSubmissionStatus(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			object propertyValue = this.StoreMessage.GetPropertyValue(context, PropTag.Message.SubmitFlags);
			if (propertyValue == null)
			{
				return null;
			}
			int num = (int)propertyValue;
			HashSet<ExchangeId> spoolerLockList = Microsoft.Exchange.Protocols.MAPI.Globals.GetSpoolerLockList(base.Logon.StoreMailbox);
			bool flag = spoolerLockList != null && spoolerLockList.Contains(this.Mid);
			if (flag)
			{
				num |= 1;
			}
			return new int?(num);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00016A24 File Offset: 0x00014C24
		internal void SetMessageStatus(MapiContext context, int messageStatus)
		{
			base.ThrowIfNotValid(null);
			this.StoreMessage.SetProperty(context, PropTag.Message.MsgStatus, messageStatus);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00016A45 File Offset: 0x00014C45
		internal bool AdjustMessageFlags(MapiContext context, MessageFlags flagsToSet, MessageFlags flagsToClear)
		{
			base.ThrowIfNotValid(null);
			return this.StoreMessage.AdjustMessageFlags(context, flagsToSet, flagsToClear);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00016A5C File Offset: 0x00014C5C
		internal bool AdjustUncomputedMessageFlags(MapiContext context, MessageFlags flagsToSet, MessageFlags flagsToClear)
		{
			base.ThrowIfNotValid(null);
			return this.StoreMessage.AdjustUncomputedMessageFlags(context, flagsToSet, flagsToClear);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00016A73 File Offset: 0x00014C73
		internal DateTime? GetDeliveryTime(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return (DateTime?)this.StoreMessage.GetPropertyValue(context, PropTag.Message.MessageDeliveryTime);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00016A92 File Offset: 0x00014C92
		internal void SetDeliveryTime(MapiContext context, DateTime? value)
		{
			base.ThrowIfNotValid(null);
			this.StoreMessage.SetProperty(context, PropTag.Message.MessageDeliveryTime, value);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00016AB3 File Offset: 0x00014CB3
		internal DateTime? GetClientSubmitTime(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return (DateTime?)this.StoreMessage.GetPropertyValue(context, PropTag.Message.ClientSubmitTime);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00016AD2 File Offset: 0x00014CD2
		internal void SetClientSubmitTime(MapiContext context, DateTime? value)
		{
			base.ThrowIfNotValid(null);
			this.StoreMessage.SetProperty(context, PropTag.Message.ClientSubmitTime, value);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00016AF3 File Offset: 0x00014CF3
		internal DateTime GetLastModificationTime(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return (DateTime)this.storeMessage.GetPropertyValue(context, PropTag.Message.LastModificationTime);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00016B12 File Offset: 0x00014D12
		internal byte[] GetChangeKey(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return (byte[])this.storeMessage.GetPropertyValue(context, PropTag.Message.ChangeKey);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00016B31 File Offset: 0x00014D31
		internal ExchangeId GetChangeNumber(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return ((TopMessage)this.storeMessage).GetLcnCurrent(context);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00016B4B File Offset: 0x00014D4B
		internal string GetInternetMessageId(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return (string)this.StoreMessage.GetPropertyValue(context, PropTag.Message.InternetMessageId);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00016B6A File Offset: 0x00014D6A
		private static void ClearNeedReceipts(MapiContext context, Message storeMessage, bool clearRNPending, bool clearNRNPending)
		{
			if (clearRNPending && storeMessage.GetNeedsReadNotification(context))
			{
				storeMessage.SetNeedsReadNotification(context, false);
			}
			if (clearNRNPending && storeMessage.GetNeedsNotReadNotification(context))
			{
				storeMessage.SetNeedsNotReadNotification(context, false);
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00016B98 File Offset: 0x00014D98
		private static ErrorCode SendReadReportIfNeeded(MapiContext context, MapiLogon mapiLogon, Message storeMessage)
		{
			bool flag = false;
			if (storeMessage.GetNeedsReadNotification(context))
			{
				Property oneProp = mapiLogon.GetOneProp(context, PropTag.Mailbox.InternetMdns);
				bool flag2 = oneProp.IsError || !(bool)oneProp.Value;
				bool flag3 = context.ClientType == ClientType.Migration || context.ClientType == ClientType.SimpleMigration || context.ClientType == ClientType.TransportSync || context.ClientType == ClientType.PublicFolderSystem;
				bool isPerUserReadUnreadTrackingEnabled = (storeMessage as TopMessage).ParentFolder.IsPerUserReadUnreadTrackingEnabled;
				if (flag2 && !flag3 && !mapiLogon.MailboxInfo.IsDiscoveryMailbox && !isPerUserReadUnreadTrackingEnabled)
				{
					ErrorCode first = MapiMessage.GenerateReadReport(context, mapiLogon, storeMessage, false, false);
					if (first != ErrorCode.NoError)
					{
						return first.Propagate((LID)36376U);
					}
				}
				flag = true;
			}
			else if (storeMessage.GetNeedsNotReadNotification(context))
			{
				flag = true;
			}
			if (flag)
			{
				storeMessage.SetReadReceiptSent(context, true);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00016C84 File Offset: 0x00014E84
		private static ErrorCode InternalGenerateReadReport(MapiContext context, MapiLogon mapiLogon, Message storeMessage, bool notRead, bool messageExpired)
		{
			ErrorCode result;
			using (context.GrantMailboxFullRights())
			{
				Properties properties = new Properties(5);
				byte[] array = null;
				string text = null;
				string text2 = null;
				string text3 = null;
				foreach (StorePropTag[] array3 in MapiMessage.originalSenderOptions)
				{
					array = (storeMessage.GetPropertyValue(context, array3[0]) as byte[]);
					if (array != null && array.Length != 0)
					{
						Eidt eidt;
						MapiAPIFlags mapiAPIFlags;
						if (AddressBookEID.IsAddressBookEntryId(context, array, out eidt, out text2))
						{
							text3 = "EX";
							text = (storeMessage.GetPropertyValue(context, array3[1]) as string);
							if (string.IsNullOrEmpty(text))
							{
								text = text2;
								break;
							}
							break;
						}
						else if (AddressBookEID.IsOneOffEntryId(context, array, out mapiAPIFlags, ref text3, ref text2, ref text))
						{
							if (!string.IsNullOrEmpty(text))
							{
								break;
							}
							text = (storeMessage.GetPropertyValue(context, array3[1]) as string);
							if (string.IsNullOrEmpty(text))
							{
								text = text2;
								break;
							}
							break;
						}
						else
						{
							text = (storeMessage.GetPropertyValue(context, array3[1]) as string);
							text2 = (storeMessage.GetPropertyValue(context, array3[2]) as string);
							text3 = (storeMessage.GetPropertyValue(context, array3[3]) as string);
							if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
							{
								if (string.IsNullOrEmpty(text))
								{
									text = text2;
								}
								array = AddressBookEID.MakeOneOffEntryID(text3, text2, text, true, 0);
								break;
							}
						}
					}
				}
				if (array != null && array.Length != 0 && !string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
				{
					properties.Add(PropTag.Recipient.RecipientType, 1);
					properties.Add(PropTag.Recipient.EntryId, array);
					properties.Add(PropTag.Recipient.DisplayName, text);
					properties.Add(PropTag.Recipient.EmailAddress, text2);
					properties.Add(PropTag.Recipient.AddressType, text3);
					MapiMessage mapiMessage;
					ErrorCode first = MapiMailbox.PrepareReportMessage(context, mapiLogon, new Properties[]
					{
						properties
					}, out mapiMessage);
					using (mapiMessage)
					{
						if (first != ErrorCode.NoError)
						{
							result = first.Propagate((LID)29720U);
						}
						else
						{
							foreach (StorePropTag propTag in MapiMessage.copyPropList)
							{
								mapiMessage.StoreMessage.SetProperty(context, propTag, storeMessage.GetPropertyValue(context, propTag));
							}
							foreach (KeyValuePair<StorePropTag, StorePropTag> keyValuePair in MapiMessage.copyRemapPropList)
							{
								StorePropTag key = keyValuePair.Key;
								StorePropTag value = keyValuePair.Value;
								mapiMessage.StoreMessage.SetProperty(context, value, storeMessage.GetPropertyValue(context, key));
							}
							string messageClass = (string)storeMessage.GetPropertyValue(context, PropTag.Message.MessageClass);
							string value2 = MapiMessage.GenerateReadReportMessageClass(messageClass, notRead);
							mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, value2);
							string str = (string)storeMessage.GetPropertyValue(context, PropTag.Message.SubjectPrefix);
							mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.SubjectPrefix, (notRead ? "Not read: " : "Read: ") + str);
							DateTime utcNow = mapiMessage.Logon.MapiMailbox.StoreMailbox.UtcNow;
							if (notRead)
							{
								mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.NonReceiptReason, 0);
								mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.DiscardReason, messageExpired ? 0 : 1);
							}
							else
							{
								mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ReceiptTime, utcNow);
							}
							mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ReportTime, utcNow);
							mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.SenderEntryId, mapiLogon.MailboxOwnerAddressInfo.UserEntryId());
							mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.SenderEmailAddress, mapiLogon.MailboxOwnerAddressInfo.PrimaryEmailAddress);
							mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.SenderAddressType, mapiLogon.MailboxOwnerAddressInfo.PrimaryEmailAddressType);
							mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.SenderName, mapiLogon.MailboxOwnerAddressInfo.DisplayName);
							result = mapiMessage.SubmitMessage(context, RopFlags.Mapi0, mapiLogon.MailboxOwnerAddressInfo.UserEntryId(), mapiLogon.MailboxOwnerAddressInfo, SubmitMessageRightsCheckFlags.SendAsRights).Propagate((LID)29728U);
						}
					}
				}
				else
				{
					DiagnosticContext.TraceLocation((LID)29752U);
					result = ErrorCode.NoError;
				}
			}
			return result;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001715C File Offset: 0x0001535C
		internal static ErrorCode SetReadFlag(MapiContext context, MapiLogon mapiLogon, Message storeMessage, SetReadFlagFlags flags, out bool didChange, out ExchangeId readCn)
		{
			bool flag = (byte)(flags & (SetReadFlagFlags.SuppressReceipt | SetReadFlagFlags.ClearReadNotificationPending | SetReadFlagFlags.ClearNonReadNotificationPending)) == 0;
			bool flag2 = (byte)(flags & (SetReadFlagFlags.ClearReadFlag | SetReadFlagFlags.ClearReadNotificationPending | SetReadFlagFlags.ClearNonReadNotificationPending)) == 0;
			if ((byte)(flags & SetReadFlagFlags.GenerateReceiptOnly) != 0 && !flag)
			{
				throw new StoreException((LID)35064U, ErrorCodeValue.InvalidParameter);
			}
			bool flag3 = !storeMessage.IsNew && !storeMessage.IsEmbedded;
			if ((byte)(flags & SetReadFlagFlags.GenerateReceiptOnly) != 0)
			{
				didChange = false;
				readCn = ExchangeId.Null;
				if (flag3)
				{
					return MapiMessage.SendReadReportIfNeeded(context, mapiLogon, storeMessage).Propagate((LID)29732U);
				}
			}
			else if ((byte)(flags & (SetReadFlagFlags.ClearReadNotificationPending | SetReadFlagFlags.ClearNonReadNotificationPending)) == 0)
			{
				if (flag3)
				{
					if (flag && flag2)
					{
						ErrorCode first = MapiMessage.SendReadReportIfNeeded(context, mapiLogon, storeMessage);
						if (first != ErrorCode.NoError)
						{
							didChange = false;
							readCn = ExchangeId.Null;
							return first.Propagate((LID)29736U);
						}
					}
					Folder.SetReadMessage(context, (TopMessage)storeMessage, 0 == (byte)(flags & SetReadFlagFlags.ClearReadFlag), out didChange, out readCn);
					if ((byte)(flags & SetReadFlagFlags.SuppressReceipt) != 0)
					{
						ClientType clientType = context.ClientType;
						if (clientType == ClientType.User || clientType == ClientType.RpcHttp || clientType == ClientType.MoMT)
						{
							MapiMessage.ClearNeedReceipts(context, storeMessage, true, true);
						}
					}
				}
				else
				{
					didChange = storeMessage.SetIsRead(context, (byte)(flags & SetReadFlagFlags.ClearReadFlag) == 0);
					readCn = ExchangeId.Null;
				}
			}
			else
			{
				didChange = false;
				readCn = ExchangeId.Null;
				MapiMessage.ClearNeedReceipts(context, storeMessage, (byte)(flags & SetReadFlagFlags.ClearReadNotificationPending) != 0, (byte)(flags & SetReadFlagFlags.ClearNonReadNotificationPending) != 0);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000172C5 File Offset: 0x000154C5
		internal static FolderSecurity.ExchangeSecurityDescriptorFolderRights MessageRightsFromMessageConfigureFlags(MessageConfigureFlags flags)
		{
			if ((flags & MessageConfigureFlags.RequestWrite) == MessageConfigureFlags.RequestWrite)
			{
				return MapiMessage.WriteRights;
			}
			if ((flags & MessageConfigureFlags.RequestReadOnly) == MessageConfigureFlags.RequestReadOnly)
			{
				return MapiMessage.ReadOnlyRights;
			}
			return MapiMessage.ReadOnlyRights;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000172E6 File Offset: 0x000154E6
		private byte[] GetConversationIndex(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return (byte[])this.StoreMessage.GetPropertyValue(context, PropTag.Message.ConversationIndex);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00017305 File Offset: 0x00015505
		private void SetConversationIndex(MapiContext context, byte[] value)
		{
			base.ThrowIfNotValid(null);
			this.StoreMessage.SetProperty(context, PropTag.Message.ConversationIndex, value);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00017321 File Offset: 0x00015521
		private string GetConversationTopic(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return (string)this.StoreMessage.GetPropertyValue(context, PropTag.Message.ConversationTopic);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00017340 File Offset: 0x00015540
		private void SetConversationTopic(MapiContext context, string value)
		{
			base.ThrowIfNotValid(null);
			this.StoreMessage.SetProperty(context, PropTag.Message.ConversationTopic, value);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001735C File Offset: 0x0001555C
		private string GetNormalizedSubject(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return (string)this.StoreMessage.GetPropertyValue(context, PropTag.Message.NormalizedSubject);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001737B File Offset: 0x0001557B
		public MapiPersonCollection GetRecipients()
		{
			base.ThrowIfNotValid(null);
			if (this.people == null)
			{
				this.people = new MapiPersonCollection(this);
			}
			return this.people;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0001739E File Offset: 0x0001559E
		private void SetConversationTopicIfNecessary(MapiContext context)
		{
			if (this.GetConversationTopic(context) == null)
			{
				this.SetConversationTopic(context, this.GetNormalizedSubject(context));
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000173B8 File Offset: 0x000155B8
		internal void SaveChangesInternal(MapiContext context, MapiSaveMessageChangesFlags saveFlags, out ExchangeId newMid)
		{
			base.ThrowIfNotValid(null);
			this.InjectFailureIfNeeded(context, true);
			if (base.Logon.IsMoveDestination)
			{
				saveFlags |= MapiSaveMessageChangesFlags.NonFatalDuplicateKey;
			}
			if (this.StoreMessage.DescendantCount > ConfigurationSchema.MapiMessageMaxSupportedDescendantCount.Value && !base.Logon.AllowLargeItem() && this.StoreMessage.SubobjectsChangeCookie > 0)
			{
				throw new StoreException((LID)46944U, ErrorCodeValue.MaxAttachmentExceeded);
			}
			if (this.streamSizeInvalid)
			{
				throw new ExExceptionMaxSubmissionExceeded((LID)36764U, "Exceeded the size limitation");
			}
			if (this.StoreMessage.IsNew && !this.IsEmbedded)
			{
				ErrorCode errorCode = MapiMailboxShape.PerformMailboxShapeQuotaCheck(context, base.Logon, ((TopMessage)this.StoreMessage).ParentFolder, MapiMailboxShape.Operation.CreateMessage, this.isReportMessage);
				if (errorCode != ErrorCode.NoError)
				{
					throw new StoreException((LID)58176U, errorCode);
				}
			}
			if (this.StoreMessage.IsDirty && context.PerfInstance != null)
			{
				context.PerfInstance.MapiMessagesModifiedRate.Increment();
			}
			base.CommitDirtyStreams(context);
			string messageClass = this.GetMessageClass(context);
			this.SetMessageClass(context, messageClass);
			this.DoAutoResponseSuppression(context);
			if (this.people != null)
			{
				this.people.SaveChanges(context);
			}
			if ((saveFlags & MapiSaveMessageChangesFlags.SkipSizeCheck) == MapiSaveMessageChangesFlags.None)
			{
				this.EnforceMaxSaveSize(context, MapiSaveMessageChangesFlags.None != (saveFlags & MapiSaveMessageChangesFlags.Submit));
			}
			this.SetCreatorLastModifierAddressInfoProperties(context, saveFlags);
			if (!this.IsEmbedded)
			{
				if (context.ClientType != ClientType.Migration)
				{
					DateTime utcNow = base.Logon.MapiMailbox.StoreMailbox.UtcNow;
					if (this.GetDeliveryTime(context) == null && this.storeMessage.IsNew)
					{
						this.SetDeliveryTime(context, new DateTime?(utcNow));
					}
					if (this.GetClientSubmitTime(context) == null && this.storeMessage.IsNew)
					{
						this.SetClientSubmitTime(context, new DateTime?(utcNow));
					}
				}
				if (this.GetConversationIndex(context) == null)
				{
					this.SetConversationIndex(context, ConversationIdHelpers.GenerateNewConversationIndex());
				}
				this.AdjustIRMMessageFlag(context);
				bool flag = false;
				bool flag2 = true;
				if (this.icsImport != null)
				{
					if (this.icsImport.ConflictingChange)
					{
						if (this.icsImport.CreateConflictMessage || !this.icsImport.ConflictWinnerNew)
						{
							flag = true;
						}
					}
					else
					{
						bool isNew = this.storeMessage.IsNew;
					}
					this.storeMessage.SetProperty(context, PropTag.Message.PCL, this.icsImport.Pcl.DumpBinaryLXCN());
					this.storeMessage.SetProperty(context, PropTag.Message.InternalChangeKey, this.icsImport.ChangeKey);
					this.storeMessage.SetProperty(context, PropTag.Message.LastModificationTime, this.icsImport.LastModificationTime);
				}
				if (!this.PerformShutoffQuotaCheck(context, true))
				{
					saveFlags |= MapiSaveMessageChangesFlags.SkipQuotaCheck;
				}
				if (!base.Logon.MapiMailbox.IsPublicFolderMailbox)
				{
					saveFlags |= MapiSaveMessageChangesFlags.SkipFolderQuotaCheck;
				}
				TopMessage topMessage = (TopMessage)this.storeMessage;
				if (topMessage.GetPropertyValue(context, PropTag.Message.MessageLocaleId) == null)
				{
					base.InternalSetOnePropShouldNotFail(context, PropTag.Message.MessageLocaleId, base.Logon.Session.LcidString);
				}
				if (string.IsNullOrEmpty(topMessage.GetInternetMessageId(context)))
				{
					base.InternalSetOnePropShouldNotFail(context, PropTag.Message.InternetMessageId, LocalHost.GenerateInternetMessageId(base.Logon.LoggedOnUserAddressInfo.PrimarySmtpAddress, base.Logon.MailboxInfo.ExternalDirectoryOrganizationId));
					this.assignedInternetMessageId = true;
				}
				if (flag || !topMessage.SaveChanges(context, MapiMessage.ToLogicalLayerSaveChangesFlags(saveFlags)))
				{
					if (this.icsImport != null)
					{
						if (this.icsImport.FailOnConflict)
						{
							throw new StoreException((LID)55544U, ErrorCodeValue.SyncConflict);
						}
						using (MapiMessage mapiMessage = new MapiMessage())
						{
							MessageConfigureFlags flags = MessageConfigureFlags.RequestWrite;
							ErrorCode errorCode2 = mapiMessage.ConfigureMessage(context, base.Logon, this.GetFid(), this.Mid, flags, base.Logon.Session.CodePage);
							if (errorCode2 != ErrorCode.NoError)
							{
								throw new StoreException((LID)59640U, errorCode2);
							}
							bool flag3 = MapiMessage.PickConflictWinner(this.icsImport.LastModificationTime, this.icsImport.ChangeKey, mapiMessage.GetLastModificationTime(context), mapiMessage.GetChangeKey(context));
							if (!this.icsImport.CreateConflictMessage)
							{
								PCL remotePCL = default(PCL);
								remotePCL.LoadBinaryLXCN((byte[])mapiMessage.GetOnePropValue(context, PropTag.Message.PCL));
								this.icsImport.Pcl.Merge(remotePCL);
								if (flag3)
								{
									mapiMessage.Scrub(context);
									CopyToFlags flags2 = CopyToFlags.CopyRecipients | CopyToFlags.CopyAttachments;
									List<MapiPropertyProblem> list = null;
									this.CopyTo(context, mapiMessage, null, flags2, ref list);
									mapiMessage.storeMessage.SetProperty(context, PropTag.Message.PCL, this.icsImport.Pcl.DumpBinaryLXCN());
									mapiMessage.storeMessage.SetProperty(context, PropTag.Message.InternalChangeKey, this.icsImport.ChangeKey);
									mapiMessage.storeMessage.SetProperty(context, PropTag.Message.LastModificationTime, this.icsImport.LastModificationTime);
									mapiMessage.SaveChangesInternal(context, saveFlags, out newMid);
								}
								else
								{
									mapiMessage.storeMessage.SetProperty(context, PropTag.Message.PCL, this.icsImport.Pcl.DumpBinaryLXCN());
									mapiMessage.storeMessage.SaveChanges(context);
									flag2 = false;
								}
							}
							if (this.people != null)
							{
								this.people.Dispose();
								this.people = null;
							}
							this.DisposeStoreMessage(true);
							this.storeMessage = mapiMessage.ReleaseStoreMessage();
							topMessage = (TopMessage)this.storeMessage;
							goto IL_67F;
						}
					}
					if ((saveFlags & MapiSaveMessageChangesFlags.ForceSave) != MapiSaveMessageChangesFlags.None)
					{
						using (MapiMessage mapiMessage2 = new MapiMessage())
						{
							MessageConfigureFlags flags3 = MessageConfigureFlags.RequestWrite;
							ErrorCode errorCode3 = mapiMessage2.ConfigureMessage(context, base.Logon, this.GetFid(), this.Mid, flags3, base.Logon.Session.CodePage);
							if (errorCode3 != ErrorCode.NoError)
							{
								throw new StoreException((LID)43256U, errorCode3);
							}
							mapiMessage2.Scrub(context);
							List<MapiPropertyProblem> list2 = null;
							CopyToFlags flags4 = CopyToFlags.CopyRecipients | CopyToFlags.CopyAttachments;
							this.CopyTo(context, mapiMessage2, null, flags4, ref list2);
							mapiMessage2.SaveChangesInternal(context, saveFlags, out newMid);
							if (this.people != null)
							{
								this.people.Dispose();
								this.people = null;
							}
							this.DisposeStoreMessage(true);
							this.storeMessage = mapiMessage2.ReleaseStoreMessage();
							topMessage = (TopMessage)this.storeMessage;
							goto IL_67F;
						}
					}
					throw new ExExceptionObjectChanged((LID)37112U, "message was changed under us");
				}
				this.mid = topMessage.GetId(context);
				IL_67F:
				if (this.icsImport != null)
				{
					if (flag2)
					{
						ExchangeId lcnCurrent = topMessage.GetLcnCurrent(context);
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(topMessage.ParentFolder.GetCnsetSeen(context).Contains(lcnCurrent), "the CN must be in a folder's CnsetSeen");
						this.icsImport.CnsetToUpdate.Insert(lcnCurrent);
					}
					this.icsImport = null;
				}
			}
			else
			{
				this.StoreMessage.SaveChanges(context);
			}
			this.scrubbed = false;
			newMid = this.Mid;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00017AE8 File Offset: 0x00015CE8
		private void PreSaveChangesWork(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			this.CheckRights(context, MapiMessage.WriteRights, false, AccessCheckOperation.MessageSaveChanges, (LID)53095U);
			base.CommitDirtyStreams(context);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00017B14 File Offset: 0x00015D14
		public IChunked PrepareToSaveChanges(MapiContext context)
		{
			if (!ConfigurationSchema.AttachmentMessageSaveChunking.Value)
			{
				return null;
			}
			this.PreSaveChangesWork(context);
			if (!this.IsEmbedded || this.storeMessage.GetLargeDirtyStreamsSize() < ConfigurationSchema.AttachmentMessageSaveChunkingMinSize.Value)
			{
				return null;
			}
			return this.storeMessage.PrepareToSaveChanges(context);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00017B64 File Offset: 0x00015D64
		internal ErrorCode SaveChanges(MapiContext context, MapiSaveMessageChangesFlags saveFlags, out ExchangeId newMid)
		{
			this.PreSaveChangesWork(context);
			newMid = ExchangeId.Null;
			ErrorCode result;
			try
			{
				if (this.storeMessage.IsNew && !this.IsEmbedded && base.Logon.SpoolerRights)
				{
					this.storeMessage.SetReadReceiptSent(context, false);
				}
				this.SaveChangesInternal(context, saveFlags, out newMid);
				result = ErrorCode.NoError;
			}
			catch (ObjectNotFoundException ex)
			{
				context.OnExceptionCatch(ex);
				if (ExTraceGlobals.GeneralTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(200);
					stringBuilder.Append("Someone deleted this message (MID=");
					stringBuilder.Append(this.Mid.ToString());
					stringBuilder.Append(") below us! Ignoring SaveChanges on it. Message=");
					stringBuilder.Append(ex.Message);
					stringBuilder.Append(" StackTrace=");
					stringBuilder.Append(ex.StackTrace);
					ExTraceGlobals.GeneralTracer.TraceError(0L, stringBuilder.ToString());
				}
				result = ErrorCode.CreateObjectDeleted((LID)45080U);
			}
			return result;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00017C78 File Offset: 0x00015E78
		public void Scrub(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			this.storeMessage.Scrub(context);
			if (this.people != null)
			{
				this.people.Dispose();
				this.people = null;
			}
			TopMessage topMessage = (TopMessage)this.storeMessage;
			topMessage.SetProperty(context, PropTag.Message.InternetMessageId, LocalHost.GenerateInternetMessageId(base.Logon.LoggedOnUserAddressInfo.PrimarySmtpAddress, base.Logon.MailboxInfo.ExternalDirectoryOrganizationId));
			this.assignedInternetMessageId = true;
			topMessage.SetMessageClass(context, "IPM.Note");
			this.scrubbed = true;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00017D0C File Offset: 0x00015F0C
		internal void SetReadFlag(MapiContext context, SetReadFlagFlags flags, out bool didChange)
		{
			base.ThrowIfNotValid(null);
			this.CheckPropertyRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, AccessCheckOperation.MessageSetReadFlag, (LID)29700U);
			ExchangeId exchangeId;
			ErrorCode first;
			if (this.IsEmbedded || this.StoreMessage.IsNew)
			{
				first = MapiMessage.SetReadFlag(context, base.Logon, this.storeMessage, flags, out didChange, out exchangeId).Propagate((LID)29748U);
				if (first == ErrorCode.NoError && this.IsEmbedded)
				{
					this.storeMessage.SaveChanges(context);
				}
				return;
			}
			Folder folder = Folder.OpenFolder(context, base.Logon.StoreMailbox, this.GetFid());
			if (folder == null)
			{
				throw new StoreException((LID)52139U, ErrorCodeValue.NotFound, "Parent folder not found");
			}
			first = MapiMessage.SetReadFlag(context, base.Logon, folder, null, this.Mid, flags, out didChange, out exchangeId).Propagate((LID)29744U);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00017DF8 File Offset: 0x00015FF8
		internal static ErrorCode SetReadFlag(MapiContext context, MapiLogon mapiLogon, Folder storeFolder, AccessCheckState accessCheckState, ExchangeId mid, SetReadFlagFlags flags, out bool didChange, out ExchangeId readCn)
		{
			ErrorCode result;
			using (TopMessage topMessage = TopMessage.OpenMessage(context, storeFolder.Mailbox, storeFolder.GetId(context), mid))
			{
				if (topMessage == null)
				{
					didChange = false;
					readCn = ExchangeId.Null;
					result = ErrorCode.CreateNotFound((LID)29712U);
				}
				else
				{
					if (accessCheckState != null)
					{
						MapiFolder.CheckMessageRights(context, storeFolder.GetId(context).ToExchangeShortId(), accessCheckState, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, true, topMessage, false, AccessCheckOperation.FolderSetReadFlag, (LID)42855U);
					}
					ErrorCode first = MapiMessage.SetReadFlag(context, mapiLogon, topMessage, flags, out didChange, out readCn);
					if (first == ErrorCode.NoError)
					{
						topMessage.SaveChanges(context, SaveMessageChangesFlags.SkipQuotaCheck);
					}
					result = first.Propagate((LID)29716U);
				}
			}
			return result;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00017EC0 File Offset: 0x000160C0
		internal void SetIcsImportState(PCL pcl, byte[] changeKey, DateTime lastModificationTime, IdSet cnsetToUpdate, bool conflictingChange, bool conflictWinnerNew, bool createConflictMessage, bool failOnConflict)
		{
			this.icsImport = new MapiMessage.IcsImportState(pcl, changeKey, lastModificationTime, cnsetToUpdate, conflictingChange, conflictWinnerNew, createConflictMessage, failOnConflict);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00017EE8 File Offset: 0x000160E8
		internal ErrorCode ImportMove(MapiContext context, MapiFolder destinationFolder, ExchangeId destinationMid, byte[] destinationSourceKey, PCL pcl, byte[] changeKey)
		{
			base.ThrowIfNotValid(null);
			TopMessage topMessage = (TopMessage)this.storeMessage;
			MapiFolder.CheckFolderRights(context, this.GetFid().ToExchangeShortId(), topMessage.ParentFolder.IsSearchFolder(context), topMessage.ParentFolder.IsInternalAccess(context), this.accessCheckState, FolderSecurity.ExchangeSecurityDescriptorFolderRights.Delete, true, AccessCheckOperation.FolderImportMessageMoveSource, (LID)63847U);
			MapiFolder.CheckMessageRights(context, this.GetFid().ToExchangeShortId(), this.accessCheckState, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, true, topMessage, topMessage.IsNew, AccessCheckOperation.FolderImportMessageMoveSource, (LID)39271U);
			destinationFolder.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody, AccessCheckOperation.FolderImportMessageMoveDestination, (LID)55655U);
			ErrorCode first = MapiMailboxShape.PerformMailboxShapeQuotaCheck(context, destinationFolder.Logon, destinationFolder.StoreFolder, MapiMailboxShape.Operation.MoveMessage, false);
			if (first != ErrorCode.NoError)
			{
				return first.Propagate((LID)60480U);
			}
			topMessage.Move(context, destinationFolder.StoreFolder);
			if (changeKey != null)
			{
				this.storeMessage.SetProperty(context, PropTag.Message.InternalChangeKey, changeKey);
			}
			if (destinationMid.IsValid)
			{
				this.storeMessage.SetProperty(context, PropTag.Message.MidBin, destinationMid.To26ByteArray());
			}
			if (destinationSourceKey != null)
			{
				this.storeMessage.SetProperty(context, PropTag.Message.InternalSourceKey, destinationSourceKey);
			}
			topMessage.SetPCL(context, pcl.DumpBinaryLXCN(), true);
			SaveMessageChangesFlags saveMessageChangesFlags = SaveMessageChangesFlags.None;
			if (!base.Logon.MapiMailbox.IsPublicFolderMailbox)
			{
				saveMessageChangesFlags |= SaveMessageChangesFlags.SkipFolderQuotaCheck;
			}
			bool assertCondition = topMessage.SaveChanges(context, saveMessageChangesFlags);
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(assertCondition, "We must not see a save conflict on this SaveChanges - we just have opened the message!");
			return ErrorCode.NoError;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00018068 File Offset: 0x00016268
		internal override void CheckRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, bool allRights, AccessCheckOperation operation, LID lid)
		{
			if (PropertyBagHelpers.TestPropertyFlags(context, this.StoreMessage, PropTag.Message.MessageFlagsActual, 4, 4) && !base.Logon.SpoolerRights && (requestedRights & (FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty | FolderSecurity.ExchangeSecurityDescriptorFolderRights.Delete)) != FolderSecurity.ExchangeSecurityDescriptorFolderRights.None)
			{
				DiagnosticContext.TraceDword((LID)54151U, (uint)operation);
			}
			MapiPropBagBase mapiPropBagBase = this;
			while (!(mapiPropBagBase.ParentObject is MapiLogon))
			{
				mapiPropBagBase = mapiPropBagBase.ParentObject;
			}
			MapiMessage mapiMessage = mapiPropBagBase as MapiMessage;
			if (this.IsReadOnly && (requestedRights & MapiMessage.WriteRights) != FolderSecurity.ExchangeSecurityDescriptorFolderRights.None)
			{
				DiagnosticContext.TraceDword((LID)60264U, (uint)operation);
				throw new ExExceptionAccessDenied(lid, "Write access on ReadOnly object.");
			}
			if (this.storeMessage.IsNew)
			{
				return;
			}
			if (((TopMessage)mapiMessage.storeMessage).ParentFolder.IsInternalAccess(context) && !context.HasInternalAccessRights)
			{
				DiagnosticContext.TraceLocation((LID)38732U);
				throw new ExExceptionAccessDenied(lid, "Insufficient rights for InternalAccess folder.");
			}
			bool flag = mapiMessage.accessCheckState.CheckMessageRights(context, requestedRights, allRights, new AccessCheckState.CreatorSecurityIdentifierAccessor((TopMessage)mapiMessage.StoreMessage));
			if (ExTraceGlobals.AccessCheckTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug(0L, "MapiMessage({0}) access check: Operation={1}, Requested rights={2}, Allowed={3}", new object[]
				{
					mapiMessage.Mid,
					operation,
					requestedRights,
					flag
				});
			}
			if (!flag)
			{
				SecurityIdentifier creatorSecurityIdentifier = ((TopMessage)mapiMessage.storeMessage).GetCreatorSecurityIdentifier(context);
				DiagnosticContext.TraceDword(lid, (uint)operation);
				DiagnosticContext.TraceDword((LID)58344U, (uint)mapiMessage.accessCheckState.GetFolderRights(context));
				DiagnosticContext.TraceDword((LID)62440U, (uint)mapiMessage.accessCheckState.GetMessageRights(context, creatorSecurityIdentifier));
				throw new ExExceptionAccessDenied(lid, "Insufficient rights.");
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00018220 File Offset: 0x00016420
		internal override void CheckPropertyRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, AccessCheckOperation operation, LID lid)
		{
			if (this.IsReadOnly && (requestedRights & MapiMessage.WriteRights) != FolderSecurity.ExchangeSecurityDescriptorFolderRights.None)
			{
				DiagnosticContext.TraceDword((LID)52072U, (uint)operation);
				throw new ExExceptionAccessDenied(lid, "Write access on ReadOnly object.");
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00018250 File Offset: 0x00016450
		internal static bool IsMessageSecurityRelatedProperty(uint propertyTag)
		{
			return propertyTag == 267649027U || propertyTag == 267845635U;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00018264 File Offset: 0x00016464
		internal object CalculateSecurityRelatedProperty(MapiContext context, StorePropTag propertyTag)
		{
			MapiPropBagBase mapiPropBagBase = this;
			while (!(mapiPropBagBase.ParentObject is MapiLogon))
			{
				mapiPropBagBase = mapiPropBagBase.ParentObject;
			}
			MapiMessage mapiMessage = mapiPropBagBase as MapiMessage;
			return MapiMessage.CalculateSecurityRelatedProperty(context, propertyTag, mapiMessage.accessCheckState, ((TopMessage)mapiMessage.StoreMessage).GetCreatorSecurityIdentifier(context), ((TopMessage)mapiMessage.StoreMessage).GetConversationCreatorSecurityIdentifier(context));
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000182C0 File Offset: 0x000164C0
		internal static object CalculateSecurityRelatedProperty(MapiContext context, StorePropTag propertyTag, AccessCheckState accessCheckState, SecurityIdentifier messageCreatorSecurityIdentifier, SecurityIdentifier conversationCreatorSecurityIdentifier)
		{
			if (propertyTag == PropTag.Message.AccessLevel)
			{
				if (accessCheckState.CheckMessageRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, true, new AccessCheckState.CreatorSecurityIdentifierAccessor(messageCreatorSecurityIdentifier, conversationCreatorSecurityIdentifier)))
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (propertyTag == PropTag.Message.Access)
				{
					MapiAccess mapiAccess = MapiAccess.None;
					if (accessCheckState.CheckMessageRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, true, new AccessCheckState.CreatorSecurityIdentifierAccessor(messageCreatorSecurityIdentifier, conversationCreatorSecurityIdentifier)))
					{
						mapiAccess |= MapiAccess.Read;
					}
					if (accessCheckState.CheckMessageRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, true, new AccessCheckState.CreatorSecurityIdentifierAccessor(messageCreatorSecurityIdentifier, conversationCreatorSecurityIdentifier)))
					{
						mapiAccess |= MapiAccess.Modify;
					}
					if (accessCheckState.CheckMessageRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.Delete, true, new AccessCheckState.CreatorSecurityIdentifierAccessor(messageCreatorSecurityIdentifier, conversationCreatorSecurityIdentifier)))
					{
						mapiAccess |= MapiAccess.Delete;
					}
					return (int)mapiAccess;
				}
				return null;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001835D File Offset: 0x0001655D
		internal static bool PickConflictWinner(DateTime lastModificationTime1, byte[] changeKey1, DateTime lastModificationTime2, byte[] changeKey2)
		{
			return lastModificationTime1 > lastModificationTime2 || (!(lastModificationTime1 < lastModificationTime2) && ValueHelper.ArraysCompare<byte>(changeKey1, changeKey2) > 0);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00018380 File Offset: 0x00016580
		public ErrorCode CreateAttachment(MapiContext context, out MapiAttachment attachment)
		{
			attachment = null;
			MapiAttachment mapiAttachment = null;
			if (this.AttachmentCount >= ConfigurationSchema.MapiMessageMaxSupportedAttachmentCount.Value && !base.Logon.AllowLargeItem())
			{
				return ErrorCode.CreateMaxAttachmentExceeded((LID)29992U);
			}
			ErrorCode first;
			try
			{
				mapiAttachment = new MapiAttachment();
				first = mapiAttachment.ConfigureNew(context, this);
				if (first != ErrorCode.NoError)
				{
					return first.Propagate((LID)30000U);
				}
				mapiAttachment.InternalSetOnePropShouldNotFail(context, PropTag.Attachment.RecordKey, Guid.NewGuid().ToByteArray());
				attachment = mapiAttachment;
				mapiAttachment = null;
			}
			finally
			{
				if (mapiAttachment != null)
				{
					mapiAttachment.Dispose();
					mapiAttachment = null;
				}
			}
			return first.Propagate((LID)30008U);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00018440 File Offset: 0x00016640
		public ErrorCode OpenAttachment(MapiContext context, int attachmentNumber, AttachmentConfigureFlags configFlags, out MapiAttachment attachment)
		{
			attachment = null;
			MapiAttachment mapiAttachment = null;
			ErrorCode errorCode;
			try
			{
				mapiAttachment = new MapiAttachment();
				errorCode = mapiAttachment.Configure(context, this, configFlags, attachmentNumber);
				if (errorCode == ErrorCode.NoError)
				{
					attachment = mapiAttachment;
					mapiAttachment = null;
				}
			}
			finally
			{
				if (mapiAttachment != null)
				{
					mapiAttachment.Dispose();
				}
			}
			return errorCode;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00018494 File Offset: 0x00016694
		public ErrorCode DeleteAttachment(MapiContext context, int attachNum)
		{
			ErrorCode errorCode;
			using (MapiAttachment mapiAttachment = new MapiAttachment())
			{
				errorCode = mapiAttachment.Configure(context, this, AttachmentConfigureFlags.RequestWrite, attachNum);
				if (errorCode != ErrorCode.NoError)
				{
					mapiAttachment.Dispose();
					return errorCode;
				}
				errorCode = mapiAttachment.Delete(context);
			}
			return errorCode;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000184F0 File Offset: 0x000166F0
		private void DoAutoResponseSuppression(MapiContext context)
		{
			object propertyValue = this.storeMessage.GetPropertyValue(context, PropTag.Message.AutoResponseSuppress);
			if (propertyValue == null)
			{
				return;
			}
			AutoResponseSuppress autoResponseSuppress = (AutoResponseSuppress)((int)propertyValue);
			List<StorePropTag> list = null;
			if (this.people != null)
			{
				list = this.people.GetRecipientPropListExtra();
			}
			foreach (KeyValuePair<StorePropTag, AutoResponseSuppress> keyValuePair in MapiMessage.autoResponseSuppressionProperties)
			{
				if ((autoResponseSuppress & keyValuePair.Value) != AutoResponseSuppress.None)
				{
					base.InternalDeleteOnePropShouldNotFail(context, keyValuePair.Key);
					if (list != null)
					{
						list.Remove(keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00018584 File Offset: 0x00016784
		private void DeleteAllAttachments(MapiContext context)
		{
			foreach (int attachNum in this.GetAttachmentNumbers())
			{
				ErrorCode errorCode = this.DeleteAttachment(context, attachNum);
				if (errorCode != ErrorCode.NoError)
				{
					throw new StoreException((LID)43832U, errorCode);
				}
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000185F8 File Offset: 0x000167F8
		public IList<StorePropTag> GetRecipientPropList(uint flags)
		{
			base.ThrowIfNotValid(null);
			IList<StorePropTag> recipientPropListStandard = MapiPerson.GetRecipientPropListStandard();
			IList<StorePropTag> recipientPropListExtra = this.GetRecipientPropListExtra();
			List<StorePropTag> list = new List<StorePropTag>(recipientPropListStandard.Count + recipientPropListExtra.Count);
			list.AddRange(recipientPropListStandard);
			list.AddRange(recipientPropListExtra);
			return list;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001863B File Offset: 0x0001683B
		internal int GetRecipientPropListCount(uint flags)
		{
			base.ThrowIfNotValid(null);
			return MapiPerson.GetRecipientPropListStandardCount(this) + this.GetRecipientPropListExtraCount();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00018651 File Offset: 0x00016851
		public IList<StorePropTag> GetRecipientPropListExtra()
		{
			base.ThrowIfNotValid(null);
			return this.GetRecipients().GetRecipientPropListExtra();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00018665 File Offset: 0x00016865
		internal int GetRecipientPropListExtraCount()
		{
			base.ThrowIfNotValid(null);
			return this.GetRecipients().GetRecipientPropListExtra().Count;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001867E File Offset: 0x0001687E
		public void SetRecipientPropListExtra(uint flags, IList<StorePropTag> propTags)
		{
			base.ThrowIfNotValid(null);
			this.GetRecipients().SetRecipientPropListExtra(new List<StorePropTag>(propTags));
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00018698 File Offset: 0x00016898
		public ErrorCode TransportDoneWithMessage(MapiContext context)
		{
			bool flag = false;
			ExchangeId @null = ExchangeId.Null;
			ErrorCode errorCode = ErrorCode.NoError;
			int? num = base.GetOnePropValue(context, PropTag.Message.SubmitResponsibility) as int?;
			if (num != null && (num.Value & 1) == 0)
			{
				this.IsReadOnly = false;
				try
				{
					int num2 = num.Value | 16 | 4096;
					base.InternalSetOnePropShouldNotFail(context, PropTag.Message.SubmitResponsibility, num2);
					this.AdjustUncomputedMessageFlags(context, MessageFlags.Submit, MessageFlags.Unsent);
					ExchangeId exchangeId;
					return this.SaveChanges(context, MapiSaveMessageChangesFlags.SkipMailboxQuotaCheck | MapiSaveMessageChangesFlags.SkipFolderQuotaCheck | MapiSaveMessageChangesFlags.SkipSizeCheck, out exchangeId);
				}
				finally
				{
					this.IsReadOnly = true;
				}
			}
			object onePropValue = base.GetOnePropValue(context, PropTag.Message.DeleteAfterSubmit);
			if (onePropValue != null && onePropValue is bool)
			{
				flag = (bool)onePropValue;
			}
			if (!flag)
			{
				onePropValue = base.GetOnePropValue(context, PropTag.Message.SentMailSvrEID);
				if (onePropValue is byte[])
				{
					ExchangeId exchangeId2;
					if (!Helper.ParseSvrEid(base.Logon, (byte[])onePropValue, false, out @null, out exchangeId2))
					{
						flag = true;
					}
				}
				else
				{
					@null = ExchangeId.Null;
				}
				if (!flag)
				{
					this.IsReadOnly = false;
					try
					{
						this.AdjustMessageFlags(context, MessageFlags.Read, MessageFlags.Submit | MessageFlags.Unsent);
						this.SetReceivedByProps(context, RecipientType.P1);
						ExchangeId exchangeId3;
						errorCode = this.SaveChanges(context, MapiSaveMessageChangesFlags.SkipMailboxQuotaCheck | MapiSaveMessageChangesFlags.SkipFolderQuotaCheck | MapiSaveMessageChangesFlags.SkipSizeCheck, out exchangeId3);
						if (errorCode != ErrorCode.NoError)
						{
							return errorCode;
						}
						if (@null.IsValid)
						{
							using (MapiFolder mapiFolder = MapiFolder.OpenFolder(context, base.Logon, @null))
							{
								if (mapiFolder == null)
								{
									flag = true;
									ExTraceGlobals.GeneralTracer.TraceDebug(0L, "deleting the message in TransportDoneWithMessage because we cannot access SentItems folder");
								}
								else
								{
									using (MapiMessage mapiMessage = new MapiMessage())
									{
										errorCode = mapiMessage.ConfigureMessage(context, base.Logon, @null, ExchangeId.Null, MessageConfigureFlags.CreateNewMessage | MessageConfigureFlags.SkipQuotaCheck, this.CodePage, this);
										if (errorCode != ErrorCode.NoError)
										{
											return errorCode;
										}
										mapiMessage.SetDeliveryTime(context, new DateTime?(base.Logon.MapiMailbox.StoreMailbox.UtcNow));
										mapiMessage.StoreMessage.SetReadReceiptSent(context, true);
										errorCode = mapiMessage.SaveChanges(context, MapiSaveMessageChangesFlags.SkipMailboxQuotaCheck | MapiSaveMessageChangesFlags.SkipFolderQuotaCheck | MapiSaveMessageChangesFlags.ForceCreatedEventForCopy | MapiSaveMessageChangesFlags.SkipSizeCheck, out exchangeId3);
										if (errorCode != ErrorCode.NoError)
										{
											return errorCode;
										}
									}
									flag = true;
								}
							}
						}
					}
					finally
					{
						this.IsReadOnly = true;
					}
				}
			}
			if (flag)
			{
				using (MapiFolder mapiFolder2 = MapiFolder.OpenFolder(context, base.Logon, this.GetFid()))
				{
					errorCode = mapiFolder2.DeleteMessage(context, false, true, this.Mid);
					this.Invalidate();
				}
			}
			return errorCode;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00018984 File Offset: 0x00016B84
		public ErrorCode DuplicateDeliveryCheck(MapiContext context, DateTime submitTime, string internetMessageId)
		{
			ErrorCode result = ErrorCode.NoError;
			if (this.storeMessage.IsEmbedded)
			{
				return result;
			}
			if (DeliveredTo.AlreadyDelivered(context, base.Logon.StoreMailbox, submitTime, base.Logon.MapiMailbox.IsPublicFolderMailbox ? this.fid : ExchangeId.Null, internetMessageId))
			{
				result = ErrorCode.CreateDuplicateDelivery((LID)61464U);
			}
			return result;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000189EC File Offset: 0x00016BEC
		public ErrorCode Deliver(MapiContext context, RecipientType recipientType, out ExchangeId mid)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			mid = ExchangeId.Zero;
			if (this.IsEmbedded)
			{
				throw new ExExceptionNoSupport((LID)51448U, "Cannot deliver embedded messages");
			}
			bool flag = false;
			this.DeliveryHousekeeping(context);
			DateTime utcNow = base.Logon.MapiMailbox.StoreMailbox.UtcNow;
			if (this.GetClientSubmitTime(context) == null)
			{
				this.SetClientSubmitTime(context, new DateTime?(utcNow));
			}
			if (base.GetOnePropValue(context, PropTag.Message.InternetMessageId) != null)
			{
				flag = true;
				errorCode = this.DuplicateDeliveryCheck(context, this.GetClientSubmitTime(context).Value, this.GetInternetMessageId(context));
				if (errorCode != ErrorCode.NoError)
				{
					return errorCode;
				}
			}
			this.SetReceivedByProps(context, recipientType);
			if (!this.IsContentAggregation)
			{
				this.SetDeliveryTime(context, new DateTime?(utcNow));
			}
			this.AdjustUncomputedMessageFlags(context, MessageFlags.Unmodified, MessageFlags.Submit | MessageFlags.Unsent | MessageFlags.Resend);
			MapiSaveMessageChangesFlags mapiSaveMessageChangesFlags = MapiSaveMessageChangesFlags.Delivery;
			if (base.Logon.IsReportMessageDelivery || base.Logon.IsNormalMessageDelivery || base.Logon.IsQuotaMessageDelivery)
			{
				mapiSaveMessageChangesFlags |= MapiSaveMessageChangesFlags.SkipMailboxQuotaCheck;
			}
			errorCode = this.SaveChanges(context, mapiSaveMessageChangesFlags, out mid);
			if (!(errorCode != ErrorCode.NoError))
			{
				if (flag)
				{
					DeliveredTo.AddToDeliveredToTable(context, base.Logon.StoreMailbox, this.GetClientSubmitTime(context).Value, base.Logon.StoreMailbox.IsPublicFolderMailbox ? this.fid : ExchangeId.Null, this.GetInternetMessageId(context));
				}
				NewMailNotificationEvent nev = NotificationEvents.CreateNewMailEvent(context, (TopMessage)this.storeMessage);
				context.RiseNotificationEvent(nev);
				if (context.PerfInstance != null)
				{
					context.PerfInstance.MessageDeliveriesRate.Increment();
				}
			}
			return errorCode;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00018B96 File Offset: 0x00016D96
		internal RecipientCollection StoreRecipientCollection(MapiContext context)
		{
			return this.StoreMessage.GetRecipients(context);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00018BA4 File Offset: 0x00016DA4
		internal Recipient CreateStorePerson(MapiContext context, int rowId)
		{
			Recipient recipient = this.StoreRecipientCollection(context).Add(null, null, RecipientType.To, rowId);
			recipient.SetProperty(context, PropTag.Recipient.Responsibility, true.GetBoxed());
			return recipient;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00018BD8 File Offset: 0x00016DD8
		internal bool IsQuotaMessage(MapiContext context)
		{
			string messageClass = this.GetMessageClass(context);
			for (int i = 0; i < 3; i++)
			{
				if (string.Equals(messageClass, MapiMessage.noSetLcidSendClasses[i], StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00018C0C File Offset: 0x00016E0C
		internal bool IsNDR(MapiContext context)
		{
			return false;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00018C10 File Offset: 0x00016E10
		internal string GetMessageClass(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			string text = (string)this.StoreMessage.GetPropertyValue(context, PropTag.Message.MessageClass);
			if (text == null || text.Length == 0)
			{
				text = "IPM.Note";
			}
			return text;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00018C50 File Offset: 0x00016E50
		internal void SetNeedAttachResolve(MapiContext context, bool needResolve)
		{
			MapiAttachment mapiAttachment = base.ParentObject as MapiAttachment;
			if (mapiAttachment != null)
			{
				((MapiMessage)mapiAttachment.ParentObject).SetNeedAttachResolve(context, needResolve);
				return;
			}
			base.InternalSetOnePropShouldNotFail(context, PropTag.Message.RequiresRefResolve, needResolve);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00018C94 File Offset: 0x00016E94
		internal Message ReleaseStoreMessage()
		{
			Message result = this.storeMessage;
			this.DisposeStoreMessage(false);
			return result;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00018CB0 File Offset: 0x00016EB0
		internal void InjectFailureIfNeeded(MapiContext context, bool forUpload)
		{
			if (!ComponentTrace<ManagedStore_MapiTags>.CheckEnabled(31))
			{
				return;
			}
			if (context == null || (context.ClientType != ClientType.Migration && context.ClientType != ClientType.PublicFolderSystem))
			{
				return;
			}
			if (base.Logon == null)
			{
				return;
			}
			string b = forUpload ? "**JRW**BAD**TARGET**" : "**JRW**BAD**";
			string b2 = "**JRW**LARGE**";
			string normalizedSubject = this.GetNormalizedSubject(base.CurrentOperationContext);
			if (normalizedSubject == null)
			{
				return;
			}
			if (normalizedSubject == b)
			{
				throw new StoreException((LID)48184U, ErrorCodeValue.CallFailed, "Bad item injection");
			}
			if (forUpload && normalizedSubject == b2)
			{
				throw new ExExceptionMaxSubmissionExceeded((LID)41229U, "Bad item injection");
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00018D53 File Offset: 0x00016F53
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiMessage>(this);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00018D5B File Offset: 0x00016F5B
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.Invalidate();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00018D70 File Offset: 0x00016F70
		private void DeliveryHousekeeping(MapiContext context)
		{
			TopMessage topMessage = (TopMessage)this.storeMessage;
			string messageClass = topMessage.GetMessageClass(context);
			if (!string.IsNullOrEmpty(messageClass))
			{
				foreach (string b in MapiMessage.forbiddenDeliveryClasses)
				{
					if (string.Equals(messageClass, b, StringComparison.OrdinalIgnoreCase))
					{
						this.SetMessageClass(context, "IPM.Note");
						break;
					}
				}
			}
			else
			{
				this.SetMessageClass(context, "IPM.Note");
			}
			this.SetConversationTopicIfNecessary(context);
			this.SetFromMeFlag(context);
			for (int j = 0; j < MapiMessage.deliverForbiddenProps.Length; j++)
			{
				base.InternalDeleteOnePropShouldNotFail(context, MapiMessage.deliverForbiddenProps[j]);
			}
			topMessage.SetIsHidden(context, false);
			bool? flag = (bool?)topMessage.GetPropertyValue(context, PropTag.Message.DeliverAsRead);
			if (flag != null && flag.Value)
			{
				topMessage.SetIsRead(context, true);
			}
			else
			{
				topMessage.SetIsRead(context, false);
				topMessage.ResetEverRead(context);
			}
			this.AdjustUncomputedMessageFlags(context, MessageFlags.None, MessageFlags.Unsent);
			topMessage.SetReadReceiptSent(context, false);
			this.storeMessage.SetIsDeliveryCompleted(context, true);
			MapiPersonCollection recipients = this.GetRecipients();
			if (recipients != null)
			{
				foreach (MapiPerson mapiPerson in ((IEnumerable<MapiPerson>)recipients))
				{
					if (!mapiPerson.IsDeleted && mapiPerson.StorePerson.RecipientType == RecipientType.Bcc)
					{
						mapiPerson.Delete();
					}
				}
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00018EE8 File Offset: 0x000170E8
		private void Invalidate()
		{
			if (this.people != null)
			{
				this.people.Dispose();
				this.people = null;
			}
			if (base.ParentObject is MapiAttachment)
			{
				((MapiAttachment)base.ParentObject).EmbeddedMessageClosed(this);
			}
			this.DisposeStoreMessage(true);
			base.IsValid = false;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00018F3B File Offset: 0x0001713B
		private void DisposeStoreMessage(bool doDispose)
		{
			if (this.storeMessage != null)
			{
				if (doDispose)
				{
					this.storeMessage.Dispose();
				}
				this.storeMessage = null;
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00018F5C File Offset: 0x0001715C
		public override void CopyProps(MapiContext context, MapiPropBagBase destination, IList<StorePropTag> propTags, bool replaceIfExists, ref List<MapiPropertyProblem> propProblems)
		{
			base.ThrowIfNotValid(null);
			MapiMessage mapiMessage = destination as MapiMessage;
			if (mapiMessage == null)
			{
				throw base.CreateCopyPropsNotSupportedException((LID)45304U, destination);
			}
			propProblems = null;
			if (object.ReferenceEquals(this, destination) || (this.mid.IsValid && mapiMessage.mid == this.mid))
			{
				throw new ExExceptionAccessDenied((LID)61688U, "Cannot copy message onto itself");
			}
			List<StorePropTag> list = new List<StorePropTag>(propTags);
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].PropId == 3603)
				{
					flag = true;
					list.RemoveAt(i);
					i--;
				}
				else if (list[i].PropId == 3602)
				{
					flag2 = true;
					list.RemoveAt(i);
					i--;
				}
				else if (list[i] == PropTag.Message.Subject)
				{
					list.RemoveAt(i);
					i--;
					list.Add(PropTag.Message.NormalizedSubject);
					list.Add(PropTag.Message.SubjectPrefix);
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
				this.CopyToCopyAttachmentsToDestination(context, mapiMessage, replaceIfExists ? CopyToFlags.None : CopyToFlags.DoNotReplaceProperties);
			}
			if (flag2)
			{
				this.CopyToCopyRecipientsToDestination(context, mapiMessage, replaceIfExists ? CopyToFlags.None : CopyToFlags.DoNotReplaceProperties);
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000190EC File Offset: 0x000172EC
		public override bool IsStreamSizeInvalid(MapiContext context, long size)
		{
			if (base.Logon.MailboxInfo.MaxStreamSize.IsUnlimited)
			{
				return false;
			}
			if ((base.Logon.InTransitStatus & InTransitStatus.AllowLargeItem) == InTransitStatus.AllowLargeItem)
			{
				return false;
			}
			if (this.checkStreamSize == null)
			{
				this.checkStreamSize = new bool?(ConfigurationSchema.CheckStreamSize.Value);
			}
			if (!this.checkStreamSize.Value)
			{
				return false;
			}
			if (size <= base.Logon.MailboxInfo.MaxStreamSize.Value)
			{
				return false;
			}
			DiagnosticContext.TraceDword((LID)53340U, (uint)size);
			DiagnosticContext.TraceDword((LID)41052U, (uint)base.Logon.MailboxInfo.MaxStreamSize.Value);
			this.streamSizeInvalid = true;
			return true;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000191B0 File Offset: 0x000173B0
		protected override void CopyToInternal(MapiContext context, MapiPropBagBase destination, IList<StorePropTag> propTagsExclude, CopyToFlags flags, ref List<MapiPropertyProblem> propProblems)
		{
			MapiMessage mapiMessage = destination as MapiMessage;
			if (mapiMessage == null)
			{
				throw base.CreateCopyToNotSupportedException((LID)53496U, destination);
			}
			if (object.ReferenceEquals(this, destination) || (this.mid.IsValid && mapiMessage.mid == this.mid && !mapiMessage.IsScrubbed))
			{
				throw new ExExceptionAccessDenied((LID)41208U, "Cannot Copy Message onto itself");
			}
			if (propTagsExclude != null && propTagsExclude.Count != 0)
			{
				for (int i = 0; i < propTagsExclude.Count; i++)
				{
					if (propTagsExclude[i].PropId == 3603)
					{
						flags &= ~CopyToFlags.CopyAttachments;
					}
					else if (propTagsExclude[i].PropId == 3602)
					{
						flags &= ~CopyToFlags.CopyRecipients;
					}
					else if (propTagsExclude[i].PropId == 3642)
					{
						flags |= CopyToFlags.CopyFirstLevelEmbeddedMessage;
					}
				}
			}
			if ((CopyToFlags.CopyRecipients & flags) != CopyToFlags.None)
			{
				this.CopyToCopyRecipientsToDestination(context, (MapiMessage)destination, flags);
			}
			if (((CopyToFlags.CopyAttachments | CopyToFlags.CopyFirstLevelEmbeddedMessage) & flags) != CopyToFlags.None)
			{
				this.CopyToCopyAttachmentsToDestination(context, (MapiMessage)destination, flags);
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000192C8 File Offset: 0x000174C8
		protected override void CopyToRemoveNoAccessProps(MapiContext context, MapiPropBagBase destination, List<StorePropTag> propTagsToCopy)
		{
			base.CopyToRemoveNoAccessProps(context, destination, propTagsToCopy);
			MapiMessage mapiMessage = (MapiMessage)destination;
			for (int i = propTagsToCopy.Count - 1; i >= 0; i--)
			{
				StorePropTag tag = propTagsToCopy[i];
				if (tag == PropTag.Message.BodyUnicode || tag == PropTag.Message.BodyHtml || tag == PropTag.Message.RtfCompressed)
				{
					short? bodyType = this.StoreMessage.GetBodyType(context);
					if ((bodyType == MapiMessage.htmlBodyFormat && tag != PropTag.Message.BodyHtml) || (bodyType == MapiMessage.textBodyFormat && tag != PropTag.Message.BodyUnicode) || (bodyType == MapiMessage.rtfBodyFormat && tag != PropTag.Message.RtfCompressed))
					{
						propTagsToCopy.RemoveAt(i);
					}
				}
				else if (tag == PropTag.Message.MessageFlags && ((!mapiMessage.IsEmbedded && !mapiMessage.StoreMessage.IsNew && !mapiMessage.IsScrubbed) || PropertyBagHelpers.TestPropertyFlags(context, this.StoreMessage, PropTag.Message.MessageFlagsActual, 4, 4)))
				{
					propTagsToCopy.RemoveAt(i);
					propTagsToCopy.Add(PropTag.Message.Read);
				}
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001944C File Offset: 0x0001764C
		private void CopyToCopyRecipientsToDestination(MapiContext context, MapiMessage destination, CopyToFlags flags)
		{
			MapiPersonCollection recipients = destination.GetRecipients();
			if ((CopyToFlags.DoNotReplaceProperties & flags) != CopyToFlags.None && recipients.GetAliveCount() != 0)
			{
				return;
			}
			recipients.DeleteAll();
			List<StorePropTag> list = new List<StorePropTag>(this.GetRecipientPropList(0U));
			base.CopyToRemoveNoAccessProps(context, null, list);
			IList<StorePropTag> recipientPropListExtra = this.GetRecipientPropListExtra();
			destination.SetRecipientPropListExtra(0U, recipientPropListExtra);
			int i = 0;
			int num = 0;
			while (i < this.GetRecipients().GetCount())
			{
				MapiPerson mapiPerson = this.GetRecipients()[i];
				if (mapiPerson != null && !mapiPerson.IsDeleted && mapiPerson.StorePerson != null)
				{
					MapiPerson item = recipients.GetItem(num, true);
					Properties props = mapiPerson.GetProps(context, list);
					MapiPropBagBase.CopyToRemoveInvalidProps(props);
					item.InternalSetPropsShouldNotFail(context, props);
					num++;
				}
				i++;
			}
			if ((CopyToFlags.MoveProperties & flags) != CopyToFlags.None)
			{
				this.GetRecipients().DeleteAll();
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00019520 File Offset: 0x00017720
		private bool IsAncestorOf(MapiMessage message)
		{
			MapiMessage mapiMessage2;
			for (MapiMessage mapiMessage = message; mapiMessage != null; mapiMessage = mapiMessage2)
			{
				MapiAttachment mapiAttachment = mapiMessage.ParentObject as MapiAttachment;
				if (mapiAttachment == null)
				{
					break;
				}
				mapiMessage2 = (mapiAttachment.ParentObject as MapiMessage);
				if (object.ReferenceEquals(mapiMessage2, this))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00019560 File Offset: 0x00017760
		private void CopyToCopyAttachmentsToDestination(MapiContext context, MapiMessage destination, CopyToFlags flags)
		{
			if ((CopyToFlags.DoNotReplaceProperties & flags) != CopyToFlags.None && destination.AttachmentCount != 0)
			{
				return;
			}
			if (this.IsAncestorOf(destination))
			{
				throw new StoreException((LID)57592U, ErrorCodeValue.MessageCycle, "Message cycle detected");
			}
			bool flag = (flags & CopyToFlags.CopyFirstLevelEmbeddedMessage) != CopyToFlags.None;
			destination.DeleteAllAttachments(context);
			if (this.AttachmentCount != 0)
			{
				foreach (int attachNum in this.GetAttachmentNumbers())
				{
					using (MapiAttachment mapiAttachment = new MapiAttachment())
					{
						mapiAttachment.Configure(context, this, AttachmentConfigureFlags.RequestReadOnly, attachNum);
						if (!flag || mapiAttachment.IsEmbeddedMessage(context))
						{
							MapiAttachment mapiAttachment2 = new MapiAttachment();
							mapiAttachment2.ConfigureNew(context, destination);
							List<MapiPropertyProblem> list = null;
							mapiAttachment.CopyTo(context, mapiAttachment2, null, flag ? CopyToFlags.CopyFirstLevelEmbeddedMessage : CopyToFlags.CopyEmbeddedMessage, ref list);
							mapiAttachment2.InternalSetOnePropShouldNotFail(context, PropTag.Attachment.RecordKey, Guid.NewGuid().ToByteArray());
							mapiAttachment2.SaveChanges(context);
						}
					}
				}
				if ((CopyToFlags.MoveProperties & flags) != CopyToFlags.None)
				{
					this.DeleteAllAttachments(context);
				}
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00019694 File Offset: 0x00017894
		private ErrorCode ConvertSendOptionProps(MapiContext context, DateTime submitTime)
		{
			ErrorCode noError = ErrorCode.NoError;
			object onePropValue = base.GetOnePropValue(context, PropTag.Message.DeferredSendNumber);
			object onePropValue2 = base.GetOnePropValue(context, PropTag.Message.DeferredSendUnits);
			object onePropValue3 = base.GetOnePropValue(context, PropTag.Message.DeferredSendTime);
			if (onePropValue != null && onePropValue2 != null)
			{
				DateTime utcNow = base.Logon.MapiMailbox.StoreMailbox.UtcNow;
				if (((int)onePropValue == 0 && (int)onePropValue2 == 0) || (onePropValue3 != null && DateTime.Compare((DateTime)onePropValue3, utcNow) < 0))
				{
					base.InternalDeleteOnePropShouldNotFail(context, PropTag.Message.DeferredSendTime);
				}
				else if ((int)onePropValue <= 999 && (int)onePropValue2 < 4)
				{
					DateTime dateTime = submitTime;
					switch ((int)onePropValue2)
					{
					case 0:
						dateTime = dateTime.AddMinutes((double)((int)onePropValue));
						break;
					case 1:
						dateTime = dateTime.AddHours((double)((int)onePropValue));
						break;
					case 2:
						dateTime = dateTime.AddDays((double)((int)onePropValue));
						break;
					case 3:
						dateTime = dateTime.AddDays((double)((int)onePropValue * 7));
						break;
					}
					base.InternalSetOnePropShouldNotFail(context, PropTag.Message.DeferredSendTime, dateTime);
				}
			}
			object onePropValue4 = base.GetOnePropValue(context, PropTag.Message.ExpiryNumber);
			object onePropValue5 = base.GetOnePropValue(context, PropTag.Message.ExpiryUnits);
			if (onePropValue4 != null && onePropValue5 != null)
			{
				DateTime dateTime2 = submitTime;
				switch ((int)onePropValue5)
				{
				case 0:
					dateTime2 = dateTime2.AddMinutes((double)((int)onePropValue4));
					break;
				case 1:
					dateTime2 = dateTime2.AddHours((double)((int)onePropValue4));
					break;
				case 2:
					dateTime2 = dateTime2.AddDays((double)((int)onePropValue4));
					break;
				case 3:
					dateTime2 = dateTime2.AddDays((double)((int)onePropValue4 * 7));
					break;
				}
				base.InternalSetOnePropShouldNotFail(context, PropTag.Message.ExpiryTime, dateTime2);
			}
			return noError;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001986C File Offset: 0x00017A6C
		private ErrorCode SetSender(MapiContext context, bool systemSubmit, bool ignoreSendAsRightsRequested, object sentRepresentingEntryId, AddressInfo addressInfoForAuthorization, bool needsSpooler, SubmitMessageRightsCheckFlags submitMessageRightsCheckFlags)
		{
			ErrorCode noError = ErrorCode.NoError;
			Trace submitMessageTracer = ExTraceGlobals.SubmitMessageTracer;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = MessageClassHelper.IsSchedulePlusMessage(this.GetMessageClass(context));
			bool flag5 = base.Logon.MapiMailbox.IsPublicFolderMailbox && ignoreSendAsRightsRequested;
			bool flag6 = (submitMessageRightsCheckFlags & SubmitMessageRightsCheckFlags.SendingAsDL) == SubmitMessageRightsCheckFlags.SendingAsDL;
			bool flag7 = addressInfoForAuthorization != null && addressInfoForAuthorization.IsMailPublicFolder;
			if (!systemSubmit || (!this.IsNDR(context) && !this.IsQuotaMessage(context)))
			{
				StorePropName propName = NamedPropInfo.Sharing.SharingInstanceGuid.PropName;
				ushort propId;
				StoreNamedPropInfo propertyInfo;
				base.Logon.StoreMailbox.NamedPropertyMap.GetNumberFromName(context, propName, true, base.Logon.StoreMailbox.QuotaInfo, out propId, out propertyInfo);
				StorePropTag propTag = new StorePropTag(propId, PropertyType.Guid, propertyInfo, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);
				object onePropValue = base.GetOnePropValue(context, propTag);
				if (onePropValue != null)
				{
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug(0L, "We have subscription ID present, we will not check SendAs/SOBO rights in store and leave that to Transport");
					}
					flag = true;
				}
				else
				{
					if (sentRepresentingEntryId == null)
					{
						throw new ExExceptionSendAsDenied((LID)48232U, "SentRepresentingEntryId is null");
					}
					base.InternalSetOnePropShouldNotFail(context, PropTag.Message.SentRepresentingEntryId, sentRepresentingEntryId);
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug<string>(0L, "SetSender: SentRepresentingEntryId={0}", BitConverter.ToString((byte[])sentRepresentingEntryId));
					}
					if (addressInfoForAuthorization == null && !flag4 && !flag5)
					{
						if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							submitMessageTracer.TraceDebug(0L, "SendAs check has failed becuase AI for authhorization is null");
							DiagnosticContext.TraceLocation((LID)39864U);
						}
						throw new ExExceptionSendAsDenied((LID)56248U, "SendAs check failed becuase AI is null");
					}
					flag3 = ((submitMessageRightsCheckFlags & SubmitMessageRightsCheckFlags.SOBORights) == SubmitMessageRightsCheckFlags.SOBORights);
					flag2 = ((submitMessageRightsCheckFlags & SubmitMessageRightsCheckFlags.SendAsRights) == SubmitMessageRightsCheckFlags.SendAsRights);
					if (base.Logon.MapiMailbox.IsGroupMailbox && (context.ClientType == ClientType.OWA || context.ClientType == ClientType.WebServices))
					{
						flag3 = true;
						flag2 = false;
					}
					if (!flag2)
					{
						if (flag5)
						{
							flag2 = true;
						}
						else if (flag4)
						{
							flag = true;
							if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								submitMessageTracer.TraceDebug(0L, "SchedulePlus message; skipping SOBO access check, but we do need to set the Sender");
							}
						}
						else
						{
							if (!flag3)
							{
								throw new ExExceptionSendAsDenied((LID)33016U, "SOBO rights have been checked but are not granted");
							}
							flag = true;
						}
					}
				}
				if (flag2 || flag3)
				{
					if (addressInfoForAuthorization != null)
					{
						string displayName = addressInfoForAuthorization.DisplayName;
						string simpleDisplayName = addressInfoForAuthorization.SimpleDisplayName;
						base.InternalSetOnePropShouldNotFail(context, PropTag.Message.SentRepresentingName, displayName);
						if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							submitMessageTracer.TraceDebug<string>(0L, "SetSender: SentRepresentingName={0}", (displayName == null) ? "N/A" : displayName);
						}
						if (simpleDisplayName != null)
						{
							base.InternalSetOnePropShouldNotFail(context, PropTag.Message.SentRepresentingSimpleDisplayName, simpleDisplayName);
							if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								submitMessageTracer.TraceDebug<string>(0L, "SetSender: SentRepresentingSimpleDisplayName={0}", simpleDisplayName);
							}
						}
						if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							submitMessageTracer.TraceDebug<string, string>(0L, "SentRepresentingName set to {0}; SentRepresentingSimpleDisplayName set to {1}", displayName, simpleDisplayName);
						}
					}
					if (ConfigurationSchema.EnableSetSenderSpoofingFix.Value)
					{
						this.InternalSetOneProp(context, PropTag.Message.SentRepresentingAddressType, null);
						this.InternalSetOneProp(context, PropTag.Message.SentRepresentingEmailAddress, null);
					}
				}
				if (flag6 || flag7)
				{
					AddressInfo effectiveOwnerAddressInfo = base.Logon.EffectiveOwnerAddressInfo;
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug<string, string, string>(0L, "Submitting as a {0}; originator set to {1}; distinguishedName {2}", flag6 ? "DL" : "MailPublicFolder", effectiveOwnerAddressInfo.DisplayName, effectiveOwnerAddressInfo.DistinguishedName);
					}
					this.SetOriginatorFromAddressInfo(context, effectiveOwnerAddressInfo);
				}
				if (flag || needsSpooler)
				{
					AddressInfo addressInfo;
					if (base.Logon.MapiMailbox.IsGroupMailbox && (context.ClientType == ClientType.OWA || context.ClientType == ClientType.WebServices) && !flag4)
					{
						addressInfo = base.Logon.MailboxOwnerAddressInfo;
					}
					else if (!base.Logon.IsPrimaryOwner)
					{
						addressInfo = base.Logon.LoggedOnUserAddressInfo;
					}
					else
					{
						addressInfo = base.Logon.EffectiveOwnerAddressInfo;
					}
					this.SetSenderFromAI(context, addressInfo);
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug<string, string>(0L, "SenderName set to {0}; distinguishedName {1}", addressInfo.DisplayName, addressInfo.DistinguishedName);
					}
				}
				else if (!systemSubmit && (!flag2 || !ignoreSendAsRightsRequested || context.ClientType != ClientType.TimeBasedAssistants))
				{
					if (submitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						submitMessageTracer.TraceDebug(0L, "Removed sender information");
					}
					this.RemoveSender(context);
				}
			}
			if (!flag2 && !flag3 && submitMessageTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				submitMessageTracer.TraceDebug(0L, "SendAs/SOBO check has failed");
				DiagnosticContext.TraceLocation((LID)54855U);
			}
			return noError;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00019C84 File Offset: 0x00017E84
		private void SetOriginatorFromAddressInfo(MapiContext context, AddressInfo addressInfo)
		{
			if (ConfigurationSchema.EnableSetSenderSpoofingFix.Value)
			{
				AddressInfoSetter.Delete(context, this.storeMessage, AddressInfoTags.Originator);
			}
			object[] values = new object[]
			{
				addressInfo.UserEntryId(),
				addressInfo.PrimaryEmailAddressType,
				addressInfo.DisplayName,
				addressInfo.PrimaryEmailAddress,
				addressInfo.SimpleDisplayName,
				addressInfo.UserFlags()
			};
			base.InternalSetPropsShouldNotFail(context, new Properties(MapiMessage.originatorProps, values));
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00019D04 File Offset: 0x00017F04
		private void RemoveSender(MapiContext context)
		{
			if (ConfigurationSchema.EnableSetSenderSpoofingFix.Value)
			{
				AddressInfoSetter.Delete(context, this.storeMessage, AddressInfoTags.Sender);
				return;
			}
			base.InternalDeletePropsShouldNotFail(context, MapiMessage.senderPropsRemove);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00019D30 File Offset: 0x00017F30
		private void SetSenderFromAI(MapiContext context, AddressInfo addressInfo)
		{
			if (ConfigurationSchema.EnableSetSenderSpoofingFix.Value)
			{
				AddressInfoSetter.Delete(context, this.storeMessage, AddressInfoTags.Sender);
			}
			byte[] array = null;
			SecurityIdentifier securityIdentifier = SecurityHelper.ComputeObjectSID(addressInfo.UserSid, addressInfo.MasterAccountSid);
			if (securityIdentifier != null)
			{
				array = new byte[securityIdentifier.BinaryLength];
				securityIdentifier.GetBinaryForm(array, 0);
			}
			object[] values = new object[]
			{
				addressInfo.UserEntryId(),
				addressInfo.PrimaryEmailAddressType,
				addressInfo.PrimaryEmailAddress,
				addressInfo.DisplayName,
				addressInfo.SimpleDisplayName,
				addressInfo.UserFlags(),
				array,
				addressInfo.ObjectId.ToByteArray()
			};
			base.InternalSetPropsShouldNotFail(context, new Properties(MapiMessage.senderPropsSetAI, values));
			if (ExTraceGlobals.SubmitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SubmitMessageTracer.TraceDebug(0L, "SetSenderFromAI: EntryId={0}, AddressType={1}, DisplayName={2}, EmailAddress={3}, ObjectId={4}", new object[]
				{
					(addressInfo.UserEntryId() == null) ? "N/A" : BitConverter.ToString(addressInfo.UserEntryId()),
					(addressInfo.PrimaryEmailAddressType == null) ? "N/A" : addressInfo.PrimaryEmailAddressType,
					(addressInfo.DisplayName == null) ? "N/A" : addressInfo.DisplayName,
					(addressInfo.PrimaryEmailAddress == null) ? "N/A" : addressInfo.PrimaryEmailAddress,
					addressInfo.ObjectId
				});
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00019E9C File Offset: 0x0001809C
		private void SetResponsibility(MapiContext context, out bool mdbRecipients, out bool spoolerRecipients)
		{
			mdbRecipients = false;
			spoolerRecipients = false;
			MapiPersonCollection recipients = this.GetRecipients();
			for (int i = 0; i < recipients.GetCount(); i++)
			{
				MapiPerson mapiPerson = recipients[i];
				if (!mapiPerson.IsDeleted)
				{
					object onePropValue = mapiPerson.GetOnePropValue(context, PropTag.Recipient.AddressType);
					object onePropValue2 = mapiPerson.GetOnePropValue(context, PropTag.Recipient.EntryId);
					object onePropValue3 = mapiPerson.GetOnePropValue(context, PropTag.Recipient.Responsibility);
					object onePropValue4 = mapiPerson.GetOnePropValue(context, PropTag.Recipient.RecipientType);
					if (onePropValue == null || onePropValue4 == null || onePropValue3 == null || onePropValue2 == null)
					{
						throw new ExExceptionInvalidRecipients((LID)49400U, "Recipient is missing ptagAddressType, ptagEntryId, ptagRecipientType, or ptagResponsibility");
					}
					if ((string)onePropValue == "SMTP" && mapiPerson.GetOnePropValue(context, PropTag.Recipient.EmailAddress) == null)
					{
						throw new ExExceptionInvalidRecipients((LID)48888U, "Recipient is missing ptagEmailAddress");
					}
					if ((RecipientType)onePropValue4 == RecipientType.To || (RecipientType)onePropValue4 == RecipientType.Cc || (RecipientType)onePropValue4 == RecipientType.Bcc || (RecipientType)onePropValue4 == RecipientType.P1)
					{
						if (!(bool)onePropValue3)
						{
							spoolerRecipients = true;
						}
						else
						{
							mdbRecipients = true;
						}
					}
				}
			}
			if (!mdbRecipients && !spoolerRecipients)
			{
				throw new ExExceptionInvalidRecipients((LID)57080U, "Neither MDB nor spooler is responsible - that is bad");
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00019FD0 File Offset: 0x000181D0
		private void SetFromMeFlag(MapiContext context)
		{
			byte[] array = base.GetOnePropValue(context, PropTag.Message.SenderEntryId) as byte[];
			bool flag = false;
			Eidt eidt;
			string strB;
			if (array != null && AddressBookEID.IsAddressBookEntryId(context, array, out eidt, out strB))
			{
				string legacyExchangeDN = base.Logon.EffectiveOwnerAddressInfo.LegacyExchangeDN;
				flag = (string.Compare(legacyExchangeDN, strB, StringComparison.OrdinalIgnoreCase) == 0);
			}
			if (flag)
			{
				this.AdjustUncomputedMessageFlags(context, MessageFlags.FromMe, MessageFlags.None);
				return;
			}
			this.AdjustUncomputedMessageFlags(context, MessageFlags.None, MessageFlags.FromMe);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001A03C File Offset: 0x0001823C
		private void SetReceivedByProps(MapiContext context, RecipientType recipientType)
		{
			byte[] value = base.Logon.MailboxOwnerAddressInfo.UserEntryId();
			string legacyExchangeDN = base.Logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
			string displayName = base.Logon.MailboxOwnerAddressInfo.DisplayName;
			string simpleDisplayName = base.Logon.MailboxOwnerAddressInfo.SimpleDisplayName;
			string originalEmailAddress = base.Logon.MailboxOwnerAddressInfo.OriginalEmailAddress;
			if (this.IsNDR(context))
			{
				recipientType = RecipientType.To;
			}
			else if (recipientType == RecipientType.Unknown)
			{
				MapiPersonCollection recipients = this.GetRecipients();
				for (int i = 0; i < recipients.GetCount(); i++)
				{
					MapiPerson mapiPerson = recipients[i];
					Eidt eidt;
					string strB;
					if (!mapiPerson.IsDeleted && AddressBookEID.IsAddressBookEntryId(context, mapiPerson.GetEntryId(), out eidt, out strB) && string.Compare(legacyExchangeDN, strB, StringComparison.OrdinalIgnoreCase) == 0)
					{
						recipientType = (RecipientType)mapiPerson.GetRecipientType();
						displayName = mapiPerson.GetDisplayName();
						simpleDisplayName = mapiPerson.GetSimpleDisplayName();
						break;
					}
				}
			}
			bool flag = this.IsNDR(context) || base.GetOnePropValue(context, PropTag.Message.ReceivedRepresentingEntryId) == null;
			AddressInfoSetter.Delete(context, this.storeMessage, AddressInfoTags.ReceivedBy);
			base.InternalSetOnePropShouldNotFail(context, PropTag.Message.ReceivedByEntryId, value);
			base.InternalSetOnePropShouldNotFail(context, PropTag.Message.ReceivedByName, displayName);
			base.InternalSetOnePropShouldNotFail(context, PropTag.Message.ReceivedBySimpleDisplayName, simpleDisplayName);
			if (originalEmailAddress != null)
			{
				base.InternalSetOnePropShouldNotFail(context, PropTag.Message.ReceivedBySMTPAddress, originalEmailAddress);
			}
			if (flag)
			{
				AddressInfoSetter.Delete(context, this.storeMessage, AddressInfoTags.ReceivedRepresenting);
				base.InternalSetOnePropShouldNotFail(context, PropTag.Message.ReceivedRepresentingEntryId, value);
				base.InternalSetOnePropShouldNotFail(context, PropTag.Message.ReceivedRepresentingName, displayName);
				base.InternalSetOnePropShouldNotFail(context, PropTag.Message.ReceivedRepresentingSimpleDisplayName, simpleDisplayName);
				if (originalEmailAddress != null)
				{
					base.InternalSetOnePropShouldNotFail(context, PropTag.Message.ReceivedRepresentingSMTPAddress, originalEmailAddress);
				}
			}
			if (recipientType == RecipientType.To || recipientType == RecipientType.Cc || recipientType == RecipientType.Bcc)
			{
				base.InternalSetOnePropShouldNotFail(context, PropTag.Message.MessageToMe, recipientType == RecipientType.To);
				base.InternalSetOnePropShouldNotFail(context, PropTag.Message.MessageCCMe, recipientType == RecipientType.Cc);
				base.InternalSetOnePropShouldNotFail(context, PropTag.Message.MessageRecipMe, recipientType == RecipientType.To || recipientType == RecipientType.Cc || recipientType == RecipientType.Bcc);
				return;
			}
			base.InternalDeleteOnePropShouldNotFail(context, PropTag.Message.MessageToMe);
			base.InternalDeleteOnePropShouldNotFail(context, PropTag.Message.MessageCCMe);
			base.InternalDeleteOnePropShouldNotFail(context, PropTag.Message.MessageRecipMe);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001A24C File Offset: 0x0001844C
		private void SpecialRecipientProcessing(MapiContext context)
		{
			bool flag = PropertyBagHelpers.TestPropertyFlags(context, this.StoreMessage, PropTag.Message.MessageFlagsActual, 128, 128);
			bool flag2 = PropertyBagHelpers.TestPropertyFlags(context, this.StoreMessage, PropTag.Message.MessageFlagsActual, 131072, 131072);
			if (!flag && !flag2)
			{
				return;
			}
			if (ExTraceGlobals.SubmitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (flag)
				{
					ExTraceGlobals.SubmitMessageTracer.TraceDebug(35888L, "Prepare the resend message for submission");
				}
				else
				{
					ExTraceGlobals.SubmitMessageTracer.TraceDebug(35888L, "Prepare the message which need special handling of recipients");
				}
			}
			MapiPersonCollection recipients = this.GetRecipients();
			for (int i = 0; i < recipients.GetCount(); i++)
			{
				MapiPerson mapiPerson = recipients[i];
				if (!mapiPerson.IsDeleted)
				{
					RecipientType recipientType = (RecipientType)mapiPerson.GetRecipientType();
					RecipientType recipientType2 = recipientType;
					if (flag)
					{
						recipientType2 = this.ProcessRecipientForResend(mapiPerson, recipientType);
					}
					if (flag2)
					{
						recipientType2 = this.ProcessSpecialRecipient(mapiPerson, recipientType);
					}
					mapiPerson.SetRecipientType((int)recipientType2);
					if (ExTraceGlobals.SubmitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SubmitMessageTracer.TraceDebug<RecipientType, RecipientType>(52272L, "Change {0} recipient type into {1} recipient type", recipientType, recipientType2);
					}
				}
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001A358 File Offset: 0x00018558
		private RecipientType ProcessRecipientForResend(MapiPerson recip, RecipientType recipientType)
		{
			RecipientType result;
			if ((recipientType & RecipientType.Submitted) == RecipientType.Submitted)
			{
				result = (recipientType & (RecipientType)2147483647);
				recip.SetResponsibility(true);
			}
			else
			{
				result = RecipientType.P1;
			}
			return result;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001A390 File Offset: 0x00018590
		private RecipientType ProcessSpecialRecipient(MapiPerson recip, RecipientType recipientType)
		{
			RecipientType result = recipientType;
			if ((recipientType & RecipientType.Submitted) == RecipientType.Submitted)
			{
				result = (recipientType & (RecipientType)2147483647);
				recip.SetResponsibility(false);
			}
			return result;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001A3C0 File Offset: 0x000185C0
		private void UndoSpecialRecipientProcessing(MapiContext context)
		{
			PropertyBagHelpers.TestPropertyFlags(context, this.StoreMessage, PropTag.Message.MessageFlagsActual, 128, 128);
			if (ExTraceGlobals.SubmitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SubmitMessageTracer.TraceDebug(46128L, "Prepare the resend message to abort submission");
			}
			MapiPersonCollection recipients = this.GetRecipients();
			for (int i = 0; i < recipients.GetCount(); i++)
			{
				MapiPerson mapiPerson = recipients[i];
				if (!mapiPerson.IsDeleted)
				{
					RecipientType recipientType = (RecipientType)mapiPerson.GetRecipientType();
					RecipientType recipientType2;
					if (recipientType != RecipientType.P1)
					{
						recipientType2 = (recipientType | RecipientType.Submitted);
						mapiPerson.SetResponsibility(true);
					}
					else
					{
						recipientType2 = RecipientType.To;
					}
					mapiPerson.SetRecipientType((int)recipientType2);
					if (ExTraceGlobals.SubmitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SubmitMessageTracer.TraceDebug<RecipientType, RecipientType>(62512L, "Change {0} recipient type into {1} recipient type", recipientType, recipientType2);
					}
				}
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001A484 File Offset: 0x00018684
		private bool PerformShutoffQuotaCheck(MapiContext context, bool save)
		{
			if (base.Logon.IsMoveDestination)
			{
				return false;
			}
			if (base.Logon.ExchangeTransportServiceRights && (base.Logon.IsReportMessageDelivery || base.Logon.IsNormalMessageDelivery || base.Logon.IsQuotaMessageDelivery))
			{
				return false;
			}
			TopMessage topMessage = (TopMessage)this.storeMessage;
			if ((context.ClientType == ClientType.TimeBasedAssistants || context.ClientType == ClientType.EventBasedAssistants || context.ClientType == ClientType.ELC) && (context.ClientType == ClientType.ELC || (context.Diagnostics != null && context.Diagnostics.ClientComponentName != null && context.Diagnostics.ClientComponentName.Equals("ELCAssistant", StringComparison.OrdinalIgnoreCase))))
			{
				if (!save || !this.storeMessage.IsNew)
				{
					return false;
				}
				string messageClass = this.GetMessageClass(context);
				if (MessageClassHelper.MatchingMessageClass(messageClass, "IPM.Configuration.MRM") || MessageClassHelper.MatchingMessageClass(messageClass, "IPM.Microsoft.MRM.Log") || MessageClassHelper.MatchingMessageClass(messageClass, "IPM.Configuration.ELC"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001A57C File Offset: 0x0001877C
		private MapiMessage CreateTargetMessageForOutlook(MapiContext context, byte[] targetEntryId)
		{
			ExchangeId @null = ExchangeId.Null;
			ExchangeId null2 = ExchangeId.Null;
			if (!Helper.ParseMessageEntryId(context, base.Logon, targetEntryId, out @null, out null2))
			{
				if (ExTraceGlobals.SubmitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SubmitMessageTracer.TraceDebug(55480L, "TargetEntryId is invalid: " + BitConverter.ToString(targetEntryId));
				}
				return null;
			}
			using (MapiFolder mapiFolder = MapiFolder.OpenFolder(context, base.Logon, @null))
			{
				if (mapiFolder == null)
				{
					if (ExTraceGlobals.SubmitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SubmitMessageTracer.TraceDebug(43192L, "Cannot open the target folder");
					}
					return null;
				}
				if (mapiFolder.IsSearchFolder())
				{
					if (ExTraceGlobals.SubmitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SubmitMessageTracer.TraceDebug(59576L, "Cannot have search folder as target folder");
					}
					return null;
				}
			}
			MapiMessage mapiMessage = null;
			MapiMessage mapiMessage2;
			mapiMessage = (mapiMessage2 = new MapiMessage());
			try
			{
				ErrorCode errorCode = mapiMessage.ConfigureMessage(context, base.Logon, @null, null2, MessageConfigureFlags.None, this.CodePage);
				if (errorCode != ErrorCodeValue.NotFound)
				{
					if (ExTraceGlobals.SubmitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SubmitMessageTracer.TraceDebug(32952L, "Target message has been there");
					}
					return null;
				}
			}
			finally
			{
				if (mapiMessage2 != null)
				{
					((IDisposable)mapiMessage2).Dispose();
				}
			}
			bool flag = false;
			try
			{
				mapiMessage = new MapiMessage();
				ErrorCode first = mapiMessage.ConfigureMessage(context, base.Logon, @null, null2, MessageConfigureFlags.CreateNewMessage | MessageConfigureFlags.SkipQuotaCheck, this.CodePage, this);
				if (first != ErrorCode.NoError)
				{
					if (ExTraceGlobals.SubmitMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SubmitMessageTracer.TraceDebug(35000L, "Unable to configure the target message, error code " + first.ToString());
					}
					return null;
				}
				mapiMessage.SetMessageId(context, null2);
				first = FaultInjection.InjectError(MapiMessage.hookableCreateTargetMessageForOutlook);
				if (first != ErrorCode.NoError)
				{
					return null;
				}
				mapiMessage.StoreMessage.SetReadReceiptSent(context, true);
				mapiMessage.StoreMessage.SetIsRead(context, true);
				mapiMessage.AdjustUncomputedMessageFlags(context, MessageFlags.None, MessageFlags.Unsent);
				flag = true;
			}
			finally
			{
				if (!flag && mapiMessage != null && mapiMessage.StoreMessage != null)
				{
					((TopMessage)mapiMessage.StoreMessage).Delete(context);
					mapiMessage.Dispose();
					mapiMessage = null;
				}
			}
			return mapiMessage;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0001A7CC File Offset: 0x000189CC
		private void EnforceMaxSaveSize(MapiContext context, bool submitMessage)
		{
			bool flag = ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (base.Logon.SystemRights)
			{
				if (flag)
				{
					ExTraceGlobals.QuotaTracer.TraceDebug<ExchangeId>(0L, "System logon, no size limits applied to message {0}", this.Mid);
				}
				return;
			}
			UnlimitedBytes unlimitedBytes;
			if (submitMessage)
			{
				unlimitedBytes = base.Logon.MailboxInfo.MaxSendSize;
				if (flag)
				{
					ExTraceGlobals.QuotaTracer.TraceDebug<string>(0L, "MaxSendSize: {0}", unlimitedBytes.IsUnlimited ? "Unlimited" : unlimitedBytes.Value.ToString());
				}
			}
			else
			{
				if (base.Logon.AllowLargeItem())
				{
					if (flag)
					{
						ExTraceGlobals.QuotaTracer.TraceDebug<ExchangeId>(0L, "Intraorg mailbox move, no size limits applied to message {0}", this.Mid);
					}
					return;
				}
				if (MessageClassHelper.IsCalendarFamilyMessage(this.GetMessageClass(context)))
				{
					if (flag)
					{
						ExTraceGlobals.QuotaTracer.TraceDebug<ExchangeId>(0L, "Calendar message, no size limits applied to message {0}", this.Mid);
					}
					return;
				}
				TopMessage topMessage = this.storeMessage as TopMessage;
				if (base.Logon.MapiMailbox.IsPublicFolderMailbox && topMessage != null)
				{
					unlimitedBytes = topMessage.ParentFolder.GetMaxPublicFolderItemSize(context);
					if (unlimitedBytes > base.Logon.MailboxInfo.MaxMessageSize)
					{
						unlimitedBytes = base.Logon.MailboxInfo.MaxMessageSize;
					}
					if (flag)
					{
						ExTraceGlobals.QuotaTracer.TraceDebug<string>(0L, "MaxPublicFolderItemSize: {0}", unlimitedBytes.IsUnlimited ? "Unlimited" : unlimitedBytes.Value.ToString());
					}
				}
				else
				{
					unlimitedBytes = base.Logon.MailboxInfo.MaxMessageSize;
				}
				if (flag)
				{
					ExTraceGlobals.QuotaTracer.TraceDebug<string>(0L, "MaxMessageSize: {0}", unlimitedBytes.IsUnlimited ? "Unlimited" : unlimitedBytes.Value.ToString());
				}
			}
			if (unlimitedBytes.IsUnlimited)
			{
				if (flag)
				{
					ExTraceGlobals.QuotaTracer.TraceDebug<ExchangeId>(0L, "Unlimited size applied to message {0}", this.Mid);
				}
				return;
			}
			long num = this.StoreMessage.CurrentSize - unlimitedBytes.Value;
			if (num <= 1048576L || num < unlimitedBytes.Value / 10L)
			{
				return;
			}
			if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				ExTraceGlobals.QuotaTracer.TraceError<ExchangeId, long, long>(0L, "Message {0} over size limitation, current size={1}, limitation={2}", this.Mid, this.StoreMessage.CurrentSize, unlimitedBytes.Value);
			}
			DiagnosticContext.TraceDwordAndString((LID)59176U, (uint)unlimitedBytes.Value, "Limitation");
			DiagnosticContext.TraceDwordAndString((LID)34600U, (uint)this.StoreMessage.CurrentSize, "CurrentSize");
			throw new ExExceptionMaxSubmissionExceeded((LID)42792U, "Message over size limitation");
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001AA4C File Offset: 0x00018C4C
		private object[] AddressInfoValuesFromAddressInfo(AddressInfo addressInfo)
		{
			SecurityIdentifier securityIdentifier = SecurityHelper.ComputeObjectSID(addressInfo.UserSid, addressInfo.MasterAccountSid);
			if (securityIdentifier == null)
			{
				securityIdentifier = MapiMessage.localSystemSID;
			}
			byte[] array = new byte[securityIdentifier.BinaryLength];
			securityIdentifier.GetBinaryForm(array, 0);
			return new object[]
			{
				addressInfo.UserEntryId(),
				addressInfo.PrimaryEmailAddressType,
				addressInfo.PrimaryEmailAddress,
				addressInfo.DisplayName,
				addressInfo.SimpleDisplayName,
				addressInfo.UserFlags(),
				null,
				null,
				array,
				addressInfo.ObjectId.ToByteArray()
			};
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001AAE8 File Offset: 0x00018CE8
		private void SetCreatorLastModifierAddressInfoProperties(MapiContext context, MapiSaveMessageChangesFlags saveFlags)
		{
			bool flag = AddressInfoGetter.IsAddressInfoTagSet(context, this.storeMessage, AddressInfoTags.SentRepresenting);
			bool flag2 = AddressInfoGetter.IsAddressInfoTagSet(context, this.storeMessage, AddressInfoTags.Sender);
			object[] array;
			if ((saveFlags & MapiSaveMessageChangesFlags.Delivery) != MapiSaveMessageChangesFlags.None)
			{
				if (flag || flag2)
				{
					int num = 0;
					array = new object[AddressInfoTags.Sender.Length - 1];
					foreach (StorePropTag storePropTag in AddressInfoTags.Sender)
					{
						if (!(storePropTag == PropTag.Message.SenderSearchKey))
						{
							object propertyValueImp = this.GetPropertyValueImp(context, storePropTag);
							if (storePropTag == PropTag.Message.SenderSID && propertyValueImp != null && !SecurityHelper.IsValidSecurityIdentifierBlob(context, (byte[])propertyValueImp))
							{
								throw new StoreException((LID)51808U, ErrorCodeValue.InvalidParameter, "Corrupted SenderSID");
							}
							array[num++] = propertyValueImp;
						}
					}
				}
				else
				{
					array = this.AddressInfoValuesFromAddressInfo(base.Logon.EffectiveOwnerAddressInfo);
				}
				AddressInfoSetter.Delete(context, this.storeMessage, AddressInfoTags.Creator);
			}
			else
			{
				array = this.AddressInfoValuesFromAddressInfo(base.Logon.EffectiveOwnerAddressInfo);
			}
			bool flag3 = AddressInfoGetter.IsAddressInfoTagSet(context, this.storeMessage, AddressInfoTags.Creator);
			bool flag4 = AddressInfoGetter.IsAddressInfoTagSet(context, this.storeMessage, AddressInfoTags.LastModifier);
			bool flag5 = MessageClassHelper.IsFreeDocumentMessage(this.GetMessageClass(context));
			bool flag6 = base.Logon.StoreMailbox.SharedState.MailboxTypeDetail == MailboxInfo.MailboxTypeDetail.TeamMailbox;
			bool flag7 = base.Logon.IsMoveDestination || context.ClientType == ClientType.Migration || context.ClientType == ClientType.SimpleMigration || context.ClientType == ClientType.PublicFolderSystem;
			if (!flag3 && !flag7 && this.storeMessage.IsNew)
			{
				List<StorePropTag> list = new List<StorePropTag>(AddressInfoTags.Creator);
				list.Remove(StorePropTag.Invalid);
				base.InternalSetPropsShouldNotFail(context, new Properties(list, array));
			}
			if (!flag7 && (!flag6 || !flag5) && !this.IsEmbedded && (this.icsImport == null || !flag4 || !base.Logon.IsOwner))
			{
				AddressInfoSetter.Delete(context, this.storeMessage, AddressInfoTags.LastModifier);
				List<StorePropTag> list = new List<StorePropTag>(AddressInfoTags.LastModifier);
				list.Remove(StorePropTag.Invalid);
				base.InternalSetPropsShouldNotFail(context, new Properties(list, array));
			}
			if (!this.IsEmbedded && AddressInfoGetter.AddressInfoTagsAreProperSubset(context, this.storeMessage, AddressInfoTags.AddressInfoType.Creator, AddressInfoTags.AddressInfoType.LastModifier))
			{
				AddressInfoSetter.CopyAddressInfoTags(context, this.storeMessage, AddressInfoTags.AddressInfoType.LastModifier, AddressInfoTags.AddressInfoType.Creator);
			}
			if (AddressInfoGetter.AddressInfoTagsAreProperSubset(context, this.storeMessage, AddressInfoTags.AddressInfoType.LastModifier, AddressInfoTags.AddressInfoType.Creator))
			{
				AddressInfoSetter.Delete(context, this.storeMessage, AddressInfoTags.LastModifier);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001AD68 File Offset: 0x00018F68
		private void AdjustIRMMessageFlag(MapiContext context)
		{
			bool flag = false;
			ushort numberFromName = base.Logon.MapiMailbox.GetNumberFromName(context, false, NamedPropInfo.InternetHeaders.ContentClass.PropName, base.Logon);
			if (numberFromName != 0)
			{
				StorePropTag propTag = LegacyHelper.ConvertFromLegacyPropTag((uint)(((int)numberFromName << 16) + 31), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message, base.Logon.MapiMailbox, false);
				Property property = this.InternalGetOneProp(context, propTag);
				if (!property.IsError)
				{
					string text = property.Value as string;
					if (text != null && (string.Equals(text, "rpmsg.message", StringComparison.OrdinalIgnoreCase) || string.Equals(text, "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA", StringComparison.OrdinalIgnoreCase) || string.Equals(text, "IPM.Note.rpmsg.Microsoft.Voicemail.UM", StringComparison.OrdinalIgnoreCase)))
					{
						bool flag2 = false;
						foreach (int attachNum in this.GetAttachmentNumbers())
						{
							if (!flag2)
							{
								flag2 = true;
								using (MapiAttachment mapiAttachment = new MapiAttachment())
								{
									ErrorCode first = mapiAttachment.Configure(context, this, AttachmentConfigureFlags.RequestReadOnly, attachNum);
									if (first == ErrorCode.NoError)
									{
										string text2 = mapiAttachment.GetOnePropValue(context, PropTag.Attachment.ContentType) as string;
										if (text2 != null && string.Equals(text2, "messag~1.rpm", StringComparison.OrdinalIgnoreCase))
										{
											flag = true;
										}
									}
									continue;
								}
							}
							flag = false;
							break;
						}
					}
				}
			}
			this.storeMessage.AdjustUncomputedMessageFlags(context, flag ? MessageFlags.Irm : MessageFlags.None, flag ? MessageFlags.None : MessageFlags.Irm);
		}

		// Token: 0x040001F1 RID: 497
		internal const string RpmsgMessageContentClass = "rpmsg.message";

		// Token: 0x040001F2 RID: 498
		internal const string ExchangeUMProtectedMessageClass = "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA";

		// Token: 0x040001F3 RID: 499
		internal const string ExchangeUMProtectedPureAudioClass = "IPM.Note.rpmsg.Microsoft.Voicemail.UM";

		// Token: 0x040001F4 RID: 500
		internal const string RpmsgAttachmentFilename = "messag~1.rpm";

		// Token: 0x040001F5 RID: 501
		internal const string ReadReportClassPrefix = "REPORT";

		// Token: 0x040001F6 RID: 502
		internal const string ReadReportClassPrefixWithDot = "REPORT.";

		// Token: 0x040001F7 RID: 503
		internal const string ReadReportClassSuffix = "IPNRN";

		// Token: 0x040001F8 RID: 504
		internal const string ReadReportClassSuffixWithDot = ".IPNRN";

		// Token: 0x040001F9 RID: 505
		internal const string NotReadReportClassSuffix = "IPNNRN";

		// Token: 0x040001FA RID: 506
		internal const string NotReadReportClassSuffixWithDot = ".IPNNRN";

		// Token: 0x040001FB RID: 507
		private const int NRN_RE_IPM_DISCARDED = 0;

		// Token: 0x040001FC RID: 508
		private const int NRN_RE_IPM_AUTO_FORWARDED = 1;

		// Token: 0x040001FD RID: 509
		private const int DIS_RE_NO_DISCARD = -1;

		// Token: 0x040001FE RID: 510
		private const int DIS_RE_IPM_EXPIRED = 0;

		// Token: 0x040001FF RID: 511
		private const int DIS_RE_IPM_OBSOLETED = 1;

		// Token: 0x04000200 RID: 512
		private const int DIS_RE_USER_TERMINATED = 2;

		// Token: 0x04000201 RID: 513
		private static Hookable<MapiMessage.GenerateReadReportDelegate> hookableGenerateReadReport;

		// Token: 0x04000202 RID: 514
		private static SecurityIdentifier localSystemSID = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);

		// Token: 0x04000203 RID: 515
		private static Hookable<Func<ErrorCode>> hookableCreateTargetMessageForOutlook = Hookable<Func<ErrorCode>>.Create(true, null);

		// Token: 0x04000204 RID: 516
		private static StorePropTag[] deliverForbiddenProps = new StorePropTag[]
		{
			PropTag.Message.RuleMsgVersion,
			PropTag.Message.RuleMsgState,
			PropTag.Message.RuleMsgUserFlags,
			PropTag.Message.RuleMsgProvider,
			PropTag.Message.RuleMsgName,
			PropTag.Message.RuleMsgLevel,
			PropTag.Message.RuleMsgProviderData,
			PropTag.Message.RuleMsgActions,
			PropTag.Message.RuleMsgCondition,
			PropTag.Message.RuleMsgConditionLCID,
			PropTag.Message.RuleMsgSequence,
			PropTag.Message.ExtendedRuleCondition,
			PropTag.Message.ExtendedRuleActions
		};

		// Token: 0x04000205 RID: 517
		private static StorePropTag[] originatorProps = new StorePropTag[]
		{
			PropTag.Message.OriginatorEntryId,
			PropTag.Message.OriginatorAddressType,
			PropTag.Message.OriginatorName,
			PropTag.Message.OriginatorEmailAddress,
			PropTag.Message.OriginatorSimpleDisplayName,
			PropTag.Message.OriginatorFlags
		};

		// Token: 0x04000206 RID: 518
		private static StorePropTag[] senderPropsRemove = new StorePropTag[]
		{
			PropTag.Message.SenderEntryId,
			PropTag.Message.SenderAddressType,
			PropTag.Message.SenderName,
			PropTag.Message.SenderEmailAddress,
			PropTag.Message.SenderFlags,
			PropTag.Message.SenderSimpleDisplayName,
			PropTag.Message.SenderOrgAddressType,
			PropTag.Message.SenderOrgEmailAddr,
			PropTag.Message.SenderSID,
			PropTag.Message.SenderGuid
		};

		// Token: 0x04000207 RID: 519
		private static StorePropTag[] senderPropsSetAI = new StorePropTag[]
		{
			PropTag.Message.SenderEntryId,
			PropTag.Message.SenderAddressType,
			PropTag.Message.SenderEmailAddress,
			PropTag.Message.SenderName,
			PropTag.Message.SenderSimpleDisplayName,
			PropTag.Message.SenderFlags,
			PropTag.Message.SenderSID,
			PropTag.Message.SenderGuid
		};

		// Token: 0x04000208 RID: 520
		private static StorePropTag[] copyPropList = new StorePropTag[]
		{
			PropTag.Message.ConversationIndex,
			PropTag.Message.ConversationKey,
			PropTag.Message.ConversationTopic,
			PropTag.Message.Importance,
			PropTag.Message.InternetCPID,
			PropTag.Message.MessageCodePage,
			PropTag.Message.MessageLocaleId,
			PropTag.Message.NormalizedSubject,
			PropTag.Message.Priority,
			PropTag.Message.ReportTag
		};

		// Token: 0x04000209 RID: 521
		private static KeyValuePair<StorePropTag, StorePropTag>[] copyRemapPropList = new KeyValuePair<StorePropTag, StorePropTag>[]
		{
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.ClientSubmitTime, PropTag.Message.OriginalSubmitTime),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.DisplayBcc, PropTag.Message.OriginalDisplayBcc),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.DisplayCc, PropTag.Message.OriginalDisplayCc),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.DisplayTo, PropTag.Message.OriginalDisplayTo),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.InternetMessageId, PropTag.Message.OriginalInternetMessageId),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.MessageDeliveryTime, PropTag.Message.OriginalDeliveryTime),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.OriginallyIntendedRecipientEntryId, PropTag.Message.OriginallyIntendedRecipientName),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.ReceivedRepresentingGuid, PropTag.Message.SentRepresentingGuid),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.ReceivedRepresentingName, PropTag.Message.SentRepresentingName),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.SearchKey, PropTag.Message.OriginalSearchKey),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.SearchKey, PropTag.Message.ParentKey),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.SenderAddressType, PropTag.Message.OriginalSenderAddressType),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.SenderEmailAddress, PropTag.Message.OriginalSenderEmailAddress),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.SenderEntryId, PropTag.Message.OriginalSenderEntryId),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.SenderName, PropTag.Message.OriginalSenderName),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.SentRepresentingAddressType, PropTag.Message.OriginalSentRepresentingAddressType),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.SentRepresentingEmailAddress, PropTag.Message.OriginalSentRepresentingEmailAddress),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.SentRepresentingEntryId, PropTag.Message.OriginalSentRepresentingEntryId),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.SentRepresentingName, PropTag.Message.OriginalSentRepresentingName),
			new KeyValuePair<StorePropTag, StorePropTag>(PropTag.Message.Subject, PropTag.Message.OriginalSubject)
		};

		// Token: 0x0400020A RID: 522
		private static StorePropTag[][] originalSenderOptions = new StorePropTag[][]
		{
			new StorePropTag[]
			{
				PropTag.Message.ReadReceiptEntryId,
				PropTag.Message.ReadReceiptDisplayName,
				PropTag.Message.ReadReceiptEmailAddress,
				PropTag.Message.ReadReceiptAddressType
			},
			new StorePropTag[]
			{
				PropTag.Message.SenderEntryId,
				PropTag.Message.SenderName,
				PropTag.Message.SenderEmailAddress,
				PropTag.Message.SenderAddressType
			}
		};

		// Token: 0x0400020B RID: 523
		private static KeyValuePair<StorePropTag, AutoResponseSuppress>[] autoResponseSuppressionProperties = new KeyValuePair<StorePropTag, AutoResponseSuppress>[]
		{
			new KeyValuePair<StorePropTag, AutoResponseSuppress>(PropTag.Message.DeliveryReportRequested, AutoResponseSuppress.DeliveryReceipt),
			new KeyValuePair<StorePropTag, AutoResponseSuppress>(PropTag.Message.NonDeliveryReportRequested, AutoResponseSuppress.NonDeliveryReceipt),
			new KeyValuePair<StorePropTag, AutoResponseSuppress>(PropTag.Message.ReadReceiptRequested, AutoResponseSuppress.ReadNotification),
			new KeyValuePair<StorePropTag, AutoResponseSuppress>(PropTag.Message.NonReceiptNotificationRequested, AutoResponseSuppress.NotReadNotification)
		};

		// Token: 0x0400020C RID: 524
		private static string[] forbiddenDeliveryClasses = new string[]
		{
			"IPM.Rule.Message",
			"IPM.Rule.Version2.Message",
			"IPM.ExtendedRule.Message",
			"IPM.Configuration.ELC"
		};

		// Token: 0x0400020D RID: 525
		private static string[] noSetLcidSendClasses = new string[]
		{
			"IPM.Note.StorageQuotaWarning.Warning",
			"IPM.Note.StorageQuotaWarning.Send",
			"IPM.Note.StorageQuotaWarning.SendReceive",
			"IPM.Conflict.Resolution.Folder",
			"IPM.Conflict.Resolution.Message"
		};

		// Token: 0x0400020E RID: 526
		private static short? rtfBodyFormat = new short?(2);

		// Token: 0x0400020F RID: 527
		private static short? htmlBodyFormat = new short?(3);

		// Token: 0x04000210 RID: 528
		private static short? textBodyFormat = new short?(1);

		// Token: 0x04000211 RID: 529
		internal static FolderSecurity.ExchangeSecurityDescriptorFolderRights ReadOnlyRights = FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty;

		// Token: 0x04000212 RID: 530
		internal static FolderSecurity.ExchangeSecurityDescriptorFolderRights WriteRights = FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody | FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty | FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteAttributes;

		// Token: 0x04000213 RID: 531
		private bool assignedInternetMessageId;

		// Token: 0x04000214 RID: 532
		private bool streamSizeInvalid;

		// Token: 0x04000215 RID: 533
		private ExchangeId fid;

		// Token: 0x04000216 RID: 534
		private ExchangeId mid;

		// Token: 0x04000217 RID: 535
		private Message storeMessage;

		// Token: 0x04000218 RID: 536
		private MapiPersonCollection people;

		// Token: 0x04000219 RID: 537
		private CodePage codePage = CodePage.None;

		// Token: 0x0400021A RID: 538
		private MapiMessage.IcsImportState icsImport;

		// Token: 0x0400021B RID: 539
		private AccessCheckState accessCheckState;

		// Token: 0x0400021C RID: 540
		private bool scrubbed;

		// Token: 0x0400021D RID: 541
		private bool setPropsCalled;

		// Token: 0x0400021E RID: 542
		private bool? checkStreamSize;

		// Token: 0x0400021F RID: 543
		private bool readOnly;

		// Token: 0x04000220 RID: 544
		private bool isReportMessage;

		// Token: 0x04000221 RID: 545
		private bool attachmentTableExists;

		// Token: 0x0200006C RID: 108
		// (Invoke) Token: 0x06000353 RID: 851
		internal delegate ErrorCode GenerateReadReportDelegate(MapiContext context, MapiLogon mapiLogon, Message storeMessage, bool notRead, bool messageExpired);

		// Token: 0x0200006D RID: 109
		private class IcsImportState
		{
			// Token: 0x06000356 RID: 854 RVA: 0x0001B69C File Offset: 0x0001989C
			public IcsImportState(PCL pcl, byte[] changeKey, DateTime lastModificationTime, IdSet cnsetToUpdate, bool conflictingChange, bool conflictWinnerNew, bool createConflictMessage, bool failOnConflict)
			{
				this.pcl = pcl;
				this.changeKey = changeKey;
				this.lastModificationTime = lastModificationTime;
				this.cnsetToUpdate = cnsetToUpdate;
				this.conflictingChange = conflictingChange;
				this.conflictWinnerNew = conflictWinnerNew;
				this.createConflictMessage = createConflictMessage;
				this.failOnConflict = failOnConflict;
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x06000357 RID: 855 RVA: 0x0001B6EC File Offset: 0x000198EC
			public PCL Pcl
			{
				get
				{
					return this.pcl;
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000358 RID: 856 RVA: 0x0001B6F4 File Offset: 0x000198F4
			public byte[] ChangeKey
			{
				get
				{
					return this.changeKey;
				}
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000359 RID: 857 RVA: 0x0001B6FC File Offset: 0x000198FC
			public DateTime LastModificationTime
			{
				get
				{
					return this.lastModificationTime;
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x0600035A RID: 858 RVA: 0x0001B704 File Offset: 0x00019904
			public IdSet CnsetToUpdate
			{
				get
				{
					return this.cnsetToUpdate;
				}
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x0600035B RID: 859 RVA: 0x0001B70C File Offset: 0x0001990C
			public bool ConflictingChange
			{
				get
				{
					return this.conflictingChange;
				}
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x0600035C RID: 860 RVA: 0x0001B714 File Offset: 0x00019914
			public bool ConflictWinnerNew
			{
				get
				{
					return this.conflictWinnerNew;
				}
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x0600035D RID: 861 RVA: 0x0001B71C File Offset: 0x0001991C
			public bool CreateConflictMessage
			{
				get
				{
					return this.createConflictMessage;
				}
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x0600035E RID: 862 RVA: 0x0001B724 File Offset: 0x00019924
			public bool FailOnConflict
			{
				get
				{
					return this.failOnConflict;
				}
			}

			// Token: 0x04000223 RID: 547
			private PCL pcl;

			// Token: 0x04000224 RID: 548
			private byte[] changeKey;

			// Token: 0x04000225 RID: 549
			private DateTime lastModificationTime;

			// Token: 0x04000226 RID: 550
			private IdSet cnsetToUpdate;

			// Token: 0x04000227 RID: 551
			private bool conflictingChange;

			// Token: 0x04000228 RID: 552
			private bool conflictWinnerNew;

			// Token: 0x04000229 RID: 553
			private bool createConflictMessage;

			// Token: 0x0400022A RID: 554
			private bool failOnConflict;
		}
	}
}
