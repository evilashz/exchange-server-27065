using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011FD RID: 4605
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OperationNotSupportedOnLegacMailboxException : LocalizedException
	{
		// Token: 0x0600B9C9 RID: 47561 RVA: 0x002A6695 File Offset: 0x002A4895
		public OperationNotSupportedOnLegacMailboxException(string use) : base(Strings.OperationNotSupportedOnLegacMailboxException(use))
		{
			this.use = use;
		}

		// Token: 0x0600B9CA RID: 47562 RVA: 0x002A66AA File Offset: 0x002A48AA
		public OperationNotSupportedOnLegacMailboxException(string use, Exception innerException) : base(Strings.OperationNotSupportedOnLegacMailboxException(use), innerException)
		{
			this.use = use;
		}

		// Token: 0x0600B9CB RID: 47563 RVA: 0x002A66C0 File Offset: 0x002A48C0
		protected OperationNotSupportedOnLegacMailboxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.use = (string)info.GetValue("use", typeof(string));
		}

		// Token: 0x0600B9CC RID: 47564 RVA: 0x002A66EA File Offset: 0x002A48EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("use", this.use);
		}

		// Token: 0x17003A52 RID: 14930
		// (get) Token: 0x0600B9CD RID: 47565 RVA: 0x002A6705 File Offset: 0x002A4905
		public string Use
		{
			get
			{
				return this.use;
			}
		}

		// Token: 0x0400646D RID: 25709
		private readonly string use;
	}
}
