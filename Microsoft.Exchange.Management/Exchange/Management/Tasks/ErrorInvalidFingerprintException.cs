using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FF3 RID: 4083
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorInvalidFingerprintException : LocalizedException
	{
		// Token: 0x0600AE83 RID: 44675 RVA: 0x0029314D File Offset: 0x0029134D
		public ErrorInvalidFingerprintException(string content) : base(Strings.ErrorInvalidFingerprint(content))
		{
			this.content = content;
		}

		// Token: 0x0600AE84 RID: 44676 RVA: 0x00293162 File Offset: 0x00291362
		public ErrorInvalidFingerprintException(string content, Exception innerException) : base(Strings.ErrorInvalidFingerprint(content), innerException)
		{
			this.content = content;
		}

		// Token: 0x0600AE85 RID: 44677 RVA: 0x00293178 File Offset: 0x00291378
		protected ErrorInvalidFingerprintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.content = (string)info.GetValue("content", typeof(string));
		}

		// Token: 0x0600AE86 RID: 44678 RVA: 0x002931A2 File Offset: 0x002913A2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("content", this.content);
		}

		// Token: 0x170037D4 RID: 14292
		// (get) Token: 0x0600AE87 RID: 44679 RVA: 0x002931BD File Offset: 0x002913BD
		public string Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x0400613A RID: 24890
		private readonly string content;
	}
}
