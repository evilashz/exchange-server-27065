using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000017 RID: 23
	internal class AccessRequest
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x000042C7 File Offset: 0x000024C7
		private AccessRequest()
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000042D0 File Offset: 0x000024D0
		public static SafeHGlobalHandle AllocateNativeStruct(AccessMask requestAccess, Guid[] extendedRightGuids, SecurityIdentifier principalSelfSid)
		{
			int num = (extendedRightGuids == null) ? 0 : extendedRightGuids.GetLength(0);
			int num2 = AccessRequest.AuthzAccessRequest.MarshalSize;
			num2 += AccessRequest.AuthzObjectTypeList.MarshalSize * num;
			num2 += AccessRequest.GuidMarshalSize * num;
			if (principalSelfSid != null)
			{
				num2 += principalSelfSid.BinaryLength;
			}
			SafeHGlobalHandle safeHGlobalHandle = NativeMethods.AllocHGlobal(num2);
			AccessRequest.AuthzAccessRequest authzAccessRequest;
			authzAccessRequest.ObjectTypeListLength = (uint)num;
			IntPtr intPtr = (IntPtr)((long)safeHGlobalHandle.DangerousGetHandle() + (long)AccessRequest.AuthzAccessRequest.MarshalSize);
			if (num != 0)
			{
				authzAccessRequest.ObjectTypeList = intPtr;
				intPtr = (IntPtr)((long)intPtr + (long)(AccessRequest.AuthzObjectTypeList.MarshalSize * num) + (long)(AccessRequest.GuidMarshalSize * num));
			}
			else
			{
				authzAccessRequest.ObjectTypeList = IntPtr.Zero;
			}
			authzAccessRequest.DesiredAccess = requestAccess;
			authzAccessRequest.OptionalArguments = IntPtr.Zero;
			if (principalSelfSid != null)
			{
				authzAccessRequest.PrincipalSelfSid = intPtr;
			}
			else
			{
				authzAccessRequest.PrincipalSelfSid = IntPtr.Zero;
			}
			Marshal.StructureToPtr(authzAccessRequest, safeHGlobalHandle.DangerousGetHandle(), false);
			IntPtr intPtr2 = authzAccessRequest.ObjectTypeList;
			IntPtr intPtr3 = (IntPtr)((long)authzAccessRequest.ObjectTypeList + (long)(AccessRequest.AuthzObjectTypeList.MarshalSize * num));
			for (int i = 0; i < num; i++)
			{
				AccessRequest.AuthzObjectTypeList authzObjectTypeList;
				authzObjectTypeList.Sbz = 0;
				if (i > 0)
				{
					authzObjectTypeList.Level = ObjectLevel.AccessPropertySetGuid;
				}
				else
				{
					authzObjectTypeList.Level = ObjectLevel.AccessObjectGuid;
				}
				authzObjectTypeList.ObjectType = intPtr3;
				Marshal.StructureToPtr(authzObjectTypeList, intPtr2, false);
				Marshal.StructureToPtr(extendedRightGuids[i], intPtr3, false);
				intPtr2 = (IntPtr)((long)intPtr2 + (long)AccessRequest.AuthzObjectTypeList.MarshalSize);
				intPtr3 = (IntPtr)((long)intPtr3 + (long)AccessRequest.GuidMarshalSize);
			}
			if (principalSelfSid != null)
			{
				byte[] array = new byte[principalSelfSid.BinaryLength];
				principalSelfSid.GetBinaryForm(array, 0);
				Marshal.Copy(array, 0, authzAccessRequest.PrincipalSelfSid, principalSelfSid.BinaryLength);
			}
			return safeHGlobalHandle;
		}

		// Token: 0x04000066 RID: 102
		private static readonly int GuidMarshalSize = Marshal.SizeOf(typeof(Guid));

		// Token: 0x02000018 RID: 24
		private struct AuthzAccessRequest
		{
			// Token: 0x04000067 RID: 103
			public static readonly int MarshalSize = Marshal.SizeOf(typeof(AccessRequest.AuthzAccessRequest));

			// Token: 0x04000068 RID: 104
			public AccessMask DesiredAccess;

			// Token: 0x04000069 RID: 105
			public IntPtr PrincipalSelfSid;

			// Token: 0x0400006A RID: 106
			public IntPtr ObjectTypeList;

			// Token: 0x0400006B RID: 107
			public uint ObjectTypeListLength;

			// Token: 0x0400006C RID: 108
			public IntPtr OptionalArguments;
		}

		// Token: 0x02000019 RID: 25
		private struct AuthzObjectTypeList
		{
			// Token: 0x0400006D RID: 109
			public static readonly int MarshalSize = Marshal.SizeOf(typeof(AccessRequest.AuthzObjectTypeList));

			// Token: 0x0400006E RID: 110
			public ObjectLevel Level;

			// Token: 0x0400006F RID: 111
			public ushort Sbz;

			// Token: 0x04000070 RID: 112
			public IntPtr ObjectType;
		}
	}
}
