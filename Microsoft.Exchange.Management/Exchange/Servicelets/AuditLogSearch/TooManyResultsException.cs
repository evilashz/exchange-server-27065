using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x02001150 RID: 4432
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TooManyResultsException : LocalizedException
	{
		// Token: 0x0600B579 RID: 46457 RVA: 0x0029E4E9 File Offset: 0x0029C6E9
		public TooManyResultsException(int count) : base(Strings.ErrorAlsTooManyLogResults(count))
		{
			this.count = count;
		}

		// Token: 0x0600B57A RID: 46458 RVA: 0x0029E4FE File Offset: 0x0029C6FE
		public TooManyResultsException(int count, Exception innerException) : base(Strings.ErrorAlsTooManyLogResults(count), innerException)
		{
			this.count = count;
		}

		// Token: 0x0600B57B RID: 46459 RVA: 0x0029E514 File Offset: 0x0029C714
		protected TooManyResultsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.count = (int)info.GetValue("count", typeof(int));
		}

		// Token: 0x0600B57C RID: 46460 RVA: 0x0029E53E File Offset: 0x0029C73E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("count", this.count);
		}

		// Token: 0x17003956 RID: 14678
		// (get) Token: 0x0600B57D RID: 46461 RVA: 0x0029E559 File Offset: 0x0029C759
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x040062BC RID: 25276
		private readonly int count;
	}
}
