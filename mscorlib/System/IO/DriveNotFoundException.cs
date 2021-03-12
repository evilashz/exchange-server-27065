﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000180 RID: 384
	[ComVisible(true)]
	[Serializable]
	public class DriveNotFoundException : IOException
	{
		// Token: 0x06001795 RID: 6037 RVA: 0x0004BA81 File Offset: 0x00049C81
		public DriveNotFoundException() : base(Environment.GetResourceString("Arg_DriveNotFoundException"))
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0004BA9E File Offset: 0x00049C9E
		public DriveNotFoundException(string message) : base(message)
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0004BAB2 File Offset: 0x00049CB2
		public DriveNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0004BAC7 File Offset: 0x00049CC7
		protected DriveNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
