using System;
using System.IO;
using System.Reflection;
using System.Security;

namespace System.Globalization
{
	// Token: 0x0200038E RID: 910
	internal sealed class GlobalizationAssembly
	{
		// Token: 0x06002E37 RID: 11831 RVA: 0x000B12B8 File Offset: 0x000AF4B8
		[SecurityCritical]
		internal unsafe static byte* GetGlobalizationResourceBytePtr(Assembly assembly, string tableName)
		{
			Stream manifestResourceStream = assembly.GetManifestResourceStream(tableName);
			UnmanagedMemoryStream unmanagedMemoryStream = manifestResourceStream as UnmanagedMemoryStream;
			if (unmanagedMemoryStream != null)
			{
				byte* positionPointer = unmanagedMemoryStream.PositionPointer;
				if (positionPointer != null)
				{
					return positionPointer;
				}
			}
			throw new InvalidOperationException();
		}
	}
}
