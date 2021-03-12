using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004C2 RID: 1218
	[StructLayout(LayoutKind.Sequential)]
	internal class MetadataRecord : IDisposable
	{
		// Token: 0x06002A75 RID: 10869 RVA: 0x000AAB5D File Offset: 0x000A8D5D
		internal MetadataRecord(int bufferSize)
		{
			if (bufferSize > 0)
			{
				this.DataBuf = new SafeHGlobalHandle(Marshal.AllocHGlobal(bufferSize));
			}
			else
			{
				this.DataBuf = SafeHGlobalHandle.InvalidHandle;
			}
			this.DataLen = bufferSize;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000AAB8E File Offset: 0x000A8D8E
		internal MetadataRecord(string value)
		{
			this.DataBuf = new SafeHGlobalHandle(Marshal.StringToHGlobalUni(value));
			this.DataLen = (value.Length + 1) * 2;
			this.DataType = MBDataType.String;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000AABBE File Offset: 0x000A8DBE
		public void Dispose()
		{
			if (this.DataBuf != null)
			{
				this.DataBuf.Dispose();
			}
		}

		// Token: 0x04001FA7 RID: 8103
		public MBIdentifier Identifier;

		// Token: 0x04001FA8 RID: 8104
		public MBAttributes Attributes;

		// Token: 0x04001FA9 RID: 8105
		public MBUserType UserType;

		// Token: 0x04001FAA RID: 8106
		public MBDataType DataType;

		// Token: 0x04001FAB RID: 8107
		public int DataLen;

		// Token: 0x04001FAC RID: 8108
		public SafeHGlobalHandle DataBuf;

		// Token: 0x04001FAD RID: 8109
		public int DataTag;
	}
}
