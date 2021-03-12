using System;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000ACA RID: 2762
	public static class MailboxSecurity
	{
		// Token: 0x06003B33 RID: 15155 RVA: 0x000984D4 File Offset: 0x000966D4
		public static SecurityDescriptor CreateMailboxSecurityDescriptor(SecurityDescriptor databaseOrServerADSecurityDescriptor, SecurityDescriptor mailboxADSecurityDescriptor)
		{
			if (databaseOrServerADSecurityDescriptor == null)
			{
				throw new ArgumentNullException("databaseOrServerADSecurityDescriptor");
			}
			RawSecurityDescriptor rawSecurityDescriptor = databaseOrServerADSecurityDescriptor.ToRawSecurityDescriptor();
			DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(true, true, rawSecurityDescriptor.DiscretionaryAcl.Count);
			foreach (GenericAce genericAce in rawSecurityDescriptor.DiscretionaryAcl)
			{
				KnownAce knownAce = (KnownAce)genericAce;
				uint accessMask = (uint)knownAce.AccessMask;
				uint num = accessMask & 2031616U;
				Guid b = Guid.Empty;
				Guid b2 = Guid.Empty;
				bool flag = knownAce.AceType == AceType.AccessAllowedObject || knownAce.AceType == AceType.AccessDeniedObject;
				if (flag)
				{
					ObjectAce objectAce = knownAce as ObjectAce;
					b = objectAce.ObjectAceType;
					b2 = objectAce.InheritedObjectAceType;
				}
				if (!flag && (accessMask & 256U) != 0U)
				{
					num |= 1U;
				}
				if (!b.Equals(Guid.Empty) && (accessMask & 256U) != 0U && MailboxSecurity.GetObjectRightsGuid(MailboxSecurity.ObjectRights.ReceiveAs) == b)
				{
					num |= 1U;
				}
				if (num != 0U && (b2.Equals(Guid.Empty) || MailboxSecurity.GetObjectRightsGuid(MailboxSecurity.ObjectRights.User) == b2))
				{
					switch (knownAce.AceType)
					{
					case AceType.AccessAllowed:
					case AceType.AccessDenied:
					{
						KnownAce knownAce2 = knownAce;
						discretionaryAcl.AddAccess((knownAce2.AceType == AceType.AccessAllowed) ? AccessControlType.Allow : AccessControlType.Deny, knownAce2.SecurityIdentifier, (int)num, knownAce2.InheritanceFlags, knownAce2.PropagationFlags);
						break;
					}
					case AceType.AccessAllowedObject:
					case AceType.AccessDeniedObject:
					{
						ObjectAce objectAce2 = (ObjectAce)knownAce;
						if (objectAce2.IsInherited)
						{
							ObjectAce objectAce3 = objectAce2;
							objectAce3.AceFlags |= AceFlags.ContainerInherit;
						}
						discretionaryAcl.AddAccess((objectAce2.AceType == AceType.AccessAllowedObject) ? AccessControlType.Allow : AccessControlType.Deny, objectAce2.SecurityIdentifier, (int)num, objectAce2.InheritanceFlags, objectAce2.PropagationFlags);
						break;
					}
					}
				}
			}
			byte[] binaryForm = new byte[discretionaryAcl.BinaryLength];
			discretionaryAcl.GetBinaryForm(binaryForm, 0);
			RawAcl discretionaryAcl2 = new RawAcl(binaryForm, 0);
			RawSecurityDescriptor rawSecurityDescriptor2 = new RawSecurityDescriptor(rawSecurityDescriptor.ControlFlags, rawSecurityDescriptor.Owner, rawSecurityDescriptor.Group, rawSecurityDescriptor.SystemAcl, discretionaryAcl2);
			byte[] array = new byte[rawSecurityDescriptor2.BinaryLength];
			rawSecurityDescriptor2.GetBinaryForm(array, 0);
			byte[] binaryForm2 = null;
			if (!NativeMethods.CreatePrivateObjectSecurityEx(array, (mailboxADSecurityDescriptor != null) ? mailboxADSecurityDescriptor.BinaryForm : null, out binaryForm2, Guid.Empty, true, 123U, null, MailboxSecurity.GenericMappingMailbox))
			{
				return null;
			}
			return new SecurityDescriptor(binaryForm2);
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x00098725 File Offset: 0x00096925
		internal static NativeMethods.GENERIC_MAPPING GenericMappingMailbox
		{
			get
			{
				return MailboxSecurity.genericMappingMailbox;
			}
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x0009872C File Offset: 0x0009692C
		internal static bool IsKnownRights(Guid rights)
		{
			return MailboxSecurity.objectRights.Contains(rights);
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x00098739 File Offset: 0x00096939
		internal static Guid GetObjectRightsGuid(MailboxSecurity.ObjectRights type)
		{
			return MailboxSecurity.objectRights[(int)type];
		}

		// Token: 0x040033F4 RID: 13300
		private static readonly NativeMethods.GENERIC_MAPPING genericMappingMailbox = new NativeMethods.GENERIC_MAPPING
		{
			GenericRead = 131072U,
			GenericWrite = 196608U,
			GenericExecute = 131072U,
			GenericAll = 2031619U
		};

		// Token: 0x040033F5 RID: 13301
		private static Guid[] objectRights = new Guid[]
		{
			new Guid("ab721a56-1e2f-11d0-9819-00aa0040529b"),
			new Guid("ab721a54-1e2f-11d0-9819-00aa0040529b"),
			new Guid("bf967aba-0de6-11d0-a285-00aa003049e2"),
			new Guid("9fbec2a1-f761-11d9-963d-00065bbd3175"),
			new Guid("9fbec2a2-f761-11d9-963d-00065bbd3175"),
			new Guid("d74a8762-22b9-11d3-aa62-00c04f8eedd8")
		};

		// Token: 0x02000ACB RID: 2763
		public enum MailboxAccessMask
		{
			// Token: 0x040033F7 RID: 13303
			MailboxOwner = 1,
			// Token: 0x040033F8 RID: 13304
			SendAs
		}

		// Token: 0x02000ACC RID: 2764
		public enum ObjectRights
		{
			// Token: 0x040033FA RID: 13306
			ReceiveAs,
			// Token: 0x040033FB RID: 13307
			SendAs,
			// Token: 0x040033FC RID: 13308
			User,
			// Token: 0x040033FD RID: 13309
			ConstrainedDelegation,
			// Token: 0x040033FE RID: 13310
			TransportAccess,
			// Token: 0x040033FF RID: 13311
			AdminFullAccess,
			// Token: 0x04003400 RID: 13312
			NumberOfSupportedObjectRights
		}
	}
}
