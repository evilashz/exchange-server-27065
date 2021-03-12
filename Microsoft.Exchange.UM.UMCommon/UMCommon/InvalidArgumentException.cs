using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001A7 RID: 423
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidArgumentException : LocalizedException
	{
		// Token: 0x06000E6F RID: 3695 RVA: 0x0003508E File Offset: 0x0003328E
		public InvalidArgumentException(string s) : base(Strings.InvalidArgumentException(s))
		{
			this.s = s;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000350A3 File Offset: 0x000332A3
		public InvalidArgumentException(string s, Exception innerException) : base(Strings.InvalidArgumentException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000350B9 File Offset: 0x000332B9
		protected InvalidArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x000350E3 File Offset: 0x000332E3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x000350FE File Offset: 0x000332FE
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04000783 RID: 1923
		private readonly string s;
	}
}
