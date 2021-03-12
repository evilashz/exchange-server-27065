using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ABC RID: 2748
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidCrossTenantIdFormatException : LocalizedException
	{
		// Token: 0x06008068 RID: 32872 RVA: 0x001A52D9 File Offset: 0x001A34D9
		public InvalidCrossTenantIdFormatException(string str) : base(DirectoryStrings.InvalidCrossTenantIdFormat(str))
		{
			this.str = str;
		}

		// Token: 0x06008069 RID: 32873 RVA: 0x001A52EE File Offset: 0x001A34EE
		public InvalidCrossTenantIdFormatException(string str, Exception innerException) : base(DirectoryStrings.InvalidCrossTenantIdFormat(str), innerException)
		{
			this.str = str;
		}

		// Token: 0x0600806A RID: 32874 RVA: 0x001A5304 File Offset: 0x001A3504
		protected InvalidCrossTenantIdFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.str = (string)info.GetValue("str", typeof(string));
		}

		// Token: 0x0600806B RID: 32875 RVA: 0x001A532E File Offset: 0x001A352E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("str", this.str);
		}

		// Token: 0x17002ECF RID: 11983
		// (get) Token: 0x0600806C RID: 32876 RVA: 0x001A5349 File Offset: 0x001A3549
		public string Str
		{
			get
			{
				return this.str;
			}
		}

		// Token: 0x040055A9 RID: 21929
		private readonly string str;
	}
}
