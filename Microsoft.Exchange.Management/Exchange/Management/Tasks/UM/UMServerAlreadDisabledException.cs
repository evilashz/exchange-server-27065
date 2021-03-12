using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011C0 RID: 4544
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMServerAlreadDisabledException : LocalizedException
	{
		// Token: 0x0600B8AA RID: 47274 RVA: 0x002A4EC9 File Offset: 0x002A30C9
		public UMServerAlreadDisabledException(string s) : base(Strings.UMServerAlreadDisabledException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B8AB RID: 47275 RVA: 0x002A4EDE File Offset: 0x002A30DE
		public UMServerAlreadDisabledException(string s, Exception innerException) : base(Strings.UMServerAlreadDisabledException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B8AC RID: 47276 RVA: 0x002A4EF4 File Offset: 0x002A30F4
		protected UMServerAlreadDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B8AD RID: 47277 RVA: 0x002A4F1E File Offset: 0x002A311E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A27 RID: 14887
		// (get) Token: 0x0600B8AE RID: 47278 RVA: 0x002A4F39 File Offset: 0x002A3139
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006442 RID: 25666
		private readonly string s;
	}
}
