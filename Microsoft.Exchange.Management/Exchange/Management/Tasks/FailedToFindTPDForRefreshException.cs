using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010DB RID: 4315
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToFindTPDForRefreshException : LocalizedException
	{
		// Token: 0x0600B337 RID: 45879 RVA: 0x0029AE8B File Offset: 0x0029908B
		public FailedToFindTPDForRefreshException(string name) : base(Strings.FailedToFindTPDForRefresh(name))
		{
			this.name = name;
		}

		// Token: 0x0600B338 RID: 45880 RVA: 0x0029AEA0 File Offset: 0x002990A0
		public FailedToFindTPDForRefreshException(string name, Exception innerException) : base(Strings.FailedToFindTPDForRefresh(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600B339 RID: 45881 RVA: 0x0029AEB6 File Offset: 0x002990B6
		protected FailedToFindTPDForRefreshException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600B33A RID: 45882 RVA: 0x0029AEE0 File Offset: 0x002990E0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170038E8 RID: 14568
		// (get) Token: 0x0600B33B RID: 45883 RVA: 0x0029AEFB File Offset: 0x002990FB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400624E RID: 25166
		private readonly string name;
	}
}
