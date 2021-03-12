using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001109 RID: 4361
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FatalAutodiscoverException : LocalizedException
	{
		// Token: 0x0600B41E RID: 46110 RVA: 0x0029C531 File Offset: 0x0029A731
		public FatalAutodiscoverException(string failure) : base(Strings.messageFatalAutodiscoverException(failure))
		{
			this.failure = failure;
		}

		// Token: 0x0600B41F RID: 46111 RVA: 0x0029C546 File Offset: 0x0029A746
		public FatalAutodiscoverException(string failure, Exception innerException) : base(Strings.messageFatalAutodiscoverException(failure), innerException)
		{
			this.failure = failure;
		}

		// Token: 0x0600B420 RID: 46112 RVA: 0x0029C55C File Offset: 0x0029A75C
		protected FatalAutodiscoverException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failure = (string)info.GetValue("failure", typeof(string));
		}

		// Token: 0x0600B421 RID: 46113 RVA: 0x0029C586 File Offset: 0x0029A786
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failure", this.failure);
		}

		// Token: 0x17003917 RID: 14615
		// (get) Token: 0x0600B422 RID: 46114 RVA: 0x0029C5A1 File Offset: 0x0029A7A1
		public string Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x0400627D RID: 25213
		private readonly string failure;
	}
}
