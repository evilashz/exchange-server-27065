using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EED RID: 3821
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplServiceNotRunningOnNodeException : LocalizedException
	{
		// Token: 0x0600A98A RID: 43402 RVA: 0x0028BF0E File Offset: 0x0028A10E
		public ReplServiceNotRunningOnNodeException(string nodeName) : base(Strings.ReplServiceNotRunningOnNodeException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x0600A98B RID: 43403 RVA: 0x0028BF23 File Offset: 0x0028A123
		public ReplServiceNotRunningOnNodeException(string nodeName, Exception innerException) : base(Strings.ReplServiceNotRunningOnNodeException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x0600A98C RID: 43404 RVA: 0x0028BF39 File Offset: 0x0028A139
		protected ReplServiceNotRunningOnNodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x0600A98D RID: 43405 RVA: 0x0028BF63 File Offset: 0x0028A163
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x170036F3 RID: 14067
		// (get) Token: 0x0600A98E RID: 43406 RVA: 0x0028BF7E File Offset: 0x0028A17E
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x04006059 RID: 24665
		private readonly string nodeName;
	}
}
