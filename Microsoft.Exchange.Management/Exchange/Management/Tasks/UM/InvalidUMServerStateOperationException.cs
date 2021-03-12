using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011C1 RID: 4545
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidUMServerStateOperationException : LocalizedException
	{
		// Token: 0x0600B8AF RID: 47279 RVA: 0x002A4F41 File Offset: 0x002A3141
		public InvalidUMServerStateOperationException(string s) : base(Strings.InvalidUMServerStateOperationException(s))
		{
			this.s = s;
		}

		// Token: 0x0600B8B0 RID: 47280 RVA: 0x002A4F56 File Offset: 0x002A3156
		public InvalidUMServerStateOperationException(string s, Exception innerException) : base(Strings.InvalidUMServerStateOperationException(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600B8B1 RID: 47281 RVA: 0x002A4F6C File Offset: 0x002A316C
		protected InvalidUMServerStateOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600B8B2 RID: 47282 RVA: 0x002A4F96 File Offset: 0x002A3196
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x17003A28 RID: 14888
		// (get) Token: 0x0600B8B3 RID: 47283 RVA: 0x002A4FB1 File Offset: 0x002A31B1
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006443 RID: 25667
		private readonly string s;
	}
}
