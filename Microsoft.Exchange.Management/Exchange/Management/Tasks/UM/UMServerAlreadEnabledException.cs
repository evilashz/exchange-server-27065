using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011BF RID: 4543
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMServerAlreadEnabledException : LocalizedException
	{
		// Token: 0x0600B8A5 RID: 47269 RVA: 0x002A4E51 File Offset: 0x002A3051
		public UMServerAlreadEnabledException(string s) : base(Strings.UMServerAlreadEnabledException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B8A6 RID: 47270 RVA: 0x002A4E66 File Offset: 0x002A3066
		public UMServerAlreadEnabledException(string s, Exception innerException) : base(Strings.UMServerAlreadEnabledException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B8A7 RID: 47271 RVA: 0x002A4E7C File Offset: 0x002A307C
		protected UMServerAlreadEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B8A8 RID: 47272 RVA: 0x002A4EA6 File Offset: 0x002A30A6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A26 RID: 14886
		// (get) Token: 0x0600B8A9 RID: 47273 RVA: 0x002A4EC1 File Offset: 0x002A30C1
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006441 RID: 25665
		private readonly string s;
	}
}
