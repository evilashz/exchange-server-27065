using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000011 RID: 17
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class BackendRehydrationException : LocalizedException
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00006495 File Offset: 0x00004695
		public BackendRehydrationException(LocalizedString reason) : base(SecurityStrings.BackendRehydrationException(reason))
		{
			this.reason = reason;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000064AA File Offset: 0x000046AA
		public BackendRehydrationException(LocalizedString reason, Exception innerException) : base(SecurityStrings.BackendRehydrationException(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000064C0 File Offset: 0x000046C0
		protected BackendRehydrationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (LocalizedString)info.GetValue("reason", typeof(LocalizedString));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000064EA File Offset: 0x000046EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000650A File Offset: 0x0000470A
		public LocalizedString Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000126 RID: 294
		private readonly LocalizedString reason;
	}
}
