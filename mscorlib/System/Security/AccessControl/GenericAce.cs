using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000200 RID: 512
	public abstract class GenericAce
	{
		// Token: 0x06001E41 RID: 7745 RVA: 0x00069AE4 File Offset: 0x00067CE4
		internal void MarshalHeader(byte[] binaryForm, int offset)
		{
			int binaryLength = this.BinaryLength;
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (binaryForm.Length - offset < this.BinaryLength)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
			if (binaryLength > 65535)
			{
				throw new SystemException();
			}
			binaryForm[offset] = (byte)this.AceType;
			binaryForm[offset + 1] = (byte)this.AceFlags;
			binaryForm[offset + 2] = (byte)binaryLength;
			binaryForm[offset + 3] = (byte)(binaryLength >> 8);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00069B73 File Offset: 0x00067D73
		internal GenericAce(AceType type, AceFlags flags)
		{
			this._type = type;
			this._flags = flags;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00069B8C File Offset: 0x00067D8C
		internal static AceFlags AceFlagsFromAuditFlags(AuditFlags auditFlags)
		{
			AceFlags aceFlags = AceFlags.None;
			if ((auditFlags & AuditFlags.Success) != AuditFlags.None)
			{
				aceFlags |= AceFlags.SuccessfulAccess;
			}
			if ((auditFlags & AuditFlags.Failure) != AuditFlags.None)
			{
				aceFlags |= AceFlags.FailedAccess;
			}
			if (aceFlags == AceFlags.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "auditFlags");
			}
			return aceFlags;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x00069BCC File Offset: 0x00067DCC
		internal static AceFlags AceFlagsFromInheritanceFlags(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			AceFlags aceFlags = AceFlags.None;
			if ((inheritanceFlags & InheritanceFlags.ContainerInherit) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ContainerInherit;
			}
			if ((inheritanceFlags & InheritanceFlags.ObjectInherit) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ObjectInherit;
			}
			if (aceFlags != AceFlags.None)
			{
				if ((propagationFlags & PropagationFlags.NoPropagateInherit) != PropagationFlags.None)
				{
					aceFlags |= AceFlags.NoPropagateInherit;
				}
				if ((propagationFlags & PropagationFlags.InheritOnly) != PropagationFlags.None)
				{
					aceFlags |= AceFlags.InheritOnly;
				}
			}
			return aceFlags;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x00069C04 File Offset: 0x00067E04
		internal static void VerifyHeader(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (binaryForm.Length - offset < 4)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
			if (((int)binaryForm[offset + 3] << 8) + (int)binaryForm[offset + 2] > binaryForm.Length - offset)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x00069C80 File Offset: 0x00067E80
		public static GenericAce CreateFromBinaryForm(byte[] binaryForm, int offset)
		{
			GenericAce.VerifyHeader(binaryForm, offset);
			AceType aceType = (AceType)binaryForm[offset];
			GenericAce genericAce;
			if (aceType == AceType.AccessAllowed || aceType == AceType.AccessDenied || aceType == AceType.SystemAudit || aceType == AceType.SystemAlarm || aceType == AceType.AccessAllowedCallback || aceType == AceType.AccessDeniedCallback || aceType == AceType.SystemAuditCallback || aceType == AceType.SystemAlarmCallback)
			{
				AceQualifier qualifier;
				int accessMask;
				SecurityIdentifier sid;
				bool isCallback;
				byte[] opaque;
				if (!CommonAce.ParseBinaryForm(binaryForm, offset, out qualifier, out accessMask, out sid, out isCallback, out opaque))
				{
					goto IL_1A8;
				}
				AceFlags flags = (AceFlags)binaryForm[offset + 1];
				genericAce = new CommonAce(flags, qualifier, accessMask, sid, isCallback, opaque);
			}
			else if (aceType == AceType.AccessAllowedObject || aceType == AceType.AccessDeniedObject || aceType == AceType.SystemAuditObject || aceType == AceType.SystemAlarmObject || aceType == AceType.AccessAllowedCallbackObject || aceType == AceType.AccessDeniedCallbackObject || aceType == AceType.SystemAuditCallbackObject || aceType == AceType.SystemAlarmCallbackObject)
			{
				AceQualifier qualifier2;
				int accessMask2;
				SecurityIdentifier sid2;
				ObjectAceFlags flags2;
				Guid type;
				Guid inheritedType;
				bool isCallback2;
				byte[] opaque2;
				if (!ObjectAce.ParseBinaryForm(binaryForm, offset, out qualifier2, out accessMask2, out sid2, out flags2, out type, out inheritedType, out isCallback2, out opaque2))
				{
					goto IL_1A8;
				}
				AceFlags aceFlags = (AceFlags)binaryForm[offset + 1];
				genericAce = new ObjectAce(aceFlags, qualifier2, accessMask2, sid2, flags2, type, inheritedType, isCallback2, opaque2);
			}
			else if (aceType == AceType.AccessAllowedCompound)
			{
				int accessMask3;
				CompoundAceType compoundAceType;
				SecurityIdentifier sid3;
				if (!CompoundAce.ParseBinaryForm(binaryForm, offset, out accessMask3, out compoundAceType, out sid3))
				{
					goto IL_1A8;
				}
				AceFlags flags3 = (AceFlags)binaryForm[offset + 1];
				genericAce = new CompoundAce(flags3, accessMask3, compoundAceType, sid3);
			}
			else
			{
				AceFlags flags4 = (AceFlags)binaryForm[offset + 1];
				byte[] array = null;
				int num = (int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8);
				if (num % 4 != 0)
				{
					goto IL_1A8;
				}
				int num2 = num - 4;
				if (num2 > 0)
				{
					array = new byte[num2];
					for (int i = 0; i < num2; i++)
					{
						array[i] = binaryForm[offset + num - num2 + i];
					}
				}
				genericAce = new CustomAce(aceType, flags4, array);
			}
			if ((genericAce is ObjectAce || (int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8) == genericAce.BinaryLength) && (!(genericAce is ObjectAce) || (int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8) == genericAce.BinaryLength || (int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8) - 32 == genericAce.BinaryLength))
			{
				return genericAce;
			}
			IL_1A8:
			throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidAceBinaryForm"), "binaryForm");
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x00069E49 File Offset: 0x00068049
		public AceType AceType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x00069E51 File Offset: 0x00068051
		// (set) Token: 0x06001E49 RID: 7753 RVA: 0x00069E59 File Offset: 0x00068059
		public AceFlags AceFlags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x00069E62 File Offset: 0x00068062
		public bool IsInherited
		{
			get
			{
				return (this.AceFlags & AceFlags.Inherited) > AceFlags.None;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x00069E70 File Offset: 0x00068070
		public InheritanceFlags InheritanceFlags
		{
			get
			{
				InheritanceFlags inheritanceFlags = InheritanceFlags.None;
				if ((this.AceFlags & AceFlags.ContainerInherit) != AceFlags.None)
				{
					inheritanceFlags |= InheritanceFlags.ContainerInherit;
				}
				if ((this.AceFlags & AceFlags.ObjectInherit) != AceFlags.None)
				{
					inheritanceFlags |= InheritanceFlags.ObjectInherit;
				}
				return inheritanceFlags;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x00069E9C File Offset: 0x0006809C
		public PropagationFlags PropagationFlags
		{
			get
			{
				PropagationFlags propagationFlags = PropagationFlags.None;
				if ((this.AceFlags & AceFlags.InheritOnly) != AceFlags.None)
				{
					propagationFlags |= PropagationFlags.InheritOnly;
				}
				if ((this.AceFlags & AceFlags.NoPropagateInherit) != AceFlags.None)
				{
					propagationFlags |= PropagationFlags.NoPropagateInherit;
				}
				return propagationFlags;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x00069EC8 File Offset: 0x000680C8
		public AuditFlags AuditFlags
		{
			get
			{
				AuditFlags auditFlags = AuditFlags.None;
				if ((this.AceFlags & AceFlags.SuccessfulAccess) != AceFlags.None)
				{
					auditFlags |= AuditFlags.Success;
				}
				if ((this.AceFlags & AceFlags.FailedAccess) != AceFlags.None)
				{
					auditFlags |= AuditFlags.Failure;
				}
				return auditFlags;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001E4E RID: 7758
		public abstract int BinaryLength { get; }

		// Token: 0x06001E4F RID: 7759
		public abstract void GetBinaryForm(byte[] binaryForm, int offset);

		// Token: 0x06001E50 RID: 7760 RVA: 0x00069EFC File Offset: 0x000680FC
		public GenericAce Copy()
		{
			byte[] binaryForm = new byte[this.BinaryLength];
			this.GetBinaryForm(binaryForm, 0);
			return GenericAce.CreateFromBinaryForm(binaryForm, 0);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00069F24 File Offset: 0x00068124
		public sealed override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			GenericAce genericAce = o as GenericAce;
			if (genericAce == null)
			{
				return false;
			}
			if (this.AceType != genericAce.AceType || this.AceFlags != genericAce.AceFlags)
			{
				return false;
			}
			int binaryLength = this.BinaryLength;
			int binaryLength2 = genericAce.BinaryLength;
			if (binaryLength != binaryLength2)
			{
				return false;
			}
			byte[] array = new byte[binaryLength];
			byte[] array2 = new byte[binaryLength2];
			this.GetBinaryForm(array, 0);
			genericAce.GetBinaryForm(array2, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x00069FBC File Offset: 0x000681BC
		public sealed override int GetHashCode()
		{
			int binaryLength = this.BinaryLength;
			byte[] array = new byte[binaryLength];
			this.GetBinaryForm(array, 0);
			int num = 0;
			for (int i = 0; i < binaryLength; i += 4)
			{
				int num2 = (int)array[i] + ((int)array[i + 1] << 8) + ((int)array[i + 2] << 16) + ((int)array[i + 3] << 24);
				num ^= num2;
			}
			return num;
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0006A014 File Offset: 0x00068214
		public static bool operator ==(GenericAce left, GenericAce right)
		{
			return (left == null && right == null) || (left != null && right != null && left.Equals(right));
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0006A03C File Offset: 0x0006823C
		public static bool operator !=(GenericAce left, GenericAce right)
		{
			return !(left == right);
		}

		// Token: 0x04000AE4 RID: 2788
		private readonly AceType _type;

		// Token: 0x04000AE5 RID: 2789
		private AceFlags _flags;

		// Token: 0x04000AE6 RID: 2790
		internal ushort _indexInAcl;

		// Token: 0x04000AE7 RID: 2791
		internal const int HeaderLength = 4;
	}
}
