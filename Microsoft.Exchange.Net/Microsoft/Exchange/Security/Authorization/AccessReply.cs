using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000015 RID: 21
	internal class AccessReply
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00004100 File Offset: 0x00002300
		private AccessReply(AccessReply.AuthzAccessReply accessReply, AccessMask accessMaskToCheck)
		{
			this.results = new bool[accessReply.ResultListLength];
			this.grantedAccessMasks = new int[accessReply.ResultListLength];
			this.errors = new int[accessReply.ResultListLength];
			Marshal.Copy(accessReply.GrantedAccessMask, this.grantedAccessMasks, 0, (int)accessReply.ResultListLength);
			Marshal.Copy(accessReply.Error, this.errors, 0, (int)accessReply.ResultListLength);
			int num = 0;
			while ((long)num < (long)((ulong)accessReply.ResultListLength))
			{
				if (this.errors[num] == 0 && this.grantedAccessMasks[num] == (int)accessMaskToCheck)
				{
					this.results[num] = true;
				}
				else
				{
					this.results[num] = false;
				}
				num++;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000041BD File Offset: 0x000023BD
		public int[] GrantedAccessMasks
		{
			get
			{
				return this.grantedAccessMasks;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000041C5 File Offset: 0x000023C5
		public int[] Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000041CD File Offset: 0x000023CD
		public bool[] Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000041D8 File Offset: 0x000023D8
		public static SafeHGlobalHandle AllocateNativeStruct(int replyCount)
		{
			int num = AccessReply.AuthzAccessReply.MarshalSize;
			num += AccessReply.UInt32MarshalSize * replyCount;
			num += AccessReply.UInt32MarshalSize * replyCount;
			SafeHGlobalHandle safeHGlobalHandle = NativeMethods.AllocHGlobal(num);
			AccessReply.AuthzAccessReply authzAccessReply;
			authzAccessReply.ResultListLength = (uint)replyCount;
			authzAccessReply.GrantedAccessMask = (IntPtr)((long)safeHGlobalHandle.DangerousGetHandle() + (long)AccessReply.AuthzAccessReply.MarshalSize);
			authzAccessReply.SaclEvaluationResults = IntPtr.Zero;
			authzAccessReply.Error = (IntPtr)((long)authzAccessReply.GrantedAccessMask + (long)(AccessReply.UInt32MarshalSize * replyCount));
			Marshal.StructureToPtr(authzAccessReply, safeHGlobalHandle.DangerousGetHandle(), false);
			return safeHGlobalHandle;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000426C File Offset: 0x0000246C
		public static AccessReply Create(SafeHGlobalHandle safeHandle)
		{
			return AccessReply.Create(safeHandle, AccessMask.ControlAccess);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004279 File Offset: 0x00002479
		public static AccessReply Create(SafeHGlobalHandle safeHandle, AccessMask accessMaskToCheck)
		{
			return new AccessReply((AccessReply.AuthzAccessReply)Marshal.PtrToStructure(safeHandle.DangerousGetHandle(), typeof(AccessReply.AuthzAccessReply)), accessMaskToCheck);
		}

		// Token: 0x0400005D RID: 93
		private static readonly int UInt32MarshalSize = Marshal.SizeOf(typeof(uint));

		// Token: 0x0400005E RID: 94
		private int[] grantedAccessMasks;

		// Token: 0x0400005F RID: 95
		private int[] errors;

		// Token: 0x04000060 RID: 96
		private bool[] results;

		// Token: 0x02000016 RID: 22
		private struct AuthzAccessReply
		{
			// Token: 0x04000061 RID: 97
			public static readonly int MarshalSize = Marshal.SizeOf(typeof(AccessReply.AuthzAccessReply));

			// Token: 0x04000062 RID: 98
			public uint ResultListLength;

			// Token: 0x04000063 RID: 99
			public IntPtr GrantedAccessMask;

			// Token: 0x04000064 RID: 100
			public IntPtr SaclEvaluationResults;

			// Token: 0x04000065 RID: 101
			public IntPtr Error;
		}
	}
}
