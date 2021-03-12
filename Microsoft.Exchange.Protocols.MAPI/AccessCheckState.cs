using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000003 RID: 3
	internal class AccessCheckState
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AccessCheckState(MapiContext context, Folder folder)
		{
			this.folder = folder;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DF File Offset: 0x000002DF
		public AccessCheckState(MapiContext context, SecurityDescriptor folderSecurityDescriptor)
		{
			this.folderSecurityDescriptor = folderSecurityDescriptor;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F0 File Offset: 0x000002F0
		public bool CheckContextRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights)
		{
			FolderSecurity.ExchangeSecurityDescriptorFolderRights contextRights = this.GetContextRights(context, false);
			if (AccessCheckState.CheckRights(contextRights, requestedRights, true))
			{
				return true;
			}
			this.TraceFolderAccessDenied(context, requestedRights, true);
			return false;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000211C File Offset: 0x0000031C
		public bool CheckFolderRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, bool allRights)
		{
			FolderSecurity.ExchangeSecurityDescriptorFolderRights exchangeSecurityDescriptorFolderRights = this.GetContextRights(context, false);
			if (AccessCheckState.CheckRights(exchangeSecurityDescriptorFolderRights, requestedRights, allRights))
			{
				return true;
			}
			exchangeSecurityDescriptorFolderRights |= this.GetFolderRights(context);
			if (AccessCheckState.CheckRights(exchangeSecurityDescriptorFolderRights, requestedRights, allRights))
			{
				return true;
			}
			this.TraceFolderAccessDenied(context, requestedRights, allRights);
			return false;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002160 File Offset: 0x00000360
		public bool CheckMessageRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, bool allRights, AccessCheckState.CreatorSecurityIdentifierAccessor messageCreatorAccessor)
		{
			if (context.HasMailboxFullRights)
			{
				return true;
			}
			FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights2 = requestedRights;
			SecurityIdentifier securityIdentifier = messageCreatorAccessor.GetSecurityIdentifier(context);
			bool flag = this.IsOwner(context, securityIdentifier);
			SecurityIdentifier securityIdentifier2 = null;
			if (!flag && context.LockedMailboxState.MailboxTypeDetail == MailboxInfo.MailboxTypeDetail.GroupMailbox)
			{
				securityIdentifier2 = messageCreatorAccessor.GetConversationSecurityIdentifier(context);
				flag = this.IsOwner(context, securityIdentifier2);
			}
			if (flag)
			{
				if ((requestedRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.Delete) != FolderSecurity.ExchangeSecurityDescriptorFolderRights.None)
				{
					requestedRights &= ~FolderSecurity.ExchangeSecurityDescriptorFolderRights.Delete;
					requestedRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.DeleteOwnItem;
				}
				if ((requestedRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteAnyAccess) != FolderSecurity.ExchangeSecurityDescriptorFolderRights.None)
				{
					requestedRights &= ~(FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody | FolderSecurity.ExchangeSecurityDescriptorFolderRights.AppendMsg | FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty | FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteAttributes | FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteSD | FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteOwner);
					requestedRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteOwnProperty;
				}
				requestedRights &= ~FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty;
				if (requestedRights == FolderSecurity.ExchangeSecurityDescriptorFolderRights.None)
				{
					return true;
				}
			}
			FolderSecurity.ExchangeSecurityDescriptorFolderRights exchangeSecurityDescriptorFolderRights = this.GetContextRights(context, true);
			if (AccessCheckState.CheckRights(exchangeSecurityDescriptorFolderRights, requestedRights, allRights))
			{
				return true;
			}
			exchangeSecurityDescriptorFolderRights |= this.GetMessageRights(context, securityIdentifier);
			if (AccessCheckState.CheckRights(exchangeSecurityDescriptorFolderRights, requestedRights, allRights))
			{
				return true;
			}
			this.TraceMessageAccessDenied(context, requestedRights2, allRights, securityIdentifier, securityIdentifier2);
			return false;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002231 File Offset: 0x00000431
		public FolderSecurity.ExchangeSecurityDescriptorFolderRights GetFolderRights(MapiContext context)
		{
			this.LoadSecurityDescriptorAndCheckAccess(context, false, null);
			return this.folderAccessRights.Value;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002247 File Offset: 0x00000447
		public FolderSecurity.ExchangeSecurityDescriptorFolderRights GetMessageRights(MapiContext context, SecurityIdentifier messageOwner)
		{
			this.LoadSecurityDescriptorAndCheckAccess(context, true, messageOwner);
			return this.messageAccessRights.Value;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000225D File Offset: 0x0000045D
		public FolderSecurity.ExchangeSecurityDescriptorFolderRights GetContextRights(MapiContext context, bool forMessage)
		{
			if (!context.HasMailboxFullRights)
			{
				return FolderSecurity.ExchangeSecurityDescriptorFolderRights.None;
			}
			if (!forMessage)
			{
				return FolderSecurity.ExchangeSecurityDescriptorFolderRights.AllFolder;
			}
			return FolderSecurity.ExchangeSecurityDescriptorFolderRights.AllMessage;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002277 File Offset: 0x00000477
		private static bool CheckRights(FolderSecurity.ExchangeSecurityDescriptorFolderRights current, FolderSecurity.ExchangeSecurityDescriptorFolderRights requested, bool allRights)
		{
			return (current & requested) == requested || ((current & requested) != FolderSecurity.ExchangeSecurityDescriptorFolderRights.None && !allRights);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000228C File Offset: 0x0000048C
		private void LoadSecurityDescriptorAndCheckAccess(MapiContext context, bool forMessage, SecurityIdentifier messageOwner)
		{
			if (this.folder != null)
			{
				object aclTableVersionCookie = this.folder.AclTableVersionCookie;
				if (!object.ReferenceEquals(this.lastSeenAclTableVersionCookie, aclTableVersionCookie))
				{
					this.folderSecurityDescriptor = null;
					this.messageSecurityDescriptor = null;
					this.messageSecurityDescriptorOwner = null;
					this.folderAccessRights = null;
					this.messageAccessRights = null;
					this.lastSeenAclTableVersionCookie = aclTableVersionCookie;
				}
			}
			if (this.folderAccessRights == null)
			{
				if (this.folder != null)
				{
					this.folderSecurityDescriptor = this.folder.GetSecurityDescriptor(context);
				}
				this.folderAccessRights = new FolderSecurity.ExchangeSecurityDescriptorFolderRights?(this.GetRights(context, false));
				if (ExTraceGlobals.AccessCheckTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.AccessCheckTracer.TraceDebug<FolderSecurity.ExchangeSecurityDescriptorFolderRights?>(0L, "Allowed folder rights={0}", this.folderAccessRights);
				}
			}
			if (forMessage && (this.messageAccessRights == null || this.messageSecurityDescriptorOwner != messageOwner))
			{
				RawSecurityDescriptor creatorDescriptor = new RawSecurityDescriptor(ControlFlags.DiscretionaryAclPresent, context.Session.CurrentSecurityContext.UserSid, context.Session.CurrentSecurityContext.PrimaryGroupSid, null, new RawAcl(2, 0));
				NativeMethods.AutoInheritFlags autoInheritFlags = NativeMethods.AutoInheritFlags.AutoInheritDACL | NativeMethods.AutoInheritFlags.AutoInheritSACL | NativeMethods.AutoInheritFlags.AvoidOwnerCheck | NativeMethods.AutoInheritFlags.DefaultOwnerFromParent | NativeMethods.AutoInheritFlags.DefaultGroupFromParent;
				RawSecurityDescriptor rawSecurityDescriptor = SecurityHelper.CreateRawSecurityDescriptor(this.folderSecurityDescriptor);
				if (messageOwner != null)
				{
					rawSecurityDescriptor.Owner = messageOwner;
				}
				RawSecurityDescriptor rawSecurityDescriptor2;
				if (!NativeMethods.CreatePrivateObjectSecurityEx(rawSecurityDescriptor, creatorDescriptor, out rawSecurityDescriptor2, Guid.Empty, false, (uint)autoInheritFlags, null, FolderSecurity.GenericMapping))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (ExTraceGlobals.AccessCheckTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.AccessCheckTracer.TraceError<int, string>(0L, "Failed to create message SD (Error={0}): Folder SD={1}", lastWin32Error, (this.folderSecurityDescriptor != null) ? SecurityHelper.GetSDDL(this.folderSecurityDescriptor) : string.Empty);
					}
					throw new StoreException((LID)50055U, (ErrorCodeValue)lastWin32Error, "CreatePrivateObjectSecurityEx failed");
				}
				this.messageSecurityDescriptor = SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor2);
				this.messageSecurityDescriptorOwner = messageOwner;
				this.messageAccessRights = new FolderSecurity.ExchangeSecurityDescriptorFolderRights?(this.GetRights(context, true));
				if (ExTraceGlobals.AccessCheckTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, FolderSecurity.ExchangeSecurityDescriptorFolderRights?>(0L, "Message SD created: Folder SD={0}, Message SD={1}, Allowed message rights={2}", (this.folderSecurityDescriptor != null) ? SecurityHelper.GetSDDL(this.folderSecurityDescriptor) : string.Empty, SecurityHelper.GetSDDL(this.messageSecurityDescriptor), this.messageAccessRights);
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000024A4 File Offset: 0x000006A4
		private FolderSecurity.ExchangeSecurityDescriptorFolderRights GetRights(MapiContext context, bool forMessage)
		{
			SecurityDescriptor securityDescriptor = forMessage ? this.messageSecurityDescriptor : this.folderSecurityDescriptor;
			if (securityDescriptor == null)
			{
				DiagnosticContext.TraceLocation((LID)48263U);
				return FolderSecurity.ExchangeSecurityDescriptorFolderRights.None;
			}
			FolderSecurity.ExchangeSecurityDescriptorFolderRights grantedAccess;
			try
			{
				grantedAccess = (FolderSecurity.ExchangeSecurityDescriptorFolderRights)context.SecurityContext.GetGrantedAccess(securityDescriptor, AccessMask.MaximumAllowed);
			}
			catch (AuthzException ex)
			{
				context.OnExceptionCatch(ex);
				throw new ExExceptionAccessDenied((LID)40071U, "Failed to check access", ex);
			}
			return grantedAccess;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000251C File Offset: 0x0000071C
		private void TraceFolderAccessDenied(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, bool allRights)
		{
			if (ExTraceGlobals.AccessCheckTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug(0L, "Folder access denied: User SID={0}, Requested rights({1})={2}, Allowed folder rights={3}, Allowed folder logon rights={4}, Folder SD={5}", new object[]
				{
					context.SecurityContext.UserSid,
					allRights ? "all" : "any",
					requestedRights,
					this.folderAccessRights.Value,
					this.GetContextRights(context, false),
					(this.folderSecurityDescriptor != null) ? SecurityHelper.GetSDDL(this.folderSecurityDescriptor) : string.Empty
				});
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025BC File Offset: 0x000007BC
		private void TraceMessageAccessDenied(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, bool allRights, SecurityIdentifier messageOwner, SecurityIdentifier conversationOwner)
		{
			if (ExTraceGlobals.AccessCheckTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug(0L, "Message access denied: User SID={0}, Requested rights({1})={2}, Message owner={3}, Conversation owner={4}, Allowed message rights={5}, Allowed message logon rights={6}, Message SD={7}", new object[]
				{
					context.SecurityContext.UserSid,
					allRights ? "all" : "any",
					requestedRights,
					messageOwner,
					(conversationOwner != null) ? conversationOwner.ToString() : "<none>",
					this.messageAccessRights.Value,
					this.GetContextRights(context, true),
					(this.messageSecurityDescriptor != null) ? SecurityHelper.GetSDDL(this.messageSecurityDescriptor) : string.Empty
				});
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000267C File Offset: 0x0000087C
		private bool IsOwner(MapiContext context, SecurityIdentifier securityIdentifier)
		{
			if (securityIdentifier == null)
			{
				return false;
			}
			if (context.SecurityContext.UserSid == securityIdentifier)
			{
				return true;
			}
			IdentityReferenceCollection groups;
			try
			{
				groups = context.SecurityContext.GetGroups();
			}
			catch (AuthzException ex)
			{
				context.OnExceptionCatch(ex);
				throw new ExExceptionAccessDenied((LID)37800U, "Failed to get token groups", ex);
			}
			if (groups == null)
			{
				DiagnosticContext.TraceLocation((LID)54184U);
				return false;
			}
			foreach (IdentityReference identityReference in groups)
			{
				SecurityIdentifier left = identityReference as SecurityIdentifier;
				if (left != null && left == securityIdentifier)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400002F RID: 47
		private readonly Folder folder;

		// Token: 0x04000030 RID: 48
		private SecurityDescriptor folderSecurityDescriptor;

		// Token: 0x04000031 RID: 49
		private SecurityDescriptor messageSecurityDescriptor;

		// Token: 0x04000032 RID: 50
		private SecurityIdentifier messageSecurityDescriptorOwner;

		// Token: 0x04000033 RID: 51
		private FolderSecurity.ExchangeSecurityDescriptorFolderRights? folderAccessRights;

		// Token: 0x04000034 RID: 52
		private FolderSecurity.ExchangeSecurityDescriptorFolderRights? messageAccessRights;

		// Token: 0x04000035 RID: 53
		private object lastSeenAclTableVersionCookie;

		// Token: 0x02000004 RID: 4
		internal struct CreatorSecurityIdentifierAccessor
		{
			// Token: 0x0600000F RID: 15 RVA: 0x00002750 File Offset: 0x00000950
			internal CreatorSecurityIdentifierAccessor(SecurityIdentifier messageCreatorSecurityIdentifier, SecurityIdentifier conversationCreatorSecurityIdentifier)
			{
				this.messageCreatorSecurityIdentifier = messageCreatorSecurityIdentifier;
				this.conversationCreatorSecurityIdentifier = conversationCreatorSecurityIdentifier;
				this.topMessage = null;
			}

			// Token: 0x06000010 RID: 16 RVA: 0x00002767 File Offset: 0x00000967
			internal CreatorSecurityIdentifierAccessor(TopMessage topMessage)
			{
				this.messageCreatorSecurityIdentifier = null;
				this.conversationCreatorSecurityIdentifier = null;
				this.topMessage = topMessage;
			}

			// Token: 0x06000011 RID: 17 RVA: 0x0000277E File Offset: 0x0000097E
			internal SecurityIdentifier GetSecurityIdentifier(MapiContext context)
			{
				if (this.messageCreatorSecurityIdentifier != null)
				{
					return this.messageCreatorSecurityIdentifier;
				}
				if (this.topMessage != null)
				{
					return this.topMessage.GetCreatorSecurityIdentifier(context);
				}
				return null;
			}

			// Token: 0x06000012 RID: 18 RVA: 0x000027AB File Offset: 0x000009AB
			internal SecurityIdentifier GetConversationSecurityIdentifier(MapiContext context)
			{
				if (this.conversationCreatorSecurityIdentifier != null)
				{
					return this.conversationCreatorSecurityIdentifier;
				}
				if (this.topMessage != null)
				{
					return this.topMessage.GetConversationCreatorSecurityIdentifier(context);
				}
				return null;
			}

			// Token: 0x04000036 RID: 54
			private readonly TopMessage topMessage;

			// Token: 0x04000037 RID: 55
			private readonly SecurityIdentifier messageCreatorSecurityIdentifier;

			// Token: 0x04000038 RID: 56
			private readonly SecurityIdentifier conversationCreatorSecurityIdentifier;
		}
	}
}
