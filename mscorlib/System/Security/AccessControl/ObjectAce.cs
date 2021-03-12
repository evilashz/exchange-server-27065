﻿using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000209 RID: 521
	public sealed class ObjectAce : QualifiedAce
	{
		// Token: 0x06001E76 RID: 7798 RVA: 0x0006A6FD File Offset: 0x000688FD
		public ObjectAce(AceFlags aceFlags, AceQualifier qualifier, int accessMask, SecurityIdentifier sid, ObjectAceFlags flags, Guid type, Guid inheritedType, bool isCallback, byte[] opaque) : base(ObjectAce.TypeFromQualifier(isCallback, qualifier), aceFlags, accessMask, sid, opaque)
		{
			this._objectFlags = flags;
			this._objectAceType = type;
			this._inheritedObjectAceType = inheritedType;
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0006A72C File Offset: 0x0006892C
		private static AceType TypeFromQualifier(bool isCallback, AceQualifier qualifier)
		{
			switch (qualifier)
			{
			case AceQualifier.AccessAllowed:
				if (!isCallback)
				{
					return AceType.AccessAllowedObject;
				}
				return AceType.AccessAllowedCallbackObject;
			case AceQualifier.AccessDenied:
				if (!isCallback)
				{
					return AceType.AccessDeniedObject;
				}
				return AceType.AccessDeniedCallbackObject;
			case AceQualifier.SystemAudit:
				if (!isCallback)
				{
					return AceType.SystemAuditObject;
				}
				return AceType.SystemAuditCallbackObject;
			case AceQualifier.SystemAlarm:
				if (!isCallback)
				{
					return AceType.SystemAlarmObject;
				}
				return AceType.SystemAlarmCallbackObject;
			default:
				throw new ArgumentOutOfRangeException("qualifier", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0006A788 File Offset: 0x00068988
		internal bool ObjectTypesMatch(ObjectAceFlags objectFlags, Guid objectType)
		{
			return (this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == (objectFlags & ObjectAceFlags.ObjectAceTypePresent) && ((this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None || this.ObjectAceType.Equals(objectType));
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0006A7C4 File Offset: 0x000689C4
		internal bool InheritedObjectTypesMatch(ObjectAceFlags objectFlags, Guid inheritedObjectType)
		{
			return (this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == (objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) && ((this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None || this.InheritedObjectAceType.Equals(inheritedObjectType));
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0006A800 File Offset: 0x00068A00
		internal static bool ParseBinaryForm(byte[] binaryForm, int offset, out AceQualifier qualifier, out int accessMask, out SecurityIdentifier sid, out ObjectAceFlags objectFlags, out Guid objectAceType, out Guid inheritedObjectAceType, out bool isCallback, out byte[] opaque)
		{
			byte[] array = new byte[16];
			GenericAce.VerifyHeader(binaryForm, offset);
			if (binaryForm.Length - offset >= 12 + SecurityIdentifier.MinBinaryLength)
			{
				AceType aceType = (AceType)binaryForm[offset];
				if (aceType == AceType.AccessAllowedObject || aceType == AceType.AccessDeniedObject || aceType == AceType.SystemAuditObject || aceType == AceType.SystemAlarmObject)
				{
					isCallback = false;
				}
				else
				{
					if (aceType != AceType.AccessAllowedCallbackObject && aceType != AceType.AccessDeniedCallbackObject && aceType != AceType.SystemAuditCallbackObject && aceType != AceType.SystemAlarmCallbackObject)
					{
						goto IL_209;
					}
					isCallback = true;
				}
				if (aceType == AceType.AccessAllowedObject || aceType == AceType.AccessAllowedCallbackObject)
				{
					qualifier = AceQualifier.AccessAllowed;
				}
				else if (aceType == AceType.AccessDeniedObject || aceType == AceType.AccessDeniedCallbackObject)
				{
					qualifier = AceQualifier.AccessDenied;
				}
				else if (aceType == AceType.SystemAuditObject || aceType == AceType.SystemAuditCallbackObject)
				{
					qualifier = AceQualifier.SystemAudit;
				}
				else
				{
					if (aceType != AceType.SystemAlarmObject && aceType != AceType.SystemAlarmCallbackObject)
					{
						goto IL_209;
					}
					qualifier = AceQualifier.SystemAlarm;
				}
				int num = offset + 4;
				int num2 = 0;
				accessMask = (int)binaryForm[num] + ((int)binaryForm[num + 1] << 8) + ((int)binaryForm[num + 2] << 16) + ((int)binaryForm[num + 3] << 24);
				num2 += 4;
				objectFlags = (ObjectAceFlags)((int)binaryForm[num + num2] + ((int)binaryForm[num + num2 + 1] << 8) + ((int)binaryForm[num + num2 + 2] << 16) + ((int)binaryForm[num + num2 + 3] << 24));
				num2 += 4;
				if ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
				{
					for (int i = 0; i < 16; i++)
					{
						array[i] = binaryForm[num + num2 + i];
					}
					num2 += 16;
				}
				else
				{
					for (int j = 0; j < 16; j++)
					{
						array[j] = 0;
					}
				}
				objectAceType = new Guid(array);
				if ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
				{
					for (int k = 0; k < 16; k++)
					{
						array[k] = binaryForm[num + num2 + k];
					}
					num2 += 16;
				}
				else
				{
					for (int l = 0; l < 16; l++)
					{
						array[l] = 0;
					}
				}
				inheritedObjectAceType = new Guid(array);
				sid = new SecurityIdentifier(binaryForm, num + num2);
				opaque = null;
				int num3 = ((int)binaryForm[offset + 3] << 8) + (int)binaryForm[offset + 2];
				if (num3 % 4 == 0)
				{
					int num4 = num3 - 4 - 4 - 4 - (int)((byte)sid.BinaryLength);
					if ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
					{
						num4 -= 16;
					}
					if ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
					{
						num4 -= 16;
					}
					if (num4 > 0)
					{
						opaque = new byte[num4];
						for (int m = 0; m < num4; m++)
						{
							opaque[m] = binaryForm[offset + num3 - num4 + m];
						}
					}
					return true;
				}
			}
			IL_209:
			qualifier = AceQualifier.AccessAllowed;
			accessMask = 0;
			sid = null;
			objectFlags = ObjectAceFlags.None;
			objectAceType = Guid.NewGuid();
			inheritedObjectAceType = Guid.NewGuid();
			isCallback = false;
			opaque = null;
			return false;
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x0006AA45 File Offset: 0x00068C45
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x0006AA4D File Offset: 0x00068C4D
		public ObjectAceFlags ObjectAceFlags
		{
			get
			{
				return this._objectFlags;
			}
			set
			{
				this._objectFlags = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0006AA56 File Offset: 0x00068C56
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x0006AA5E File Offset: 0x00068C5E
		public Guid ObjectAceType
		{
			get
			{
				return this._objectAceType;
			}
			set
			{
				this._objectAceType = value;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x0006AA67 File Offset: 0x00068C67
		// (set) Token: 0x06001E80 RID: 7808 RVA: 0x0006AA6F File Offset: 0x00068C6F
		public Guid InheritedObjectAceType
		{
			get
			{
				return this._inheritedObjectAceType;
			}
			set
			{
				this._inheritedObjectAceType = value;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x0006AA78 File Offset: 0x00068C78
		public override int BinaryLength
		{
			get
			{
				int num = (((this._objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None) ? 16 : 0) + (((this._objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None) ? 16 : 0);
				return 12 + num + base.SecurityIdentifier.BinaryLength + base.OpaqueLength;
			}
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x0006AABC File Offset: 0x00068CBC
		public static int MaxOpaqueLength(bool isCallback)
		{
			return 65491 - SecurityIdentifier.MaxBinaryLength;
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x0006AAC9 File Offset: 0x00068CC9
		internal override int MaxOpaqueLengthInternal
		{
			get
			{
				return ObjectAce.MaxOpaqueLength(base.IsCallback);
			}
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0006AAD8 File Offset: 0x00068CD8
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			base.MarshalHeader(binaryForm, offset);
			int num = offset + 4;
			int num2 = 0;
			binaryForm[num] = (byte)base.AccessMask;
			binaryForm[num + 1] = (byte)(base.AccessMask >> 8);
			binaryForm[num + 2] = (byte)(base.AccessMask >> 16);
			binaryForm[num + 3] = (byte)(base.AccessMask >> 24);
			num2 += 4;
			binaryForm[num + num2] = (byte)this.ObjectAceFlags;
			binaryForm[num + num2 + 1] = (byte)(this.ObjectAceFlags >> 8);
			binaryForm[num + num2 + 2] = (byte)(this.ObjectAceFlags >> 16);
			binaryForm[num + num2 + 3] = (byte)(this.ObjectAceFlags >> 24);
			num2 += 4;
			if ((this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
			{
				this.ObjectAceType.ToByteArray().CopyTo(binaryForm, num + num2);
				num2 += 16;
			}
			if ((this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
			{
				this.InheritedObjectAceType.ToByteArray().CopyTo(binaryForm, num + num2);
				num2 += 16;
			}
			base.SecurityIdentifier.GetBinaryForm(binaryForm, num + num2);
			num2 += base.SecurityIdentifier.BinaryLength;
			if (base.GetOpaque() != null)
			{
				if (base.OpaqueLength > this.MaxOpaqueLengthInternal)
				{
					throw new SystemException();
				}
				base.GetOpaque().CopyTo(binaryForm, num + num2);
			}
		}

		// Token: 0x04000AFD RID: 2813
		private ObjectAceFlags _objectFlags;

		// Token: 0x04000AFE RID: 2814
		private Guid _objectAceType;

		// Token: 0x04000AFF RID: 2815
		private Guid _inheritedObjectAceType;

		// Token: 0x04000B00 RID: 2816
		private const int ObjectFlagsLength = 4;

		// Token: 0x04000B01 RID: 2817
		private const int GuidLength = 16;

		// Token: 0x04000B02 RID: 2818
		internal static readonly int AccessMaskWithObjectType = 315;
	}
}
