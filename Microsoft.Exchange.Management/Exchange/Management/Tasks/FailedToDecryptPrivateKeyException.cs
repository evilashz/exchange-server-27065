using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010EA RID: 4330
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToDecryptPrivateKeyException : LocalizedException
	{
		// Token: 0x0600B380 RID: 45952 RVA: 0x0029B549 File Offset: 0x00299749
		public FailedToDecryptPrivateKeyException(Exception e) : base(Strings.FailedToDecryptPrivateKey(e))
		{
			this.e = e;
		}

		// Token: 0x0600B381 RID: 45953 RVA: 0x0029B55E File Offset: 0x0029975E
		public FailedToDecryptPrivateKeyException(Exception e, Exception innerException) : base(Strings.FailedToDecryptPrivateKey(e), innerException)
		{
			this.e = e;
		}

		// Token: 0x0600B382 RID: 45954 RVA: 0x0029B574 File Offset: 0x00299774
		protected FailedToDecryptPrivateKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.e = (Exception)info.GetValue("e", typeof(Exception));
		}

		// Token: 0x0600B383 RID: 45955 RVA: 0x0029B59E File Offset: 0x0029979E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("e", this.e);
		}

		// Token: 0x170038F5 RID: 14581
		// (get) Token: 0x0600B384 RID: 45956 RVA: 0x0029B5B9 File Offset: 0x002997B9
		public Exception E
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x0400625B RID: 25179
		private readonly Exception e;
	}
}
