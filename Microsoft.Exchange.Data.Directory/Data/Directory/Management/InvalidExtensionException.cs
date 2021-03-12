using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000ACC RID: 2764
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidExtensionException : LocalizedException
	{
		// Token: 0x060080BE RID: 32958 RVA: 0x001A5C75 File Offset: 0x001A3E75
		public InvalidExtensionException(string s, int i) : base(DirectoryStrings.ExtensionIsInvalid(s, i))
		{
			this.s = s;
			this.i = i;
		}

		// Token: 0x060080BF RID: 32959 RVA: 0x001A5C92 File Offset: 0x001A3E92
		public InvalidExtensionException(string s, int i, Exception innerException) : base(DirectoryStrings.ExtensionIsInvalid(s, i), innerException)
		{
			this.s = s;
			this.i = i;
		}

		// Token: 0x060080C0 RID: 32960 RVA: 0x001A5CB0 File Offset: 0x001A3EB0
		protected InvalidExtensionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
			this.i = (int)info.GetValue("i", typeof(int));
		}

		// Token: 0x060080C1 RID: 32961 RVA: 0x001A5D05 File Offset: 0x001A3F05
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
			info.AddValue("i", this.i);
		}

		// Token: 0x17002EE5 RID: 12005
		// (get) Token: 0x060080C2 RID: 32962 RVA: 0x001A5D31 File Offset: 0x001A3F31
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x17002EE6 RID: 12006
		// (get) Token: 0x060080C3 RID: 32963 RVA: 0x001A5D39 File Offset: 0x001A3F39
		public int I
		{
			get
			{
				return this.i;
			}
		}

		// Token: 0x040055BF RID: 21951
		private readonly string s;

		// Token: 0x040055C0 RID: 21952
		private readonly int i;
	}
}
