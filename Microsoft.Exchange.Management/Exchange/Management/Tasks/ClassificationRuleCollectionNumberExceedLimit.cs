using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FD7 RID: 4055
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionNumberExceedLimit : LocalizedException
	{
		// Token: 0x0600AE02 RID: 44546 RVA: 0x00292779 File Offset: 0x00290979
		public ClassificationRuleCollectionNumberExceedLimit(int limit) : base(Strings.ClassificationRuleCollectionNumberExceedLimit(limit))
		{
			this.limit = limit;
		}

		// Token: 0x0600AE03 RID: 44547 RVA: 0x0029278E File Offset: 0x0029098E
		public ClassificationRuleCollectionNumberExceedLimit(int limit, Exception innerException) : base(Strings.ClassificationRuleCollectionNumberExceedLimit(limit), innerException)
		{
			this.limit = limit;
		}

		// Token: 0x0600AE04 RID: 44548 RVA: 0x002927A4 File Offset: 0x002909A4
		protected ClassificationRuleCollectionNumberExceedLimit(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.limit = (int)info.GetValue("limit", typeof(int));
		}

		// Token: 0x0600AE05 RID: 44549 RVA: 0x002927CE File Offset: 0x002909CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("limit", this.limit);
		}

		// Token: 0x170037C3 RID: 14275
		// (get) Token: 0x0600AE06 RID: 44550 RVA: 0x002927E9 File Offset: 0x002909E9
		public int Limit
		{
			get
			{
				return this.limit;
			}
		}

		// Token: 0x04006129 RID: 24873
		private readonly int limit;
	}
}
