﻿using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000009 RID: 9
	internal sealed class DatabaseInfo
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003674 File Offset: 0x00001874
		public DatabaseInfo(SecurityDescriptor databaseSecurityDescriptor)
		{
			ArgumentValidator.ThrowIfNull("databaseSecurityDescriptor", databaseSecurityDescriptor);
			RawSecurityDescriptor databaseSD = databaseSecurityDescriptor.ToRawSecurityDescriptor();
			this.parentSDForMailboxes = DatabaseInfo.TransformToMailboxSD(databaseSD);
			if (ExTraceGlobals.AccessControlTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AccessControlTracer.TraceDebug<string>(0, Activity.TraceId, "Database security descriptor that will be used for mailboxes:\r\n{0}", this.parentSDForMailboxes.GetSddlForm(AccessControlSections.Access));
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000036D3 File Offset: 0x000018D3
		public RawSecurityDescriptor InheritMailboxSecurityDescriptor(RawSecurityDescriptor mailboxSecurityDescriptor)
		{
			return DatabaseInfo.InheritSecurityDescriptor(this.parentSDForMailboxes, mailboxSecurityDescriptor);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000036E4 File Offset: 0x000018E4
		private static RawSecurityDescriptor TransformToMailboxSD(RawSecurityDescriptor databaseSD)
		{
			RawAcl rawAcl = new RawAcl(databaseSD.DiscretionaryAcl.Revision, databaseSD.DiscretionaryAcl.BinaryLength);
			foreach (GenericAce genericAce in databaseSD.DiscretionaryAcl)
			{
				QualifiedAce qualifiedAce = genericAce as QualifiedAce;
				if (!(qualifiedAce == null))
				{
					int num = qualifiedAce.AccessMask & 2031616;
					ObjectAce objectAce = genericAce as ObjectAce;
					if (objectAce != null && objectAce.ObjectAceType == MailboxRights.ReceiveAsExtendedRightGuid)
					{
						num |= 1;
					}
					if (objectAce == null || (objectAce.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None || objectAce.InheritedObjectAceType == MailboxRights.UserObjectType)
					{
						AceFlags flags = (AceFlags)((qualifiedAce.IsInherited ? 2 : 0) | (byte)(qualifiedAce.AceFlags & (AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.NoPropagateInherit | AceFlags.InheritOnly | AceFlags.Inherited)));
						rawAcl.InsertAce(rawAcl.Count, new CommonAce(flags, qualifiedAce.AceQualifier, num, qualifiedAce.SecurityIdentifier, qualifiedAce.IsCallback, qualifiedAce.GetOpaque()));
					}
				}
			}
			return new RawSecurityDescriptor(ControlFlags.DiscretionaryAclPresent | ControlFlags.SystemAclPresent, databaseSD.Owner, databaseSD.Group, databaseSD.SystemAcl, rawAcl);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003804 File Offset: 0x00001A04
		private static RawSecurityDescriptor InheritSecurityDescriptor(RawSecurityDescriptor parent, RawSecurityDescriptor child)
		{
			RawSecurityDescriptor rawSecurityDescriptor;
			if (NativeMethods.CreatePrivateObjectSecurityEx(parent, child, out rawSecurityDescriptor, Guid.Empty, true, 123U, null, MailboxRights.GenericRightsMapping))
			{
				if (ExTraceGlobals.AccessControlTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.AccessControlTracer.TraceDebug<string, string, string>(0, Activity.TraceId, "Inherited a mailbox SD from a database SD.\r\nDatabase: {0}\r\nMailbox: {1}\r\nMerged: {2}", parent.GetSddlForm(AccessControlSections.Access), child.GetSddlForm(AccessControlSections.Access), rawSecurityDescriptor.GetSddlForm(AccessControlSections.Access));
				}
				return rawSecurityDescriptor;
			}
			throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
		}

		// Token: 0x0400003A RID: 58
		private readonly RawSecurityDescriptor parentSDForMailboxes;
	}
}
