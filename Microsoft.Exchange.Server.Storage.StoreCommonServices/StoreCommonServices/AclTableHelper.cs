using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000154 RID: 340
	public static class AclTableHelper
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00042D06 File Offset: 0x00040F06
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x00042D0D File Offset: 0x00040F0D
		internal static Action TestHook
		{
			get
			{
				return AclTableHelper.testHook;
			}
			set
			{
				AclTableHelper.testHook = value;
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00042D18 File Offset: 0x00040F18
		public static FolderSecurity.AclTableAndSecurityDescriptorProperty Parse(IExecutionContext context, byte[] buffer)
		{
			if (buffer == null)
			{
				throw new StoreException((LID)56473U, ErrorCodeValue.InvalidParameter, "AclTableAndSecurityDescriptorProperty parsing failed (null buffer)");
			}
			FolderSecurity.AclTableAndSecurityDescriptorProperty result;
			try
			{
				if (AclTableHelper.TestHook != null)
				{
					AclTableHelper.TestHook();
				}
				result = FolderSecurity.AclTableAndSecurityDescriptorProperty.Parse(buffer);
			}
			catch (ArgumentException ex)
			{
				context.Diagnostics.OnExceptionCatch(ex);
				throw new StoreException((LID)60536U, ErrorCodeValue.InvalidParameter, "AclTableAndSecurityDescriptorProperty parsing failed", ex);
			}
			catch (IndexOutOfRangeException ex2)
			{
				context.Diagnostics.OnExceptionCatch(ex2);
				throw new StoreException((LID)44152U, ErrorCodeValue.InvalidParameter, "AclTableAndSecurityDescriptorProperty parsing failed", ex2);
			}
			catch (EndOfStreamException ex3)
			{
				context.Diagnostics.OnExceptionCatch(ex3);
				throw new StoreException((LID)35960U, ErrorCodeValue.InvalidParameter, "AclTableAndSecurityDescriptorProperty parsing failed", ex3);
			}
			return result;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00042E00 File Offset: 0x00041000
		public static List<FolderSecurity.AclTableEntry> ParseAclTable(IExecutionContext context, ArraySegment<byte> buffer)
		{
			if (buffer.Array == null)
			{
				throw new StoreException((LID)32692U, ErrorCodeValue.InvalidParameter, "AclTableList parsing failed (null buffer)");
			}
			List<FolderSecurity.AclTableEntry> result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(buffer.Array, buffer.Offset, buffer.Count))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						result = FolderSecurity.AclTableEntry.ParseTableEntries(binaryReader);
					}
				}
			}
			catch (OutOfMemoryException ex)
			{
				context.Diagnostics.OnExceptionCatch(ex);
				throw new StoreException((LID)30496U, ErrorCodeValue.InvalidParameter, "AclTableList parsing failed", ex);
			}
			catch (ArgumentException ex2)
			{
				context.Diagnostics.OnExceptionCatch(ex2);
				throw new StoreException((LID)34393U, ErrorCodeValue.InvalidParameter, "AclTableList parsing failed", ex2);
			}
			catch (IndexOutOfRangeException ex3)
			{
				context.Diagnostics.OnExceptionCatch(ex3);
				throw new StoreException((LID)31876U, ErrorCodeValue.InvalidParameter, "AclTableList parsing failed", ex3);
			}
			catch (EndOfStreamException ex4)
			{
				context.Diagnostics.OnExceptionCatch(ex4);
				throw new StoreException((LID)44025U, ErrorCodeValue.InvalidParameter, "AclTableList parsing failed", ex4);
			}
			return result;
		}

		// Token: 0x04000764 RID: 1892
		private static Action testHook;
	}
}
